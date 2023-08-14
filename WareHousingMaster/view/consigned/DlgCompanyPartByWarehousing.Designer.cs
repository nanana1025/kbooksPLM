using DevExpress.XtraLayout;
using static System.Windows.Forms.ImageList;

namespace WareHousingMaster.view.consigned
{
    partial class DlgCompanyPartByWarehousing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgCompanyPartByWarehousing));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.rideCreateDt = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcPart = new DevExpress.XtraGrid.GridControl();
            this.gvPart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riiceAssignYn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgAssignYn = new DevExpress.Utils.ImageCollection(this.components);
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.risePartPrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rileConsignedType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.sbCreate = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgPartList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.icCheckYn = new DevExpress.Utils.ImageCollection(this.components);
            this.icHinge = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riiceAssignYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAssignYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.risePartPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileConsignedType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPartList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
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
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1330, 489);
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
            this.riiceAssignYn,
            this.repositoryItemLookUpEdit2,
            this.risePartPrice,
            this.rileConsignedType,
            this.repositoryItemCheckEdit1});
            this.gcPart.Size = new System.Drawing.Size(1320, 434);
            this.gcPart.TabIndex = 11;
            this.gcPart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPart});
            // 
            // gvPart
            // 
            this.gvPart.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn30,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gcPrice,
            this.gcTotalPrice});
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
            this.gvPart.DoubleClick += new System.EventHandler(this.gvPart_DoubleClick);
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
            this.gridColumn30.Width = 40;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "입고번호";
            this.gridColumn4.FieldName = "WAREHOUSING";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 100;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "입고일";
            this.gridColumn3.FieldName = "WAREHOUSING_DT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 119;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "설명";
            this.gridColumn2.FieldName = "SPEC_NM";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 149;
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
            this.gridColumn19.OptionsColumn.ReadOnly = true;
            this.gridColumn19.Width = 70;
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
            this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "DETAIL_DATA", "TOTAL: {0}EA")});
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 3;
            this.gridColumn20.Width = 234;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "입고개수";
            this.gridColumn1.DisplayFormat.FormatString = "N0";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "WAREHOUSING_CNT";
            this.gridColumn1.MaxWidth = 80;
            this.gridColumn1.MinWidth = 80;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WAREHOUSING_CNT", "{0:N0}")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 80;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "재고수량[1]";
            this.gridColumn5.DisplayFormat.FormatString = "N0";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "TOTAL_CNT";
            this.gridColumn5.MaxWidth = 80;
            this.gridColumn5.MinWidth = 80;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_CNT", "{0:N0}")});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn6.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn6.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "정상수량";
            this.gridColumn6.DisplayFormat.FormatString = "N0";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "GOOD_CNT";
            this.gridColumn6.MaxWidth = 80;
            this.gridColumn6.MinWidth = 80;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GOOD_CNT", "{0:N0}")});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn7.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "불량수량";
            this.gridColumn7.DisplayFormat.FormatString = "N0";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "FAULT_CNT";
            this.gridColumn7.MaxWidth = 80;
            this.gridColumn7.MinWidth = 80;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FAULT_CNT", "{0:N0}")});
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 80;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn8.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "출고수량";
            this.gridColumn8.DisplayFormat.FormatString = "N0";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "RELEASE_CNT";
            this.gridColumn8.MaxWidth = 80;
            this.gridColumn8.MinWidth = 80;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RELEASE_CNT", "{0:N0}")});
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 80;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn9.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn9.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "대기수량";
            this.gridColumn9.DisplayFormat.FormatString = "N0";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "LOCK_CNT";
            this.gridColumn9.MaxWidth = 80;
            this.gridColumn9.MinWidth = 80;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LOCK_CNT", "{0:N0}")});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 80;
            // 
            // gcPrice
            // 
            this.gcPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcPrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPrice.Caption = "부품가[2]";
            this.gcPrice.DisplayFormat.FormatString = "N0";
            this.gcPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcPrice.FieldName = "PART_RELEASE_PRICE";
            this.gcPrice.MaxWidth = 80;
            this.gcPrice.MinWidth = 80;
            this.gcPrice.Name = "gcPrice";
            this.gcPrice.OptionsColumn.AllowEdit = false;
            this.gcPrice.OptionsColumn.ReadOnly = true;
            this.gcPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PART_RELEASE_PRICE", "{0:N0}")});
            this.gcPrice.Visible = true;
            this.gcPrice.VisibleIndex = 10;
            this.gcPrice.Width = 80;
            // 
            // gcTotalPrice
            // 
            this.gcTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcTotalPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcTotalPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcTotalPrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcTotalPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcTotalPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcTotalPrice.Caption = "총 부품가[1*2]";
            this.gcTotalPrice.DisplayFormat.FormatString = "N0";
            this.gcTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcTotalPrice.FieldName = "TOTAL_PRICE";
            this.gcTotalPrice.MaxWidth = 100;
            this.gcTotalPrice.MinWidth = 100;
            this.gcTotalPrice.Name = "gcTotalPrice";
            this.gcTotalPrice.OptionsColumn.AllowEdit = false;
            this.gcTotalPrice.OptionsColumn.ReadOnly = true;
            this.gcTotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_PRICE", "{0:N0}")});
            this.gcTotalPrice.Visible = true;
            this.gcTotalPrice.VisibleIndex = 11;
            this.gcTotalPrice.Width = 100;
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
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
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
            // rileConsignedType
            // 
            this.rileConsignedType.AutoHeight = false;
            this.rileConsignedType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileConsignedType.DropDownRows = 2;
            this.rileConsignedType.Name = "rileConsignedType";
            this.rileConsignedType.NullText = "";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // sbCreate
            // 
            this.sbCreate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCreate.ImageOptions.Image")));
            this.sbCreate.Location = new System.Drawing.Point(1231, 3);
            this.sbCreate.Name = "sbCreate";
            this.sbCreate.Size = new System.Drawing.Size(96, 22);
            this.sbCreate.StyleController = this.layoutControl1;
            this.sbCreate.TabIndex = 9;
            this.sbCreate.Text = "확인";
            this.sbCreate.Click += new System.EventHandler(this.sbCreate_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgPartList,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(1330, 489);
            this.Root.TextVisible = false;
            // 
            // lcgPartList
            // 
            this.lcgPartList.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcgPartList.CaptionImageOptions.Image")));
            buttonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions1.Image")));
            this.lcgPartList.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("부품복사", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, 1, -1)});
            this.lcgPartList.CustomizationFormText = "현황";
            this.lcgPartList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgPartList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgPartList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcgPartList.Location = new System.Drawing.Point(0, 26);
            this.lcgPartList.Name = "lcgPartList";
            this.lcgPartList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgPartList.Size = new System.Drawing.Size(1328, 461);
            this.lcgPartList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgPartList.Text = "부품 리스트";
            this.lcgPartList.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgPartList_CustomButtonClick);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcPart;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1324, 438);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbCreate;
            this.layoutControlItem1.Location = new System.Drawing.Point(1228, 0);
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
            this.emptySpaceItem1.Size = new System.Drawing.Size(1228, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
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
            // DlgCompanyPartByWarehousing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 489);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgCompanyPartByWarehousing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "부품 정보 확인";
            this.Load += new System.EventHandler(this.usrAdjustmentExamineList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riiceAssignYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAssignYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.risePartPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileConsignedType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPartList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
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
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileConsignedType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riiceAssignYn;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit risePartPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private LayoutControlGroup lcgPartList;
        private LayoutControlItem layoutControlItem2;
        private EmptySpaceItem emptySpaceItem1;
        private DevExpress.Utils.ImageCollection imgAssignYn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gcPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcTotalPrice;
    }
}