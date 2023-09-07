namespace WareHousingMaster.view.kbooks.search.common
{
    partial class usrBookSalePerformance
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
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCpu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReceiptCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleaseCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileComponentCd = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rileMemCapacity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.risleCpu = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rileStgType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rileStgCapacity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rileVgaYn = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rileOs = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rileLanguage = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rileUsedYn = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComponentCd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileMemCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.risleCpu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileStgType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileStgCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileVgaYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileOs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileUsedYn)).BeginInit();
            this.SuspendLayout();
            // 
            // gcList
            // 
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(0, 0);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileComponentCd,
            this.rileMemCapacity,
            this.risleCpu,
            this.rileStgType,
            this.rileStgCapacity,
            this.rileVgaYn,
            this.rileOs,
            this.rileLanguage,
            this.rileUsedYn});
            this.gcList.Size = new System.Drawing.Size(952, 502);
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
            this.gridColumn2,
            this.gridColumn23,
            this.gridColumn1,
            this.gcComponent,
            this.gridColumn4,
            this.gcCpu,
            this.gcStg,
            this.gcReceiptCnt,
            this.gcReleaseCnt,
            this.gcPrice,
            this.gcReleasePrice,
            this.gcDes});
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
            this.gridColumn21.VisibleIndex = 0;
            this.gridColumn21.Width = 35;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn23.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn23.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.Caption = "판매수량";
            this.gridColumn23.DisplayFormat.FormatString = "N0";
            this.gridColumn23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn23.FieldName = "SALE_COUNT";
            this.gridColumn23.MaxWidth = 80;
            this.gridColumn23.MinWidth = 80;
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.OptionsColumn.ReadOnly = true;
            this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SALE_COUNT", "{0:N0}")});
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 2;
            this.gridColumn23.Width = 80;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "매입수량";
            this.gridColumn1.DisplayFormat.FormatString = "N0";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "BUY_COUNT";
            this.gridColumn1.MaxWidth = 80;
            this.gridColumn1.MinWidth = 80;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BUY_COUNT", "{0:N0}")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 80;
            // 
            // gcComponent
            // 
            this.gcComponent.AppearanceCell.Options.UseTextOptions = true;
            this.gcComponent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcComponent.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcComponent.AppearanceHeader.Options.UseBackColor = true;
            this.gcComponent.AppearanceHeader.Options.UseTextOptions = true;
            this.gcComponent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcComponent.Caption = "반품수량";
            this.gcComponent.DisplayFormat.FormatString = "N0";
            this.gcComponent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcComponent.FieldName = "RET_COUNT";
            this.gcComponent.MaxWidth = 80;
            this.gcComponent.MinWidth = 80;
            this.gcComponent.Name = "gcComponent";
            this.gcComponent.OptionsColumn.AllowEdit = false;
            this.gcComponent.OptionsColumn.ReadOnly = true;
            this.gcComponent.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RET_COUNT", "{0:N0}")});
            this.gcComponent.Visible = true;
            this.gcComponent.VisibleIndex = 4;
            this.gcComponent.Width = 80;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "증정수량";
            this.gridColumn4.DisplayFormat.FormatString = "N0";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "PRESENT_COUNT";
            this.gridColumn4.MaxWidth = 80;
            this.gridColumn4.MinWidth = 80;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PRESENT_COUNT", "{0:N0}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 80;
            // 
            // gcCpu
            // 
            this.gcCpu.AppearanceCell.Options.UseTextOptions = true;
            this.gcCpu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcCpu.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCpu.AppearanceHeader.Options.UseBackColor = true;
            this.gcCpu.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCpu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCpu.Caption = "폐기수량";
            this.gcCpu.DisplayFormat.FormatString = "N0";
            this.gcCpu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcCpu.FieldName = "DISUSE_COUNT";
            this.gcCpu.MaxWidth = 80;
            this.gcCpu.MinWidth = 80;
            this.gcCpu.Name = "gcCpu";
            this.gcCpu.OptionsColumn.AllowEdit = false;
            this.gcCpu.OptionsColumn.ReadOnly = true;
            this.gcCpu.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DISUSE_COUNT", "{0:N0}")});
            this.gcCpu.Visible = true;
            this.gcCpu.VisibleIndex = 6;
            this.gcCpu.Width = 80;
            // 
            // gcStg
            // 
            this.gcStg.AppearanceCell.Options.UseTextOptions = true;
            this.gcStg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStg.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStg.AppearanceHeader.Options.UseBackColor = true;
            this.gcStg.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStg.Caption = "로스수량";
            this.gcStg.DisplayFormat.FormatString = "N0";
            this.gcStg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStg.FieldName = "LOSS_COUNT";
            this.gcStg.MaxWidth = 80;
            this.gcStg.MinWidth = 80;
            this.gcStg.Name = "gcStg";
            this.gcStg.OptionsColumn.AllowEdit = false;
            this.gcStg.OptionsColumn.ReadOnly = true;
            this.gcStg.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LOSS_COUNT", "{0:N0}")});
            this.gcStg.Visible = true;
            this.gcStg.VisibleIndex = 7;
            this.gcStg.Width = 80;
            // 
            // gcReceiptCnt
            // 
            this.gcReceiptCnt.AppearanceCell.Options.UseTextOptions = true;
            this.gcReceiptCnt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReceiptCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReceiptCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReceiptCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReceiptCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReceiptCnt.Caption = "특판판매수량";
            this.gcReceiptCnt.DisplayFormat.FormatString = "N0";
            this.gcReceiptCnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcReceiptCnt.FieldName = "SPEC_SALE_COUNT";
            this.gcReceiptCnt.MaxWidth = 80;
            this.gcReceiptCnt.MinWidth = 80;
            this.gcReceiptCnt.Name = "gcReceiptCnt";
            this.gcReceiptCnt.OptionsColumn.AllowEdit = false;
            this.gcReceiptCnt.OptionsColumn.ReadOnly = true;
            this.gcReceiptCnt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SPEC_SALE_COUNT", "{0:N0}")});
            this.gcReceiptCnt.Visible = true;
            this.gcReceiptCnt.VisibleIndex = 8;
            this.gcReceiptCnt.Width = 80;
            // 
            // gcReleaseCnt
            // 
            this.gcReleaseCnt.AppearanceCell.Options.UseTextOptions = true;
            this.gcReleaseCnt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReleaseCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleaseCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleaseCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleaseCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleaseCnt.Caption = "대출수량";
            this.gcReleaseCnt.DisplayFormat.FormatString = "N0";
            this.gcReleaseCnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcReleaseCnt.FieldName = "LD_OUT_COUNT";
            this.gcReleaseCnt.MaxWidth = 80;
            this.gcReleaseCnt.MinWidth = 80;
            this.gcReleaseCnt.Name = "gcReleaseCnt";
            this.gcReleaseCnt.OptionsColumn.AllowEdit = false;
            this.gcReleaseCnt.OptionsColumn.ReadOnly = true;
            this.gcReleaseCnt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LD_OUT_COUNT", "{0:N0}")});
            this.gcReleaseCnt.Visible = true;
            this.gcReleaseCnt.VisibleIndex = 9;
            this.gcReleaseCnt.Width = 80;
            // 
            // gcPrice
            // 
            this.gcPrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcPrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPrice.Caption = "대입수량";
            this.gcPrice.DisplayFormat.FormatString = "N0";
            this.gcPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcPrice.FieldName = "LD_IN_COUNT";
            this.gcPrice.MaxWidth = 80;
            this.gcPrice.MinWidth = 80;
            this.gcPrice.Name = "gcPrice";
            this.gcPrice.OptionsColumn.AllowEdit = false;
            this.gcPrice.OptionsColumn.ReadOnly = true;
            this.gcPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LD_IN_COUNT", "{0:n0}")});
            this.gcPrice.Visible = true;
            this.gcPrice.VisibleIndex = 10;
            this.gcPrice.Width = 80;
            // 
            // gcReleasePrice
            // 
            this.gcReleasePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReleasePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleasePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleasePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleasePrice.Caption = "창고수량";
            this.gcReleasePrice.DisplayFormat.FormatString = "N0";
            this.gcReleasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcReleasePrice.FieldName = "WAREHOUSE_COUNT";
            this.gcReleasePrice.MaxWidth = 80;
            this.gcReleasePrice.MinWidth = 80;
            this.gcReleasePrice.Name = "gcReleasePrice";
            this.gcReleasePrice.OptionsColumn.AllowEdit = false;
            this.gcReleasePrice.OptionsColumn.ReadOnly = true;
            this.gcReleasePrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WAREHOUSE_COUNT", "{0:n0}")});
            this.gcReleasePrice.Visible = true;
            this.gcReleasePrice.VisibleIndex = 11;
            this.gcReleasePrice.Width = 80;
            // 
            // gcDes
            // 
            this.gcDes.AppearanceCell.Options.UseTextOptions = true;
            this.gcDes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDes.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcDes.AppearanceHeader.Options.UseBackColor = true;
            this.gcDes.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDes.Caption = "기타";
            this.gcDes.FieldName = "ETC";
            this.gcDes.Name = "gcDes";
            this.gcDes.OptionsColumn.AllowEdit = false;
            this.gcDes.OptionsColumn.ReadOnly = true;
            this.gcDes.Visible = true;
            this.gcDes.VisibleIndex = 12;
            this.gcDes.Width = 80;
            // 
            // rileComponentCd
            // 
            this.rileComponentCd.AutoHeight = false;
            this.rileComponentCd.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileComponentCd.Name = "rileComponentCd";
            this.rileComponentCd.NullText = "";
            // 
            // rileMemCapacity
            // 
            this.rileMemCapacity.AutoHeight = false;
            this.rileMemCapacity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileMemCapacity.Name = "rileMemCapacity";
            this.rileMemCapacity.NullText = "";
            // 
            // risleCpu
            // 
            this.risleCpu.AutoHeight = false;
            this.risleCpu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.risleCpu.Name = "risleCpu";
            this.risleCpu.NullText = "";
            this.risleCpu.PopupView = this.repositoryItemSearchLookUpEdit3View;
            // 
            // repositoryItemSearchLookUpEdit3View
            // 
            this.repositoryItemSearchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit3View.Name = "repositoryItemSearchLookUpEdit3View";
            this.repositoryItemSearchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit3View.OptionsView.ShowGroupPanel = false;
            // 
            // rileStgType
            // 
            this.rileStgType.AutoHeight = false;
            this.rileStgType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileStgType.Name = "rileStgType";
            this.rileStgType.NullText = "";
            // 
            // rileStgCapacity
            // 
            this.rileStgCapacity.AutoHeight = false;
            this.rileStgCapacity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileStgCapacity.Name = "rileStgCapacity";
            this.rileStgCapacity.NullText = "";
            // 
            // rileVgaYn
            // 
            this.rileVgaYn.AutoHeight = false;
            this.rileVgaYn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileVgaYn.Name = "rileVgaYn";
            this.rileVgaYn.NullText = "";
            // 
            // rileOs
            // 
            this.rileOs.AutoHeight = false;
            this.rileOs.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileOs.Name = "rileOs";
            this.rileOs.NullText = "";
            // 
            // rileLanguage
            // 
            this.rileLanguage.AutoHeight = false;
            this.rileLanguage.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileLanguage.Name = "rileLanguage";
            this.rileLanguage.NullText = "";
            // 
            // rileUsedYn
            // 
            this.rileUsedYn.AutoHeight = false;
            this.rileUsedYn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileUsedYn.Name = "rileUsedYn";
            this.rileUsedYn.NullText = "";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "판매일자";
            this.gridColumn2.FieldName = "BOOK_DATE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "BOOK_DATE", "합계")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // usrBookSalePerformance
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcList);
            this.Name = "usrBookSalePerformance";
            this.Size = new System.Drawing.Size(952, 502);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComponentCd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileMemCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.risleCpu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileStgType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileStgCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileVgaYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileOs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileUsedYn)).EndInit();
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
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileComponentCd;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit risleCpu;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit3View;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileMemCapacity;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileStgType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileStgCapacity;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileVgaYn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileOs;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileLanguage;
        private DevExpress.XtraGrid.Columns.GridColumn gcReceiptCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcDes;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleaseCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcCpu;
        private DevExpress.XtraGrid.Columns.GridColumn gcStg;
        private DevExpress.XtraGrid.Columns.GridColumn gcPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleasePrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileUsedYn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}
