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

    /// <summary>
    /// formular pro nastaveni proxy
    /// </summary>
    public partial class NastaveniF : Form
    {
        #region promenne
        public ProxySetup Proxy { get; set; }
        #endregion
        #region konstruktor
        public NastaveniF()
        {
            InitializeComponent();
        }
        #endregion
        #region udalosti
        /// <summary>
        /// udalost zobrazeni formulare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NastaveniF_Load(object sender, EventArgs e)
        {
            if (Proxy != null)
            {
                rbProsyOn.Checked = Proxy.UseSystem;
                cbDefaultNC.Checked = Proxy.UseDefaultNetworkCredentials;
                tbUser.Text = Proxy.UserName;
                tbPwd.Text = Proxy.Password;
                tbDom.Text = Proxy.Domain;
            }
        }
        /// <summary>
        /// udalost tlacitko Ok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOk_Click(object sender, EventArgs e)
        {
            if (Proxy != null)
            {
                Proxy.UseSystem = rbProsyOn.Checked;
                Proxy.UseDefaultNetworkCredentials = cbDefaultNC.Checked;
                Proxy.UserName = tbUser.Text.Trim();
                Proxy.Password = tbPwd.Text.Trim();
                Proxy.Domain = tbDom.Text.Trim();
                DialogResult = DialogResult.OK;
            }
        }
        #endregion
    }
}
