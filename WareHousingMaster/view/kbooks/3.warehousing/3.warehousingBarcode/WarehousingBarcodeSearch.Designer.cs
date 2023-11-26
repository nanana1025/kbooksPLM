namespace WareHousingMaster.view.kbooks.warehouisng
{
    partial class WarehousingBarcodeSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarehousingBarcodeSearch));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.leShopCd = new DevExpress.XtraEditors.LookUpEdit();
            this.tePurchaseCd = new DevExpress.XtraEditors.TextEdit();
            this.tePurchaseNm = new DevExpress.XtraEditors.TextEdit();
            this.rgProcessType = new DevExpress.XtraEditors.RadioGroup();
            this.rgBarcodeFg = new DevExpress.XtraEditors.RadioGroup();
            this.rgBookType = new DevExpress.XtraEditors.RadioGroup();
            this.dePurDt = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcReleaseCategory2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchaseCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchaseNm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgProcessType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgBarcodeFg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgBookType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePurDt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePurDt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReleaseCategory2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbSearch);
            this.layoutControl1.Controls.Add(this.leShopCd);
            this.layoutControl1.Controls.Add(this.tePurchaseCd);
            this.layoutControl1.Controls.Add(this.tePurchaseNm);
            this.layoutControl1.Controls.Add(this.rgProcessType);
            this.layoutControl1.Controls.Add(this.rgBarcodeFg);
            this.layoutControl1.Controls.Add(this.rgBookType);
            this.layoutControl1.Controls.Add(this.dePurDt);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(820, 91);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbSearch
            // 
            this.sbSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbSearch.Appearance.Options.UseFont = true;
            this.sbSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSearch.ImageOptions.Image")));
            this.sbSearch.Location = new System.Drawing.Point(707, 2);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(96, 24);
            this.sbSearch.StyleController = this.layoutControl1;
            this.sbSearch.TabIndex = 11;
            this.sbSearch.Text = "조건확정";
            this.sbSearch.Click += new System.EventHandler(this.sbSearch_Click);
            // 
            // leShopCd
            // 
            this.leShopCd.EditValue = "01";
            this.leShopCd.Location = new System.Drawing.Point(132, 2);
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
            // tePurchaseCd
            // 
            this.tePurchaseCd.Location = new System.Drawing.Point(487, 31);
            this.tePurchaseCd.Name = "tePurchaseCd";
            this.tePurchaseCd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePurchaseCd.Properties.Appearance.Options.UseFont = true;
            this.tePurchaseCd.Size = new System.Drawing.Size(66, 24);
            this.tePurchaseCd.StyleController = this.layoutControl1;
            this.tePurchaseCd.TabIndex = 9;
            this.tePurchaseCd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tePurchaseCd_KeyDown);
            // 
            // tePurchaseNm
            // 
            this.tePurchaseNm.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.tePurchaseNm.Location = new System.Drawing.Point(557, 31);
            this.tePurchaseNm.Name = "tePurchaseNm";
            this.tePurchaseNm.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tePurchaseNm.Properties.Appearance.Options.UseFont = true;
            this.tePurchaseNm.Size = new System.Drawing.Size(146, 24);
            this.tePurchaseNm.StyleController = this.layoutControl1;
            this.tePurchaseNm.TabIndex = 10;
            this.tePurchaseNm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teStand_KeyDown);
            // 
            // rgProcessType
            // 
            this.rgProcessType.EditValue = 1;
            this.rgProcessType.Location = new System.Drawing.Point(132, 60);
            this.rgProcessType.Name = "rgProcessType";
            this.rgProcessType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgProcessType.Properties.Appearance.Options.UseFont = true;
            this.rgProcessType.Properties.Columns = 3;
            this.rgProcessType.Properties.FlowLayoutItemHorzIndent = 20;
            this.rgProcessType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "매입처"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "입고일"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "도서별")});
            this.rgProcessType.Size = new System.Drawing.Size(196, 26);
            this.rgProcessType.StyleController = this.layoutControl1;
            this.rgProcessType.TabIndex = 4;
            this.rgProcessType.SelectedIndexChanged += new System.EventHandler(this.rgGSType_SelectedIndexChanged);
            this.rgProcessType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rgProcessType_KeyDown);
            // 
            // rgBarcodeFg
            // 
            this.rgBarcodeFg.EditValue = 1;
            this.rgBarcodeFg.Location = new System.Drawing.Point(487, 60);
            this.rgBarcodeFg.Name = "rgBarcodeFg";
            this.rgBarcodeFg.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgBarcodeFg.Properties.Appearance.Options.UseFont = true;
            this.rgBarcodeFg.Properties.Columns = 2;
            this.rgBarcodeFg.Properties.FlowLayoutItemHorzIndent = 20;
            this.rgBarcodeFg.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "미발행"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "발행+미발행")});
            this.rgBarcodeFg.Size = new System.Drawing.Size(216, 26);
            this.rgBarcodeFg.StyleController = this.layoutControl1;
            this.rgBarcodeFg.TabIndex = 8;
            this.rgBarcodeFg.SelectedIndexChanged += new System.EventHandler(this.rgPurchaseRange_SelectedIndexChanged);
            // 
            // rgBookType
            // 
            this.rgBookType.EditValue = 1;
            this.rgBookType.Location = new System.Drawing.Point(132, 30);
            this.rgBookType.Name = "rgBookType";
            this.rgBookType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgBookType.Properties.Appearance.Options.UseFont = true;
            this.rgBookType.Properties.Columns = 2;
            this.rgBookType.Properties.FlowLayoutItemHorzIndent = 20;
            this.rgBookType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "도서"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "문구")});
            this.rgBookType.Size = new System.Drawing.Size(196, 26);
            this.rgBookType.StyleController = this.layoutControl1;
            this.rgBookType.TabIndex = 1;
            this.rgBookType.SelectedIndexChanged += new System.EventHandler(this.rgSearchType_SelectedIndexChanged);
            this.rgBookType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rgSearchType_KeyDown);
            // 
            // dePurDt
            // 
            this.dePurDt.EditValue = null;
            this.dePurDt.Location = new System.Drawing.Point(487, 2);
            this.dePurDt.Name = "dePurDt";
            this.dePurDt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dePurDt.Properties.Appearance.Options.UseFont = true;
            this.dePurDt.Properties.BeepOnError = false;
            this.dePurDt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dePurDt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dePurDt.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dePurDt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dePurDt.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dePurDt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dePurDt.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.dePurDt.Size = new System.Drawing.Size(216, 24);
            this.dePurDt.StyleController = this.layoutControl1;
            this.dePurDt.TabIndex = 7;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcReleaseCategory2,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.emptySpaceItem3,
            this.layoutControlItem8,
            this.lcType,
            this.layoutControlItem7,
            this.lcReceiptNo7,
            this.lcReceiptNo5,
            this.layoutControlItem50,
            this.emptySpaceItem6});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(820, 91);
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
            this.lcReleaseCategory2.MaxSize = new System.Drawing.Size(330, 28);
            this.lcReleaseCategory2.MinSize = new System.Drawing.Size(330, 25);
            this.lcReleaseCategory2.Name = "lcReleaseCategory2";
            this.lcReleaseCategory2.Size = new System.Drawing.Size(330, 28);
            this.lcReleaseCategory2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReleaseCategory2.Text = "점 코드명";
            this.lcReleaseCategory2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReleaseCategory2.TextSize = new System.Drawing.Size(120, 20);
            this.lcReleaseCategory2.TextToControlDistance = 10;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(805, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(15, 91);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.rgBarcodeFg;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "구분";
            this.layoutControlItem5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem5.ImageOptions.Image")));
            this.layoutControlItem5.Location = new System.Drawing.Point(355, 58);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(215, 30);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(350, 33);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "발행구분";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(120, 20);
            this.layoutControlItem5.TextToControlDistance = 10;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(330, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(25, 0);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(25, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(25, 91);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem8.Control = this.rgBookType;
            this.layoutControlItem8.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem8.CustomizationFormText = "구분";
            this.layoutControlItem8.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem8.ImageOptions.Image")));
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(215, 30);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(330, 30);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "조건선택";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(120, 20);
            this.layoutControlItem8.TextToControlDistance = 10;
            // 
            // lcType
            // 
            this.lcType.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcType.AppearanceItemCaption.Options.UseFont = true;
            this.lcType.Control = this.rgProcessType;
            this.lcType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcType.CustomizationFormText = "구분";
            this.lcType.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcType.ImageOptions.Image")));
            this.lcType.Location = new System.Drawing.Point(0, 58);
            this.lcType.MaxSize = new System.Drawing.Size(0, 30);
            this.lcType.MinSize = new System.Drawing.Size(215, 30);
            this.lcType.Name = "lcType";
            this.lcType.Size = new System.Drawing.Size(330, 33);
            this.lcType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcType.Text = "처리구분";
            this.lcType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcType.TextSize = new System.Drawing.Size(120, 20);
            this.lcType.TextToControlDistance = 10;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem7.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem7.Control = this.dePurDt;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "정가 변경일";
            this.layoutControlItem7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem7.ImageOptions.Image")));
            this.layoutControlItem7.Location = new System.Drawing.Point(355, 0);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(350, 28);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(350, 28);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(350, 28);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "입고일자";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(120, 20);
            this.layoutControlItem7.TextToControlDistance = 10;
            // 
            // lcReceiptNo7
            // 
            this.lcReceiptNo7.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReceiptNo7.AppearanceItemCaption.Options.UseFont = true;
            this.lcReceiptNo7.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo7.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo7.Control = this.tePurchaseCd;
            this.lcReceiptNo7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo7.CustomizationFormText = "접수번호";
            this.lcReceiptNo7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo7.ImageOptions.Image")));
            this.lcReceiptNo7.Location = new System.Drawing.Point(355, 28);
            this.lcReceiptNo7.MaxSize = new System.Drawing.Size(200, 30);
            this.lcReceiptNo7.MinSize = new System.Drawing.Size(200, 30);
            this.lcReceiptNo7.Name = "lcReceiptNo7";
            this.lcReceiptNo7.Size = new System.Drawing.Size(200, 30);
            this.lcReceiptNo7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo7.Text = "매입처 코드 / 명";
            this.lcReceiptNo7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo7.TextSize = new System.Drawing.Size(120, 20);
            this.lcReceiptNo7.TextToControlDistance = 10;
            // 
            // lcReceiptNo5
            // 
            this.lcReceiptNo5.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo5.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo5.Control = this.tePurchaseNm;
            this.lcReceiptNo5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo5.CustomizationFormText = "접수번호";
            this.lcReceiptNo5.Location = new System.Drawing.Point(555, 28);
            this.lcReceiptNo5.MaxSize = new System.Drawing.Size(200, 28);
            this.lcReceiptNo5.MinSize = new System.Drawing.Size(100, 25);
            this.lcReceiptNo5.Name = "lcReceiptNo5";
            this.lcReceiptNo5.Size = new System.Drawing.Size(150, 30);
            this.lcReceiptNo5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo5.Text = "서가 코드 / 명";
            this.lcReceiptNo5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo5.TextSize = new System.Drawing.Size(0, 0);
            this.lcReceiptNo5.TextToControlDistance = 0;
            this.lcReceiptNo5.TextVisible = false;
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.Control = this.sbSearch;
            this.layoutControlItem50.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem50.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem50.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem50.ImageOptions.Image")));
            this.layoutControlItem50.Location = new System.Drawing.Point(705, 0);
            this.layoutControlItem50.MaxSize = new System.Drawing.Size(100, 28);
            this.layoutControlItem50.MinSize = new System.Drawing.Size(100, 28);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Size = new System.Drawing.Size(100, 28);
            this.layoutControlItem50.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem50.Text = "layoutControlItem14";
            this.layoutControlItem50.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem50.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem50.TextToControlDistance = 0;
            this.layoutControlItem50.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(705, 28);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(100, 63);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // WarehousingBarcodeSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximumSize = new System.Drawing.Size(0, 91);
            this.MinimumSize = new System.Drawing.Size(820, 91);
            this.Name = "WarehousingBarcodeSearch";
            this.Size = new System.Drawing.Size(820, 91);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchaseCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePurchaseNm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgProcessType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgBarcodeFg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgBookType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePurDt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dePurDt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReleaseCategory2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit tePurchaseCd;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo7;
        private DevExpress.XtraEditors.TextEdit tePurchaseNm;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.RadioGroup rgProcessType;
        private DevExpress.XtraLayout.LayoutControlItem lcType;
        private DevExpress.XtraEditors.RadioGroup rgBarcodeFg;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.RadioGroup rgBookType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.DateEdit dePurDt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
    }
}
