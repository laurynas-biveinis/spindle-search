//-------------------------------------------------------------------\\
// CD / DVD Spindle Search Plugin for Google Desktop Search          \\
// Copyright (c) 2005, Manas Tungare. http://www.manastungare.com/   \\
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Diagnostics;
using Org.ManasTungare.Google.Desktop;
using ICSharpCode.SharpZipLib.Zip;

namespace Org.ManasTungare.SpindleSearch {
  public class MainWindow : System.Windows.Forms.Form {
    #region  Window Elements 
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.TextBox comments;
    private System.Windows.Forms.TextBox location;
    private System.Windows.Forms.TextBox medium;
    private System.Windows.Forms.TextBox manufacturer;
    private System.Windows.Forms.TextBox volumeLabel;
    private System.Windows.Forms.TabPage driveTab;
    private System.Windows.Forms.TabPage metaInfoTab;
    private System.Windows.Forms.TabPage progressTab;
    private System.Windows.Forms.Button backButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.TabControl tabStrip;
    private System.Windows.Forms.RichTextBox progressTextField;
    private System.Windows.Forms.ListView drivesList;
    private System.Windows.Forms.ImageList imageList;
    private System.Windows.Forms.LinkLabel manastungareWebLink;
    private System.Windows.Forms.PictureBox mtIcon;
    private System.Windows.Forms.PictureBox logoIcon;
    private System.Windows.Forms.Label instructionsLabel;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.Label introLabel;
    private System.Windows.Forms.Label selectDriveLabel;
    private System.Windows.Forms.Label commentsLabel;
    private System.Windows.Forms.Label locationLabel;
    private System.Windows.Forms.Label volumeLabelLabel;
    private System.Windows.Forms.Label mediumLabel;
    private System.Windows.Forms.Label manufacturerLabel;
    private System.Windows.Forms.Label headerLabel;
    private System.Windows.Forms.Label gdsLabel;
    private System.Windows.Forms.GroupBox metaInfoGroupBox;
    private System.Windows.Forms.PictureBox headerPictureBox;
    private System.Windows.Forms.Button loadCatalogFromFile;
    private System.Windows.Forms.CheckBox saveCatalogCheckBox;
    private System.Windows.Forms.LinkLabel refreshLink;
    private System.ComponentModel.IContainer components;
    #endregion

    #region  Boilerplate 
    public MainWindow() {
      InitializeComponent();
      this.Text += " " + Application.ProductVersion;
    }

    protected override void Dispose( bool disposing ) {
      if( disposing ) {
        if(components != null) {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
      Application.Exit();
    }
    #endregion

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainWindow));
      this.backButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.tabStrip = new System.Windows.Forms.TabControl();
      this.driveTab = new System.Windows.Forms.TabPage();
      this.instructionsLabel = new System.Windows.Forms.Label();
      this.drivesList = new System.Windows.Forms.ListView();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.titleLabel = new System.Windows.Forms.Label();
      this.introLabel = new System.Windows.Forms.Label();
      this.selectDriveLabel = new System.Windows.Forms.Label();
      this.metaInfoTab = new System.Windows.Forms.TabPage();
      this.metaInfoGroupBox = new System.Windows.Forms.GroupBox();
      this.saveCatalogCheckBox = new System.Windows.Forms.CheckBox();
      this.comments = new System.Windows.Forms.TextBox();
      this.location = new System.Windows.Forms.TextBox();
      this.medium = new System.Windows.Forms.TextBox();
      this.manufacturer = new System.Windows.Forms.TextBox();
      this.commentsLabel = new System.Windows.Forms.Label();
      this.locationLabel = new System.Windows.Forms.Label();
      this.volumeLabelLabel = new System.Windows.Forms.Label();
      this.mediumLabel = new System.Windows.Forms.Label();
      this.manufacturerLabel = new System.Windows.Forms.Label();
      this.volumeLabel = new System.Windows.Forms.TextBox();
      this.progressTab = new System.Windows.Forms.TabPage();
      this.progressTextField = new System.Windows.Forms.RichTextBox();
      this.headerPictureBox = new System.Windows.Forms.PictureBox();
      this.headerLabel = new System.Windows.Forms.Label();
      this.manastungareWebLink = new System.Windows.Forms.LinkLabel();
      this.gdsLabel = new System.Windows.Forms.Label();
      this.mtIcon = new System.Windows.Forms.PictureBox();
      this.nextButton = new System.Windows.Forms.Button();
      this.logoIcon = new System.Windows.Forms.PictureBox();
      this.loadCatalogFromFile = new System.Windows.Forms.Button();
      this.refreshLink = new System.Windows.Forms.LinkLabel();
      this.tabStrip.SuspendLayout();
      this.driveTab.SuspendLayout();
      this.metaInfoTab.SuspendLayout();
      this.metaInfoGroupBox.SuspendLayout();
      this.progressTab.SuspendLayout();
      this.SuspendLayout();
      // 
      // backButton
      // 
      this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.backButton.Enabled = false;
      this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.backButton.Location = new System.Drawing.Point(264, 360);
      this.backButton.Name = "backButton";
      this.backButton.Size = new System.Drawing.Size(80, 24);
      this.backButton.TabIndex = 6;
      this.backButton.Text = "< Back";
      this.backButton.Click += new System.EventHandler(this.backButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cancelButton.Location = new System.Drawing.Point(440, 360);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(80, 24);
      this.cancelButton.TabIndex = 7;
      this.cancelButton.Text = "Quit";
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // tabStrip
      // 
      this.tabStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.tabStrip.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
      this.tabStrip.Controls.Add(this.driveTab);
      this.tabStrip.Controls.Add(this.metaInfoTab);
      this.tabStrip.Controls.Add(this.progressTab);
      this.tabStrip.Location = new System.Drawing.Point(8, 80);
      this.tabStrip.Name = "tabStrip";
      this.tabStrip.SelectedIndex = 0;
      this.tabStrip.ShowToolTips = true;
      this.tabStrip.Size = new System.Drawing.Size(512, 264);
      this.tabStrip.TabIndex = 102;
      this.tabStrip.SelectedIndexChanged += new System.EventHandler(this.tabStrip_SelectedIndexChanged);
      // 
      // driveTab
      // 
      this.driveTab.Controls.Add(this.refreshLink);
      this.driveTab.Controls.Add(this.instructionsLabel);
      this.driveTab.Controls.Add(this.drivesList);
      this.driveTab.Controls.Add(this.titleLabel);
      this.driveTab.Controls.Add(this.introLabel);
      this.driveTab.Controls.Add(this.selectDriveLabel);
      this.driveTab.Location = new System.Drawing.Point(4, 25);
      this.driveTab.Name = "driveTab";
      this.driveTab.Size = new System.Drawing.Size(504, 235);
      this.driveTab.TabIndex = 0;
      this.driveTab.Text = "Drive";
      // 
      // instructionsLabel
      // 
      this.instructionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.instructionsLabel.Location = new System.Drawing.Point(8, 208);
      this.instructionsLabel.Name = "instructionsLabel";
      this.instructionsLabel.Size = new System.Drawing.Size(224, 16);
      this.instructionsLabel.TabIndex = 17;
      this.instructionsLabel.Text = "To begin, select a drive and click \'Next\'";
      this.instructionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // drivesList
      // 
      this.drivesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.drivesList.Location = new System.Drawing.Point(248, 32);
      this.drivesList.Name = "drivesList";
      this.drivesList.Size = new System.Drawing.Size(256, 200);
      this.drivesList.SmallImageList = this.imageList;
      this.drivesList.TabIndex = 16;
      this.drivesList.View = System.Windows.Forms.View.List;
      this.drivesList.SelectedIndexChanged += new System.EventHandler(this.drivesList_SelectedIndexChanged);
      // 
      // imageList
      // 
      this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imageList.ImageSize = new System.Drawing.Size(24, 24);
      this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // titleLabel
      // 
      this.titleLabel.AutoSize = true;
      this.titleLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.titleLabel.Location = new System.Drawing.Point(40, 16);
      this.titleLabel.Name = "titleLabel";
      this.titleLabel.Size = new System.Drawing.Size(193, 19);
      this.titleLabel.TabIndex = 14;
      this.titleLabel.Text = "CD / DVD Spindle Search";
      // 
      // introLabel
      // 
      this.introLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left)));
      this.introLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.introLabel.Location = new System.Drawing.Point(16, 40);
      this.introLabel.Name = "introLabel";
      this.introLabel.Size = new System.Drawing.Size(216, 160);
      this.introLabel.TabIndex = 13;
      this.introLabel.Text = @"Spindle Search is a way to index files on your CDs and DVDs so you can search for them using the Google Desktop Search software. Searching each disk is much more time-consuming than searching your hard-drive because it also involves the extra effort of inserting and removing each disk into the drive. Spindle Search makes this easier.";
      this.introLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // selectDriveLabel
      // 
      this.selectDriveLabel.AutoSize = true;
      this.selectDriveLabel.Location = new System.Drawing.Point(248, 16);
      this.selectDriveLabel.Name = "selectDriveLabel";
      this.selectDriveLabel.Size = new System.Drawing.Size(68, 17);
      this.selectDriveLabel.TabIndex = 12;
      this.selectDriveLabel.Text = "Select Drive:";
      // 
      // metaInfoTab
      // 
      this.metaInfoTab.Controls.Add(this.metaInfoGroupBox);
      this.metaInfoTab.Location = new System.Drawing.Point(4, 25);
      this.metaInfoTab.Name = "metaInfoTab";
      this.metaInfoTab.Size = new System.Drawing.Size(504, 235);
      this.metaInfoTab.TabIndex = 1;
      this.metaInfoTab.Text = "Information";
      // 
      // metaInfoGroupBox
      // 
      this.metaInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.metaInfoGroupBox.Controls.Add(this.saveCatalogCheckBox);
      this.metaInfoGroupBox.Controls.Add(this.comments);
      this.metaInfoGroupBox.Controls.Add(this.location);
      this.metaInfoGroupBox.Controls.Add(this.medium);
      this.metaInfoGroupBox.Controls.Add(this.manufacturer);
      this.metaInfoGroupBox.Controls.Add(this.commentsLabel);
      this.metaInfoGroupBox.Controls.Add(this.locationLabel);
      this.metaInfoGroupBox.Controls.Add(this.volumeLabelLabel);
      this.metaInfoGroupBox.Controls.Add(this.mediumLabel);
      this.metaInfoGroupBox.Controls.Add(this.manufacturerLabel);
      this.metaInfoGroupBox.Controls.Add(this.volumeLabel);
      this.metaInfoGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.metaInfoGroupBox.Location = new System.Drawing.Point(-48, -21);
      this.metaInfoGroupBox.Name = "metaInfoGroupBox";
      this.metaInfoGroupBox.Size = new System.Drawing.Size(600, 277);
      this.metaInfoGroupBox.TabIndex = 101;
      this.metaInfoGroupBox.TabStop = false;
      this.metaInfoGroupBox.Text = " Disk Information: ";
      // 
      // saveCatalogCheckBox
      // 
      this.saveCatalogCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.saveCatalogCheckBox.Location = new System.Drawing.Point(156, 228);
      this.saveCatalogCheckBox.Name = "saveCatalogCheckBox";
      this.saveCatalogCheckBox.Size = new System.Drawing.Size(372, 24);
      this.saveCatalogCheckBox.TabIndex = 6;
      this.saveCatalogCheckBox.Text = "Save Disk Catalog as a file (*.catalog)";
      // 
      // comments
      // 
      this.comments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.comments.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.comments.Location = new System.Drawing.Point(156, 120);
      this.comments.Name = "comments";
      this.comments.Size = new System.Drawing.Size(376, 21);
      this.comments.TabIndex = 3;
      this.comments.Text = "";
      // 
      // location
      // 
      this.location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.location.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.location.Location = new System.Drawing.Point(156, 84);
      this.location.Name = "location";
      this.location.Size = new System.Drawing.Size(376, 21);
      this.location.TabIndex = 2;
      this.location.Text = "";
      // 
      // medium
      // 
      this.medium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.medium.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.medium.Location = new System.Drawing.Point(156, 192);
      this.medium.Name = "medium";
      this.medium.Size = new System.Drawing.Size(376, 21);
      this.medium.TabIndex = 5;
      this.medium.Text = "";
      // 
      // manufacturer
      // 
      this.manufacturer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.manufacturer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.manufacturer.Location = new System.Drawing.Point(156, 156);
      this.manufacturer.Name = "manufacturer";
      this.manufacturer.Size = new System.Drawing.Size(376, 21);
      this.manufacturer.TabIndex = 4;
      this.manufacturer.Text = "";
      // 
      // commentsLabel
      // 
      this.commentsLabel.AutoSize = true;
      this.commentsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.commentsLabel.Location = new System.Drawing.Point(84, 123);
      this.commentsLabel.Name = "commentsLabel";
      this.commentsLabel.Size = new System.Drawing.Size(61, 17);
      this.commentsLabel.TabIndex = 5;
      this.commentsLabel.Text = "Comments:";
      // 
      // locationLabel
      // 
      this.locationLabel.AutoSize = true;
      this.locationLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.locationLabel.Location = new System.Drawing.Point(96, 88);
      this.locationLabel.Name = "locationLabel";
      this.locationLabel.Size = new System.Drawing.Size(50, 17);
      this.locationLabel.TabIndex = 4;
      this.locationLabel.Text = "Location:";
      // 
      // volumeLabelLabel
      // 
      this.volumeLabelLabel.AutoSize = true;
      this.volumeLabelLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.volumeLabelLabel.Location = new System.Drawing.Point(72, 52);
      this.volumeLabelLabel.Name = "volumeLabelLabel";
      this.volumeLabelLabel.Size = new System.Drawing.Size(75, 17);
      this.volumeLabelLabel.TabIndex = 0;
      this.volumeLabelLabel.Text = "Volume Label:";
      // 
      // mediumLabel
      // 
      this.mediumLabel.AutoSize = true;
      this.mediumLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.mediumLabel.Location = new System.Drawing.Point(96, 196);
      this.mediumLabel.Name = "mediumLabel";
      this.mediumLabel.Size = new System.Drawing.Size(48, 17);
      this.mediumLabel.TabIndex = 3;
      this.mediumLabel.Text = "Medium:";
      // 
      // manufacturerLabel
      // 
      this.manufacturerLabel.AutoSize = true;
      this.manufacturerLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.manufacturerLabel.Location = new System.Drawing.Point(72, 160);
      this.manufacturerLabel.Name = "manufacturerLabel";
      this.manufacturerLabel.Size = new System.Drawing.Size(74, 17);
      this.manufacturerLabel.TabIndex = 2;
      this.manufacturerLabel.Text = "Manufacturer:";
      // 
      // volumeLabel
      // 
      this.volumeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.volumeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.volumeLabel.Location = new System.Drawing.Point(156, 48);
      this.volumeLabel.Name = "volumeLabel";
      this.volumeLabel.Size = new System.Drawing.Size(376, 21);
      this.volumeLabel.TabIndex = 1;
      this.volumeLabel.Text = "";
      // 
      // progressTab
      // 
      this.progressTab.Controls.Add(this.progressTextField);
      this.progressTab.Location = new System.Drawing.Point(4, 25);
      this.progressTab.Name = "progressTab";
      this.progressTab.Size = new System.Drawing.Size(504, 235);
      this.progressTab.TabIndex = 2;
      this.progressTab.Text = "Progress";
      // 
      // progressTextField
      // 
      this.progressTextField.Dock = System.Windows.Forms.DockStyle.Fill;
      this.progressTextField.Location = new System.Drawing.Point(0, 0);
      this.progressTextField.Name = "progressTextField";
      this.progressTextField.ReadOnly = true;
      this.progressTextField.Size = new System.Drawing.Size(504, 235);
      this.progressTextField.TabIndex = 0;
      this.progressTextField.Text = "";
      this.progressTextField.WordWrap = false;
      // 
      // headerPictureBox
      // 
      this.headerPictureBox.BackColor = System.Drawing.Color.White;
      this.headerPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
      this.headerPictureBox.Location = new System.Drawing.Point(0, 0);
      this.headerPictureBox.Name = "headerPictureBox";
      this.headerPictureBox.Size = new System.Drawing.Size(530, 64);
      this.headerPictureBox.TabIndex = 103;
      this.headerPictureBox.TabStop = false;
      // 
      // headerLabel
      // 
      this.headerLabel.AutoSize = true;
      this.headerLabel.BackColor = System.Drawing.Color.White;
      this.headerLabel.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.headerLabel.ForeColor = System.Drawing.SystemColors.Highlight;
      this.headerLabel.Location = new System.Drawing.Point(64, 8);
      this.headerLabel.Name = "headerLabel";
      this.headerLabel.Size = new System.Drawing.Size(240, 27);
      this.headerLabel.TabIndex = 104;
      this.headerLabel.Text = "CD / DVD Spindle Search";
      // 
      // manastungareWebLink
      // 
      this.manastungareWebLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.manastungareWebLink.AutoSize = true;
      this.manastungareWebLink.BackColor = System.Drawing.Color.White;
      this.manastungareWebLink.Cursor = System.Windows.Forms.Cursors.Hand;
      this.manastungareWebLink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.manastungareWebLink.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.manastungareWebLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.manastungareWebLink.Location = new System.Drawing.Point(352, 40);
      this.manastungareWebLink.Name = "manastungareWebLink";
      this.manastungareWebLink.Size = new System.Drawing.Size(106, 17);
      this.manastungareWebLink.TabIndex = 105;
      this.manastungareWebLink.TabStop = true;
      this.manastungareWebLink.Text = "© Manas Tungare";
      this.manastungareWebLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.manastungareWebLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkManasTungare_LinkClicked);
      // 
      // gdsLabel
      // 
      this.gdsLabel.AutoSize = true;
      this.gdsLabel.BackColor = System.Drawing.Color.White;
      this.gdsLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.gdsLabel.Location = new System.Drawing.Point(80, 40);
      this.gdsLabel.Name = "gdsLabel";
      this.gdsLabel.Size = new System.Drawing.Size(159, 17);
      this.gdsLabel.TabIndex = 106;
      this.gdsLabel.Text = "For Google Desktop Search";
      // 
      // mtIcon
      // 
      this.mtIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.mtIcon.Image = ((System.Drawing.Image)(resources.GetObject("mtIcon.Image")));
      this.mtIcon.Location = new System.Drawing.Point(472, 8);
      this.mtIcon.Name = "mtIcon";
      this.mtIcon.Size = new System.Drawing.Size(48, 48);
      this.mtIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.mtIcon.TabIndex = 107;
      this.mtIcon.TabStop = false;
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.nextButton.Location = new System.Drawing.Point(344, 360);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(80, 24);
      this.nextButton.TabIndex = 7;
      this.nextButton.Text = "Next >";
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // logoIcon
      // 
      this.logoIcon.Image = ((System.Drawing.Image)(resources.GetObject("logoIcon.Image")));
      this.logoIcon.Location = new System.Drawing.Point(8, 8);
      this.logoIcon.Name = "logoIcon";
      this.logoIcon.Size = new System.Drawing.Size(48, 48);
      this.logoIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.logoIcon.TabIndex = 109;
      this.logoIcon.TabStop = false;
      // 
      // loadCatalogFromFile
      // 
      this.loadCatalogFromFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.loadCatalogFromFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.loadCatalogFromFile.Location = new System.Drawing.Point(16, 360);
      this.loadCatalogFromFile.Name = "loadCatalogFromFile";
      this.loadCatalogFromFile.Size = new System.Drawing.Size(104, 24);
      this.loadCatalogFromFile.TabIndex = 110;
      this.loadCatalogFromFile.Text = "Load Catalog ...";
      this.loadCatalogFromFile.Click += new System.EventHandler(this.loadCatalogFromFile_Click);
      // 
      // refreshLink
      // 
      this.refreshLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.refreshLink.Location = new System.Drawing.Point(408, 16);
      this.refreshLink.Name = "refreshLink";
      this.refreshLink.Size = new System.Drawing.Size(96, 16);
      this.refreshLink.TabIndex = 18;
      this.refreshLink.TabStop = true;
      this.refreshLink.Text = "Refresh List";
      this.refreshLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.refreshLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.refreshLink_LinkClicked);
      // 
      // MainWindow
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(530, 399);
      this.Controls.Add(this.loadCatalogFromFile);
      this.Controls.Add(this.logoIcon);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.mtIcon);
      this.Controls.Add(this.gdsLabel);
      this.Controls.Add(this.manastungareWebLink);
      this.Controls.Add(this.headerLabel);
      this.Controls.Add(this.headerPictureBox);
      this.Controls.Add(this.tabStrip);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.backButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "MainWindow";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "CD / DVD Spindle Search for Google Desktop Search";
      this.Load += new System.EventHandler(this.MainWindow_Load);
      this.tabStrip.ResumeLayout(false);
      this.driveTab.ResumeLayout(false);
      this.metaInfoTab.ResumeLayout(false);
      this.metaInfoGroupBox.ResumeLayout(false);
      this.progressTab.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    private const string DISK_CATALOG_FILE_FILTER = "Disk Catalog Files (*.catalog)|*.catalog|All Files (*.*)|*.*";
    private const string CATALOG_FILENAME_INSIDE_ZIP = "SpindleCatalog.xml";
    private const int BUFFER_LENGTH = 4096;

    /// <summary>
    /// List of tabs on the window
    /// </summary>
    private enum IndexerTabs {
      DiskTab,
      MetaInfoTab,
      ProgressTab
    }

    /// <summary>
    /// Stores current tab
    /// </summary>
    private IndexerTabs _currentTab;

    /// <summary>
    /// Gets/sets current tab; setting the current tab also initiates 
    /// related changes (e.g. enable/disable Back/Next buttons).
    /// </summary>
    private IndexerTabs CurrentTab {
      get {
        return _currentTab;
      }
      set {
        _currentTab = value;
        switch (_currentTab) {
          case IndexerTabs.DiskTab:
            tabStrip.SelectedTab = driveTab;
            this.backButton.Enabled = false;
            this.nextButton.Enabled = true;
            break;
          case IndexerTabs.MetaInfoTab:
            tabStrip.SelectedTab = metaInfoTab;
            this.backButton.Enabled = true;
            this.nextButton.Enabled = true;
            break;
          case IndexerTabs.ProgressTab:
            tabStrip.SelectedTab = progressTab;
            this.backButton.Enabled = true;
            this.nextButton.Enabled = false;
            break;
        }
      }
    }

    /// <summary>
    /// Initial population of list of drives &amp; volume names
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainWindow_Load(object sender, System.EventArgs e) {
      RefreshDirectories();
    }

    /// <summary>
    /// Refresh list of drives &amp; volume names
    /// </summary>
    private void RefreshDirectories() {
      drivesList.Items.Clear();
      foreach (string volumeName in System.IO.Directory.GetLogicalDrives()) {
        ListViewItem driveItem = new ListViewItem(volumeName.Substring(0, 2) + " (" + FileSystem.Dir(volumeName, FileAttribute.Volume) + ")" , 0);
        drivesList.Items.Add (driveItem);
      }
    }

    /// <summary>
    /// Quit Application
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cancelButton_Click(object sender, System.EventArgs e) {
      Close();
    }

    /// <summary>
    /// Helper method that, well, duh, starts creating a catalog.
    /// </summary>
    private void StartCreatingCatalog(string saveToCatalogFile) {
      addProgressLine("Starting Catalog Creation ...");
      
      Catalog cat = new Catalog();
      cat.FileCataloged += new Catalog.FileCatalogedEvent(FileCataloged);
      string selDriveLetter = drivesList.SelectedItems[0].Text.Substring(0, 2);
      cat.CreateCatalogFrom(new DirectoryInfo( selDriveLetter + Path.DirectorySeparatorChar ), selDriveLetter.Replace( ":", "" ).ToLower());
      cat.AddMetaInformation("Volume", this.volumeLabel.Text.Trim());
      cat.AddMetaInformation("Manufacturer", this.manufacturer.Text.Trim());
      cat.AddMetaInformation("Medium", this.medium.Text.Trim());
      cat.AddMetaInformation("Location", this.location.Text.Trim());
      cat.AddMetaInformation("Comments", this.comments.Text.Trim());

      // Write a catalog file to the current directory, for debugging purposes.
      try {
        FileStream testCatalogFile = new FileStream(Application.StartupPath + "\\CurrentDisk.catalog", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        cat.ExportTo(testCatalogFile);
        testCatalogFile.Close();
      }
      catch(Exception ex) {
        MessageBox.Show("Spindle Search was unable to write the catalog to disk; the error reported was: " + ex.Message,
          "Error writing Catalog to disk",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      // If user chose to export, save another copy.
      if (0 != saveToCatalogFile.Length) {
        try {
          // First have the catalog write itself out to a MemoryStream
          MemoryStream bufferStream = new MemoryStream();
          cat.ExportTo( bufferStream );

          // Reset seek position on MemoryStream
          bufferStream.Seek(0, SeekOrigin.Begin);

          // Now create a Zip file
          ZipWriter zipFile = new ZipWriter(new FileInfo(saveToCatalogFile));
          zipFile.AddStream(bufferStream, CATALOG_FILENAME_INSIDE_ZIP);
          bufferStream.Close();
          zipFile.Close();
        }
        catch(Exception ex) {
          MessageBox.Show("There was an error trying to save the catalog to disk: " + ex.Message);
        }
      }

      addProgressLine("Adding Catalog to Google Desktop Search ...");

      Indexer gds = new Indexer ();
      gds.FileIndexed += new Indexer.FileIndexedEvent(FileIndexed);

      try {
        gds.IndexCatalog(cat);
        addProgressLine("Completed!");
        MessageBox.Show ("Catalog created and added successfully to Google Desktop Search.",
          "Google Desktop Search Catalog",
          MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch(GoogleDesktopException e) {
        switch (e.GoogleErrorCode) {
          case GoogleDesktopException.GoogleErrorCodes.E_SERVICE_NOT_RUNNING:
            MessageBox.Show ("The Google Desktop Search Service does not appear to be running. " + 
              "\nPlease start Google Desktop Search before adding spindles to the index.",
              "Google Desktop Search : Not Running",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;
          case GoogleDesktopException.GoogleErrorCodes.S_INDEXING_PAUSED:
            MessageBox.Show ("The Google Desktop Search Service has currently been paused and no longer" +
              "actively indexing your content.\nPlease resume indexing before adding spindles to the index.",
              "Google Desktop Search : Indexing Paused",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;
          default:
            MessageBox.Show ("An Error Occurred: " + e.GoogleErrorCode + ": " + e.InnerException.Message,
              "Google Desktop Search Error",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;
        }
      }
    }

    /// <summary>
    /// Change tabs, initiate action based on current tab.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void backButton_Click(object sender, System.EventArgs e) {
      switch (CurrentTab) {
        case IndexerTabs.DiskTab:
          break;
        case IndexerTabs.MetaInfoTab:
          CurrentTab = IndexerTabs.DiskTab;
          RefreshDirectories();
          break;
        case IndexerTabs.ProgressTab:
          CurrentTab = IndexerTabs.MetaInfoTab;
          break;
      }
    }

    /// <summary>
    /// Change tabs, initiate action based on current tab.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void nextButton_Click(object sender, System.EventArgs e) {
      switch (CurrentTab) {
        case IndexerTabs.DiskTab:
          CurrentTab = IndexerTabs.MetaInfoTab;
          volumeLabel.Focus();
          break;
        case IndexerTabs.MetaInfoTab:
          string validationSummary = IsMetaInfoValid();
          if ( (0 == validationSummary.Length) || 
            (DialogResult.Yes == MessageBox.Show(validationSummary + "\n\nDo you wish to index the disk anyway?", "Information Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))) {

            string saveToCatalogFile = "";
            if (this.saveCatalogCheckBox.Checked) {
              FileDialog chooseSaveFile = new SaveFileDialog();
              chooseSaveFile.AddExtension = true;
              chooseSaveFile.DefaultExt = ".catalog";
              chooseSaveFile.Filter = DISK_CATALOG_FILE_FILTER;
              chooseSaveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
              chooseSaveFile.Title = "Choose Save Destination ...";
              chooseSaveFile.ShowDialog();

              if (0 != chooseSaveFile.FileName.Length) {
                saveToCatalogFile = chooseSaveFile.FileName;
              }
            }

            CurrentTab = IndexerTabs.ProgressTab;
            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;
            StartCreatingCatalog(saveToCatalogFile); // Don't save if blank.
            CurrentTab = IndexerTabs.DiskTab;
          }
          break;
      }
    }
  
    /// <summary>
    /// Helper method to validate if at least some of the meta-information fields
    /// were filled in. If not, it is likely to be an error.
    /// </summary>
    /// <returns>a summary of the validation performed if invalid; an empty string if all data items are valid.</returns>
    private string IsMetaInfoValid() {
      if (0 == this.volumeLabel.Text.Length &&
        0 == this.manufacturer.Text.Length &&
        0 == this.medium.Text.Length &&
        0 == this.location.Text.Length &&
        0 == this.comments.Text.Length) {
        return 
          "You have not entered any information that might help you locate this disk after it is indexed." +
          "\nTypically, you would enter at least the location and volume label.";
      }
      return "";
    }

    /// <summary>
    /// Update current tab if user clicked on the tab-button to change tabs.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tabStrip_SelectedIndexChanged(object sender, System.EventArgs e) {
      switch (tabStrip.SelectedIndex) {
        case 0:
          _currentTab = IndexerTabs.DiskTab;
          break;
        case 1:
          _currentTab = IndexerTabs.MetaInfoTab;
          break;
        case 2:
          _currentTab = IndexerTabs.ProgressTab;
          break;
      }
    }

    /// <summary>
    /// Handle events sent by the Google Desktop Indexer module; add a line to the progress log.
    /// </summary>
    /// <param name="o"></param>
    /// <param name="e"></param>
    public void FileIndexed(object o, FileIndexedEventArgs e) {
      Console.WriteLine(e.File);
      addProgressLine("Indexed: " + e.File);
    }

    /// <summary>
    /// Handle events sent by the Cataloging module; add a line to the progress log.
    /// </summary>
    /// <param name="o"></param>
    /// <param name="e"></param>
    public void FileCataloged(object o, FileCatalogedEventArgs e) {
      addProgressLine("Cataloged: " + e.File);
    }

    /// <summary>
    /// Open a browser to author's homepage.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void lnkManasTungare_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
      try {
        ProcessStartInfo spawnBrowser = new ProcessStartInfo();
        spawnBrowser.FileName = "http://www.manastungare.com/";
        Process.Start(spawnBrowser);
      }
      catch(Exception ex) {
        // Ignore
      }
    }

    /// <summary>
    /// Helper method; adds a line to the progress log.
    /// </summary>
    /// <param name="message"></param>
    private void addProgressLine(string message) {
      this.progressTextField.AppendText(message + "\r\n");
    }

    /// <summary>
    /// Auto-fill the volume label when the user selects a different disk.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void drivesList_SelectedIndexChanged(object sender, System.EventArgs e) {
      if ( drivesList.SelectedItems.Count > 0 ) {
        string selDrive = drivesList.SelectedItems[0].Text.ToString().Substring(0, 2);
        volumeLabel.Text = FileSystem.Dir(selDrive , FileAttribute.Volume);
      }
    }

    private void loadCatalogFromFile_Click(object sender, System.EventArgs e) {
      LoadCatalogFromFile();
    }

    /// <summary>
    /// Loads a saved catalog from file to add to the index. This method is mainly to 
    /// enable moving catalogs from one machine to another, both running GDS. Since this is 
    /// for removable media, it is entirely possible to use the same media on different
    /// machines. In such a case, the user should not need to reindex the drive.
    /// </summary>
    private void LoadCatalogFromFile() {
      // Display FileOpen dialog
      FileDialog fileOpenDialog = new OpenFileDialog();
      fileOpenDialog.CheckFileExists = true;
      fileOpenDialog.Filter = DISK_CATALOG_FILE_FILTER;
      fileOpenDialog.Title = "Select a Catalog ...";
      fileOpenDialog.ShowDialog();

      if (0 != fileOpenDialog.FileName.Length) {
        // Update UI
        CurrentTab = IndexerTabs.ProgressTab;
        Application.DoEvents();
        Cursor.Current = Cursors.WaitCursor;

        // Open zip file
        ZipInputStream zipStream = new ZipInputStream(File.OpenRead(fileOpenDialog.FileName));
        ZipEntry zipEntry;
        Catalog fromCatalogFile = new Catalog();
        try {
          while ((zipEntry = zipStream.GetNextEntry()) != null) {
            if (zipEntry.Name.Equals(CATALOG_FILENAME_INSIDE_ZIP )) {
              fromCatalogFile.LoadCatalogFrom(zipStream);
              fromCatalogFile.ExportTo(new FileInfo("D:\\test.txt"));
            }
          }
        }
        catch (Exception ex) {
          MessageBox.Show ("The file you selected does not appear to be a valid Disk Catalog generated by Spindle Search.",
            "Spindle Search: Invalid Catalog File",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
          CurrentTab = IndexerTabs.DiskTab;
          return;
        }

        // Create Indexer 
        Indexer gds = new Indexer ();
        gds.FileIndexed += new Indexer.FileIndexedEvent(FileIndexed);

        // Start Indexing
        try {
          gds.IndexCatalog(fromCatalogFile);
          addProgressLine("Completed!");
          MessageBox.Show ("Catalog added successfully to Google Desktop Search.",
            "Google Desktop Search Catalog",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch(GoogleDesktopException ex) {
          switch (ex.GoogleErrorCode) {
            case GoogleDesktopException.GoogleErrorCodes.E_SERVICE_NOT_RUNNING:
              MessageBox.Show ("The Google Desktop Search Service does not appear to be running. " + 
                "\nPlease start Google Desktop Search before adding spindles to the index.",
                "Google Desktop Search : Not Running",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
              break;
            case GoogleDesktopException.GoogleErrorCodes.S_INDEXING_PAUSED:
              MessageBox.Show ("The Google Desktop Search Service has currently been paused and no longer" +
                "actively indexing your content.\nPlease resume indexing before adding spindles to the index.",
                "Google Desktop Search : Indexing Paused",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
              break;
            default:
              MessageBox.Show ("An Error Occurred: " + ex.GoogleErrorCode + ": " + ex.InnerException.Message,
                "Google Desktop Search Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
              break;
          }
          CurrentTab = IndexerTabs.DiskTab;
          return;
        }
      }
    }

    private void refreshLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
      RefreshDirectories();
    }
  }
}
