namespace WareHousingMaster.view.price.partPriceMapping
{
    partial class DlgGetWmPartList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgGetWmPartList));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcWMPartList = new DevExpress.XtraGrid.GridControl();
            this.gvWMPartList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riicbePArtState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgState = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riicbeMappingYn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgMapping = new DevExpress.Utils.ImageCollection(this.components);
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.leComponentCd = new DevExpress.XtraEditors.LookUpEdit();
            this.sbInsert = new DevExpress.XtraEditors.SimpleButton();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBarcodeList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWMPartList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWMPartList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbePArtState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbeMappingYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMapping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leComponentCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcWMPartList);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Controls.Add(this.leComponentCd);
            this.layoutControl1.Controls.Add(this.sbInsert);
            this.layoutControl1.Controls.Add(this.sbSearch);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1022, 465);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcWMPartList
            // 
            this.gcWMPartList.Location = new System.Drawing.Point(5, 50);
            this.gcWMPartList.MainView = this.gvWMPartList;
            this.gcWMPartList.Name = "gcWMPartList";
            this.gcWMPartList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riicbeMappingYn,
            this.riicbePArtState});
            this.gcWMPartList.Size = new System.Drawing.Size(1012, 410);
            this.gcWMPartList.TabIndex = 11;
            this.gcWMPartList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWMPartList});
            // 
            // gvWMPartList
            // 
            this.gvWMPartList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gcNo,
            this.gridColumn2,
            this.gridColumn6,
            this.gc2,
            this.gridColumn12,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn3});
            this.gvWMPartList.GridControl = this.gcWMPartList;
            this.gvWMPartList.Name = "gvWMPartList";
            this.gvWMPartList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvWMPartList.OptionsView.ShowAutoFilterRow = true;
            this.gvWMPartList.OptionsView.ShowFooter = true;
            this.gvWMPartList.OptionsView.ShowGroupPanel = false;
            this.gvWMPartList.OptionsView.ShowIndicator = false;
            this.gvWMPartList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvWMPartList_FocusedRowChanged);
            this.gvWMPartList.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvWMPartList_FocusedRowObjectChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "SEQ";
            this.gridColumn1.FieldName = "SEQ";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Tag = "";
            // 
            // gcNo
            // 
            this.gcNo.AppearanceCell.Options.UseTextOptions = true;
            this.gcNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcNo.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcNo.AppearanceHeader.Options.UseBackColor = true;
            this.gcNo.AppearanceHeader.Options.UseTextOptions = true;
            this.gcNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcNo.Caption = "카테고리1";
            this.gcNo.FieldName = "PARTCAT1";
            this.gcNo.MinWidth = 50;
            this.gcNo.Name = "gcNo";
            this.gcNo.OptionsColumn.AllowEdit = false;
            this.gcNo.OptionsColumn.ReadOnly = true;
            this.gcNo.Visible = true;
            this.gcNo.VisibleIndex = 0;
            this.gcNo.Width = 53;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "카테고리2";
            this.gridColumn2.FieldName = "PARTCAT2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 55;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn6.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn6.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "카테고리3";
            this.gridColumn6.FieldName = "PARTCAT3";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 66;
            // 
            // gc2
            // 
            this.gc2.AppearanceCell.Options.UseTextOptions = true;
            this.gc2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gc2.AppearanceHeader.Options.UseBackColor = true;
            this.gc2.AppearanceHeader.Options.UseTextOptions = true;
            this.gc2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc2.Caption = "코드";
            this.gc2.FieldName = "PARTCODE";
            this.gc2.Name = "gc2";
            this.gc2.OptionsColumn.ReadOnly = true;
            this.gc2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "RECEIPT", "{0} 건")});
            this.gc2.Visible = true;
            this.gc2.VisibleIndex = 3;
            this.gc2.Width = 68;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn12.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn12.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "부품명";
            this.gridColumn12.FieldName = "PARTNAME";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            this.gridColumn12.Width = 221;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "매입가";
            this.gridColumn4.DisplayFormat.FormatString = "N0";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "MONEY";
            this.gridColumn4.MaxWidth = 80;
            this.gridColumn4.MinWidth = 80;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 80;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "부품상태";
            this.gridColumn5.ColumnEdit = this.riicbePArtState;
            this.gridColumn5.FieldName = "PART_STATE";
            this.gridColumn5.MaxWidth = 70;
            this.gridColumn5.MinWidth = 70;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 7;
            this.gridColumn5.Width = 70;
            // 
            // riicbePArtState
            // 
            this.riicbePArtState.AutoHeight = false;
            this.riicbePArtState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicbePArtState.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riicbePArtState.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2)});
            this.riicbePArtState.LargeImages = this.imgState;
            this.riicbePArtState.Name = "riicbePArtState";
            this.riicbePArtState.SmallImages = this.imgState;
            // 
            // imgState
            // 
            this.imgState.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgState.ImageStream")));
            this.imgState.InsertImage(global::WareHousingMaster.Properties.Resources.addnewdatasource_16x161, "addnewdatasource_16x161", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.imgState.Images.SetKeyName(0, "addnewdatasource_16x161");
            this.imgState.InsertImage(global::WareHousingMaster.Properties.Resources.trash_16x164, "trash_16x164", typeof(global::WareHousingMaster.Properties.Resources), 1);
            this.imgState.Images.SetKeyName(1, "trash_16x164");
            this.imgState.InsertImage(global::WareHousingMaster.Properties.Resources.iconsetsymbols3_16x16, "iconsetsymbols3_16x16", typeof(global::WareHousingMaster.Properties.Resources), 2);
            this.imgState.Images.SetKeyName(2, "iconsetsymbols3_16x16");
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "매핑유무";
            this.gridColumn3.ColumnEdit = this.riicbeMappingYn;
            this.gridColumn3.FieldName = "MAPPING_YN";
            this.gridColumn3.MaxWidth = 70;
            this.gridColumn3.MinWidth = 70;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            this.gridColumn3.Width = 70;
            // 
            // riicbeMappingYn
            // 
            this.riicbeMappingYn.AutoHeight = false;
            this.riicbeMappingYn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicbeMappingYn.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riicbeMappingYn.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 1)});
            this.riicbeMappingYn.LargeImages = this.imgMapping;
            this.riicbeMappingYn.Name = "riicbeMappingYn";
            this.riicbeMappingYn.SmallImages = this.imgMapping;
            // 
            // imgMapping
            // 
            this.imgMapping.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgMapping.ImageStream")));
            this.imgMapping.InsertImage(global::WareHousingMaster.Properties.Resources.apply_16x1610, "apply_16x1610", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.imgMapping.Images.SetKeyName(0, "apply_16x1610");
            this.imgMapping.InsertImage(global::WareHousingMaster.Properties.Resources.cancel_16x165, "cancel_16x165", typeof(global::WareHousingMaster.Properties.Resources), 1);
            this.imgMapping.Images.SetKeyName(1, "cancel_16x165");
            // 
            // sbCancel
            // 
            this.sbCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancel.ImageOptions.Image")));
            this.sbCancel.Location = new System.Drawing.Point(923, 3);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(96, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 10;
            this.sbCancel.Text = "취소";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // leComponentCd
            // 
            this.leComponentCd.Location = new System.Drawing.Point(78, 3);
            this.leComponentCd.Name = "leComponentCd";
            this.leComponentCd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leComponentCd.Size = new System.Drawing.Size(232, 20);
            this.leComponentCd.StyleController = this.layoutControl1;
            this.leComponentCd.TabIndex = 9;
            // 
            // sbInsert
            // 
            this.sbInsert.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbInsert.ImageOptions.Image")));
            this.sbInsert.Location = new System.Drawing.Point(823, 3);
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
            this.sbSearch.Location = new System.Drawing.Point(314, 3);
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
            this.Root.Size = new System.Drawing.Size(1022, 465);
            this.Root.TextVisible = false;
            // 
            // lcgBarcodeList
            // 
            this.lcgBarcodeList.CustomizationFormText = "현황";
            this.lcgBarcodeList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBarcodeList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgBarcodeList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.lcgBarcodeList.Location = new System.Drawing.Point(0, 26);
            this.lcgBarcodeList.Name = "lcgBarcodeList";
            this.lcgBarcodeList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgBarcodeList.Size = new System.Drawing.Size(1020, 437);
            this.lcgBarcodeList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBarcodeList.Text = "현황";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gcWMPartList;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1016, 414);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(411, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(409, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbInsert;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(820, 0);
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
            this.layoutControlItem1.Size = new System.Drawing.Size(311, 26);
            this.layoutControlItem1.Text = "품목명";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(70, 16);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbSearch;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem3.Location = new System.Drawing.Point(311, 0);
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
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(920, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // DlgGetWmPartList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 465);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgGetWmPartList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "부품 리스트";
            this.Load += new System.EventHandler(this.dlgGetPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcWMPartList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWMPartList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbePArtState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicbeMappingYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMapping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leComponentCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControlGroup lcgBarcodeList;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbInsert;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LookUpEdit leComponentCd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gcWMPartList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWMPartList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gcNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gc2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbePArtState;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicbeMappingYn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.Utils.ImageCollection imgState;
        private DevExpress.Utils.ImageCollection imgMapping;
    }
}