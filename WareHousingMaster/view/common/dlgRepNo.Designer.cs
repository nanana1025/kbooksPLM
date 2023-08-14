namespace WareHousingMaster.view.common
{
    partial class dlgRepNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgRepNo));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riceCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rilePallet = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.leRepType = new DevExpress.XtraEditors.LookUpEdit();
            this.teNo = new DevExpress.XtraEditors.TextEdit();
            this.sbInput = new DevExpress.XtraEditors.SimpleButton();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBarcodeList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcNoNm = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilePallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leRepType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNoNm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcList);
            this.layoutControl1.Controls.Add(this.leRepType);
            this.layoutControl1.Controls.Add(this.teNo);
            this.layoutControl1.Controls.Add(this.sbInput);
            this.layoutControl1.Controls.Add(this.sbSave);
            this.layoutControl1.Controls.Add(this.sbClose);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(281, 273);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcList
            // 
            this.gcList.Location = new System.Drawing.Point(5, 101);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riceCheck,
            this.rilePallet,
            this.repositoryItemSpinEdit1});
            this.gcList.Size = new System.Drawing.Size(271, 167);
            this.gcList.TabIndex = 8;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcName,
            this.gridColumn5,
            this.gcCheck});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvList.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save;
            this.gvList.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Direct;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvList_FocusedRowObjectChanged);
            // 
            // gcName
            // 
            this.gcName.AppearanceCell.Options.UseTextOptions = true;
            this.gcName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcName.AppearanceHeader.Options.UseTextOptions = true;
            this.gcName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcName.Caption = "관리번호";
            this.gcName.FieldName = "REP_NO";
            this.gcName.Name = "gcName";
            this.gcName.OptionsColumn.AllowEdit = false;
            this.gcName.OptionsColumn.ReadOnly = true;
            this.gcName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BARCODE", "{0}")});
            this.gcName.Visible = true;
            this.gcName.VisibleIndex = 0;
            this.gcName.Width = 161;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "순서";
            this.gridColumn5.ColumnEdit = this.repositoryItemSpinEdit1;
            this.gridColumn5.FieldName = "SEQ";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 63;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // gcCheck
            // 
            this.gcCheck.AppearanceCell.Options.UseTextOptions = true;
            this.gcCheck.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.Caption = "선택";
            this.gcCheck.ColumnEdit = this.riceCheck;
            this.gcCheck.FieldName = "CHECK";
            this.gcCheck.MaxWidth = 45;
            this.gcCheck.MinWidth = 45;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.Visible = true;
            this.gcCheck.VisibleIndex = 2;
            this.gcCheck.Width = 45;
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
            // leRepType
            // 
            this.leRepType.Location = new System.Drawing.Point(93, 29);
            this.leRepType.Name = "leRepType";
            this.leRepType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leRepType.Properties.NullText = "";
            this.leRepType.Size = new System.Drawing.Size(185, 20);
            this.leRepType.StyleController = this.layoutControl1;
            this.leRepType.TabIndex = 4;
            this.leRepType.EditValueChanged += new System.EventHandler(this.leRepType_EditValueChanged);
            // 
            // teNo
            // 
            this.teNo.Location = new System.Drawing.Point(93, 55);
            this.teNo.Name = "teNo";
            this.teNo.Size = new System.Drawing.Size(115, 20);
            this.teNo.StyleController = this.layoutControl1;
            this.teNo.TabIndex = 5;
            // 
            // sbInput
            // 
            this.sbInput.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbInput.ImageOptions.Image")));
            this.sbInput.Location = new System.Drawing.Point(212, 54);
            this.sbInput.Name = "sbInput";
            this.sbInput.Size = new System.Drawing.Size(66, 22);
            this.sbInput.StyleController = this.layoutControl1;
            this.sbInput.TabIndex = 6;
            this.sbInput.Text = "입력";
            this.sbInput.Click += new System.EventHandler(this.sbInput_Click);
            // 
            // sbSave
            // 
            this.sbSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbSave.ImageOptions.Image")));
            this.sbSave.Location = new System.Drawing.Point(142, 3);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(66, 22);
            this.sbSave.StyleController = this.layoutControl1;
            this.sbSave.TabIndex = 6;
            this.sbSave.Text = "저장";
            this.sbSave.Click += new System.EventHandler(this.sbSave_Click);
            // 
            // sbClose
            // 
            this.sbClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbClose.ImageOptions.Image")));
            this.sbClose.Location = new System.Drawing.Point(212, 3);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(66, 22);
            this.sbClose.StyleController = this.layoutControl1;
            this.sbClose.TabIndex = 6;
            this.sbClose.Text = "닫기";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgBarcodeList,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem19,
            this.lcNoNm,
            this.layoutControlItem24});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(281, 273);
            this.Root.TextVisible = false;
            // 
            // lcgBarcodeList
            // 
            buttonImageOptions1.Image = global::WareHousingMaster.Properties.Resources.trash_16x161;
            buttonImageOptions2.Image = global::WareHousingMaster.Properties.Resources.checkbox2_16x161;
            this.lcgBarcodeList.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("삭제", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, 1, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("전체선택", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, "", -1, true, null, true, false, true, null, -1)});
            this.lcgBarcodeList.CustomizationFormText = "리스트";
            this.lcgBarcodeList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBarcodeList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgBarcodeList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.lcgBarcodeList.Location = new System.Drawing.Point(0, 77);
            this.lcgBarcodeList.Name = "lcgBarcodeList";
            this.lcgBarcodeList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgBarcodeList.Size = new System.Drawing.Size(279, 194);
            this.lcgBarcodeList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBarcodeList.Text = "현황";
            this.lcgBarcodeList.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonClick);
            this.lcgBarcodeList.CustomButtonUnchecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonUnchecked);
            this.lcgBarcodeList.CustomButtonChecked += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.lcgBarcodeList_CustomButtonChecked);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcList;
            this.layoutControlItem11.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(275, 171);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbSave;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem1.Location = new System.Drawing.Point(139, 0);
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
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(139, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbClose;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem2.Location = new System.Drawing.Point(209, 0);
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
            // layoutControlItem19
            // 
            this.layoutControlItem19.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem19.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem19.Control = this.leRepType;
            this.layoutControlItem19.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem19.CustomizationFormText = "입고번호";
            this.layoutControlItem19.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem19.ImageOptions.Image")));
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem19.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(279, 25);
            this.layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem19.Text = "번호타입";
            this.layoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem19.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem19.TextToControlDistance = 10;
            // 
            // lcNoNm
            // 
            this.lcNoNm.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lcNoNm.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lcNoNm.Control = this.teNo;
            this.lcNoNm.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lcNoNm.CustomizationFormText = "입력";
            this.lcNoNm.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcNoNm.ImageOptions.Image")));
            this.lcNoNm.Location = new System.Drawing.Point(0, 51);
            this.lcNoNm.MaxSize = new System.Drawing.Size(0, 24);
            this.lcNoNm.MinSize = new System.Drawing.Size(144, 24);
            this.lcNoNm.Name = "lcNoNm";
            this.lcNoNm.Size = new System.Drawing.Size(209, 26);
            this.lcNoNm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcNoNm.Text = "번호";
            this.lcNoNm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcNoNm.TextSize = new System.Drawing.Size(80, 20);
            this.lcNoNm.TextToControlDistance = 10;
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.sbInput;
            this.layoutControlItem24.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem24.Location = new System.Drawing.Point(209, 51);
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
            // dlgRepNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 273);
            this.Controls.Add(this.layoutControl1);
            this.Name = "dlgRepNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "대표번호관리";
            this.Load += new System.EventHandler(this.dlgUpdateWarehouseMovementList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilePallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leRepType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNoNm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraGrid.Columns.GridColumn gcName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riceCheck;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBarcodeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.LookUpEdit leRepType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraEditors.TextEdit teNo;
        private DevExpress.XtraLayout.LayoutControlItem lcNoNm;
        private DevExpress.XtraEditors.SimpleButton sbInput;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilePallet;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
    }
}