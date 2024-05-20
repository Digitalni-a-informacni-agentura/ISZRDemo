// -----------------------------------------------------------------------
// <copyright file="EgonServices.cs" company="AucoContCZ">
// Zdrojový kód aplikace i přeloženou aplikaci lze použít libovolným způsobem. 
// Tvůrce aplikace a ostatních částí (aplikace, zdrojové kódy, dokumentace, 
// certifikát, ostatní soubory a další) není v žádném ohledu odpovědný za jakýkoliv 
// důsledek přímo nebo nepřímo vzniklý v souvislosti s libovolnou částí díla.
// </copyright>
// -----------------------------------------------------------------------

namespace Autocont.ISZRDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ServiceModel.Channels;
    using System.ServiceModel;
    using System.Reflection;
    using System.Net;
    using Autocont.ISZRDemo.Cls;

    /// <summary>
    /// Trida volajici eGON sluzby
    /// </summary>
    public class EgonServices
    {
        #region konstanty
        /// <summary>
        /// timeout v sekundach pro volani sluzby
        /// </summary>
        private const int Timeout = 60;
        #endregion

        #region metody pomocne
        //------------------------------------------------------------------------------------
        /// <summary>
        /// konstukce URL apriblusneho bindingu, osetreni proxy
        /// </summary>
        /// <param name="volani"></param>
        /// <param name="svc"></param>
        /// <param name="lzeAsync"></param>
        /// <param name="bind"></param>
        /// <returns></returns>
        private static EndpointAddress GetEgonAddr(Volani volani, String svc, bool lzeAsync, out Binding bind)
        {
            String addr = volani.Prostredi.GetServiceUrl(svc, lzeAsync);
            bind = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            BasicHttpBinding bbind = (BasicHttpBinding)bind;
            bbind.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
            bbind.MaxReceivedMessageSize = 65536 * 16;
            TimeSpan tmo = new TimeSpan(0, 0, Timeout);
            bind.SendTimeout = tmo;
            bind.ReceiveTimeout = tmo;
            //
            ProxySetup ps = volani.Prostredi.Proxy;
            if (!ps.UseSystem)
            {
                WebRequest.DefaultWebProxy = null;
                volani.Log.Log("Proxy: vypnuto");
            }
            else
            {
                if (ps.UseDefaultNetworkCredentials)
                {
                    WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                }
                else
                {
                    WebRequest.DefaultWebProxy.Credentials = new NetworkCredential(ps.UserName, ps.Password, ps.Domain);
                }
            }
			if (volani.Prostredi.Tls12)
			{
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.MaxServicePointIdleTime = 1;
			}
			else
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
				ServicePointManager.MaxServicePointIdleTime = 1;
			}
            //
            EndpointAddress adr = new EndpointAddress(addr);
            volani.Log.Log("URL služby: " + addr);
            return adr;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// nastaveni vlastnosti objektu
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetProperty(object obj, string name, object value)
        {
            PropertyInfo inf = obj.GetType().GetProperty(name);
            inf.SetValue(obj, value, new object[] { });
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// cteni vlastnosti objektu
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static String GetProperty(object resp, string prop)
        {
            if (resp == null) return String.Empty;
            int i = prop.IndexOf('/');
            String pf = prop;
            if (i > 0) pf = prop.Substring(0, i);
            PropertyInfo inf = null;
            int infidx = -1;
            int j = pf.IndexOf("[");
            if (j > 0)
            {
                String px1 = pf.Substring(0, j);
                String px2 = pf.Substring(j + 1).TrimEnd(new char[] { ' ', ']' });
                inf = resp.GetType().GetProperty(px1);
                infidx = Convert.ToInt32(px2);
            }
            else
            {
                inf = resp.GetType().GetProperty(pf);
            }
            if (inf != null)
            {
                object o = null;
                if (infidx < 0) o = inf.GetValue(resp, new object[] { });
                else
                {
                    o = inf.GetValue(resp, new object[] { });
                    if (o == null) return String.Empty;
                    try
                    {
                        o = ((System.Array)o).GetValue(infidx); 
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return String.Empty;
                    }
                }
                if (i < 0)
                {
                    if (o == null) return String.Empty;
                    return o.ToString();
                }
                else
                {
                    return GetProperty(o, prop.Substring(i + 1));
                }
            }
            return String.Empty;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// naplneni systemove hlavicky dotazu
        /// </summary>
        /// <param name="volani"></param>
        /// <param name="zadostInfo"></param>
        private static void FillSystemPart(Volani volani, object zadostInfo)
        {
            SetProperty(zadostInfo, "CasZadosti", DateTime.Now);
            SetProperty(zadostInfo, "Agenda", volani.Hlavicka.Agenda);
            SetProperty(zadostInfo, "AgendovaRole", volani.Hlavicka.Role);
            SetProperty(zadostInfo, "Ovm", volani.Hlavicka.Ovm);
            SetProperty(zadostInfo, "Ais", volani.Hlavicka.Ais);
            SetProperty(zadostInfo, "AisSpecified", true);
            SetProperty(zadostInfo, "AgendaZadostId", Guid.NewGuid().ToString());
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// zapis do logu historie volani
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="volani"></param>
        /// <param name="resp"></param>
        private static void LogHistory(String svc, Volani volani, object resp)
        {
            String iszrZadostId = GetProperty(resp, "OdpovedInfo/IszrZadostId");
            String vysledekKod = GetProperty(resp, "OdpovedInfo/Status/VysledekKod");
            volani.Log.LogHistory(
                svc + (volani.Prostredi.Sync? "" : "-async")+ ": " + 
                vysledekKod + 
                (String.IsNullOrEmpty(iszrZadostId) ? "" : ", IszrZadostId=" + iszrZadostId)
            );
        }
        #endregion
        #region metody - eGON sluzby
        //------------------------------------------------------------------------------------
        /// <summary>
        /// E278 - robCtiPodleUdaju2 (vybrane udaje)
        /// </summary>
        /// <param name="volani"></param>
        /// <param name="e278ZadostData"></param>
        internal static void E278(Volani volani, E278ZadostData e278ZadostData)
        {
            String svc = "E278";
            Binding bind = null;
            EndpointAddress adr = GetEgonAddr(volani, svc, true, out bind);
            //
            RobCtiPodleUdaju2.IszrRobCtiPodleUdaju2PortTypeClient cl = new RobCtiPodleUdaju2.IszrRobCtiPodleUdaju2PortTypeClient(bind, adr);
            //
            cl.ClientCredentials.ClientCertificate.Certificate = volani.Certifikat;
            //
            RobCtiPodleUdaju2.RobCtiPodleUdaju2Type req = new RobCtiPodleUdaju2.RobCtiPodleUdaju2Type();
            //
            req.ZadostInfo = new RobCtiPodleUdaju2.ZadostInfoIszrType();
            FillSystemPart(volani, req.ZadostInfo);
            // pri cteni ROB je vyzadovano
            req.ZadostInfo.Subjekt = "Subjekt";
            req.ZadostInfo.Uzivatel = "Uzivatel156";
            req.ZadostInfo.DuvodUcel = "Duvod a ucel";
            //
            req.AutorizaceInfo = new RobCtiPodleUdaju2.AutorizaceType();
            req.AutorizaceInfo.SeznamUdaju = volani.Hlavicka.AutorizaceInfo;
            //
            req.Zadost = new RobCtiPodleUdaju2.RobCtiPodleUdaju2TypeZadost();
            req.Zadost.RobCtiPodleUdaju2Data = new RobCtiPodleUdaju2.RobCtiPodleUdaju2DataType();
            if (e278ZadostData.PouzitPrijmeni)
            {
                req.Zadost.RobCtiPodleUdaju2Data.Prijmeni = string.IsNullOrEmpty(e278ZadostData.Prijmeni) ? null : e278ZadostData.Prijmeni;
                req.Zadost.RobCtiPodleUdaju2Data.PrijmeniSpecified = true;
            }
            if (e278ZadostData.PouzitJmeno)
            {
                req.Zadost.RobCtiPodleUdaju2Data.Jmeno = string.IsNullOrEmpty(e278ZadostData.Jmeno) ? null : e278ZadostData.Jmeno;
                req.Zadost.RobCtiPodleUdaju2Data.JmenoSpecified = true;
            }
            if (e278ZadostData.PouzitAdresa)
            {
                if (e278ZadostData.Adresa != null)
                {
                    req.Zadost.RobCtiPodleUdaju2Data.AdresaPobytu = new RobCtiPodleUdaju2.AdresaPobytuType();
                    req.Zadost.RobCtiPodleUdaju2Data.AdresaPobytu.Value = e278ZadostData.Adresa.Value;
                }
                req.Zadost.RobCtiPodleUdaju2Data.AdresaPobytuSpecified = true;
            }
            if (e278ZadostData.DatumNarozeni != null)
            {
                req.Zadost.RobCtiPodleUdaju2Data.DatumNarozeni = e278ZadostData.DatumNarozeni;
                req.Zadost.RobCtiPodleUdaju2Data.DatumNarozeniSpecified = true;
            }
            if (e278ZadostData.DatumUmrti != null)
            {
                req.Zadost.RobCtiPodleUdaju2Data.DatumUmrti = e278ZadostData.DatumUmrti;
                req.Zadost.RobCtiPodleUdaju2Data.DatumUmrtiSpecified = true;
            }
            if (e278ZadostData.PouzitDatovaSchrankaId)
            {
                req.Zadost.RobCtiPodleUdaju2Data.DatovaSchrankaId = e278ZadostData.DatovaSchrankaId;
                req.Zadost.RobCtiPodleUdaju2Data.DatovaSchrankaIdSpecified = true;
            }
            if (e278ZadostData.PouzitMistoNarozeni)
            {
                if (e278ZadostData.MistoNarozeni != null)
                {
                    req.Zadost.RobCtiPodleUdaju2Data.MistoNarozeni = new RobCtiPodleUdaju2.MistoNarozeniType();
                    var mistoCr = new RobCtiPodleUdaju2.MistoCrType();
                    mistoCr.Value = e278ZadostData.MistoNarozeni.Value;
                    req.Zadost.RobCtiPodleUdaju2Data.MistoNarozeni.Item = mistoCr;
                }
                req.Zadost.RobCtiPodleUdaju2Data.MistoNarozeniSpecified = true;
            }
            if (e278ZadostData.PouzitMistoUmrti)
            {
                if (e278ZadostData.MistoUmrti != null)
                {
                    req.Zadost.RobCtiPodleUdaju2Data.MistoUmrti = new RobCtiPodleUdaju2.MistoUmrtiType();
                    var mistoCr = new RobCtiPodleUdaju2.MistoCrType();
                    mistoCr.Value = e278ZadostData.MistoUmrti.Value;
                    req.Zadost.RobCtiPodleUdaju2Data.MistoUmrti.Item = mistoCr;
                }
                req.Zadost.RobCtiPodleUdaju2Data.MistoUmrtiSpecified = true;
            }
            if (e278ZadostData.PouzitRodnePrijmeni)
            {
                req.Zadost.RobCtiPodleUdaju2Data.RodnePrijmeni = string.IsNullOrEmpty(e278ZadostData.RodnePrijmeni) ? null : e278ZadostData.RodnePrijmeni;
                req.Zadost.RobCtiPodleUdaju2Data.RodnePrijmeniSpecified = true;
            }
            if (e278ZadostData.PouzitOmezeniSvepravnosti)
            {
                req.Zadost.RobCtiPodleUdaju2Data.OmezeniSvepravnosti = e278ZadostData.OmezeniSvepravnosti;
                req.Zadost.RobCtiPodleUdaju2Data.OmezeniSvepravnostiSpecified = true;
            }
            if (e278ZadostData.PouzitRodinnyStav)
            {
                req.Zadost.RobCtiPodleUdaju2Data.RodinnyStavPartnerstvi = e278ZadostData.RodinnyStav;
                req.Zadost.RobCtiPodleUdaju2Data.RodinnyStavPartnerstviSpecified = true;
            }
            if (e278ZadostData.PouzitPohlavi)
            {
                req.Zadost.RobCtiPodleUdaju2Data.Pohlavi = e278ZadostData.Pohlavi;
                req.Zadost.RobCtiPodleUdaju2Data.PohlaviSpecified = true;
            }
            //
            cl.Endpoint.Behaviors.Add(new MessageLoggerBehavior(volani.Log));
            RobCtiPodleUdaju2.RobCtiPodleUdaju2ResponseType resp = cl.IszrRobCtiPodleUdaju2(req);
            LogHistory(svc, volani, resp);
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// E256 - rosCtiIco2
        /// </summary>
        /// <param name="volani"></param>
        /// <param name="ico"></param>
        internal static void E256(Volani volani, int ico)
        {
            String svc = "E256";
            Binding bind = null;
            EndpointAddress adr = GetEgonAddr(volani, svc, true, out bind);
            //
            RosCtiIco2.IszrRosCtiIco2PortTypeClient cl = new RosCtiIco2.IszrRosCtiIco2PortTypeClient(bind, adr);
            //
            cl.ClientCredentials.ClientCertificate.Certificate = volani.Certifikat;
            //
            RosCtiIco2.RosCtiIco2Type req = new RosCtiIco2.RosCtiIco2Type();
            //
            req.ZadostInfo = new RosCtiIco2.ZadostInfoIszrType();
            FillSystemPart(volani, req.ZadostInfo);
            // pokud se pripojuji k udajum z ROS udaje z ROB, pak je vyzadovano
            req.ZadostInfo.Subjekt = "Subjekt";
            req.ZadostInfo.Uzivatel = "Uzivatel156";
            req.ZadostInfo.DuvodUcel = "Duvod a ucel";
            //
            req.AutorizaceInfo = new RosCtiIco2.AutorizaceType();
            req.AutorizaceInfo.SeznamUdaju = volani.Hlavicka.AutorizaceInfo;
            //
            req.Zadost = new RosCtiIco2.RosCtiIco2TypeZadost();
            req.Zadost.RosCtiIco2Data = new RosCtiIco2.RosCtiIco2DataType();
            req.Zadost.RosCtiIco2Data.Ico = ico;
            //
            cl.Endpoint.Behaviors.Add(new MessageLoggerBehavior(volani.Log));
            RosCtiIco2.RosCtiIco2ResponseType resp = cl.IszrRosCtiIco2(req);
            LogHistory(svc, volani, resp);
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// E99 - iszrAsyncVypisFronty
        /// </summary>
        /// <param name="volani"></param>
        internal static void E99(Volani volani)
        {
            String svc = "E99";
            Binding bind = null;
            EndpointAddress adr = GetEgonAddr(volani, svc, true, out bind);
            //
            IszrAsyncVypisFronty.IszrAsyncVypisFrontyPortTypeClient cl = new IszrAsyncVypisFronty.IszrAsyncVypisFrontyPortTypeClient(bind, adr);
            //
            cl.ClientCredentials.ClientCertificate.Certificate = volani.Certifikat;
            //
            IszrAsyncVypisFronty.IszrAsyncVypisFrontyType req = new IszrAsyncVypisFronty.IszrAsyncVypisFrontyType();
            //
            req.ZadostInfo = new IszrAsyncVypisFronty.ZadostInfoIszrType();
            FillSystemPart(volani, req.ZadostInfo);
            //
            req.Zadost = new IszrAsyncVypisFronty.IszrAsyncVypisFrontyTypeZadost();
            req.Zadost.IszrAsyncVypisFrontyData = new IszrAsyncVypisFronty.OmezeniVypisuType();
            //
            cl.Endpoint.Behaviors.Add(new MessageLoggerBehavior(volani.Log));
            IszrAsyncVypisFronty.IszrAsyncVypisFrontyResponseType resp = cl.IszrAsyncVypisFronty(req);
            LogHistory(svc, volani, resp);
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// E100 - iszrAsyncOdpovedZFronty
        /// </summary>
        /// <param name="volani"></param>
        /// <param name="iszrZadostId"></param>
        internal static void E100(Volani volani, String iszrZadostId)
        {
            String svc = "E100";
            Binding bind = null;
            EndpointAddress adr = GetEgonAddr(volani, svc, true, out bind);
            //
            IszrAsyncOdpovedZFronty.IszrAsyncOdpovedZFrontyPortTypeClient cl = new IszrAsyncOdpovedZFronty.IszrAsyncOdpovedZFrontyPortTypeClient(bind, adr);
            //
            cl.ClientCredentials.ClientCertificate.Certificate = volani.Certifikat;
            //
            IszrAsyncOdpovedZFronty.IszrAsyncOdpovedZFrontyType req = new IszrAsyncOdpovedZFronty.IszrAsyncOdpovedZFrontyType();
            //
            req.ZadostInfo = new IszrAsyncOdpovedZFronty.ZadostInfoIszrType();
            FillSystemPart(volani, req.ZadostInfo);
            //
            req.KodAsyncSluzby = svc;
            req.Zadost = new IszrAsyncOdpovedZFronty.IszrAsyncOdpovedZFrontyTypeZadost();
            req.Zadost.IszrAsyncOdpovedZFrontyData = new IszrAsyncOdpovedZFronty.IszrAsyncOdpovedZFrontyTypeZadostIszrAsyncOdpovedZFrontyData();
            req.Zadost.IszrAsyncOdpovedZFrontyData.IszrZadostId = iszrZadostId;
            //
            cl.Endpoint.Behaviors.Add(new MessageLoggerBehavior(volani.Log));
            IszrAsyncOdpovedZFronty.IszrAsyncOdpovedZFrontyResponseType resp = cl.IszrAsyncOdpovedZFronty(req);
            LogHistory(svc, volani, resp);
        }
        #endregion
    }
}
