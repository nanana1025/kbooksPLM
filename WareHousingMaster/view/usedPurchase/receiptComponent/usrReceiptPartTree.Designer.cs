﻿namespace WareHousingMaster.view.usedPurchase.receiptComponent
{
    partial class usrReceiptPartTree
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
            this.tlPart = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn15 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn14 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcCompareCnt = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rileCompare = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.tlcComparePart = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlExamineResult = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rileExamineResult = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.tlExamineDetail = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.riteExamineDetail = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.tlPurchaseCost = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.risePurchaseCost = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.tlAdjustCost = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rileAdjustCost = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.tlTotalCost = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcUpdate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ribeUpdate = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tlPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileExamineResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteExamineDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.risePurchaseCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileAdjustCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribeUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // tlPart
            // 
            this.tlPart.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn5,
            this.treeListColumn6,
            this.treeListColumn15,
            this.treeListColumn14,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn7,
            this.treeListColumn1,
            this.treeListColumn4,
            this.treeListColumn9,
            this.treeListColumn8,
            this.tlcCompareCnt,
            this.tlcComparePart,
            this.tlExamineResult,
            this.tlExamineDetail,
            this.tlPurchaseCost,
            this.tlAdjustCost,
            this.tlTotalCost,
            this.tlcUpdate});
            this.tlPart.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPart.KeyFieldName = "PART_ID";
            this.tlPart.Location = new System.Drawing.Point(0, 0);
            this.tlPart.Name = "tlPart";
            this.tlPart.OptionsView.ShowIndicator = false;
            this.tlPart.OptionsView.ShowSummaryFooter = true;
            this.tlPart.ParentFieldName = "P_PART_ID";
            this.tlPart.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileCompare,
            this.rileExamineResult,
            this.riteExamineDetail,
            this.risePurchaseCost,
            this.rileAdjustCost,
            this.ribeUpdate});
            this.tlPart.Size = new System.Drawing.Size(847, 502);
            this.tlPart.TabIndex = 5;
            this.tlPart.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tlPart_NodeCellStyle);
            this.tlPart.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tlPart_FocusedNodeChanged);
            this.tlPart.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.tlPart_CellValueChanged);
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "COMPONENT_ID";
            this.treeListColumn5.FieldName = "COMPONENT_ID";
            this.treeListColumn5.Name = "treeListColumn5";
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "INVENTORY_ID";
            this.treeListColumn6.FieldName = "INVENTORY_ID";
            this.treeListColumn6.Name = "treeListColumn6";
            // 
            // treeListColumn15
            // 
            this.treeListColumn15.Caption = "P_PART_ID";
            this.treeListColumn15.FieldName = "P_PART_ID";
            this.treeListColumn15.Name = "treeListColumn15";
            // 
            // treeListColumn14
            // 
            this.treeListColumn14.Caption = "PART_ID";
            this.treeListColumn14.FieldName = "PART_ID";
            this.treeListColumn14.Name = "treeListColumn14";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeListColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn2.Caption = "품목명";
            this.treeListColumn2.FieldName = "COMPONENT_CD";
            this.treeListColumn2.MaxWidth = 100;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 59;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.treeListColumn3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeListColumn3.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn3.Caption = "접수 모델명";
            this.treeListColumn3.FieldName = "RECEIPT_MODEL_NM";
            this.treeListColumn3.MaxWidth = 400;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.ReadOnly = true;
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            this.treeListColumn3.Width = 63;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeListColumn7.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn7.Caption = "부품개수";
            this.treeListColumn7.FieldName = "RECEIPT_PART_CNT";
            this.treeListColumn7.Format.FormatString = "n0";
            this.treeListColumn7.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn7.MaxWidth = 55;
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.OptionsColumn.AllowEdit = false;
            this.treeListColumn7.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.treeListColumn7.SummaryFooterStrFormat = "{0:N0}";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 2;
            this.treeListColumn7.Width = 44;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeListColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn1.Caption = "신청금액";
            this.treeListColumn1.FieldName = "RECEIPT_COST";
            this.treeListColumn1.Format.FormatString = "n0";
            this.treeListColumn1.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn1.MaxWidth = 100;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.treeListColumn1.SummaryFooterStrFormat = "{0:n0}";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 3;
            this.treeListColumn1.Width = 64;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeListColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn4.Caption = "부품명";
            this.treeListColumn4.FieldName = "COMPONENT";
            this.treeListColumn4.MaxWidth = 100;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.AllowEdit = false;
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 4;
            this.treeListColumn4.Width = 89;
            // 
            // treeListColumn9
            // 
            this.treeListColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.treeListColumn9.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeListColumn9.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn9.Caption = "검수 모델명";
            this.treeListColumn9.FieldName = "EXAMINE_MODEL_NM";
            this.treeListColumn9.Name = "treeListColumn9";
            this.treeListColumn9.OptionsColumn.ReadOnly = true;
            this.treeListColumn9.Visible = true;
            this.treeListColumn9.VisibleIndex = 5;
            this.treeListColumn9.Width = 60;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeListColumn8.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn8.Caption = "부품개수";
            this.treeListColumn8.FieldName = "EXAMINE_PART_CNT";
            this.treeListColumn8.Format.FormatString = "n0";
            this.treeListColumn8.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn8.MaxWidth = 55;
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.OptionsColumn.AllowEdit = false;
            this.treeListColumn8.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.treeListColumn8.SummaryFooterStrFormat = "{0:N0}";
            this.treeListColumn8.Visible = true;
            this.treeListColumn8.VisibleIndex = 6;
            this.treeListColumn8.Width = 44;
            // 
            // tlcCompareCnt
            // 
            this.tlcCompareCnt.AppearanceCell.Options.UseTextOptions = true;
            this.tlcCompareCnt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlcCompareCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tlcCompareCnt.AppearanceHeader.Options.UseBackColor = true;
            this.tlcCompareCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.tlcCompareCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlcCompareCnt.Caption = "수량비교";
            this.tlcCompareCnt.ColumnEdit = this.rileCompare;
            this.tlcCompareCnt.FieldName = "COMPARE_CNT";
            this.tlcCompareCnt.MaxWidth = 60;
            this.tlcCompareCnt.Name = "tlcCompareCnt";
            this.tlcCompareCnt.Visible = true;
            this.tlcCompareCnt.VisibleIndex = 7;
            this.tlcCompareCnt.Width = 40;
            // 
            // rileCompare
            // 
            this.rileCompare.AutoHeight = false;
            this.rileCompare.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileCompare.Name = "rileCompare";
            this.rileCompare.NullText = "";
            // 
            // tlcComparePart
            // 
            this.tlcComparePart.AppearanceCell.Options.UseTextOptions = true;
            this.tlcComparePart.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlcComparePart.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tlcComparePart.AppearanceHeader.Options.UseBackColor = true;
            this.tlcComparePart.AppearanceHeader.Options.UseTextOptions = true;
            this.tlcComparePart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlcComparePart.Caption = "부품비교";
            this.tlcComparePart.ColumnEdit = this.rileCompare;
            this.tlcComparePart.FieldName = "COMPARE";
            this.tlcComparePart.MaxWidth = 60;
            this.tlcComparePart.Name = "tlcComparePart";
            this.tlcComparePart.Visible = true;
            this.tlcComparePart.VisibleIndex = 8;
            this.tlcComparePart.Width = 38;
            // 
            // tlExamineResult
            // 
            this.tlExamineResult.AppearanceCell.Options.UseTextOptions = true;
            this.tlExamineResult.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlExamineResult.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tlExamineResult.AppearanceHeader.Options.UseBackColor = true;
            this.tlExamineResult.AppearanceHeader.Options.UseTextOptions = true;
            this.tlExamineResult.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlExamineResult.Caption = "검수결과";
            this.tlExamineResult.ColumnEdit = this.rileExamineResult;
            this.tlExamineResult.FieldName = "INVENTORY_CAT";
            this.tlExamineResult.MaxWidth = 60;
            this.tlExamineResult.Name = "tlExamineResult";
            this.tlExamineResult.Visible = true;
            this.tlExamineResult.VisibleIndex = 9;
            this.tlExamineResult.Width = 42;
            // 
            // rileExamineResult
            // 
            this.rileExamineResult.AutoHeight = false;
            this.rileExamineResult.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileExamineResult.Name = "rileExamineResult";
            this.rileExamineResult.NullText = "";
            this.rileExamineResult.EditValueChanged += new System.EventHandler(this.rileExamineResult_EditValueChanged);
            // 
            // tlExamineDetail
            // 
            this.tlExamineDetail.AppearanceCell.Options.UseTextOptions = true;
            this.tlExamineDetail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.tlExamineDetail.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tlExamineDetail.AppearanceHeader.Options.UseBackColor = true;
            this.tlExamineDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.tlExamineDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlExamineDetail.Caption = "검수내역";
            this.tlExamineDetail.ColumnEdit = this.riteExamineDetail;
            this.tlExamineDetail.FieldName = "DES";
            this.tlExamineDetail.MaxWidth = 130;
            this.tlExamineDetail.Name = "tlExamineDetail";
            this.tlExamineDetail.Visible = true;
            this.tlExamineDetail.VisibleIndex = 10;
            this.tlExamineDetail.Width = 122;
            // 
            // riteExamineDetail
            // 
            this.riteExamineDetail.AutoHeight = false;
            this.riteExamineDetail.Name = "riteExamineDetail";
            // 
            // tlPurchaseCost
            // 
            this.tlPurchaseCost.AppearanceCell.Options.UseTextOptions = true;
            this.tlPurchaseCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tlPurchaseCost.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tlPurchaseCost.AppearanceHeader.Options.UseBackColor = true;
            this.tlPurchaseCost.AppearanceHeader.Options.UseTextOptions = true;
            this.tlPurchaseCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlPurchaseCost.Caption = "매입기준가";
            this.tlPurchaseCost.ColumnEdit = this.risePurchaseCost;
            this.tlPurchaseCost.FieldName = "PURCHASE_COST";
            this.tlPurchaseCost.Format.FormatString = "n0";
            this.tlPurchaseCost.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tlPurchaseCost.MaxWidth = 100;
            this.tlPurchaseCost.Name = "tlPurchaseCost";
            this.tlPurchaseCost.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.tlPurchaseCost.SummaryFooterStrFormat = "{0:n0}";
            this.tlPurchaseCost.Visible = true;
            this.tlPurchaseCost.VisibleIndex = 11;
            this.tlPurchaseCost.Width = 63;
            // 
            // risePurchaseCost
            // 
            this.risePurchaseCost.AutoHeight = false;
            this.risePurchaseCost.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.risePurchaseCost.Name = "risePurchaseCost";
            this.risePurchaseCost.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.risePurchaseCost_EditValueChanging);
            // 
            // tlAdjustCost
            // 
            this.tlAdjustCost.AppearanceCell.Options.UseTextOptions = true;
            this.tlAdjustCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tlAdjustCost.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tlAdjustCost.AppearanceHeader.Options.UseBackColor = true;
            this.tlAdjustCost.AppearanceHeader.Options.UseTextOptions = true;
            this.tlAdjustCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlAdjustCost.Caption = "조정금액";
            this.tlAdjustCost.ColumnEdit = this.rileAdjustCost;
            this.tlAdjustCost.FieldName = "ADJUST_COST";
            this.tlAdjustCost.Format.FormatString = "n0";
            this.tlAdjustCost.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tlAdjustCost.MaxWidth = 100;
            this.tlAdjustCost.Name = "tlAdjustCost";
            this.tlAdjustCost.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.tlAdjustCost.SummaryFooterStrFormat = "{0:n0}";
            this.tlAdjustCost.Visible = true;
            this.tlAdjustCost.VisibleIndex = 12;
            this.tlAdjustCost.Width = 54;
            // 
            // rileAdjustCost
            // 
            this.rileAdjustCost.AutoHeight = false;
            this.rileAdjustCost.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileAdjustCost.Name = "rileAdjustCost";
            this.rileAdjustCost.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rileAdjustCost_EditValueChanging);
            // 
            // tlTotalCost
            // 
            this.tlTotalCost.AppearanceCell.Options.UseTextOptions = true;
            this.tlTotalCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tlTotalCost.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tlTotalCost.AppearanceHeader.Options.UseBackColor = true;
            this.tlTotalCost.AppearanceHeader.Options.UseTextOptions = true;
            this.tlTotalCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlTotalCost.Caption = "실 매입금액";
            this.tlTotalCost.FieldName = "TOTAL_COST";
            this.tlTotalCost.Format.FormatString = "n0";
            this.tlTotalCost.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tlTotalCost.MaxWidth = 100;
            this.tlTotalCost.Name = "tlTotalCost";
            this.tlTotalCost.OptionsColumn.AllowEdit = false;
            this.tlTotalCost.OptionsColumn.ReadOnly = true;
            this.tlTotalCost.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.tlTotalCost.SummaryFooterStrFormat = "{0:n0}";
            this.tlTotalCost.Visible = true;
            this.tlTotalCost.VisibleIndex = 13;
            this.tlTotalCost.Width = 63;
            // 
            // tlcUpdate
            // 
            this.tlcUpdate.AppearanceHeader.Options.UseTextOptions = true;
            this.tlcUpdate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlcUpdate.Caption = "수정";
            this.tlcUpdate.ColumnEdit = this.ribeUpdate;
            this.tlcUpdate.FieldName = "UPDATE";
            this.tlcUpdate.Name = "tlcUpdate";
            // 
            // ribeUpdate
            // 
            this.ribeUpdate.AutoHeight = false;
            this.ribeUpdate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ribeUpdate.Name = "ribeUpdate";
            // 
            // usrReceiptPartTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlPart);
            this.Name = "usrReceiptPartTree";
            this.Size = new System.Drawing.Size(847, 502);
            ((System.ComponentModel.ISupportInitialize)(this.tlPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileExamineResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteExamineDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.risePurchaseCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileAdjustCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribeUpdate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tlPart;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn15;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn14;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcComparePart;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlExamineDetail;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlPurchaseCost;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlExamineResult;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlAdjustCost;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlTotalCost;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileCompare;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileExamineResult;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riteExamineDetail;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit risePurchaseCost;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rileAdjustCost;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcUpdate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribeUpdate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcCompareCnt;
    }
}
