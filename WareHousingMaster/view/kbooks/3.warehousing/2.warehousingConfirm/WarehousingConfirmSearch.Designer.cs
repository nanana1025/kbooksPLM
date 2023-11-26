namespace WareHousingMaster.view.kbooks.warehouisng
{
    partial class WarehousingConfirmSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarehousingConfirmSearch));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.leShopCd = new DevExpress.XtraEditors.LookUpEdit();
            this.lePurchaseCd_E = new DevExpress.XtraEditors.LookUpEdit();
            this.lePurchaseCd_S = new DevExpress.XtraEditors.LookUpEdit();
            this.tePurchaseCd_S = new DevExpress.XtraEditors.TextEdit();
            this.tePurchaseCd_E = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcReleaseCategory2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcTypeNm = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lePurchaseCd_E.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lePurchaseCd_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchaseCd_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchaseCd_E.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReleaseCategory2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcTypeNm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbSearch);
            this.layoutControl1.Controls.Add(this.leShopCd);
            this.layoutControl1.Controls.Add(this.lePurchaseCd_E);
            this.layoutControl1.Controls.Add(this.lePurchaseCd_S);
            this.layoutControl1.Controls.Add(this.tePurchaseCd_S);
            this.layoutControl1.Controls.Add(this.tePurchaseCd_E);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(600, 92);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbSearch
            // 
            this.sbSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbSearch.Appearance.Options.UseFont = true;
            this.sbSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSearch.ImageOptions.Image")));
            this.sbSearch.Location = new System.Drawing.Point(477, 2);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(96, 22);
            this.sbSearch.StyleController = this.layoutControl1;
            this.sbSearch.TabIndex = 5;
            this.sbSearch.Text = "조건확정";
            this.sbSearch.Click += new System.EventHandler(this.sbSearch_Click);
            // 
            // leShopCd
            // 
            this.leShopCd.EditValue = "01";
            this.leShopCd.Location = new System.Drawing.Point(132, 3);
            this.leShopCd.Name = "leShopCd";
            this.leShopCd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.leShopCd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leShopCd.Properties.Appearance.Options.UseBackColor = true;
            this.leShopCd.Properties.Appearance.Options.UseFont = true;
            this.leShopCd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leShopCd.Properties.NullText = "선택";
            this.leShopCd.Properties.ReadOnly = true;
            this.leShopCd.Size = new System.Drawing.Size(196, 24);
            this.leShopCd.StyleController = this.layoutControl1;
            this.leShopCd.TabIndex = 4;
            this.leShopCd.TabStop = false;
            // 
            // lePurchaseCd_E
            // 
            this.lePurchaseCd_E.EditValue = 99;
            this.lePurchaseCd_E.Location = new System.Drawing.Point(202, 64);
            this.lePurchaseCd_E.Name = "lePurchaseCd_E";
            this.lePurchaseCd_E.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lePurchaseCd_E.Properties.Appearance.Options.UseFont = true;
            this.lePurchaseCd_E.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lePurchaseCd_E.Properties.NullText = "";
            this.lePurchaseCd_E.Size = new System.Drawing.Size(246, 24);
            this.lePurchaseCd_E.StyleController = this.layoutControl1;
            this.lePurchaseCd_E.TabIndex = 3;
            this.lePurchaseCd_E.EditValueChanged += new System.EventHandler(this.lePurchaseCd_E_EditValueChanged);
            this.lePurchaseCd_E.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lePurchaseCd_E_KeyDown);
            // 
            // lePurchaseCd_S
            // 
            this.lePurchaseCd_S.Location = new System.Drawing.Point(202, 33);
            this.lePurchaseCd_S.Name = "lePurchaseCd_S";
            this.lePurchaseCd_S.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lePurchaseCd_S.Properties.Appearance.Options.UseFont = true;
            this.lePurchaseCd_S.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lePurchaseCd_S.Properties.NullText = "";
            this.lePurchaseCd_S.Size = new System.Drawing.Size(246, 24);
            this.lePurchaseCd_S.StyleController = this.layoutControl1;
            this.lePurchaseCd_S.TabIndex = 4;
            this.lePurchaseCd_S.EditValueChanged += new System.EventHandler(this.lePurchaseCd_S_EditValueChanged);
            this.lePurchaseCd_S.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lePurchaseCd_S_KeyDown);
            // 
            // tePurchaseCd_S
            // 
            this.tePurchaseCd_S.Location = new System.Drawing.Point(132, 33);
            this.tePurchaseCd_S.Name = "tePurchaseCd_S";
            this.tePurchaseCd_S.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePurchaseCd_S.Properties.Appearance.Options.UseFont = true;
            this.tePurchaseCd_S.Properties.ReadOnly = true;
            this.tePurchaseCd_S.Size = new System.Drawing.Size(66, 24);
            this.tePurchaseCd_S.StyleController = this.layoutControl1;
            this.tePurchaseCd_S.TabIndex = 9;
            // 
            // tePurchaseCd_E
            // 
            this.tePurchaseCd_E.Location = new System.Drawing.Point(132, 64);
            this.tePurchaseCd_E.Name = "tePurchaseCd_E";
            this.tePurchaseCd_E.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePurchaseCd_E.Properties.Appearance.Options.UseFont = true;
            this.tePurchaseCd_E.Properties.ReadOnly = true;
            this.tePurchaseCd_E.Size = new System.Drawing.Size(66, 24);
            this.tePurchaseCd_E.StyleController = this.layoutControl1;
            this.tePurchaseCd_E.TabIndex = 9;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcReleaseCategory2,
            this.emptySpaceItem2,
            this.emptySpaceItem4,
            this.emptySpaceItem3,
            this.layoutControlItem50,
            this.lcTypeNm,
            this.lcReceiptNo7,
            this.lcReceiptNo1,
            this.lcReceiptNo2,
            this.emptySpaceItem5});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(600, 92);
            this.Root.TextVisible = false;
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
            this.lcReleaseCategory2.MaxSize = new System.Drawing.Size(330, 30);
            this.lcReleaseCategory2.MinSize = new System.Drawing.Size(330, 30);
            this.lcReleaseCategory2.Name = "lcReleaseCategory2";
            this.lcReleaseCategory2.Size = new System.Drawing.Size(330, 30);
            this.lcReleaseCategory2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReleaseCategory2.Text = "점 코드명";
            this.lcReleaseCategory2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReleaseCategory2.TextSize = new System.Drawing.Size(120, 20);
            this.lcReleaseCategory2.TextToControlDistance = 10;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(575, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(25, 92);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(475, 26);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(100, 66);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(450, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(25, 0);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(25, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(25, 92);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.Control = this.sbSearch;
            this.layoutControlItem50.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem50.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem50.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem50.ImageOptions.Image")));
            this.layoutControlItem50.Location = new System.Drawing.Point(475, 0);
            this.layoutControlItem50.MaxSize = new System.Drawing.Size(100, 28);
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
            // lcTypeNm
            // 
            this.lcTypeNm.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcTypeNm.AppearanceItemCaption.Options.UseFont = true;
            this.lcTypeNm.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcTypeNm.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcTypeNm.Control = this.lePurchaseCd_E;
            this.lcTypeNm.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcTypeNm.CustomizationFormText = "접수번호";
            this.lcTypeNm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcTypeNm.ImageOptions.Image")));
            this.lcTypeNm.Location = new System.Drawing.Point(200, 60);
            this.lcTypeNm.MaxSize = new System.Drawing.Size(250, 30);
            this.lcTypeNm.MinSize = new System.Drawing.Size(250, 30);
            this.lcTypeNm.Name = "lcTypeNm";
            this.lcTypeNm.Size = new System.Drawing.Size(250, 32);
            this.lcTypeNm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcTypeNm.Text = "매입코드 끝";
            this.lcTypeNm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcTypeNm.TextLocation = DevExpress.Utils.Locations.Left;
            this.lcTypeNm.TextSize = new System.Drawing.Size(0, 0);
            this.lcTypeNm.TextToControlDistance = 0;
            this.lcTypeNm.TextVisible = false;
            // 
            // lcReceiptNo7
            // 
            this.lcReceiptNo7.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReceiptNo7.AppearanceItemCaption.Options.UseFont = true;
            this.lcReceiptNo7.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo7.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo7.Control = this.lePurchaseCd_S;
            this.lcReceiptNo7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo7.CustomizationFormText = "접수번호";
            this.lcReceiptNo7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo7.ImageOptions.Image")));
            this.lcReceiptNo7.Location = new System.Drawing.Point(200, 30);
            this.lcReceiptNo7.MaxSize = new System.Drawing.Size(250, 30);
            this.lcReceiptNo7.MinSize = new System.Drawing.Size(250, 30);
            this.lcReceiptNo7.Name = "lcReceiptNo7";
            this.lcReceiptNo7.Size = new System.Drawing.Size(250, 30);
            this.lcReceiptNo7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo7.Text = "매입처코드 시작";
            this.lcReceiptNo7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo7.TextLocation = DevExpress.Utils.Locations.Left;
            this.lcReceiptNo7.TextSize = new System.Drawing.Size(0, 0);
            this.lcReceiptNo7.TextToControlDistance = 0;
            this.lcReceiptNo7.TextVisible = false;
            // 
            // lcReceiptNo1
            // 
            this.lcReceiptNo1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReceiptNo1.AppearanceItemCaption.Options.UseFont = true;
            this.lcReceiptNo1.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo1.Control = this.tePurchaseCd_S;
            this.lcReceiptNo1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo1.CustomizationFormText = "접수번호";
            this.lcReceiptNo1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo1.ImageOptions.Image")));
            this.lcReceiptNo1.Location = new System.Drawing.Point(0, 30);
            this.lcReceiptNo1.MaxSize = new System.Drawing.Size(200, 30);
            this.lcReceiptNo1.MinSize = new System.Drawing.Size(200, 30);
            this.lcReceiptNo1.Name = "lcReceiptNo1";
            this.lcReceiptNo1.Size = new System.Drawing.Size(200, 30);
            this.lcReceiptNo1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo1.Text = "매입처코드 시작";
            this.lcReceiptNo1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo1.TextSize = new System.Drawing.Size(120, 20);
            this.lcReceiptNo1.TextToControlDistance = 10;
            // 
            // lcReceiptNo2
            // 
            this.lcReceiptNo2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReceiptNo2.AppearanceItemCaption.Options.UseFont = true;
            this.lcReceiptNo2.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo2.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo2.Control = this.tePurchaseCd_E;
            this.lcReceiptNo2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo2.CustomizationFormText = "접수번호";
            this.lcReceiptNo2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo2.ImageOptions.Image")));
            this.lcReceiptNo2.Location = new System.Drawing.Point(0, 60);
            this.lcReceiptNo2.MaxSize = new System.Drawing.Size(200, 30);
            this.lcReceiptNo2.MinSize = new System.Drawing.Size(200, 30);
            this.lcReceiptNo2.Name = "lcReceiptNo2";
            this.lcReceiptNo2.Size = new System.Drawing.Size(200, 32);
            this.lcReceiptNo2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo2.Text = "매입처코드 끝";
            this.lcReceiptNo2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo2.TextSize = new System.Drawing.Size(120, 20);
            this.lcReceiptNo2.TextToControlDistance = 10;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(330, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(120, 30);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // WarehousingConfirmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximumSize = new System.Drawing.Size(600, 92);
            this.MinimumSize = new System.Drawing.Size(600, 92);
            this.Name = "WarehousingConfirmSearch";
            this.Size = new System.Drawing.Size(600, 92);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lePurchaseCd_E.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lePurchaseCd_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchaseCd_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchaseCd_E.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReleaseCategory2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcTypeNm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraEditors.LookUpEdit leShopCd;
        private DevExpress.XtraLayout.LayoutControlItem lcReleaseCategory2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo7;
        private DevExpress.XtraLayout.LayoutControlItem lcTypeNm;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.LookUpEdit lePurchaseCd_E;
        private DevExpress.XtraEditors.LookUpEdit lePurchaseCd_S;
        private DevExpress.XtraEditors.TextEdit tePurchaseCd_S;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo1;
        private DevExpress.XtraEditors.TextEdit tePurchaseCd_E;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
    }
}
