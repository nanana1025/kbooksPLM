namespace WareHousingMaster.UtilTest
{
    partial class DlgImgArray
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcImage = new DevExpress.XtraGrid.GridControl();
            this.gvImage = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riceCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rilePallet = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilePallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 33;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcImage);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1137, 529);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcImage
            // 
            this.gcImage.Location = new System.Drawing.Point(2, 2);
            this.gcImage.MainView = this.gvImage;
            this.gcImage.Name = "gcImage";
            this.gcImage.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riceCheck,
            this.rilePallet});
            this.gcImage.Size = new System.Drawing.Size(1133, 525);
            this.gcImage.TabIndex = 9;
            this.gcImage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvImage});
            // 
            // gvImage
            // 
            this.gvImage.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCheck,
            this.gridColumn1});
            this.gvImage.GridControl = this.gcImage;
            this.gvImage.Name = "gvImage";
            this.gvImage.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvImage.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save;
            this.gvImage.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Direct;
            this.gvImage.OptionsView.ShowGroupPanel = false;
            this.gvImage.OptionsView.ShowIndicator = false;
            this.gvImage.RowHeight = 500;
            // 
            // gcCheck
            // 
            this.gcCheck.AppearanceCell.Options.UseTextOptions = true;
            this.gcCheck.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.Caption = "IMAGE";
            this.gcCheck.FieldName = "IMAGE1";
            this.gcCheck.MinWidth = 10;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.OptionsColumn.AllowEdit = false;
            this.gcCheck.OptionsColumn.ReadOnly = true;
            this.gcCheck.Visible = true;
            this.gcCheck.VisibleIndex = 0;
            this.gcCheck.Width = 565;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "IMAGE";
            this.gridColumn1.FieldName = "IMAGE2";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 566;
            // 
            // riceCheck
            // 
            this.riceCheck.AutoHeight = false;
            this.riceCheck.Name = "riceCheck";
            // 
            // rilePallet
            // 
            this.rilePallet.AutoHeight = false;
            this.rilePallet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rilePallet.Name = "rilePallet";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1137, 529);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcImage;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1137, 529);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // DlgImgArray
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1137, 529);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgImgArray";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "이미지";
            this.Load += new System.EventHandler(this.DlgCameraTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilePallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
       
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcImage;
        private DevExpress.XtraGrid.Views.Grid.GridView gvImage;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riceCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilePallet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}