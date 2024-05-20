// -----------------------------------------------------------------------
// <copyright file="Hlavicka.cs" company="AucoContCZ">
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
    /// Data pro systemovou hlavicku volani sluzby
    /// </summary>
    public class Hlavicka
    {
        /// <summary>Kod AIS</summary>
        public int Ais { get; set; }
        /// <summary>Kod agendy</summary>
        public String Agenda { get; set; }
        /// <summary>Kod agendove role</summary>
        public String Role { get; set; }
        /// <summary>Kod OVM</summary>
        public String Ovm { get; set; }
        /// <summary>Retezec AutorizaceInfo</summary>
        public String AutorizaceInfo { get; set; }
    }
}
