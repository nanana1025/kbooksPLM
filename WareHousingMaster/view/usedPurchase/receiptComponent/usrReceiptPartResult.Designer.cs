namespace WareHousingMaster.view.usedPurchase.receiptComponent
{
    partial class usrReceiptPartResult
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
            this.gcPart = new DevExpress.XtraGrid.GridControl();
            this.gvPart = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gbReceipt = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rileComponentCd = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gc1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riseCnt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gc2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbExam = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcCompare = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rileCompare = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bgcInventoryCat = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rileInventoryCat = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bgcDes = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcAdjustPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riseAdjustPrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.bgcTotalPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcCheck = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riicbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.riicbProductState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComponentCd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryCat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseAdjustPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbProductState)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPart
            // 
            this.gcPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPart.Location = new System.Drawing.Point(0, 0);
            this.gcPart.MainView = this.gvPart;
            this.gcPart.Name = "gcPart";
            this.gcPart.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.riicbState,
            this.riicbProductState,
            this.riseCnt,
            this.rileComponentCd,
            this.rileInventoryCat,
            this.rileCompare,
            this.riseAdjustPrice});
            this.gcPart.Size = new System.Drawing.Size(957, 502);
            this.gcPart.TabIndex = 6;
            this.gcPart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPart});
            // 
            // gvPart
            // 
            this.gvPart.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gbReceipt,
            this.gbExam});
            this.gvPart.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumn4,
            this.gridColumn12,
            this.gc1,
            this.gridColumn9,
            this.gc2,
            this.bandedGridColumn2,
            this.bandedGridColumn1,
            this.gridColumn11,
            this.bandedGridColumn8,
            this.bandedGridColumn7,
            this.bgcCompare,
            this.bgcInventoryCat,
            this.bgcDes,
            this.bgcPrice,
            this.bgcAdjustPrice,
            this.bgcTotalPrice,
            this.bgcCheck,
            this.bandedGridColumn3});
            this.gvPart.GridControl = this.gcPart;
            this.gvPart.Name = "gvPart";
            this.gvPart.OptionsView.ShowAutoFilterRow = true;
            this.gvPart.OptionsView.ShowFooter = true;
            this.gvPart.OptionsView.ShowGroupPanel = false;
            this.gvPart.OptionsView.ShowIndicator = false;
            this.gvPart.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPart_RowCellStyle);
            this.gvPart.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvPart_FocusedRowChanged);
            this.gvPart.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvPart_FocusedRowObjectChanged);
            this.gvPart.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvPart_CellValueChanged);
            // 
            // gbReceipt
            // 
            this.gbReceipt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gbReceipt.AppearanceHeader.Options.UseBackColor = true;
            this.gbReceipt.AppearanceHeader.Options.UseTextOptions = true;
            this.gbReceipt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gbReceipt.Caption = "신청현황";
            this.gbReceipt.Columns.Add(this.gridColumn4);
            this.gbReceipt.Columns.Add(this.gridColumn12);
            this.gbReceipt.Columns.Add(this.gc1);
            this.gbReceipt.Columns.Add(this.gridColumn9);
            this.gbReceipt.Columns.Add(this.gc2);
            this.gbReceipt.Columns.Add(this.bandedGridColumn2);
            this.gbReceipt.Name = "gbReceipt";
            this.gbReceipt.VisibleIndex = 0;
            this.gbReceipt.Width = 285;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "INVENTORY_ID";
            this.gridColumn4.FieldName = "INVENTORY_ID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Tag = "";
            this.gridColumn4.Width = 32;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.AppearanceHeader.BackColor = System.Drawing.Color.Silver;
            this.gridColumn12.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "품목명";
            this.gridColumn12.ColumnEdit = this.rileComponentCd;
            this.gridColumn12.FieldName = "RECEIPT_COMPONENT_CD";
            this.gridColumn12.MinWidth = 40;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.Width = 41;
            // 
            // rileComponentCd
            // 
            this.rileComponentCd.AutoHeight = false;
            this.rileComponentCd.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileComponentCd.Name = "rileComponentCd";
            this.rileComponentCd.NullText = "";
            // 
            // gc1
            // 
            this.gc1.AppearanceCell.Options.UseTextOptions = true;
            this.gc1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gc1.AppearanceHeader.BackColor = System.Drawing.Color.Silver;
            this.gc1.AppearanceHeader.Options.UseBackColor = true;
            this.gc1.AppearanceHeader.Options.UseTextOptions = true;
            this.gc1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc1.Caption = "부품명";
            this.gc1.FieldName = "RECEIPT_MODEL_NM";
            this.gc1.Name = "gc1";
            this.gc1.OptionsColumn.ReadOnly = true;
            this.gc1.Visible = true;
            this.gc1.Width = 63;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn9.AppearanceHeader.BackColor = System.Drawing.Color.Silver;
            this.gridColumn9.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "수량";
            this.gridColumn9.ColumnEdit = this.riseCnt;
            this.gridColumn9.DisplayFormat.FormatString = "n0";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "RECEIPT_PART_CNT";
            this.gridColumn9.MinWidth = 40;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RECEIPT_PART_CNT", "{0:N0}")});
            this.gridColumn9.Visible = true;
            this.gridColumn9.Width = 41;
            // 
            // riseCnt
            // 
            this.riseCnt.AutoHeight = false;
            this.riseCnt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riseCnt.Name = "riseCnt";
            this.riseCnt.EditValueChanged += new System.EventHandler(this.riseCnt_EditValueChanged);
            // 
            // gc2
            // 
            this.gc2.AppearanceCell.Options.UseTextOptions = true;
            this.gc2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gc2.AppearanceHeader.BackColor = System.Drawing.Color.Silver;
            this.gc2.AppearanceHeader.Options.UseBackColor = true;
            this.gc2.AppearanceHeader.Options.UseTextOptions = true;
            this.gc2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc2.Caption = "금액";
            this.gc2.DisplayFormat.FormatString = "n0";
            this.gc2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc2.FieldName = "RECEIPT_PART_PRICE";
            this.gc2.MinWidth = 40;
            this.gc2.Name = "gc2";
            this.gc2.OptionsColumn.AllowEdit = false;
            this.gc2.OptionsColumn.ReadOnly = true;
            this.gc2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RECEIPT_PART_PRICE", "{0:n0}")});
            this.gc2.Visible = true;
            this.gc2.Width = 41;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.bandedGridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.Silver;
            this.bandedGridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.bandedGridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn2.Caption = "총 금액";
            this.bandedGridColumn2.DisplayFormat.FormatString = "N0";
            this.bandedGridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn2.FieldName = "RECEIPT_TOTAL_PRICE";
            this.bandedGridColumn2.MinWidth = 50;
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn2.OptionsColumn.ReadOnly = true;
            this.bandedGridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RECEIPT_TOTAL_PRICE", "{0:N0}")});
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 99;
            // 
            // gbExam
            // 
            this.gbExam.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gbExam.AppearanceHeader.Options.UseBackColor = true;
            this.gbExam.AppearanceHeader.Options.UseTextOptions = true;
            this.gbExam.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gbExam.Caption = "검수현황";
            this.gbExam.Columns.Add(this.bandedGridColumn1);
            this.gbExam.Columns.Add(this.gridColumn11);
            this.gbExam.Columns.Add(this.bandedGridColumn8);
            this.gbExam.Columns.Add(this.bandedGridColumn7);
            this.gbExam.Columns.Add(this.bgcCompare);
            this.gbExam.Columns.Add(this.bgcInventoryCat);
            this.gbExam.Columns.Add(this.bgcDes);
            this.gbExam.Columns.Add(this.bgcPrice);
            this.gbExam.Columns.Add(this.bgcAdjustPrice);
            this.gbExam.Columns.Add(this.bgcTotalPrice);
            this.gbExam.Columns.Add(this.bgcCheck);
            this.gbExam.Columns.Add(this.bandedGridColumn3);
            this.gbExam.Name = "gbExam";
            this.gbExam.VisibleIndex = 1;
            this.gbExam.Width = 670;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bandedGridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.bandedGridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn1.Caption = "품목명";
            this.bandedGridColumn1.ColumnEdit = this.rileComponentCd;
            this.bandedGridColumn1.FieldName = "EXAM_COMPONENT_CD";
            this.bandedGridColumn1.MinWidth = 40;
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn1.OptionsColumn.ReadOnly = true;
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 40;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn11.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "부품코드";
            this.gridColumn11.FieldName = "COMPONENT";
            this.gridColumn11.MinWidth = 40;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.ReadOnly = true;
            this.gridColumn11.Visible = true;
            this.gridColumn11.Width = 40;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.bandedGridColumn8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bandedGridColumn8.AppearanceHeader.Options.UseBackColor = true;
            this.bandedGridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn8.Caption = "부품명";
            this.bandedGridColumn8.FieldName = "EXAM_MODEL_NM";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.OptionsColumn.ReadOnly = true;
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.Width = 215;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.bandedGridColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bandedGridColumn7.AppearanceHeader.Options.UseBackColor = true;
            this.bandedGridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumn7.Caption = "수량";
            this.bandedGridColumn7.DisplayFormat.FormatString = "N0";
            this.bandedGridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn7.FieldName = "EXAM_PART_CNT";
            this.bandedGridColumn7.MinWidth = 30;
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "EXAM_PART_CNT", "{0:N0}")});
            this.bandedGridColumn7.Visible = true;
            this.bandedGridColumn7.Width = 39;
            // 
            // bgcCompare
            // 
            this.bgcCompare.AppearanceCell.Options.UseTextOptions = true;
            this.bgcCompare.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcCompare.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bgcCompare.AppearanceHeader.Options.UseBackColor = true;
            this.bgcCompare.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcCompare.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcCompare.Caption = "비교";
            this.bgcCompare.ColumnEdit = this.rileCompare;
            this.bgcCompare.FieldName = "COMPARE";
            this.bgcCompare.MinWidth = 30;
            this.bgcCompare.Name = "bgcCompare";
            this.bgcCompare.OptionsColumn.AllowEdit = false;
            this.bgcCompare.Visible = true;
            this.bgcCompare.Width = 37;
            // 
            // rileCompare
            // 
            this.rileCompare.AutoHeight = false;
            this.rileCompare.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileCompare.Name = "rileCompare";
            this.rileCompare.NullText = "";
            // 
            // bgcInventoryCat
            // 
            this.bgcInventoryCat.AppearanceCell.Options.UseTextOptions = true;
            this.bgcInventoryCat.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcInventoryCat.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bgcInventoryCat.AppearanceHeader.Options.UseBackColor = true;
            this.bgcInventoryCat.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcInventoryCat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcInventoryCat.Caption = "검수결과";
            this.bgcInventoryCat.ColumnEdit = this.rileInventoryCat;
            this.bgcInventoryCat.FieldName = "INVENTORY_CAT";
            this.bgcInventoryCat.MinWidth = 40;
            this.bgcInventoryCat.Name = "bgcInventoryCat";
            this.bgcInventoryCat.Visible = true;
            this.bgcInventoryCat.Width = 40;
            // 
            // rileInventoryCat
            // 
            this.rileInventoryCat.AutoHeight = false;
            this.rileInventoryCat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileInventoryCat.Name = "rileInventoryCat";
            this.rileInventoryCat.NullText = "";
            // 
            // bgcDes
            // 
            this.bgcDes.AppearanceCell.Options.UseTextOptions = true;
            this.bgcDes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.bgcDes.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bgcDes.AppearanceHeader.Options.UseBackColor = true;
            this.bgcDes.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcDes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcDes.Caption = "검수내역";
            this.bgcDes.FieldName = "DES";
            this.bgcDes.Name = "bgcDes";
            this.bgcDes.Visible = true;
            this.bgcDes.Width = 84;
            // 
            // bgcPrice
            // 
            this.bgcPrice.AppearanceCell.Options.UseTextOptions = true;
            this.bgcPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.bgcPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bgcPrice.AppearanceHeader.Options.UseBackColor = true;
            this.bgcPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcPrice.Caption = "매입기준금액";
            this.bgcPrice.ColumnEdit = this.riseCnt;
            this.bgcPrice.DisplayFormat.FormatString = "n0";
            this.bgcPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bgcPrice.FieldName = "EXAM_PART_PRICE";
            this.bgcPrice.Name = "bgcPrice";
            this.bgcPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "EXAM_PART_PRICE", "{0:n0}")});
            this.bgcPrice.Visible = true;
            this.bgcPrice.Width = 42;
            // 
            // bgcAdjustPrice
            // 
            this.bgcAdjustPrice.AppearanceCell.Options.UseTextOptions = true;
            this.bgcAdjustPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.bgcAdjustPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bgcAdjustPrice.AppearanceHeader.Options.UseBackColor = true;
            this.bgcAdjustPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcAdjustPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcAdjustPrice.Caption = "조정금액";
            this.bgcAdjustPrice.ColumnEdit = this.riseAdjustPrice;
            this.bgcAdjustPrice.DisplayFormat.FormatString = "N0";
            this.bgcAdjustPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bgcAdjustPrice.FieldName = "EXAM_ADJUST_PRICE";
            this.bgcAdjustPrice.Name = "bgcAdjustPrice";
            this.bgcAdjustPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "EXAM_ADJUST_PRICE", "{0:N0}")});
            this.bgcAdjustPrice.Visible = true;
            this.bgcAdjustPrice.Width = 44;
            // 
            // riseAdjustPrice
            // 
            this.riseAdjustPrice.AutoHeight = false;
            this.riseAdjustPrice.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riseAdjustPrice.Name = "riseAdjustPrice";
            this.riseAdjustPrice.EditValueChanged += new System.EventHandler(this.riseAdjustPrice_EditValueChanged);
            // 
            // bgcTotalPrice
            // 
            this.bgcTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.bgcTotalPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.bgcTotalPrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bgcTotalPrice.AppearanceHeader.Options.UseBackColor = true;
            this.bgcTotalPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcTotalPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcTotalPrice.Caption = "실 매입금액";
            this.bgcTotalPrice.DisplayFormat.FormatString = "N0";
            this.bgcTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bgcTotalPrice.FieldName = "EXAM_TOTAL_PRICE";
            this.bgcTotalPrice.Name = "bgcTotalPrice";
            this.bgcTotalPrice.OptionsColumn.AllowEdit = false;
            this.bgcTotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "EXAM_TOTAL_PRICE", "{0:N0}")});
            this.bgcTotalPrice.Visible = true;
            this.bgcTotalPrice.Width = 45;
            // 
            // bgcCheck
            // 
            this.bgcCheck.AppearanceCell.Options.UseTextOptions = true;
            this.bgcCheck.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcCheck.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bgcCheck.AppearanceHeader.Options.UseBackColor = true;
            this.bgcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcCheck.Caption = "반송체크";
            this.bgcCheck.FieldName = "CHECK";
            this.bgcCheck.MinWidth = 10;
            this.bgcCheck.Name = "bgcCheck";
            this.bgcCheck.Visible = true;
            this.bgcCheck.Width = 44;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "S";
            this.bandedGridColumn3.FieldName = "STATE";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // riicbState
            // 
            this.riicbState.AutoHeight = false;
            this.riicbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicbState.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riicbState.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", false, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0)});
            this.riicbState.Name = "riicbState";
            // 
            // riicbProductState
            // 
            this.riicbProductState.AutoHeight = false;
            this.riicbProductState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicbProductState.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riicbProductState.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", false, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 1)});
            this.riicbProductState.Name = "riicbProductState";
            // 
            // usrReceiptPartResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcPart);
            this.Name = "usrReceiptPartResult";
            this.Size = new System.Drawing.Size(957, 502);
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComponentCd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryCat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseAdjustPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbProductState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPart;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileComponentCd;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseCnt;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbProductState;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvPart;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn12;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gc1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gc2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcInventoryCat;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileInventoryCat;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcDes;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcAdjustPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcTotalPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcCheck;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcCompare;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileCompare;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseAdjustPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbReceipt;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbExam;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
    }
}
