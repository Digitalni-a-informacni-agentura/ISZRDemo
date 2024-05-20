// -----------------------------------------------------------------------
// <copyright file="ProxySetup.cs" company="AucoContCZ">
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

    /// <summary>
    /// Trida nastaveni proxy
    /// </summary>
    public class ProxySetup
    {
        #region vlastnosti
        /// <summary>pouziti proxy definovane v systemu</summary>
        public bool UseSystem { get; set; }
        /// <summary>pouziti defaulr credentials pri pristupu na proxy</summary>
        public bool UseDefaultNetworkCredentials { get; set; }
        /// <summary>pristup na proxy - uz. jmeno</summary>
        public String UserName { get; set; }
        /// <summary>pristup na proxy - heslo</summary>
        public String Password { get; set; }
        /// <summary>pristup na proxy - domena uzivatele</summary>
        public String Domain { get; set; }
        #endregion

        #region konstruktor
        public ProxySetup()
        {
            UseSystem = Config.Cfg("Proxy.UseSystem", true);
            UseDefaultNetworkCredentials = Config.Cfg("Proxy.UseDefaultNetworkCredentials", true);
            UserName = Config.Cfg("Proxy.UserName", "");
            Password = Config.Cfg("Proxy.Password", "");
            Domain = Config.Cfg("Proxy.Domain", "");
        }
        #endregion
    }
}
