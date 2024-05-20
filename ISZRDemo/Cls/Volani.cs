// -----------------------------------------------------------------------
// <copyright file="Volani.cs" company="AucoContCZ">
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
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Vstupni data pro bussines logiku volani sluzby
    /// </summary>
    public class Volani
    {
        #region vlastnosti
        /// <summary>interface pro logovani</summary>
        public ILog Log { get; private set; }
        /// <summary>parametry prostredi</summary>
        public Prostredi Prostredi { get; private set; }
        /// <summary>klientsky certifikat AIS</summary>
        public X509Certificate2 Certifikat { get; private set; }
        /// <summary>data pro systemovou cast dotazu</summary>
        public Hlavicka Hlavicka { get; private set; }
        #endregion

        #region konstruktor
        public Volani(
            ILog log,
            Prostredi prostredi,
            X509Certificate2 certifikat,
            Hlavicka hlavicka
        )
        {
            this.Log = log;
            this.Prostredi = prostredi;
            this.Certifikat = certifikat;
            this.Hlavicka = hlavicka;
        }
        #endregion
    }
}
