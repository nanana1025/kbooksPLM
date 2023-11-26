namespace WareHousingMaster.view.kbooks.warehouisng
{
    partial class WarehousingBarcodeList
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBookCd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riteBookCd = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcBookNm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riteTitle = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcBookAuthor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPubshNm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPurchCd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rilePurchCd = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcPurProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileOrderRatio = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcRateKbn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileCondition = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcReceiptCnt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riseInpCnt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gcStg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReleasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rileVCnt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riseCnt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteBookCd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilePurchCd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileOrderRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseInpCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileVCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseCnt)).BeginInit();
            this.SuspendLayout();
            // 
            // gcList
            // 
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.EmbeddedNavigator.Appearance.Options.UseTextOptions = true;
            this.gcList.EmbeddedNavigator.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Show;
            this.gcList.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.gcList.Location = new System.Drawing.Point(0, 0);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riteTitle,
            this.riteBookCd,
            this.rilePurchCd,
            this.rileOrderRatio,
            this.riseCnt,
            this.rileCondition,
            this.riseInpCnt,
            this.rileVCnt});
            this.gcList.Size = new System.Drawing.Size(1103, 502);
            this.gcList.TabIndex = 7;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            this.gcList.DoubleClick += new System.EventHandler(this.gcList_DoubleClick);
            this.gcList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcList_KeyDown);
            // 
            // gvList
            // 
            this.gvList.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gvList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvList.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.Control;
            this.gvList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvList.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvList.ColumnPanelRowHeight = 23;
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gcCheck,
            this.gridColumn21,
            this.gcBookCd,
            this.gcBookNm,
            this.gcBookAuthor,
            this.gcPubshNm,
            this.gcPurchCd,
            this.gcPurProcess,
            this.gcRateKbn,
            this.gcReceiptCnt,
            this.gcStg,
            this.gcReleasePrice,
            this.gcSalePrice,
            this.gcDes,
            this.gridColumn1,
            this.gridColumn2});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvList.OptionsEditForm.ShowOnF2Key = DevExpress.Utils.DefaultBoolean.False;
            this.gvList.OptionsView.ShowFooter = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.RowHeight = 22;
            this.gvList.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvList_RowCellStyle);
            this.gvList.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvList_CustomRowCellEdit);
            this.gvList.ShownEditor += new System.EventHandler(this.gvList_ShownEditor);
            this.gvList.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvList_FocusedRowObjectChanged);
            this.gvList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvList_CellValueChanged);
            this.gvList.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvList_CellValueChanging);
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn6.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "STATE";
            this.gridColumn6.FieldName = "STATE";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gcCheck
            // 
            this.gcCheck.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcCheck.AppearanceHeader.Options.UseBackColor = true;
            this.gcCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCheck.Caption = "체크";
            this.gcCheck.FieldName = "CHECK";
            this.gcCheck.MaxWidth = 35;
            this.gcCheck.MinWidth = 35;
            this.gcCheck.Name = "gcCheck";
            this.gcCheck.Width = 35;
            // 
            // gridColumn21
            // 
            this.gridColumn21.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.gridColumn21.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn21.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn21.AppearanceCell.Options.UseFont = true;
            this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn21.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn21.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn21.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn21.AppearanceHeader.Options.UseFont = true;
            this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn21.Caption = "No";
            this.gridColumn21.DisplayFormat.FormatString = "n0";
            this.gridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn21.FieldName = "NO";
            this.gridColumn21.MaxWidth = 45;
            this.gridColumn21.MinWidth = 45;
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.OptionsColumn.ReadOnly = true;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 0;
            this.gridColumn21.Width = 45;
            // 
            // gcBookCd
            // 
            this.gcBookCd.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.gcBookCd.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcBookCd.AppearanceCell.Options.UseBackColor = true;
            this.gcBookCd.AppearanceCell.Options.UseFont = true;
            this.gcBookCd.AppearanceCell.Options.UseTextOptions = true;
            this.gcBookCd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcBookCd.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcBookCd.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcBookCd.AppearanceHeader.Options.UseBackColor = true;
            this.gcBookCd.AppearanceHeader.Options.UseFont = true;
            this.gcBookCd.AppearanceHeader.Options.UseTextOptions = true;
            this.gcBookCd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcBookCd.Caption = "도서코드";
            this.gcBookCd.ColumnEdit = this.riteBookCd;
            this.gcBookCd.FieldName = "BOOKCD";
            this.gcBookCd.MaxWidth = 130;
            this.gcBookCd.MinWidth = 80;
            this.gcBookCd.Name = "gcBookCd";
            this.gcBookCd.OptionsColumn.ReadOnly = true;
            this.gcBookCd.Visible = true;
            this.gcBookCd.VisibleIndex = 1;
            this.gcBookCd.Width = 120;
            // 
            // riteBookCd
            // 
            this.riteBookCd.AutoHeight = false;
            this.riteBookCd.Name = "riteBookCd";
            this.riteBookCd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riteBookCd_KeyDown);
            // 
            // gcBookNm
            // 
            this.gcBookNm.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.gcBookNm.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcBookNm.AppearanceCell.Options.UseBackColor = true;
            this.gcBookNm.AppearanceCell.Options.UseFont = true;
            this.gcBookNm.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcBookNm.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcBookNm.AppearanceHeader.Options.UseBackColor = true;
            this.gcBookNm.AppearanceHeader.Options.UseFont = true;
            this.gcBookNm.AppearanceHeader.Options.UseTextOptions = true;
            this.gcBookNm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcBookNm.Caption = "도서명";
            this.gcBookNm.ColumnEdit = this.riteTitle;
            this.gcBookNm.FieldName = "BOOKNM";
            this.gcBookNm.MaxWidth = 300;
            this.gcBookNm.Name = "gcBookNm";
            this.gcBookNm.OptionsColumn.ReadOnly = true;
            this.gcBookNm.Visible = true;
            this.gcBookNm.VisibleIndex = 2;
            this.gcBookNm.Width = 141;
            // 
            // riteTitle
            // 
            this.riteTitle.AutoHeight = false;
            this.riteTitle.Name = "riteTitle";
            this.riteTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riteTitle_KeyDown);
            // 
            // gcBookAuthor
            // 
            this.gcBookAuthor.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.gcBookAuthor.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcBookAuthor.AppearanceCell.Options.UseBackColor = true;
            this.gcBookAuthor.AppearanceCell.Options.UseFont = true;
            this.gcBookAuthor.AppearanceCell.Options.UseTextOptions = true;
            this.gcBookAuthor.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcBookAuthor.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcBookAuthor.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcBookAuthor.AppearanceHeader.Options.UseBackColor = true;
            this.gcBookAuthor.AppearanceHeader.Options.UseFont = true;
            this.gcBookAuthor.AppearanceHeader.Options.UseTextOptions = true;
            this.gcBookAuthor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcBookAuthor.Caption = "저자명";
            this.gcBookAuthor.FieldName = "AUTHORNM";
            this.gcBookAuthor.MaxWidth = 300;
            this.gcBookAuthor.Name = "gcBookAuthor";
            this.gcBookAuthor.OptionsColumn.ReadOnly = true;
            this.gcBookAuthor.Visible = true;
            this.gcBookAuthor.VisibleIndex = 3;
            this.gcBookAuthor.Width = 82;
            // 
            // gcPubshNm
            // 
            this.gcPubshNm.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.gcPubshNm.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcPubshNm.AppearanceCell.Options.UseBackColor = true;
            this.gcPubshNm.AppearanceCell.Options.UseFont = true;
            this.gcPubshNm.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcPubshNm.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcPubshNm.AppearanceHeader.Options.UseBackColor = true;
            this.gcPubshNm.AppearanceHeader.Options.UseFont = true;
            this.gcPubshNm.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPubshNm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPubshNm.Caption = "출판사명";
            this.gcPubshNm.FieldName = "PUBSHNM";
            this.gcPubshNm.MaxWidth = 200;
            this.gcPubshNm.MinWidth = 100;
            this.gcPubshNm.Name = "gcPubshNm";
            this.gcPubshNm.OptionsColumn.AllowEdit = false;
            this.gcPubshNm.OptionsColumn.ReadOnly = true;
            this.gcPubshNm.Visible = true;
            this.gcPubshNm.VisibleIndex = 4;
            this.gcPubshNm.Width = 100;
            // 
            // gcPurchCd
            // 
            this.gcPurchCd.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.gcPurchCd.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcPurchCd.AppearanceCell.Options.UseBackColor = true;
            this.gcPurchCd.AppearanceCell.Options.UseFont = true;
            this.gcPurchCd.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcPurchCd.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcPurchCd.AppearanceHeader.Options.UseBackColor = true;
            this.gcPurchCd.AppearanceHeader.Options.UseFont = true;
            this.gcPurchCd.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPurchCd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPurchCd.Caption = "매입처명";
            this.gcPurchCd.ColumnEdit = this.rilePurchCd;
            this.gcPurchCd.FieldName = "PURCHCD";
            this.gcPurchCd.MaxWidth = 200;
            this.gcPurchCd.MinWidth = 100;
            this.gcPurchCd.Name = "gcPurchCd";
            this.gcPurchCd.Visible = true;
            this.gcPurchCd.VisibleIndex = 5;
            this.gcPurchCd.Width = 100;
            // 
            // rilePurchCd
            // 
            this.rilePurchCd.AutoHeight = false;
            this.rilePurchCd.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rilePurchCd.Name = "rilePurchCd";
            this.rilePurchCd.NullText = "";
            this.rilePurchCd.EditValueChanged += new System.EventHandler(this.rilePurchCd_EditValueChanged);
            this.rilePurchCd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rilePurchCd_KeyDown);
            // 
            // gcPurProcess
            // 
            this.gcPurProcess.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.gcPurProcess.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcPurProcess.AppearanceCell.Options.UseBackColor = true;
            this.gcPurProcess.AppearanceCell.Options.UseFont = true;
            this.gcPurProcess.AppearanceCell.Options.UseTextOptions = true;
            this.gcPurProcess.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPurProcess.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcPurProcess.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcPurProcess.AppearanceHeader.Options.UseBackColor = true;
            this.gcPurProcess.AppearanceHeader.Options.UseFont = true;
            this.gcPurProcess.AppearanceHeader.Options.UseTextOptions = true;
            this.gcPurProcess.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcPurProcess.Caption = "매입률";
            this.gcPurProcess.ColumnEdit = this.rileOrderRatio;
            this.gcPurProcess.FieldName = "INP_RATE";
            this.gcPurProcess.MaxWidth = 100;
            this.gcPurProcess.MinWidth = 70;
            this.gcPurProcess.Name = "gcPurProcess";
            this.gcPurProcess.Visible = true;
            this.gcPurProcess.VisibleIndex = 6;
            this.gcPurProcess.Width = 70;
            // 
            // rileOrderRatio
            // 
            this.rileOrderRatio.AutoHeight = false;
            this.rileOrderRatio.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileOrderRatio.DisplayFormat.FormatString = "N0";
            this.rileOrderRatio.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rileOrderRatio.EditFormat.FormatString = "N0";
            this.rileOrderRatio.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rileOrderRatio.Name = "rileOrderRatio";
            this.rileOrderRatio.NullText = "";
            this.rileOrderRatio.EditValueChanged += new System.EventHandler(this.rileOrderRatio_EditValueChanged);
            this.rileOrderRatio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rileOrderRatio_KeyDown);
            // 
            // gcRateKbn
            // 
            this.gcRateKbn.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.gcRateKbn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcRateKbn.AppearanceCell.Options.UseBackColor = true;
            this.gcRateKbn.AppearanceCell.Options.UseFont = true;
            this.gcRateKbn.AppearanceCell.Options.UseTextOptions = true;
            this.gcRateKbn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcRateKbn.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcRateKbn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcRateKbn.AppearanceHeader.Options.UseBackColor = true;
            this.gcRateKbn.AppearanceHeader.Options.UseFont = true;
            this.gcRateKbn.AppearanceHeader.Options.UseTextOptions = true;
            this.gcRateKbn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcRateKbn.Caption = "매입구분";
            this.gcRateKbn.ColumnEdit = this.rileCondition;
            this.gcRateKbn.FieldName = "RATE_KBN";
            this.gcRateKbn.MaxWidth = 70;
            this.gcRateKbn.MinWidth = 70;
            this.gcRateKbn.Name = "gcRateKbn";
            this.gcRateKbn.Visible = true;
            this.gcRateKbn.VisibleIndex = 7;
            this.gcRateKbn.Width = 70;
            // 
            // rileCondition
            // 
            this.rileCondition.AutoHeight = false;
            this.rileCondition.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rileCondition.Name = "rileCondition";
            this.rileCondition.NullText = "";
            this.rileCondition.EditValueChanged += new System.EventHandler(this.rileCondition_EditValueChanged);
            this.rileCondition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rileCondition_KeyDown);
            // 
            // gcReceiptCnt
            // 
            this.gcReceiptCnt.AppearanceCell.BackColor = System.Drawing.Color.Transparent;
            this.gcReceiptCnt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReceiptCnt.AppearanceCell.Options.UseBackColor = true;
            this.gcReceiptCnt.AppearanceCell.Options.UseFont = true;
            this.gcReceiptCnt.AppearanceCell.Options.UseTextOptions = true;
            this.gcReceiptCnt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReceiptCnt.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReceiptCnt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReceiptCnt.AppearanceHeader.Options.UseBackColor = true;
            this.gcReceiptCnt.AppearanceHeader.Options.UseFont = true;
            this.gcReceiptCnt.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReceiptCnt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReceiptCnt.Caption = "발행매수";
            this.gcReceiptCnt.ColumnEdit = this.riseInpCnt;
            this.gcReceiptCnt.DisplayFormat.FormatString = "n0";
            this.gcReceiptCnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcReceiptCnt.FieldName = "INP_COUNT";
            this.gcReceiptCnt.MaxWidth = 70;
            this.gcReceiptCnt.MinWidth = 70;
            this.gcReceiptCnt.Name = "gcReceiptCnt";
            this.gcReceiptCnt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "INP_CNT", "{0:N0}")});
            this.gcReceiptCnt.Visible = true;
            this.gcReceiptCnt.VisibleIndex = 8;
            this.gcReceiptCnt.Width = 70;
            // 
            // riseInpCnt
            // 
            this.riseInpCnt.AutoHeight = false;
            this.riseInpCnt.DisplayFormat.FormatString = "n0";
            this.riseInpCnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.riseInpCnt.IsFloatValue = false;
            this.riseInpCnt.MaskSettings.Set("mask", "N00");
            this.riseInpCnt.Name = "riseInpCnt";
            this.riseInpCnt.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.riseInpCnt_EditValueChanging);
            this.riseInpCnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riseInpCnt_KeyDown);
            // 
            // gcStg
            // 
            this.gcStg.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gcStg.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcStg.AppearanceCell.Options.UseBackColor = true;
            this.gcStg.AppearanceCell.Options.UseFont = true;
            this.gcStg.AppearanceCell.Options.UseTextOptions = true;
            this.gcStg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcStg.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcStg.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcStg.AppearanceHeader.Options.UseBackColor = true;
            this.gcStg.AppearanceHeader.Options.UseFont = true;
            this.gcStg.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStg.Caption = "원가금액";
            this.gcStg.DisplayFormat.FormatString = "n0";
            this.gcStg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcStg.FieldName = "TOTAL_PRICE";
            this.gcStg.MaxWidth = 80;
            this.gcStg.MinWidth = 80;
            this.gcStg.Name = "gcStg";
            this.gcStg.OptionsColumn.AllowEdit = false;
            this.gcStg.OptionsColumn.ReadOnly = true;
            this.gcStg.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_PRICE", "{0:n0}")});
            this.gcStg.Width = 80;
            // 
            // gcReleasePrice
            // 
            this.gcReleasePrice.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gcReleasePrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReleasePrice.AppearanceCell.Options.UseBackColor = true;
            this.gcReleasePrice.AppearanceCell.Options.UseFont = true;
            this.gcReleasePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcReleasePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcReleasePrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReleasePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcReleasePrice.AppearanceHeader.Options.UseFont = true;
            this.gcReleasePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcReleasePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcReleasePrice.Caption = "정가";
            this.gcReleasePrice.DisplayFormat.FormatString = "n0";
            this.gcReleasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcReleasePrice.FieldName = "PRICE";
            this.gcReleasePrice.MaxWidth = 80;
            this.gcReleasePrice.MinWidth = 80;
            this.gcReleasePrice.Name = "gcReleasePrice";
            this.gcReleasePrice.OptionsColumn.AllowEdit = false;
            this.gcReleasePrice.OptionsColumn.ReadOnly = true;
            this.gcReleasePrice.Width = 80;
            // 
            // gcSalePrice
            // 
            this.gcSalePrice.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gcSalePrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcSalePrice.AppearanceCell.Options.UseBackColor = true;
            this.gcSalePrice.AppearanceCell.Options.UseFont = true;
            this.gcSalePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gcSalePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcSalePrice.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcSalePrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcSalePrice.AppearanceHeader.Options.UseBackColor = true;
            this.gcSalePrice.AppearanceHeader.Options.UseFont = true;
            this.gcSalePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gcSalePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcSalePrice.Caption = "원단가";
            this.gcSalePrice.DisplayFormat.FormatString = "n0";
            this.gcSalePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcSalePrice.FieldName = "WAREHOUSING_PRICE";
            this.gcSalePrice.MaxWidth = 80;
            this.gcSalePrice.MinWidth = 80;
            this.gcSalePrice.Name = "gcSalePrice";
            this.gcSalePrice.OptionsColumn.ReadOnly = true;
            this.gcSalePrice.Width = 80;
            // 
            // gcDes
            // 
            this.gcDes.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcDes.AppearanceCell.Options.UseFont = true;
            this.gcDes.AppearanceCell.Options.UseTextOptions = true;
            this.gcDes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDes.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gcDes.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcDes.AppearanceHeader.Options.UseBackColor = true;
            this.gcDes.AppearanceHeader.Options.UseFont = true;
            this.gcDes.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDes.Caption = "대입수량";
            this.gcDes.ColumnEdit = this.rileVCnt;
            this.gcDes.DisplayFormat.FormatString = "n0";
            this.gcDes.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcDes.FieldName = "VCNT";
            this.gcDes.MaxWidth = 70;
            this.gcDes.MinWidth = 70;
            this.gcDes.Name = "gcDes";
            this.gcDes.Width = 70;
            // 
            // rileVCnt
            // 
            this.rileVCnt.AutoHeight = false;
            this.rileVCnt.DisplayFormat.FormatString = "N0";
            this.rileVCnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rileVCnt.IsFloatValue = false;
            this.rileVCnt.MaskSettings.Set("mask", "N00");
            this.rileVCnt.Name = "rileVCnt";
            this.rileVCnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rileVCnt_KeyDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "주문수량";
            this.gridColumn1.ColumnEdit = this.riseCnt;
            this.gridColumn1.DisplayFormat.FormatString = "n0";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "ORD_CNT";
            this.gridColumn1.MinWidth = 60;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ORDER_CNT", "{0:n0}")});
            this.gridColumn1.Width = 60;
            // 
            // riseCnt
            // 
            this.riseCnt.AutoHeight = false;
            this.riseCnt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riseCnt.IsFloatValue = false;
            this.riseCnt.MaskSettings.Set("mask", "N00");
            this.riseCnt.Name = "riseCnt";
            this.riseCnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riseCnt_KeyDown);
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "기타";
            this.gridColumn2.FieldName = "ETC";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 9;
            this.gridColumn2.Width = 20;
            // 
            // WarehousingBarcodeList
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcList);
            this.Name = "WarehousingBarcodeList";
            this.Size = new System.Drawing.Size(1103, 502);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteBookCd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilePurchCd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileOrderRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseInpCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rileVCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riseCnt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gcBookNm;
        private DevExpress.XtraGrid.Columns.GridColumn gcBookAuthor;
        private DevExpress.XtraGrid.Columns.GridColumn gcCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gcPubshNm;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gcReceiptCnt;
        private DevExpress.XtraGrid.Columns.GridColumn gcDes;
        private DevExpress.XtraGrid.Columns.GridColumn gcPurchCd;
        private DevExpress.XtraGrid.Columns.GridColumn gcBookCd;
        private DevExpress.XtraGrid.Columns.GridColumn gcStg;
        private DevExpress.XtraGrid.Columns.GridColumn gcPurProcess;
        private DevExpress.XtraGrid.Columns.GridColumn gcReleasePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcRateKbn;
        private DevExpress.XtraGrid.Columns.GridColumn gcSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riteTitle;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riteBookCd;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilePurchCd;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileOrderRatio;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseCnt;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rileCondition;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riseInpCnt;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rileVCnt;
    }
}
