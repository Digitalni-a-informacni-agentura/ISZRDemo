﻿// -----------------------------------------------------------------------
// <copyright file="ILog.cs" company="AucoContCZ">
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
    /// rozhrani pro logovani
    /// </summary>
    public interface ILog
    {
        void Log(String msg);
        void Log(Exception ex);
        void LogHistory(String msg);
    }
}
