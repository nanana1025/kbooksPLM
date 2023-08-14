namespace WareHousingMaster.view.usedPurchase
{
    partial class dlgEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgEditor));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.meText = new DevExpress.XtraEditors.MemoEdit();
            this.sbInsert = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCustom = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.meText);
            this.layoutControl1.Controls.Add(this.sbInsert);
            this.layoutControl1.Controls.Add(this.sbClose);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(430, 340);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // meText
            // 
            this.meText.Location = new System.Drawing.Point(3, 29);
            this.meText.Name = "meText";
            this.meText.Size = new System.Drawing.Size(424, 308);
            this.meText.StyleController = this.layoutControl1;
            this.meText.TabIndex = 7;
            // 
            // sbInsert
            // 
            this.sbInsert.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbInsert.ImageOptions.Image")));
            this.sbInsert.Location = new System.Drawing.Point(231, 3);
            this.sbInsert.Name = "sbInsert";
            this.sbInsert.Size = new System.Drawing.Size(96, 22);
            this.sbInsert.StyleController = this.layoutControl1;
            this.sbInsert.TabIndex = 6;
            this.sbInsert.Text = "확인";
            this.sbInsert.Click += new System.EventHandler(this.sbInsert_Click);
            // 
            // sbClose
            // 
            this.sbClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbClose.ImageOptions.Image")));
            this.sbClose.Location = new System.Drawing.Point(331, 3);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(96, 22);
            this.sbClose.StyleController = this.layoutControl1;
            this.sbClose.TabIndex = 6;
            this.sbClose.Text = "닫기";
            this.sbClose.Click += new System.EventHandler(this.sbCustom_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.lcCustom,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(430, 340);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(228, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbInsert;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem2.ImageOptions.Image")));
            this.layoutControlItem2.Location = new System.Drawing.Point(228, 0);
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
            // lcCustom
            // 
            this.lcCustom.Control = this.sbClose;
            this.lcCustom.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcCustom.CustomizationFormText = "layoutControlItem14";
            this.lcCustom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcCustom.ImageOptions.Image")));
            this.lcCustom.Location = new System.Drawing.Point(328, 0);
            this.lcCustom.MaxSize = new System.Drawing.Size(100, 26);
            this.lcCustom.MinSize = new System.Drawing.Size(100, 26);
            this.lcCustom.Name = "lcCustom";
            this.lcCustom.Size = new System.Drawing.Size(100, 26);
            this.lcCustom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcCustom.Text = "layoutControlItem14";
            this.lcCustom.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcCustom.TextSize = new System.Drawing.Size(0, 0);
            this.lcCustom.TextToControlDistance = 0;
            this.lcCustom.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.meText;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(428, 312);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // dlgEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 340);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "차감내용";
            this.Load += new System.EventHandler(this.dlgNewPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCustom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbInsert;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem lcCustom;
        private DevExpress.XtraEditors.MemoEdit meText;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}