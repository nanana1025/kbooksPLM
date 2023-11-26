namespace WareHousingMaster.view.kbooks.search.booksearch
{
    partial class usrSearchBookSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrSearchBookSearch));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.teTitle = new DevExpress.XtraEditors.TextEdit();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.leShopCd = new DevExpress.XtraEditors.LookUpEdit();
            this.tePublisherCd = new DevExpress.XtraEditors.TextEdit();
            this.teAuthor = new DevExpress.XtraEditors.TextEdit();
            this.tePublisher = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcReceiptNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReleaseCategory2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePublisherCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAuthor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePublisher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReleaseCategory2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teTitle);
            this.layoutControl1.Controls.Add(this.sbSearch);
            this.layoutControl1.Controls.Add(this.leShopCd);
            this.layoutControl1.Controls.Add(this.tePublisherCd);
            this.layoutControl1.Controls.Add(this.teAuthor);
            this.layoutControl1.Controls.Add(this.tePublisher);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1065, 30);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teTitle
            // 
            this.teTitle.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.teTitle.Location = new System.Drawing.Point(328, 4);
            this.teTitle.Name = "teTitle";
            this.teTitle.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teTitle.Properties.Appearance.Options.UseFont = true;
            this.teTitle.Size = new System.Drawing.Size(122, 24);
            this.teTitle.StyleController = this.layoutControl1;
            this.teTitle.TabIndex = 1;
            this.teTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teTitle_KeyDown);
            // 
            // sbSearch
            // 
            this.sbSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbSearch.Appearance.Options.UseFont = true;
            this.sbSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSearch.ImageOptions.Image")));
            this.sbSearch.Location = new System.Drawing.Point(956, 3);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(96, 23);
            this.sbSearch.StyleController = this.layoutControl1;
            this.sbSearch.TabIndex = 5;
            this.sbSearch.Text = "검색";
            this.sbSearch.Click += new System.EventHandler(this.sbSearch_Click);
            // 
            // leShopCd
            // 
            this.leShopCd.EditValue = "01";
            this.leShopCd.Location = new System.Drawing.Point(92, 4);
            this.leShopCd.Name = "leShopCd";
            this.leShopCd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.leShopCd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leShopCd.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.leShopCd.Properties.Appearance.Options.UseBackColor = true;
            this.leShopCd.Properties.Appearance.Options.UseFont = true;
            this.leShopCd.Properties.Appearance.Options.UseForeColor = true;
            this.leShopCd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leShopCd.Properties.NullText = "선택";
            this.leShopCd.Properties.ReadOnly = true;
            this.leShopCd.Size = new System.Drawing.Size(157, 24);
            this.leShopCd.TabIndex = 4;
            this.leShopCd.TabStop = false;
            // 
            // tePublisherCd
            // 
            this.tePublisherCd.Location = new System.Drawing.Point(795, 4);
            this.tePublisherCd.Name = "tePublisherCd";
            this.tePublisherCd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePublisherCd.Properties.Appearance.Options.UseFont = true;
            this.tePublisherCd.Size = new System.Drawing.Size(56, 24);
            this.tePublisherCd.StyleController = this.layoutControl1;
            this.tePublisherCd.TabIndex = 3;
            this.tePublisherCd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tePublisherCd_KeyDown);
            // 
            // teAuthor
            // 
            this.teAuthor.Location = new System.Drawing.Point(529, 4);
            this.teAuthor.Name = "teAuthor";
            this.teAuthor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teAuthor.Properties.Appearance.Options.UseFont = true;
            this.teAuthor.Size = new System.Drawing.Size(122, 24);
            this.teAuthor.StyleController = this.layoutControl1;
            this.teAuthor.TabIndex = 2;
            this.teAuthor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teAuthor_KeyDown);
            // 
            // tePublisher
            // 
            this.tePublisher.Location = new System.Drawing.Point(855, 4);
            this.tePublisher.Name = "tePublisher";
            this.tePublisher.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePublisher.Properties.Appearance.Options.UseFont = true;
            this.tePublisher.Size = new System.Drawing.Size(97, 24);
            this.tePublisher.StyleController = this.layoutControl1;
            this.tePublisher.TabIndex = 4;
            this.tePublisher.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tePublisher_KeyDown);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcReceiptNo,
            this.lcReleaseCategory2,
            this.lcReceiptNo2,
            this.lcReceiptNo1,
            this.lcReceiptNo3,
            this.emptySpaceItem1,
            this.layoutControlItem50});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1065, 30);
            this.Root.TextVisible = false;
            // 
            // lcReceiptNo
            // 
            this.lcReceiptNo.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReceiptNo.AppearanceItemCaption.Options.UseFont = true;
            this.lcReceiptNo.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo.Control = this.teTitle;
            this.lcReceiptNo.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo.CustomizationFormText = "접수번호";
            this.lcReceiptNo.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_black;
            this.lcReceiptNo.Location = new System.Drawing.Point(251, 0);
            this.lcReceiptNo.MaxSize = new System.Drawing.Size(350, 25);
            this.lcReceiptNo.MinSize = new System.Drawing.Size(200, 25);
            this.lcReceiptNo.Name = "lcReceiptNo";
            this.lcReceiptNo.Size = new System.Drawing.Size(201, 30);
            this.lcReceiptNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo.Text = "도서명";
            this.lcReceiptNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo.TextSize = new System.Drawing.Size(65, 20);
            this.lcReceiptNo.TextToControlDistance = 10;
            // 
            // lcReleaseCategory2
            // 
            this.lcReleaseCategory2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReleaseCategory2.AppearanceItemCaption.Options.UseFont = true;
            this.lcReleaseCategory2.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReleaseCategory2.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReleaseCategory2.Control = this.leShopCd;
            this.lcReleaseCategory2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReleaseCategory2.CustomizationFormText = "입고번호";
            this.lcReleaseCategory2.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_black;
            this.lcReleaseCategory2.Location = new System.Drawing.Point(0, 0);
            this.lcReleaseCategory2.MaxSize = new System.Drawing.Size(300, 25);
            this.lcReleaseCategory2.MinSize = new System.Drawing.Size(250, 25);
            this.lcReleaseCategory2.Name = "lcReleaseCategory2";
            this.lcReleaseCategory2.Size = new System.Drawing.Size(251, 30);
            this.lcReleaseCategory2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReleaseCategory2.Text = "점 코드명";
            this.lcReleaseCategory2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReleaseCategory2.TextSize = new System.Drawing.Size(80, 20);
            this.lcReleaseCategory2.TextToControlDistance = 10;
            // 
            // lcReceiptNo2
            // 
            this.lcReceiptNo2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReceiptNo2.AppearanceItemCaption.Options.UseFont = true;
            this.lcReceiptNo2.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo2.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo2.Control = this.teAuthor;
            this.lcReceiptNo2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo2.CustomizationFormText = "접수번호";
            this.lcReceiptNo2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo2.ImageOptions.Image")));
            this.lcReceiptNo2.Location = new System.Drawing.Point(452, 0);
            this.lcReceiptNo2.MaxSize = new System.Drawing.Size(250, 25);
            this.lcReceiptNo2.MinSize = new System.Drawing.Size(200, 25);
            this.lcReceiptNo2.Name = "lcReceiptNo2";
            this.lcReceiptNo2.Size = new System.Drawing.Size(201, 30);
            this.lcReceiptNo2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo2.Text = "저자명";
            this.lcReceiptNo2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo2.TextSize = new System.Drawing.Size(65, 20);
            this.lcReceiptNo2.TextToControlDistance = 10;
            // 
            // lcReceiptNo1
            // 
            this.lcReceiptNo1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReceiptNo1.AppearanceItemCaption.Options.UseFont = true;
            this.lcReceiptNo1.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo1.Control = this.tePublisherCd;
            this.lcReceiptNo1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo1.CustomizationFormText = "접수번호";
            this.lcReceiptNo1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo1.ImageOptions.Image")));
            this.lcReceiptNo1.Location = new System.Drawing.Point(653, 0);
            this.lcReceiptNo1.MaxSize = new System.Drawing.Size(200, 25);
            this.lcReceiptNo1.MinSize = new System.Drawing.Size(200, 25);
            this.lcReceiptNo1.Name = "lcReceiptNo1";
            this.lcReceiptNo1.Size = new System.Drawing.Size(200, 30);
            this.lcReceiptNo1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo1.Text = "출판사 코드 / 명";
            this.lcReceiptNo1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo1.TextSize = new System.Drawing.Size(130, 20);
            this.lcReceiptNo1.TextToControlDistance = 10;
            // 
            // lcReceiptNo3
            // 
            this.lcReceiptNo3.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo3.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo3.Control = this.tePublisher;
            this.lcReceiptNo3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo3.CustomizationFormText = "접수번호";
            this.lcReceiptNo3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo3.ImageOptions.Image")));
            this.lcReceiptNo3.Location = new System.Drawing.Point(853, 0);
            this.lcReceiptNo3.MaxSize = new System.Drawing.Size(200, 25);
            this.lcReceiptNo3.MinSize = new System.Drawing.Size(100, 25);
            this.lcReceiptNo3.Name = "lcReceiptNo3";
            this.lcReceiptNo3.Size = new System.Drawing.Size(101, 30);
            this.lcReceiptNo3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo3.Text = "서가 코드 / 명";
            this.lcReceiptNo3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo3.TextSize = new System.Drawing.Size(0, 0);
            this.lcReceiptNo3.TextToControlDistance = 0;
            this.lcReceiptNo3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(1054, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(11, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem50.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem50.Control = this.sbSearch;
            this.layoutControlItem50.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem50.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem50.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem50.ImageOptions.Image")));
            this.layoutControlItem50.Location = new System.Drawing.Point(954, 0);
            this.layoutControlItem50.MaxSize = new System.Drawing.Size(100, 0);
            this.layoutControlItem50.MinSize = new System.Drawing.Size(100, 30);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Size = new System.Drawing.Size(100, 30);
            this.layoutControlItem50.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem50.Text = "layoutControlItem14";
            this.layoutControlItem50.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem50.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem50.TextToControlDistance = 0;
            this.layoutControlItem50.TextVisible = false;
            // 
            // usrSearchBookSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximumSize = new System.Drawing.Size(0, 30);
            this.MinimumSize = new System.Drawing.Size(1065, 30);
            this.Name = "usrSearchBookSearch";
            this.Size = new System.Drawing.Size(1065, 30);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usrSearchBookSearch_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePublisherCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAuthor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePublisher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReleaseCategory2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit teTitle;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraEditors.LookUpEdit leShopCd;
        private DevExpress.XtraLayout.LayoutControlItem lcReleaseCategory2;
        private DevExpress.XtraEditors.TextEdit tePublisherCd;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo1;
        private DevExpress.XtraEditors.TextEdit teAuthor;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo2;
        private DevExpress.XtraEditors.TextEdit tePublisher;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
