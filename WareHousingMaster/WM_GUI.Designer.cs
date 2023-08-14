namespace WareHousingMaster
{
    partial class WM_GUI
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WM_GUI));
            this.menu_group = new System.Windows.Forms.GroupBox();
            this.lbRepresentative = new DevExpress.XtraEditors.LabelControl();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btn_compelete_ = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_set = new System.Windows.Forms.Button();
            this.btn_stg = new System.Windows.Forms.Button();
            this.btn_save_ = new System.Windows.Forms.Button();
            this.w_numb_txt_ = new System.Windows.Forms.TextBox();
            this.lbl_location = new System.Windows.Forms.Label();
            this.btn_all = new System.Windows.Forms.Button();
            this.btn_add_ = new System.Windows.Forms.Button();
            this.btn_cpu = new System.Windows.Forms.Button();
            this.btn_mbd = new System.Windows.Forms.Button();
            this.btn_mem = new System.Windows.Forms.Button();
            this.btn_vga = new System.Windows.Forms.Button();
            this.btn_mon = new System.Windows.Forms.Button();
            this.btn_psu = new System.Windows.Forms.Button();
            this.combo_printer_ = new System.Windows.Forms.ComboBox();
            this.product_print_ = new System.Windows.Forms.Button();
            this.btn_print_ = new System.Windows.Forms.Button();
            this.selected = new System.Windows.Forms.GroupBox();
            this.lbType = new DevExpress.XtraEditors.LabelControl();
            this.dbName = new System.Windows.Forms.Label();
            this.lbPCType = new DevExpress.XtraEditors.LabelControl();
            this.lbl_ver_ = new System.Windows.Forms.Label();
            this.summary_grid_ = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbCheckList = new System.Windows.Forms.ComboBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lbl_notice_ = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_product_no = new System.Windows.Forms.Label();
            this.lbl_superviseno = new System.Windows.Forms.Label();
            this.lbl_username = new System.Windows.Forms.Label();
            this.combo_location_ = new System.Windows.Forms.ComboBox();
            this.detail_grid_ = new System.Windows.Forms.DataGridView();
            this.menu_group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.selected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.summary_grid_)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detail_grid_)).BeginInit();
            this.SuspendLayout();
            // 
            // menu_group
            // 
            this.menu_group.Controls.Add(this.lbRepresentative);
            this.menu_group.Controls.Add(this.btnUpdate);
            this.menu_group.Controls.Add(this.btn_compelete_);
            this.menu_group.Controls.Add(this.pictureBox1);
            this.menu_group.Controls.Add(this.btn_set);
            this.menu_group.Controls.Add(this.btn_stg);
            this.menu_group.Controls.Add(this.btn_save_);
            this.menu_group.Controls.Add(this.w_numb_txt_);
            this.menu_group.Controls.Add(this.lbl_location);
            this.menu_group.Controls.Add(this.btn_all);
            this.menu_group.Controls.Add(this.btn_add_);
            this.menu_group.Controls.Add(this.btn_cpu);
            this.menu_group.Controls.Add(this.btn_mbd);
            this.menu_group.Controls.Add(this.btn_mem);
            this.menu_group.Controls.Add(this.btn_vga);
            this.menu_group.Controls.Add(this.btn_mon);
            this.menu_group.Controls.Add(this.btn_psu);
            this.menu_group.Location = new System.Drawing.Point(6, 7);
            this.menu_group.Name = "menu_group";
            this.menu_group.Size = new System.Drawing.Size(948, 88);
            this.menu_group.TabIndex = 6;
            this.menu_group.TabStop = false;
            // 
            // lbRepresentative
            // 
            this.lbRepresentative.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRepresentative.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRepresentative.Appearance.Options.UseFont = true;
            this.lbRepresentative.Appearance.Options.UseTextOptions = true;
            this.lbRepresentative.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbRepresentative.AppearanceDisabled.Options.UseTextOptions = true;
            this.lbRepresentative.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbRepresentative.AppearanceHovered.Options.UseTextOptions = true;
            this.lbRepresentative.AppearanceHovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbRepresentative.AppearancePressed.Options.UseTextOptions = true;
            this.lbRepresentative.AppearancePressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbRepresentative.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbRepresentative.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.lbRepresentative.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lbRepresentative.Location = new System.Drawing.Point(448, 60);
            this.lbRepresentative.Name = "lbRepresentative";
            this.lbRepresentative.Size = new System.Drawing.Size(131, 19);
            this.lbRepresentative.TabIndex = 22;
            this.lbRepresentative.Text = "(생산대행) 입고번호";
            // 
            // btnUpdate
            // 
            this.btnUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.btnUpdate.BackColor = System.Drawing.Color.Gainsboro;
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpdate.ForeColor = System.Drawing.Color.Gray;
            this.btnUpdate.Location = new System.Drawing.Point(883, 57);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(58, 25);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "부품U+";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.EnabledChanged += new System.EventHandler(this.btnUpdate_EnabledChanged);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btn_compelete_
            // 
            this.btn_compelete_.BackColor = System.Drawing.Color.DarkGreen;
            this.btn_compelete_.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btn_compelete_.ForeColor = System.Drawing.Color.White;
            this.btn_compelete_.Location = new System.Drawing.Point(543, 22);
            this.btn_compelete_.Name = "btn_compelete_";
            this.btn_compelete_.Size = new System.Drawing.Size(68, 25);
            this.btn_compelete_.TabIndex = 13;
            this.btn_compelete_.Text = "검수완료";
            this.btn_compelete_.UseVisualStyleBackColor = false;
            this.btn_compelete_.Visible = false;
            this.btn_compelete_.Click += new System.EventHandler(this.Complete_Btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::WareHousingMaster.Properties.Resources.title_img1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(846, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 32);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // btn_set
            // 
            this.btn_set.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_set.Enabled = false;
            this.btn_set.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_set.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_set.Location = new System.Drawing.Point(502, 15);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(35, 38);
            this.btn_set.TabIndex = 8;
            this.btn_set.Text = "SET";
            this.btn_set.UseVisualStyleBackColor = false;
            this.btn_set.Visible = false;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // btn_stg
            // 
            this.btn_stg.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_stg.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_stg.Location = new System.Drawing.Point(235, 18);
            this.btn_stg.Name = "btn_stg";
            this.btn_stg.Size = new System.Drawing.Size(56, 53);
            this.btn_stg.TabIndex = 12;
            this.btn_stg.Text = "STG";
            this.btn_stg.UseVisualStyleBackColor = false;
            this.btn_stg.Click += new System.EventHandler(this.btn_stg_Click);
            // 
            // btn_save_
            // 
            this.btn_save_.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_save_.Enabled = false;
            this.btn_save_.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_save_.ForeColor = System.Drawing.Color.Gray;
            this.btn_save_.Location = new System.Drawing.Point(758, 57);
            this.btn_save_.Name = "btn_save_";
            this.btn_save_.Size = new System.Drawing.Size(62, 25);
            this.btn_save_.TabIndex = 9;
            this.btn_save_.Text = "재고+";
            this.btn_save_.UseVisualStyleBackColor = false;
            this.btn_save_.EnabledChanged += new System.EventHandler(this.btn_save_EnabledChanged);
            this.btn_save_.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // w_numb_txt_
            // 
            this.w_numb_txt_.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.w_numb_txt_.Location = new System.Drawing.Point(583, 57);
            this.w_numb_txt_.Name = "w_numb_txt_";
            this.w_numb_txt_.Size = new System.Drawing.Size(167, 25);
            this.w_numb_txt_.TabIndex = 19;
            // 
            // lbl_location
            // 
            this.lbl_location.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_location.AutoSize = true;
            this.lbl_location.BackColor = System.Drawing.Color.White;
            this.lbl_location.Location = new System.Drawing.Point(818, 9);
            this.lbl_location.Name = "lbl_location";
            this.lbl_location.Size = new System.Drawing.Size(0, 12);
            this.lbl_location.TabIndex = 10;
            this.lbl_location.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_all
            // 
            this.btn_all.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_all.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_all.Location = new System.Drawing.Point(3, 18);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(56, 53);
            this.btn_all.TabIndex = 16;
            this.btn_all.Text = "ALL";
            this.btn_all.UseVisualStyleBackColor = false;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // btn_add_
            // 
            this.btn_add_.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.btn_add_.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_add_.Enabled = false;
            this.btn_add_.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_add_.ForeColor = System.Drawing.Color.Gray;
            this.btn_add_.Location = new System.Drawing.Point(824, 57);
            this.btn_add_.Name = "btn_add_";
            this.btn_add_.Size = new System.Drawing.Size(58, 25);
            this.btn_add_.TabIndex = 8;
            this.btn_add_.Text = "부품+";
            this.btn_add_.UseVisualStyleBackColor = false;
            this.btn_add_.EnabledChanged += new System.EventHandler(this.btn_add_EnabledChanged);
            this.btn_add_.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_cpu
            // 
            this.btn_cpu.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_cpu.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_cpu.Location = new System.Drawing.Point(61, 18);
            this.btn_cpu.Name = "btn_cpu";
            this.btn_cpu.Size = new System.Drawing.Size(56, 53);
            this.btn_cpu.TabIndex = 15;
            this.btn_cpu.Text = "CPU";
            this.btn_cpu.UseVisualStyleBackColor = false;
            this.btn_cpu.Click += new System.EventHandler(this.btn_cpu_Click);
            // 
            // btn_mbd
            // 
            this.btn_mbd.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_mbd.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_mbd.Location = new System.Drawing.Point(119, 18);
            this.btn_mbd.Name = "btn_mbd";
            this.btn_mbd.Size = new System.Drawing.Size(56, 53);
            this.btn_mbd.TabIndex = 14;
            this.btn_mbd.Text = "MBD";
            this.btn_mbd.UseVisualStyleBackColor = false;
            this.btn_mbd.Click += new System.EventHandler(this.btn_mbd_Click);
            // 
            // btn_mem
            // 
            this.btn_mem.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_mem.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_mem.Location = new System.Drawing.Point(177, 18);
            this.btn_mem.Name = "btn_mem";
            this.btn_mem.Size = new System.Drawing.Size(56, 53);
            this.btn_mem.TabIndex = 13;
            this.btn_mem.Text = "MEM";
            this.btn_mem.UseVisualStyleBackColor = false;
            this.btn_mem.Click += new System.EventHandler(this.btn_mem_Click);
            // 
            // btn_vga
            // 
            this.btn_vga.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_vga.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_vga.Location = new System.Drawing.Point(292, 18);
            this.btn_vga.Name = "btn_vga";
            this.btn_vga.Size = new System.Drawing.Size(56, 53);
            this.btn_vga.TabIndex = 11;
            this.btn_vga.Text = "VGA";
            this.btn_vga.UseVisualStyleBackColor = false;
            this.btn_vga.Click += new System.EventHandler(this.btn_vga_Click);
            // 
            // btn_mon
            // 
            this.btn_mon.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_mon.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_mon.Location = new System.Drawing.Point(349, 18);
            this.btn_mon.Name = "btn_mon";
            this.btn_mon.Size = new System.Drawing.Size(56, 53);
            this.btn_mon.TabIndex = 10;
            this.btn_mon.Text = "MON";
            this.btn_mon.UseVisualStyleBackColor = false;
            this.btn_mon.Click += new System.EventHandler(this.btn_mon_Click);
            // 
            // btn_psu
            // 
            this.btn_psu.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_psu.Enabled = false;
            this.btn_psu.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_psu.Location = new System.Drawing.Point(406, 18);
            this.btn_psu.Name = "btn_psu";
            this.btn_psu.Size = new System.Drawing.Size(56, 53);
            this.btn_psu.TabIndex = 9;
            this.btn_psu.Text = "PSU";
            this.btn_psu.UseVisualStyleBackColor = false;
            this.btn_psu.Visible = false;
            this.btn_psu.Click += new System.EventHandler(this.btn_psu_Click);
            // 
            // combo_printer_
            // 
            this.combo_printer_.DisplayMember = "5001";
            this.combo_printer_.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_printer_.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_printer_.Font = new System.Drawing.Font("굴림", 11.25F);
            this.combo_printer_.FormattingEnabled = true;
            this.combo_printer_.Location = new System.Drawing.Point(5, 15);
            this.combo_printer_.Name = "combo_printer_";
            this.combo_printer_.Size = new System.Drawing.Size(119, 23);
            this.combo_printer_.TabIndex = 9;
            this.combo_printer_.ValueMember = "5001";
            // 
            // product_print_
            // 
            this.product_print_.BackColor = System.Drawing.Color.Gainsboro;
            this.product_print_.Enabled = false;
            this.product_print_.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.product_print_.ForeColor = System.Drawing.Color.Black;
            this.product_print_.Location = new System.Drawing.Point(130, 14);
            this.product_print_.Name = "product_print_";
            this.product_print_.Size = new System.Drawing.Size(79, 25);
            this.product_print_.TabIndex = 12;
            this.product_print_.Text = "제품출력";
            this.product_print_.UseVisualStyleBackColor = false;
            this.product_print_.Click += new System.EventHandler(this.product_print__Click);
            // 
            // btn_print_
            // 
            this.btn_print_.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_print_.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_print_.ForeColor = System.Drawing.Color.Black;
            this.btn_print_.Location = new System.Drawing.Point(215, 15);
            this.btn_print_.Name = "btn_print_";
            this.btn_print_.Size = new System.Drawing.Size(84, 25);
            this.btn_print_.TabIndex = 11;
            this.btn_print_.Text = "바코드출력";
            this.btn_print_.UseVisualStyleBackColor = false;
            this.btn_print_.Click += new System.EventHandler(this.btn_print__Click);
            // 
            // selected
            // 
            this.selected.Controls.Add(this.lbType);
            this.selected.Controls.Add(this.dbName);
            this.selected.Controls.Add(this.lbPCType);
            this.selected.Controls.Add(this.lbl_ver_);
            this.selected.Controls.Add(this.summary_grid_);
            this.selected.Location = new System.Drawing.Point(6, 98);
            this.selected.Name = "selected";
            this.selected.Size = new System.Drawing.Size(457, 411);
            this.selected.TabIndex = 7;
            this.selected.TabStop = false;
            // 
            // lbType
            // 
            this.lbType.Appearance.BackColor = System.Drawing.Color.DarkGray;
            this.lbType.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbType.Appearance.Options.UseBackColor = true;
            this.lbType.Appearance.Options.UseFont = true;
            this.lbType.Location = new System.Drawing.Point(13, 387);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(33, 12);
            this.lbType.TabIndex = 23;
            this.lbType.Text = "Type:";
            // 
            // dbName
            // 
            this.dbName.AutoSize = true;
            this.dbName.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.dbName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dbName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dbName.Location = new System.Drawing.Point(314, 387);
            this.dbName.MaximumSize = new System.Drawing.Size(130, 0);
            this.dbName.MinimumSize = new System.Drawing.Size(130, 0);
            this.dbName.Name = "dbName";
            this.dbName.Size = new System.Drawing.Size(130, 12);
            this.dbName.TabIndex = 19;
            this.dbName.Text = "DB: dangol365";
            this.dbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPCType
            // 
            this.lbPCType.Appearance.BackColor = System.Drawing.Color.DarkGray;
            this.lbPCType.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPCType.Appearance.Options.UseBackColor = true;
            this.lbPCType.Appearance.Options.UseFont = true;
            this.lbPCType.Appearance.Options.UseTextOptions = true;
            this.lbPCType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lbPCType.AppearanceDisabled.Options.UseTextOptions = true;
            this.lbPCType.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lbPCType.Location = new System.Drawing.Point(52, 388);
            this.lbPCType.Name = "lbPCType";
            this.lbPCType.Size = new System.Drawing.Size(17, 12);
            this.lbPCType.TabIndex = 22;
            this.lbPCType.Text = "PC";
            // 
            // lbl_ver_
            // 
            this.lbl_ver_.AutoSize = true;
            this.lbl_ver_.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_ver_.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_ver_.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_ver_.Location = new System.Drawing.Point(172, 388);
            this.lbl_ver_.Name = "lbl_ver_";
            this.lbl_ver_.Size = new System.Drawing.Size(114, 12);
            this.lbl_ver_.TabIndex = 18;
            this.lbl_ver_.Text = "Version : 1.0.0.0416";
            this.lbl_ver_.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // summary_grid_
            // 
            this.summary_grid_.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.summary_grid_.Location = new System.Drawing.Point(6, 17);
            this.summary_grid_.Name = "summary_grid_";
            this.summary_grid_.ReadOnly = true;
            this.summary_grid_.RowTemplate.Height = 23;
            this.summary_grid_.Size = new System.Drawing.Size(445, 388);
            this.summary_grid_.TabIndex = 0;
            this.summary_grid_.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_grid__CellClick);
            this.summary_grid_.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.summary_grid__CellContentClick);
            this.summary_grid_.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.summary_grid__CellPainting);
            this.summary_grid_.KeyDown += new System.Windows.Forms.KeyEventHandler(this.summary_grid__KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbCheckList);
            this.groupBox3.Controls.Add(this.btnCheck);
            this.groupBox3.Controls.Add(this.lbl_notice_);
            this.groupBox3.Controls.Add(this.combo_printer_);
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Controls.Add(this.detail_grid_);
            this.groupBox3.Controls.Add(this.btn_print_);
            this.groupBox3.Controls.Add(this.product_print_);
            this.groupBox3.Location = new System.Drawing.Point(469, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(485, 411);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // cbCheckList
            // 
            this.cbCheckList.DisplayMember = "5001";
            this.cbCheckList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCheckList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCheckList.Font = new System.Drawing.Font("굴림", 11.25F);
            this.cbCheckList.FormattingEnabled = true;
            this.cbCheckList.Location = new System.Drawing.Point(306, 16);
            this.cbCheckList.Name = "cbCheckList";
            this.cbCheckList.Size = new System.Drawing.Size(88, 23);
            this.cbCheckList.TabIndex = 22;
            this.cbCheckList.ValueMember = "5001";
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.DarkGreen;
            this.btnCheck.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(398, 16);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(81, 25);
            this.btnCheck.TabIndex = 21;
            this.btnCheck.Text = "검수체크";
            this.btnCheck.UseVisualStyleBackColor = false;
            // 
            // lbl_notice_
            // 
            this.lbl_notice_.AutoSize = true;
            this.lbl_notice_.ForeColor = System.Drawing.Color.Red;
            this.lbl_notice_.Location = new System.Drawing.Point(12, 16);
            this.lbl_notice_.Name = "lbl_notice_";
            this.lbl_notice_.Size = new System.Drawing.Size(0, 12);
            this.lbl_notice_.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.82759F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.17241F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 231F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_product_no, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_superviseno, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_username, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.combo_location_, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 46);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.02041F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.97959F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(476, 49);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(176, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = "부품번호";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(3, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "검수위치";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(176, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "관리번호";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "검수자";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_product_no
            // 
            this.lbl_product_no.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_product_no.AutoSize = true;
            this.lbl_product_no.BackColor = System.Drawing.Color.White;
            this.lbl_product_no.Location = new System.Drawing.Point(247, 25);
            this.lbl_product_no.Name = "lbl_product_no";
            this.lbl_product_no.Size = new System.Drawing.Size(226, 24);
            this.lbl_product_no.TabIndex = 11;
            this.lbl_product_no.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_superviseno
            // 
            this.lbl_superviseno.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_superviseno.AutoSize = true;
            this.lbl_superviseno.BackColor = System.Drawing.Color.AliceBlue;
            this.lbl_superviseno.Location = new System.Drawing.Point(247, 0);
            this.lbl_superviseno.Name = "lbl_superviseno";
            this.lbl_superviseno.Size = new System.Drawing.Size(226, 25);
            this.lbl_superviseno.TabIndex = 9;
            this.lbl_superviseno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_username
            // 
            this.lbl_username.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_username.AutoSize = true;
            this.lbl_username.BackColor = System.Drawing.Color.White;
            this.lbl_username.Location = new System.Drawing.Point(81, 0);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(89, 25);
            this.lbl_username.TabIndex = 8;
            this.lbl_username.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // combo_location_
            // 
            this.combo_location_.DisplayMember = "5001";
            this.combo_location_.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_location_.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_location_.Font = new System.Drawing.Font("굴림", 11.25F);
            this.combo_location_.FormattingEnabled = true;
            this.combo_location_.Location = new System.Drawing.Point(81, 28);
            this.combo_location_.Name = "combo_location_";
            this.combo_location_.Size = new System.Drawing.Size(89, 23);
            this.combo_location_.TabIndex = 14;
            this.combo_location_.ValueMember = "5001";
            // 
            // detail_grid_
            // 
            this.detail_grid_.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detail_grid_.Location = new System.Drawing.Point(3, 102);
            this.detail_grid_.Name = "detail_grid_";
            this.detail_grid_.ReadOnly = true;
            this.detail_grid_.RowTemplate.Height = 23;
            this.detail_grid_.Size = new System.Drawing.Size(476, 303);
            this.detail_grid_.TabIndex = 0;
            this.detail_grid_.KeyDown += new System.Windows.Forms.KeyEventHandler(this.detail_grid__KeyDown);
            // 
            // WM_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 508);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.selected);
            this.Controls.Add(this.menu_group);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WM_GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WareHousingMaster";
            this.Load += new System.EventHandler(this.WM_GUI_Load);
            this.menu_group.ResumeLayout(false);
            this.menu_group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.selected.ResumeLayout(false);
            this.selected.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.summary_grid_)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detail_grid_)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox menu_group;
        private System.Windows.Forms.Button btn_stg;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.TextBox w_numb_txt_;
        private System.Windows.Forms.Button btn_cpu;
        private System.Windows.Forms.Button btn_mbd;
        private System.Windows.Forms.Button btn_mem;
        private System.Windows.Forms.Button btn_vga;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.Button btn_mon;
        private System.Windows.Forms.Button btn_psu;
        private System.Windows.Forms.GroupBox selected;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView detail_grid_;
        private System.Windows.Forms.DataGridView summary_grid_;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_save_;
        private System.Windows.Forms.Button btn_add_;
        private System.Windows.Forms.Label lbl_product_no;
        private System.Windows.Forms.Label lbl_location;
        private System.Windows.Forms.Label lbl_superviseno;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_notice_;
        private System.Windows.Forms.Button btn_print_;
        private System.Windows.Forms.Button product_print_;
        private System.Windows.Forms.Label lbl_ver_;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox combo_printer_;
        private System.Windows.Forms.Button btn_compelete_;
        private System.Windows.Forms.ComboBox combo_location_;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ComboBox cbCheckList;
        private System.Windows.Forms.Label dbName;
        private System.Windows.Forms.Button btnUpdate;
        private DevExpress.XtraEditors.LabelControl lbType;
        private DevExpress.XtraEditors.LabelControl lbPCType;
        private DevExpress.XtraEditors.LabelControl lbRepresentative;
    }
}

