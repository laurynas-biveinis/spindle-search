//-------------------------------------------------------------------\\
// CD / DVD Spindle Search Plugin for Google Desktop Search          \\
// Copyright (c) 2005, Manas Tungare. http://www.manastungare.com/   \\
// Copyright (c) 2009, 2010 spindle-search developers.               \\
// http://code.google.com/p/spindle-search/                          \\
//-------------------------------------------------------------------\\
// This program is free software; you can redistribute it and/or     \\
// modify it under the terms of the GNU General Public License       \\
// as published by the Free Software Foundation; either version 2    \\
// of the License, or (at your option) any later version.            \\
//                                                                   \\
// This program is distributed in the hope that it will be useful,   \\
// but WITHOUT ANY WARRANTY; without even the implied warranty of    \\
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the     \\
// GNU General Public License for more details.                      \\
//                                                                   \\
// GNU GPL: http://www.gnu.org/licenses/gpl.html                     \\
//-------------------------------------------------------------------\\

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Web;
using System.Diagnostics;

namespace Org.ManasTungare.SpindleSearch
{
    /// <summary>
    /// Display information about a file retrieved via a Google Desktop Search.
    /// Since the actual file does not exist online, this dialog provides
    /// an informative message about where to locate it.
    /// </summary>
    class SpindleFileInfo : System.Windows.Forms.Form
    {
        #region Window Elements
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label fileInfo;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox logoIcon;
        private System.Windows.Forms.LinkLabel fileLink;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.Label insertDiskLabel;
        #endregion

        #region Boilerplate
        private System.ComponentModel.Container components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        /// <summary>
        /// Create a SpindleFileInfo window given a file location.
        /// </summary>
        /// <param name="spindleUri">a <code>spindle://</code>-style uniform resource locator.</param>
        public SpindleFileInfo(string spindleUri)
        {
            InitializeComponent();

            System.Uri spindleFile;
            try
            {
                // Parse Uri, and display as a list of field=value pairs.
                spindleFile = new System.Uri(spindleUri);

                try
                {
                    string[] fields = spindleFile.Query.Remove(0, 1).Split('&');
                    string formattedOutput = "";
                    foreach (string field in fields)
                    {
                        string[] nameValuePair = field.Split('=');
                        formattedOutput += HttpUtility.UrlDecode(nameValuePair[0]) + ": " + HttpUtility.UrlDecode(nameValuePair[1]) + "\n";
                    }
                    fileInfo.Text = formattedOutput;
                    fileLink.Text = spindleFile.Host.ToUpper() + ":" + HttpUtility.UrlDecode(spindleFile.AbsolutePath).Replace('/', '\\');
                }
                catch (Exception)
                {
                    // If unable to parse, degrade gracefully and show the text as-is.
                    fileInfo.Text = spindleUri;
                    fileLink.Text = spindleFile.Host.ToUpper() + ":" + HttpUtility.UrlEncode(spindleFile.AbsolutePath).Replace('/', '\\');
                }
            }
            catch (Exception)
            {
                // If unable to parse, degrade gracefully and show the text as-is.
                fileInfo.Text = spindleUri;
                fileLink.Text = spindleUri;
            }
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SpindleFileInfo));
            this.titleLabel = new System.Windows.Forms.Label();
            this.fileInfo = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.logoIcon = new System.Windows.Forms.PictureBox();
            this.fileLink = new System.Windows.Forms.LinkLabel();
            this.insertDiskLabel = new System.Windows.Forms.Label();
            this.openFileButton = new System.Windows.Forms.Button();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(80, 16);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(417, 17);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "The file you requested is stored on a removable device. It is available at:";
            // 
            // fileInfo
            // 
            this.fileInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.fileInfo.Location = new System.Drawing.Point(88, 72);
            this.fileInfo.Name = "fileInfo";
            this.fileInfo.Size = new System.Drawing.Size(416, 72);
            this.fileInfo.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(424, 152);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(80, 24);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // logoIcon
            // 
            this.logoIcon.Image = ((System.Drawing.Image)(resources.GetObject("logoIcon.Image")));
            this.logoIcon.Location = new System.Drawing.Point(16, 16);
            this.logoIcon.Name = "logoIcon";
            this.logoIcon.Size = new System.Drawing.Size(48, 48);
            this.logoIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logoIcon.TabIndex = 3;
            this.logoIcon.TabStop = false;
            // 
            // fileLink
            // 
            this.fileLink.Location = new System.Drawing.Point(80, 32);
            this.fileLink.Name = "fileLink";
            this.fileLink.Size = new System.Drawing.Size(424, 16);
            this.fileLink.TabIndex = 4;
            this.fileLink.TabStop = true;
            this.fileLink.Text = "[fileLink]";
            this.fileLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fileLink_LinkClicked);
            // 
            // insertDiskLabel
            // 
            this.insertDiskLabel.Location = new System.Drawing.Point(80, 56);
            this.insertDiskLabel.Name = "insertDiskLabel";
            this.insertDiskLabel.Size = new System.Drawing.Size(400, 16);
            this.insertDiskLabel.TabIndex = 5;
            this.insertDiskLabel.Text = "Please insert the specified disk into the drive, then click on the link above.";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(80, 152);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(112, 24);
            this.openFileButton.TabIndex = 6;
            this.openFileButton.Text = "Open File";
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(200, 152);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(112, 24);
            this.openFolderButton.TabIndex = 7;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // SpindleFileInfo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(522, 191);
            this.ControlBox = false;
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.insertDiskLabel);
            this.Controls.Add(this.fileLink);
            this.Controls.Add(this.logoIcon);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.fileInfo);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpindleFileInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CD / DVD Spindle Search";
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Exit Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        /// <summary>
        /// Launch the file with the default registered application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (Launch(fileLink.Text))
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Helper method to launch a file using the default regsitered application.
        /// </summary>
        /// <param name="fileName">the file to launch.</param>
        /// <returns><code>true</code> if success</returns>
        private bool Launch(string fileName)
        {
            if (!new FileInfo(fileName).Exists
              && !new DirectoryInfo(fileName).Exists)
            {
                MessageBox.Show("The specified file cannot be located. Please confirm that you have inserted the " +
                  "\ncorrect disk in the same drive that was used to index it.",
                  Application.ProductName,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    ProcessStartInfo spawnApp = new ProcessStartInfo();
                    spawnApp.FileName = fileName;
                    Process.Start(spawnApp);
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to open file. Please check that you have an application installed " +
                      "for opening files of type \"" + (new FileInfo(fileName)).Extension + "\"",
                      Application.ProductName,
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Launch the file with the default registered application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileButton_Click(object sender, System.EventArgs e)
        {
            if (Launch(fileLink.Text))
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Open the containing folder in Windows Explorer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFolderButton_Click(object sender, System.EventArgs e)
        {
            string directoryName = new FileInfo(fileLink.Text).DirectoryName;
            if (Launch(directoryName))
            {
                Application.Exit();
            }
        }
    }
}
