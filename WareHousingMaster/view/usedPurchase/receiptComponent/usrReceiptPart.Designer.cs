namespace WareHousingMaster.view.usedPurchase.receiptComponent
{
    partial class usrReceiptPart
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
            this.gvPart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riseCnt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gc4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riicbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.riicbProductState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rileComponentCd = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbProductState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComponentCd)).BeginInit();
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
            this.rileComponentCd});
            this.gcPart.Size = new System.Drawing.Size(847, 502);
            this.gcPart.TabIndex = 6;
            this.gcPart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPart});
            // 
            // gvPart
            // 
            this.gvPart.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gcNo,
            this.gridColumn12,
            this.gridColumn11,
            this.gc1,
            this.gc2,
            this.gridColumn9,
            this.gc4,
            this.gcCheck,
            this.gridColumn1});
            this.gvPart.GridControl = this.gcPart;
            this.gvPart.Name = "gvPart";
            this.gvPart.OptionsView.ShowAutoFilterRow = true;
            this.gvPart.OptionsView.ShowFooter = true;
            this.gvPart.OptionsView.ShowGroupPanel = false;
            this.gvPart.OptionsView.ShowIndicator = false;
            this.gvPart.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPart_RowCellStyle);
            this.gvPart.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvPart_FocusedRowObjectChanged);
            this.gvPart.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvPart_CellValueChanged);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "COMPONENT_ID";
            this.gridColumn4.FieldName = "INVENTORY_ID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Tag = "";
            // 
            // gcNo
            // 
            this.gcNo.AppearanceCell.Options.UseTextOptions = true;
            this.gcNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcNo.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcNo.AppearanceHeader.Options.UseBackColor = true;
            this.gcNo.AppearanceHeader.Options.UseTextOptions = true;
            this.gcNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcNo.Caption = "No";
            this.gcNo.FieldName = "NO";
            this.gcNo.MaxWidth = 30;
            this.gcNo.MinWidth = 30;
            this.gcNo.Name = "gcNo";
            this.gcNo.OptionsColumn.AllowEdit = false;
            this.gcNo.OptionsColumn.ReadOnly = true;
            this.gcNo.Visible = true;
            this.gcNo.VisibleIndex = 0;
            this.gcNo.Width = 30;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn12.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn12.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "품목명";
            this.gridColumn12.FieldName = "COMPONENT_CD";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 1;
            this.gridColumn12.Width = 92;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn11.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "부품코드";
            this.gridColumn11.FieldName = "PARTCODE";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.ReadOnly = true;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            this.gridColumn11.Width = 118;
            // 
            // gc1
            // 
            this.gc1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gc1.AppearanceHeader.Options.UseBackColor = true;
            this.gc1.AppearanceHeader.Options.UseTextOptions = true;
            this.gc1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc1.Caption = "부품명";
            this.gc1.FieldName = "MODEL_NM";
            this.gc1.Name = "gc1";
            this.gc1.OptionsColumn.AllowEdit = false;
            this.gc1.OptionsColumn.ReadOnly = true;
            this.gc1.Visible = true;
            this.gc1.VisibleIndex = 3;
            this.gc1.Width = 365;
            // 
            // gc2
            // 
            this.gc2.AppearanceCell.Options.UseTextOptions = true;
            this.gc2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gc2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gc2.AppearanceHeader.Options.UseBackColor = true;
            this.gc2.AppearanceHeader.Options.UseTextOptions = true;
            this.gc2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc2.Caption = "매입기준가";
            this.gc2.DisplayFormat.FormatString = "c0";
            this.gc2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc2.FieldName = "PRICE";
            this.gc2.Name = "gc2";
            this.gc2.OptionsColumn.AllowEdit = false;
            this.gc2.OptionsColumn.ReadOnly = true;
            this.gc2.Visible = true;
            this.gc2.VisibleIndex = 4;
            this.gc2.Width = 107;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn9.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn9.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "수량";
            this.gridColumn9.ColumnEdit = this.riseCnt;
            this.gridColumn9.DisplayFormat.FormatString = "n0";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "PART_CNT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PART_CNT", "{0:N0}")});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            this.gridColumn9.Width = 67;
            // 
            // riseCnt
            // 
            this.riseCnt.AutoHeight = false;
            this.riseCnt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riseCnt.Name = "riseCnt";
            this.riseCnt.EditValueChanged += new System.EventHandler(this.riseCnt_EditValueChanged);
            // 
            // gc4
            // 
            this.gc4.AppearanceCell.Options.UseTextOptions = true;
            this.gc4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gc4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gc4.AppearanceHeader.Options.UseBackColor = true;
            this.gc4.AppearanceHeader.Options.UseTextOptions = true;
            this.gc4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc4.Caption = "총 금액";
            this.gc4.DisplayFormat.FormatString = "c0";
            this.gc4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gc4.FieldName = "TOTAL_PRICE";
            this.gc4.Name = "gc4";
            this.gc4.OptionsColumn.AllowEdit = false;
            this.gc4.OptionsColumn.ReadOnly = true;
            this.gc4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_PRICE", "{0:c0}")});
            this.gc4.Visible = true;
            this.gc4.VisibleIndex = 6;
            this.gc4.Width = 116;
            // 
            // gcCheck
            // 
            this.gcCheck.Caption = "체크";
            this.gcCheck.FieldName = "UPDATE_YN";
            this.gcCheck.MaxWidth = 30;
            this.gcCheck.MinWidth = 30;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.Width = 30;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "S";
            this.gridColumn1.FieldName = "STATE";
            this.gridColumn1.Name = "gridColumn1";
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
            // rileComponentCd
            // 
            this.rileComponentCd.AutoHeight = false;
            this.rileComponentCd.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileComponentCd.Name = "rileComponentCd";
            // 
            // usrReceiptPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcPart);
            this.Name = "usrReceiptPart";
            this.Size = new System.Drawing.Size(847, 502);
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbProductState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComponentCd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPart;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPart;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gcNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileComponentCd;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gc1;
        private DevExpress.XtraGrid.Columns.GridColumn gc2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gc4;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbProductState;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}
