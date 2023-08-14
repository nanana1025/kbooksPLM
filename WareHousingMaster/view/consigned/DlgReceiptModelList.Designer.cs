using DevExpress.XtraLayout;
using static System.Windows.Forms.ImageList;

namespace WareHousingMaster.view.consigned
{
    partial class DlgReceiptModelList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgReceiptModelList));
            this.rideCreateDt = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcPart = new DevExpress.XtraGrid.GridControl();
            this.gvPart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcConsignedType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riiceAssignYn = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.risePartPrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rileConsignedType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.sbCreate = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBarcodeList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.icCheckYn = new DevExpress.Utils.ImageCollection(this.components);
            this.icHinge = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riiceAssignYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.risePartPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileConsignedType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCheckYn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icHinge)).BeginInit();
            this.SuspendLayout();
            // 
            // rideCreateDt
            // 
            this.rideCreateDt.AutoHeight = false;
            this.rideCreateDt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rideCreateDt.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rideCreateDt.Name = "rideCreateDt";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcPart);
            this.layoutControl1.Controls.Add(this.sbCreate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(383, 360);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcPart
            // 
            this.gcPart.Location = new System.Drawing.Point(4, 30);
            this.gcPart.MainView = this.gvPart;
            this.gcPart.MinimumSize = new System.Drawing.Size(287, 0);
            this.gcPart.Name = "gcPart";
            this.gcPart.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riiceAssignYn,
            this.repositoryItemLookUpEdit2,
            this.risePartPrice,
            this.rileConsignedType,
            this.repositoryItemCheckEdit1});
            this.gcPart.Size = new System.Drawing.Size(375, 326);
            this.gcPart.TabIndex = 11;
            this.gcPart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPart});
            // 
            // gvPart
            // 
            this.gvPart.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn30,
            this.gridColumn19,
            this.gridColumn20,
            this.gcConsignedType,
            this.gridColumn22,
            this.gridColumn21});
            this.gvPart.GridControl = this.gcPart;
            this.gvPart.Name = "gvPart";
            this.gvPart.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPart.OptionsView.ShowFooter = true;
            this.gvPart.OptionsView.ShowGroupPanel = false;
            this.gvPart.OptionsView.ShowIndicator = false;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "TYPE";
            this.gridColumn18.FieldName = "TYPE";
            this.gridColumn18.Name = "gridColumn18";
            // 
            // gridColumn30
            // 
            this.gridColumn30.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn30.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn30.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn30.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn30.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn30.Caption = "체크";
            this.gridColumn30.FieldName = "CHECK";
            this.gridColumn30.MaxWidth = 40;
            this.gridColumn30.MinWidth = 40;
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Width = 40;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn19.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.Caption = "품목명";
            this.gridColumn19.FieldName = "COMPONENT_CD";
            this.gridColumn19.MaxWidth = 70;
            this.gridColumn19.MinWidth = 30;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.Width = 51;
            // 
            // gridColumn20
            // 
            this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn20.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn20.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn20.Caption = "모델명";
            this.gridColumn20.FieldName = "MODEL_NM";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.ReadOnly = true;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 0;
            this.gridColumn20.Width = 255;
            // 
            // gcConsignedType
            // 
            this.gcConsignedType.AppearanceCell.Options.UseTextOptions = true;
            this.gcConsignedType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcConsignedType.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcConsignedType.AppearanceHeader.Options.UseBackColor = true;
            this.gcConsignedType.AppearanceHeader.Options.UseTextOptions = true;
            this.gcConsignedType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcConsignedType.Caption = "개수";
            this.gcConsignedType.DisplayFormat.FormatString = "N0";
            this.gcConsignedType.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcConsignedType.FieldName = "MODEL_CNT";
            this.gcConsignedType.MaxWidth = 70;
            this.gcConsignedType.MinWidth = 40;
            this.gcConsignedType.Name = "gcConsignedType";
            this.gcConsignedType.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MODEL_CNT", "{0:N0}")});
            this.gcConsignedType.Visible = true;
            this.gcConsignedType.VisibleIndex = 1;
            this.gcConsignedType.Width = 70;
            // 
            // gridColumn22
            // 
            this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn22.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn22.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn22.Caption = "할당여부";
            this.gridColumn22.ColumnEdit = this.riiceAssignYn;
            this.gridColumn22.FieldName = "ASSIGN_YN";
            this.gridColumn22.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.gridColumn22.MaxWidth = 60;
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.ReadOnly = true;
            this.gridColumn22.Width = 47;
            // 
            // riiceAssignYn
            // 
            this.riiceAssignYn.AutoHeight = false;
            this.riiceAssignYn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riiceAssignYn.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riiceAssignYn.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", false, 1)});
            this.riiceAssignYn.Name = "riiceAssignYn";
            // 
            // gridColumn21
            // 
            this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn21.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn21.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn21.Caption = "금액";
            this.gridColumn21.ColumnEdit = this.risePartPrice;
            this.gridColumn21.DisplayFormat.FormatString = "N0";
            this.gridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn21.FieldName = "PROXY_PRICE";
            this.gridColumn21.MaxWidth = 60;
            this.gridColumn21.MinWidth = 30;
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PROXY_PRICE", "{0:N0}")});
            this.gridColumn21.Width = 55;
            // 
            // risePartPrice
            // 
            this.risePartPrice.AutoHeight = false;
            this.risePartPrice.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.risePartPrice.DisplayFormat.FormatString = "n0";
            this.risePartPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.risePartPrice.Name = "risePartPrice";
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            // 
            // rileConsignedType
            // 
            this.rileConsignedType.AutoHeight = false;
            this.rileConsignedType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileConsignedType.DropDownRows = 2;
            this.rileConsignedType.Name = "rileConsignedType";
            this.rileConsignedType.NullText = "";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // sbCreate
            // 
            this.sbCreate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCreate.ImageOptions.Image")));
            this.sbCreate.Location = new System.Drawing.Point(284, 3);
            this.sbCreate.Name = "sbCreate";
            this.sbCreate.Size = new System.Drawing.Size(96, 22);
            this.sbCreate.StyleController = this.layoutControl1;
            this.sbCreate.TabIndex = 9;
            this.sbCreate.Text = "확인";
            this.sbCreate.Click += new System.EventHandler(this.sbCreate_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgBarcodeList,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(383, 360);
            this.Root.TextVisible = false;
            // 
            // lcgBarcodeList
            // 
            this.lcgBarcodeList.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lcgBarcodeList.CaptionImageOptions.Image")));
            this.lcgBarcodeList.CustomizationFormText = "현황";
            this.lcgBarcodeList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBarcodeList.GroupBordersVisible = false;
            this.lcgBarcodeList.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lcgBarcodeList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcgBarcodeList.Location = new System.Drawing.Point(0, 26);
            this.lcgBarcodeList.Name = "lcgBarcodeList";
            this.lcgBarcodeList.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lcgBarcodeList.Size = new System.Drawing.Size(381, 332);
            this.lcgBarcodeList.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBarcodeList.Text = "부품 리스트";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcPart;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(379, 330);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbCreate;
            this.layoutControlItem1.Location = new System.Drawing.Point(281, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(281, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // icCheckYn
            // 
            this.icCheckYn.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icCheckYn.ImageStream")));
            this.icCheckYn.InsertImage(global::WareHousingMaster.Properties.Resources.cancel_16x164, "cancel_16x164", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.icCheckYn.Images.SetKeyName(0, "cancel_16x164");
            // 
            // icHinge
            // 
            this.icHinge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icHinge.ImageStream")));
            this.icHinge.InsertImage(global::WareHousingMaster.Properties.Resources.ide_16x161, "ide_16x161", typeof(global::WareHousingMaster.Properties.Resources), 0);
            this.icHinge.Images.SetKeyName(0, "ide_16x161");
            // 
            // DlgReceiptModelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 360);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DlgReceiptModelList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "접수 모델 리스트";
            this.Load += new System.EventHandler(this.usrAdjustmentExamineList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rideCreateDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riiceAssignYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.risePartPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileConsignedType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBarcodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCheckYn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icHinge)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sbCreate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rideCreateDt;
        private DevExpress.Utils.ImageCollection icHinge;
        private DevExpress.Utils.ImageCollection icCheckYn;
        private DevExpress.XtraGrid.GridControl gcPart;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPart;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gcConsignedType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileConsignedType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riiceAssignYn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit risePartPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private LayoutControlGroup lcgBarcodeList;
        private LayoutControlItem layoutControlItem2;
        private EmptySpaceItem emptySpaceItem1;
    }
}