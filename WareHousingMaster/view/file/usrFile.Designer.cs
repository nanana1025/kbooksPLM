namespace WareHousingMaster.view.file
{
    partial class usrFile
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrFile));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcFile = new DevExpress.XtraGrid.GridControl();
            this.gvFile = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFileType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricbExtension = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icExtension = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCreateDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCreateUserId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileUserId = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricbDownload = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icDownload = new DevExpress.Utils.ImageCollection(this.components);
            this.lcgFile = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricbExtension)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icExtension)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricbDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcFile);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.lcgFile;
            this.layoutControl1.Size = new System.Drawing.Size(585, 292);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcFile
            // 
            this.gcFile.Location = new System.Drawing.Point(4, 25);
            this.gcFile.MainView = this.gvFile;
            this.gcFile.Name = "gcFile";
            this.gcFile.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricbExtension,
            this.ricbDownload,
            this.rileUserId});
            this.gcFile.Size = new System.Drawing.Size(577, 263);
            this.gcFile.TabIndex = 8;
            this.gcFile.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFile});
            // 
            // gvFile
            // 
            this.gvFile.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn21,
            this.gcFileType,
            this.gridColumn22,
            this.gcCreateDt,
            this.gcCreateUserId,
            this.gridColumn13});
            this.gvFile.GridControl = this.gcFile;
            this.gvFile.Name = "gvFile";
            this.gvFile.OptionsView.ShowAutoFilterRow = true;
            this.gvFile.OptionsView.ShowGroupPanel = false;
            this.gvFile.OptionsView.ShowIndicator = false;
            this.gvFile.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvFile_RowCellClick);
            this.gvFile.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvFile_FocusedRowObjectChanged);
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn3.Caption = "체크";
            this.gridColumn3.FieldName = "CHECK";
            this.gridColumn3.MaxWidth = 40;
            this.gridColumn3.MinWidth = 40;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 40;
            // 
            // gridColumn21
            // 
            this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn21.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn21.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn21.Caption = "No";
            this.gridColumn21.DisplayFormat.FormatString = "n0";
            this.gridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn21.FieldName = "NO";
            this.gridColumn21.MaxWidth = 30;
            this.gridColumn21.MinWidth = 30;
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.OptionsColumn.ReadOnly = true;
            this.gridColumn21.Width = 30;
            // 
            // gcFileType
            // 
            this.gcFileType.AppearanceCell.Options.UseTextOptions = true;
            this.gcFileType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcFileType.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcFileType.AppearanceHeader.Options.UseBackColor = true;
            this.gcFileType.AppearanceHeader.Options.UseTextOptions = true;
            this.gcFileType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcFileType.Caption = "file type";
            this.gcFileType.ColumnEdit = this.ricbExtension;
            this.gcFileType.FieldName = "EXTENSION";
            this.gcFileType.MaxWidth = 50;
            this.gcFileType.Name = "gcFileType";
            this.gcFileType.OptionsColumn.AllowEdit = false;
            this.gcFileType.OptionsColumn.ReadOnly = true;
            this.gcFileType.Visible = true;
            this.gcFileType.VisibleIndex = 0;
            this.gcFileType.Width = 50;
            // 
            // ricbExtension
            // 
            this.ricbExtension.AutoHeight = false;
            this.ricbExtension.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricbExtension.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ricbExtension.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "xlsx", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "pdf", 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "docx", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "img", 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "etc", 4)});
            this.ricbExtension.Name = "ricbExtension";
            this.ricbExtension.SmallImages = this.icExtension;
            // 
            // icExtension
            // 
            this.icExtension.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icExtension.ImageStream")));
            // 
            // gridColumn22
            // 
            this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn22.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn22.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn22.Caption = "file name";
            this.gridColumn22.FieldName = "FILE_NM";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.OptionsColumn.ReadOnly = true;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 1;
            this.gridColumn22.Width = 267;
            // 
            // gcCreateDt
            // 
            this.gcCreateDt.AppearanceCell.Options.UseTextOptions = true;
            this.gcCreateDt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcCreateDt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCreateDt.AppearanceHeader.Options.UseBackColor = true;
            this.gcCreateDt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCreateDt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCreateDt.Caption = "uploaded dt";
            this.gcCreateDt.FieldName = "CREATE_DT";
            this.gcCreateDt.Name = "gcCreateDt";
            this.gcCreateDt.OptionsColumn.AllowEdit = false;
            this.gcCreateDt.OptionsColumn.ReadOnly = true;
            this.gcCreateDt.Visible = true;
            this.gcCreateDt.VisibleIndex = 2;
            this.gcCreateDt.Width = 109;
            // 
            // gcCreateUserId
            // 
            this.gcCreateUserId.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCreateUserId.AppearanceHeader.Options.UseBackColor = true;
            this.gcCreateUserId.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCreateUserId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCreateUserId.Caption = "uploaded user";
            this.gcCreateUserId.ColumnEdit = this.rileUserId;
            this.gcCreateUserId.FieldName = "CREATE_USER_ID";
            this.gcCreateUserId.Name = "gcCreateUserId";
            this.gcCreateUserId.OptionsColumn.AllowEdit = false;
            this.gcCreateUserId.OptionsColumn.ReadOnly = true;
            this.gcCreateUserId.Visible = true;
            this.gcCreateUserId.VisibleIndex = 3;
            this.gcCreateUserId.Width = 82;
            // 
            // rileUserId
            // 
            this.rileUserId.AutoHeight = false;
            this.rileUserId.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileUserId.Name = "rileUserId";
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn13.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "download";
            this.gridColumn13.ColumnEdit = this.ricbDownload;
            this.gridColumn13.FieldName = "DOWNLOAD";
            this.gridColumn13.MaxWidth = 60;
            this.gridColumn13.MinWidth = 50;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.ReadOnly = true;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            this.gridColumn13.Width = 50;
            // 
            // ricbDownload
            // 
            this.ricbDownload.AutoHeight = false;
            this.ricbDownload.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricbDownload.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ricbDownload.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 1)});
            this.ricbDownload.Name = "ricbDownload";
            this.ricbDownload.SmallImages = this.icDownload;
            // 
            // icDownload
            // 
            this.icDownload.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDownload.ImageStream")));
            // 
            // lcgFile
            // 
            this.lcgFile.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcgFile.CaptionImageOptions.Image")));
            buttonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions1.Image")));
            buttonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions2.Image")));
            this.lcgFile.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("add", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, 1, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("delete", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, 2, -1)});
            this.lcgFile.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgFile.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgFile.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcgFile.Name = "lcgFile";
            this.lcgFile.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgFile.Size = new System.Drawing.Size(585, 292);
            this.lcgFile.Text = "attachement file";
            this.lcgFile.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgFile_CustomButtonClick);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcFile;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(581, 267);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // usrFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "usrFile";
            this.Size = new System.Drawing.Size(585, 292);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricbExtension)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icExtension)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricbDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup lcgFile;
        private DevExpress.XtraGrid.GridControl gcFile;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gcFileType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gcCreateDt;
        private DevExpress.XtraGrid.Columns.GridColumn gcCreateUserId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ricbExtension;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ricbDownload;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileUserId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.Utils.ImageCollection icExtension;
        private DevExpress.Utils.ImageCollection icDownload;
    }
}
