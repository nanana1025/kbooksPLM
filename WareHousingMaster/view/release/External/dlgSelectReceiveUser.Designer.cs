namespace WareHousingMaster.view.release.External
{
    partial class dlgSelectReceiveUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSelectReceiveUser));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcUser = new DevExpress.XtraGrid.GridControl();
            this.gvUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileDeptNm = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileUserId = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileClass = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.leUserId = new DevExpress.XtraEditors.LookUpEdit();
            this.sbSelect = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lgcOrderPart = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileDeptNm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgcOrderPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcUser);
            this.layoutControl1.Controls.Add(this.leUserId);
            this.layoutControl1.Controls.Add(this.sbSelect);
            this.layoutControl1.Controls.Add(this.sbClose);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(416, 465);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcUser
            // 
            this.gcUser.Location = new System.Drawing.Point(5, 50);
            this.gcUser.MainView = this.gvUser;
            this.gcUser.MinimumSize = new System.Drawing.Size(287, 0);
            this.gcUser.Name = "gcUser";
            this.gcUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rileDeptNm,
            this.rileClass,
            this.rileUserId});
            this.gcUser.Size = new System.Drawing.Size(406, 410);
            this.gcUser.TabIndex = 11;
            this.gcUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUser});
            // 
            // gvUser
            // 
            this.gvUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCheck,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn22,
            this.gridColumn2});
            this.gvUser.GridControl = this.gcUser;
            this.gvUser.Name = "gvUser";
            this.gvUser.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvUser.OptionsSelection.MultiSelect = true;
            this.gvUser.OptionsView.ShowAutoFilterRow = true;
            this.gvUser.OptionsView.ShowGroupPanel = false;
            this.gvUser.OptionsView.ShowIndicator = false;
            this.gvUser.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvUser_RowCellStyle);
            this.gvUser.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvUser_FocusedRowObjectChanged);
            // 
            // gcCheck
            // 
            this.gcCheck.AppearanceCell.Options.UseTextOptions = true;
            this.gcCheck.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCheck.AppearanceHeader.Options.UseBackColor = true;
            this.gcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.Caption = "CH";
            this.gcCheck.FieldName = "CHECK";
            this.gcCheck.MaxWidth = 40;
            this.gcCheck.MinWidth = 40;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.Visible = true;
            this.gcCheck.VisibleIndex = 0;
            this.gcCheck.Width = 40;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn19.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.Caption = "DEPT";
            this.gridColumn19.ColumnEdit = this.rileDeptNm;
            this.gridColumn19.FieldName = "DEPT_CD";
            this.gridColumn19.MinWidth = 30;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.OptionsColumn.ReadOnly = true;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 1;
            this.gridColumn19.Width = 97;
            // 
            // rileDeptNm
            // 
            this.rileDeptNm.AutoHeight = false;
            this.rileDeptNm.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileDeptNm.Name = "rileDeptNm";
            // 
            // gridColumn20
            // 
            this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn20.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn20.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn20.Caption = "USER NM";
            this.gridColumn20.ColumnEdit = this.rileUserId;
            this.gridColumn20.FieldName = "USER_ID";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.OptionsColumn.ReadOnly = true;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 3;
            this.gridColumn20.Width = 161;
            // 
            // rileUserId
            // 
            this.rileUserId.AutoHeight = false;
            this.rileUserId.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileUserId.Name = "rileUserId";
            // 
            // gridColumn22
            // 
            this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn22.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn22.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn22.Caption = "JOB CLASS";
            this.gridColumn22.ColumnEdit = this.rileClass;
            this.gridColumn22.FieldName = "CLASS";
            this.gridColumn22.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.gridColumn22.MinWidth = 50;
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.OptionsColumn.ReadOnly = true;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 2;
            this.gridColumn22.Width = 106;
            // 
            // rileClass
            // 
            this.rileClass.AutoHeight = false;
            this.rileClass.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileClass.Name = "rileClass";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "EXIST";
            this.gridColumn2.FieldName = "EXIST";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // leUserId
            // 
            this.leUserId.Location = new System.Drawing.Point(88, 3);
            this.leUserId.Name = "leUserId";
            this.leUserId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leUserId.Size = new System.Drawing.Size(111, 20);
            this.leUserId.StyleController = this.layoutControl1;
            this.leUserId.TabIndex = 9;
            // 
            // sbSelect
            // 
            this.sbSelect.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSelect.ImageOptions.Image")));
            this.sbSelect.Location = new System.Drawing.Point(217, 3);
            this.sbSelect.Name = "sbSelect";
            this.sbSelect.Size = new System.Drawing.Size(96, 22);
            this.sbSelect.StyleController = this.layoutControl1;
            this.sbSelect.TabIndex = 6;
            this.sbSelect.Text = "select";
            this.sbSelect.Click += new System.EventHandler(this.sbSelect_Click);
            // 
            // sbClose
            // 
            this.sbClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbClose.ImageOptions.Image")));
            this.sbClose.Location = new System.Drawing.Point(317, 3);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(96, 22);
            this.sbClose.StyleController = this.layoutControl1;
            this.sbClose.TabIndex = 6;
            this.sbClose.Text = "close";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lgcOrderPart,
            this.emptySpaceItem1,
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(416, 465);
            this.Root.TextVisible = false;
            // 
            // lgcOrderPart
            // 
            this.lgcOrderPart.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lgcOrderPart.CaptionImageOptions.Image")));
            this.lgcOrderPart.CustomizationFormText = "현황";
            this.lgcOrderPart.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lgcOrderPart.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lgcOrderPart.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.lgcOrderPart.Location = new System.Drawing.Point(0, 26);
            this.lgcOrderPart.Name = "lgcOrderPart";
            this.lgcOrderPart.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lgcOrderPart.Size = new System.Drawing.Size(414, 437);
            this.lgcOrderPart.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lgcOrderPart.Text = "Receiver";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcUser;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(410, 414);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(200, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(14, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbSelect;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem6.Location = new System.Drawing.Point(214, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem14";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.leUserId;
            this.layoutControlItem1.ImageOptions.Image = global::WareHousingMaster.Properties.Resources.bullet_blue;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(139, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(200, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "SENDER";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(80, 16);
            this.layoutControlItem1.TextToControlDistance = 5;
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbClose;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(314, 0);
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
            // dlgSelectReceiveUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 465);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgSelectReceiveUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Receiver";
            this.Load += new System.EventHandler(this.dlgNewPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileDeptNm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgcOrderPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup lgcOrderPart;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LookUpEdit leUserId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUser;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileDeptNm;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton sbSelect;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileUserId;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileClass;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}