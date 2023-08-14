/// <summary>
/// WM_GUI 클래스중 UI 관련 기능을 따로 분리하여 놓음.
/// </summary>
/// 
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using WareHousingMaster.view.inventory;
using WareHousingMaster.view.login;
using WareHousingMaster.view.representative;

namespace WareHousingMaster
{
    public partial class WM_GUI : Form
    {

        private string _version = "";
        //Dictionary<string, short> _DicNtbCheck;
        string[] ComponetCd = new string[] { "CPU", "MBD", "MEM", "VGA", "STG", "MON", "CAS", "ADP", "POW", "KEY", "MOU", "FAN", "CAB", "BAT" };
        public string _representativeType { get; private set; }


        int selected_button_ = (int)PART_NAME.CPU;
        Button past_button_ = new Button();

        string _userId;
        string _userPasswd;

        public WM_GUI()
        {
            InitializeComponent();

            //_DicNtbCheck = new Dictionary<string, short>();
        }

        class DictionaryInfo
        {
            public DictionaryInfo(string db_name, string table_name)
            {
                this.db_name = db_name;
                this.table_name = table_name;
            }

            public string db_name { get; set; }

            public string table_name { get; set; }

            // 값 저장
            public string value;
        }

        // 기기정보 관련 
        private Dictionary<string, int> _dicPartCnt = new Dictionary<string, int>();
        Dictionary<string, DictionaryInfo> device_info_dic_ = new Dictionary<string, DictionaryInfo>();
        Dictionary<string, string> barcode_list_ = new Dictionary<string, string>();
        Dictionary<string, short> _DicNtbCheck = new Dictionary<string, short>();
        string _caseDestroyDescription = "";
        string _batteryRemain = "";
        Dictionary<string, short> _DicAllInOneCheck = new Dictionary<string, short>();
        Dictionary<string, Dictionary<string, int>> _dicInventoryCheck = new Dictionary<string, Dictionary<string, int>>();

        DeviceDatasController data_controller;

        DateTime _warehousingDate = new DateTime();

        // all 상태일때 전체선택 check 박스를 grid 위에 그려줄지 여부 확인 
        CheckBox all_cb_ = new CheckBox();

        //bool reload_ = false;
        int past_index_ = 0;

        /// <summary>
        /// 화면에 표출될 Version String 반환하는 함수
        /// 추후 버전 변경시에는 properties/AssemblyInfo.cs 파일에서만 버전 변경하면 UI 전체에 적용됨
        /// </summary>
        /// <returns></returns>
        string GetVersionString()
        {
            string program_version = "";

            // 
            Assembly assemObj = Assembly.GetExecutingAssembly();
            Version v = assemObj.GetName().Version;
            _version = v.ToString();

#if DEBUG
            program_version = "V " + v.ToString() + "d";
#else
            program_version = "V " + v.ToString();
#endif

            return program_version;
        }

        /// <summary>
        /// 프로그램 실행시에 실행되는 윈도우 기본함수.  
        /// 처음 화면을 생성할 때 ShowAll 함수를 이곳에서 실행하도록 하였음.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void WM_GUI_Load(object sender, EventArgs e)
        {
            // assembly load하여 version 설정
            string program_version = GetVersionString();

            // loading popup 버전 표출
            LoadingForm loading_frm = new LoadingForm(program_version);

            // 로딩 폼 보이기
            loading_frm.Show();


            // 로딩폼 가운데로 위치 변경
            loading_frm.Location = new Point(this.Location.X + (this.Size.Width / 2) - (loading_frm.Size.Width / 2),
                this.Location.Y + (this.Size.Height / 2) - (loading_frm.Size.Height / 2));

            InitWM_GUI(program_version);

            loading_frm.Close();

        }

        /// <summary>
        /// 초기 UI 화면 설정 및 표출
        /// </summary>
        /// <param name="program_version"></param>
        void InitWM_GUI(string program_version)
        {
            Cursor.Current = Cursors.WaitCursor;

            // login.ini에서 가져올 데이터
            string location = "";
            string printer = "";

            bool autoLogin = false;

            // ini file 에서 login id, password location 정보 가져오기
            if (GetINIInfo(ref _userId, ref _userPasswd, ref autoLogin, ref location, ref printer))
            {
                // send login data                 
                //if (!SendUserCheckRequest(id, pw))

                if (!autoLogin)
                {
                    //usrControlLogin login = new usrControlLogin(this, _userId, _userPasswd);

                    //login.StartPosition = FormStartPosition.Manual;
                    //login.Location = new Point(this.Location.X + (this.Size.Width / 2) - (login.Size.Width / 2),
                    //this.Location.Y + (this.Size.Height / 2) - (login.Size.Height / 2));



                    //if (login.ShowDialog(this) == DialogResult.OK)
                    //{
                    //    _userId = login._userId;
                    //    _userPasswd = login._userPasswd;
                    //}
                    //else
                    //    ExitProgram();
                }
                else
                {
                    //if (!SendUserCheckRequest(_userId, _userPasswd))
                    //{
                    //    MessageBox.Show("아이디 또는 비밀번호가 올바르지 않습니다.");

                    //    usrControlLogin login = new usrControlLogin(this, _userId, _userPasswd, false);

                    //    login.StartPosition = FormStartPosition.Manual;
                    //    login.Location = new Point(this.Location.X + (this.Size.Width / 2) - (login.Size.Width / 2),
                    //    this.Location.Y + (this.Size.Height / 2) - (login.Size.Height / 2));

                    //    if (login.ShowDialog(this) == DialogResult.OK)
                    //    {
                    //        _userId = login._userId;
                    //        _userPasswd = login._userPasswd;
                    //    }
                    //    else
                    //        ExitProgram();
                    //}
                }

                //if (!SendUserCheckRequest(_userId, _userPasswd))
                //{
                //ExitProgram();
                //}
                //else
                {
                    lbl_ver_.Text = program_version;
#if DEBUG
                    dbName.Text = "DB: InventoryTest";
#else
                    dbName.Text = "DB: dangol365";
#endif

                    lbl_username.Text = _userId;

                    // set printer combo
                    InitPrinterComboBox(printer);

                    // set location combo
                    InitLocationComboBox(location);

                    // set printer combo
                    InitCheckComboBox(printer);

                    Dictionary<string, string> dicVersion = getVersion(_userId);
                    string version = dicVersion["VERSION"];

                    if (!_version.Equals(version))
                    {
                        string content = dicVersion["CONTENT"];

                        if (MessageBox.Show(
                                               $@"프로그램이 업데이트 되었습니다. 최신 버전을 다운로드 하세요.
[ 최신버전: {version} ]
[ 업데이트 내용: {content}]
*'예'를 누르면 다운로드 링크로 이동합니다.
*'아니오'를 누르면 기존버전으로 실행됩니다.
", "Version", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start($"{{ProjectInfo._url}}");
                            ExitProgram();
                        }

                        //MessageBox.Show($"검수 프로그램이 업데이트 되었습니다. 최신 버전을 다운로드 해 주세요.\n[ 최신버전: {version} ]");
                    }

                    // dll load
                    data_controller = new DeviceDatasController();
                    if (!data_controller.LoadCPUZDll())
                    {
                        MessageBox.Show($"CPUZ DLL을 로드하는 과정에서 오류가 발생했습니다. 관리자에게 문의하세요.");
                        ExitProgram();
                    }




                    // PC 정보 가져오기
                    List<DeviceInfo> device_info_list = new List<DeviceInfo>();

                    //bool isSuccess = data_controller.GetPCInfo(ref device_info_list, ComponetCd);
                    //_dicPartCnt = data_controller._deviceCnt;
                    //SendPartInfo(data_controller._msg, device_info_list);

                    //if (!isSuccess) // device data read
                    //{
                    //    MessageBox.Show($"부품정보를 로드하는 과정에서 오류가 발생했습니다. 관리자에게 문의하세요.\n{data_controller._msg}");
                    //    ExitProgram();
                    //}

                    // PC정보들을 저장하기 위한 dictionary 생성(dbname, table name, value 구조)
                    InitDictionary();

                    // 가져온 데이터를 dictionary에 데이터 저장
                    SetDictionary(device_info_list);


                    // UI 컨트롤 초기화.
                    InintControls();


                    using (DlgSelectRepresentativeType dlgRepresentativeType = new DlgSelectRepresentativeType())
                    {
                        dlgRepresentativeType.ShowDialog(this);

                        _representativeType = dlgRepresentativeType._representativeType;
                    }

                    if (string.IsNullOrEmpty(_representativeType))
                    {
                        MessageBox.Show("입출고 정보가 선택되지 않았습니다.");
                        ExitProgram();
                    }

                    if (_representativeType.Equals("W"))
                    {
                        lbRepresentative.Text = "입고번호";
                    }
                    else if (_representativeType.Equals("C"))
                    {
                        lbRepresentative.Text = "(생산대행) 입고번호";
                    }
                    else if (_representativeType.Equals("O"))
                    {
                        lbRepresentative.Text = "출고번호";
                        cbCheckList.Visible = false;
                        btnCheck.Visible = false;
                    }
                    else if (_representativeType.Equals("P"))
                    {
                        lbRepresentative.Text = "접수번호";
                        cbCheckList.Visible = false;
                        btnCheck.Visible = false;
                    }




                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("로그인 정보가 없습니다.");

                ExitProgram();
            }

            
        }

        private void SendPartInfo(string msg, List<DeviceInfo> partInfo)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, int> items in _dicPartCnt)
            {
                string key = items.Key;
                int value = items.Value;

                sb.Append($"{key}:{value}/");
            }

            foreach (DeviceInfo part in partInfo)
            {
                string key = part.name;
                string value = part.value;
                if (string.IsNullOrEmpty(value))
                    value = "NA";

                sb.Append($"{key}:{value}/");
            }

            SendPartInfo(msg, sb);

        }

        void InitPrinterComboBox(string default_value)
        {
            if (string.IsNullOrEmpty(default_value))
                default_value = "5000";
            else
            {
                string[] st = default_value.Split(' ');
                default_value = st[st.Length - 1];

            }
            try
            {
                // dictionary 생성
                Dictionary<string, string> printer_port_dic = new Dictionary<string, string>();
                printer_port_dic.Add("PORT 5000", "5000");
                printer_port_dic.Add("PORT 5001", "5001");
                printer_port_dic.Add("PORT 5002", "5002");
                printer_port_dic.Add("PORT 5003", "5003");
                printer_port_dic.Add("PORT 5004", "5004");
                printer_port_dic.Add("PORT 5005", "5005");
                printer_port_dic.Add("PORT 5006", "5006");
                printer_port_dic.Add("PORT 5007", "5007");
                printer_port_dic.Add("PORT 5008", "5008");
                printer_port_dic.Add("PORT 5009", "5009");

                // 데이터 바인딩
                combo_printer_.DataSource = new BindingSource(printer_port_dic, null);
                combo_printer_.DisplayMember = "Key";
                combo_printer_.ValueMember = "Value";

                // 기본 값 설정
                int default_index = combo_printer_.FindString($"PORT {default_value}");
                combo_printer_.SelectedIndex = default_index;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            if (combo_printer_.SelectedValue == null)
                combo_printer_.SelectedValue = "5000";
        }

        void InitCheckComboBox(string default_value)
        {
            try
            {
                Dictionary<string, string> dicCheckList = new Dictionary<string, string>();
                dicCheckList.Add("노트북", "NTB");
                dicCheckList.Add("일체형PC", "ALLINONE");
                dicCheckList.Add("모니터", "MON");
                dicCheckList.Add("부품", "INVENTORY");
                //dicCheckList.Add("PRINTER 5004", "5004");
                //dicCheckList.Add("PRINTER 5005", "5005");

                string[] keys = new string[] { "노트북", "일체형PC", "모니터", "부품" };
                string[] values = new string[] { "NTB", "ALLINONE", "MON", "INVENTORY" };

                DataTable dt = new DataTable();
                dt.Columns.Add("key", typeof(string));
                dt.Columns.Add("Value", typeof(string));


                for (int i = 0; i < keys.Length; i++)
                {
                    DataRow row = dt.NewRow();
                    row["key"] = keys[i];
                    row["Value"] = values[i];
                    dt.Rows.Add(row);
                }

                // 데이터 바인딩
                //cbCheckList.DataSource = new BindingSource(dicCheckList, null);
                cbCheckList.DataSource = dt;
                cbCheckList.DisplayMember = "Key";
                cbCheckList.ValueMember = "Value";

                // 기본 값 설정
                int defaultindex = cbCheckList.FindString("노트북");
                cbCheckList.SelectedIndex = defaultindex;


            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 서버에서 Location 목록을 가져와서 comboBox 에 셋팅
        /// </summary>
        /// <param name="default_value"></param>
        void InitLocationComboBox(string default_value)
        {
            try
            {
                // json object 생성
                JObject jobj = new JObject();

                jobj.Add("CODE", "CD0104"); // 코드값 고정

                string url = "/common/getCodeList.json";
                string result = SendRequestMessage(jobj, url);

                // 결과값 추가, 성공 실패 여부 확인 없이 리턴값으로 추가            
                JArray jArray = JArray.Parse(result);

                // 재고위치 데이터 받을 dictionary 생성            
                Dictionary<string, string> location_dic = new Dictionary<string, string>();
                foreach (JObject element in jArray)
                {
                    location_dic.Add(element["CODE_CD"].ToString(), element["CODE_NM"].ToString());
                }

                // 데이터 바인딩
                combo_location_.DataSource = new BindingSource(location_dic, null);
                combo_location_.DisplayMember = "Value";
                combo_location_.ValueMember = "Key";

                // 기본 값 설정
                int default_index = combo_location_.FindString(location_dic[default_value]);
                combo_location_.SelectedIndex = default_index;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ExitProgram()
        {
            Application.ExitThread();
            Environment.Exit(0);
        }

        /// <summary>
        /// 기기정보를 담고 관리하는 device_info_dic_를 초기화 한다.
        /// Key 값은 부품명을 사용하고 Value는 DictionaryInfo 클래스의 인스턴스를 사용한다.
        /// </summary>
        void InitDictionary()
        {

            try
            {
                //etc
                device_info_dic_.Add("TYPE", new DictionaryInfo("PC_TYPE", "PC 타입"));
                // cpu          
                device_info_dic_.Add("CPU_MANUFACTURE", new DictionaryInfo("MANUFACTURE_NM", "제조사"));
                device_info_dic_.Add("CPU_CODENAME", new DictionaryInfo("CODE_NM", "코드명"));
                device_info_dic_.Add("CPU_MODEL", new DictionaryInfo("MODEL_NM", "모델명"));
                device_info_dic_.Add("CPU_SPEC", new DictionaryInfo("SPEC_NM", "상세정보"));
                device_info_dic_.Add("CPU_SOCKET", new DictionaryInfo("SOCKET_NM", "소켓"));
                device_info_dic_.Add("CPU_CORE", new DictionaryInfo("CORE_CNT", "코어"));
                device_info_dic_.Add("CPU_THREAD", new DictionaryInfo("THREAD_CNT", "쓰레드"));

                device_info_dic_.Add("CPU_END", new DictionaryInfo("", ""));

                // barcode
                barcode_list_.Add("CPU_BARCODE", "");

                // mainboard
                device_info_dic_.Add("MBD_MANUFACTURE", new DictionaryInfo("MANUFACTURE_NM", "제조사"));
                device_info_dic_.Add("MBD_NB", new DictionaryInfo("NB_NM", "칩셋"));
                device_info_dic_.Add("MBD_CODE", new DictionaryInfo("CODE_NM", "코드명"));
                device_info_dic_.Add("MBD_SB", new DictionaryInfo("SB_NM", "SB"));
                device_info_dic_.Add("MBD_MODEL", new DictionaryInfo("MODEL_NM", "모델명"));
                device_info_dic_.Add("MBD_MAX_MEM_CNT", new DictionaryInfo("MAX_MEM", "최대장착 RAM 수"));
                device_info_dic_.Add("MBD_DIMM", new DictionaryInfo("NO_OF_DIMM", "최대장착 RAM "));
                device_info_dic_.Add("MBD_MEM_TYPE", new DictionaryInfo("MEM_TYPE", "RAM종류"));
                device_info_dic_.Add("MBD_SKU", new DictionaryInfo("SKU_NM", "SKU"));
                device_info_dic_.Add("MBD_SERIAL", new DictionaryInfo("SERIAL_NO", "시리얼"));
                device_info_dic_.Add("MBD_FAMILY", new DictionaryInfo("FAMILY_NM", "FAMILY"));

                device_info_dic_.Add("MBD_MODEL_NAME", new DictionaryInfo("MBD_MODEL_NM", "모델명1"));
                device_info_dic_.Add("MBD_SYSTEM_PRODUCT_NAME", new DictionaryInfo("PRODUCT_NAME", "제품명"));
                device_info_dic_.Add("MBD_REVISION", new DictionaryInfo("REVISION", "리비전"));
                device_info_dic_.Add("MBD_SYSTEM_VERSION", new DictionaryInfo("SYSTEM_VERSION", "시스템 버전"));
                device_info_dic_.Add("MBD_BIOS_VENDOR", new DictionaryInfo("BIOS_VENDOR", "BIOS VENDOR"));
                device_info_dic_.Add("MBD_BIOS_VERSION", new DictionaryInfo("BIOS_VERSION", "BIOS VERSION"));
                device_info_dic_.Add("MBD_BIOS_DATE", new DictionaryInfo("BIOS_DATE", "BIOS DATE"));
                device_info_dic_.Add("MBD_UUID", new DictionaryInfo("UUID", "UUID"));
                device_info_dic_.Add("MBD_CHASSIS_MANUFACTURER", new DictionaryInfo("CHASSIS_MANUFACTURER", "CHASSIS 제조사"));
                device_info_dic_.Add("MBD_CHASSIS_SN", new DictionaryInfo("CHASSIS_SN", "CHASSIS 시리얼"));

                device_info_dic_.Add("MBD_PROCESSOR_SOCKET", new DictionaryInfo("PROCESSOR_SOCKET", "프로세서 소켓"));
                device_info_dic_.Add("MBD_SN", new DictionaryInfo("MBD_SN", "메인보드 시리얼"));
                device_info_dic_.Add("MBD_SYSTEM_SN", new DictionaryInfo("SYSTEM_SN", "시스템 시리얼"));
                device_info_dic_.Add("MBD_MEM_SIZE", new DictionaryInfo("MEM_SIZE", "MEM 사이즈"));
                device_info_dic_.Add("MBD_NO_OF_MEM_DEVICE", new DictionaryInfo("NO_OF_MEM_DEVICE", "MEM 디바이스 수"));

                device_info_dic_.Add("MBD_END", new DictionaryInfo("", ""));


                // barcode
                barcode_list_.Add("MBD_BARCODE", "");

                int memCnt = _dicPartCnt["MEM"];
                // memory 
                for (int i = 0; i < memCnt; i++)
                {
                    device_info_dic_.Add("MEM_" + i + "_MANUFACTURE", new DictionaryInfo("MANUFACTURE_NM", "제조사"));
                    device_info_dic_.Add("MEM_" + i + "_TYPE", new DictionaryInfo("MEM_TYPE", "메모리 타입"));
                    device_info_dic_.Add("MEM_" + i + "_MODEL", new DictionaryInfo("MODEL_NM", "모델명"));
                    device_info_dic_.Add("MEM_" + i + "_MODULE", new DictionaryInfo("MODULE_NM", "모듈"));
                    device_info_dic_.Add("MEM_" + i + "_SIZE", new DictionaryInfo("CAPACITY", "크기"));
                    device_info_dic_.Add("MEM_" + i + "_BANDWIDTH", new DictionaryInfo("BANDWIDTH", "대역"));
                    device_info_dic_.Add("MEM_" + i + "_MANUFACTURE_DT", new DictionaryInfo("MANUFACTURE_DT", "제조일"));
                    device_info_dic_.Add("MEM_" + i + "_VOLT", new DictionaryInfo("VOLTAGE", "전압"));
                    device_info_dic_.Add("MEM_" + i + "_SERIAL", new DictionaryInfo("SERIAL_NO", "시리얼"));
                    device_info_dic_.Add("MEM_" + i + "_SHLEE_SIZE", new DictionaryInfo("MEM_SHLEE_SIZE", "사이즈"));
                    device_info_dic_.Add("MEM_" + i + "_SHLEE_SZTYPE", new DictionaryInfo("_SHLEE_SZTYPE", "타입"));
                    device_info_dic_.Add("MEM_" + i + "_END", new DictionaryInfo("", ""));

                    // barcode 
                    barcode_list_.Add("MEM_" + i + "_BARCODE", "");
                }

                int stgCnt = _dicPartCnt["STG"];

                // storage 
                for (int i = 0; i < stgCnt; i++)
                {
                    device_info_dic_.Add("STG_" + i + "_MODEL", new DictionaryInfo("MODEL_NM", "모델명"));
                    device_info_dic_.Add("STG_" + i + "_TYPE", new DictionaryInfo("STG_TYPE", "종류"));
                    device_info_dic_.Add("STG_" + i + "_CAPACITY", new DictionaryInfo("CAPACITY", "용량"));
                    device_info_dic_.Add("STG_" + i + "_SERIAL", new DictionaryInfo("SERIAL_NO", "시리얼"));
                    device_info_dic_.Add("STG_" + i + "_BUS_TYPE", new DictionaryInfo("BUS_TYPE", "버스종류"));
                    device_info_dic_.Add("STG_" + i + "_FEATURE", new DictionaryInfo("FEATURE", "특징 "));
                    //device_info_dic_.Add("STG_" + i + "_VOLUME", new DictionaryInfo("VOLUME", ""));
                    device_info_dic_.Add("STG_" + i + "_SPEED", new DictionaryInfo("SPEED", "회전속도"));
                    device_info_dic_.Add("STG_" + i + "_REVISION", new DictionaryInfo("REVISION", "리비전"));
                    device_info_dic_.Add("STG_" + i + "_END", new DictionaryInfo("", ""));

                    // barcode
                    barcode_list_.Add("STG_" + i + "_BARCODE", "");
                }

                int vgaCnt = _dicPartCnt["VGA"];
                // vga
                for (int i = 0; i < vgaCnt; i++)
                {
                    device_info_dic_.Add($"VGA_{i}_MANUFACTURE", new DictionaryInfo("MANUFACTURE_NM", "제조사"));
                    device_info_dic_.Add($"VGA_{i}_MODEL", new DictionaryInfo("MODEL_NM", "모델명"));
                    device_info_dic_.Add($"VGA_{i}_REVISION", new DictionaryInfo("REVISION", "REVISION"));
                    device_info_dic_.Add($"VGA_{i}_CODENAME", new DictionaryInfo("CODE_NM", "코드명"));
                    device_info_dic_.Add($"VGA_{i}_PROCESS", new DictionaryInfo("PROCESS", "공정"));
                    device_info_dic_.Add($"VGA_{i}_MEM_SIZE", new DictionaryInfo("CAPACITY", "VRAM 크기"));
                    device_info_dic_.Add($"VGA_{i}_MEM_TYPE", new DictionaryInfo("MEM_TYPE", "VRAM 종류"));
                    device_info_dic_.Add($"VGA_{i}_MEM_VENDOR", new DictionaryInfo("TECH_NM", "VRAM 제조사"));
                    device_info_dic_.Add($"VGA_{i}_SERIAL", new DictionaryInfo("SERIAL_NO", "시리얼"));
                    device_info_dic_.Add($"VGA_{i}_END", new DictionaryInfo("", ""));

                    barcode_list_.Add($"VGA_{i}_BARCODE", "");
                }


                int monCnt = _dicPartCnt["MON"];
                // monitor 0, 1, 2
                for (int i = 0; i < monCnt; i++)
                {
                    device_info_dic_.Add("MON_" + i + "_VENDOR", new DictionaryInfo("MANUFACTURE_NM", "제조사"));
                    device_info_dic_.Add("MON_" + i + "_MODEL", new DictionaryInfo("MODEL_NM", "모델명"));
                    device_info_dic_.Add("MON_" + i + "_ID", new DictionaryInfo("MODEL_ID", "ID"));
                    device_info_dic_.Add("MON_" + i + "_SIZE", new DictionaryInfo("SIZE", "화면크기"));
                    device_info_dic_.Add("MON_" + i + "_RESOLUTION", new DictionaryInfo("RESOLUTION", "해상도"));
                    device_info_dic_.Add("MON_" + i + "_MANUFACTURE_DT", new DictionaryInfo("MANUFACTURED_DT", "제조일"));
                    device_info_dic_.Add("MON_" + i + "_SERIAL", new DictionaryInfo("SERIAL_NO", "시리얼"));
                    device_info_dic_.Add("MON_" + i + "_DEVICE_NAME", new DictionaryInfo("DEVICE_NAME", "디바이스명"));
                    device_info_dic_.Add("MON_" + i + "_GAMMA", new DictionaryInfo("GAMMA", "GAMMA"));
                    device_info_dic_.Add("MON_" + i + "_MAX_PIXEL", new DictionaryInfo("MAX_PIXEL", "MAX PIXEL"));
                    device_info_dic_.Add("MON_" + i + "_END", new DictionaryInfo("", ""));

                    // barcode
                    barcode_list_.Add("MON_" + i + "_BARCODE", "");
                }
            }
            catch
            {
                MessageBox.Show("Init Dictionary Error");
            }
        }

        /// <summary>
        /// device_info_list_의 name값과 일치하는 Key값을 가진, 
        /// device_info_dic_ 의 element에 기기정보를 입력한다.
        /// </summary>
        /// <param name="device_info_list_">기기정보 리스트</param>
        /// <returns></returns>
        private bool SetDictionary(List<DeviceInfo> partInfo)
        {
            string key = "";
            try
            {
                foreach (DeviceInfo part in partInfo)
                {
                    key = part.name;
                    string value = part.value;
                   
                    if (device_info_dic_.ContainsKey(key))
                    {
                        if (string.IsNullOrEmpty(value))
                            value = "";
                        else
                        {
                            string check = value.ToUpper();
                            if (check.Equals("TO BE FILLED BY O.E.M."))
                                value = "";
                        }
                       
                        device_info_dic_[key].value = value;
                    }
                    else
                    {

                    }

                    //device_info_dic_[device_info_list_[i].name].value = device_info_list_[i].value;
                }

                key = "TYPE";

                if (device_info_dic_.ContainsKey("TYPE"))
                {
                    string type = device_info_dic_["TYPE"].value;

                    if (!string.IsNullOrEmpty(type))
                    {
                        if (device_info_dic_["TYPE"].value.ToUpper().Equals("DESKTOP"))
                        {
                            device_info_dic_["TYPE"].value = "1";
                            lbPCType.Text = "PC";
                        }
                        else if (device_info_dic_["TYPE"].value.ToUpper().Equals("LAPTOP") || device_info_dic_["TYPE"].value.ToUpper().Equals("NOTEBOOK"))
                        {
                            device_info_dic_["TYPE"].value = "2";
                            lbPCType.Text = "노트북";
                        }
                        else if (device_info_dic_["TYPE"].value.ToUpper().Equals("ALLINONE"))
                        {
                            device_info_dic_["TYPE"].value = "3";
                            lbPCType.Text = "일체형PC";
                        }
                        else
                        {
                            device_info_dic_["TYPE"].value = "1";
                            lbPCType.Text = "PC";
                        }
                    }
                    else
                    {
                        device_info_dic_["TYPE"].value = "0";
                        lbPCType.Text = "Unknown";
                    }
                }
            }
            catch (Exception e)
            {
                SendPartInfo(key, new StringBuilder(e.Message.ToString()));

                if(key.Equals("TYPE"))
                {
                    device_info_dic_["TYPE"].value = "0";
                    lbPCType.Text = "Unknown";
                    MessageBox.Show("디바이스 TYPE을 가져오는 과정에서 오류가 발생했지만 검수는 정상적으로 진행 가능합니다.");
                    return true;
                }

                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }

        private void InintControls()
        {
            InitSummaryGrid();
            InitDetailGrid();
        }

        private void InitSummaryGrid()
        {
            summary_grid_.RowHeadersVisible = false;
            summary_grid_.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void InitDetailGrid()
        {
            detail_grid_.RowHeadersVisible = false;
            detail_grid_.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        internal bool ChangeControls(object sender)
        {
            btn_add_.Enabled = false;
            btn_save_.Enabled = false;
            btnUpdate.Enabled = false;

            // clear grid
            lbl_notice_.Text = "";

            ClearGrid(summary_grid_);
            ClearGrid(detail_grid_);

            // set button color        
            if (!SetButtonColor(sender))
            {
                return true;
            }

            // set column name
            if (!SetSummaryColumnsName())
            {
                return false;
            }

            // set summary datas
            if (!SetSummaryData())
            {
                return false;
            }

            if (!SetDetailDatas())
            {
                return false;
            }

            return true;
        }

        void ClearGrid(DataGridView grid)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();

            lbl_product_no.Text = "";

            if (grid != detail_grid_)
            {
                lbl_superviseno.Text = "";
            }

        }

        bool SetButtonColor(object sender)
        {
            Button selected_button = (Button)sender;

            all_cb_.Checked = false;

            // 현재 선택된 버튼과 이전에 선택된 버튼이 같을 때 버튼색 기본색으로 변경, past button 초기화
            if (selected_button == past_button_)
            {
                selected_button.BackColor = Color.Gainsboro;
                past_button_ = new Button();

                // alll check box 숨기기
                all_cb_.Visible = false;

                // 바코드출력 disable
                btn_print_.Enabled = false;

                // 제품출력 disable
                product_print_.Enabled = false;


                return false;
            }
            else
            {
                selected_button.BackColor = Color.Gray;
                past_button_.BackColor = Color.Gainsboro;
                past_button_ = (Button)sender;

                btn_print_.Enabled = true;

                return true;
            }
        }

        void SetSummaryGridSize(DataGridView grid, int column_count, int size0, int size1, int size2, int size3, int size4)
        {

            grid.ColumnCount = column_count + 1; // + no. column

            if (size0 > 0)
            {
                grid.Columns[0].Width = size0;
            }

            if (size1 > 0)
            {
                grid.Columns[1].Width = size1;
            }

            if (size2 > 0)
            {
                grid.Columns[2].Width = size2;
            }

            if (size3 > 0)
            {
                grid.Columns[3].Width = size3;
            }

            if (size4 > 0)
            {
                grid.Columns[4].Width = size4;
            }

            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        internal bool SetSummaryColumnsName()
        {
            bool result = false;
            string[] column_names;
            try
            {
                switch (selected_button_)
                {
                    case (int)PART_NAME.ALL: // all 일 때 체크박스 추가 필요
                        column_names = new string[] { "부품명", "부품정보1", "부품정보2", "부품정보3" }; // 컬럼명 합치기 안됨
                        SetSummaryGridSize(summary_grid_, column_names.Length, 30, 80, 0, 0, 0);
                        break;

                    case (int)PART_NAME.CPU:
                        column_names = new string[]
                        {
                            device_info_dic_["CPU_CODENAME"].table_name,
                            device_info_dic_["CPU_SPEC"].table_name,
                            device_info_dic_["CPU_SOCKET"].table_name,
                            //device_info_dic_["CPU_THREAD"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 20, 90, 230, 0, 0);
                        break;
                    case (int)PART_NAME.MBD:
                        column_names = new string[]
                        {
                            device_info_dic_["MBD_MANUFACTURE"].table_name,
                            device_info_dic_["MBD_MODEL"].table_name,
                            device_info_dic_["MBD_MAX_MEM_CNT"].table_name,
                            device_info_dic_["MBD_SKU"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 20, 100, 150, 0, 0);
                        break;
                    case (int)PART_NAME.MEM:
                        column_names = new string[]
                        {
                            device_info_dic_["MEM_0_MANUFACTURE"].table_name,
                            device_info_dic_["MEM_0_SIZE"].table_name,
                            device_info_dic_["MEM_0_BANDWIDTH"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 20, 180, 0, 0, 0);
                        break;
                    case (int)PART_NAME.STG:
                        column_names = new string[]
                        {
                            device_info_dic_["STG_0_TYPE"].table_name,
                            device_info_dic_["STG_0_MODEL"].table_name,
                            device_info_dic_["STG_0_CAPACITY"].table_name,
                            device_info_dic_["STG_0_BUS_TYPE"].table_name,
                            device_info_dic_["STG_0_SPEED"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 20, 80, 150, 0, 0);
                        break;
                    case (int)PART_NAME.VGA:
                        column_names = new string[]
                        {
                            device_info_dic_["VGA_0_MANUFACTURE"].table_name,
                            device_info_dic_["VGA_0_MODEL"].table_name,
                            device_info_dic_["VGA_0_MEM_TYPE"].table_name,
                            device_info_dic_["VGA_0_MEM_SIZE"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 20, 100, 180, 0, 0);
                        break;
                    case (int)PART_NAME.MON:
                        column_names = new string[]
                        {
                            device_info_dic_["MON_0_MODEL"].table_name,
                            device_info_dic_["MON_0_ID"].table_name,
                            device_info_dic_["MON_0_RESOLUTION"].table_name,
                            device_info_dic_["MON_0_SIZE"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 20, 120, 0, 0, 0);
                        break;
                    case (int)PART_NAME.PSU:
                        column_names = new string[]
                        {
                            device_info_dic_["CPU_CODENAME"].table_name,
                            device_info_dic_["CPU_SPEC"].table_name,
                            device_info_dic_["CPU_SOCKET"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 30, 100, 300, 0, 0);
                        break;
                    case (int)PART_NAME.SET:
                        column_names = new string[]
                        {
                            device_info_dic_["CPU_CODENAME"].table_name,
                            device_info_dic_["CPU_SPEC"].table_name,
                            device_info_dic_["CPU_SOCKET"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 30, 100, 300, 0, 0);
                        break;
                    default:
                        column_names = new string[]
                        {
                            device_info_dic_["CPU_CODENAME"].table_name,
                            device_info_dic_["CPU_SPEC"].table_name,
                            device_info_dic_["CPU_SOCKET"].table_name
                        };
                        SetSummaryGridSize(summary_grid_, column_names.Length, 30, 100, 300, 0, 0);
                        break;
                }

                for (int i = 0; i < column_names.Length; i++)
                {
                    summary_grid_.Columns[i + 1].Name = column_names[i];
                    summary_grid_.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return result;
            }
        }

        private bool SetSummaryData()
        {
            try
            {
                int index = 1;
                switch (selected_button_)
                {
                    case (int)PART_NAME.ALL:
                        SetAllData();
                        break;
                    case (int)PART_NAME.CPU:
                        summary_grid_.Rows.Add(
                            index,
                            device_info_dic_["CPU_CODENAME"].value,
                            device_info_dic_["CPU_SPEC"].value,
                            device_info_dic_["CPU_SOCKET"].value);
                        _dicPartCnt["CPU"] = 1;
                        break;
                    case (int)PART_NAME.MBD:
                        summary_grid_.Rows.Add(
                            index,
                            device_info_dic_["MBD_MANUFACTURE"].value,
                            device_info_dic_["MBD_MODEL"].value,
                            device_info_dic_["MBD_MAX_MEM_CNT"].value,
                            device_info_dic_["MBD_SKU"].value);
                        _dicPartCnt["MBD"] = 1;
                        break;
                    case (int)PART_NAME.MEM:
                        int memCnt = _dicPartCnt["MEM"];
                        _dicPartCnt["MEM"] = 0;
                        for (int i = 0; i < memCnt; i++)
                        {
                            string manufac = device_info_dic_["MEM_" + i + "_MANUFACTURE"].value;
                            string size = device_info_dic_["MEM_" + i + "_SIZE"].value;
                            string bandwidth = device_info_dic_["MEM_" + i + "_BANDWIDTH"].value;
                            if (!(string.IsNullOrEmpty(manufac) && string.IsNullOrEmpty(size) && string.IsNullOrEmpty(bandwidth)))
                            {
                                _dicPartCnt["MEM"] = _dicPartCnt["MEM"] + 1;
                                summary_grid_.Rows.Add(index++, manufac, size, bandwidth);
                            }
                        }
                        break;
                    case (int)PART_NAME.STG:
                        int stgCnt = _dicPartCnt["STG"];
                        _dicPartCnt["STG"] = 0;
                        for (int i = 0; i < stgCnt; i++)
                        {
                            string type = device_info_dic_["STG_" + i + "_TYPE"].value;
                            string model = device_info_dic_["STG_" + i + "_MODEL"].value;
                            string capacity = device_info_dic_["STG_" + i + "_CAPACITY"].value;
                            string bus_type = device_info_dic_["STG_" + i + "_BUS_TYPE"].value;
                            string speed = device_info_dic_["STG_" + i + "_SPEED"].value;
                            if (!(string.IsNullOrEmpty(type) && string.IsNullOrEmpty(model) && string.IsNullOrEmpty(capacity) && string.IsNullOrEmpty(bus_type) && string.IsNullOrEmpty(speed)))
                            {
                                _dicPartCnt["STG"] = _dicPartCnt["STG"] + 1;
                                summary_grid_.Rows.Add(index++, type, model, capacity, bus_type, speed);
                            }
                        }
                        break;
                    case (int)PART_NAME.VGA:
                        int vgaCnt = _dicPartCnt["VGA"];
                        _dicPartCnt["VGA"] = 0;
                        for (int i = 0; i < vgaCnt; i++)
                        {

                            string manufact = device_info_dic_[$"VGA_{i}_MANUFACTURE"].value;
                            string model = device_info_dic_[$"VGA_{i}_MODEL"].value;
                            string memType = device_info_dic_[$"VGA_{i}_MEM_TYPE"].value;
                            string memSize = device_info_dic_[$"VGA_{i}_MEM_SIZE"].value;

                            if (!(string.IsNullOrEmpty(manufact) && string.IsNullOrEmpty(model) && string.IsNullOrEmpty(model) && string.IsNullOrEmpty(memType) && string.IsNullOrEmpty(memSize)))
                            {
                                _dicPartCnt["VGA"] = _dicPartCnt["VGA"] + 1;
                                summary_grid_.Rows.Add(index++, manufact, model, model, memType, memSize);
                            }
                        }
                        break;
                    case (int)PART_NAME.MON:
                        int monCnt = _dicPartCnt["MON"];
                        _dicPartCnt["MON"] = 0;
                        for (int i = 0; i < monCnt; i++)
                        {
                            string model = device_info_dic_["MON_" + i + "_MODEL"].value;
                            string id = device_info_dic_["MON_" + i + "_ID"].value;
                            string resolution = device_info_dic_["MON_" + i + "_RESOLUTION"].value;
                            string size = device_info_dic_["MON_" + i + "_SIZE"].value;

                            //MessageBox.Show($"{model},  {id},  {resolution},  {size}");

                            if (!(string.IsNullOrEmpty(model) && string.IsNullOrEmpty(id) && string.IsNullOrEmpty(resolution) && string.IsNullOrEmpty(size)))
                            {
                                _dicPartCnt["MON"] = _dicPartCnt["MON"] + 1;
                                summary_grid_.Rows.Add(index++, model, id, resolution, size);

                            }
                        }
                        break;
                    case (int)PART_NAME.PSU:
                        break;
                    case (int)PART_NAME.SET:
                        break;
                    default:
                        break;
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return false;
            }
        }

        private void SetAllData()
        {
            try
            {
                // check box 컬럼 추가
                DataGridViewCheckBoxColumn check_column = new DataGridViewCheckBoxColumn();
                summary_grid_.Columns.Add(check_column);

                int index = 1;

                // CPU  
                summary_grid_.Rows.Add(index++, "CPU", device_info_dic_["CPU_CODENAME"].value,
                    device_info_dic_["CPU_SPEC"].value, device_info_dic_["CPU_SOCKET"].value);

                // MBD
                summary_grid_.Rows.Add(
                    index++,
                    "MBD",
                    device_info_dic_["MBD_MANUFACTURE"].value,
                    device_info_dic_["MBD_MODEL"].value,
                device_info_dic_["MBD_SKU"].value);

                int memCnt = _dicPartCnt["MEM"];
                for (int i = 0; i < memCnt; i++)
                {
                    string manufac = device_info_dic_["MEM_" + i + "_MANUFACTURE"].value;
                    string bandwidth = device_info_dic_["MEM_" + i + "_TYPE"].value;
                    string size = device_info_dic_["MEM_" + i + "_SIZE"].value;
                    if (!(string.IsNullOrEmpty(manufac) && string.IsNullOrEmpty(size) && string.IsNullOrEmpty(bandwidth)))
                    {
                        summary_grid_.Rows.Add(index++, "MEM_" + i, manufac, bandwidth, size);
                    }
                }

                int stgCnt = _dicPartCnt["STG"];
                for (int i = 0; i < stgCnt; i++)
                {
                    string type = device_info_dic_["STG_" + i + "_TYPE"].value;
                    string model = device_info_dic_["STG_" + i + "_MODEL"].value;
                    string capacity = device_info_dic_["STG_" + i + "_CAPACITY"].value;
                    if (!(string.IsNullOrEmpty(type) && string.IsNullOrEmpty(model) && string.IsNullOrEmpty(capacity)))
                    {
                        summary_grid_.Rows.Add(index++, "STG_" + i, type, model, capacity);
                    }
                }

                int vgaCnt = _dicPartCnt["VGA"];
                for (int i = 0; i < vgaCnt; i++)
                {
                    string manufact = device_info_dic_[$"VGA_{i}_MANUFACTURE"].value;
                    string model = device_info_dic_[$"VGA_{i}_MODEL"].value;
                    string memType = device_info_dic_[$"VGA_{i}_MEM_TYPE"].value;
                    string memSize = device_info_dic_[$"VGA_{i}_MEM_SIZE"].value;

                    if (!(string.IsNullOrEmpty(manufact) && string.IsNullOrEmpty(model) && string.IsNullOrEmpty(model) && string.IsNullOrEmpty(memType) && string.IsNullOrEmpty(memSize)))
                    {
                        summary_grid_.Rows.Add(index++, "VGA_" + i, manufact, model, memSize);
                    }
                }

                int monCnt = _dicPartCnt["MON"];
                for (int i = 0; i < monCnt; i++)
                {
                    string model = device_info_dic_["MON_" + i + "_MODEL"].value;
                    string resolution = device_info_dic_["MON_" + i + "_RESOLUTION"].value;
                    string size = device_info_dic_["MON_" + i + "_SIZE"].value;
                    if (!string.IsNullOrEmpty(resolution) && !string.IsNullOrEmpty(size))
                    {
                        summary_grid_.Rows.Add(index++, "MON_" + i, model, resolution, size);
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void data_grid__CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != past_index_)
            {
                lbl_superviseno.Text = "";
            }

            SetDetailDatas();
            past_index_ = e.RowIndex;
        }

        private bool SetDetailDatas()
        {
            // clear
            lbl_notice_.Text = "";
            ClearGrid(detail_grid_);

            // column number 가져오기 
            if (summary_grid_.RowCount > 1)
            {
                int selected_index = summary_grid_.CurrentCell.RowIndex;
                if (summary_grid_[0, selected_index].Value != null) // 비어있는지 체크
                {
                    List<string> spec_name_list = new List<string>(); // 한글 이름
                    List<string> device_info_list = new List<string>(); // PC에서 가져온 정보
                    JObject matching_info_obj = new JObject();// api에서 리턴받은 정보

                    string column_names = "";
                    string part_type = GetPartType(); // 부품타입
                    string end_key = GetEndKey(ref part_type); // Dictionary Key값 중 해당 부품의 마지막 Key값을 가져온다.

                    // Key값을 이용하여 Local Device Data 가져오기
                    if (!GetSpecAndDeviceInfoList(part_type, end_key, ref spec_name_list, ref device_info_list, ref column_names))
                    {
                        return false;
                    }

                    // component 테이블과 매칭하여 PC의 부품 모델명을과 모델명이 일치하는 부품정보 가져오기
                    bool add_btn_state = false;
                    if (!SendCheckComponentRequest(part_type, ref matching_info_obj, ref add_btn_state))
                    {
                        return false;
                    }

                    // set column name
                    btn_add_.Enabled = add_btn_state;
                    btnUpdate.Enabled = !add_btn_state;

                    // set add btn & barcode num(lbl_superviseno)
                    SetBarcodeNumber(part_type);

                    SetDetailGridColumn();

                    if (selected_button_ == (int)PART_NAME.ALL)
                    {
                        product_print_.Enabled = true;
                    }
                    else
                    {
                        product_print_.Enabled = false;
                    }

                    // input data
                    if (SetDetailDatas(column_names, spec_name_list, device_info_list, matching_info_obj))
                    {
                        return false;
                    }
                }
                else
                {
                    lbl_notice_.Text = "";
                    ClearGrid(detail_grid_);
                }
            }

            return true;
        }

        void SetBarcodeNumber(string part_type)
        {
            if (barcode_list_[part_type + "_BARCODE"].Length > 0)
            {
                lbl_superviseno.Text = barcode_list_[part_type + "_BARCODE"];
                btn_save_.Enabled = false;
            }
            else
            {
                lbl_superviseno.Text = "";
                btn_save_.Enabled = true;
            }

            if (selected_button_ == (int)PART_NAME.ALL)
            {
                btn_save_.Enabled = true;
            }
        }

        private bool GetSpecAndDeviceInfoList(string part_type, string end_key, ref List<string> spec_list, ref List<string> device_info, ref string column_names)
        {
            try
            {
                foreach (KeyValuePair<string, DictionaryInfo> temp in device_info_dic_)
                {
                    if (temp.Key.Contains(part_type))
                    {
                        spec_list.Add(temp.Value.table_name);
                        device_info.Add(temp.Value.value);
                        column_names += temp.Value.db_name;

                        if (temp.Key.Equals(end_key))
                            break;

                        column_names += ",";
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("[GetSpecAndDeviceInfoList]::" + e.Message);

                return false;
            }

            return true;
        }

        private bool SetDetailDatas(string column_names, List<string> spec_list, List<string> device_info, JObject matching_info)
        {
            try
            {
                string[] columns = column_names.Split(',');

                for (int i = 0; i < spec_list.Count; i++)
                {
                    detail_grid_.Rows.Add(spec_list[i], device_info[i], matching_info[columns[i]]);
                }

                detail_grid_.Rows.RemoveAt(detail_grid_.Rows.Count - 2);
                detail_grid_.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                detail_grid_.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("[SetDetailDatas]::" + e.Message);

                return false;
            }
        }

        private void SetDetailGridColumn()
        {
            detail_grid_.ColumnCount = 3;
            detail_grid_.Columns[0].Name = "SPEC";
            detail_grid_.Columns[1].Name = "제품정보";
            detail_grid_.Columns[2].Name = "매칭정보";
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            // 선택된 행에 값이 없을 때 아무동작 안함
            int rowIndex = summary_grid_.CurrentRow.Index;
            if (summary_grid_.Rows[rowIndex].Cells[0].Value == null)
            {
                return;
            }
            //else if (selected_button_ > 1)
            //{
            //    MessageBox.Show("부품 추가는 한개씩만 가능합니다.");
            //    return;
            //}

            if (!AddDeviceInfo("CPN", _representativeType))
            {
                return;
            }
            else
            {
                btn_add_.Enabled = false;
                SetDetailDatas();
                MessageBox.Show("부품 정보가 추가되었습니다.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 선택된 행에 값이 없을 때 아무동작 안함
            int rowIndex = summary_grid_.CurrentRow.Index;
            if (summary_grid_.Rows[rowIndex].Cells[0].Value == null)
            {
                return;
            }
            //else if(selected_button_ > 1)
            //{
            //    MessageBox.Show("부품 수정은 한개씩만 가능합니다.");
            //    return;
            //}

            if (!UpdateDeviceInfo("UPT", _representativeType))
            {
                return;
            }
            else
            {
                btn_add_.Enabled = false;
                SetDetailDatas();
            }
        }


        bool CheckInputBox()
        {
            if (w_numb_txt_.Text.Length <= 0)
            {
                MessageBox.Show("입고번호가 입력되지 않았습니다.");

                return false;
            }

            return true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            // 선택된 행에 값이 없을 때 아무동작 안함      
            if (!CheckInputBox())
            {
                return;
            }

            if (selected_button_ == (int)PART_NAME.ALL)
            {
                AddSelectIVT();
            }
            else
            {
                AddIVT();
            }
        }

        private void AddIVT()
        {
            int rowIndex = summary_grid_.CurrentRow.Index;
            if (summary_grid_.Rows[rowIndex].Cells[0].Value == null)
            {
                return;
            }

            if (!AddDeviceInfo("IVT", _representativeType))
            {
                return;
            }
            else
            {
                if (SetDetailDatas())
                {
                    MessageBox.Show("재고 정보가 추가되었습니다.");
                }
            }
        }

        private void AddSelectIVT()
        {
            // 전체 grid에서 체크 되어 있는 값 확인 (마지막 한줄은 빈 줄ㅇ)
            string fail_list = "";
            string success_list = "";

            for (int i = 0; i < summary_grid_.Rows.Count - 1; i++)
            {
                // 체크박스 선택된 상태 &  바코드가 없으면 
                if (Convert.ToBoolean(summary_grid_[5, i].Value))
                {
                    string part_type = summary_grid_[1, i].Value.ToString();
                    if (barcode_list_[part_type + "_BARCODE"].Length <= 0)
                    {
                        if (!SendDeviceInfo("IVT", part_type, _representativeType))
                        {
                            fail_list += part_type + ",";
                        }
                        else
                        {
                            success_list += part_type + ",";
                        }
                    }
                }
            }

            if (success_list.Length <= 0 && fail_list.Length <= 0)
            {
                return;
            }

            string msg = "";
            if (success_list.Length > 0)
            {
                msg = "성공 : " + success_list.Substring(0, success_list.Length - 1);
            }

            if (fail_list.Length > 0)
            {
                msg = msg + "실패 : " + fail_list.Substring(0, fail_list.Length - 1);
            }

            MessageBox.Show(msg, "재고추가 결과");
        }


        private void btn_all_Click(object sender, EventArgs e)
        {
            selected_button_ = (int)PART_NAME.ALL;
            ChangeControls(sender);
        }

        private void btn_cpu_Click(object sender, EventArgs e)
        {
            selected_button_ = (int)PART_NAME.CPU;
            ChangeControls(sender);
        }

        private void btn_mbd_Click(object sender, EventArgs e)
        {
            selected_button_ = (int)PART_NAME.MBD;
            ChangeControls(sender);
        }

        private void btn_mem_Click(object sender, EventArgs e)
        {
            selected_button_ = (int)PART_NAME.MEM;
            ChangeControls(sender);
        }

        private void btn_stg_Click(object sender, EventArgs e)
        {
            selected_button_ = (int)PART_NAME.STG;
            ChangeControls(sender);
        }

        private void btn_vga_Click(object sender, EventArgs e)
        {
            selected_button_ = (int)PART_NAME.VGA;
            ChangeControls(sender);
        }

        private void btn_mon_Click(object sender, EventArgs e)
        {
            selected_button_ = (int)PART_NAME.MON;
            ChangeControls(sender);
        }

        private void btn_psu_Click(object sender, EventArgs e)
        {
            selected_button_ = (int)PART_NAME.PSU;
            ChangeControls(sender);
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            ChangeControls(sender);
        }

        private void summary_grid__KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void detail_grid__KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_add_EnabledChanged(object sender, EventArgs e)
        {
            btn_add_.BackColor = btn_add_.Enabled == false ? Color.Gainsboro : Color.RoyalBlue;
            btn_add_.ForeColor = btn_add_.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btn_save_EnabledChanged(object sender, EventArgs e)
        {
            btn_save_.BackColor = btn_save_.Enabled == false ? Color.Gainsboro : Color.Red;
            btn_save_.ForeColor = btn_save_.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnUpdate_EnabledChanged(object sender, EventArgs e)
        {
            btnUpdate.BackColor = btnUpdate.Enabled == false ? Color.Gainsboro : Color.LightGreen;
            btnUpdate.ForeColor = btnUpdate.Enabled == false ? Color.LightGray : Color.White;
        }


        private void btn_print__Click(object sender, EventArgs e)
        {

            if (selected_button_ == (int)PART_NAME.ALL)
            {
                printMulti();
            }
            else
            {
                printOne();
            }
        }


        private void printMulti()
        {
            // 전체 grid에서 체크 되어 있는 값 확인 (마지막 한줄은 빈 줄ㅇ)

            if (w_numb_txt_.Text.Trim().Equals(""))
            {
                MessageBox.Show("입고번호를 확인하여 주세요");
                return;
            }


            string url = "/print/print.json";
            JObject result_obj;
            int checkedCnt = 0;
            int printedCnt = 0;
            try
            {

                for (int i = 0; i < summary_grid_.Rows.Count - 1; i++)
                {
                    // 체크박스 선택된 상태 &  바코드가 없으면 
                    if (Convert.ToBoolean(summary_grid_[5, i].Value))
                    {
                        checkedCnt++;

                        string part_type = summary_grid_[1, i].Value.ToString();
                        if (barcode_list_[part_type + "_BARCODE"].Length > 0)
                        {
                            
                            string barcode = barcode_list_[part_type + "_BARCODE"];

                            JObject jobj = new JObject();

                            jobj.Add("USER_ID", lbl_username.Text);
                            jobj.Add("BARCODE", barcode);

                            //jobj.Add("WAREHOUSING", w_numb_txt_.Text);

                            if (_representativeType.Equals("O"))
                                jobj.Add("RELEASES", w_numb_txt_.Text);
                            else if (_representativeType.Equals("W"))
                                jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                            else if (_representativeType.Equals("C"))
                                jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                            else if (_representativeType.Equals("P"))
                                jobj.Add("RECEIPT", w_numb_txt_.Text);
                            else
                                jobj.Add("WAREHOUSING", w_numb_txt_.Text);

                            jobj.Add("REPRESENTATIVE_TYPE", _representativeType);

                            string result = SendRequestMessage(jobj, url);

                            result_obj = JObject.Parse(result);
                            if (result_obj["SUCCESS"].ToString().Equals("True"))
                                printedCnt++;
                        }
                    }
                }

                MessageBox.Show($"선택하신 {checkedCnt}개 품목 중 {printedCnt}개가 프린트 완료되었습니다.");

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }

        private void printOne()
        { 
            if (lbl_superviseno.Text == null)
            {
                MessageBox.Show("관리번호를 확인하여 주세요");

                return;
            }
            else if (w_numb_txt_.Text.Trim().Equals(""))
            {
                MessageBox.Show("입고번호를 확인하여 주세요");
            }
            else
            {
                try
                {
                    string url = "/print/print.json";

                    JObject jobj = new JObject();

                    jobj.Add("USER_ID", lbl_username.Text);
                    jobj.Add("BARCODE", lbl_superviseno.Text);
                    if (_representativeType.Equals("O"))
                        jobj.Add("RELEASES", w_numb_txt_.Text);
                    else if (_representativeType.Equals("W"))
                        jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                    else if (_representativeType.Equals("C"))
                        jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                    else if (_representativeType.Equals("P"))
                        jobj.Add("RECEIPT", w_numb_txt_.Text);
                    else
                        jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                    //jobj.Add("WAREHOUSING", w_numb_txt_.Text);

                    jobj.Add("REPRESENTATIVE_TYPE", _representativeType);

                    string result = SendRequestMessage(jobj, url);

                    JObject result_obj = JObject.Parse(result);

                    MessageBox.Show(result_obj["MSG"].ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void summary_grid__CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (summary_grid_.Columns[e.ColumnIndex].Index == 5) // max 5
            {
                bool isChecked = (bool)summary_grid_[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                summary_grid_.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !isChecked;
                summary_grid_.EndEdit();
            }
        }

        private void summary_grid__CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((selected_button_ == (int)PART_NAME.ALL))
            {
                if (e.ColumnIndex == 5 && e.RowIndex == -1)
                {
                    e.PaintBackground(e.ClipBounds, false);

                    Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

                    int nChkBoxWidth = 15;
                    int nChkBoxHeight = 15;
                    int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
                    int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

                    pt.X += offsetx;
                    pt.Y += offsety;


                    all_cb_.Size = new Size(nChkBoxWidth, nChkBoxHeight);
                    all_cb_.Location = pt;
                    all_cb_.CheckedChanged += new EventHandler(gvSheetListCheckBox_CheckedChanged);

                    ((DataGridView)sender).Controls.Add(all_cb_);

                    e.Handled = true;
                }

                all_cb_.Visible = true;
            }
            else
            {
                all_cb_.Visible = false;
            }
        }

        private void gvSheetListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in summary_grid_.Rows)
            {
                r.Cells[5].Value = ((CheckBox)sender).Checked;
            }
        }

        private void product_print__Click(object sender, EventArgs e)
        {
            PrintBarcode();
        }

        void PrintBarcode()
        {
            try
            {
                if (!CheckInputBox())
                {
                    return;
                }

                JObject jobj = new JObject();

                if (!MakeProductObject(ref jobj))
                {
                    MessageBox.Show("제품 오브젝트 생산 실패 ");
                }

                if (_representativeType.Equals("O"))
                    jobj.Add("RELEASES", w_numb_txt_.Text);
                else if (_representativeType.Equals("W"))
                    jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                else if (_representativeType.Equals("C"))
                    jobj.Add("WAREHOUSING", w_numb_txt_.Text);
                else if (_representativeType.Equals("P"))
                    jobj.Add("RECEIPT", w_numb_txt_.Text);
                else
                    jobj.Add("WAREHOUSING", w_numb_txt_.Text);

                jobj.Add("REPRESENTATIVE_TYPE", _representativeType);

                jobj.Add(device_info_dic_["TYPE"].db_name, device_info_dic_["TYPE"].value);

                string url = "/print/printProduct.json";

                string result = SendRequestMessage(jobj, url);

                JObject result_obj = JObject.Parse(result);

                MessageBox.Show(result_obj["MSG"].ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        bool MakeProductObject(ref JObject jobj)
        {
            try
            {
                string mem_values = "";
                string stg_values = "";
                string mon_values = "";
                string vga_values = "";

                for (int i = 0; i < summary_grid_.Rows.Count - 1; i++)
                {
                    // 체크한 것만 
                    if (Convert.ToBoolean(summary_grid_[5, i].Value))
                    {
                        string part_type = summary_grid_[1, i].Value.ToString();
                        if (part_type.Length <= 3) //part type이 3글자일 때 
                        {
                            jobj.Add(part_type, barcode_list_[part_type + "_BARCODE"]);
                        }
                        else
                        {
                            if (part_type.Substring(0, 3).Equals("MEM"))
                            {
                                mem_values += barcode_list_[part_type + "_BARCODE"] + ",";
                            }

                            if (part_type.Substring(0, 3).Equals("STG"))
                            {
                                stg_values += barcode_list_[part_type + "_BARCODE"] + ",";
                            }

                            if (part_type.Substring(0, 3).Equals("MON"))
                            {
                                mon_values += barcode_list_[part_type + "_BARCODE"] + ",";
                            }

                            if (part_type.Substring(0, 3).Equals("VGA"))
                            {
                                vga_values += barcode_list_[part_type + "_BARCODE"] + ",";
                            }
                        }
                    }
                }

                // mem, stg, mon  jobj에 추가
                if (mem_values.Length > 0)
                {
                    jobj.Add("MEM", mem_values);
                }

                if (stg_values.Length > 0)
                {
                    jobj.Add("STG", stg_values);
                }

                if (mon_values.Length > 0)
                {
                    jobj.Add("MON", mon_values);
                }

                if (vga_values.Length > 0)
                {
                    jobj.Add("VGA", vga_values);
                }

                // 입고번호 추가
                //jobj.Add("WAREHOUSING", w_numb_txt_.Text);

                // user id 추가
                jobj.Add("USER_ID", lbl_username.Text);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("제품 오브젝트 생산 실패 : " + e.Message);
                return false;
            }
        }

        private void Complete_Btn_Click(object sender, EventArgs e)
        {

            try
            {
                if (!CheckInputBox())
                {
                    return;
                }

                JObject jobj = new JObject();

                jobj.Add("USER_ID", lbl_username.Text);
                jobj.Add("WAREHOUSING", w_numb_txt_.Text);

                string url = "/produce/WarehousingExamineComplete.json";

                string result = SendRequestMessage(jobj, url);

                JObject result_obj = JObject.Parse(result);

                MessageBox.Show(result_obj["MSG"].ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private List<string> GetInventorysExceptBMD()
        {
            List<string> listInventory = new List<string>();

            try
            {
                if (!string.IsNullOrEmpty(barcode_list_["CPU_BARCODE"]))
                    listInventory.Add(barcode_list_["CPU_BARCODE"]);

                //if (string.IsNullOrEmpty(barcode_list_["MBD_BARCODE"]))
                //    listInventory.Add(barcode_list_["MBD_BARCODE"]);

                int memCnt = _dicPartCnt["MEM"];
                // memory 0, 1, 2, 3, 4, 5, 6, 7
                for (int i = 0; i < memCnt; i++)
                {
                    if (!string.IsNullOrEmpty(barcode_list_[$"MEM_{i}_BARCODE"]))
                        listInventory.Add(barcode_list_[$"MEM_{i}_BARCODE"]);
                }

                int stgCnt = _dicPartCnt["STG"];
                // storage 0, 2, 3, 4
                for (int i = 0; i < stgCnt; i++)
                {
                    if (!string.IsNullOrEmpty(barcode_list_[$"STG_{i}_BARCODE"]))
                        listInventory.Add(barcode_list_[$"STG_{i}_BARCODE"]);
                }

                int vgaCnt = _dicPartCnt["VGA"];
                for (int i = 0; i < vgaCnt; i++)
                {
                    // vga
                    if (!string.IsNullOrEmpty(barcode_list_[$"VGA_{i}_BARCODE"]))
                        listInventory.Add(barcode_list_[$"VGA_{i}_BARCODE"]);
                }


                int monCnt = _dicPartCnt["MON"];
                // monitor 0, 1, 2
                for (int i = 0; i < monCnt; i++)
                {
                    if (!string.IsNullOrEmpty(barcode_list_[$"MON_{i}_BARCODE"]))
                        listInventory.Add(barcode_list_[$"MON_{i}_BARCODE"]);
                }
            }
            catch
            {
                MessageBox.Show("Init Dictionary Error");
                return null;
            }

            return listInventory;
        }

        //private void btnCheck_Click(object sender, EventArgs e)
        //{
        //    string warehousing = w_numb_txt_.Text.ToUpper();

        //    if (string.IsNullOrEmpty(warehousing))
        //    {
        //        MessageBox.Show($"입고번호가 입력되지 않았습니다.");
        //        return;
        //    }
        //    else
        //    {
        //        if(!getWarehousingInfo(warehousing, ref _warehousingDate))
        //        {
        //            return;
        //        }
        //    }



        //    if (combo_printer_.SelectedValue == null)
        //        combo_printer_.SelectedValue = "5000";

        //    string checkValue = ConvertUtil.ToString(cbCheckList.SelectedValue);

        //    if (checkValue.Equals("NTB"))
        //    {
        //        if(!lbPCType.Text.Equals("노트북"))
        //        {
        //            if (MessageBox.Show($"제품 타입과 선택한 타입이 다릅니다. 계속하시겠습니까?\n(제품:{lbPCType.Text}, 선택:노트북)", "노트북 검수 체크", MessageBoxButtons.YesNo) == DialogResult.No)
        //                return;
        //        }

        //        object oBarcode = barcode_list_["MBD_BARCODE"];
        //        string barcode = ConvertUtil.ToString(oBarcode);

        //        if (string.IsNullOrEmpty(barcode))
        //        {
        //            MessageBox.Show("노트북 검수체크는 재고등록후 후에 가능합니다.(메인보드 필수 등록)");
        //            return;
        //        }

        //        List<string> listInventory = GetInventorysExceptBMD();

        //        if (listInventory == null)
        //        {
        //            MessageBox.Show("오류가 발생했습니다. 관리자에게 문의하세요.");
        //            return;
        //        }

        //        if (listInventory.Count < 1)
        //        {
        //            MessageBox.Show("등록된 재고가 없습니다. 재고등록후 검수해 주세요.");
        //            return;
        //        }
        //        else
        //        {
        //            if (MessageBox.Show($"등록된 재고가 {listInventory.Count + 1}개 입니다. 검수를 진행 하시겠습니까?", "노트북 검수 체크", MessageBoxButtons.YesNo) == DialogResult.No)
        //            {
        //                return;
        //            }
        //        }
                

        //        if (warehousing.Equals("B200806001") || warehousing.Equals("B201030004") || _warehousingDate > Convert.ToDateTime("2020-11-11"))
        //        {
        //            using (DlgNtb2ndEditionCheck ntbCheck = new DlgNtb2ndEditionCheck(barcode, _DicNtbCheck, _caseDestroyDescription, _batteryRemain, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (ntbCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    _caseDestroyDescription = ntbCheck._caseDestroyDescription;
        //                    _batteryRemain = ntbCheck._batteryRemain;
        //                    InsertNtbCheck2ndEdition(barcode, _DicNtbCheck, listInventory, _caseDestroyDescription, _batteryRemain);
                            
        //                    if (ntbCheck._isPrint)
        //                        PrintNtbCheck2ndEdition(barcode, _DicNtbCheck, _caseDestroyDescription, _batteryRemain, ntbCheck._port);

        //                }
        //            }
        //        }
        //        else
        //        {
        //            using (DlgNtbCheck ntbCheck = new DlgNtbCheck(barcode, _DicNtbCheck, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (ntbCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertNtbCheck(barcode, _DicNtbCheck, listInventory);

        //                    if (ntbCheck._isPrint)
        //                        PrintNtbCheck(barcode, _DicNtbCheck, ntbCheck._port);

        //                }
        //            }
        //        }
        //    }
        //    else if (checkValue.Equals("ALLINONE"))
        //    {
        //        if (!lbPCType.Text.Equals("일체형PC"))
        //        {
        //            if (MessageBox.Show($"제품 타입과 선택한 타입이 다릅니다. 계속하시겠습니까?\n(제품:{lbPCType.Text}, 선택:일체형PC)", "올인원 검수 체크", MessageBoxButtons.YesNo) == DialogResult.No)
        //                return;
        //        }

        //        object oBarcode = barcode_list_["MBD_BARCODE"];
        //        string barcode = ConvertUtil.ToString(oBarcode);

        //        if (string.IsNullOrEmpty(barcode))
        //        {
        //            MessageBox.Show("일체형PC 검수체크는 재고등록후 후에 가능합니다.(메인보드 필수 등록)");
        //            return;
        //        }

        //        List<string> listInventory = GetInventorysExceptBMD();

        //        if (listInventory == null)
        //        {
        //            MessageBox.Show("오류가 발생했습니다. 관리자에게 문의하세요.");
        //            return;
        //        }

        //        if (listInventory.Count < 1)
        //        {
        //            MessageBox.Show("등록된 재고가 없습니다. 재고등록후 검수해 주세요.");
        //            return;
        //        }
        //        else
        //        {
        //            if (MessageBox.Show($"등록된 재고가 {listInventory.Count + 1}개 입니다. 검수를 진행 하시겠습니까?", "일체형PC 검수 체크", MessageBoxButtons.YesNo) == DialogResult.No)
        //            {
        //                return;
        //            }
        //        }


        //        using (DlgAllInOneCheck allInOneCheck = new DlgAllInOneCheck(barcode, _DicAllInOneCheck, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //        {
        //            if (allInOneCheck.ShowDialog(this) == DialogResult.OK)
        //            {
        //                InsertAllInOneCheck(barcode, _DicAllInOneCheck, listInventory);

        //                if (allInOneCheck._isPrint)
        //                    PrintAllInOneCheck(barcode, _DicAllInOneCheck, allInOneCheck._port);
        //            }
        //        }
        //    }
        //    else if (checkValue.Equals("MON"))
        //    {
        //        string component = lbl_product_no.Text;
        //        string barcode = lbl_superviseno.Text;



        //        if (string.IsNullOrWhiteSpace(component))
        //        {
        //            MessageBox.Show("검수체크는 재고등록후 후에 가능합니다.");
        //            return;
        //        }

        //        if (!component.Substring(0, 3).Equals("MON"))
        //        {
        //            MessageBox.Show("모니터 검수는 모니터만 가능합니다.");
        //            return;
        //        }

        //        if (string.IsNullOrEmpty(barcode))
        //        {
        //            MessageBox.Show("검수체크는 재고등록후 후에 가능합니다.");
        //            return;
        //        }


        //        Dictionary<string, int> dicMonitor;

        //        if (_dicInventoryCheck.ContainsKey(barcode))
        //            dicMonitor = _dicInventoryCheck[barcode];
        //        else
        //        {
        //            Dictionary<string, int> dic = new Dictionary<string, int>();
        //            _dicInventoryCheck.Add(barcode, dic);
        //            dicMonitor = _dicInventoryCheck[barcode];
        //        }


        //        using (DlgMonitorCheck monitorCheck = new DlgMonitorCheck(barcode, dicMonitor, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //        {
        //            if (monitorCheck.ShowDialog(this) == DialogResult.OK)
        //            {
        //                InsertMonitorCheck(barcode, dicMonitor);

        //                if (monitorCheck._isPrint)
        //                    PrintMonitorCheck(barcode, dicMonitor, checkValue, monitorCheck._port);
        //            }
        //        }
        //    }

        //    else if (checkValue.Equals("INVENTORY"))
        //    {
        //        string component = lbl_product_no.Text;
        //        string barcode = lbl_superviseno.Text;

        //        if (string.IsNullOrWhiteSpace(component))
        //        {
        //            MessageBox.Show("검수체크는 재고등록후 후에 가능합니다.");
        //            return;
        //        }

        //        string componentCd = component.Substring(0, 3);

        //        if (string.IsNullOrEmpty(barcode))
        //        {
        //            MessageBox.Show("검수체크는 재고등록후 후에 가능합니다.");
        //            return;
        //        }

        //        Dictionary<string, int> dicInventory;

        //        if (_dicInventoryCheck.ContainsKey(barcode))
        //            dicInventory = _dicInventoryCheck[barcode];
        //        else
        //        {
        //            Dictionary<string, int> dic = new Dictionary<string, int>();
        //            _dicInventoryCheck.Add(barcode, dic);
        //            dicInventory = _dicInventoryCheck[barcode];
        //        }


        //        if (componentCd.Equals("MON"))
        //        {
        //            using (DlgMonitorCheck monitorCheck = new DlgMonitorCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (monitorCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertMonitorCheck(barcode, dicInventory);

        //                    if (monitorCheck._isPrint)
        //                        PrintMonitorCheck(barcode, dicInventory, componentCd, monitorCheck._port);
        //                }
        //            }
        //        }
        //        else if (componentCd.Equals("CAS"))
        //        {
        //            using (DlgCasCheck inventoryCheck = new DlgCasCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertCasCheck(barcode, dicInventory);

        //                    if (inventoryCheck._isPrint)
        //                        PrintCasCheck(barcode, dicInventory, componentCd, inventoryCheck._port);
        //                }
        //            }
        //        }
        //        else if (componentCd.Equals("CPU"))
        //        {
        //            using (DlgCpuCheck inventoryCheck = new DlgCpuCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertInventoryCheck(barcode, componentCd, dicInventory);

        //                    if (inventoryCheck._isPrint)
        //                        PrintInventoryCheck(barcode, dicInventory, componentCd, inventoryCheck._port);
        //                }
        //            }
        //        }
        //        else if (componentCd.Equals("MEM"))
        //        {
        //            using (DlgRamCheck inventoryCheck = new DlgRamCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertInventoryCheck(barcode, componentCd, dicInventory);

        //                    if (inventoryCheck._isPrint)
        //                        PrintInventoryCheck(barcode, dicInventory, componentCd, inventoryCheck._port);
        //                }
        //            }
        //        }
        //        else if (componentCd.Equals("MBD"))
        //        {
        //            using (DlgMbdCheck inventoryCheck = new DlgMbdCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertInventoryCheck(barcode, componentCd, dicInventory);

        //                    if (inventoryCheck._isPrint)
        //                        PrintInventoryCheck(barcode, dicInventory, componentCd, inventoryCheck._port);
        //                }
        //            }
        //        }
        //        else if (componentCd.Equals("VGA"))
        //        {
        //            using (DlgVgaCheck inventoryCheck = new DlgVgaCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertInventoryCheck(barcode, componentCd, dicInventory);

        //                    if (inventoryCheck._isPrint)
        //                        PrintInventoryCheck(barcode, dicInventory, componentCd, inventoryCheck._port);
        //                }
        //            }
        //        }
        //        else if (componentCd.Equals("STG"))
        //        {
        //            string type = ConvertUtil.ToString(detail_grid_.Rows[0].Cells[1].Value);

        //            if (type.Contains("SSD"))
        //            {
        //                using (DlgSsdCheck inventoryCheck = new DlgSsdCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //                {
        //                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                    {
        //                        InsertInventoryCheck(barcode, componentCd, dicInventory);

        //                        if (inventoryCheck._isPrint)
        //                            PrintInventoryCheck(barcode, dicInventory, "SSD", inventoryCheck._port);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                using (DlgHddCheck inventoryCheck = new DlgHddCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //                {
        //                    if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                    {
        //                        InsertInventoryCheck(barcode, componentCd, dicInventory);

        //                        if (inventoryCheck._isPrint)
        //                            PrintInventoryCheck(barcode, dicInventory, "HDD", inventoryCheck._port);
        //                    }
        //                }
        //            }
        //        }
        //        else if (componentCd.Equals("ODD"))
        //        {
        //            using (DlgOddCheck inventoryCheck = new DlgOddCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertInventoryCheck(barcode, componentCd, dicInventory);

        //                    if (inventoryCheck._isPrint)
        //                        PrintInventoryCheck(barcode, dicInventory, componentCd, inventoryCheck._port);
        //                }
        //            }
        //        }
        //        else if (componentCd.Equals("POW"))
        //        {
        //            using (DlgPowCheck inventoryCheck = new DlgPowCheck(barcode, dicInventory, ConvertUtil.ToInt32(combo_printer_.SelectedValue.ToString())))
        //            {
        //                if (inventoryCheck.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    InsertInventoryCheck(barcode, componentCd, dicInventory);

        //                    if (inventoryCheck._isPrint)
        //                        PrintInventoryCheck(barcode, dicInventory, componentCd, inventoryCheck._port);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("오류가 발생했습니다. 다시 시도해 주세요.");
        //            return;
        //        }
        //    }
        //}

    }
}