using DevExpress.XtraLayout;
using static System.Windows.Forms.ImageList;

namespace WareHousingMaster.view.adjustment
{
    partial class usrAdjustmentReceiptList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrAdjustmentReceiptList));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions3 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions4 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions5 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.rideCreateDt = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.deDtFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.deDtTo = new DevExpress.XtraEditors.DateEdit();
            this.leReceiptState = new DevExpress.XtraEditors.LookUpEdit();
            this.leAdjustmentState = new DevExpress.XtraEditors.LookUpEdit();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.teProductCnt = new DevExpress.XtraEditors.SpinEdit();
            this.teTotalPrice = new DevExpress.XtraEditors.SpinEdit();
            this.teTax = new DevExpress.XtraEditors.SpinEdit();
            this.tePurchasedPrice = new DevExpress.XtraEditors.SpinEdit();
            this.teAdjustmentPrice = new DevExpress.XtraEditors.SpinEdit();
            this.teProductPrice = new DevExpress.XtraEditors.SpinEdit();
            this.rgAdjustmentState = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcAdjustmentList = new DevExpress.XtraGrid.GridControl();
            this.gvAdjustmentList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileCompanyId = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileAdjustmentState = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcInventoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcApprovalUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileUserId = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcCreateDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileComapnyId2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcWarehousing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBarcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcInitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAdjust = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riicbeCnt = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icAdjust = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcManufactureType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileManufactureType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcMBDManufactureNm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMBDModelNm1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMBDModelNm2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCpu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileCategory = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcGeneration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileNickName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcMonSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCheckYn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riicbCheckYn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icCheckYn = new DevExpress.Utils.ImageCollection(this.components);
            this.gcProductGrade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileProductGrade = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcDestroyedCase = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcScracth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStabbed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPressed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDisColored = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHinge = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileHinge = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcCaseDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDisplay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUsb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMousePad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcKeyboard = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBattery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWirelessLan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWiredLan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcODD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHDD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBios = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTestCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSTATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riicbHinge = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icHinge = new DevExpress.Utils.ImageCollection(this.components);
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBarcodeList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leReceiptState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leAdjustmentState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teProductCnt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTotalPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchasedPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAdjustmentPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teProductPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgAdjustmentState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAdjustmentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAdjustmentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCompanyId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileAdjustmentState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComapnyId2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbeCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAdjust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileManufactureType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileNickName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbCheckYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCheckYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileProductGrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileHinge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbHinge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icHinge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            this.SuspendLayout();
            // 
            // rideCreateDt
            // 
            this.rideCreateDt.AutoHeight = false;
            this.rideCreateDt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rideCreateDt.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rideCreateDt.Name = "rideCreateDt";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl4);
            this.layoutControl1.Controls.Add(this.layoutControl3);
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Controls.Add(this.gcList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1354, 594);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.deDtFrom);
            this.layoutControl4.Controls.Add(this.labelControl11);
            this.layoutControl4.Controls.Add(this.deDtTo);
            this.layoutControl4.Controls.Add(this.leReceiptState);
            this.layoutControl4.Controls.Add(this.leAdjustmentState);
            this.layoutControl4.Controls.Add(this.sbSearch);
            this.layoutControl4.Location = new System.Drawing.Point(3, 3);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup3;
            this.layoutControl4.Size = new System.Drawing.Size(1348, 26);
            this.layoutControl4.TabIndex = 12;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // deDtFrom
            // 
            this.deDtFrom.EditValue = null;
            this.deDtFrom.Location = new System.Drawing.Point(77, 3);
            this.deDtFrom.Name = "deDtFrom";
            this.deDtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtFrom.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deDtFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtFrom.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deDtFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtFrom.Properties.Mask.EditMask = "";
            this.deDtFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.deDtFrom.Properties.NullText = "선택";
            this.deDtFrom.Size = new System.Drawing.Size(111, 20);
            this.deDtFrom.StyleController = this.layoutControl4;
            this.deDtFrom.TabIndex = 4;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(192, 2);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(9, 14);
            this.labelControl11.StyleController = this.layoutControl4;
            this.labelControl11.TabIndex = 6;
            this.labelControl11.Text = "~";
            // 
            // deDtTo
            // 
            this.deDtTo.EditValue = null;
            this.deDtTo.Location = new System.Drawing.Point(205, 3);
            this.deDtTo.Name = "deDtTo";
            this.deDtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtTo.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deDtTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtTo.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deDtTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtTo.Properties.Mask.EditMask = "";
            this.deDtTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.deDtTo.Properties.NullText = "선택";
            this.deDtTo.Size = new System.Drawing.Size(106, 20);
            this.deDtTo.StyleController = this.layoutControl4;
            this.deDtTo.TabIndex = 4;
            // 
            // leReceiptState
            // 
            this.leReceiptState.Location = new System.Drawing.Point(390, 3);
            this.leReceiptState.Name = "leReceiptState";
            this.leReceiptState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leReceiptState.Properties.NullText = "선택";
            this.leReceiptState.Size = new System.Drawing.Size(111, 20);
            this.leReceiptState.StyleController = this.layoutControl4;
            this.leReceiptState.TabIndex = 4;
            // 
            // leAdjustmentState
            // 
            this.leAdjustmentState.Location = new System.Drawing.Point(580, 3);
            this.leAdjustmentState.Name = "leAdjustmentState";
            this.leAdjustmentState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leAdjustmentState.Properties.NullText = "선택";
            this.leAdjustmentState.Size = new System.Drawing.Size(111, 20);
            this.leAdjustmentState.StyleController = this.layoutControl4;
            this.leAdjustmentState.TabIndex = 4;
            // 
            // sbSearch
            // 
            this.sbSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSearch.ImageOptions.Image")));
            this.sbSearch.Location = new System.Drawing.Point(705, 2);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(96, 22);
            this.sbSearch.StyleController = this.layoutControl4;
            this.sbSearch.TabIndex = 5;
            this.sbSearch.Text = "검색";
            this.sbSearch.Click += new System.EventHandler(this.sbSearch_Click);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem10,
            this.layoutControlItem9,
            this.layoutControlItem17,
            this.layoutControlItem4,
            this.layoutControlItem15,
            this.emptySpaceItem5,
            this.emptySpaceItem1});
            this.layoutControlGroup3.Name = "Root";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1348, 26);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem8.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem8.Control = this.deDtFrom;
            this.layoutControlItem8.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem8.CustomizationFormText = "입고번호";
            this.layoutControlItem8.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem8.ImageOptions.Image")));
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(190, 0);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(190, 26);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "정산일";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(65, 20);
            this.layoutControlItem8.TextToControlDistance = 10;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.labelControl11;
            this.layoutControlItem10.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(190, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(13, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem9.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem9.Control = this.deDtTo;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "입고번호";
            this.layoutControlItem9.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem9.ImageOptions.Image")));
            this.layoutControlItem9.Location = new System.Drawing.Point(203, 0);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(110, 0);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(110, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "입고번호";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem17.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem17.Control = this.leReceiptState;
            this.layoutControlItem17.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem17.CustomizationFormText = "입고번호";
            this.layoutControlItem17.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem17.ImageOptions.Image")));
            this.layoutControlItem17.Location = new System.Drawing.Point(313, 0);
            this.layoutControlItem17.MaxSize = new System.Drawing.Size(190, 0);
            this.layoutControlItem17.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(190, 26);
            this.layoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem17.Text = "제품등급";
            this.layoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem17.TextSize = new System.Drawing.Size(65, 20);
            this.layoutControlItem17.TextToControlDistance = 10;
            this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem4.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem4.Control = this.leAdjustmentState;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "입고번호";
            this.layoutControlItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem4.ImageOptions.Image")));
            this.layoutControlItem4.Location = new System.Drawing.Point(503, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(190, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(190, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "정산상태";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(65, 20);
            this.layoutControlItem4.TextToControlDistance = 10;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.sbSearch;
            this.layoutControlItem15.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem15.Location = new System.Drawing.Point(703, 0);
            this.layoutControlItem15.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem15.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem15.Text = "layoutControlItem14";
            this.layoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(693, 0);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(10, 0);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(803, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(545, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.teProductCnt);
            this.layoutControl3.Controls.Add(this.teTotalPrice);
            this.layoutControl3.Controls.Add(this.teTax);
            this.layoutControl3.Controls.Add(this.tePurchasedPrice);
            this.layoutControl3.Controls.Add(this.teAdjustmentPrice);
            this.layoutControl3.Controls.Add(this.teProductPrice);
            this.layoutControl3.Controls.Add(this.rgAdjustmentState);
            this.layoutControl3.Location = new System.Drawing.Point(424, 33);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(927, 86);
            this.layoutControl3.TabIndex = 11;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // teProductCnt
            // 
            this.teProductCnt.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.teProductCnt.Location = new System.Drawing.Point(67, 46);
            this.teProductCnt.Name = "teProductCnt";
            this.teProductCnt.Properties.Appearance.Options.UseTextOptions = true;
            this.teProductCnt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.teProductCnt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teProductCnt.Properties.DisplayFormat.FormatString = "N0";
            this.teProductCnt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teProductCnt.Properties.EditFormat.FormatString = "N0";
            this.teProductCnt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teProductCnt.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.teProductCnt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.teProductCnt.Properties.ReadOnly = true;
            this.teProductCnt.Size = new System.Drawing.Size(81, 20);
            this.teProductCnt.StyleController = this.layoutControl3;
            this.teProductCnt.TabIndex = 4;
            // 
            // teTotalPrice
            // 
            this.teTotalPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.teTotalPrice.Location = new System.Drawing.Point(917, 46);
            this.teTotalPrice.Name = "teTotalPrice";
            this.teTotalPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.teTotalPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.teTotalPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teTotalPrice.Properties.DisplayFormat.FormatString = "N0";
            this.teTotalPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teTotalPrice.Properties.EditFormat.FormatString = "N0";
            this.teTotalPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teTotalPrice.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.teTotalPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.teTotalPrice.Properties.ReadOnly = true;
            this.teTotalPrice.Size = new System.Drawing.Size(101, 20);
            this.teTotalPrice.StyleController = this.layoutControl3;
            this.teTotalPrice.TabIndex = 9;
            // 
            // teTax
            // 
            this.teTax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.teTax.Location = new System.Drawing.Point(727, 46);
            this.teTax.Name = "teTax";
            this.teTax.Properties.Appearance.Options.UseTextOptions = true;
            this.teTax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.teTax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teTax.Properties.DisplayFormat.FormatString = "N0";
            this.teTax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teTax.Properties.EditFormat.FormatString = "N0";
            this.teTax.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teTax.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.teTax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.teTax.Properties.ReadOnly = true;
            this.teTax.Size = new System.Drawing.Size(101, 20);
            this.teTax.StyleController = this.layoutControl3;
            this.teTax.TabIndex = 8;
            // 
            // tePurchasedPrice
            // 
            this.tePurchasedPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tePurchasedPrice.Location = new System.Drawing.Point(557, 46);
            this.tePurchasedPrice.Name = "tePurchasedPrice";
            this.tePurchasedPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.tePurchasedPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tePurchasedPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tePurchasedPrice.Properties.DisplayFormat.FormatString = "N0";
            this.tePurchasedPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tePurchasedPrice.Properties.EditFormat.FormatString = "N0";
            this.tePurchasedPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tePurchasedPrice.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.tePurchasedPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.tePurchasedPrice.Properties.ReadOnly = true;
            this.tePurchasedPrice.Size = new System.Drawing.Size(101, 20);
            this.tePurchasedPrice.StyleController = this.layoutControl3;
            this.tePurchasedPrice.TabIndex = 7;
            // 
            // teAdjustmentPrice
            // 
            this.teAdjustmentPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.teAdjustmentPrice.Location = new System.Drawing.Point(387, 46);
            this.teAdjustmentPrice.Name = "teAdjustmentPrice";
            this.teAdjustmentPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.teAdjustmentPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.teAdjustmentPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teAdjustmentPrice.Properties.DisplayFormat.FormatString = "N0";
            this.teAdjustmentPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teAdjustmentPrice.Properties.EditFormat.FormatString = "N0";
            this.teAdjustmentPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teAdjustmentPrice.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.teAdjustmentPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.teAdjustmentPrice.Properties.ReadOnly = true;
            this.teAdjustmentPrice.Size = new System.Drawing.Size(101, 20);
            this.teAdjustmentPrice.StyleController = this.layoutControl3;
            this.teAdjustmentPrice.TabIndex = 6;
            // 
            // teProductPrice
            // 
            this.teProductPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.teProductPrice.Location = new System.Drawing.Point(217, 46);
            this.teProductPrice.Name = "teProductPrice";
            this.teProductPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.teProductPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.teProductPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teProductPrice.Properties.DisplayFormat.FormatString = "N0";
            this.teProductPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teProductPrice.Properties.EditFormat.FormatString = "N0";
            this.teProductPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.teProductPrice.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.teProductPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.teProductPrice.Properties.ReadOnly = true;
            this.teProductPrice.Size = new System.Drawing.Size(101, 20);
            this.teProductPrice.StyleController = this.layoutControl3;
            this.teProductPrice.TabIndex = 5;
            // 
            // rgAdjustmentState
            // 
            this.rgAdjustmentState.Location = new System.Drawing.Point(2, 21);
            this.rgAdjustmentState.Name = "rgAdjustmentState";
            this.rgAdjustmentState.Properties.Columns = 5;
            this.rgAdjustmentState.Size = new System.Drawing.Size(510, 21);
            this.rgAdjustmentState.StyleController = this.layoutControl3;
            this.rgAdjustmentState.TabIndex = 4;
            this.rgAdjustmentState.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rgAdjustmentState_EditValueChanging);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem16,
            this.emptySpaceItem4,
            this.layoutControlItem39,
            this.emptySpaceItem3});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1030, 69);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.teProductCnt;
            this.layoutControlItem6.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(150, 0);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(150, 25);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(150, 25);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "제품수";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 16);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.teProductPrice;
            this.layoutControlItem7.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem7.Location = new System.Drawing.Point(150, 44);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(200, 0);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(170, 1);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(170, 25);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "제품가";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 20);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.teAdjustmentPrice;
            this.layoutControlItem12.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem12.Location = new System.Drawing.Point(320, 44);
            this.layoutControlItem12.MaxSize = new System.Drawing.Size(200, 0);
            this.layoutControlItem12.MinSize = new System.Drawing.Size(170, 25);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(170, 25);
            this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem12.Text = "차감가";
            this.layoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(60, 20);
            this.layoutControlItem12.TextToControlDistance = 5;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.tePurchasedPrice;
            this.layoutControlItem13.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem13.Location = new System.Drawing.Point(490, 44);
            this.layoutControlItem13.MaxSize = new System.Drawing.Size(200, 0);
            this.layoutControlItem13.MinSize = new System.Drawing.Size(170, 25);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(170, 25);
            this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem13.Text = "매입가";
            this.layoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(60, 20);
            this.layoutControlItem13.TextToControlDistance = 5;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.teTax;
            this.layoutControlItem14.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem14.Location = new System.Drawing.Point(660, 44);
            this.layoutControlItem14.MaxSize = new System.Drawing.Size(200, 0);
            this.layoutControlItem14.MinSize = new System.Drawing.Size(170, 25);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(170, 25);
            this.layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem14.Text = "부가세";
            this.layoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(60, 20);
            this.layoutControlItem14.TextToControlDistance = 5;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.teTotalPrice;
            this.layoutControlItem16.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem16.Location = new System.Drawing.Point(830, 44);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(220, 0);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(190, 25);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(190, 25);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Text = "최종 매입가";
            this.layoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem16.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem16.TextToControlDistance = 5;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(1020, 44);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 25);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem39
            // 
            this.layoutControlItem39.Control = this.rgAdjustmentState;
            this.layoutControlItem39.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem39.CustomizationFormText = "진행정보";
            this.layoutControlItem39.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem39.ImageOptions.Image")));
            this.layoutControlItem39.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem39.Name = "layoutControlItem39";
            this.layoutControlItem39.Size = new System.Drawing.Size(514, 44);
            this.layoutControlItem39.Text = "진행정보";
            this.layoutControlItem39.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem39.TextSize = new System.Drawing.Size(61, 16);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(514, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(516, 44);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcAdjustmentList);
            this.layoutControl2.Location = new System.Drawing.Point(3, 33);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(405, 558);
            this.layoutControl2.TabIndex = 10;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcAdjustmentList
            // 
            this.gcAdjustmentList.Location = new System.Drawing.Point(3, 22);
            this.gcAdjustmentList.MainView = this.gvAdjustmentList;
            this.gcAdjustmentList.Name = "gcAdjustmentList";
            this.gcAdjustmentList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileAdjustmentState,
            this.rileCompanyId});
            this.gcAdjustmentList.Size = new System.Drawing.Size(399, 533);
            this.gcAdjustmentList.TabIndex = 7;
            this.gcAdjustmentList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAdjustmentList});
            // 
            // gvAdjustmentList
            // 
            this.gvAdjustmentList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn25,
            this.gridColumn24,
            this.gridColumn23,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn1,
            this.gridColumn2});
            this.gvAdjustmentList.GridControl = this.gcAdjustmentList;
            this.gvAdjustmentList.Name = "gvAdjustmentList";
            this.gvAdjustmentList.OptionsView.ShowAutoFilterRow = true;
            this.gvAdjustmentList.OptionsView.ShowFooter = true;
            this.gvAdjustmentList.OptionsView.ShowGroupPanel = false;
            this.gvAdjustmentList.OptionsView.ShowIndicator = false;
            this.gvAdjustmentList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvAdjustmentList_FocusedRowChanged);
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "ADJUSTMENT_ID";
            this.gridColumn25.FieldName = "ADJUSTMENT_ID";
            this.gridColumn25.Name = "gridColumn25";
            // 
            // gridColumn24
            // 
            this.gridColumn24.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn24.Caption = "정산번호";
            this.gridColumn24.FieldName = "ADJUSTMENT";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.OptionsColumn.ReadOnly = true;
            this.gridColumn24.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ADJUSTMENT", "{0:n0}")});
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 0;
            this.gridColumn24.Width = 64;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.Caption = "정산생성일";
            this.gridColumn23.ColumnEdit = this.rideCreateDt;
            this.gridColumn23.FieldName = "CREATE_DT";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.OptionsColumn.ReadOnly = true;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 1;
            this.gridColumn23.Width = 62;
            // 
            // gridColumn21
            // 
            this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn21.Caption = "거래업체";
            this.gridColumn21.ColumnEdit = this.rileCompanyId;
            this.gridColumn21.FieldName = "COMPANY_ID";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.ReadOnly = true;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 2;
            this.gridColumn21.Width = 56;
            // 
            // rileCompanyId
            // 
            this.rileCompanyId.AutoHeight = false;
            this.rileCompanyId.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileCompanyId.Name = "rileCompanyId";
            this.rileCompanyId.NullText = "";
            // 
            // gridColumn22
            // 
            this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn22.Caption = "정산상태";
            this.gridColumn22.ColumnEdit = this.rileAdjustmentState;
            this.gridColumn22.FieldName = "ADJUSTMENT_STATE";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.OptionsColumn.ReadOnly = true;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 3;
            this.gridColumn22.Width = 48;
            // 
            // rileAdjustmentState
            // 
            this.rileAdjustmentState.AutoHeight = false;
            this.rileAdjustmentState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileAdjustmentState.Name = "rileAdjustmentState";
            this.rileAdjustmentState.NullText = "";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "제품수";
            this.gridColumn1.DisplayFormat.FormatString = "N0";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "PRODUCT_CNT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PRODUCT_CNT", "{0:N0}")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 52;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "정산금액";
            this.gridColumn2.DisplayFormat.FormatString = "N0";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "TOTAL_PRICE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_PRICE", "{0:N0}")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
            this.gridColumn2.Width = 59;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlGroup1.CaptionImageOptions.Image")));
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(405, 558);
            this.layoutControlGroup1.Text = "입고리스트";
            this.layoutControlGroup1.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.layoutControlGroup1_CustomButtonClick);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcAdjustmentList;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(403, 537);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // gcList
            // 
            this.gcList.Location = new System.Drawing.Point(426, 144);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileUserId,
            this.rileCategory,
            this.rileNickName,
            this.riicbCheckYn,
            this.riicbHinge,
            this.rileProductGrade,
            this.rileComapnyId2,
            this.rileManufactureType,
            this.rileHinge,
            this.riicbeCnt});
            this.gcList.Size = new System.Drawing.Size(923, 445);
            this.gcList.TabIndex = 8;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcInventoryId,
            this.gcCheck,
            this.gcApprovalUser,
            this.gcCreateDt,
            this.gcCompanyId,
            this.gcWarehousing,
            this.gcBarcode,
            this.gcInitPrice,
            this.gcAdjust,
            this.gridColumn10,
            this.gridColumn9,
            this.gridColumn8,
            this.gridColumn7,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn4,
            this.gridColumn3,
            this.gcManufactureType,
            this.gcMBDManufactureNm,
            this.gcMBDModelNm1,
            this.gcMBDModelNm2,
            this.gcCpu,
            this.gcCategory,
            this.gcGeneration,
            this.gcMonSize,
            this.gcCheckYn,
            this.gcProductGrade,
            this.gcDestroyedCase,
            this.gcScracth,
            this.gcStabbed,
            this.gcPressed,
            this.gcDisColored,
            this.gcHinge,
            this.gcCaseDes,
            this.gcDisplay,
            this.gcUsb,
            this.gcMousePad,
            this.gcKeyboard,
            this.gcBattery,
            this.gcCam,
            this.gcWirelessLan,
            this.gcWiredLan,
            this.gcODD,
            this.gcHDD,
            this.gcBios,
            this.gcOS,
            this.gcTestCheck,
            this.gcSTATE});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvList.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save;
            this.gvList.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Direct;
            this.gvList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvList.OptionsView.ShowAutoFilterRow = true;
            this.gvList.OptionsView.ShowFooter = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvList_RowCellStyle);
            this.gvList.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvList_FocusedRowObjectChanged);
            this.gvList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvList_CellValueChanged);
            // 
            // gcInventoryId
            // 
            this.gcInventoryId.Caption = "INVENTORY_ID";
            this.gcInventoryId.FieldName = "INVENTORY_ID";
            this.gcInventoryId.Name = "gcInventoryId";
            // 
            // gcCheck
            // 
            this.gcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.Caption = "체크";
            this.gcCheck.FieldName = "CHECK";
            this.gcCheck.MaxWidth = 40;
            this.gcCheck.MinWidth = 40;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.Width = 40;
            // 
            // gcApprovalUser
            // 
            this.gcApprovalUser.AppearanceHeader.Options.UseTextOptions = true;
            this.gcApprovalUser.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcApprovalUser.Caption = "승인자";
            this.gcApprovalUser.ColumnEdit = this.rileUserId;
            this.gcApprovalUser.FieldName = "APPROVAL_USER_ID";
            this.gcApprovalUser.MaxWidth = 60;
            this.gcApprovalUser.MinWidth = 60;
            this.gcApprovalUser.Name = "gcApprovalUser";
            this.gcApprovalUser.OptionsColumn.AllowEdit = false;
            this.gcApprovalUser.OptionsColumn.ReadOnly = true;
            this.gcApprovalUser.Width = 60;
            // 
            // rileUserId
            // 
            this.rileUserId.AutoHeight = false;
            this.rileUserId.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileUserId.Name = "rileUserId";
            this.rileUserId.NullText = "";
            // 
            // gcCreateDt
            // 
            this.gcCreateDt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCreateDt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCreateDt.Caption = "검수일";
            this.gcCreateDt.FieldName = "CREATE_DT";
            this.gcCreateDt.MaxWidth = 200;
            this.gcCreateDt.MinWidth = 80;
            this.gcCreateDt.Name = "gcCreateDt";
            this.gcCreateDt.OptionsColumn.AllowEdit = false;
            this.gcCreateDt.OptionsColumn.ReadOnly = true;
            this.gcCreateDt.Visible = true;
            this.gcCreateDt.VisibleIndex = 0;
            this.gcCreateDt.Width = 80;
            // 
            // gcCompanyId
            // 
            this.gcCompanyId.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCompanyId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCompanyId.Caption = "업체명";
            this.gcCompanyId.ColumnEdit = this.rileComapnyId2;
            this.gcCompanyId.FieldName = "COMPANY_ID";
            this.gcCompanyId.MaxWidth = 200;
            this.gcCompanyId.MinWidth = 80;
            this.gcCompanyId.Name = "gcCompanyId";
            this.gcCompanyId.OptionsColumn.AllowEdit = false;
            this.gcCompanyId.OptionsColumn.ReadOnly = true;
            this.gcCompanyId.Visible = true;
            this.gcCompanyId.VisibleIndex = 1;
            this.gcCompanyId.Width = 80;
            // 
            // rileComapnyId2
            // 
            this.rileComapnyId2.AutoHeight = false;
            this.rileComapnyId2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileComapnyId2.Name = "rileComapnyId2";
            this.rileComapnyId2.NullText = "";
            // 
            // gcWarehousing
            // 
            this.gcWarehousing.AppearanceHeader.Options.UseTextOptions = true;
            this.gcWarehousing.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcWarehousing.Caption = "입고번호";
            this.gcWarehousing.FieldName = "WAREHOUSING";
            this.gcWarehousing.MaxWidth = 200;
            this.gcWarehousing.MinWidth = 80;
            this.gcWarehousing.Name = "gcWarehousing";
            this.gcWarehousing.OptionsColumn.ReadOnly = true;
            this.gcWarehousing.Visible = true;
            this.gcWarehousing.VisibleIndex = 2;
            this.gcWarehousing.Width = 80;
            // 
            // gcBarcode
            // 
            this.gcBarcode.AppearanceHeader.Options.UseTextOptions = true;
            this.gcBarcode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcBarcode.Caption = "재고번호";
            this.gcBarcode.FieldName = "BARCODE";
            this.gcBarcode.MaxWidth = 200;
            this.gcBarcode.MinWidth = 100;
            this.gcBarcode.Name = "gcBarcode";
            this.gcBarcode.OptionsColumn.ReadOnly = true;
            this.gcBarcode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BARCODE", "{0}")});
            this.gcBarcode.Visible = true;
            this.gcBarcode.VisibleIndex = 3;
            this.gcBarcode.Width = 100;
            // 
            // gcInitPrice
            // 
            this.gcInitPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcInitPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcInitPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcInitPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcInitPrice.Caption = "제품가";
            this.gcInitPrice.DisplayFormat.FormatString = "N0";
            this.gcInitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcInitPrice.FieldName = "PRODUCT_PRICE";
            this.gcInitPrice.MaxWidth = 150;
            this.gcInitPrice.MinWidth = 100;
            this.gcInitPrice.Name = "gcInitPrice";
            this.gcInitPrice.OptionsColumn.AllowEdit = false;
            this.gcInitPrice.OptionsColumn.ReadOnly = true;
            this.gcInitPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PRODUCT_PRICE", "{0:N0}")});
            this.gcInitPrice.Visible = true;
            this.gcInitPrice.VisibleIndex = 4;
            this.gcInitPrice.Width = 150;
            // 
            // gcAdjust
            // 
            this.gcAdjust.AppearanceCell.Options.UseTextOptions = true;
            this.gcAdjust.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcAdjust.AppearanceHeader.Options.UseTextOptions = true;
            this.gcAdjust.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcAdjust.Caption = "차감가";
            this.gcAdjust.DisplayFormat.FormatString = "N0";
            this.gcAdjust.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcAdjust.FieldName = "PRODUCT_ADJUST_PRICE";
            this.gcAdjust.MaxWidth = 100;
            this.gcAdjust.MinWidth = 100;
            this.gcAdjust.Name = "gcAdjust";
            this.gcAdjust.OptionsColumn.AllowEdit = false;
            this.gcAdjust.OptionsColumn.ReadOnly = true;
            this.gcAdjust.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PRODUCT_ADJUST_PRICE", "{0:N0}")});
            this.gcAdjust.Visible = true;
            this.gcAdjust.VisibleIndex = 5;
            this.gcAdjust.Width = 100;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "패스워드";
            this.gridColumn10.ColumnEdit = this.riicbeCnt;
            this.gridColumn10.FieldName = "PASSWORD_CNT";
            this.gridColumn10.MaxWidth = 60;
            this.gridColumn10.MinWidth = 60;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 6;
            this.gridColumn10.Width = 60;
            // 
            // riicbeCnt
            // 
            this.riicbeCnt.AutoHeight = false;
            this.riicbeCnt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicbeCnt.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.riicbeCnt.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riicbeCnt.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0)});
            this.riicbeCnt.LargeImages = this.icAdjust;
            this.riicbeCnt.Name = "riicbeCnt";
            this.riicbeCnt.SmallImages = this.icAdjust;
            // 
            // icAdjust
            // 
            this.icAdjust.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icAdjust.ImageStream")));
            this.icAdjust.InsertImage(global::WareHousingMaster.Properties.Resources.apply_16x1610, "apply_16x1610", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.icAdjust.Images.SetKeyName(0, "apply_16x1610");
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "액정";
            this.gridColumn9.ColumnEdit = this.riicbeCnt;
            this.gridColumn9.FieldName = "DISPLAY_CNT";
            this.gridColumn9.MaxWidth = 60;
            this.gridColumn9.MinWidth = 60;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 7;
            this.gridColumn9.Width = 60;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "키보드";
            this.gridColumn8.ColumnEdit = this.riicbeCnt;
            this.gridColumn8.FieldName = "KEYBOARD_CNT";
            this.gridColumn8.MaxWidth = 60;
            this.gridColumn8.MinWidth = 60;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 60;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "USB";
            this.gridColumn7.ColumnEdit = this.riicbeCnt;
            this.gridColumn7.FieldName = "USB_CNT";
            this.gridColumn7.MaxWidth = 60;
            this.gridColumn7.MinWidth = 60;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 9;
            this.gridColumn7.Width = 60;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "케이스";
            this.gridColumn6.ColumnEdit = this.riicbeCnt;
            this.gridColumn6.FieldName = "CASE_CNT";
            this.gridColumn6.MaxWidth = 60;
            this.gridColumn6.MinWidth = 60;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 10;
            this.gridColumn6.Width = 60;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "무선랜";
            this.gridColumn5.ColumnEdit = this.riicbeCnt;
            this.gridColumn5.FieldName = "WIRELESS_CNT";
            this.gridColumn5.MaxWidth = 60;
            this.gridColumn5.MinWidth = 60;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 11;
            this.gridColumn5.Width = 60;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "배터리";
            this.gridColumn4.ColumnEdit = this.riicbeCnt;
            this.gridColumn4.FieldName = "BATTERY_CNT";
            this.gridColumn4.MaxWidth = 60;
            this.gridColumn4.MinWidth = 60;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 12;
            this.gridColumn4.Width = 60;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "기타";
            this.gridColumn3.ColumnEdit = this.riicbeCnt;
            this.gridColumn3.FieldName = "ETC_CNT";
            this.gridColumn3.MaxWidth = 60;
            this.gridColumn3.MinWidth = 60;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 13;
            this.gridColumn3.Width = 60;
            // 
            // gcManufactureType
            // 
            this.gcManufactureType.AppearanceCell.Options.UseTextOptions = true;
            this.gcManufactureType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcManufactureType.AppearanceHeader.Options.UseTextOptions = true;
            this.gcManufactureType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcManufactureType.Caption = "제조사구분";
            this.gcManufactureType.ColumnEdit = this.rileManufactureType;
            this.gcManufactureType.FieldName = "MANUFACTURE_TYPE";
            this.gcManufactureType.MaxWidth = 60;
            this.gcManufactureType.MinWidth = 60;
            this.gcManufactureType.Name = "gcManufactureType";
            this.gcManufactureType.OptionsColumn.AllowEdit = false;
            this.gcManufactureType.OptionsColumn.ReadOnly = true;
            this.gcManufactureType.Visible = true;
            this.gcManufactureType.VisibleIndex = 14;
            this.gcManufactureType.Width = 60;
            // 
            // rileManufactureType
            // 
            this.rileManufactureType.AutoHeight = false;
            this.rileManufactureType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileManufactureType.DropDownRows = 3;
            this.rileManufactureType.Name = "rileManufactureType";
            this.rileManufactureType.NullText = "";
            // 
            // gcMBDManufactureNm
            // 
            this.gcMBDManufactureNm.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMBDManufactureNm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMBDManufactureNm.Caption = "MBD제조사";
            this.gcMBDManufactureNm.FieldName = "MANUFACTURE_NM";
            this.gcMBDManufactureNm.MaxWidth = 200;
            this.gcMBDManufactureNm.MinWidth = 80;
            this.gcMBDManufactureNm.Name = "gcMBDManufactureNm";
            this.gcMBDManufactureNm.OptionsColumn.ReadOnly = true;
            this.gcMBDManufactureNm.Visible = true;
            this.gcMBDManufactureNm.VisibleIndex = 15;
            this.gcMBDManufactureNm.Width = 80;
            // 
            // gcMBDModelNm1
            // 
            this.gcMBDModelNm1.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMBDModelNm1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMBDModelNm1.Caption = "MBD모델명1";
            this.gcMBDModelNm1.FieldName = "MBD_MODEL_NM";
            this.gcMBDModelNm1.MaxWidth = 200;
            this.gcMBDModelNm1.MinWidth = 100;
            this.gcMBDModelNm1.Name = "gcMBDModelNm1";
            this.gcMBDModelNm1.OptionsColumn.ReadOnly = true;
            this.gcMBDModelNm1.Visible = true;
            this.gcMBDModelNm1.VisibleIndex = 16;
            this.gcMBDModelNm1.Width = 100;
            // 
            // gcMBDModelNm2
            // 
            this.gcMBDModelNm2.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMBDModelNm2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMBDModelNm2.Caption = "MBD모델명2";
            this.gcMBDModelNm2.FieldName = "PRODUCT_NAME";
            this.gcMBDModelNm2.MaxWidth = 200;
            this.gcMBDModelNm2.MinWidth = 100;
            this.gcMBDModelNm2.Name = "gcMBDModelNm2";
            this.gcMBDModelNm2.OptionsColumn.ReadOnly = true;
            this.gcMBDModelNm2.Visible = true;
            this.gcMBDModelNm2.VisibleIndex = 17;
            this.gcMBDModelNm2.Width = 100;
            // 
            // gcCpu
            // 
            this.gcCpu.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCpu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCpu.Caption = "CPU정보";
            this.gcCpu.FieldName = "CPU_MODEL_NM";
            this.gcCpu.MaxWidth = 200;
            this.gcCpu.MinWidth = 130;
            this.gcCpu.Name = "gcCpu";
            this.gcCpu.OptionsColumn.ReadOnly = true;
            this.gcCpu.Visible = true;
            this.gcCpu.VisibleIndex = 18;
            this.gcCpu.Width = 130;
            // 
            // gcCategory
            // 
            this.gcCategory.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCategory.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCategory.Caption = "구분";
            this.gcCategory.ColumnEdit = this.rileCategory;
            this.gcCategory.FieldName = "CATEGORY";
            this.gcCategory.MaxWidth = 200;
            this.gcCategory.MinWidth = 80;
            this.gcCategory.Name = "gcCategory";
            this.gcCategory.Width = 60;
            // 
            // rileCategory
            // 
            this.rileCategory.AutoHeight = false;
            this.rileCategory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileCategory.Name = "rileCategory";
            this.rileCategory.NullText = "";
            // 
            // gcGeneration
            // 
            this.gcGeneration.AppearanceHeader.Options.UseTextOptions = true;
            this.gcGeneration.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcGeneration.Caption = "상세스펙";
            this.gcGeneration.ColumnEdit = this.rileNickName;
            this.gcGeneration.FieldName = "NTB_LIST_ID";
            this.gcGeneration.MaxWidth = 200;
            this.gcGeneration.MinWidth = 80;
            this.gcGeneration.Name = "gcGeneration";
            this.gcGeneration.OptionsColumn.AllowEdit = false;
            this.gcGeneration.OptionsColumn.ReadOnly = true;
            this.gcGeneration.Visible = true;
            this.gcGeneration.VisibleIndex = 19;
            this.gcGeneration.Width = 80;
            // 
            // rileNickName
            // 
            this.rileNickName.AutoHeight = false;
            this.rileNickName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileNickName.DropDownRows = 10;
            this.rileNickName.Name = "rileNickName";
            this.rileNickName.NullText = "";
            // 
            // gcMonSize
            // 
            this.gcMonSize.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMonSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMonSize.Caption = "MON사이즈";
            this.gcMonSize.FieldName = "MON_SIZE";
            this.gcMonSize.MaxWidth = 200;
            this.gcMonSize.MinWidth = 80;
            this.gcMonSize.Name = "gcMonSize";
            this.gcMonSize.OptionsColumn.ReadOnly = true;
            this.gcMonSize.Width = 80;
            // 
            // gcCheckYn
            // 
            this.gcCheckYn.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheckYn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheckYn.Caption = "검수여부";
            this.gcCheckYn.ColumnEdit = this.riicbCheckYn;
            this.gcCheckYn.FieldName = "CHECK_YN";
            this.gcCheckYn.MaxWidth = 60;
            this.gcCheckYn.MinWidth = 60;
            this.gcCheckYn.Name = "gcCheckYn";
            this.gcCheckYn.OptionsColumn.AllowEdit = false;
            this.gcCheckYn.OptionsColumn.ReadOnly = true;
            this.gcCheckYn.Width = 60;
            // 
            // riicbCheckYn
            // 
            this.riicbCheckYn.AutoHeight = false;
            this.riicbCheckYn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicbCheckYn.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riicbCheckYn.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("X", false, 0)});
            this.riicbCheckYn.LargeImages = this.icCheckYn;
            this.riicbCheckYn.Name = "riicbCheckYn";
            this.riicbCheckYn.SmallImages = this.icCheckYn;
            // 
            // icCheckYn
            // 
            this.icCheckYn.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icCheckYn.ImageStream")));
            this.icCheckYn.InsertImage(global::WareHousingMaster.Properties.Resources.cancel_16x164, "cancel_16x164", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.icCheckYn.Images.SetKeyName(0, "cancel_16x164");
            // 
            // gcProductGrade
            // 
            this.gcProductGrade.AppearanceCell.Options.UseTextOptions = true;
            this.gcProductGrade.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcProductGrade.AppearanceHeader.Options.UseTextOptions = true;
            this.gcProductGrade.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcProductGrade.Caption = "등급";
            this.gcProductGrade.ColumnEdit = this.rileProductGrade;
            this.gcProductGrade.FieldName = "PRODUCT_GRADE";
            this.gcProductGrade.MaxWidth = 60;
            this.gcProductGrade.MinWidth = 60;
            this.gcProductGrade.Name = "gcProductGrade";
            this.gcProductGrade.Visible = true;
            this.gcProductGrade.VisibleIndex = 20;
            this.gcProductGrade.Width = 60;
            // 
            // rileProductGrade
            // 
            this.rileProductGrade.AutoHeight = false;
            this.rileProductGrade.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileProductGrade.Name = "rileProductGrade";
            this.rileProductGrade.NullText = "";
            this.rileProductGrade.ReadOnly = true;
            // 
            // gcDestroyedCase
            // 
            this.gcDestroyedCase.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDestroyedCase.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDestroyedCase.Caption = "파손(case)";
            this.gcDestroyedCase.FieldName = "CASE_DESTROYED";
            this.gcDestroyedCase.MaxWidth = 200;
            this.gcDestroyedCase.MinWidth = 100;
            this.gcDestroyedCase.Name = "gcDestroyedCase";
            this.gcDestroyedCase.OptionsColumn.AllowEdit = false;
            this.gcDestroyedCase.OptionsColumn.ReadOnly = true;
            this.gcDestroyedCase.Visible = true;
            this.gcDestroyedCase.VisibleIndex = 21;
            this.gcDestroyedCase.Width = 100;
            // 
            // gcScracth
            // 
            this.gcScracth.AppearanceHeader.Options.UseTextOptions = true;
            this.gcScracth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcScracth.Caption = "스크래치(case)";
            this.gcScracth.FieldName = "CASE_SCRATCH";
            this.gcScracth.MaxWidth = 200;
            this.gcScracth.MinWidth = 100;
            this.gcScracth.Name = "gcScracth";
            this.gcScracth.OptionsColumn.AllowEdit = false;
            this.gcScracth.OptionsColumn.ReadOnly = true;
            this.gcScracth.Visible = true;
            this.gcScracth.VisibleIndex = 22;
            this.gcScracth.Width = 100;
            // 
            // gcStabbed
            // 
            this.gcStabbed.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStabbed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStabbed.Caption = "찍힘(case)";
            this.gcStabbed.FieldName = "CASE_STABBED";
            this.gcStabbed.MaxWidth = 200;
            this.gcStabbed.MinWidth = 100;
            this.gcStabbed.Name = "gcStabbed";
            this.gcStabbed.OptionsColumn.AllowEdit = false;
            this.gcStabbed.OptionsColumn.ReadOnly = true;
            this.gcStabbed.Visible = true;
            this.gcStabbed.VisibleIndex = 23;
            this.gcStabbed.Width = 100;
            // 
            // gcPressed
            // 
            this.gcPressed.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPressed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPressed.Caption = "눌림(case)";
            this.gcPressed.FieldName = "CASE_PRESSED";
            this.gcPressed.MaxWidth = 200;
            this.gcPressed.MinWidth = 100;
            this.gcPressed.Name = "gcPressed";
            this.gcPressed.OptionsColumn.AllowEdit = false;
            this.gcPressed.OptionsColumn.ReadOnly = true;
            this.gcPressed.Visible = true;
            this.gcPressed.VisibleIndex = 24;
            this.gcPressed.Width = 100;
            // 
            // gcDisColored
            // 
            this.gcDisColored.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDisColored.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDisColored.Caption = "변색(case)";
            this.gcDisColored.FieldName = "CASE_DISCOLORED";
            this.gcDisColored.MaxWidth = 200;
            this.gcDisColored.MinWidth = 100;
            this.gcDisColored.Name = "gcDisColored";
            this.gcDisColored.OptionsColumn.AllowEdit = false;
            this.gcDisColored.OptionsColumn.ReadOnly = true;
            this.gcDisColored.Visible = true;
            this.gcDisColored.VisibleIndex = 25;
            this.gcDisColored.Width = 100;
            // 
            // gcHinge
            // 
            this.gcHinge.AppearanceHeader.Options.UseTextOptions = true;
            this.gcHinge.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcHinge.Caption = "힌지파손";
            this.gcHinge.ColumnEdit = this.rileHinge;
            this.gcHinge.FieldName = "CASE_HINGE";
            this.gcHinge.MaxWidth = 60;
            this.gcHinge.MinWidth = 60;
            this.gcHinge.Name = "gcHinge";
            this.gcHinge.OptionsColumn.AllowEdit = false;
            this.gcHinge.OptionsColumn.ReadOnly = true;
            this.gcHinge.Visible = true;
            this.gcHinge.VisibleIndex = 26;
            this.gcHinge.Width = 60;
            // 
            // rileHinge
            // 
            this.rileHinge.AutoHeight = false;
            this.rileHinge.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileHinge.Name = "rileHinge";
            this.rileHinge.NullText = "";
            // 
            // gcCaseDes
            // 
            this.gcCaseDes.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCaseDes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCaseDes.Caption = "파손부위";
            this.gcCaseDes.FieldName = "CASE_DES";
            this.gcCaseDes.MaxWidth = 200;
            this.gcCaseDes.MinWidth = 100;
            this.gcCaseDes.Name = "gcCaseDes";
            this.gcCaseDes.OptionsColumn.AllowEdit = false;
            this.gcCaseDes.OptionsColumn.ReadOnly = true;
            this.gcCaseDes.Visible = true;
            this.gcCaseDes.VisibleIndex = 27;
            this.gcCaseDes.Width = 100;
            // 
            // gcDisplay
            // 
            this.gcDisplay.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDisplay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDisplay.Caption = "액정";
            this.gcDisplay.FieldName = "DISPLAY";
            this.gcDisplay.MaxWidth = 200;
            this.gcDisplay.MinWidth = 100;
            this.gcDisplay.Name = "gcDisplay";
            this.gcDisplay.OptionsColumn.AllowEdit = false;
            this.gcDisplay.OptionsColumn.ReadOnly = true;
            this.gcDisplay.Visible = true;
            this.gcDisplay.VisibleIndex = 28;
            this.gcDisplay.Width = 100;
            // 
            // gcUsb
            // 
            this.gcUsb.AppearanceHeader.Options.UseTextOptions = true;
            this.gcUsb.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcUsb.Caption = "USB";
            this.gcUsb.FieldName = "USB";
            this.gcUsb.MaxWidth = 200;
            this.gcUsb.MinWidth = 100;
            this.gcUsb.Name = "gcUsb";
            this.gcUsb.OptionsColumn.AllowEdit = false;
            this.gcUsb.OptionsColumn.ReadOnly = true;
            this.gcUsb.Visible = true;
            this.gcUsb.VisibleIndex = 29;
            this.gcUsb.Width = 100;
            // 
            // gcMousePad
            // 
            this.gcMousePad.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMousePad.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMousePad.Caption = "마우스패드";
            this.gcMousePad.FieldName = "MOUSEPAD";
            this.gcMousePad.MaxWidth = 200;
            this.gcMousePad.MinWidth = 100;
            this.gcMousePad.Name = "gcMousePad";
            this.gcMousePad.OptionsColumn.AllowEdit = false;
            this.gcMousePad.OptionsColumn.ReadOnly = true;
            this.gcMousePad.Visible = true;
            this.gcMousePad.VisibleIndex = 30;
            this.gcMousePad.Width = 100;
            // 
            // gcKeyboard
            // 
            this.gcKeyboard.AppearanceHeader.Options.UseTextOptions = true;
            this.gcKeyboard.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcKeyboard.Caption = "키보드";
            this.gcKeyboard.FieldName = "KEYBOARD";
            this.gcKeyboard.MaxWidth = 200;
            this.gcKeyboard.MinWidth = 100;
            this.gcKeyboard.Name = "gcKeyboard";
            this.gcKeyboard.OptionsColumn.AllowEdit = false;
            this.gcKeyboard.OptionsColumn.ReadOnly = true;
            this.gcKeyboard.Visible = true;
            this.gcKeyboard.VisibleIndex = 31;
            this.gcKeyboard.Width = 100;
            // 
            // gcBattery
            // 
            this.gcBattery.AppearanceHeader.Options.UseTextOptions = true;
            this.gcBattery.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcBattery.Caption = "배터리";
            this.gcBattery.FieldName = "BATTERY";
            this.gcBattery.MaxWidth = 200;
            this.gcBattery.MinWidth = 100;
            this.gcBattery.Name = "gcBattery";
            this.gcBattery.OptionsColumn.AllowEdit = false;
            this.gcBattery.OptionsColumn.ReadOnly = true;
            this.gcBattery.Visible = true;
            this.gcBattery.VisibleIndex = 32;
            this.gcBattery.Width = 100;
            // 
            // gcCam
            // 
            this.gcCam.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCam.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCam.Caption = "CAM";
            this.gcCam.FieldName = "CAM";
            this.gcCam.MaxWidth = 200;
            this.gcCam.MinWidth = 100;
            this.gcCam.Name = "gcCam";
            this.gcCam.OptionsColumn.AllowEdit = false;
            this.gcCam.OptionsColumn.ReadOnly = true;
            this.gcCam.Visible = true;
            this.gcCam.VisibleIndex = 33;
            this.gcCam.Width = 100;
            // 
            // gcWirelessLan
            // 
            this.gcWirelessLan.AppearanceHeader.Options.UseTextOptions = true;
            this.gcWirelessLan.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcWirelessLan.Caption = "무선LAN";
            this.gcWirelessLan.FieldName = "LAN_WIRELESS";
            this.gcWirelessLan.MaxWidth = 200;
            this.gcWirelessLan.MinWidth = 100;
            this.gcWirelessLan.Name = "gcWirelessLan";
            this.gcWirelessLan.OptionsColumn.AllowEdit = false;
            this.gcWirelessLan.OptionsColumn.ReadOnly = true;
            this.gcWirelessLan.Visible = true;
            this.gcWirelessLan.VisibleIndex = 34;
            this.gcWirelessLan.Width = 100;
            // 
            // gcWiredLan
            // 
            this.gcWiredLan.AppearanceHeader.Options.UseTextOptions = true;
            this.gcWiredLan.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcWiredLan.Caption = "유선LAN";
            this.gcWiredLan.FieldName = "LAN_WIRED";
            this.gcWiredLan.MaxWidth = 200;
            this.gcWiredLan.MinWidth = 100;
            this.gcWiredLan.Name = "gcWiredLan";
            this.gcWiredLan.OptionsColumn.AllowEdit = false;
            this.gcWiredLan.OptionsColumn.ReadOnly = true;
            this.gcWiredLan.Visible = true;
            this.gcWiredLan.VisibleIndex = 35;
            this.gcWiredLan.Width = 100;
            // 
            // gcODD
            // 
            this.gcODD.AppearanceHeader.Options.UseTextOptions = true;
            this.gcODD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcODD.Caption = "ODD";
            this.gcODD.FieldName = "ODD";
            this.gcODD.MaxWidth = 200;
            this.gcODD.MinWidth = 100;
            this.gcODD.Name = "gcODD";
            this.gcODD.OptionsColumn.AllowEdit = false;
            this.gcODD.OptionsColumn.ReadOnly = true;
            this.gcODD.Visible = true;
            this.gcODD.VisibleIndex = 36;
            this.gcODD.Width = 100;
            // 
            // gcHDD
            // 
            this.gcHDD.AppearanceHeader.Options.UseTextOptions = true;
            this.gcHDD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcHDD.Caption = "HDD";
            this.gcHDD.FieldName = "HDD";
            this.gcHDD.MaxWidth = 200;
            this.gcHDD.MinWidth = 100;
            this.gcHDD.Name = "gcHDD";
            this.gcHDD.OptionsColumn.AllowEdit = false;
            this.gcHDD.OptionsColumn.ReadOnly = true;
            this.gcHDD.Visible = true;
            this.gcHDD.VisibleIndex = 37;
            this.gcHDD.Width = 100;
            // 
            // gcBios
            // 
            this.gcBios.AppearanceHeader.Options.UseTextOptions = true;
            this.gcBios.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcBios.Caption = "BIOS";
            this.gcBios.FieldName = "BIOS";
            this.gcBios.MaxWidth = 200;
            this.gcBios.MinWidth = 100;
            this.gcBios.Name = "gcBios";
            this.gcBios.OptionsColumn.AllowEdit = false;
            this.gcBios.OptionsColumn.ReadOnly = true;
            this.gcBios.Visible = true;
            this.gcBios.VisibleIndex = 38;
            this.gcBios.Width = 100;
            // 
            // gcOS
            // 
            this.gcOS.AppearanceHeader.Options.UseTextOptions = true;
            this.gcOS.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcOS.Caption = "OS";
            this.gcOS.FieldName = "OS";
            this.gcOS.MaxWidth = 200;
            this.gcOS.MinWidth = 100;
            this.gcOS.Name = "gcOS";
            this.gcOS.OptionsColumn.AllowEdit = false;
            this.gcOS.OptionsColumn.ReadOnly = true;
            this.gcOS.Visible = true;
            this.gcOS.VisibleIndex = 39;
            this.gcOS.Width = 100;
            // 
            // gcTestCheck
            // 
            this.gcTestCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcTestCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcTestCheck.Caption = "검수불가";
            this.gcTestCheck.FieldName = "TEST_CHECK";
            this.gcTestCheck.MaxWidth = 200;
            this.gcTestCheck.MinWidth = 100;
            this.gcTestCheck.Name = "gcTestCheck";
            this.gcTestCheck.OptionsColumn.AllowEdit = false;
            this.gcTestCheck.OptionsColumn.ReadOnly = true;
            this.gcTestCheck.Visible = true;
            this.gcTestCheck.VisibleIndex = 40;
            this.gcTestCheck.Width = 100;
            // 
            // gcSTATE
            // 
            this.gcSTATE.AppearanceCell.Options.UseTextOptions = true;
            this.gcSTATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcSTATE.AppearanceHeader.Options.UseTextOptions = true;
            this.gcSTATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcSTATE.Caption = "STATE";
            this.gcSTATE.FieldName = "STATE";
            this.gcSTATE.MaxWidth = 60;
            this.gcSTATE.MinWidth = 60;
            this.gcSTATE.Name = "gcSTATE";
            this.gcSTATE.Width = 60;
            // 
            // riicbHinge
            // 
            this.riicbHinge.AutoHeight = false;
            this.riicbHinge.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riicbHinge.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("힌지파손", 1, 0)});
            this.riicbHinge.LargeImages = this.icHinge;
            this.riicbHinge.Name = "riicbHinge";
            this.riicbHinge.SmallImages = this.icHinge;
            // 
            // icHinge
            // 
            this.icHinge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icHinge.ImageStream")));
            this.icHinge.InsertImage(global::WareHousingMaster.Properties.Resources.ide_16x161, "ide_16x161", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.icHinge.Images.SetKeyName(0, "ide_16x161");
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgBarcodeList,
            this.layoutControlItem2,
            this.splitterItem1,
            this.layoutControlItem5,
            this.layoutControlItem18});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(1354, 594);
            this.Root.TextVisible = false;
            // 
            // lcgBarcodeList
            // 
            this.lcgBarcodeList.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcgBarcodeList.CaptionImageOptions.Image")));
            buttonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions1.Image")));
            buttonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions2.Image")));
            buttonImageOptions3.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions3.Image")));
            buttonImageOptions4.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions4.Image")));
            buttonImageOptions5.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions5.Image")));
            this.lcgBarcodeList.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("저장", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, false, 1, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("승인완료", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, false, 2, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("상세 정산 정보", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, 3, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("내보내기", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, 4, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("전체선택", true, buttonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, false, false, null, -1)});
            this.lcgBarcodeList.CustomizationFormText = "현황";
            this.lcgBarcodeList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBarcodeList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgBarcodeList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.lcgBarcodeList.Location = new System.Drawing.Point(421, 120);
            this.lcgBarcodeList.Name = "lcgBarcodeList";
            this.lcgBarcodeList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgBarcodeList.Size = new System.Drawing.Size(931, 472);
            this.lcgBarcodeList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBarcodeList.Text = "정산 리스트";
            this.lcgBarcodeList.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonClick);
            this.lcgBarcodeList.CustomButtonUnchecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonUnchecked);
            this.lcgBarcodeList.CustomButtonChecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonChecked);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcList;
            this.layoutControlItem11.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(927, 449);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.layoutControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(409, 562);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(409, 30);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(12, 562);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.layoutControl3;
            this.layoutControlItem5.Location = new System.Drawing.Point(421, 30);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 90);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(24, 90);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(931, 90);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.layoutControl4;
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem18.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem18.MinSize = new System.Drawing.Size(24, 30);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(1352, 30);
            this.layoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextVisible = false;
            // 
            // usrAdjustmentReceiptList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 594);
            this.Controls.Add(this.layoutControl1);
            this.Name = "usrAdjustmentReceiptList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "정산 리스트";
            this.Load += new System.EventHandler(this.usrAdjustmentExamineList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deDtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leReceiptState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leAdjustmentState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teProductCnt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTotalPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchasedPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAdjustmentPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teProductPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgAdjustmentState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAdjustmentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAdjustmentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCompanyId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileAdjustmentState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComapnyId2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbeCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAdjust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileManufactureType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileNickName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbCheckYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCheckYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileProductGrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileHinge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbHinge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icHinge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBarcodeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraGrid.Columns.GridColumn gcInventoryId;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gcApprovalUser;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileUserId;
        private DevExpress.XtraGrid.Columns.GridColumn gcCreateDt;
        private DevExpress.XtraGrid.Columns.GridColumn gcCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn gcWarehousing;
        private DevExpress.XtraGrid.Columns.GridColumn gcBarcode;
        private DevExpress.XtraGrid.Columns.GridColumn gcInitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcManufactureType;
        private DevExpress.XtraGrid.Columns.GridColumn gcMBDManufactureNm;
        private DevExpress.XtraGrid.Columns.GridColumn gcMBDModelNm1;
        private DevExpress.XtraGrid.Columns.GridColumn gcMBDModelNm2;
        private DevExpress.XtraGrid.Columns.GridColumn gcCpu;
        private DevExpress.XtraGrid.Columns.GridColumn gcCategory;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gcGeneration;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileNickName;
        private DevExpress.XtraGrid.Columns.GridColumn gcMonSize;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheckYn;
        private DevExpress.XtraGrid.Columns.GridColumn gcDestroyedCase;
        private DevExpress.XtraGrid.Columns.GridColumn gcScracth;
        private DevExpress.XtraGrid.Columns.GridColumn gcStabbed;
        private DevExpress.XtraGrid.Columns.GridColumn gcPressed;
        private DevExpress.XtraGrid.Columns.GridColumn gcDisColored;
        private DevExpress.XtraGrid.Columns.GridColumn gcHinge;
        private DevExpress.XtraGrid.Columns.GridColumn gcCaseDes;
        private DevExpress.XtraGrid.Columns.GridColumn gcDisplay;
        private DevExpress.XtraGrid.Columns.GridColumn gcUsb;
        private DevExpress.XtraGrid.Columns.GridColumn gcMousePad;
        private DevExpress.XtraGrid.Columns.GridColumn gcKeyboard;
        private DevExpress.XtraGrid.Columns.GridColumn gcBattery;
        private DevExpress.XtraGrid.Columns.GridColumn gcCam;
        private DevExpress.XtraGrid.Columns.GridColumn gcWirelessLan;
        private DevExpress.XtraGrid.Columns.GridColumn gcWiredLan;
        private DevExpress.XtraGrid.Columns.GridColumn gcODD;
        private DevExpress.XtraGrid.Columns.GridColumn gcHDD;
        private DevExpress.XtraGrid.Columns.GridColumn gcBios;
        private DevExpress.XtraGrid.Columns.GridColumn gcOS;
        private DevExpress.XtraGrid.Columns.GridColumn gcTestCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbCheckYn;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbHinge;
        private DevExpress.XtraGrid.Columns.GridColumn gcAdjust;
        private DevExpress.XtraGrid.Columns.GridColumn gcSTATE;
        private LayoutControl layoutControl2;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gcAdjustmentList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAdjustmentList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileAdjustmentState;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileCompanyId;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rideCreateDt;
        private DevExpress.XtraGrid.Columns.GridColumn gcProductGrade;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileProductGrade;
        private SplitterItem splitterItem1;
        private DevExpress.Utils.ImageCollection icHinge;
        private DevExpress.Utils.ImageCollection icCheckYn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileComapnyId2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileManufactureType;
        private LayoutControl layoutControl3;
        private LayoutControlGroup layoutControlGroup2;
        private LayoutControlItem layoutControlItem6;
        private LayoutControlItem layoutControlItem7;
        private LayoutControlItem layoutControlItem12;
        private LayoutControlItem layoutControlItem13;
        private LayoutControlItem layoutControlItem14;
        private LayoutControlItem layoutControlItem16;
        private EmptySpaceItem emptySpaceItem4;
        private LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SpinEdit teProductCnt;
        private DevExpress.XtraEditors.SpinEdit teTotalPrice;
        private DevExpress.XtraEditors.SpinEdit teTax;
        private DevExpress.XtraEditors.SpinEdit tePurchasedPrice;
        private DevExpress.XtraEditors.SpinEdit teAdjustmentPrice;
        private DevExpress.XtraEditors.SpinEdit teProductPrice;
        private DevExpress.XtraEditors.RadioGroup rgAdjustmentState;
        private LayoutControlItem layoutControlItem39;
        private EmptySpaceItem emptySpaceItem3;
        private LayoutControl layoutControl4;
        private LayoutControlGroup layoutControlGroup3;
        private LayoutControlItem layoutControlItem18;
        private DevExpress.XtraEditors.DateEdit deDtFrom;
        private LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.DateEdit deDtTo;
        private LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.LookUpEdit leReceiptState;
        private LayoutControlItem layoutControlItem17;
        private DevExpress.XtraEditors.LookUpEdit leAdjustmentState;
        private LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private LayoutControlItem layoutControlItem15;
        private EmptySpaceItem emptySpaceItem5;
        private EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileHinge;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbeCnt;
        private DevExpress.Utils.ImageCollection icAdjust;
    }
}