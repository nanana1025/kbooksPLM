namespace WareHousingMaster.view.kbooks.search.booksearch
{
    partial class usrOrderBookModifyList
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
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleaseCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riteTitle = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStore99 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEtc = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // gcList
            // 
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(0, 0);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riteTitle});
            this.gcList.Size = new System.Drawing.Size(1316, 502);
            this.gcList.TabIndex = 7;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gcCheck,
            this.gridColumn21,
            this.gcReleaseCnt,
            this.gridColumn23,
            this.gcDes,
            this.gridColumn4,
            this.gcStore1,
            this.gcStore2,
            this.gcStore4,
            this.gcStore3,
            this.gcStore5,
            this.gcStore6,
            this.gcStore7,
            this.gcStore8,
            this.gcStore9,
            this.gcStore10,
            this.gcStore11,
            this.gcStore12,
            this.gcStore13,
            this.gcStore14,
            this.gcStore15,
            this.gcStore99,
            this.gcEtc});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsView.ShowAutoFilterRow = true;
            this.gvList.OptionsView.ShowFooter = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvList_RowCellStyle);
            this.gvList.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvList_FocusedRowObjectChanged);
            this.gvList.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvList_FocusedColumnChanged);
            this.gvList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvList_CellValueChanged);
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn6.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "STATE";
            this.gridColumn6.FieldName = "STATE";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gcCheck
            // 
            this.gcCheck.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCheck.AppearanceHeader.Options.UseBackColor = true;
            this.gcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.Caption = "체크";
            this.gcCheck.FieldName = "CHECK";
            this.gcCheck.MaxWidth = 35;
            this.gcCheck.MinWidth = 35;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.Visible = true;
            this.gcCheck.VisibleIndex = 0;
            this.gcCheck.Width = 35;
            // 
            // gridColumn21
            // 
            this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn21.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn21.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn21.Caption = "No";
            this.gridColumn21.DisplayFormat.FormatString = "n0";
            this.gridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn21.FieldName = "NO";
            this.gridColumn21.MaxWidth = 45;
            this.gridColumn21.MinWidth = 45;
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.OptionsColumn.ReadOnly = true;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 1;
            this.gridColumn21.Width = 45;
            // 
            // gcReleaseCnt
            // 
            this.gcReleaseCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleaseCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleaseCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleaseCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleaseCnt.Caption = "매입처명";
            this.gcReleaseCnt.FieldName = "PURCHNM";
            this.gcReleaseCnt.MaxWidth = 300;
            this.gcReleaseCnt.MinWidth = 150;
            this.gcReleaseCnt.Name = "gcReleaseCnt";
            this.gcReleaseCnt.OptionsColumn.AllowEdit = false;
            this.gcReleaseCnt.OptionsColumn.ReadOnly = true;
            this.gcReleaseCnt.Visible = true;
            this.gcReleaseCnt.VisibleIndex = 2;
            this.gcReleaseCnt.Width = 150;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn23.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.Caption = "도서명";
            this.gridColumn23.ColumnEdit = this.riteTitle;
            this.gridColumn23.FieldName = "BOOKNM";
            this.gridColumn23.MaxWidth = 500;
            this.gridColumn23.MinWidth = 200;
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.OptionsColumn.ReadOnly = true;
            this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BOOKNM", "{0:N0}")});
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 3;
            this.gridColumn23.Width = 264;
            // 
            // riteTitle
            // 
            this.riteTitle.AutoHeight = false;
            this.riteTitle.Name = "riteTitle";
            this.riteTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riteTitle_KeyDown);
            // 
            // gcDes
            // 
            this.gcDes.AppearanceCell.Options.UseTextOptions = true;
            this.gcDes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDes.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcDes.AppearanceHeader.Options.UseBackColor = true;
            this.gcDes.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDes.Caption = "현재고";
            this.gcDes.DisplayFormat.FormatString = "N0";
            this.gcDes.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcDes.FieldName = "STOCK";
            this.gcDes.MaxWidth = 80;
            this.gcDes.MinWidth = 60;
            this.gcDes.Name = "gcDes";
            this.gcDes.OptionsColumn.ReadOnly = true;
            this.gcDes.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STOCK", "{0:N0}")});
            this.gcDes.Visible = true;
            this.gcDes.VisibleIndex = 4;
            this.gcDes.Width = 60;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "주문합계";
            this.gridColumn4.DisplayFormat.FormatString = "N0";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "TOTAL";
            this.gridColumn4.MaxWidth = 80;
            this.gridColumn4.MinWidth = 60;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL", "{0:N0}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 60;
            // 
            // gcStore1
            // 
            this.gcStore1.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore1.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore1.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore1.Caption = "1매장";
            this.gcStore1.DisplayFormat.FormatString = "N0";
            this.gcStore1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore1.FieldName = "STORE1";
            this.gcStore1.MaxWidth = 80;
            this.gcStore1.MinWidth = 50;
            this.gcStore1.Name = "gcStore1";
            this.gcStore1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE1", "{0:n0}")});
            this.gcStore1.Visible = true;
            this.gcStore1.VisibleIndex = 6;
            this.gcStore1.Width = 70;
            // 
            // gcStore2
            // 
            this.gcStore2.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore2.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore2.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore2.Caption = "2매장";
            this.gcStore2.DisplayFormat.FormatString = "N0";
            this.gcStore2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore2.FieldName = "STORE2";
            this.gcStore2.MaxWidth = 80;
            this.gcStore2.MinWidth = 50;
            this.gcStore2.Name = "gcStore2";
            this.gcStore2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE2", "{0:n0}")});
            this.gcStore2.Visible = true;
            this.gcStore2.VisibleIndex = 7;
            this.gcStore2.Width = 70;
            // 
            // gcStore4
            // 
            this.gcStore4.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore4.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore4.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore4.Caption = "4매장";
            this.gcStore4.DisplayFormat.FormatString = "N0";
            this.gcStore4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore4.FieldName = "STORE4";
            this.gcStore4.MaxWidth = 80;
            this.gcStore4.MinWidth = 50;
            this.gcStore4.Name = "gcStore4";
            this.gcStore4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE4", "{0:N0}")});
            this.gcStore4.Visible = true;
            this.gcStore4.VisibleIndex = 9;
            this.gcStore4.Width = 70;
            // 
            // gcStore3
            // 
            this.gcStore3.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore3.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore3.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore3.Caption = "3매장";
            this.gcStore3.DisplayFormat.FormatString = "N0";
            this.gcStore3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore3.FieldName = "STORE3";
            this.gcStore3.MaxWidth = 80;
            this.gcStore3.MinWidth = 50;
            this.gcStore3.Name = "gcStore3";
            this.gcStore3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE3", "{0:N0}")});
            this.gcStore3.Visible = true;
            this.gcStore3.VisibleIndex = 8;
            this.gcStore3.Width = 70;
            // 
            // gcStore5
            // 
            this.gcStore5.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore5.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore5.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore5.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore5.Caption = "5매장";
            this.gcStore5.DisplayFormat.FormatString = "N0";
            this.gcStore5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore5.FieldName = "STORE5";
            this.gcStore5.MaxWidth = 80;
            this.gcStore5.MinWidth = 50;
            this.gcStore5.Name = "gcStore5";
            this.gcStore5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE5", "{0:N0}")});
            this.gcStore5.Visible = true;
            this.gcStore5.VisibleIndex = 10;
            this.gcStore5.Width = 70;
            // 
            // gcStore6
            // 
            this.gcStore6.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore6.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore6.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore6.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore6.Caption = "6매장";
            this.gcStore6.DisplayFormat.FormatString = "N0";
            this.gcStore6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore6.FieldName = "STORE6";
            this.gcStore6.MaxWidth = 80;
            this.gcStore6.MinWidth = 50;
            this.gcStore6.Name = "gcStore6";
            this.gcStore6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE6", "{0:N0}")});
            this.gcStore6.Visible = true;
            this.gcStore6.VisibleIndex = 11;
            this.gcStore6.Width = 58;
            // 
            // gcStore7
            // 
            this.gcStore7.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore7.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore7.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore7.Caption = "7매장";
            this.gcStore7.DisplayFormat.FormatString = "N0";
            this.gcStore7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore7.FieldName = "STORE7";
            this.gcStore7.MaxWidth = 80;
            this.gcStore7.MinWidth = 50;
            this.gcStore7.Name = "gcStore7";
            this.gcStore7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE7", "{0:n0}")});
            this.gcStore7.Visible = true;
            this.gcStore7.VisibleIndex = 12;
            this.gcStore7.Width = 70;
            // 
            // gcStore8
            // 
            this.gcStore8.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore8.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore8.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore8.Caption = "8매장";
            this.gcStore8.DisplayFormat.FormatString = "N0";
            this.gcStore8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore8.FieldName = "STORE8";
            this.gcStore8.MaxWidth = 80;
            this.gcStore8.MinWidth = 50;
            this.gcStore8.Name = "gcStore8";
            this.gcStore8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE8", "{0:N0}")});
            this.gcStore8.Visible = true;
            this.gcStore8.VisibleIndex = 13;
            this.gcStore8.Width = 55;
            // 
            // gcStore9
            // 
            this.gcStore9.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore9.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore9.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore9.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore9.Caption = "9매장";
            this.gcStore9.DisplayFormat.FormatString = "N0";
            this.gcStore9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore9.FieldName = "STORE9";
            this.gcStore9.MaxWidth = 80;
            this.gcStore9.MinWidth = 50;
            this.gcStore9.Name = "gcStore9";
            this.gcStore9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE9", "{0:N0}")});
            this.gcStore9.Visible = true;
            this.gcStore9.VisibleIndex = 14;
            this.gcStore9.Width = 65;
            // 
            // gcStore10
            // 
            this.gcStore10.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore10.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore10.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore10.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore10.Caption = "10매장";
            this.gcStore10.DisplayFormat.FormatString = "N0";
            this.gcStore10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore10.FieldName = "STORE10";
            this.gcStore10.MaxWidth = 80;
            this.gcStore10.MinWidth = 50;
            this.gcStore10.Name = "gcStore10";
            this.gcStore10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE10", "{0:N0}")});
            this.gcStore10.Visible = true;
            this.gcStore10.VisibleIndex = 15;
            this.gcStore10.Width = 70;
            // 
            // gcStore11
            // 
            this.gcStore11.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore11.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore11.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore11.Caption = "11매장";
            this.gcStore11.DisplayFormat.FormatString = "N0";
            this.gcStore11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore11.FieldName = "STORE11";
            this.gcStore11.MaxWidth = 80;
            this.gcStore11.MinWidth = 50;
            this.gcStore11.Name = "gcStore11";
            this.gcStore11.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE11", "{0:N0}")});
            this.gcStore11.Visible = true;
            this.gcStore11.VisibleIndex = 16;
            this.gcStore11.Width = 70;
            // 
            // gcStore12
            // 
            this.gcStore12.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore12.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore12.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore12.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore12.Caption = "12매장";
            this.gcStore12.DisplayFormat.FormatString = "N0";
            this.gcStore12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore12.FieldName = "STORE12";
            this.gcStore12.MaxWidth = 80;
            this.gcStore12.MinWidth = 50;
            this.gcStore12.Name = "gcStore12";
            this.gcStore12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE12", "{0:N0}")});
            this.gcStore12.Visible = true;
            this.gcStore12.VisibleIndex = 17;
            this.gcStore12.Width = 70;
            // 
            // gcStore13
            // 
            this.gcStore13.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore13.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore13.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore13.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore13.Caption = "13매장";
            this.gcStore13.DisplayFormat.FormatString = "N0";
            this.gcStore13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore13.FieldName = "STORE13";
            this.gcStore13.MaxWidth = 80;
            this.gcStore13.MinWidth = 50;
            this.gcStore13.Name = "gcStore13";
            this.gcStore13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE13", "{0:N0}")});
            this.gcStore13.Visible = true;
            this.gcStore13.VisibleIndex = 18;
            this.gcStore13.Width = 70;
            // 
            // gcStore14
            // 
            this.gcStore14.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore14.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore14.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore14.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore14.Caption = "14매장";
            this.gcStore14.DisplayFormat.FormatString = "N0";
            this.gcStore14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore14.FieldName = "STORE14";
            this.gcStore14.MaxWidth = 80;
            this.gcStore14.MinWidth = 50;
            this.gcStore14.Name = "gcStore14";
            this.gcStore14.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE14", "{0:N0}")});
            this.gcStore14.Visible = true;
            this.gcStore14.VisibleIndex = 19;
            this.gcStore14.Width = 70;
            // 
            // gcStore15
            // 
            this.gcStore15.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore15.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore15.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore15.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore15.Caption = "15매장";
            this.gcStore15.DisplayFormat.FormatString = "N0";
            this.gcStore15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore15.FieldName = "STORE15";
            this.gcStore15.MaxWidth = 80;
            this.gcStore15.MinWidth = 50;
            this.gcStore15.Name = "gcStore15";
            this.gcStore15.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STORE15", "{0:N0}")});
            this.gcStore15.Visible = true;
            this.gcStore15.VisibleIndex = 20;
            this.gcStore15.Width = 70;
            // 
            // gcStore99
            // 
            this.gcStore99.AppearanceCell.Options.UseTextOptions = true;
            this.gcStore99.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStore99.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStore99.AppearanceHeader.Options.UseBackColor = true;
            this.gcStore99.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStore99.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStore99.Caption = "99매장";
            this.gcStore99.DisplayFormat.FormatString = "n0";
            this.gcStore99.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStore99.FieldName = "STORE99";
            this.gcStore99.MaxWidth = 80;
            this.gcStore99.MinWidth = 50;
            this.gcStore99.Name = "gcStore99";
            // 
            // gcEtc
            // 
            this.gcEtc.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcEtc.AppearanceHeader.Options.UseBackColor = true;
            this.gcEtc.AppearanceHeader.Options.UseTextOptions = true;
            this.gcEtc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcEtc.Caption = "기타";
            this.gcEtc.FieldName = "ETC";
            this.gcEtc.MinWidth = 10;
            this.gcEtc.Name = "gcEtc";
            this.gcEtc.Visible = true;
            this.gcEtc.VisibleIndex = 21;
            this.gcEtc.Width = 534;
            // 
            // usrOrderBookModifyList
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcList);
            this.Name = "usrOrderBookModifyList";
            this.Size = new System.Drawing.Size(1316, 502);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore3;
        private DevExpress.XtraGrid.Columns.GridColumn gcDes;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleaseCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore8;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore5;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore7;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore1;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore2;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore4;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore6;
        private DevExpress.XtraGrid.Columns.GridColumn gcEtc;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riteTitle;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore10;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore11;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore12;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore13;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore14;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore15;
        private DevExpress.XtraGrid.Columns.GridColumn gcStore99;
    }
}
