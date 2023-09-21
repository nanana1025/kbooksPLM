namespace WareHousingMaster.view.kbooks.search.booksearch
{
    partial class usrSaleDataList
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleaseCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMarginCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReceiptCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOrdRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileOrderRatio = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcOrderCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileCnt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCpu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileOrderRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCnt)).BeginInit();
            this.SuspendLayout();
            // 
            // gcList
            // 
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(0, 0);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileOrderRatio,
            this.rileCnt});
            this.gcList.Size = new System.Drawing.Size(1499, 502);
            this.gcList.TabIndex = 7;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            this.gcList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcList_KeyDown);
            // 
            // gvList
            // 
            this.gvList.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gvList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvList.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.Control;
            this.gvList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvList.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvList.ColumnPanelRowHeight = 22;
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gcCheck,
            this.gridColumn21,
            this.gcReleaseCnt,
            this.gridColumn12,
            this.gridColumn23,
            this.gcComponent,
            this.gridColumn5,
            this.gcReleasePrice,
            this.gcMarginCost,
            this.gcSalePrice,
            this.gcReceiptCnt,
            this.gcStg,
            this.gridColumn1,
            this.gcDes,
            this.gcOrdRate,
            this.gcOrderCnt,
            this.gridColumn11,
            this.gridColumn10,
            this.gridColumn9,
            this.gridColumn8,
            this.gridColumn7,
            this.gridColumn3,
            this.gcCpu,
            this.gridColumn2});
            this.gvList.GridControl = this.gcList;
            this.gvList.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvList.Name = "gvList";
            this.gvList.OptionsPrint.AutoWidth = false;
            this.gvList.OptionsView.ShowFooter = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.RowHeight = 22;
            this.gvList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvList_RowCellStyle);
            this.gvList.ShownEditor += new System.EventHandler(this.gvList_ShownEditor);
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
            this.gcCheck.Width = 35;
            // 
            // gridColumn21
            // 
            this.gridColumn21.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn21.AppearanceCell.Options.UseFont = true;
            this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn21.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn21.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn21.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn21.AppearanceHeader.Options.UseFont = true;
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
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 0;
            this.gridColumn21.Width = 45;
            // 
            // gcReleaseCnt
            // 
            this.gcReleaseCnt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReleaseCnt.AppearanceCell.Options.UseFont = true;
            this.gcReleaseCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleaseCnt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReleaseCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleaseCnt.AppearanceHeader.Options.UseFont = true;
            this.gcReleaseCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleaseCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleaseCnt.Caption = "매입처명";
            this.gcReleaseCnt.FieldName = "PURCHNM";
            this.gcReleaseCnt.MinWidth = 100;
            this.gcReleaseCnt.Name = "gcReleaseCnt";
            this.gcReleaseCnt.OptionsColumn.AllowEdit = false;
            this.gcReleaseCnt.Visible = true;
            this.gcReleaseCnt.VisibleIndex = 1;
            this.gcReleaseCnt.Width = 100;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn12.AppearanceCell.Options.UseFont = true;
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn12.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn12.AppearanceHeader.Options.UseFont = true;
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "매장코드";
            this.gridColumn12.FieldName = "STORECD";
            this.gridColumn12.MaxWidth = 70;
            this.gridColumn12.MinWidth = 60;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            this.gridColumn12.Width = 70;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn23.AppearanceCell.Options.UseFont = true;
            this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn23.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn23.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn23.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn23.AppearanceHeader.Options.UseFont = true;
            this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.Caption = "도서명";
            this.gridColumn23.FieldName = "BOOKNM";
            this.gridColumn23.MinWidth = 150;
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "COMPONENT_CD", "{0:N0}")});
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 3;
            this.gridColumn23.Width = 200;
            // 
            // gcComponent
            // 
            this.gcComponent.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcComponent.AppearanceCell.Options.UseFont = true;
            this.gcComponent.AppearanceCell.Options.UseTextOptions = true;
            this.gcComponent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcComponent.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcComponent.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcComponent.AppearanceHeader.Options.UseBackColor = true;
            this.gcComponent.AppearanceHeader.Options.UseFont = true;
            this.gcComponent.AppearanceHeader.Options.UseTextOptions = true;
            this.gcComponent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcComponent.Caption = "저자명";
            this.gcComponent.FieldName = "AUTHORNM";
            this.gcComponent.MinWidth = 80;
            this.gcComponent.Name = "gcComponent";
            this.gcComponent.OptionsColumn.AllowEdit = false;
            this.gcComponent.Visible = true;
            this.gcComponent.VisibleIndex = 5;
            this.gcComponent.Width = 81;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "거래조건";
            this.gridColumn5.FieldName = "TRADE_ITEM";
            this.gridColumn5.MinWidth = 60;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Width = 60;
            // 
            // gcReleasePrice
            // 
            this.gcReleasePrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReleasePrice.AppearanceCell.Options.UseFont = true;
            this.gcReleasePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReleasePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleasePrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReleasePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleasePrice.AppearanceHeader.Options.UseFont = true;
            this.gcReleasePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleasePrice.Caption = "정가";
            this.gcReleasePrice.DisplayFormat.FormatString = "n0";
            this.gcReleasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcReleasePrice.FieldName = "PRICE";
            this.gcReleasePrice.MaxWidth = 80;
            this.gcReleasePrice.MinWidth = 60;
            this.gcReleasePrice.Name = "gcReleasePrice";
            this.gcReleasePrice.OptionsColumn.AllowEdit = false;
            this.gcReleasePrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PRICE", "{0:n0}")});
            this.gcReleasePrice.Visible = true;
            this.gcReleasePrice.VisibleIndex = 6;
            this.gcReleasePrice.Width = 60;
            // 
            // gcMarginCost
            // 
            this.gcMarginCost.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcMarginCost.AppearanceCell.Options.UseFont = true;
            this.gcMarginCost.AppearanceCell.Options.UseTextOptions = true;
            this.gcMarginCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcMarginCost.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcMarginCost.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcMarginCost.AppearanceHeader.Options.UseBackColor = true;
            this.gcMarginCost.AppearanceHeader.Options.UseFont = true;
            this.gcMarginCost.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMarginCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMarginCost.Caption = "납품예정수량";
            this.gcMarginCost.DisplayFormat.FormatString = "n0";
            this.gcMarginCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcMarginCost.FieldName = "ESTI_SUM";
            this.gcMarginCost.MaxWidth = 80;
            this.gcMarginCost.MinWidth = 90;
            this.gcMarginCost.Name = "gcMarginCost";
            this.gcMarginCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ESTI_SUM", "{0:n0}")});
            this.gcMarginCost.Visible = true;
            this.gcMarginCost.VisibleIndex = 7;
            this.gcMarginCost.Width = 90;
            // 
            // gcSalePrice
            // 
            this.gcSalePrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcSalePrice.AppearanceCell.Options.UseFont = true;
            this.gcSalePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcSalePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcSalePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcSalePrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcSalePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcSalePrice.AppearanceHeader.Options.UseFont = true;
            this.gcSalePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcSalePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcSalePrice.Caption = "반품예정수량";
            this.gcSalePrice.DisplayFormat.FormatString = "n0";
            this.gcSalePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcSalePrice.FieldName = "RET_SUM";
            this.gcSalePrice.MaxWidth = 80;
            this.gcSalePrice.MinWidth = 90;
            this.gcSalePrice.Name = "gcSalePrice";
            this.gcSalePrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RET_SUM", "{0:N0}")});
            this.gcSalePrice.Visible = true;
            this.gcSalePrice.VisibleIndex = 8;
            this.gcSalePrice.Width = 90;
            // 
            // gcReceiptCnt
            // 
            this.gcReceiptCnt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReceiptCnt.AppearanceCell.Options.UseFont = true;
            this.gcReceiptCnt.AppearanceCell.Options.UseTextOptions = true;
            this.gcReceiptCnt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReceiptCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReceiptCnt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReceiptCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReceiptCnt.AppearanceHeader.Options.UseFont = true;
            this.gcReceiptCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReceiptCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReceiptCnt.Caption = "주문예정수량";
            this.gcReceiptCnt.DisplayFormat.FormatString = "n0";
            this.gcReceiptCnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcReceiptCnt.FieldName = "ORD_SUM";
            this.gcReceiptCnt.MaxWidth = 90;
            this.gcReceiptCnt.MinWidth = 80;
            this.gcReceiptCnt.Name = "gcReceiptCnt";
            this.gcReceiptCnt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ORD_SUM", "{0:N0}")});
            this.gcReceiptCnt.Visible = true;
            this.gcReceiptCnt.VisibleIndex = 9;
            this.gcReceiptCnt.Width = 81;
            // 
            // gcStg
            // 
            this.gcStg.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcStg.AppearanceCell.Options.UseFont = true;
            this.gcStg.AppearanceCell.Options.UseTextOptions = true;
            this.gcStg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStg.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStg.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcStg.AppearanceHeader.Options.UseBackColor = true;
            this.gcStg.AppearanceHeader.Options.UseFont = true;
            this.gcStg.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStg.Caption = "입고예정수량";
            this.gcStg.DisplayFormat.FormatString = "n0";
            this.gcStg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStg.FieldName = "INP_SUM";
            this.gcStg.MaxWidth = 90;
            this.gcStg.MinWidth = 80;
            this.gcStg.Name = "gcStg";
            this.gcStg.OptionsColumn.AllowEdit = false;
            this.gcStg.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "INP_SUM", "{0:n0}")});
            this.gcStg.Visible = true;
            this.gcStg.VisibleIndex = 10;
            this.gcStg.Width = 81;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "판매수량";
            this.gridColumn1.DisplayFormat.FormatString = "n0";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "SALE_SUM";
            this.gridColumn1.MaxWidth = 70;
            this.gridColumn1.MinWidth = 60;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SALE_SUM", "{0:n0}")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 11;
            this.gridColumn1.Width = 60;
            // 
            // gcDes
            // 
            this.gcDes.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcDes.AppearanceCell.Options.UseFont = true;
            this.gcDes.AppearanceCell.Options.UseTextOptions = true;
            this.gcDes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDes.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcDes.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcDes.AppearanceHeader.Options.UseBackColor = true;
            this.gcDes.AppearanceHeader.Options.UseFont = true;
            this.gcDes.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDes.Caption = "현재고";
            this.gcDes.DisplayFormat.FormatString = "n0";
            this.gcDes.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcDes.FieldName = "STOCK";
            this.gcDes.MaxWidth = 70;
            this.gcDes.MinWidth = 40;
            this.gcDes.Name = "gcDes";
            this.gcDes.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STOCK", "{0:n0}")});
            this.gcDes.Visible = true;
            this.gcDes.VisibleIndex = 12;
            this.gcDes.Width = 51;
            // 
            // gcOrdRate
            // 
            this.gcOrdRate.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gcOrdRate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcOrdRate.AppearanceCell.Options.UseBackColor = true;
            this.gcOrdRate.AppearanceCell.Options.UseFont = true;
            this.gcOrdRate.AppearanceCell.Options.UseTextOptions = true;
            this.gcOrdRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcOrdRate.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcOrdRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcOrdRate.AppearanceHeader.Options.UseBackColor = true;
            this.gcOrdRate.AppearanceHeader.Options.UseFont = true;
            this.gcOrdRate.AppearanceHeader.Options.UseTextOptions = true;
            this.gcOrdRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcOrdRate.Caption = "주문 매입률";
            this.gcOrdRate.ColumnEdit = this.rileOrderRatio;
            this.gcOrdRate.DisplayFormat.FormatString = "n1";
            this.gcOrdRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcOrdRate.FieldName = "ORD_RATE";
            this.gcOrdRate.MaxWidth = 100;
            this.gcOrdRate.MinWidth = 70;
            this.gcOrdRate.Name = "gcOrdRate";
            this.gcOrdRate.Visible = true;
            this.gcOrdRate.VisibleIndex = 13;
            this.gcOrdRate.Width = 73;
            // 
            // rileOrderRatio
            // 
            this.rileOrderRatio.AutoHeight = false;
            this.rileOrderRatio.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileOrderRatio.Name = "rileOrderRatio";
            this.rileOrderRatio.NullText = "";
            // 
            // gcOrderCnt
            // 
            this.gcOrderCnt.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gcOrderCnt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcOrderCnt.AppearanceCell.Options.UseBackColor = true;
            this.gcOrderCnt.AppearanceCell.Options.UseFont = true;
            this.gcOrderCnt.AppearanceCell.Options.UseTextOptions = true;
            this.gcOrderCnt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcOrderCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcOrderCnt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcOrderCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcOrderCnt.AppearanceHeader.Options.UseFont = true;
            this.gcOrderCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcOrderCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcOrderCnt.Caption = "주문수량";
            this.gcOrderCnt.ColumnEdit = this.rileCnt;
            this.gcOrderCnt.DisplayFormat.FormatString = "n0";
            this.gcOrderCnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcOrderCnt.FieldName = "ORDER_CNT";
            this.gcOrderCnt.MaxWidth = 100;
            this.gcOrderCnt.MinWidth = 70;
            this.gcOrderCnt.Name = "gcOrderCnt";
            this.gcOrderCnt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ORDER_CNT", "{0:n0}")});
            this.gcOrderCnt.Visible = true;
            this.gcOrderCnt.VisibleIndex = 14;
            this.gcOrderCnt.Width = 70;
            // 
            // rileCnt
            // 
            this.rileCnt.AutoHeight = false;
            this.rileCnt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.rileCnt.IsFloatValue = false;
            this.rileCnt.MaskSettings.Set("mask", "N00");
            this.rileCnt.Name = "rileCnt";
            this.rileCnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rileCnt_KeyDown);
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn11.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "부코드";
            this.gridColumn11.FieldName = "DEPTCD";
            this.gridColumn11.MinWidth = 60;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Width = 62;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn10.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "도서코드";
            this.gridColumn10.FieldName = "BOOKCD";
            this.gridColumn10.MinWidth = 80;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            this.gridColumn10.Width = 80;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn9.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "주문수량유무";
            this.gridColumn9.FieldName = "ORDER_CNT_YN";
            this.gridColumn9.MinWidth = 75;
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn8.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "조코드";
            this.gridColumn8.FieldName = "GROUPCD";
            this.gridColumn8.MinWidth = 60;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 60;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn7.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "서가코드";
            this.gridColumn7.FieldName = "STANDCD";
            this.gridColumn7.MinWidth = 60;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 60;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "주문담당조";
            this.gridColumn3.FieldName = "ORD_GROUPCD";
            this.gridColumn3.MinWidth = 60;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 61;
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
            this.gcCpu.FieldName = "BOOK_CD";
            this.gcCpu.Name = "gcCpu";
            this.gcCpu.OptionsColumn.AllowEdit = false;
            this.gcCpu.Width = 55;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "기타";
            this.gridColumn2.FieldName = "ORDER_CNT_HIDE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 15;
            this.gridColumn2.Width = 60;
            // 
            // usrSaleDataList
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcList);
            this.Name = "usrSaleDataList";
            this.Size = new System.Drawing.Size(1499, 502);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileOrderRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCnt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gcOrderCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gcComponent;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gcReceiptCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcDes;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleaseCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcCpu;
        private DevExpress.XtraGrid.Columns.GridColumn gcStg;
        private DevExpress.XtraGrid.Columns.GridColumn gcOrdRate;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleasePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcMarginCost;
        private DevExpress.XtraGrid.Columns.GridColumn gcSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileOrderRatio;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rileCnt;
    }
}
