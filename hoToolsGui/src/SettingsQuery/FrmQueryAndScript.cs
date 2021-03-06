﻿using System;
using System.Windows.Forms;
using hoTools.EaServices;
using hoTools.EaServices.WiKiRefs;

namespace hoTools.Settings
{
    public partial class FrmQueryAndScript : Form
    {
        /// <summary>
        /// Global Settings of hoTools
        /// </summary>
        readonly AddinSettings _settings;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FrmQueryAndScript(AddinSettings settings)
        {
            InitializeComponent();
            _settings = settings;

            // Query window without script
            switch (_settings.OnlyQueryWindow) {
                case AddinSettings.ShowInWindow.AddinWindow:
                    rbOnlyQueryAddinWindow.Checked = true;
                    break;
                case AddinSettings.ShowInWindow.TabWindow:
                    rbOnlyQueryTabWindow.Checked = true;
                    break;
                default:
                    rbOnlyQueryDisableWindow.Checked = true;
                    break;
            }
            // Query window wit script
            switch (_settings.ScriptAndQueryWindow)
            {
                case AddinSettings.ShowInWindow.AddinWindow:
                    rbScriptAndQueryAddinWindow.Checked = true;
                    break;
                case AddinSettings.ShowInWindow.TabWindow:
                    rbScriptAndQueryTabWindow.Checked = true;
                    break;
                default:
                    rbScriptAndQueryDisableWindow.Checked = true;
                    break;
            }

            // Ask for update if sql file has changed
            chkIsAskForUpdate.Checked = _settings.IsAskForQueryUpdateOutside;
            // SQL editor
            txtSqlEditor.Text = _settings.SqlEditor;

            txtAddinTabToFirstActivate.Text = _settings.AddinTabToFirstActivate;

            txtSqlSearchPath.Text = _settings.SqlPaths;

            txtExtensionPath.Text = _settings.CodeExtensionsPath;
        }
        #endregion

        #region Button Close
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Button ok
        void btnOk_Click(object sender, EventArgs e)
        {
            // only SQL query window
            _settings.OnlyQueryWindow = AddinSettings.ShowInWindow.Disabled;
            _settings.AddinTabToFirstActivate = txtAddinTabToFirstActivate.Text;

            if (rbOnlyQueryAddinWindow.Checked) _settings.OnlyQueryWindow = AddinSettings.ShowInWindow.AddinWindow;
            if (rbOnlyQueryTabWindow.Checked) _settings.OnlyQueryWindow = AddinSettings.ShowInWindow.TabWindow;

            // SQL query + script window
            _settings.ScriptAndQueryWindow = AddinSettings.ShowInWindow.Disabled;
            if (rbScriptAndQueryAddinWindow.Checked) _settings.ScriptAndQueryWindow = AddinSettings.ShowInWindow.AddinWindow;
            if (rbScriptAndQueryTabWindow.Checked) _settings.ScriptAndQueryWindow = AddinSettings.ShowInWindow.TabWindow;

            _settings.IsAskForQueryUpdateOutside = chkIsAskForUpdate.Checked;
            _settings.SqlEditor = txtSqlEditor.Text;

            _settings.SqlPaths = txtSqlSearchPath.Text;

            _settings.CodeExtensionsPath = txtExtensionPath.Text;


            // save setting
            _settings.Save();
            Close();
        }
        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WikiRef.Wiki();
        }

        private void settingsSQLScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WikiRef.WikiSettingsSql();
        }

        private void sQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WikiRef.WikiSql();
        }

        private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WikiRef.WikiScript();
        }
        /// <summary>
        /// Ensured that the modal windows is always on top
        /// - On 4K monitors the dialog sometimes get in the background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmQueryAndScript_Shown(object sender, EventArgs e)
        {
            TopMost = true;
        }
    }
}
