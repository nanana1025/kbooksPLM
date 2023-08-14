namespace WareHousingMaster.view.usedPurchase
{
    partial class dlgNewETCPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgNewETCPart));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.leComponentCd = new DevExpress.XtraEditors.LookUpEdit();
            this.temodelNm = new DevExpress.XtraEditors.TextEdit();
            this.sePrice = new DevExpress.XtraEditors.SpinEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leComponentCd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temodelNm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sePrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbClose);
            this.layoutControl1.Controls.Add(this.leComponentCd);
            this.layoutControl1.Controls.Add(this.temodelNm);
            this.layoutControl1.Controls.Add(this.sePrice);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(401, 112);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbClose
            // 
            this.sbClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbClose.ImageOptions.Image")));
            this.sbClose.Location = new System.Drawing.Point(302, 3);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(96, 22);
            this.sbClose.StyleController = this.layoutControl1;
            this.sbClose.TabIndex = 6;
            this.sbClose.Text = "추가";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click);
            // 
            // leComponentCd
            // 
            this.leComponentCd.Location = new System.Drawing.Point(106, 29);
            this.leComponentCd.Name = "leComponentCd";
            this.leComponentCd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leComponentCd.Properties.NullText = "선택하세요.";
            this.leComponentCd.Size = new System.Drawing.Size(292, 20);
            this.leComponentCd.StyleController = this.layoutControl1;
            this.leComponentCd.TabIndex = 9;
            // 
            // temodelNm
            // 
            this.temodelNm.Location = new System.Drawing.Point(106, 53);
            this.temodelNm.Name = "temodelNm";
            this.temodelNm.Size = new System.Drawing.Size(292, 20);
            this.temodelNm.StyleController = this.layoutControl1;
            this.temodelNm.TabIndex = 5;
            // 
            // sePrice
            // 
            this.sePrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sePrice.Location = new System.Drawing.Point(106, 77);
            this.sePrice.Name = "sePrice";
            this.sePrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sePrice.Properties.DisplayFormat.FormatString = "n0";
            this.sePrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sePrice.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.sePrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.sePrice.Size = new System.Drawing.Size(292, 20);
            this.sePrice.StyleController = this.layoutControl1;
            this.sePrice.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem15,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(401, 112);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(299, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbClose;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(299, 0);
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
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "품목명";
            this.layoutControlItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem1.ImageOptions.Image")));
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(399, 24);
            this.layoutControlItem1.Text = "품목명";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(100, 16);
            this.layoutControlItem1.TextToControlDistance = 3;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 98);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(399, 12);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.temodelNm;
            this.layoutControlItem15.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem15.CustomizationFormText = "접수일";
            this.layoutControlItem15.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem15.ImageOptions.Image")));
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(399, 24);
            this.layoutControlItem15.Text = "부품명";
            this.layoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem15.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(100, 16);
            this.layoutControlItem15.TextToControlDistance = 3;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sePrice;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "접수일";
            this.layoutControlItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem3.ImageOptions.Image")));
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 74);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(399, 24);
            this.layoutControlItem3.Text = "부품가";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(100, 16);
            this.layoutControlItem3.TextToControlDistance = 3;
            // 
            // dlgNewETCPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 112);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgNewETCPart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "기타품목";
            this.Load += new System.EventHandler(this.dlgNewETCPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leComponentCd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temodelNm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sePrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LookUpEdit leComponentCd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit temodelNm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SpinEdit sePrice;
    }
}