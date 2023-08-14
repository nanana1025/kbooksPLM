namespace WareHousingMaster.view.usedPurchase
{
    partial class dlgUpdatePartCnt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgUpdatePartCnt));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.seCnt = new DevExpress.XtraEditors.SpinEdit();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seCnt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbUpdate);
            this.layoutControl1.Controls.Add(this.seCnt);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(364, 28);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbUpdate
            // 
            this.sbUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbUpdate.ImageOptions.Image")));
            this.sbUpdate.Location = new System.Drawing.Point(165, 3);
            this.sbUpdate.Name = "sbUpdate";
            this.sbUpdate.Size = new System.Drawing.Size(96, 22);
            this.sbUpdate.StyleController = this.layoutControl1;
            this.sbUpdate.TabIndex = 6;
            this.sbUpdate.Text = "수정";
            this.sbUpdate.Click += new System.EventHandler(this.sbUpdate_Click);
            // 
            // seCnt
            // 
            this.seCnt.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seCnt.Location = new System.Drawing.Point(78, 3);
            this.seCnt.Name = "seCnt";
            this.seCnt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seCnt.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.seCnt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.seCnt.Properties.NullText = "[EditValue is null]";
            this.seCnt.Size = new System.Drawing.Size(83, 20);
            this.seCnt.StyleController = this.layoutControl1;
            this.seCnt.TabIndex = 9;
            // 
            // sbCancel
            // 
            this.sbCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancel.ImageOptions.Image")));
            this.sbCancel.Location = new System.Drawing.Point(265, 3);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(96, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 6;
            this.sbCancel.Text = "닫기";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(364, 28);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.seCnt;
            this.layoutControlItem1.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(162, 26);
            this.layoutControlItem1.Text = "수량";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(70, 16);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbUpdate;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(162, 0);
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
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbCancel;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem3.Location = new System.Drawing.Point(262, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem14";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // dlgUpdatePartCnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 28);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgUpdatePartCnt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "부품 수량";
            this.Load += new System.EventHandler(this.dlgNewPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seCnt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbUpdate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SpinEdit seCnt;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}