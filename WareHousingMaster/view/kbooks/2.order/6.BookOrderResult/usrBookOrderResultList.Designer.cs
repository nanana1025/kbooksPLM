namespace WareHousingMaster.view.kbooks.search.booksearch
{
    partial class usrBookOrderResultList
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
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCpu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReceiptCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMarginCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riteTitle = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
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
            this.gcList.Size = new System.Drawing.Size(1019, 502);
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
            this.gridColumn1,
            this.gcPrice,
            this.gcStg,
            this.gridColumn23,
            this.gcComponent,
            this.gridColumn4,
            this.gcCpu,
            this.gcReceiptCnt,
            this.gcDes,
            this.gcReleasePrice,
            this.gcMarginCost,
            this.gcSalePrice,
            this.gridColumn2});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsView.ShowAutoFilterRow = true;
            this.gvList.OptionsView.ShowFooter = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvList_RowCellStyle);
            this.gvList.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvList_FocusedRowObjectChanged);
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
            this.gridColumn21.MaxWidth = 50;
            this.gridColumn21.MinWidth = 45;
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
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
            this.gcReleaseCnt.MaxWidth = 200;
            this.gcReleaseCnt.MinWidth = 100;
            this.gcReleaseCnt.Name = "gcReleaseCnt";
            this.gcReleaseCnt.OptionsColumn.AllowEdit = false;
            this.gcReleaseCnt.Visible = true;
            this.gcReleaseCnt.VisibleIndex = 2;
            this.gcReleaseCnt.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "매장코드";
            this.gridColumn1.FieldName = "STORECD";
            this.gridColumn1.MinWidth = 70;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 70;
            // 
            // gcPrice
            // 
            this.gcPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcPrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPrice.Caption = "조명";
            this.gcPrice.FieldName = "GROUP_NM";
            this.gcPrice.MinWidth = 70;
            this.gcPrice.Name = "gcPrice";
            this.gcPrice.OptionsColumn.AllowEdit = false;
            this.gcPrice.Visible = true;
            this.gcPrice.VisibleIndex = 4;
            this.gcPrice.Width = 70;
            // 
            // gcStg
            // 
            this.gcStg.AppearanceCell.Options.UseTextOptions = true;
            this.gcStg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcStg.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStg.AppearanceHeader.Options.UseBackColor = true;
            this.gcStg.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStg.Caption = "도서코드";
            this.gcStg.FieldName = "BOOKCD";
            this.gcStg.MaxWidth = 120;
            this.gcStg.MinWidth = 90;
            this.gcStg.Name = "gcStg";
            this.gcStg.OptionsColumn.AllowEdit = false;
            this.gcStg.Visible = true;
            this.gcStg.VisibleIndex = 5;
            this.gcStg.Width = 105;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn23.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn23.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.Caption = "도서명";
            this.gridColumn23.FieldName = "BOOKNM";
            this.gridColumn23.MaxWidth = 300;
            this.gridColumn23.MinWidth = 100;
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 6;
            this.gridColumn23.Width = 172;
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
            this.gcComponent.FieldName = "AUTHORNM";
            this.gcComponent.Name = "gcComponent";
            this.gcComponent.OptionsColumn.AllowEdit = false;
            this.gcComponent.Visible = true;
            this.gcComponent.VisibleIndex = 7;
            this.gcComponent.Width = 110;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "주문수량";
            this.gridColumn4.DisplayFormat.FormatString = "N0";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "ORD_COUNT";
            this.gridColumn4.MaxWidth = 80;
            this.gridColumn4.MinWidth = 60;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ORD_COUNT", "{0:N0}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 8;
            this.gridColumn4.Width = 60;
            // 
            // gcCpu
            // 
            this.gcCpu.AppearanceCell.Options.UseTextOptions = true;
            this.gcCpu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcCpu.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCpu.AppearanceHeader.Options.UseBackColor = true;
            this.gcCpu.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCpu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCpu.Caption = "거래조건";
            this.gcCpu.FieldName = "ORDER_TYPE";
            this.gcCpu.MaxWidth = 100;
            this.gcCpu.MinWidth = 80;
            this.gcCpu.Name = "gcCpu";
            this.gcCpu.Width = 80;
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
            this.gcReceiptCnt.FieldName = "ORDER_EXPECTED_CNT";
            this.gcReceiptCnt.MaxWidth = 90;
            this.gcReceiptCnt.MinWidth = 80;
            this.gcReceiptCnt.Name = "gcReceiptCnt";
            this.gcReceiptCnt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RECEIPT_CNT", "{0:N0}")});
            this.gcReceiptCnt.Width = 80;
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
            this.gcDes.FieldName = "STOCK_CNT";
            this.gcDes.MaxWidth = 80;
            this.gcDes.MinWidth = 60;
            this.gcDes.Name = "gcDes";
            this.gcDes.Width = 60;
            // 
            // gcReleasePrice
            // 
            this.gcReleasePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReleasePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleasePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleasePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleasePrice.Caption = "1매장";
            this.gcReleasePrice.FieldName = "PRICE";
            this.gcReleasePrice.MaxWidth = 80;
            this.gcReleasePrice.MinWidth = 60;
            this.gcReleasePrice.Name = "gcReleasePrice";
            this.gcReleasePrice.OptionsColumn.AllowEdit = false;
            this.gcReleasePrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SALE_PRICE", "{0:n0}")});
            this.gcReleasePrice.Width = 80;
            // 
            // gcMarginCost
            // 
            this.gcMarginCost.AppearanceCell.Options.UseTextOptions = true;
            this.gcMarginCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcMarginCost.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcMarginCost.AppearanceHeader.Options.UseBackColor = true;
            this.gcMarginCost.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMarginCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMarginCost.Caption = "2매장";
            this.gcMarginCost.FieldName = "DELIVERY_CNT";
            this.gcMarginCost.MaxWidth = 80;
            this.gcMarginCost.MinWidth = 60;
            this.gcMarginCost.Name = "gcMarginCost";
            this.gcMarginCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MARGIN_COST", "{0:n0}")});
            this.gcMarginCost.Width = 80;
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
            this.gcSalePrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SALE_PRICE", "{0:N0}")});
            this.gcSalePrice.Width = 80;
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
            this.gridColumn2.VisibleIndex = 9;
            this.gridColumn2.Width = 352;
            // 
            // riteTitle
            // 
            this.riteTitle.AutoHeight = false;
            this.riteTitle.Name = "riteTitle";
            this.riteTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riteTitle_KeyDown);
            // 
            // usrBookOrderResultList
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcList);
            this.Name = "usrBookOrderResultList";
            this.Size = new System.Drawing.Size(1019, 502);
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
    }
}
