namespace WareHousingMaster.view.warehousing.componentCompare
{
    partial class usrComponentCompare
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrComponentCompare));
            this.tlPart = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn15 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn14 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rileComponentCd = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn16 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.riicbDiff = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rileInventoryCat = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.treeListColumn17 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn11 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn12 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imgDiff = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tlPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComponentCd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbDiff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryCat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgDiff)).BeginInit();
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
            this.treeListColumn1,
            this.treeListColumn4,
            this.treeListColumn16,
            this.treeListColumn7,
            this.treeListColumn9,
            this.treeListColumn8,
            this.treeListColumn17,
            this.treeListColumn11,
            this.treeListColumn12,
            this.treeListColumn10});
            this.tlPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPart.KeyFieldName = "PART_ID";
            this.tlPart.Location = new System.Drawing.Point(0, 0);
            this.tlPart.Name = "tlPart";
            this.tlPart.OptionsView.ShowAutoFilterRow = true;
            this.tlPart.OptionsView.ShowIndicator = false;
            this.tlPart.OptionsView.ShowRowFooterSummary = true;
            this.tlPart.OptionsView.ShowSummaryFooter = true;
            this.tlPart.ParentFieldName = "P_PART_ID";
            this.tlPart.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileComponentCd,
            this.riicbDiff,
            this.rileInventoryCat});
            this.tlPart.Size = new System.Drawing.Size(1046, 502);
            this.tlPart.TabIndex = 5;
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
            this.treeListColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.treeListColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn2.Caption = "품목명";
            this.treeListColumn2.ColumnEdit = this.rileComponentCd;
            this.treeListColumn2.FieldName = "COMPONENT_CD";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // rileComponentCd
            // 
            this.rileComponentCd.AutoHeight = false;
            this.rileComponentCd.Name = "rileComponentCd";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.treeListColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn3.Caption = "접수 모델명";
            this.treeListColumn3.FieldName = "PURCHASE_MODEL_NM";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn1.Caption = "수량";
            this.treeListColumn1.FieldName = "PURCHASE_CNT";
            this.treeListColumn1.Format.FormatString = "n0";
            this.treeListColumn1.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 7;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn4.Caption = "부품명";
            this.treeListColumn4.FieldName = "COMPONENT";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 3;
            // 
            // treeListColumn16
            // 
            this.treeListColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.treeListColumn16.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn16.Caption = "입고 모델명";
            this.treeListColumn16.FieldName = "입고 모델명";
            this.treeListColumn16.Name = "treeListColumn16";
            this.treeListColumn16.Visible = true;
            this.treeListColumn16.VisibleIndex = 4;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn7.Caption = "부품개수";
            this.treeListColumn7.FieldName = "PART_CNT";
            this.treeListColumn7.Format.FormatString = "n0";
            this.treeListColumn7.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 2;
            // 
            // treeListColumn9
            // 
            this.treeListColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn9.Caption = "비교";
            this.treeListColumn9.ColumnEdit = this.riicbDiff;
            this.treeListColumn9.FieldName = "DIFF";
            this.treeListColumn9.Name = "treeListColumn9";
            this.treeListColumn9.Visible = true;
            this.treeListColumn9.VisibleIndex = 6;
            // 
            // riicbDiff
            // 
            this.riicbDiff.AutoHeight = false;
            this.riicbDiff.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicbDiff.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riicbDiff.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0)});
            this.riicbDiff.LargeImages = this.imgDiff;
            this.riicbDiff.Name = "riicbDiff";
            this.riicbDiff.SmallImages = this.imgDiff;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn8.Caption = "부품상태";
            this.treeListColumn8.ColumnEdit = this.rileInventoryCat;
            this.treeListColumn8.FieldName = "INVENTORY_CAT";
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.Visible = true;
            this.treeListColumn8.VisibleIndex = 5;
            // 
            // rileInventoryCat
            // 
            this.rileInventoryCat.AutoHeight = false;
            this.rileInventoryCat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileInventoryCat.Name = "rileInventoryCat";
            // 
            // treeListColumn17
            // 
            this.treeListColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn17.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn17.Caption = "매입기준가";
            this.treeListColumn17.FieldName = "INIT_PRICE";
            this.treeListColumn17.Format.FormatString = "c0";
            this.treeListColumn17.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn17.Name = "treeListColumn17";
            this.treeListColumn17.RowFooterSummaryStrFormat = "{c0}";
            this.treeListColumn17.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.treeListColumn17.Visible = true;
            this.treeListColumn17.VisibleIndex = 8;
            // 
            // treeListColumn11
            // 
            this.treeListColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn11.Caption = "차감금액";
            this.treeListColumn11.FieldName = "ADJUST_PRICE";
            this.treeListColumn11.Format.FormatString = "c0";
            this.treeListColumn11.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn11.Name = "treeListColumn11";
            this.treeListColumn11.RowFooterSummary = DevExpress.XtraTreeList.SummaryItemType.Max;
            this.treeListColumn11.RowFooterSummaryStrFormat = "{c0}";
            this.treeListColumn11.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.treeListColumn11.Visible = true;
            this.treeListColumn11.VisibleIndex = 9;
            // 
            // treeListColumn12
            // 
            this.treeListColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn12.Caption = "최종금액";
            this.treeListColumn12.FieldName = "PRICE";
            this.treeListColumn12.Format.FormatString = "c0";
            this.treeListColumn12.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn12.Name = "treeListColumn12";
            this.treeListColumn12.RowFooterSummaryStrFormat = "{c0}";
            this.treeListColumn12.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.treeListColumn12.Visible = true;
            this.treeListColumn12.VisibleIndex = 10;
            // 
            // treeListColumn10
            // 
            this.treeListColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.treeListColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn10.Caption = "차감내역";
            this.treeListColumn10.FieldName = "ADJUST_DES";
            this.treeListColumn10.Name = "treeListColumn10";
            this.treeListColumn10.Visible = true;
            this.treeListColumn10.VisibleIndex = 11;
            // 
            // imgDiff
            // 
            this.imgDiff.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgDiff.ImageStream")));
            this.imgDiff.InsertImage(global::WareHousingMaster.Properties.Resources.suggestion_16x16, "suggestion_16x16", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.imgDiff.Images.SetKeyName(0, "suggestion_16x16");
            // 
            // usrComponentCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlPart);
            this.Name = "usrComponentCompare";
            this.Size = new System.Drawing.Size(1046, 502);
            ((System.ComponentModel.ISupportInitialize)(this.tlPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileComponentCd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbDiff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileInventoryCat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgDiff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tlPart;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn15;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn14;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileComponentCd;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn16;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbDiff;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileInventoryCat;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn17;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn11;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn12;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
        private DevExpress.Utils.ImageCollection imgDiff;
    }
}
