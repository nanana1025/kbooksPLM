namespace WareHousingMaster.view.usedPurchase
{
    partial class dlgGetPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgGetPart));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.leComponentCd = new DevExpress.XtraEditors.LookUpEdit();
            this.gcComponent = new DevExpress.XtraGrid.GridControl();
            this.gvComponent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riseCnt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.sbInsert = new DevExpress.XtraEditors.SimpleButton();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBarcodeList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leComponentCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Controls.Add(this.leComponentCd);
            this.layoutControl1.Controls.Add(this.gcComponent);
            this.layoutControl1.Controls.Add(this.sbInsert);
            this.layoutControl1.Controls.Add(this.sbSearch);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(637, 465);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // leComponentCd
            // 
            this.leComponentCd.Location = new System.Drawing.Point(78, 3);
            this.leComponentCd.Name = "leComponentCd";
            this.leComponentCd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leComponentCd.Size = new System.Drawing.Size(115, 20);
            this.leComponentCd.StyleController = this.layoutControl1;
            this.leComponentCd.TabIndex = 9;
            // 
            // gcComponent
            // 
            this.gcComponent.Location = new System.Drawing.Point(5, 50);
            this.gcComponent.MainView = this.gvComponent;
            this.gcComponent.Name = "gcComponent";
            this.gcComponent.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riseCnt});
            this.gcComponent.Size = new System.Drawing.Size(627, 410);
            this.gcComponent.TabIndex = 8;
            this.gcComponent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvComponent});
            // 
            // gvComponent
            // 
            this.gvComponent.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gc3,
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn3});
            this.gvComponent.GridControl = this.gcComponent;
            this.gvComponent.Name = "gvComponent";
            this.gvComponent.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvComponent.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save;
            this.gvComponent.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Direct;
            this.gvComponent.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvComponent.OptionsView.ShowAutoFilterRow = true;
            this.gvComponent.OptionsView.ShowGroupPanel = false;
            this.gvComponent.OptionsView.ShowIndicator = false;
            this.gvComponent.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvComponent_FocusedRowChanged);
            this.gvComponent.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvComponent_FocusedRowObjectChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "부품코드";
            this.gridColumn1.FieldName = "PARTCODE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 125;
            // 
            // gc3
            // 
            this.gc3.AppearanceCell.Options.UseTextOptions = true;
            this.gc3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gc3.AppearanceHeader.Options.UseTextOptions = true;
            this.gc3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc3.Caption = "부품명";
            this.gc3.FieldName = "MODEL_NM";
            this.gc3.Name = "gc3";
            this.gc3.OptionsColumn.AllowEdit = false;
            this.gc3.OptionsColumn.ReadOnly = true;
            this.gc3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BARCODE", "{0}")});
            this.gc3.Visible = true;
            this.gc3.VisibleIndex = 1;
            this.gc3.Width = 288;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "카테고리";
            this.gridColumn5.FieldName = "CATEGORY";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Width = 152;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "부품가격";
            this.gridColumn2.DisplayFormat.FormatString = "c0";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "PRICE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 101;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "수량";
            this.gridColumn3.ColumnEdit = this.riseCnt;
            this.gridColumn3.DisplayFormat.FormatString = "n0";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "PART_CNT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 59;
            // 
            // riseCnt
            // 
            this.riseCnt.AutoHeight = false;
            this.riseCnt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riseCnt.DisplayFormat.FormatString = "n0";
            this.riseCnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.riseCnt.Name = "riseCnt";
            // 
            // sbInsert
            // 
            this.sbInsert.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbInsert.ImageOptions.Image")));
            this.sbInsert.Location = new System.Drawing.Point(438, 3);
            this.sbInsert.Name = "sbInsert";
            this.sbInsert.Size = new System.Drawing.Size(96, 22);
            this.sbInsert.StyleController = this.layoutControl1;
            this.sbInsert.TabIndex = 6;
            this.sbInsert.Text = "선택";
            this.sbInsert.Click += new System.EventHandler(this.sbInsert_Click);
            // 
            // sbSearch
            // 
            this.sbSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSearch.ImageOptions.Image")));
            this.sbSearch.Location = new System.Drawing.Point(197, 3);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(96, 22);
            this.sbSearch.StyleController = this.layoutControl1;
            this.sbSearch.TabIndex = 6;
            this.sbSearch.Text = "검색";
            this.sbSearch.Click += new System.EventHandler(this.sbSearch_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgBarcodeList,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(637, 465);
            this.Root.TextVisible = false;
            // 
            // lcgBarcodeList
            // 
            this.lcgBarcodeList.CustomizationFormText = "현황";
            this.lcgBarcodeList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBarcodeList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgBarcodeList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.lcgBarcodeList.Location = new System.Drawing.Point(0, 26);
            this.lcgBarcodeList.Name = "lcgBarcodeList";
            this.lcgBarcodeList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgBarcodeList.Size = new System.Drawing.Size(635, 437);
            this.lcgBarcodeList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBarcodeList.Text = "현황";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcComponent;
            this.layoutControlItem11.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(631, 414);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(294, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(141, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbInsert;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(435, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem14";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.leComponentCd;
            this.layoutControlItem1.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(194, 26);
            this.layoutControlItem1.Text = "품목명";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(70, 16);
            this.layoutControlItem1.TextToControlDistance = 5;
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbSearch;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem3.Location = new System.Drawing.Point(194, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem14";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            // 
            // sbCancel
            // 
            this.sbCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancel.ImageOptions.Image")));
            this.sbCancel.Location = new System.Drawing.Point(538, 3);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(96, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 10;
            this.sbCancel.Text = "취소";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(535, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // dlgGetPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 465);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgGetPart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "부품 리스트";
            this.Load += new System.EventHandler(this.dlgGetPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leComponentCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcComponent;
        private DevExpress.XtraGrid.Views.Grid.GridView gvComponent;
        private DevExpress.XtraGrid.Columns.GridColumn gc3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBarcodeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbInsert;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.LookUpEdit leComponentCd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseCnt;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}