namespace WareHousingMaster.view.common
{
    partial class dlgColVisible
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgColVisible));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcVisibleYn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileVisibleYn = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBarcodeList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileVisibleYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcList);
            this.layoutControl1.Controls.Add(this.sbSave);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(300, 343);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcList
            // 
            this.gcList.Location = new System.Drawing.Point(5, 31);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileVisibleYn});
            this.gcList.Size = new System.Drawing.Size(290, 307);
            this.gcList.TabIndex = 8;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcName,
            this.gcVisibleYn});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvList.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save;
            this.gvList.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Direct;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvList_FocusedRowChanged);
            this.gvList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvList_CellValueChanged);
            // 
            // gcName
            // 
            this.gcName.AppearanceCell.Options.UseTextOptions = true;
            this.gcName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcName.AppearanceHeader.Options.UseTextOptions = true;
            this.gcName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcName.Caption = "컬럼명";
            this.gcName.FieldName = "COL_NAME";
            this.gcName.Name = "gcName";
            this.gcName.OptionsColumn.AllowEdit = false;
            this.gcName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BARCODE", "{0}")});
            this.gcName.Visible = true;
            this.gcName.VisibleIndex = 0;
            this.gcName.Width = 167;
            // 
            // gcVisibleYn
            // 
            this.gcVisibleYn.AppearanceCell.Options.UseTextOptions = true;
            this.gcVisibleYn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcVisibleYn.AppearanceHeader.Options.UseTextOptions = true;
            this.gcVisibleYn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcVisibleYn.Caption = "VISIBLE_YN";
            this.gcVisibleYn.ColumnEdit = this.rileVisibleYn;
            this.gcVisibleYn.FieldName = "VISIBLE_YN";
            this.gcVisibleYn.MinWidth = 45;
            this.gcVisibleYn.Name = "gcVisibleYn";
            this.gcVisibleYn.Visible = true;
            this.gcVisibleYn.VisibleIndex = 1;
            // 
            // rileVisibleYn
            // 
            this.rileVisibleYn.AutoHeight = false;
            this.rileVisibleYn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileVisibleYn.DropDownRows = 2;
            this.rileVisibleYn.Name = "rileVisibleYn";
            this.rileVisibleYn.Popup += new System.EventHandler(this.rileVisibleYn_Popup);
            // 
            // sbSave
            // 
            this.sbSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSave.ImageOptions.Image")));
            this.sbSave.Location = new System.Drawing.Point(201, 3);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(96, 22);
            this.sbSave.StyleController = this.layoutControl1;
            this.sbSave.TabIndex = 6;
            this.sbSave.Text = "확인";
            this.sbSave.Click += new System.EventHandler(this.sbSave_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgBarcodeList,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(300, 343);
            this.Root.TextVisible = false;
            // 
            // lcgBarcodeList
            // 
            buttonImageOptions1.Image = global::WareHousingMaster.Properties.Resources.trash_16x161;
            buttonImageOptions2.Image = global::WareHousingMaster.Properties.Resources.checkbox2_16x161;
            this.lcgBarcodeList.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("삭제", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, 1, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("전체선택", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, false, true, null, -1)});
            this.lcgBarcodeList.CustomizationFormText = "리스트";
            this.lcgBarcodeList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBarcodeList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgBarcodeList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.lcgBarcodeList.Location = new System.Drawing.Point(0, 26);
            this.lcgBarcodeList.Name = "lcgBarcodeList";
            this.lcgBarcodeList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgBarcodeList.Size = new System.Drawing.Size(298, 315);
            this.lcgBarcodeList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBarcodeList.Text = "현황";
            this.lcgBarcodeList.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcList;
            this.layoutControlItem11.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(294, 311);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbSave;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem1.Location = new System.Drawing.Point(198, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem14";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(198, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // dlgColVisible
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 343);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgColVisible";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "컬럼 설정";
            this.Load += new System.EventHandler(this.dlgUpdateWarehouseMovementList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileVisibleYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraGrid.Columns.GridColumn gcName;
        private DevExpress.XtraGrid.Columns.GridColumn gcVisibleYn;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBarcodeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileVisibleYn;
    }
}