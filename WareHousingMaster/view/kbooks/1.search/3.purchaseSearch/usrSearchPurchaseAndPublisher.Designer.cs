namespace WareHousingMaster.view.kbooks.search.booksearch
{
    partial class usrSearchPurchaseAndPublisher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrSearchPurchaseAndPublisher));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.leShopCd = new DevExpress.XtraEditors.LookUpEdit();
            this.teNm = new DevExpress.XtraEditors.TextEdit();
            this.teCd = new DevExpress.XtraEditors.TextEdit();
            this.lcStore = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcNm = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCd = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCd)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbSearch);
            this.layoutControl1.Controls.Add(this.leShopCd);
            this.layoutControl1.Controls.Add(this.teNm);
            this.layoutControl1.Controls.Add(this.teCd);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(888, 26);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcStore,
            this.emptySpaceItem2,
            this.lcNm,
            this.layoutControlItem50,
            this.lcCd});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(888, 26);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(814, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(74, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // sbSearch
            // 
            this.sbSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSearch.ImageOptions.Image")));
            this.sbSearch.Location = new System.Drawing.Point(716, 2);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(96, 22);
            this.sbSearch.StyleController = this.layoutControl1;
            this.sbSearch.TabIndex = 5;
            this.sbSearch.Text = "검색";
            this.sbSearch.Click += new System.EventHandler(this.sbSearch_Click);
            // 
            // leShopCd
            // 
            this.leShopCd.EditValue = 1;
            this.leShopCd.Location = new System.Drawing.Point(122, 2);
            this.leShopCd.Name = "leShopCd";
            this.leShopCd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leShopCd.Properties.NullText = "선택";
            this.leShopCd.Size = new System.Drawing.Size(110, 20);
            this.leShopCd.StyleController = this.layoutControl1;
            this.leShopCd.TabIndex = 4;
            // 
            // teNm
            // 
            this.teNm.Location = new System.Drawing.Point(506, 2);
            this.teNm.Name = "teNm";
            this.teNm.Size = new System.Drawing.Size(206, 20);
            this.teNm.StyleController = this.layoutControl1;
            this.teNm.TabIndex = 4;
            // 
            // teCd
            // 
            this.teCd.Location = new System.Drawing.Point(326, 2);
            this.teCd.Name = "teCd";
            this.teCd.Size = new System.Drawing.Size(86, 20);
            this.teCd.StyleController = this.layoutControl1;
            this.teCd.TabIndex = 4;
            // 
            // lcStore
            // 
            this.lcStore.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcStore.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcStore.Control = this.leShopCd;
            this.lcStore.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcStore.CustomizationFormText = "입고번호";
            this.lcStore.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_black;
            this.lcStore.Location = new System.Drawing.Point(0, 0);
            this.lcStore.MaxSize = new System.Drawing.Size(0, 25);
            this.lcStore.MinSize = new System.Drawing.Size(230, 25);
            this.lcStore.Name = "lcStore";
            this.lcStore.Size = new System.Drawing.Size(234, 26);
            this.lcStore.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcStore.Text = "점 코드명";
            this.lcStore.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcStore.TextSize = new System.Drawing.Size(110, 20);
            this.lcStore.TextToControlDistance = 10;
            // 
            // lcNm
            // 
            this.lcNm.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcNm.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcNm.Control = this.teNm;
            this.lcNm.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcNm.CustomizationFormText = "접수번호";
            this.lcNm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcNm.ImageOptions.Image")));
            this.lcNm.Location = new System.Drawing.Point(414, 0);
            this.lcNm.MaxSize = new System.Drawing.Size(300, 25);
            this.lcNm.MinSize = new System.Drawing.Size(200, 25);
            this.lcNm.Name = "lcNm";
            this.lcNm.Size = new System.Drawing.Size(300, 26);
            this.lcNm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcNm.Text = "매입처 명";
            this.lcNm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcNm.TextSize = new System.Drawing.Size(80, 0);
            this.lcNm.TextToControlDistance = 10;
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.Control = this.sbSearch;
            this.layoutControlItem50.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem50.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem50.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem50.ImageOptions.Image")));
            this.layoutControlItem50.Location = new System.Drawing.Point(714, 0);
            this.layoutControlItem50.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem50.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem50.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem50.Text = "layoutControlItem14";
            this.layoutControlItem50.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem50.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem50.TextToControlDistance = 0;
            this.layoutControlItem50.TextVisible = false;
            // 
            // lcCd
            // 
            this.lcCd.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcCd.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcCd.Control = this.teCd;
            this.lcCd.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcCd.CustomizationFormText = "접수번호";
            this.lcCd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcCd.ImageOptions.Image")));
            this.lcCd.Location = new System.Drawing.Point(234, 0);
            this.lcCd.MaxSize = new System.Drawing.Size(180, 25);
            this.lcCd.MinSize = new System.Drawing.Size(150, 25);
            this.lcCd.Name = "lcCd";
            this.lcCd.Size = new System.Drawing.Size(180, 26);
            this.lcCd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcCd.Text = "매입처 코드";
            this.lcCd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcCd.TextSize = new System.Drawing.Size(80, 0);
            this.lcCd.TextToControlDistance = 10;
            // 
            // usrSearchPurchaseAndPublisher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximumSize = new System.Drawing.Size(0, 26);
            this.MinimumSize = new System.Drawing.Size(888, 26);
            this.Name = "usrSearchPurchaseAndPublisher";
            this.Size = new System.Drawing.Size(888, 26);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraEditors.LookUpEdit leShopCd;
        private DevExpress.XtraLayout.LayoutControlItem lcStore;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit teNm;
        private DevExpress.XtraLayout.LayoutControlItem lcNm;
        private DevExpress.XtraEditors.TextEdit teCd;
        private DevExpress.XtraLayout.LayoutControlItem lcCd;
    }
}
