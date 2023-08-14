using DevExpress.XtraLayout;
using static System.Windows.Forms.ImageList;

namespace WareHousingMaster.view.inventory
{
    partial class DlgInventoryListByWarehousingShort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgInventoryListByWarehousingShort));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.rideCreateDt = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcPart = new DevExpress.XtraGrid.GridControl();
            this.gvPart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleaseDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSerialNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileInventoryCat = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileInventoryType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rileInventoryState = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.sbCreate = new DevExpress.XtraEditors.SimpleButton();
            this.lcgInventory = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgPartList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.imgAssignYn = new DevExpress.Utils.ImageCollection(this.components);
            this.icCheckYn = new DevExpress.Utils.ImageCollection(this.components);
            this.icHinge = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReturnCompleteDt = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryCat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPartList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAssignYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCheckYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icHinge)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.gcPart);
            this.layoutControl1.Controls.Add(this.sbCreate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.lcgInventory;
            this.layoutControl1.Size = new System.Drawing.Size(1120, 489);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcPart
            // 
            this.gcPart.Location = new System.Drawing.Point(5, 50);
            this.gcPart.MainView = this.gvPart;
            this.gcPart.MinimumSize = new System.Drawing.Size(287, 0);
            this.gcPart.Name = "gcPart";
            this.gcPart.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileInventoryType,
            this.rileInventoryState,
            this.rileInventoryCat});
            this.gcPart.Size = new System.Drawing.Size(1110, 434);
            this.gcPart.TabIndex = 11;
            this.gcPart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPart});
            // 
            // gvPart
            // 
            this.gvPart.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCheck,
            this.gridColumn1,
            this.gridColumn5,
            this.gcPrice,
            this.gcReleaseDt,
            this.gcReturnCompleteDt,
            this.gcReleasePrice,
            this.gridColumn19,
            this.gridColumn2,
            this.gridColumn20,
            this.gcSerialNo,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gvPart.GridControl = this.gcPart;
            this.gvPart.Name = "gvPart";
            this.gvPart.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPart.OptionsView.ShowAutoFilterRow = true;
            this.gvPart.OptionsView.ShowFooter = true;
            this.gvPart.OptionsView.ShowGroupPanel = false;
            this.gvPart.OptionsView.ShowIndicator = false;
            this.gvPart.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPart_RowCellStyle);
            this.gvPart.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvPart_RowStyle);
            this.gvPart.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvPart_FocusedRowObjectChanged);
            // 
            // gcCheck
            // 
            this.gcCheck.AppearanceCell.Options.UseTextOptions = true;
            this.gcCheck.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCheck.AppearanceHeader.Options.UseBackColor = true;
            this.gcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.Caption = "체크";
            this.gcCheck.FieldName = "CHECK";
            this.gcCheck.MaxWidth = 40;
            this.gcCheck.MinWidth = 40;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.Width = 40;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "입고번호";
            this.gridColumn1.FieldName = "WAREHOUSING";
            this.gridColumn1.MaxWidth = 100;
            this.gridColumn1.MinWidth = 100;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 100;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "설명";
            this.gridColumn5.FieldName = "SPEC_NM";
            this.gridColumn5.MinWidth = 80;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 166;
            // 
            // gcPrice
            // 
            this.gcPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcPrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPrice.Caption = "접수번호";
            this.gcPrice.FieldName = "RECEIPT";
            this.gcPrice.MaxWidth = 150;
            this.gcPrice.MinWidth = 100;
            this.gcPrice.Name = "gcPrice";
            this.gcPrice.OptionsColumn.ReadOnly = true;
            this.gcPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PRICE", "{0:N0}")});
            this.gcPrice.Visible = true;
            this.gcPrice.VisibleIndex = 4;
            this.gcPrice.Width = 122;
            // 
            // gcReleaseDt
            // 
            this.gcReleaseDt.AppearanceCell.Options.UseTextOptions = true;
            this.gcReleaseDt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleaseDt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleaseDt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleaseDt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleaseDt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleaseDt.Caption = "출고날짜";
            this.gcReleaseDt.FieldName = "RELEASE_DT";
            this.gcReleaseDt.Name = "gcReleaseDt";
            this.gcReleaseDt.OptionsColumn.ReadOnly = true;
            this.gcReleaseDt.Visible = true;
            this.gcReleaseDt.VisibleIndex = 2;
            this.gcReleaseDt.Width = 85;
            // 
            // gcReleasePrice
            // 
            this.gcReleasePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleasePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleasePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleasePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleasePrice.Caption = "고객명";
            this.gcReleasePrice.FieldName = "CUSTOMER_NM";
            this.gcReleasePrice.MaxWidth = 150;
            this.gcReleasePrice.MinWidth = 80;
            this.gcReleasePrice.Name = "gcReleasePrice";
            this.gcReleasePrice.OptionsColumn.ReadOnly = true;
            this.gcReleasePrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RELEASE_PRICE", "{0:N0}")});
            this.gcReleasePrice.Visible = true;
            this.gcReleasePrice.VisibleIndex = 5;
            this.gcReleasePrice.Width = 96;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn19.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.Caption = "재고번호";
            this.gridColumn19.FieldName = "BARCODE";
            this.gridColumn19.MaxWidth = 120;
            this.gridColumn19.MinWidth = 100;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.ReadOnly = true;
            this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BARCODE", "{0:N0}")});
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 6;
            this.gridColumn19.Width = 103;
            // 
            // gcSerialNo
            // 
            this.gcSerialNo.AppearanceCell.Options.UseTextOptions = true;
            this.gcSerialNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcSerialNo.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcSerialNo.AppearanceHeader.Options.UseBackColor = true;
            this.gcSerialNo.AppearanceHeader.Options.UseTextOptions = true;
            this.gcSerialNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcSerialNo.Caption = "SERIAL NO";
            this.gcSerialNo.FieldName = "SERIAL_NO";
            this.gcSerialNo.Name = "gcSerialNo";
            this.gcSerialNo.OptionsColumn.ReadOnly = true;
            this.gcSerialNo.Visible = true;
            this.gcSerialNo.VisibleIndex = 9;
            this.gcSerialNo.Width = 146;
            // 
            // gridColumn20
            // 
            this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn20.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn20.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn20.Caption = "부품명";
            this.gridColumn20.FieldName = "MODEL_NM";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.OptionsColumn.ReadOnly = true;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 8;
            this.gridColumn20.Width = 143;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn6.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "부품상태";
            this.gridColumn6.ColumnEdit = this.rileInventoryCat;
            this.gridColumn6.FieldName = "INVENTORY_CAT";
            this.gridColumn6.MaxWidth = 80;
            this.gridColumn6.MinWidth = 80;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Width = 80;
            // 
            // rileInventoryCat
            // 
            this.rileInventoryCat.AutoHeight = false;
            this.rileInventoryCat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileInventoryCat.Name = "rileInventoryCat";
            this.rileInventoryCat.NullText = "";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn7.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "사용중";
            this.gridColumn7.FieldName = "LOCK_YN";
            this.gridColumn7.MaxWidth = 80;
            this.gridColumn7.MinWidth = 80;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Width = 80;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn8.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "생성일";
            this.gridColumn8.FieldName = "CREATE_DT";
            this.gridColumn8.MaxWidth = 100;
            this.gridColumn8.MinWidth = 100;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Width = 100;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn9.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "수정일";
            this.gridColumn9.FieldName = "UPDATE_DT";
            this.gridColumn9.MaxWidth = 100;
            this.gridColumn9.MinWidth = 100;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Width = 100;
            // 
            // rileInventoryType
            // 
            this.rileInventoryType.AutoHeight = false;
            this.rileInventoryType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileInventoryType.Name = "rileInventoryType";
            this.rileInventoryType.NullText = "";
            // 
            // rileInventoryState
            // 
            this.rileInventoryState.AutoHeight = false;
            this.rileInventoryState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileInventoryState.Name = "rileInventoryState";
            this.rileInventoryState.NullText = "";
            // 
            // sbCreate
            // 
            this.sbCreate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCreate.ImageOptions.Image")));
            this.sbCreate.Location = new System.Drawing.Point(1021, 3);
            this.sbCreate.Name = "sbCreate";
            this.sbCreate.Size = new System.Drawing.Size(96, 22);
            this.sbCreate.StyleController = this.layoutControl1;
            this.sbCreate.TabIndex = 9;
            this.sbCreate.Text = "확인";
            this.sbCreate.Click += new System.EventHandler(this.sbCreate_Click);
            // 
            // lcgInventory
            // 
            this.lcgInventory.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgInventory.GroupBordersVisible = false;
            this.lcgInventory.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgPartList,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.lcgInventory.Name = "lcgInventory";
            this.lcgInventory.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgInventory.Size = new System.Drawing.Size(1120, 489);
            this.lcgInventory.TextVisible = false;
            // 
            // lcgPartList
            // 
            this.lcgPartList.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcgPartList.CaptionImageOptions.Image")));
            buttonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions1.Image")));
            buttonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions2.Image")));
            this.lcgPartList.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("삭제", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, false, 1, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("전체선택", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, false, false, null, -1)});
            this.lcgPartList.CustomizationFormText = "현황";
            this.lcgPartList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgPartList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgPartList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcgPartList.Location = new System.Drawing.Point(0, 26);
            this.lcgPartList.Name = "lcgPartList";
            this.lcgPartList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgPartList.Size = new System.Drawing.Size(1118, 461);
            this.lcgPartList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgPartList.Text = "부품 리스트";
            this.lcgPartList.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgPartList_CustomButtonClick);
            this.lcgPartList.CustomButtonUnchecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgPartList_CustomButtonUnchecked);
            this.lcgPartList.CustomButtonChecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgPartList_CustomButtonChecked);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcPart;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1114, 438);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbCreate;
            this.layoutControlItem1.Location = new System.Drawing.Point(1018, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1018, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // imgAssignYn
            // 
            this.imgAssignYn.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgAssignYn.ImageStream")));
            this.imgAssignYn.InsertImage(global::WareHousingMaster.Properties.Resources.apply_16x169, "apply_16x169", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.imgAssignYn.Images.SetKeyName(0, "apply_16x169");
            this.imgAssignYn.InsertImage(global::WareHousingMaster.Properties.Resources.delete_16x16, "delete_16x16", typeof(global::WareHousingMaster.Properties.Resources), 1);
            this.imgAssignYn.Images.SetKeyName(1, "delete_16x16");
            // 
            // icCheckYn
            // 
            this.icCheckYn.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icCheckYn.ImageStream")));
            this.icCheckYn.InsertImage(global::WareHousingMaster.Properties.Resources.cancel_16x164, "cancel_16x164", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.icCheckYn.Images.SetKeyName(0, "cancel_16x164");
            // 
            // icHinge
            // 
            this.icHinge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icHinge.ImageStream")));
            this.icHinge.InsertImage(global::WareHousingMaster.Properties.Resources.ide_16x161, "ide_16x161", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.icHinge.Images.SetKeyName(0, "ide_16x161");
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "출고가";
            this.gridColumn2.DisplayFormat.FormatString = "N0";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "RELEASE_PRICE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 7;
            this.gridColumn2.Width = 65;
            // 
            // gcReturnCompleteDt
            // 
            this.gcReturnCompleteDt.AppearanceCell.Options.UseTextOptions = true;
            this.gcReturnCompleteDt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReturnCompleteDt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReturnCompleteDt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReturnCompleteDt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReturnCompleteDt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReturnCompleteDt.Caption = "입고날짜";
            this.gcReturnCompleteDt.FieldName = "RETURN_COMPLETE_DT";
            this.gcReturnCompleteDt.Name = "gcReturnCompleteDt";
            this.gcReturnCompleteDt.OptionsColumn.ReadOnly = true;
            this.gcReturnCompleteDt.Visible = true;
            this.gcReturnCompleteDt.VisibleIndex = 3;
            this.gcReturnCompleteDt.Width = 82;
            // 
            // DlgInventoryListByWarehousingShort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 489);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgInventoryListByWarehousingShort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "부품 정보 확인";
            this.Load += new System.EventHandler(this.DlgInventoryListByWarehousing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryCat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPartList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAssignYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCheckYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icHinge)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup lcgInventory;
        private DevExpress.XtraEditors.SimpleButton sbCreate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rideCreateDt;
        private DevExpress.Utils.ImageCollection icHinge;
        private DevExpress.Utils.ImageCollection icCheckYn;
        private DevExpress.XtraGrid.GridControl gcPart;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPart;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private LayoutControlGroup lcgPartList;
        private LayoutControlItem layoutControlItem2;
        private DevExpress.Utils.ImageCollection imgAssignYn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gcPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleasePrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileInventoryType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileInventoryState;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileInventoryCat;
        private EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleaseDt;
        private DevExpress.XtraGrid.Columns.GridColumn gcSerialNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gcReturnCompleteDt;
    }
}