using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autocont.ISZRDemo.Frms
{
    /// <summary>
    /// Formular pro nastaveni AutorizaceInfo
    /// </summary>
    public partial class AutorizaceInfoF : Form
    {
        #region promenne
        public List<string> AutorizaceInfoROB;
        public List<string> AutorizaceInfoROS;
        #endregion
        #region konstruktor
        public AutorizaceInfoF()
        {
            InitializeComponent();
        }
        #endregion
        #region udalosti
        /// <summary>
        /// Udalost tlacitka Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAutorizaceInfoCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Udalost tlacitka OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAutorizaceInfoOk_Click(object sender, EventArgs e)
        {
            if (AutorizaceInfoROB != null && AutorizaceInfoROS != null)
            {
                AutorizaceInfoROB.Clear();
                AutorizaceInfoROB.AddRange(checkedListBoxROB.CheckedItems.OfType<string>());
                AutorizaceInfoROS.Clear();
                AutorizaceInfoROS.AddRange(checkedListBoxROS.CheckedItems.OfType<string>());
            }
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// Udalost zobrazeni formulare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutorizaceInfoF_Load(object sender, EventArgs e)
        {
            if (AutorizaceInfoROB != null)
            {
                for (int i = 0; i < checkedListBoxROB.Items.Count; i++)
                {
                    if (AutorizaceInfoROB.Contains(checkedListBoxROB.Items[i]))
                    {
                        checkedListBoxROB.SetItemChecked(i, true);
                    }
                    else
                    {
                        checkedListBoxROB.SetItemChecked(i, false);
                    }
                }
            }
            if (AutorizaceInfoROS != null)
            {
                for (int i = 0; i < checkedListBoxROS.Items.Count; i++)
                {
                    if (AutorizaceInfoROS.Contains(checkedListBoxROS.Items[i]))
                    {
                        checkedListBoxROS.SetItemChecked(i, true);
                    }
                    else
                    {
                        checkedListBoxROS.SetItemChecked(i, false);
                    }
                }
            }
        }
        #endregion
    }
}
