namespace WareHousingMaster.view.warehousingManagement
{
    partial class dlgUpdateWarehouseMovementList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgUpdateWarehouseMovementList));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcBarcodeList = new DevExpress.XtraGrid.GridControl();
            this.gvBarcodeList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riceCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.leWarehousingWarehouseNo = new DevExpress.XtraEditors.LookUpEdit();
            this.leWarehousingPalletNo = new DevExpress.XtraEditors.LookUpEdit();
            this.teInputBarcode = new DevExpress.XtraEditors.TextEdit();
            this.sbInput = new DevExpress.XtraEditors.SimpleButton();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBarcodeList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leWarehousingWarehouseNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leWarehousingPalletNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teInputBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcBarcodeList);
            this.layoutControl1.Controls.Add(this.leWarehousingWarehouseNo);
            this.layoutControl1.Controls.Add(this.leWarehousingPalletNo);
            this.layoutControl1.Controls.Add(this.teInputBarcode);
            this.layoutControl1.Controls.Add(this.sbInput);
            this.layoutControl1.Controls.Add(this.sbSave);
            this.layoutControl1.Controls.Add(this.sbClose);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(526, 481);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcBarcodeList
            // 
            this.gcBarcodeList.Location = new System.Drawing.Point(5, 101);
            this.gcBarcodeList.MainView = this.gvBarcodeList;
            this.gcBarcodeList.Name = "gcBarcodeList";
            this.gcBarcodeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riceCheck});
            this.gcBarcodeList.Size = new System.Drawing.Size(516, 375);
            this.gcBarcodeList.TabIndex = 8;
            this.gcBarcodeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBarcodeList});
            // 
            // gvBarcodeList
            // 
            this.gvBarcodeList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gc3,
            this.gridColumn5,
            this.gcCheck});
            this.gvBarcodeList.GridControl = this.gcBarcodeList;
            this.gvBarcodeList.Name = "gvBarcodeList";
            this.gvBarcodeList.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvBarcodeList.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save;
            this.gvBarcodeList.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Direct;
            this.gvBarcodeList.OptionsView.ShowFooter = true;
            this.gvBarcodeList.OptionsView.ShowGroupPanel = false;
            this.gvBarcodeList.OptionsView.ShowIndicator = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "INVENTORY_ID";
            this.gridColumn11.FieldName = "INVENTORY_ID";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Tag = "";
            // 
            // gc3
            // 
            this.gc3.AppearanceHeader.Options.UseTextOptions = true;
            this.gc3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc3.Caption = "관리번호";
            this.gc3.FieldName = "BARCODE";
            this.gc3.Name = "gc3";
            this.gc3.OptionsColumn.AllowEdit = false;
            this.gc3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BARCODE", "{0}")});
            this.gc3.Visible = true;
            this.gc3.VisibleIndex = 0;
            this.gc3.Width = 165;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "품목명";
            this.gridColumn5.FieldName = "COMPONENT_CD";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 115;
            // 
            // gcCheck
            // 
            this.gcCheck.AppearanceCell.Options.UseTextOptions = true;
            this.gcCheck.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.Caption = "선택";
            this.gcCheck.ColumnEdit = this.riceCheck;
            this.gcCheck.FieldName = "CHECK";
            this.gcCheck.MinWidth = 30;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.Visible = true;
            this.gcCheck.VisibleIndex = 2;
            this.gcCheck.Width = 45;
            // 
            // riceCheck
            // 
            this.riceCheck.AutoHeight = false;
            this.riceCheck.Name = "riceCheck";
            // 
            // leWarehousingWarehouseNo
            // 
            this.leWarehousingWarehouseNo.Location = new System.Drawing.Point(93, 29);
            this.leWarehousingWarehouseNo.Name = "leWarehousingWarehouseNo";
            this.leWarehousingWarehouseNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leWarehousingWarehouseNo.Properties.NullText = "";
            this.leWarehousingWarehouseNo.Properties.ReadOnly = true;
            this.leWarehousingWarehouseNo.Size = new System.Drawing.Size(159, 20);
            this.leWarehousingWarehouseNo.StyleController = this.layoutControl1;
            this.leWarehousingWarehouseNo.TabIndex = 3;
            // 
            // leWarehousingPalletNo
            // 
            this.leWarehousingPalletNo.Location = new System.Drawing.Point(346, 29);
            this.leWarehousingPalletNo.Name = "leWarehousingPalletNo";
            this.leWarehousingPalletNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leWarehousingPalletNo.Properties.NullText = "";
            this.leWarehousingPalletNo.Properties.ReadOnly = true;
            this.leWarehousingPalletNo.Size = new System.Drawing.Size(177, 20);
            this.leWarehousingPalletNo.StyleController = this.layoutControl1;
            this.leWarehousingPalletNo.TabIndex = 4;
            // 
            // teInputBarcode
            // 
            this.teInputBarcode.Location = new System.Drawing.Point(93, 55);
            this.teInputBarcode.Name = "teInputBarcode";
            this.teInputBarcode.Size = new System.Drawing.Size(330, 20);
            this.teInputBarcode.StyleController = this.layoutControl1;
            this.teInputBarcode.TabIndex = 5;
            this.teInputBarcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.teInputBarcode_KeyUp);
            // 
            // sbInput
            // 
            this.sbInput.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbInput.ImageOptions.Image")));
            this.sbInput.Location = new System.Drawing.Point(427, 54);
            this.sbInput.Name = "sbInput";
            this.sbInput.Size = new System.Drawing.Size(96, 22);
            this.sbInput.StyleController = this.layoutControl1;
            this.sbInput.TabIndex = 6;
            this.sbInput.Text = "입력";
            this.sbInput.Click += new System.EventHandler(this.sbInput_Click);
            // 
            // sbSave
            // 
            this.sbSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSave.ImageOptions.Image")));
            this.sbSave.Location = new System.Drawing.Point(327, 3);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(96, 22);
            this.sbSave.StyleController = this.layoutControl1;
            this.sbSave.TabIndex = 6;
            this.sbSave.Text = "저장";
            this.sbSave.Click += new System.EventHandler(this.sbSave_Click);
            // 
            // sbClose
            // 
            this.sbClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbClose.ImageOptions.Image")));
            this.sbClose.Location = new System.Drawing.Point(427, 3);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(96, 22);
            this.sbClose.StyleController = this.layoutControl1;
            this.sbClose.TabIndex = 6;
            this.sbClose.Text = "닫기";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgBarcodeList,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(526, 481);
            this.Root.TextVisible = false;
            // 
            // lcgBarcodeList
            // 
            buttonImageOptions1.Image = global::WareHousingMaster.Properties.Resources.trash_16x161;
            buttonImageOptions2.Image = global::WareHousingMaster.Properties.Resources.checkbox2_16x161;
            this.lcgBarcodeList.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("삭제", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, 1, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("전체선택", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, false, true, null, -1)});
            this.lcgBarcodeList.CustomizationFormText = "현황";
            this.lcgBarcodeList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBarcodeList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgBarcodeList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.lcgBarcodeList.Location = new System.Drawing.Point(0, 77);
            this.lcgBarcodeList.Name = "lcgBarcodeList";
            this.lcgBarcodeList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgBarcodeList.Size = new System.Drawing.Size(524, 402);
            this.lcgBarcodeList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBarcodeList.Text = "현황";
            this.lcgBarcodeList.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonClick);
            this.lcgBarcodeList.CustomButtonUnchecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonUnchecked);
            this.lcgBarcodeList.CustomButtonChecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonChecked);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcBarcodeList;
            this.layoutControlItem11.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(520, 379);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem18.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem18.Control = this.leWarehousingWarehouseNo;
            this.layoutControlItem18.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem18.CustomizationFormText = "입고번호";
            this.layoutControlItem18.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem18.ImageOptions.Image")));
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem18.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(253, 25);
            this.layoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem18.Text = "입고창고";
            this.layoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem18.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem18.TextToControlDistance = 10;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem19.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem19.Control = this.leWarehousingPalletNo;
            this.layoutControlItem19.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem19.CustomizationFormText = "입고번호";
            this.layoutControlItem19.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem19.ImageOptions.Image")));
            this.layoutControlItem19.Location = new System.Drawing.Point(253, 26);
            this.layoutControlItem19.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(271, 25);
            this.layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem19.Text = "적재위치";
            this.layoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem19.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem19.TextToControlDistance = 10;
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem23.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem23.Control = this.teInputBarcode;
            this.layoutControlItem23.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem23.CustomizationFormText = "입고번호";
            this.layoutControlItem23.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem23.ImageOptions.Image")));
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 51);
            this.layoutControlItem23.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem23.MinSize = new System.Drawing.Size(144, 24);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(424, 26);
            this.layoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem23.Text = "관리번호";
            this.layoutControlItem23.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem23.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem23.TextToControlDistance = 10;
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.sbInput;
            this.layoutControlItem24.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem24.Location = new System.Drawing.Point(424, 51);
            this.layoutControlItem24.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem24.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem24.Text = "layoutControlItem14";
            this.layoutControlItem24.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem24.TextToControlDistance = 0;
            this.layoutControlItem24.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbSave;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem1.Location = new System.Drawing.Point(324, 0);
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
            this.emptySpaceItem1.Size = new System.Drawing.Size(324, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbClose;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(424, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem14";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // dlgUpdateWarehouseMovementList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 481);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgUpdateWarehouseMovementList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "등록 부품 수정";
            this.Load += new System.EventHandler(this.dlgUpdateWarehouseMovementList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leWarehousingWarehouseNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leWarehousingPalletNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teInputBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcBarcodeList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBarcodeList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gc3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riceCheck;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBarcodeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.LookUpEdit leWarehousingWarehouseNo;
        private DevExpress.XtraEditors.LookUpEdit leWarehousingPalletNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraEditors.TextEdit teInputBarcode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraEditors.SimpleButton sbInput;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}