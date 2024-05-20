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
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Security.Cryptography.X509Certificates;
    using System.Diagnostics;
    using System.Reflection;
    using System.IO;
    using Autocont.ISZRDemo.Frms;
    using Autocont.ISZRDemo.Cls;

    /// <summary>
    /// halvni formular aplikace
    /// </summary>
    public partial class MainF : Form, ILog
    {
        #region konstanty
        private readonly string AutorizaceInfoSourceROB = "Aifo Prijmeni Jmeno AdresaPobytu DorucovaciAdresa DatumNarozeni MistoNarozeni DatumUmrti DatumPravniMociUmrti MistoUmrti DatovaSchrankaROB Doklad Obcanstvi RodnePrijmeni OmezeniSvepravnosti RodinnyStavPartnerstvi Pohlavi Telefon Email Certifikat";
        private readonly string AutorizaceInfoSourceROS = "Ico FyzickaOsoba ObchodniNazev DatumVznikuOpravneni DatumZanikuOpravneni PravniForma PravniStav DatovaSchrankaROS StatutarniOrgany AdresaSidla Provozovny DorucovaciAdresaOsoby KontaktniUdaje Likvidator OpatrovnikPravnickeOsoby NucenySpravce InsolvencniSpravce DatumNarozeniFyzickeOsoby DatumNarozeniAngazovaneOsoby DorucovaciAdresaProvozovny PozastaveniPreruseni ZastupcePoStatutara";
        #endregion
        #region promenne
        private ProxySetup proxy = new ProxySetup();
        private List<string> AutorizaceInfoROB = new List<string>();
        private List<string> AutorizaceInfoROS = new List<string>();
        #endregion
        #region konstruktor
        //------------------------------------------------------------------------------------
        public MainF()
        {
            InitializeComponent();
        }
        #endregion
        #region metody
        //------------------------------------------------------------------------------------
        /// <summary>
        /// ziskani prostredi z UI
        /// </summary>
        /// <returns></returns>
        private Prostredi CtiProstredi()
        {
            erp.Clear();
            Prostredi.PripojeniEnum pripojeni;
            if (rbInternet.Checked) pripojeni = Prostredi.PripojeniEnum.InetPublikace;
            else if (rbKivs.Checked) pripojeni = Prostredi.PripojeniEnum.KivsPublikace;
            else throw new InvalidOperationException("prostredi");
            Prostredi p = new Prostredi(pripojeni, rbSync.Checked, proxy, cbTls12.Checked);
            return p;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// ziskani certifikatu z UI
        /// </summary>
        /// <returns></returns>
        private X509Certificate2 CtiCertifikat()
        {
            X509Certificate2 cert = null;
            try
            {
                using (Stream s = new FileStream(tbCert.Text, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            byte[] buff = new byte[1024];
                            int len;
                            while ((len = s.Read(buff, 0, buff.Length)) > 0)
                            {
                                ms.Write(buff, 0, len);
                            }
                            ms.Seek(0, SeekOrigin.Begin);
                            cert = new X509Certificate2(ms.GetBuffer(), tbCertPwd.Text.Trim());
                            Log("Sériové číslo klientského certifikatu: " + cert.SerialNumber);
                        }
                    }
                    finally
                    {
                        if (s != null) s.Close();
                    }
                }
                return cert;
            }
            catch (Exception ex)
            {
                String msg = "Chyba při čtení certifikátu: " + ex.Message;
                erp.SetError(tbCert, msg);
                throw new ApplicationException(msg);
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// ziskani informaci pro systemovou hlavicku z UI
        /// </summary>
        /// <returns></returns>
        private Hlavicka CtiSystem(string type = "Others")
        {
            Hlavicka h = new Hlavicka();
            erp.Clear();
            SetProperty(h, "Ais", tbAis, typeof(int), true);
            SetProperty(h, "Agenda", tbAgenda, typeof(String), true);
            SetProperty(h, "Role", tbRole, typeof(String), true);
            SetProperty(h, "Ovm", tbOvm, typeof(String), true);
            if (type == "ROB")
            {
                h.AutorizaceInfo = string.Join(" ", AutorizaceInfoROB);
            }
            else if (type == "ROS")
            {
                h.AutorizaceInfo = string.Join(" ", AutorizaceInfoROS);
            }
            else
            {
                h.AutorizaceInfo = string.Empty;
            }
            return h;
        }
        //------------------------------------------------------------------------------------
        private void SetProperty(Hlavicka h, String propertyName, TextBox value, Type type, bool isRequired)
        {
            String msg = String.Empty;
            erp.SetError(value, msg);
            try
            {
                String val = value.Text.Trim();
                if (isRequired && String.IsNullOrEmpty(val))
                {
                    msg = "Požadováno vyplnění";
                    throw new ApplicationException();
                }
                PropertyInfo pi = h.GetType().GetProperty(propertyName);
                if (type == typeof(int))
                {
                    msg = "Požadováno vyplnění, číslo";
                    pi.SetValue(h, Convert.ToInt32(val), null);
                }
                else
                {
                    pi.SetValue(h, value.Text.Trim(), null);
                }
            }
            catch(Exception)
            {
                if (msg.Length > 0)
                {
                    erp.SetError(value, msg);
                    if (!cbIgnorovatChyby.Checked)
                    {
                        throw new ApplicationException("Nevyplněny povinné parametry: " + propertyName);
                    }
                }
                else
                {
                    throw;
                }
            }
        }
        //------------------------------------------------------------------------------------
        private int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
        //------------------------------------------------------------------------------------
        private bool CheckNotNull(TextBox value, Type typ)
        {
            String msg = String.Empty;
            erp.SetError(value, msg);
            String val = value.Text.Trim();
            if (String.IsNullOrEmpty(val))
            {
                msg = "Požadováno vyplnění";
                erp.SetError(value, msg);
                return false;
            }
            if (typ == typeof(int))
            {
                try
                {
                    int i = Convert.ToInt32(val);
                }
                catch (Exception)
                {
                    msg = "Požadováno vyplnění, číslo";
                    erp.SetError(value, msg);
                    return false;
                }
            }
            return true;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// zapis do logu v UI
        /// </summary>
        /// <param name="msg"></param>
        public void Log(String msg)
        {
            tbLog.AppendText(msg + Environment.NewLine);
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// zapis do logu v UI
        /// </summary>
        /// <param name="ex"></param>
        public void Log(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ex.Message);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                sb.AppendLine("--inner: " + ex.Message);
            }
            tbLog.AppendText(sb.ToString());
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// zapis do logu historie
        /// </summary>
        /// <param name="msg"></param>
        public void LogHistory(String msg)
        {
            tbHist.AppendText(msg + Environment.NewLine);
        }
        //------------------------------------------------------------------------------------
        private void SetDemo()
        {
            rbSync.Checked = true;
            rbInternet.Checked = true;
            tbAis.Text = "999002"; //"99001";
            tbAgenda.Text = "X999"; //"A999";
            tbRole.Text = "XR2"; // "CTENAR";
            tbOvm.Text = "12345678";
            //
            /*tbROBJmeno.Text = "ANDREA";
            tbROBPrijmeni.Text = "BLOOMBERG";
            tbROBAdresa.Text = "10014";
            dpROBDatumNarozeni.Value = new DateTime(1969, 12, 12);
            dpROBDatumNarozeni.Checked = false;*/
            tbROBJmeno.Text = "MAREK";
            chbROBJmeno.Checked = true;
            tbROBPrijmeni.Text = "ROLNIČKA";
            chbROBPrijmeni.Checked = true;
            tbROBAdresa.Text = "14492253";
            chbROBAdresa.Checked = true;
            dpROBDatumNarozeni.Value = new DateTime(1968, 1, 11);
            dpROBDatumNarozeni.Checked = false;
            cbROBOmezeniSvepravnosti.Items.Clear();
            cbROBOmezeniSvepravnosti.Items.Add("");
            foreach (string item in Enum.GetNames(typeof(RobCtiPodleUdaju2.OmezeniSvepravnostiType)))
            {
                cbROBOmezeniSvepravnosti.Items.Add(item);
            }
            cbROBRodinnyStav.Items.Clear();
            cbROBRodinnyStav.Items.Add("");
            foreach (string item in Enum.GetNames(typeof(RobCtiPodleUdaju2.RodinnyStavPartnerstviType)))
            {
                cbROBRodinnyStav.Items.Add(RodinnyStavTranslate1(item));
            }
            cbROBPohlavi.Items.Clear();
            cbROBPohlavi.Items.Add("");
            foreach (string item in Enum.GetNames(typeof(RobCtiPodleUdaju2.PohlaviType)))
            {
                cbROBPohlavi.Items.Add(item);
            }
            //
            tbIco.Text = "00838420";
            //
            tbIszrZadostId.Text = String.Empty;
            //
            AutorizaceInfoROB.AddRange(AutorizaceInfoSourceROB.Split(' '));
            AutorizaceInfoROS.AddRange(AutorizaceInfoSourceROS.Split(' '));
        }
        //------------------------------------------------------------------------------------
        private string RodinnyStavTranslate1(string rodinnyStav)
        {
            switch (rodinnyStav)
            {
                case "SVOBODNYSVOBODNA":
                    return "SVOBODNY/SVOBODNA";
                case "ZENATYVDANA":
                    return "ZENATY/VDANA";
                case "ROZVEDENYROZVEDENA":
                    return "ROZVEDENY/ROZVEDENA";
                case "ZANIKLEMANZELSTVI":
                    return "ZANIKLE MANZELSTVI";
                case "VDOVECVDOVA":
                    return "VDOVEC/VDOVA";
                case "PARTNERSTVI":
                    return "PARTNERSTVI";
                case "ZANIKLEPARTNERSTVIROZHODNUTIM":
                    return "ZANIKLE PARTNERSTVI ROZHODNUTIM";
                case "ZANIKLEPARTNERSTVI":
                    return "ZANIKLE PARTNERSTVI";
                case "ZANIKLEPARTNERSTVISMRTI":
                    return "ZANIKLE PARTNERSTVI SMRTI";
                case "ODLOUCENYODLOUCENA":
                    return "ODLOUCENY/ODLOUCENA";
                default:
                    return string.Empty;
            }
        }
        private string RodinnyStavTranslate2(string rodinnyStav)
        {
            switch (rodinnyStav)
            {
                case "SVOBODNY/SVOBODNA":
                    return "SVOBODNYSVOBODNA";
                case "ZENATY/VDANA":
                    return "ZENATYVDANA";
                case "ROZVEDENY/ROZVEDENA":
                    return "ROZVEDENYROZVEDENA";
                case "ZANIKLE MANZELSTVI":
                    return "ZANIKLEMANZELSTVI";
                case "VDOVEC/VDOVA":
                    return "VDOVECVDOVA";
                case "PARTNERSTVI":
                    return "PARTNERSTVI";
                case "ZANIKLE PARTNERSTVI ROZHODNUTIM":
                    return "ZANIKLEPARTNERSTVIROZHODNUTIM";
                case "ZANIKLE PARTNERSTVI":
                    return "ZANIKLEPARTNERSTVI";
                case "ZANIKLE PARTNERSTVI SMRTI":
                    return "ZANIKLEPARTNERSTVISMRTI";
                case "ODLOUCENYODLOUCENA":
                    return "ODLOUCENY/ODLOUCENA";
                default:
                    return string.Empty;
            }
        }
        #endregion
        #region udalosti
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Nacteni formulare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainF_Load(object sender, EventArgs e)
        {
            SetDemo();
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// tlacitko smazani logu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSmazat_Click(object sender, EventArgs e)
        {
            tbLog.Clear();
            tbHist.Clear();
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// tlacitko Demo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSysDemo_Click(object sender, EventArgs e)
        {
            SetDemo();
            erp.Clear();
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// tlacitko E05
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btROB_Click(object sender, EventArgs e)
        {
            try
            {
                Volani volani = new Volani(this, CtiProstredi(), CtiCertifikat(), CtiSystem("ROB"));
                E278ZadostData e278ZadostData = new E278ZadostData();
                e278ZadostData.Jmeno = tbROBJmeno.Text;
                e278ZadostData.PouzitJmeno = chbROBJmeno.Checked;
                e278ZadostData.Prijmeni = tbROBPrijmeni.Text;
                e278ZadostData.PouzitPrijmeni = chbROBPrijmeni.Checked;
                e278ZadostData.Adresa = ToNullableInt(tbROBAdresa.Text);
                e278ZadostData.PouzitAdresa = chbROBAdresa.Checked;
                if (dpROBDatumNarozeni.Checked)
                {
                    e278ZadostData.DatumNarozeni = dpROBDatumNarozeni.Value;
                }
                if (dpROBDatumUmrti.Checked)
                {
                    e278ZadostData.DatumUmrti = dpROBDatumUmrti.Value;
                }
                e278ZadostData.DatovaSchrankaId = tbROBDatovaSchrankaId.Text;
                e278ZadostData.PouzitDatovaSchrankaId = chbROBDatovaSchrankaId.Checked;
                e278ZadostData.MistoNarozeni = ToNullableInt(tbROBMistoNarozeni.Text);
                e278ZadostData.PouzitMistoNarozeni = chbROBMistoNarozeni.Checked;
                e278ZadostData.MistoUmrti = ToNullableInt(tbROBMistoUmrti.Text);
                e278ZadostData.PouzitMistoUmrti = chbROBMistoUmrti.Checked;
                e278ZadostData.RodnePrijmeni = tbROBRodnePrijmeni.Text;
                e278ZadostData.PouzitRodnePrijmeni = chbROBRodnePrijmeni.Checked;
                if (chbROBOmezeniSvepravnosti.Checked)
                {
                    if (!string.IsNullOrEmpty(cbROBOmezeniSvepravnosti.Text))
                    {
                        RobCtiPodleUdaju2.OmezeniSvepravnostiType omezeniSvepravnosti;
                        Enum.TryParse(cbROBOmezeniSvepravnosti.Text, out omezeniSvepravnosti);
                        e278ZadostData.OmezeniSvepravnosti = omezeniSvepravnosti;
                    }
                    e278ZadostData.PouzitOmezeniSvepravnosti = true;
                }
                if (chbROBRodinnyStav.Checked)
                {
                    if (!string.IsNullOrEmpty(cbROBRodinnyStav.Text))
                    {
                        RobCtiPodleUdaju2.RodinnyStavPartnerstviType rodinnyStav;
                        Enum.TryParse(RodinnyStavTranslate2(cbROBRodinnyStav.Text), out rodinnyStav);
                        e278ZadostData.RodinnyStav = rodinnyStav;
                    }
                    e278ZadostData.PouzitRodinnyStav = true;
                }
                if (chbROBPohlavi.Checked)
                {
                    if (!string.IsNullOrEmpty(cbROBPohlavi.Text))
                    {
                        RobCtiPodleUdaju2.PohlaviType pohlavi;
                        Enum.TryParse(cbROBPohlavi.Text, out pohlavi);
                        e278ZadostData.Pohlavi = pohlavi;
                    }
                    e278ZadostData.PouzitPohlavi = true;
                }
                EgonServices.E278(volani, e278ZadostData);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// tlacitko E20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btROS_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckNotNull(tbIco, typeof(int))) return;
                Volani volani = new Volani(this, CtiProstredi(), CtiCertifikat(), CtiSystem("ROS"));
                EgonServices.E256(volani, Convert.ToInt32(tbIco.Text.Trim()));
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// tlacitko E99
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btIszrE99_Click(object sender, EventArgs e)
        {
            try
            {
                Volani volani = new Volani(this, CtiProstredi(), CtiCertifikat(), CtiSystem());
                EgonServices.E99(volani);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// tlacitko E100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btIszrE100_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckNotNull(tbIszrZadostId, typeof(String))) return;
                Volani volani = new Volani(this, CtiProstredi(), CtiCertifikat(), CtiSystem());
                EgonServices.E100(volani, tbIszrZadostId.Text.Trim());
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// tlacitko Nastavit 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOstatni_Click(object sender, EventArgs e)
        {
            using (NastaveniF form = new NastaveniF())
            {
                form.Proxy = proxy;
                form.ShowDialog();
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// tlacitko vyber certifikatu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSelCert_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(od.InitialDirectory))
            {
                od.InitialDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Res");
            }
            if (od.ShowDialog() == DialogResult.OK)
            {
                tbCert.Text = od.FileName;
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Zapina a vypina pole pro adresu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbROBAdresa_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBAdresa.Checked)
            {
                tbROBAdresa.Enabled = true;
            }
            else
            {
                tbROBAdresa.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Otevre formular na nastaveni hodnoty AutorizaceInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAutorizaceInfo_Click(object sender, EventArgs e)
        {
            using (AutorizaceInfoF form = new AutorizaceInfoF())
            {
                form.checkedListBoxROB.Items.AddRange(AutorizaceInfoSourceROB.Split(' '));
                form.AutorizaceInfoROB = AutorizaceInfoROB;
                form.checkedListBoxROS.Items.AddRange(AutorizaceInfoSourceROS.Split(' '));
                form.AutorizaceInfoROS = AutorizaceInfoROS;
                form.ShowDialog();
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBJmeno_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBJmeno.Checked)
            {
                tbROBJmeno.Enabled = true;
            }
            else
            {
                tbROBJmeno.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBPrijmeni_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBPrijmeni.Checked)
            {
                tbROBPrijmeni.Enabled = true;
            }
            else
            {
                tbROBPrijmeni.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBDatovaSchrankaId_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBDatovaSchrankaId.Checked)
            {
                tbROBDatovaSchrankaId.Enabled = true;
            }
            else
            {
                tbROBDatovaSchrankaId.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBMistoNarozeni_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBMistoNarozeni.Checked)
            {
                tbROBMistoNarozeni.Enabled = true;
            }
            else
            {
                tbROBMistoNarozeni.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBMistoUmrti_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBMistoUmrti.Checked)
            {
                tbROBMistoUmrti.Enabled = true;
            }
            else
            {
                tbROBMistoUmrti.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBRodnePrijmeni_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBRodnePrijmeni.Checked)
            {
                tbROBRodnePrijmeni.Enabled = true;
            }
            else
            {
                tbROBRodnePrijmeni.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBOmezeniSvepravnosti_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBOmezeniSvepravnosti.Checked)
            {
                cbROBOmezeniSvepravnosti.Enabled = true;
            }
            else
            {
                cbROBOmezeniSvepravnosti.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBRodinnyStav_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBRodinnyStav.Checked)
            {
                cbROBRodinnyStav.Enabled = true;
            }
            else
            {
                cbROBRodinnyStav.Enabled = false;
            }
        }
        //------------------------------------------------------------------------------------
        private void chbROBPohlavi_CheckedChanged(object sender, EventArgs e)
        {
            if (chbROBPohlavi.Checked)
            {
                cbROBPohlavi.Enabled = true;
            }
            else
            {
                cbROBPohlavi.Enabled = false;
            }
        }
        #endregion
    }
}
