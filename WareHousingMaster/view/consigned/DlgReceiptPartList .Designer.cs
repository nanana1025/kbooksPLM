using DevExpress.XtraLayout;
using static System.Windows.Forms.ImageList;

namespace WareHousingMaster.view.consigned
{
    partial class DlgReceiptPartList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgReceiptPartList));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.rideCreateDt = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcPart = new DevExpress.XtraGrid.GridControl();
            this.gvPart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcConsignedType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileConsignedType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcAssign = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riiceAssignYn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgAssignYn = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.risePartPrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.sbCreate = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBarcodeList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.icCheckYn = new DevExpress.Utils.ImageCollection(this.components);
            this.icHinge = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileConsignedType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riiceAssignYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAssignYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.risePartPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(637, 360);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcPart
            // 
            this.gcPart.Location = new System.Drawing.Point(5, 52);
            this.gcPart.MainView = this.gvPart;
            this.gcPart.MinimumSize = new System.Drawing.Size(287, 0);
            this.gcPart.Name = "gcPart";
            this.gcPart.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riiceAssignYn,
            this.repositoryItemLookUpEdit2,
            this.risePartPrice,
            this.rileConsignedType,
            this.repositoryItemCheckEdit1});
            this.gcPart.Size = new System.Drawing.Size(627, 303);
            this.gcPart.TabIndex = 11;
            this.gcPart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPart});
            // 
            // gvPart
            // 
            this.gvPart.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn30,
            this.gridColumn19,
            this.gridColumn20,
            this.gcConsignedType,
            this.gcAssign,
            this.gridColumn21,
            this.gridColumn1});
            this.gvPart.GridControl = this.gcPart;
            this.gvPart.Name = "gvPart";
            this.gvPart.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPart.OptionsView.ShowFooter = true;
            this.gvPart.OptionsView.ShowGroupPanel = false;
            this.gvPart.OptionsView.ShowIndicator = false;
            this.gvPart.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPart_RowCellStyle);
            this.gvPart.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvPart_RowStyle);
            this.gvPart.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvPart_FocusedRowObjectChanged);
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "TYPE";
            this.gridColumn18.FieldName = "TYPE";
            this.gridColumn18.Name = "gridColumn18";
            // 
            // gridColumn30
            // 
            this.gridColumn30.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn30.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn30.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn30.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn30.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn30.Caption = "체크";
            this.gridColumn30.FieldName = "CHECK";
            this.gridColumn30.MaxWidth = 40;
            this.gridColumn30.MinWidth = 40;
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 0;
            this.gridColumn30.Width = 40;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn19.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.Caption = "품목명";
            this.gridColumn19.FieldName = "COMPONENT_CD";
            this.gridColumn19.MaxWidth = 70;
            this.gridColumn19.MinWidth = 30;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 1;
            this.gridColumn19.Width = 51;
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
            this.gridColumn20.FieldName = "DETAIL_DATA";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.ReadOnly = true;
            this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "DETAIL_DATA", "TOTAL: {0}EA")});
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 2;
            this.gridColumn20.Width = 120;
            // 
            // gcConsignedType
            // 
            this.gcConsignedType.AppearanceCell.Options.UseTextOptions = true;
            this.gcConsignedType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcConsignedType.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcConsignedType.AppearanceHeader.Options.UseBackColor = true;
            this.gcConsignedType.AppearanceHeader.Options.UseTextOptions = true;
            this.gcConsignedType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcConsignedType.Caption = "부품타입";
            this.gcConsignedType.ColumnEdit = this.rileConsignedType;
            this.gcConsignedType.FieldName = "CONSIGNED_TYPE";
            this.gcConsignedType.MaxWidth = 70;
            this.gcConsignedType.MinWidth = 40;
            this.gcConsignedType.Name = "gcConsignedType";
            this.gcConsignedType.Visible = true;
            this.gcConsignedType.VisibleIndex = 4;
            this.gcConsignedType.Width = 47;
            // 
            // rileConsignedType
            // 
            this.rileConsignedType.AutoHeight = false;
            this.rileConsignedType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileConsignedType.DropDownRows = 2;
            this.rileConsignedType.Name = "rileConsignedType";
            this.rileConsignedType.NullText = "";
            // 
            // gcAssign
            // 
            this.gcAssign.AppearanceCell.Options.UseTextOptions = true;
            this.gcAssign.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcAssign.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcAssign.AppearanceHeader.Options.UseBackColor = true;
            this.gcAssign.AppearanceHeader.Options.UseTextOptions = true;
            this.gcAssign.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcAssign.Caption = "할당여부";
            this.gcAssign.ColumnEdit = this.riiceAssignYn;
            this.gcAssign.FieldName = "ASSIGN_YN";
            this.gcAssign.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.gcAssign.MaxWidth = 60;
            this.gcAssign.Name = "gcAssign";
            this.gcAssign.Visible = true;
            this.gcAssign.VisibleIndex = 3;
            this.gcAssign.Width = 47;
            // 
            // riiceAssignYn
            // 
            this.riiceAssignYn.AutoHeight = false;
            this.riiceAssignYn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riiceAssignYn.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riiceAssignYn.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", false, 1)});
            this.riiceAssignYn.LargeImages = this.imgAssignYn;
            this.riiceAssignYn.Name = "riiceAssignYn";
            this.riiceAssignYn.SmallImages = this.imgAssignYn;
            // 
            // imgAssignYn
            // 
            this.imgAssignYn.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgAssignYn.ImageStream")));
            this.imgAssignYn.InsertImage(global::WareHousingMaster.Properties.Resources.apply_16x169, "apply_16x169", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.imgAssignYn.Images.SetKeyName(0, "apply_16x169");
            this.imgAssignYn.InsertImage(global::WareHousingMaster.Properties.Resources.delete_16x16, "delete_16x16", typeof(global::WareHousingMaster.Properties.Resources), 1);
            this.imgAssignYn.Images.SetKeyName(1, "delete_16x16");
            // 
            // gridColumn21
            // 
            this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn21.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn21.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn21.Caption = "금액";
            this.gridColumn21.ColumnEdit = this.risePartPrice;
            this.gridColumn21.DisplayFormat.FormatString = "N0";
            this.gridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn21.FieldName = "PROXY_PRICE";
            this.gridColumn21.MaxWidth = 60;
            this.gridColumn21.MinWidth = 30;
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PROXY_PRICE", "{0:N0}")});
            this.gridColumn21.Width = 55;
            // 
            // risePartPrice
            // 
            this.risePartPrice.AutoHeight = false;
            this.risePartPrice.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.risePartPrice.DisplayFormat.FormatString = "n0";
            this.risePartPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.risePartPrice.Name = "risePartPrice";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ASSIGN_YN_O";
            this.gridColumn1.FieldName = "ASSIGN_YN_O";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // sbCreate
            // 
            this.sbCreate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCreate.ImageOptions.Image")));
            this.sbCreate.Location = new System.Drawing.Point(438, 3);
            this.sbCreate.Name = "sbCreate";
            this.sbCreate.Size = new System.Drawing.Size(96, 22);
            this.sbCreate.StyleController = this.layoutControl1;
            this.sbCreate.TabIndex = 9;
            this.sbCreate.Text = "확인";
            this.sbCreate.Click += new System.EventHandler(this.sbCreate_Click);
            // 
            // sbCancel
            // 
            this.sbCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancel.ImageOptions.Image")));
            this.sbCancel.Location = new System.Drawing.Point(538, 3);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(96, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 9;
            this.sbCancel.Text = "취소";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgBarcodeList,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(637, 360);
            this.Root.TextVisible = false;
            // 
            // lcgBarcodeList
            // 
            this.lcgBarcodeList.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcgBarcodeList.CaptionImageOptions.Image")));
            buttonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions1.Image")));
            this.lcgBarcodeList.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("전체선택", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, false, true, null, -1)});
            this.lcgBarcodeList.CustomizationFormText = "현황";
            this.lcgBarcodeList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBarcodeList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgBarcodeList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcgBarcodeList.Location = new System.Drawing.Point(0, 26);
            this.lcgBarcodeList.Name = "lcgBarcodeList";
            this.lcgBarcodeList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgBarcodeList.Size = new System.Drawing.Size(635, 332);
            this.lcgBarcodeList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBarcodeList.Text = "부품 리스트";
            this.lcgBarcodeList.CustomButtonUnchecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonUnchecked);
            this.lcgBarcodeList.CustomButtonChecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonChecked);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcPart;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(631, 307);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbCreate;
            this.layoutControlItem1.Location = new System.Drawing.Point(435, 0);
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
            this.emptySpaceItem1.Size = new System.Drawing.Size(435, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbCancel;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem3.Location = new System.Drawing.Point(535, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem1";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
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
            // DlgReceiptPartList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 360);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgReceiptPartList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "부품 정보 확인";
            this.Load += new System.EventHandler(this.usrAdjustmentExamineList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileConsignedType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riiceAssignYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAssignYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.risePartPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCheckYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icHinge)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbCreate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rideCreateDt;
        private DevExpress.Utils.ImageCollection icHinge;
        private DevExpress.Utils.ImageCollection icCheckYn;
        private DevExpress.XtraGrid.GridControl gcPart;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPart;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gcConsignedType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileConsignedType;
        private DevExpress.XtraGrid.Columns.GridColumn gcAssign;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riiceAssignYn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit risePartPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private LayoutControlGroup lcgBarcodeList;
        private LayoutControlItem layoutControlItem2;
        private EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private LayoutControlItem layoutControlItem3;
        private DevExpress.Utils.ImageCollection imgAssignYn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}