namespace WareHousingMaster.view.kbooks.search.booksearch
{
    partial class usrUnregisteredBookOrderSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrUnregisteredBookOrderSearch));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.leShopCd = new DevExpress.XtraEditors.LookUpEdit();
            this.teStoreCd_S = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.teStoreCd_E = new DevExpress.XtraEditors.TextEdit();
            this.deDtOrder = new DevExpress.XtraEditors.DateEdit();
            this.sbDelete = new DevExpress.XtraEditors.SimpleButton();
            this.sbConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcReleaseCategory2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReceiptNo1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teStoreCd_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teStoreCd_E.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtOrder.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtOrder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReleaseCategory2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbSearch);
            this.layoutControl1.Controls.Add(this.leShopCd);
            this.layoutControl1.Controls.Add(this.teStoreCd_S);
            this.layoutControl1.Controls.Add(this.labelControl11);
            this.layoutControl1.Controls.Add(this.teStoreCd_E);
            this.layoutControl1.Controls.Add(this.deDtOrder);
            this.layoutControl1.Controls.Add(this.sbDelete);
            this.layoutControl1.Controls.Add(this.sbConfirm);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(888, 30);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbSearch
            // 
            this.sbSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbSearch.Appearance.Options.UseFont = true;
            this.sbSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSearch.ImageOptions.Image")));
            this.sbSearch.Location = new System.Drawing.Point(635, 4);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(96, 22);
            this.sbSearch.StyleController = this.layoutControl1;
            this.sbSearch.TabIndex = 4;
            this.sbSearch.Text = "검색";
            this.sbSearch.Click += new System.EventHandler(this.sbSearch_Click);
            // 
            // leShopCd
            // 
            this.leShopCd.EditValue = 1;
            this.leShopCd.Location = new System.Drawing.Point(92, 3);
            this.leShopCd.Name = "leShopCd";
            this.leShopCd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.leShopCd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leShopCd.Properties.Appearance.Options.UseBackColor = true;
            this.leShopCd.Properties.Appearance.Options.UseFont = true;
            this.leShopCd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leShopCd.Properties.NullText = "선택";
            this.leShopCd.Properties.ReadOnly = true;
            this.leShopCd.Size = new System.Drawing.Size(156, 24);
            this.leShopCd.StyleController = this.layoutControl1;
            this.leShopCd.TabIndex = 4;
            this.leShopCd.TabStop = false;
            // 
            // teStoreCd_S
            // 
            this.teStoreCd_S.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.teStoreCd_S.Location = new System.Drawing.Point(342, 4);
            this.teStoreCd_S.Name = "teStoreCd_S";
            this.teStoreCd_S.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teStoreCd_S.Properties.Appearance.Options.UseFont = true;
            this.teStoreCd_S.Size = new System.Drawing.Size(56, 24);
            this.teStoreCd_S.StyleController = this.layoutControl1;
            this.teStoreCd_S.TabIndex = 1;
            this.teStoreCd_S.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teStoreCd_S_KeyDown);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(402, 2);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(9, 14);
            this.labelControl11.StyleController = this.layoutControl1;
            this.labelControl11.TabIndex = 6;
            this.labelControl11.Text = "~";
            // 
            // teStoreCd_E
            // 
            this.teStoreCd_E.Location = new System.Drawing.Point(415, 4);
            this.teStoreCd_E.Name = "teStoreCd_E";
            this.teStoreCd_E.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teStoreCd_E.Properties.Appearance.Options.UseFont = true;
            this.teStoreCd_E.Size = new System.Drawing.Size(56, 24);
            this.teStoreCd_E.StyleController = this.layoutControl1;
            this.teStoreCd_E.TabIndex = 2;
            this.teStoreCd_E.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teStoreCd_E_KeyDown);
            // 
            // deDtOrder
            // 
            this.deDtOrder.EditValue = null;
            this.deDtOrder.Location = new System.Drawing.Point(565, 2);
            this.deDtOrder.Name = "deDtOrder";
            this.deDtOrder.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deDtOrder.Properties.Appearance.Options.UseFont = true;
            this.deDtOrder.Properties.BeepOnError = false;
            this.deDtOrder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtOrder.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDtOrder.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deDtOrder.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtOrder.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deDtOrder.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDtOrder.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.deDtOrder.Size = new System.Drawing.Size(56, 24);
            this.deDtOrder.StyleController = this.layoutControl1;
            this.deDtOrder.TabIndex = 3;
            this.deDtOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.deDtOrder_KeyDown);
            // 
            // sbDelete
            // 
            this.sbDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbDelete.ImageOptions.Image")));
            this.sbDelete.Location = new System.Drawing.Point(790, 2);
            this.sbDelete.Name = "sbDelete";
            this.sbDelete.Size = new System.Drawing.Size(46, 22);
            this.sbDelete.StyleController = this.layoutControl1;
            this.sbDelete.TabIndex = 5;
            this.sbDelete.TabStop = false;
            this.sbDelete.Text = "행삭제";
            this.sbDelete.Click += new System.EventHandler(this.sbDelete_Click);
            // 
            // sbConfirm
            // 
            this.sbConfirm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbConfirm.ImageOptions.Image")));
            this.sbConfirm.Location = new System.Drawing.Point(840, 2);
            this.sbConfirm.Name = "sbConfirm";
            this.sbConfirm.Size = new System.Drawing.Size(46, 22);
            this.sbConfirm.StyleController = this.layoutControl1;
            this.sbConfirm.TabIndex = 5;
            this.sbConfirm.TabStop = false;
            this.sbConfirm.Text = "확정";
            this.sbConfirm.Click += new System.EventHandler(this.sbConfirm_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcReleaseCategory2,
            this.emptySpaceItem4,
            this.layoutControlItem50,
            this.lcReceiptNo6,
            this.emptySpaceItem3,
            this.layoutControlItem10,
            this.lcReceiptNo1,
            this.layoutControlItem7,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(888, 30);
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
            this.lcReleaseCategory2.MinSize = new System.Drawing.Size(250, 25);
            this.lcReleaseCategory2.Name = "lcReleaseCategory2";
            this.lcReleaseCategory2.Size = new System.Drawing.Size(250, 30);
            this.lcReleaseCategory2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReleaseCategory2.Text = "점 코드명";
            this.lcReleaseCategory2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReleaseCategory2.TextSize = new System.Drawing.Size(80, 20);
            this.lcReleaseCategory2.TextToControlDistance = 10;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(623, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 30);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem50.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem50.Control = this.sbSearch;
            this.layoutControlItem50.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem50.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem50.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem50.ImageOptions.Image")));
            this.layoutControlItem50.Location = new System.Drawing.Point(633, 0);
            this.layoutControlItem50.MaxSize = new System.Drawing.Size(100, 28);
            this.layoutControlItem50.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Size = new System.Drawing.Size(100, 30);
            this.layoutControlItem50.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem50.Text = "layoutControlItem14";
            this.layoutControlItem50.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem50.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem50.TextToControlDistance = 0;
            this.layoutControlItem50.TextVisible = false;
            // 
            // lcReceiptNo6
            // 
            this.lcReceiptNo6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcReceiptNo6.AppearanceItemCaption.Options.UseFont = true;
            this.lcReceiptNo6.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo6.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo6.Control = this.teStoreCd_S;
            this.lcReceiptNo6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo6.CustomizationFormText = "접수번호";
            this.lcReceiptNo6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo6.ImageOptions.Image")));
            this.lcReceiptNo6.Location = new System.Drawing.Point(250, 0);
            this.lcReceiptNo6.MaxSize = new System.Drawing.Size(150, 28);
            this.lcReceiptNo6.MinSize = new System.Drawing.Size(150, 25);
            this.lcReceiptNo6.Name = "lcReceiptNo6";
            this.lcReceiptNo6.Size = new System.Drawing.Size(150, 30);
            this.lcReceiptNo6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo6.Text = "매장코드";
            this.lcReceiptNo6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo6.TextSize = new System.Drawing.Size(80, 20);
            this.lcReceiptNo6.TextToControlDistance = 10;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(733, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(55, 30);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.labelControl11;
            this.layoutControlItem10.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(400, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(13, 30);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            this.layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcReceiptNo1
            // 
            this.lcReceiptNo1.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcReceiptNo1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcReceiptNo1.Control = this.teStoreCd_E;
            this.lcReceiptNo1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcReceiptNo1.CustomizationFormText = "접수번호";
            this.lcReceiptNo1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcReceiptNo1.ImageOptions.Image")));
            this.lcReceiptNo1.Location = new System.Drawing.Point(413, 0);
            this.lcReceiptNo1.MaxSize = new System.Drawing.Size(60, 28);
            this.lcReceiptNo1.MinSize = new System.Drawing.Size(60, 25);
            this.lcReceiptNo1.Name = "lcReceiptNo1";
            this.lcReceiptNo1.Size = new System.Drawing.Size(60, 30);
            this.lcReceiptNo1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcReceiptNo1.Text = "매장코드";
            this.lcReceiptNo1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcReceiptNo1.TextSize = new System.Drawing.Size(0, 0);
            this.lcReceiptNo1.TextToControlDistance = 0;
            this.lcReceiptNo1.TextVisible = false;
            this.lcReceiptNo1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem7.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem7.Control = this.deDtOrder;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "정가 변경일";
            this.layoutControlItem7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem7.ImageOptions.Image")));
            this.layoutControlItem7.Location = new System.Drawing.Point(473, 0);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(210, 28);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(150, 25);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(150, 30);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "주문일자";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem7.TextToControlDistance = 10;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbDelete;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem1.ImageOptions.Image")));
            this.layoutControlItem1.Location = new System.Drawing.Point(788, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(50, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(50, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(50, 30);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem14";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbConfirm;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem2.ImageOptions.Image")));
            this.layoutControlItem2.Location = new System.Drawing.Point(838, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(50, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(50, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(50, 30);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem14";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // usrUnregisteredBookOrderSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximumSize = new System.Drawing.Size(0, 30);
            this.MinimumSize = new System.Drawing.Size(888, 30);
            this.Name = "usrUnregisteredBookOrderSearch";
            this.Size = new System.Drawing.Size(888, 30);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leShopCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teStoreCd_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teStoreCd_E.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtOrder.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDtOrder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReleaseCategory2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReceiptNo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraEditors.LookUpEdit leShopCd;
        private DevExpress.XtraLayout.LayoutControlItem lcReleaseCategory2;
        private DevExpress.XtraEditors.TextEdit teStoreCd_S;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.TextEdit teStoreCd_E;
        private DevExpress.XtraLayout.LayoutControlItem lcReceiptNo1;
        private DevExpress.XtraEditors.DateEdit deDtOrder;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton sbDelete;
        private DevExpress.XtraEditors.SimpleButton sbConfirm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
