// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="AucoContCZ">
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
    using System.Configuration;

    /// <summary>
    /// Trida pro cteni konfigurace aplikace
    /// </summary>
    public class Config
    {
        #region konstruktor
        //------------------------------------------------------------------------------------
        private Config() { }
        #endregion
        #region metody
        //------------------------------------------------------------------------------------
        /// <summary>
        /// cteni log. hodnoty z konfiguracniho souboru
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static bool Cfg(String key, bool defVal)
        {
            try
            {
                return Convert.ToBoolean(Cfg(key, defVal.ToString()));
            }
            catch (Exception)
            {
                return defVal;
            }
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// cteni retezce z konfiguracniho souboru
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static String Cfg(String key, String defVal)
        {
            return ConfigurationManager.AppSettings[key] ?? "";
        }
        #endregion
    }
}
