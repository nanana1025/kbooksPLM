namespace WareHousingMaster.view.statistics
{
    partial class usrWorkStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrWorkStatistics));
            DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.deDtFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.deDtTo = new DevExpress.XtraEditors.DateEdit();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imgState = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgState)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(952, 488);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.deDtFrom);
            this.layoutControl2.Controls.Add(this.labelControl11);
            this.layoutControl2.Controls.Add(this.deDtTo);
            this.layoutControl2.Controls.Add(this.sbSearch);
            this.layoutControl2.Location = new System.Drawing.Point(3, 3);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(946, 28);
            this.layoutControl2.TabIndex = 5;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // deDtFrom
            // 
            this.deDtFrom.EditValue = null;
            this.deDtFrom.Location = new System.Drawing.Point(93, 4);
            this.deDtFrom.Name = "deDtFrom";
            this.deDtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtFrom.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deDtFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtFrom.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deDtFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtFrom.Properties.Mask.EditMask = "";
            this.deDtFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.deDtFrom.Properties.NullText = "선택";
            this.deDtFrom.Size = new System.Drawing.Size(99, 20);
            this.deDtFrom.StyleController = this.layoutControl2;
            this.deDtFrom.TabIndex = 4;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(196, 3);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(9, 14);
            this.labelControl11.StyleController = this.layoutControl2;
            this.labelControl11.TabIndex = 6;
            this.labelControl11.Text = "~";
            // 
            // deDtTo
            // 
            this.deDtTo.EditValue = null;
            this.deDtTo.Location = new System.Drawing.Point(209, 4);
            this.deDtTo.Name = "deDtTo";
            this.deDtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtTo.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deDtTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtTo.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deDtTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtTo.Properties.Mask.EditMask = "";
            this.deDtTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.deDtTo.Properties.NullText = "선택";
            this.deDtTo.Size = new System.Drawing.Size(90, 20);
            this.deDtTo.StyleController = this.layoutControl2;
            this.deDtTo.TabIndex = 4;
            // 
            // sbSearch
            // 
            this.sbSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSearch.ImageOptions.Image")));
            this.sbSearch.Location = new System.Drawing.Point(325, 3);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(96, 22);
            this.sbSearch.StyleController = this.layoutControl2;
            this.sbSearch.TabIndex = 5;
            this.sbSearch.Text = "검색";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem10,
            this.layoutControlItem9,
            this.layoutControlItem15,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(946, 28);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem8.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem8.Control = this.deDtFrom;
            this.layoutControlItem8.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem8.CustomizationFormText = "입고번호";
            this.layoutControlItem8.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem8.ImageOptions.Image")));
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(200, 0);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(190, 25);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(193, 26);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "기간";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem8.TextToControlDistance = 10;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.labelControl11;
            this.layoutControlItem10.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(193, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(13, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem9.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem9.Control = this.deDtTo;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "입고번호";
            this.layoutControlItem9.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem9.ImageOptions.Image")));
            this.layoutControlItem9.Location = new System.Drawing.Point(206, 0);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(100, 0);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(90, 25);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "입고번호";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.sbSearch;
            this.layoutControlItem15.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem15.Location = new System.Drawing.Point(322, 0);
            this.layoutControlItem15.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem15.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem15.Text = "layoutControlItem14";
            this.layoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(300, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(22, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(422, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(522, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // chartControl1
            // 
            xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram4;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(3, 35);
            this.chartControl1.Name = "chartControl1";
            series4.Name = "Series 1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series4};
            this.chartControl1.Size = new System.Drawing.Size(946, 450);
            this.chartControl1.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(952, 488);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chartControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(950, 454);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.layoutControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(950, 32);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // imgState
            // 
            this.imgState.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgState.ImageStream")));
            this.imgState.InsertImage(global::WareHousingMaster.Properties.Resources.newdatasource_16x16, "newdatasource_16x16", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.imgState.Images.SetKeyName(0, "newdatasource_16x16");
            this.imgState.InsertImage(global::WareHousingMaster.Properties.Resources.version_16x16, "version_16x16", typeof(global::WareHousingMaster.Properties.Resources), 1);
            this.imgState.Images.SetKeyName(1, "version_16x16");
            // 
            // usrWorkStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 488);
            this.Controls.Add(this.layoutControl1);
            this.Name = "usrWorkStatistics";
            this.Text = "현황판";
            this.Load += new System.EventHandler(this.usrWarehouseState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deDtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.Utils.ImageCollection imgState;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileWarehouse;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilePallet;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit deDtFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.DateEdit deDtTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}