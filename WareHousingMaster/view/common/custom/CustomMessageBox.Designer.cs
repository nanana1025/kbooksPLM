namespace WareHousingMaster.view.common.custom
{
    partial class CustomMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomMessageBox));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.content = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.content);
            this.layoutControl1.Controls.Add(this.pictureEdit1);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Controls.Add(this.sbConfirm);
            this.layoutControl1.Controls.Add(this.sbSave);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(363, 76);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // content
            // 
            this.content.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.content.Appearance.Options.UseFont = true;
            this.content.Location = new System.Drawing.Point(73, 3);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(287, 44);
            this.content.StyleController = this.layoutControl1;
            this.content.TabIndex = 8;
            this.content.Text = "주문 DATA를 확정하시겠습니까?";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(3, 3);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Size = new System.Drawing.Size(66, 44);
            this.pictureEdit1.StyleController = this.layoutControl1;
            this.pictureEdit1.TabIndex = 7;
            // 
            // sbCancel
            // 
            this.sbCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancel.ImageOptions.Image")));
            this.sbCancel.Location = new System.Drawing.Point(294, 51);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(66, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 6;
            this.sbCancel.Text = "취소";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // sbConfirm
            // 
            this.sbConfirm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbConfirm.ImageOptions.Image")));
            this.sbConfirm.Location = new System.Drawing.Point(154, 51);
            this.sbConfirm.Name = "sbConfirm";
            this.sbConfirm.Size = new System.Drawing.Size(66, 22);
            this.sbConfirm.StyleController = this.layoutControl1;
            this.sbConfirm.TabIndex = 6;
            this.sbConfirm.Text = "확정";
            this.sbConfirm.Click += new System.EventHandler(this.sbConfirm_Click);
            // 
            // sbSave
            // 
            this.sbSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSave.ImageOptions.Image")));
            this.sbSave.Location = new System.Drawing.Point(224, 51);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(66, 22);
            this.sbSave.StyleController = this.layoutControl1;
            this.sbSave.TabIndex = 6;
            this.sbSave.Text = "저장";
            this.sbSave.Click += new System.EventHandler(this.sbSave_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem24,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(363, 76);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(151, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbSave;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(221, 48);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem14";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.sbCancel;
            this.layoutControlItem24.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem24.Location = new System.Drawing.Point(291, 48);
            this.layoutControlItem24.MaxSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem24.MinSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem24.Text = "layoutControlItem14";
            this.layoutControlItem24.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem24.TextToControlDistance = 0;
            this.layoutControlItem24.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.pictureEdit1;
            this.layoutControlItem3.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(70, 48);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.content;
            this.layoutControlItem4.Location = new System.Drawing.Point(70, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(74, 18);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(291, 48);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbConfirm;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem1.Location = new System.Drawing.Point(151, 48);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem14";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 76);
            this.Controls.Add(this.layoutControl1);
            this.Name = "MessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message";
            this.Load += new System.EventHandler(this.dlgUpdateWarehouseMovementList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraEditors.SimpleButton sbConfirm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl content;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}