namespace WareHousingMaster.view.kbooks.search.booksearch
{
    partial class usrBookOrderList
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
            this.gcCpu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riteBookCd = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riteTitle = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReceiptCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMarginCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleaseCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rilePurchCd = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileOrderRatio = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteBookCd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilePurchCd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileOrderRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcList
            // 
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(0, 0);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riteTitle,
            this.riteBookCd,
            this.rilePurchCd,
            this.rileOrderRatio,
            this.repositoryItemSpinEdit1});
            this.gcList.Size = new System.Drawing.Size(1103, 502);
            this.gcList.TabIndex = 7;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            this.gcList.DoubleClick += new System.EventHandler(this.gcList_DoubleClick);
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gcCheck,
            this.gridColumn21,
            this.gcCpu,
            this.gridColumn23,
            this.gcComponent,
            this.gridColumn4,
            this.gcStg,
            this.gcReceiptCnt,
            this.gcReleasePrice,
            this.gcSalePrice,
            this.gcMarginCost,
            this.gcDes,
            this.gcReleaseCnt,
            this.gcPrice,
            this.gridColumn1,
            this.gridColumn2});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsView.ShowAutoFilterRow = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvList_RowCellStyle);
            this.gvList.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvList_CustomRowCellEdit);
            this.gvList.ShownEditor += new System.EventHandler(this.gvList_ShownEditor);
            this.gvList.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvList_FocusedRowObjectChanged);
            this.gvList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvList_CellValueChanged);
            this.gvList.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvList_CellValueChanging);
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
            this.gridColumn21.MaxWidth = 35;
            this.gridColumn21.MinWidth = 35;
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 1;
            this.gridColumn21.Width = 35;
            // 
            // gcCpu
            // 
            this.gcCpu.AppearanceCell.Options.UseTextOptions = true;
            this.gcCpu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcCpu.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCpu.AppearanceHeader.Options.UseBackColor = true;
            this.gcCpu.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCpu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCpu.Caption = "도서코드";
            this.gcCpu.ColumnEdit = this.riteBookCd;
            this.gcCpu.FieldName = "BOOKCD";
            this.gcCpu.Name = "gcCpu";
            this.gcCpu.Visible = true;
            this.gcCpu.VisibleIndex = 2;
            this.gcCpu.Width = 68;
            // 
            // riteBookCd
            // 
            this.riteBookCd.AutoHeight = false;
            this.riteBookCd.Name = "riteBookCd";
            this.riteBookCd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riteBookCd_KeyDown);
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn23.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.Caption = "도서명";
            this.gridColumn23.ColumnEdit = this.riteTitle;
            this.gridColumn23.FieldName = "BOOKNM";
            this.gridColumn23.MaxWidth = 300;
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "COMPONENT_CD", "{0:N0}")});
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 3;
            this.gridColumn23.Width = 100;
            // 
            // riteTitle
            // 
            this.riteTitle.AutoHeight = false;
            this.riteTitle.Name = "riteTitle";
            this.riteTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riteTitle_KeyDown);
            // 
            // gcComponent
            // 
            this.gcComponent.AppearanceCell.Options.UseTextOptions = true;
            this.gcComponent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcComponent.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcComponent.AppearanceHeader.Options.UseBackColor = true;
            this.gcComponent.AppearanceHeader.Options.UseTextOptions = true;
            this.gcComponent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcComponent.Caption = "저자명";
            this.gcComponent.FieldName = "AUTHOR";
            this.gcComponent.Name = "gcComponent";
            this.gcComponent.OptionsColumn.AllowEdit = false;
            this.gcComponent.OptionsColumn.ReadOnly = true;
            this.gcComponent.Visible = true;
            this.gcComponent.VisibleIndex = 4;
            this.gcComponent.Width = 66;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "출판사명";
            this.gridColumn4.FieldName = "PUBSHNM";
            this.gridColumn4.MaxWidth = 300;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 90;
            // 
            // gcStg
            // 
            this.gcStg.AppearanceCell.Options.UseTextOptions = true;
            this.gcStg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStg.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStg.AppearanceHeader.Options.UseBackColor = true;
            this.gcStg.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStg.Caption = "입고수량";
            this.gcStg.FieldName = "ORD_CNT";
            this.gcStg.MaxWidth = 55;
            this.gcStg.MinWidth = 55;
            this.gcStg.Name = "gcStg";
            this.gcStg.OptionsColumn.AllowEdit = false;
            this.gcStg.OptionsColumn.ReadOnly = true;
            this.gcStg.Visible = true;
            this.gcStg.VisibleIndex = 9;
            this.gcStg.Width = 55;
            // 
            // gcReceiptCnt
            // 
            this.gcReceiptCnt.AppearanceCell.Options.UseTextOptions = true;
            this.gcReceiptCnt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReceiptCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReceiptCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReceiptCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReceiptCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReceiptCnt.Caption = "주문예정수량";
            this.gcReceiptCnt.FieldName = "ESTI_CNT";
            this.gcReceiptCnt.MaxWidth = 90;
            this.gcReceiptCnt.MinWidth = 80;
            this.gcReceiptCnt.Name = "gcReceiptCnt";
            this.gcReceiptCnt.OptionsColumn.ReadOnly = true;
            this.gcReceiptCnt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RECEIPT_CNT", "{0:N0}")});
            this.gcReceiptCnt.Visible = true;
            this.gcReceiptCnt.VisibleIndex = 10;
            this.gcReceiptCnt.Width = 80;
            // 
            // gcReleasePrice
            // 
            this.gcReleasePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReleasePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleasePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleasePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleasePrice.Caption = "정가";
            this.gcReleasePrice.FieldName = "PRICE";
            this.gcReleasePrice.MaxWidth = 80;
            this.gcReleasePrice.MinWidth = 80;
            this.gcReleasePrice.Name = "gcReleasePrice";
            this.gcReleasePrice.OptionsColumn.AllowEdit = false;
            this.gcReleasePrice.OptionsColumn.ReadOnly = true;
            this.gcReleasePrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SALE_PRICE", "{0:n0}")});
            this.gcReleasePrice.Visible = true;
            this.gcReleasePrice.VisibleIndex = 6;
            this.gcReleasePrice.Width = 80;
            // 
            // gcSalePrice
            // 
            this.gcSalePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcSalePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcSalePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcSalePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcSalePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcSalePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcSalePrice.Caption = "반품예정수량";
            this.gcSalePrice.FieldName = "RETURN_CNT";
            this.gcSalePrice.MaxWidth = 80;
            this.gcSalePrice.MinWidth = 80;
            this.gcSalePrice.Name = "gcSalePrice";
            this.gcSalePrice.OptionsColumn.ReadOnly = true;
            this.gcSalePrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SALE_PRICE", "{0:N0}")});
            this.gcSalePrice.Visible = true;
            this.gcSalePrice.VisibleIndex = 8;
            this.gcSalePrice.Width = 80;
            // 
            // gcMarginCost
            // 
            this.gcMarginCost.AppearanceCell.Options.UseTextOptions = true;
            this.gcMarginCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcMarginCost.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcMarginCost.AppearanceHeader.Options.UseBackColor = true;
            this.gcMarginCost.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMarginCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMarginCost.Caption = "납품예정수량";
            this.gcMarginCost.FieldName = "INP_CNT";
            this.gcMarginCost.MaxWidth = 80;
            this.gcMarginCost.MinWidth = 80;
            this.gcMarginCost.Name = "gcMarginCost";
            this.gcMarginCost.OptionsColumn.ReadOnly = true;
            this.gcMarginCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MARGIN_COST", "{0:n0}")});
            this.gcMarginCost.Visible = true;
            this.gcMarginCost.VisibleIndex = 7;
            this.gcMarginCost.Width = 80;
            // 
            // gcDes
            // 
            this.gcDes.AppearanceCell.Options.UseTextOptions = true;
            this.gcDes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDes.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcDes.AppearanceHeader.Options.UseBackColor = true;
            this.gcDes.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDes.Caption = "재고";
            this.gcDes.FieldName = "STOCK_CNT";
            this.gcDes.MaxWidth = 80;
            this.gcDes.MinWidth = 60;
            this.gcDes.Name = "gcDes";
            this.gcDes.OptionsColumn.ReadOnly = true;
            this.gcDes.Visible = true;
            this.gcDes.VisibleIndex = 11;
            this.gcDes.Width = 60;
            // 
            // gcReleaseCnt
            // 
            this.gcReleaseCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleaseCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleaseCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleaseCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleaseCnt.Caption = "매입처명";
            this.gcReleaseCnt.ColumnEdit = this.rilePurchCd;
            this.gcReleaseCnt.FieldName = "PURCHCD";
            this.gcReleaseCnt.MaxWidth = 200;
            this.gcReleaseCnt.MinWidth = 60;
            this.gcReleaseCnt.Name = "gcReleaseCnt";
            this.gcReleaseCnt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CNT", "{0:N0}")});
            this.gcReleaseCnt.Visible = true;
            this.gcReleaseCnt.VisibleIndex = 12;
            this.gcReleaseCnt.Width = 91;
            // 
            // rilePurchCd
            // 
            this.rilePurchCd.AutoHeight = false;
            this.rilePurchCd.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rilePurchCd.Name = "rilePurchCd";
            this.rilePurchCd.NullText = "";
            this.rilePurchCd.EditValueChanged += new System.EventHandler(this.rilePurchCd_EditValueChanged);
            // 
            // gcPrice
            // 
            this.gcPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcPrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPrice.Caption = "매입률";
            this.gcPrice.ColumnEdit = this.rileOrderRatio;
            this.gcPrice.FieldName = "ORDER_RATIO";
            this.gcPrice.MaxWidth = 100;
            this.gcPrice.MinWidth = 70;
            this.gcPrice.Name = "gcPrice";
            this.gcPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PRICE", "{0:n0}")});
            this.gcPrice.Visible = true;
            this.gcPrice.VisibleIndex = 13;
            this.gcPrice.Width = 86;
            // 
            // rileOrderRatio
            // 
            this.rileOrderRatio.AutoHeight = false;
            this.rileOrderRatio.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileOrderRatio.Name = "rileOrderRatio";
            this.rileOrderRatio.NullText = "";
            this.rileOrderRatio.EditValueChanged += new System.EventHandler(this.rileOrderRatio_EditValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "주문수량";
            this.gridColumn1.ColumnEdit = this.repositoryItemSpinEdit1;
            this.gridColumn1.FieldName = "ORDER_CNT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 14;
            this.gridColumn1.Width = 51;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "기타";
            this.gridColumn2.FieldName = "ETC";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 15;
            this.gridColumn2.Width = 44;
            // 
            // usrBookOrderList
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcList);
            this.Name = "usrBookOrderList";
            this.Size = new System.Drawing.Size(1103, 502);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteBookCd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilePurchCd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileOrderRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gcComponent;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gcReceiptCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcDes;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleaseCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcCpu;
        private DevExpress.XtraGrid.Columns.GridColumn gcStg;
        private DevExpress.XtraGrid.Columns.GridColumn gcPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleasePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcMarginCost;
        private DevExpress.XtraGrid.Columns.GridColumn gcSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riteTitle;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riteBookCd;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilePurchCd;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileOrderRatio;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
    }
}
