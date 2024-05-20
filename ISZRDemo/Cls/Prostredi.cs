// -----------------------------------------------------------------------
// <copyright file="Prostredi.cs" company="AucoContCZ">
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
    using System.Diagnostics;

    /// <summary>
    /// Trida pro definici volani
    /// </summary>
    public class Prostredi
    {
        #region enum
        /// <summary>
        /// podporovana prostredi
        /// </summary>
        public enum PripojeniEnum
        {
            KivsPublikace,
            InetPublikace
        }
        #endregion

        #region vlastnosti
        /// <summary>prostredi, na ktere se ma pristupovat</summary>
        public PripojeniEnum Pripojeni { get; private set; }
        /// <summary>zpusob volani sluzby</summary>
        public bool Sync { get; private set; }
        /// <summary>zpusob pristupu pres proxy</summary>
        public ProxySetup Proxy { get; private set; }
		/// <summary>zapnout podporu TLS 1.2</summary>
		public bool Tls12 { get; private set; }
        #endregion

        #region konstruktor
        //------------------------------------------------------------------------------------
        public Prostredi(PripojeniEnum pripojeni, bool sync, ProxySetup proxy, bool tls12)
        {
            this.Pripojeni = pripojeni;
            this.Sync = sync;
            this.Proxy = proxy;
			this.Tls12 = tls12;
        }
        #endregion
        #region metody
        //------------------------------------------------------------------------------------
        /// <summary>
        /// vytvoreni URL pro volani eGON sluzby
        /// </summary>
        /// <param name="kodSluzby"></param>
        /// <param name="lzeAsync"></param>
        /// <returns></returns>
        public String GetServiceUrl(String kodSluzby, bool lzeAsync)
        {
            String url = String.Empty;
            switch (Pripojeni)
            {
                case PripojeniEnum.InetPublikace:
                    url = "https://egon.gov.cz/publikace/" + kodSluzby;
                    break;
                case PripojeniEnum.KivsPublikace:
                    url = "https://pub.egon.cms2.cz/publikace/" + kodSluzby;
                    break;
                default:
                    throw new ApplicationException("ProstrediEnum=" + Pripojeni.ToString());
            }
            if (lzeAsync && !Sync) url += "?async";
            return url;
        }
        #endregion
    }
}
