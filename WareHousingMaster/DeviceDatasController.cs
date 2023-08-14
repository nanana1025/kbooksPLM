using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WareHousingMaster.view.common;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Linq;
using System.Threading;
using System.Management;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Utils;

namespace WareHousingMaster
{
    enum PART_NAME
    {
        CPU, MBD, MEM, STG, VGA, MON, PSU, SET, ALL
    }

    class DeviceInfo
    {
        public string name;
        public string value;

        public DeviceInfo(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }


    /// <summary>
    /// 기기정보 가져오는 기능과 관련된 함수들을 모아놓은 클래스
    /// 
    /// </summary>
    class DeviceDatasController
    {
        public string _msg;
        public StringBuilder _partInfo = null;
        public Dictionary<string, int> _deviceCnt;



        public static CPUIDSDK sdk_instance;
        private string _DMISystemInfoProduct { get; set; }
        private string _DMIBaseboardModel { get; set; }

        /// <summary>
        /// cpuid sdk dll 을 로드하는 함수.
        /// start_path 변수에 입력된 dll 경로에서 dll을 로드.
        /// </summary>
        /// <returns></returns>
        internal bool LoadCPUZDll()
        {
            Thread UpdateThread;
            int error_code = 0, extended_error_code = 0;
            bool res;
            int dll_version = 0;
            sdk_instance = new CPUIDSDK();
            res = sdk_instance.CreateInstance();

            if (!res)
            {
                MessageBox.Show("Failed to create instance.\nExit a running program. ", "WarehousingMaster");
                return false;
            }

            string start_path = System.Windows.Forms.Application.StartupPath + @"\";

            res = sdk_instance.Init(start_path,//".\\",
                            CPUIDSDK.szDllFilename,
                            CPUIDSDK.CPUIDSDK_CONFIG_USE_EVERYTHING,
                            ref error_code,
                            ref extended_error_code);
            //if (!sdk_instance.Init(start_path,//".\\",
            //                CPUIDSDK.file_name,
            //                CPUIDSDK.CPUIDSDK_CONFIG_USE_EVERYTHING,
            //                ref error_code,
            //                ref extended_error_code))
            //{
            //    MessageBox.Show("Failed to initialize instance\nExit a running program. ", "WarehousingMaster");
            //    return false;
            //}




            if (error_code != CPUIDSDK.CPUIDSDK_ERROR_NO_ERROR)
            {
                string error_message = "";

                //	Init failed, check errorcode
                switch ((uint)error_code)
                {
                    case CPUIDSDK.CPUIDSDK_ERROR_EVALUATION:
                        {
                            switch ((uint)extended_error_code)
                            {
                                case CPUIDSDK.CPUIDSDK_EXT_ERROR_EVAL_1:
                                    error_message = "You are running a trial version of the DLL SDK. In order to make it work, please run CPU-Z at the same time.";
                                    break;

                                case CPUIDSDK.CPUIDSDK_EXT_ERROR_EVAL_2:
                                    error_message = "Evaluation version has expired.";
                                    break;
                                default:
                                    error_message = "Eval version error " + extended_error_code;
                                    break;
                            }
                        }
                        break;

                    case CPUIDSDK.CPUIDSDK_ERROR_DRIVER:
                        error_message = "Driver error " + extended_error_code;
                        break;

                    case CPUIDSDK.CPUIDSDK_ERROR_VM_RUNNING:
                        error_message = "Virtual machine detected.";
                        break;

                    case CPUIDSDK.CPUIDSDK_ERROR_LOCKED:
                        {
                            switch ((uint)extended_error_code)
                            {
                                case 6: // ERROR_INVALID_HANDLE:
                                    error_message = "SDK mutex not created.";
                                    break;

                                default:
                                    error_message = "SDK mutex locked.";
                                    break;
                            }
                        }
                        break;

                    case CPUIDSDK.CPUIDSDK_ERROR_INVALID_DLL:
                        error_message = "Invalid DLL.";
                        break;

                    default:
                        error_message = "Error code " + error_code;
                        break;
                }

                MessageBox.Show(error_message + "\nCPUID SDK Error. Exit a running program.", "Dangol365 PLM");

                //if (res)
                //{
                //    sdk_instance.GetDllVersion(ref dll_version);

                //    UpdateThread = new Thread(new ThreadStart(ThreadLoop));
                //    UpdateThread.Start();

                //    Application.Run(new Form1());

                //    _shouldStop = true;
                //    UpdateThread.Join();
                //}

                //sdk_instance.Close();
                //sdk_instance.DestroyInstance();

                return false;
            }

            //int dll_version = 0;
            //sdk_instance.GetDllVersion(ref dll_version);
            //MessageBox.Show("Initialization succeeded! dll ver = " + dll_version, "WarehousingMaster");

            return true;
        }

        //public static void ThreadLoop()
        //{
        //    //while ((Thread.CurrentThread.IsAlive) && (!_shouldStop))
        //    while (!_shouldStop)
        //    {
        //        pSDK.RefreshInformation();
        //        Thread.Sleep(1000);
        //    }
        //}

        /// <summary>
        /// CPU, MBD, MEM, STG, VGA, MON 부품의 정보를 모두 가져와서 list 변수에 저장
        /// </summary>
        /// <param name="list">ref 변수</param>
        /// <returns></returns>
        internal bool GetPCInfo()
        {
            _deviceCnt = new Dictionary<string, int>();
            foreach (string compCd in ProjectInfo._componetCd)
                _deviceCnt.Add(compCd, 0);

            try
            {
                _msg = "부품 정보 로드/";
                // get cpu data (1)
                List<DeviceInfo> cpuInfo = GetCPUData();
                ProjectInfo._listDeviceInfo.AddRange(cpuInfo);
                _msg += "CPU 로드/";

                //Dangol.Message("CPU");
                // get mem data (4)
                int memSize = 0;
                string memType = "";
                List<DeviceInfo> memInfo = GetMEMData(ref memSize, ref memType);
                ProjectInfo._listDeviceInfo.AddRange(memInfo);
                _msg += "MEM 로드/";
                //Dangol.Message("MEM");
                // get mbd data (1)
                List<DeviceInfo> mbdInfo = GetMBDData();

                mbdInfo.Add(new DeviceInfo("MBD_MEM_TYPE", memType));
                mbdInfo.Add(new DeviceInfo("MBD_MEM_SIZE", $"{memSize / 1024}GBytes"));

                ProjectInfo._listDeviceInfo.AddRange(mbdInfo);
                _msg += "MBD 로드/";
                //Dangol.Message("MBD");
                // get stg dtata (2)
                List<DeviceInfo> stgInfo = GetSTGData();
                ProjectInfo._listDeviceInfo.AddRange(stgInfo);
                _msg += "STG 로드/";
                //Dangol.Message("STG");
                // get vga (1)
                List<DeviceInfo> vgaInfo = GetVGAData();
                ProjectInfo._listDeviceInfo.AddRange(vgaInfo);
                _msg += "VGA 로드/";
                //Dangol.Message("VGA");
                // get mon (3)
                List<DeviceInfo> monInfo = GetMONData();
                ProjectInfo._listDeviceInfo.AddRange(monInfo);
                _msg += "MON 로드/";
                //Dangol.Message("MON");

                List<DeviceInfo> oddInfo = GetOddData();
                if (oddInfo.Count > 0)
                {
                    ProjectInfo._listDeviceInfo.AddRange(oddInfo);
                    _msg += "ODD 로드/";
                }

                GetCamData();

                GetBatteryInfo();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return false;
            }
            finally
            {

            }

            return true;
        }

        private string GetMemoryTypeName(int spd_number)
        {
            string mem_type = "";

            switch (spd_number)
            {
                case CPUIDSDK.MEMORY_TYPE_SPM_RAM:
                    mem_type = "SPM";
                    break;
                case CPUIDSDK.MEMORY_TYPE_RDRAM:
                    mem_type = "RDRAM";
                    break;
                case CPUIDSDK.MEMORY_TYPE_EDO_RAM:
                    mem_type = "EDO";
                    break;
                case CPUIDSDK.MEMORY_TYPE_FPM_RAM:
                    mem_type = "FPM";
                    break;
                case CPUIDSDK.MEMORY_TYPE_SDRAM:
                    mem_type = "SDRAM";
                    break;
                case CPUIDSDK.MEMORY_TYPE_DDR_SDRAM:
                    mem_type = "DDR";
                    break;
                case CPUIDSDK.MEMORY_TYPE_DDR2_SDRAM:
                    mem_type = "DDR2";
                    break;
                case CPUIDSDK.MEMORY_TYPE_DDR2_SDRAM_FB:
                    mem_type = "FB-DDR2";
                    break;
                case CPUIDSDK.MEMORY_TYPE_DDR3_SDRAM:
                    mem_type = "DDR3";
                    break;
                case CPUIDSDK.MEMORY_TYPE_DDR4_SDRAM:
                    mem_type = "DDR4";
                    break;
                default:
                    mem_type = "";
                    break;
            }

            return mem_type;
        }

        private List<DeviceInfo> GetCPUData()
        {
            List<DeviceInfo> list = new List<DeviceInfo>();

            List<string> listIntelCpu = new List<string>(new[] { "i3", "i5", "i7", "i9" });
            List<string> listAmdCpu = new List<string>(new[] { "3", "5", "7", "9" });

            try
            {
                _deviceCnt["CPU"] = 1;
                int process_count = sdk_instance.GetNumberOfProcessors();

                string codeName = sdk_instance.GetProcessorCodeName(0);
                list.Add(new DeviceInfo("CPU_CODENAME", codeName));

                string model = sdk_instance.GetProcessorName(0);
                string spec = sdk_instance.GetProcessorSpecification(0);


                //{
                //    float fValue = sdk_instance.GetProcessorManufacturingProcess(0);
                //    string s = "";
                //    if (sdk_instance.IS_F_DEFINED(fValue))
                //    {
                //        if (fValue < 0.10f)
                //        {
                //            float fVal = fValue * 1000.0f;
                //            s = Convert.ToString((int)(fVal + 0.5f)) + " nm";
                //        }
                //        else
                //        {
                //            s = Convert.ToString(fValue) + " um";
                //        }
                //    }

                //    Dangol.Message(s);
                //    s = "";

                //    fValue = sdk_instance.GetProcessorVoltageID(0);
                //    if (sdk_instance.IS_F_DEFINED(fValue))
                //    {
                //        fValue = (float)((int)(fValue * 100.0 + 0.5)) / 100.0f;
                //        s = Convert.ToString(fValue) + " V";

                //    }

                //    Dangol.Message(s);
                //    s = "";

                //    // TDP
                //    fValue = sdk_instance.GetProcessorTDP(0);
                //    if (sdk_instance.IS_F_DEFINED(fValue))
                //    {
                //        s = Convert.ToString(fValue) + " W";
                //    }
                //    Dangol.Message(s);

                //    s = sdk_instance.GetProcessorStepping(0);

                //    Dangol.Message(s);
                //    s = "";
                //}

                string[] models = model.Split(' ');

                if (listIntelCpu.Contains(models[models.Length - 1]))
                {
                    Char[] delimiter = { '(', ')', '[', ']', '/', '+', '_', ' ', '-' };
                    string[] specs = spec.Split(delimiter);

                    string cpuCd = models[models.Length - 1];
                    string cpuCdDetail = null;
                    for (int i = 0; i < specs.Length; i++)
                    {
                        if (specs[i].Equals(cpuCd))
                        {
                            cpuCdDetail = specs[i + 1];
                            break;
                        }

                    }

                    if (!string.IsNullOrEmpty(cpuCdDetail))
                        model = $"{model} {cpuCdDetail}";
                }
                else if (model.Contains("Ryzen"))
                {
                    if (listAmdCpu.Contains(models[models.Length - 1]))
                    {
                        Char[] delimiter = { '(', ')', '[', ']', '/', '+', '_', ' ', '-' };
                        string[] specs = spec.Split(delimiter);

                        string cpuCd = models[models.Length - 1];
                        string cpuCdDetail = "";
                        for (int i = 0; i < specs.Length; i++)
                        {
                            if (specs[i].Equals("Ryzen"))
                            {
                                if (specs[i + 1].Equals(cpuCd))
                                {
                                    cpuCdDetail = specs[i + 2];
                                    break;
                                }
                            }

                        }

                        if (!string.IsNullOrEmpty(cpuCdDetail))
                        {
                            if (spec.Contains("Mobile"))
                                model = $"{model} Mobile {cpuCdDetail}";
                            else
                                model = $"{model} {cpuCdDetail}";
                        }

                    }
                }


                list.Add(new DeviceInfo("CPU_MODEL", model));
                list.Add(new DeviceInfo("CPU_SPEC", spec));

                string coreCnt = sdk_instance.GetProcessorCoreCount(0).ToString();
                list.Add(new DeviceInfo("CPU_CORE", coreCnt));

                string socket = sdk_instance.GetProcessorPackage(0);
                list.Add(new DeviceInfo("CPU_SOCKET", socket));

                string threadCnt = sdk_instance.GetProcessorThreadCount(0).ToString();
                list.Add(new DeviceInfo("CPU_THREAD", threadCnt));

                string mode = sdk_instance.GetSystemManufacturer().ToString();

                byte[] _id = new byte[10];
                sdk_instance.GetSPDModuleManufacturerID(0, _id);

                string hex = BitConverter.ToString(_id);

                //value = sdk_instance.GetSPDModuleDRAMManufacturer(0).ToString();

                uint id;
                id = sdk_instance.GetProcessorID(0);
                string manufactuere;


                if ((id & CPUIDSDK.CPU_INTEL) == CPUIDSDK.CPU_INTEL)
                {
                    manufactuere = "INTEL";
                }
                else if ((id & CPUIDSDK.CPU_AMD) == CPUIDSDK.CPU_AMD)
                {
                    manufactuere = "AMD";
                }
                else if ((id & CPUIDSDK.CPU_CYRIX) == CPUIDSDK.CPU_CYRIX)
                {
                    manufactuere = "CYRIX";
                }
                else if ((id & CPUIDSDK.CPU_VIA) == CPUIDSDK.CPU_VIA)
                {
                    manufactuere = "VIA";
                }
                else if ((id & CPUIDSDK.CPU_TRANSMETA) == CPUIDSDK.CPU_TRANSMETA)
                {
                    manufactuere = "TRANSMETA";
                }
                else if ((id & CPUIDSDK.CPU_DMP) == CPUIDSDK.CPU_DMP)
                {
                    manufactuere = "DMP";
                }
                else if ((id & CPUIDSDK.CPU_UMC) == CPUIDSDK.CPU_UMC)
                {
                    manufactuere = "UMC";
                }
                else
                {
                    manufactuere = "";
                }

                list.Add(new DeviceInfo("CPU_MANUFACTURE", manufactuere));
            }
            catch (Exception ex)
            {
                _msg += "CPU 로드 실패/";
                return list;
            }

            _msg += "CPU 로드 성공/";
            return list;
        }

        private List<DeviceInfo> GetMBDData()
        {
            List<DeviceInfo> list = new List<DeviceInfo>();
            string error = "";
            ProjectInfo._wifiYn = 0;
            try
            {
                _deviceCnt["MBD"] = 1;

                // Northbirdge MBD_NB
                string northBridge = $"{sdk_instance.GetNorthBridgeVendor()} {sdk_instance.GetNorthBridgeModel()} rev. {sdk_instance.GetNorthBridgeRevision()}";
                list.Add(new DeviceInfo("MBD_NB", northBridge));

                string southModel = sdk_instance.GetSouthBridgeModel();
                list.Add(new DeviceInfo("MBD_CODE", southModel));

                // Southbridge MBD_SB        
                string southBridge = $"{sdk_instance.GetSouthBridgeVendor()} {southModel} rev.  {sdk_instance.GetSouthBridgeRevision()}";
                list.Add(new DeviceInfo("MBD_SB", southBridge));

                string systemManufacturer = sdk_instance.GetSystemManufacturer();
                string manufacturer = "";

                _DMIBaseboardModel = sdk_instance.GetMainboardModel();
                list.Add(new DeviceInfo("MBD_MODEL_NAME", _DMIBaseboardModel));

                _DMISystemInfoProduct = sdk_instance.GetSystemProductName();
                list.Add(new DeviceInfo("MBD_SYSTEM_PRODUCT_NAME", _DMISystemInfoProduct));

                if (systemManufacturer.Equals("To Be Filled By O.E.M."))
                {
                    // MBD_MODEL        
                    list.Add(new DeviceInfo("MBD_MODEL", _DMIBaseboardModel));

                    // MBD_MANUFACTURE
                    manufacturer = sdk_instance.GetMainboardVendor();
                    list.Add(new DeviceInfo("MBD_MANUFACTURE", manufacturer));
                }
                else
                {
                    // MBD_MODEL            
                    list.Add(new DeviceInfo("MBD_MODEL", _DMISystemInfoProduct));

                    // MBD_MANUFACTURE
                    manufacturer = sdk_instance.GetSystemManufacturer();
                    list.Add(new DeviceInfo("MBD_MANUFACTURE", manufacturer));
                }


                string dimm = (sdk_instance.GetMemoryMaxCapacity() / 1024) + "GB"; //GB
                list.Add(new DeviceInfo("MBD_DIMM", dimm));

                string maxMemCnt = (sdk_instance.GetMemoryMaxNumberOfDevices()).ToString();
                list.Add(new DeviceInfo("MBD_MAX_MEM_CNT", maxMemCnt));

                string mbdRevision = sdk_instance.GetMainboardRevision();
                list.Add(new DeviceInfo("MBD_REVISION", mbdRevision));

                string uUid = sdk_instance.GetSystemUUID();
                list.Add(new DeviceInfo("MBD_UUID", uUid));


                string biosVentor = sdk_instance.GetBIOSVendor();
                list.Add(new DeviceInfo("MBD_BIOS_VENDOR", biosVentor));

                string biosVersion = sdk_instance.GetBIOSVersion();
                list.Add(new DeviceInfo("MBD_BIOS_VERSION", biosVersion));

                string biosDate = sdk_instance.GetBIOSDate();
                list.Add(new DeviceInfo("MBD_BIOS_DATE", biosDate));

                string sysVersion = sdk_instance.GetSystemVersion();
                list.Add(new DeviceInfo("MBD_SYSTEM_VERSION", sysVersion));

                string chassisManufacturer = sdk_instance.GetChassisManufacturer();
                list.Add(new DeviceInfo("MBD_CHASSIS_MANUFACTURER", chassisManufacturer));

                string chassisSN = sdk_instance.GetChassisSerialNumber();
                list.Add(new DeviceInfo("MBD_CHASSIS_SN", chassisSN));

                int noOfMemDevices = sdk_instance.GetNumberOfMemoryDevices();
                list.Add(new DeviceInfo("MBD_NO_OF_MEM_DEVICE", noOfMemDevices.ToString()));

                int processorSocket = sdk_instance.GetProcessorSockets();
                list.Add(new DeviceInfo("MBD_PROCESSOR_SOCKET", processorSocket.ToString()));


                //string memType = "";
                //string memSize = "";
                //MBD_MEM_TYPE
                //int spd_number = sdk_instance.GetNumberOfSPDModules();
                //int all_size = 0;
                //int i = 0;
                //{
                //memType = GetMemoryTypeName(sdk_instance.GetSPDModuleType(0));
                //list.Add(new DeviceInfo("MBD_MEM_TYPE", memType));

                //    //MBD_MEM_SIZE GetSPDModuleSize
                //    all_size += sdk_instance.GetSPDModuleSize(i);
                //    //if (i == spd_number - 1)
                //    {
                //        memSize = all_size / 1024 + "GBytes";
                //        list.Add(new DeviceInfo("MBD_MEM_SIZE", memSize));
                //    }
                //}


                // MBD_SKU

                //error += "MBD_SKU / ";
                string sku = sdk_instance.GetSystemSKU();
                list.Add(new DeviceInfo("MBD_SKU", sku));

                //error += "MBD_SYSTEM_SN / ";
                string systemSN = sdk_instance.GetSystemSerialNumber();
                list.Add(new DeviceInfo("MBD_SYSTEM_SN", systemSN));

                //error += "MBD_SN / ";
                string mbdSN = sdk_instance.GetMainboardSerialNumber();
                list.Add(new DeviceInfo("MBD_SN", mbdSN));

                string SN = "";

                string type = sdk_instance.GetChassisType();
                if (!string.IsNullOrEmpty(type))
                    type = type.ToUpper();
                list.Add(new DeviceInfo("TYPE", type));

                if (!string.IsNullOrEmpty(sku) && (manufacturer.Contains("SAMSUNG") && (type.Equals("LAPTOP") || type.Equals("NOTEBOOK"))))
                    SN = systemSN;
                else if (!string.IsNullOrEmpty(sku) && (sku.Contains("LENOVO") && (type.Equals("LAPTOP") || type.Equals("NOTEBOOK"))))
                    SN = _DMISystemInfoProduct + systemSN;
                else
                    SN = mbdSN;


                //error += "MBD_SERIAL / ";

                list.Add(new DeviceInfo("MBD_SERIAL", SN));

                //error += "MBD_FAMILY / ";
                string family = sdk_instance.GetSystemFamily();
                list.Add(new DeviceInfo("MBD_FAMILY", family));
                //error += "MBD_MAC_ADDRESS / ";

                bool isFind = false;
                string wifi = "";
                for (int k = 0; k < NetworkInterface.GetAllNetworkInterfaces().Length; k++)
                {
                    string name = NetworkInterface.GetAllNetworkInterfaces()[k].Name.ToString();
                    string networkInterfaceType = NetworkInterface.GetAllNetworkInterfaces()[k].NetworkInterfaceType.ToString();

                    if (!name.Contains("Bluetooth") && (name.Contains("Ethernet") || name.Contains("이더넷") || networkInterfaceType.Contains("이더넷") || networkInterfaceType.Contains("Ethernet")))
                    {
                        list.Add(new DeviceInfo("MBD_MAC_ADDRESS", NetworkInterface.GetAllNetworkInterfaces()[k].GetPhysicalAddress().ToString()));
                        isFind = true;
                    }

                    if (name.ToUpper().Contains("WI-FI"))
                    {
                        wifi = NetworkInterface.GetAllNetworkInterfaces()[k].GetPhysicalAddress().ToString();
                        ProjectInfo._wifiYn = 1;
                    }

                }

                if (!isFind)
                    list.Add(new DeviceInfo("MBD_MAC_ADDRESS", wifi));



                /*
                                list.Add(new DeviceInfo("MBD_MAC_ADDRESS", NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString()));

                                Dangol.Message($"개수 = {NetworkInterface.GetAllNetworkInterfaces().Length}");

                                for (int kk = 0; kk < NetworkInterface.GetAllNetworkInterfaces().Length; kk++)
                                {
                                    string str = $@"
                                    Description = {NetworkInterface.GetAllNetworkInterfaces()[kk].Description.ToString()},
                                    Id = {NetworkInterface.GetAllNetworkInterfaces()[kk].Id.ToString()},
                                    IsReceiveOnly = {NetworkInterface.GetAllNetworkInterfaces()[kk].IsReceiveOnly.ToString()},

                                    Name = {NetworkInterface.GetAllNetworkInterfaces()[kk].Name.ToString()},

                                    NetworkInterfaceType = {NetworkInterface.GetAllNetworkInterfaces()[kk].NetworkInterfaceType.ToString()},
                                    OperationalStatus = {NetworkInterface.GetAllNetworkInterfaces()[0].OperationalStatus.ToString()},
                                    Speed = {NetworkInterface.GetAllNetworkInterfaces()[kk].Speed.ToString()},
                                    SupportsMulticast = {NetworkInterface.GetAllNetworkInterfaces()[kk].SupportsMulticast.ToString()},

                                    macc_address = {NetworkInterface.GetAllNetworkInterfaces()[kk].GetPhysicalAddress().ToString()},
                                    ";

                                    Dangol.Message(str);

                                }
               
                Dangol.Message("wifi = " + wifi);
 

                */


                //error += "OS_SERIAL_NO / ";
                var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                using (RegistryKey keys = baseKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SoftwareProtectionPlatform", false))
                {
                    if (keys != null)
                    {
                        Object o = keys.GetValue("BackupProductKeyDefault");
                        if (o != null)
                        {
                            list.Add(new DeviceInfo("OS_SERIAL_NO", ConvertUtil.ToString(o)));
                            ProjectInfo.osSerialNo = ConvertUtil.ToString(o);
                        }
                        else
                        {
                            list.Add(new DeviceInfo("OS_SERIAL_NO", ""));
                        }
                    }
                    else
                    {
                        list.Add(new DeviceInfo("OS_SERIAL_NO", ""));
                    }
                }
                //error += "OS_PRODUCT_KEY / ";
                using (RegistryKey keys = baseKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", false))
                {
                    if (keys != null)
                    {
                        Object o = keys.GetValue("ProductId");
                        if (o != null)
                        {
                            list.Add(new DeviceInfo("OS_PRODUCT_KEY", ConvertUtil.ToString(o)));
                            ProjectInfo.osProductNo = ConvertUtil.ToString(o);
                        }
                        else
                        {
                            list.Add(new DeviceInfo("OS_PRODUCT_KEY", ""));
                        }

                        Object o1 = keys.GetValue("ProductName");
                        if (o1 != null)
                        {
                            list.Add(new DeviceInfo("OS_NAME", ConvertUtil.ToString(o1)));
                            ProjectInfo._osName = ConvertUtil.ToString(o1);
                        }
                        else
                        {
                            list.Add(new DeviceInfo("OS_NAME", ""));
                        }
                    }
                    else
                    {
                        list.Add(new DeviceInfo("OS_PRODUCT_KEY", ""));
                        list.Add(new DeviceInfo("OS_NAME", ""));

                    }
                }

            }
            catch (Exception ex)
            {
                //Dangol.Message($"ERROR = "+ex.Message);
                //Dangol.Message($"ERROR = " + error);
                Dangol.Message($"ERROR = " + ex);

                _msg += "MBD 로드 실패/";
                return list;
            }

            _msg += "MBD 로드 성공/";
            return list;
        }

        private List<DeviceInfo> GetMEMData(ref int memSize, ref string memType)
        {
            List<DeviceInfo> list = new List<DeviceInfo>();

            try
            {
                int memory_cnt = sdk_instance.GetNumberOfSPDModules();

                int[] arrayMemInfo = new int[memory_cnt];

                int index = memory_cnt;

                //Dangol.Message("memory_cnt = " + memory_cnt);

                for (int i = 0; i < memory_cnt; i++)
                {
                    // MEM_1_TYPE
                    string type = GetMemoryTypeName(sdk_instance.GetSPDModuleType(i));
                    list.Add(new DeviceInfo("MEM_" + i + "_TYPE", type));

                    memType = type;
                    // MEM_1_MODULE
                    string module = sdk_instance.GetSPDModuleFormat(i);
                    list.Add(new DeviceInfo("MEM_" + i + "_MODULE", module));

                    // MEM_1_MANUFACTURE
                    string manufacturer = sdk_instance.GetSPDModuleManufacturer(i);
                    list.Add(new DeviceInfo("MEM_" + i + "_MANUFACTURE", manufacturer));

                    // MEM_1_SIZE
                    int memesize;
                    memesize = sdk_instance.GetSPDModuleSize(i);
                    string size = $"{memesize}MBytes";
                    list.Add(new DeviceInfo("MEM_" + i + "_SIZE", size));
                    memSize += memesize;

                    arrayMemInfo[i] = memesize;

                    //Dangol.Message("size = " + size);

                    // MEM_1_BANDWIDTH
                    string bandwidth = sdk_instance.GetSPDModuleSpecification(i);
                    list.Add(new DeviceInfo("MEM_" + i + "_BANDWIDTH", bandwidth));

                    // MEM_1_MODEL
                    string model = sdk_instance.GetSPDModulePartNumber(i);
                    list.Add(new DeviceInfo("MEM_" + i + "_MODEL", model));

                    // MEM_1_SERIAL
                    string serial = sdk_instance.GetSPDModuleSerialNumber(i);
                    list.Add(new DeviceInfo("MEM_" + i + "_SERIAL", serial));

                    //Dangol.Message("serial = " + serial);

                    // MEM_1_MANUFACTURE_DT
                    string manufacturerDT = "";
                    int year = 0;
                    int week = 0;
                    if (sdk_instance.GetSPDModuleManufacturingDate(i, ref year, ref week) != -1)
                    {
                        manufacturerDT = "Week " + week + "/" + "Year " + year;
                    }
                    else
                    {
                        manufacturerDT = "";
                    }
                    list.Add(new DeviceInfo("MEM_" + i + "_MANUFACTURE_DT", manufacturerDT));

                    // MEM_1_VOLT
                    int k = 0;
                    int NbProfiles = sdk_instance.GetSPDModuleNumberOfProfiles(i);
                    string volt = "";
                    if (NbProfiles > 0)
                    {
                        //for (int k =0; i< NbProfiles; k++)
                        {
                            float frequency = (float)CPUIDSDK.F_UNDEFINED_VALUE;
                            float CL = (float)CPUIDSDK.F_UNDEFINED_VALUE;
                            float nominal_voltage = (float)CPUIDSDK.F_UNDEFINED_VALUE;

                            sdk_instance.GetSPDModuleProfileInfos(i, k, ref frequency, ref CL, ref nominal_voltage);
                            volt = string.Format("{0:f2} Volts", nominal_voltage);


                            list.Add(new DeviceInfo("MEM_" + i + "_VOLT", volt));
                        }
                    }
                }

                int memory_slotCnt = sdk_instance.GetMemoryMaxNumberOfDevices();

                //Dangol.Message("memory_slotCnt = " + memory_slotCnt);

                if (!string.IsNullOrEmpty(_DMISystemInfoProduct) && _DMISystemInfoProduct.Equals("905S3G/906S3G/915S3G"))
                    memory_slotCnt = 2;

                if (memory_cnt != memory_slotCnt)
                {
                    List<DeviceInfo> listDmi = new List<DeviceInfo>();
                    int[] arrayMemSize = new int[memory_slotCnt];
                    string[] arrayDMIType = new string[memory_slotCnt];
                    int DmiCnt = 0;

                    for (int i = 0; i < memory_slotCnt; i++)
                    {
                        int size = 0;
                        string format = "";
                        bool result;
                        string szDesignation = "";
                        string szType = "";
                        int total_width = 0;
                        int data_width = 0;
                        int speed = 0;

                        result = sdk_instance.GetMemoryDeviceInfos(i, ref size, ref format);


                        //Dangol.Message("size = " + size);
                        if (size > 0)
                        {
                            DmiCnt++;

                            result = sdk_instance.GetMemoryDeviceInfosExt(i, ref szDesignation, ref szType, ref total_width, ref data_width, ref speed);

                            listDmi.Add(new DeviceInfo("MEM_" + i + "_SHLEE_SIZE", size.ToString()));
                            listDmi.Add(new DeviceInfo("MEM_" + i + "_SHLEE_SZTYPE", szType));
                            arrayMemSize[i] = size;
                            arrayDMIType[i] = szType;
                        }
                        else
                        {
                            arrayMemSize[i] = -1;
                        }
                    }


                    bool isFind = false;

                    //Dangol.Message("memory_cnt = "+ memory_cnt+", DmiCnt = " + DmiCnt);
                    if (memory_cnt != DmiCnt)
                    {
                        for (int i = 0; i < memory_slotCnt; i++)
                        {
                            isFind = false;
                            if (arrayMemSize[i] > 0)
                            {
                                for (int j = 0; j < memory_cnt; j++)
                                {
                                    if (arrayMemSize[i] == arrayMemInfo[j])
                                    {
                                        arrayMemInfo[j] = -1;
                                        isFind = true;
                                        break;
                                    }
                                }

                                if (!isFind)
                                {
                                    string gen = arrayDMIType[i].Replace("DDR", "");
                                    list.Add(new DeviceInfo("MEM_" + index + "_MANUFACTURE", "Built-in"));
                                    list.Add(new DeviceInfo("MEM_" + index + "_TYPE", arrayDMIType[i]));
                                    list.Add(new DeviceInfo("MEM_" + index + "_SIZE", $"{arrayMemSize[i]}MBytes"));
                                    list.Add(new DeviceInfo("MEM_" + index + "_BANDWIDTH", $"{arrayDMIType[i]}-0000"));
                                    list.Add(new DeviceInfo("MEM_" + index + "_MODEL", $"BI{gen}-{arrayMemSize[i]}"));

                                    index++;
                                    memSize += arrayMemSize[i];

                                    if (string.IsNullOrWhiteSpace(memType))
                                        memType = $"{arrayDMIType[i]}";
                                    else if (!memType.Equals(arrayDMIType[i]))
                                        memType = $"{memType}/{arrayDMIType[i]}";
                                }
                            }
                        }
                    }
                }
                _deviceCnt["MEM"] = index;
            }
            catch (Exception ex)
            {
                _msg += "MEM 로드 실패/";
                return list;
            }

            _msg += "MEM 로드 성공/";
            return list;
        }

        private List<DeviceInfo> GetSTGData()
        {
            List<DeviceInfo> list = new List<DeviceInfo>();

            try
            {
                int stgCnt = sdk_instance.GetNumberOfStorageDevice();
                int externalStgCnt = 0;
                _deviceCnt["STG"] = stgCnt;
                Dictionary<int, int> dicStgTypeOrder = new Dictionary<int, int>();

                for (int i = 0; i < stgCnt; i++)
                {
                    string model = sdk_instance.GetStorageDeviceName(i);
                    if (string.IsNullOrEmpty(model))
                    {
                        externalStgCnt++;
                        continue;
                    }

                    float fCapacity = sdk_instance.GetStorageDeviceTotalCapacity(i);
                    if (fCapacity < 1.0)
                    {
                        externalStgCnt++;
                        continue;
                    }

                    string busType = GetDeviceBusType(sdk_instance.GetStorageDeviceBusType(i));

                    if (busType.Equals("USB"))
                    {
                        externalStgCnt++;
                        continue;
                    }


                    uint flag = sdk_instance.GetStorageDeviceFeatureFlag(i);

                    if ((flag & (uint)CPUIDSDK.DRIVE_FEATURE_IS_REMOVABLE) == 0) //fixed
                    {
                        dicStgTypeOrder.Add(i, 1);
                        continue;
                    }

                    if ((flag & CPUIDSDK.DRIVE_FEATURE_IS_SSD) == 0)
                    {
                        dicStgTypeOrder.Add(i, 1);
                        continue;

                    }
                    else
                    {
                        dicStgTypeOrder.Add(i, 2);
                        continue;
                    }
                }

                var orderIndex = dicStgTypeOrder.OrderBy(x => x.Value);

                int index = 0;
                foreach (KeyValuePair<int, int> item in orderIndex)
                {
                    int i = item.Key;
                    //// STG_1_TYPE
                    string type = "";
                    string is_fixed = "";
                    string storage_features = "";
                    uint flag = sdk_instance.GetStorageDeviceFeatureFlag(i);

                    if ((flag & (uint)CPUIDSDK.DRIVE_FEATURE_IS_REMOVABLE) == 0) //fixed
                    {
                        is_fixed = "Fixed";
                    }

                    string speed = "";
                    if ((flag & CPUIDSDK.DRIVE_FEATURE_IS_SSD) == 0)
                    {
                        // STG_1_SPEED
                        int i_value = sdk_instance.GetStorageDeviceRotationSpeed(i);
                        if (i_value == -1)
                        {
                            speed = "";
                        }
                        else
                        {
                            speed = i_value.ToString() + " RPM";
                        }

                        list.Add(new DeviceInfo("STG_" + index + "_SPEED", speed));

                        // SMART  0R TRIM
                    }
                    else
                    {
                        type = "SSD";
                    }

                    if ((flag & (uint)CPUIDSDK.DRIVE_FEATURE_SMART) != 0)
                    {
                        storage_features = "SMART";
                    }

                    if ((flag & (uint)CPUIDSDK.DRIVE_FEATURE_TRIM) != 0)
                    {
                        storage_features += ", TRIM";
                    }

                    string stgType = "";
                    // STG_1_TYPE
                    if (is_fixed.Equals(""))
                    {
                        // "SSD"
                        stgType = type;
                    }
                    else
                    {
                        if (type.Equals(""))
                        {
                            // "Fixed"
                            stgType = is_fixed;
                        }
                        else
                        {
                            // "Fixed, SSD"
                            stgType = is_fixed + ", " + type;
                        }
                    }
                    //stgType = "";
                    //if (string.IsNullOrEmpty(stgType))
                    //{
                    //    externalStgCnt++;
                    //    continue;
                    //}

                    list.Add(new DeviceInfo("STG_" + index + "_TYPE", stgType));


                    // STG_1_MODEL   
                    string model = sdk_instance.GetStorageDeviceName(i);
                    list.Add(new DeviceInfo("STG_" + index + "_MODEL", model));

                    // STG_1_REVISION
                    string revision = sdk_instance.GetStorageDeviceRevision(i);
                    list.Add(new DeviceInfo("STG_" + index + "_REVISION", revision));

                    // STG_1_SERIAL
                    string serial = sdk_instance.GetStorageDeviceSerialNumber(i);
                    list.Add(new DeviceInfo("STG_" + index + "_SERIAL", serial));

                    //// STG_1_CAPACITY
                    float fCapacity = sdk_instance.GetStorageDeviceTotalCapacity(i);
                    //MessageBox.Show(fCapacity.ToString());
                    //double baseValue = 1073741824.0;
                    //double portionValue = 1000000000000.0;
                    double baseValue = 0.001048576;
                    double modifiedCapacity = fCapacity * baseValue;
                    double dsfdsf = Math.Round(modifiedCapacity, 1);
                    int iCapacity = ConvertUtil.ToInt32(Math.Round(modifiedCapacity, 1));
                    string capacity = string.Format("{0:f1} GB", fCapacity / 1024);

                    string capacityM = "";
                    if (iCapacity > 999)
                    {
                        double dCapa = iCapacity / 1000.0;
                        if (iCapacity % 1000 == 0)
                            capacityM = string.Format("{0:f0} TB", dCapa);
                        else
                            capacityM = string.Format("{0:f1} TB", dCapa);
                    }
                    else
                        capacityM = string.Format("{0:d0} GB", iCapacity);

                    list.Add(new DeviceInfo("STG_" + index + "_CAPACITY", capacity));
                    list.Add(new DeviceInfo("STG_" + index + "_CAPACITY_M", capacityM));

                    // STG_1_BUS_TYPE
                    string busType = GetDeviceBusType(sdk_instance.GetStorageDeviceBusType(i));
                    list.Add(new DeviceInfo("STG_" + index + "_BUS_TYPE", busType));

                    // STG_1_SPEED
                    //string speed = "";
                    //flag = sdk_instance.GetStorageDeviceFeatureFlag(i);

                    //if ((flag & CPUIDSDK.DRIVE_FEATURE_IS_SSD) == 0)
                    //{
                    //    // STG_1_SPEED
                    //    int i_value = sdk_instance.GetStorageDeviceRotationSpeed(i);
                    //    if (i_value == -1)
                    //    {
                    //        speed = "";
                    //    }
                    //    else
                    //    {
                    //        speed = i_value.ToString() + " RPM";
                    //    }
                    //}
                    //else
                    //{
                    //    speed = "";
                    //}

                    //list.Add(new DeviceInfo("STG_" + i + "_SPEED", speed));

                    // STG_1_FEATURE
                    string feature = storage_features;
                    list.Add(new DeviceInfo("STG_" + index + "_FEATURE", feature));

                    // STG_1_VOLUME
                    for (int volume = 0; volume < sdk_instance.GetStorageDeviceNumberOfVolumes(i); volume++)
                    {
                        string letter = sdk_instance.GetStorageDeviceVolumeLetter(i, volume);
                        float total_capacity = (float)Math.Round(sdk_instance.GetStorageDeviceVolumeTotalCapacity(i, volume), 1);
                        float free_capacity = (float)Math.Round(sdk_instance.GetStorageDeviceVolumeAvailableCapacity(i, volume), 1);
                        float percent = (float)Math.Round((free_capacity / total_capacity) * 100, 1);

                        string svolume = letter + ":\\, " + total_capacity + " (" + percent + " percent avaliable)";
                        list.Add(new DeviceInfo("STG_" + index + "_VOLUME", svolume));
                    }
                    index++;
                }

                _deviceCnt["STG"] = stgCnt - externalStgCnt;
            }
            catch (Exception ex)
            {
                _msg += "STG 로드 실패/";
                return list;
            }
            _msg += "STG 로드 성공/";
            return list;
        }

        private string GetDeviceBusType(int bus_number)
        {
            string bus = "";
            switch (bus_number)
            {
                case CPUIDSDK.BUS_TYPE_SCSI:
                    bus = "SCSI";
                    break;
                case CPUIDSDK.BUS_TYPE_ATAPI:
                    bus = "ATAPI";
                    break;
                case CPUIDSDK.BUS_TYPE_ATA:
                    bus = "ATA";
                    break;
                case CPUIDSDK.BUS_TYPE_IEEE1394:
                    bus = "IEEE1394";
                    break;
                case CPUIDSDK.BUS_TYPE_SSA:
                    bus = "SSA";
                    break;
                case CPUIDSDK.BUS_TYPE_FIBRE:
                    bus = "Fiber channel";
                    break;
                case CPUIDSDK.BUS_TYPE_USB:
                    bus = "USB";
                    break;
                case CPUIDSDK.BUS_TYPE_RAID:
                    bus = "RAID";
                    break;
                case CPUIDSDK.BUS_TYPE_ISCSI:
                    bus = "iSCSI";
                    break;
                case CPUIDSDK.BUS_TYPE_SAS:
                    bus = "SAS";
                    break;
                case CPUIDSDK.BUS_TYPE_SATA:
                    bus = "SATA";
                    break;
                case CPUIDSDK.BUS_TYPE_SD:
                    bus = "SD";
                    break;
                case CPUIDSDK.BUS_TYPE_MMC:
                    bus = "MMC";
                    break;
                case CPUIDSDK.BUS_TYPE_VIRTUAL:
                    bus = "Virtual";
                    break;
                case CPUIDSDK.BUS_TYPE_FILEBACKEDVIRTUAL:
                    bus = "Virtual";
                    break;
                case CPUIDSDK.BUS_TYPE_SPACES:
                    bus = "Spaces";
                    break;
                case CPUIDSDK.BUS_TYPE_NVME:
                    bus = "NVMe";
                    break;
                default: break;
            }

            return bus;
        }

        private List<DeviceInfo> GetMONData()
        {
            List<DeviceInfo> list = new List<DeviceInfo>();

            try
            {

                int cnt = sdk_instance.GetNumberOfMonitors();

                _deviceCnt["MON"] = cnt;

                for (int i = 0; i < cnt; i++)
                {
                    // MON_1_MODEL
                    string model = sdk_instance.GetMonitorName(i);
                    list.Add(new DeviceInfo("MON_" + i + "_MODEL", model));

                    // MON_1_ID
                    string id = sdk_instance.GetMonitorID(i);
                    list.Add(new DeviceInfo("MON_" + i + "_ID", id));


                    string deviceName = sdk_instance.GetDeviceName(i);
                    list.Add(new DeviceInfo("MON_" + i + "_DEVICE_NAME", deviceName));

                    // MON_1_SERIAL
                    string serial = sdk_instance.GetMonitorSerial(i);
                    list.Add(new DeviceInfo("MON_" + i + "_SERIAL", serial));


                    string vendor = sdk_instance.GetMonitorVendor(i);
                    list.Add(new DeviceInfo("MON_" + i + "_VENDOR", vendor));

                    int maxPixel = sdk_instance.GetMonitorMaxPixelClock(i);
                    list.Add(new DeviceInfo("MON_" + i + "_MAX_PIXEL", maxPixel.ToString()));

                    float gamma = sdk_instance.GetMonitorGamma(i);
                    list.Add(new DeviceInfo("MON_" + i + "_GAMMA", gamma.ToString()));

                    // MON_1_MANUFACTURE_DT
                    int week = 0;
                    int year = 0;
                    string manufactureDT = "";
                    if (sdk_instance.GetMonitorManufacturingDate(i, ref week, ref year))
                    {
                        manufactureDT = "Week " + week + ", Year " + year;
                    }
                    else
                    {
                        manufactureDT = "";
                    }

                    list.Add(new DeviceInfo("MON_" + i + "_MANUFACTURE_DT", manufactureDT));

                    // MON_1_SIZE
                    string size = "";
                    double f_value = Math.Round(sdk_instance.GetMonitorSize(i), 1);
                    if (f_value == -1)
                    {
                        size = "";
                    }
                    else
                    {
                        size = f_value.ToString() + " inches";
                    }
                    list.Add(new DeviceInfo("MON_" + i + "_SIZE", size));

                    // MON_1_RESOLUTION
                    string resolution = "";
                    int width = 0;
                    int height = 0;
                    int freq = 0;
                    if (sdk_instance.GetMonitorResolution(i, ref width, ref height, ref freq))
                    {
                        resolution = width + " x " + height + " @ " + freq + " Hz";
                        list.Add(new DeviceInfo("MON_" + i + "_RESOLUTION", resolution));
                    }

                }
            }
            catch (Exception ex)
            {
                _msg += "MON 로드 실패/";
                return list;
            }
            _msg += "MON 로드 성공/";
            return list;
        }

        private List<DeviceInfo> GetOddData()
        {
            List<DeviceInfo> list = new List<DeviceInfo>();

            try
            {
                ProjectInfo._oddType = "";
                int oddCnt = 0;
                SelectQuery selectQuery = new SelectQuery("Win32_CDROMDrive");

                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(selectQuery);

                foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                {
                    foreach (PropertyData propertyData in managementObject.Properties)
                    {
                        string valueString = null;

                        if (propertyData.Value != null)
                        {
                            valueString = propertyData.Value.ToString();
                        }

                        string[] itemArray = { propertyData.Name, valueString };

                        if (itemArray[0].Equals("Manufacturer"))
                            list.Add(new DeviceInfo($"ODD_{oddCnt}MANUFACTURE_NM", itemArray[1]));
                        else if (itemArray[0].Equals("MediaType"))
                        {
                            list.Add(new DeviceInfo($"ODD_{oddCnt}TYPE", itemArray[1]));
                            ProjectInfo._oddType = itemArray[1];
                        }
                        else if (itemArray[0].Equals("Name"))
                            list.Add(new DeviceInfo($"ODD_{oddCnt}MODEL_NM", itemArray[1]));
                    }
                    oddCnt++;
                }

                //list.Add(new DeviceInfo($"ODD_{oddCnt}MANUFACTURE_NM", "LG"));
                //list.Add(new DeviceInfo($"ODD_{oddCnt}TYPE", "DVD RW"));
                //list.Add(new DeviceInfo($"ODD_{oddCnt}MODEL_NM", "ODD123"));
                //ProjectInfo._oddType = "DVD RW";


                
                _deviceCnt["ODD"] = oddCnt;

            }
            catch (Exception ex)
            {
                _msg += "ODD 로드 실패/";
                return list;
            }


            if (list.Count > 0)
                _msg += "ODD 로드 성공/";

            return list;
        }

        private void GetCamData()
        {
            try
            {
                ProjectInfo._camModelNm = "";
                var cameraNames = new List<string>();
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (PNPClass = 'Image' OR PNPClass = 'Camera')"))
                {
                    foreach (var device in searcher.Get())
                    {
                        ProjectInfo._camModelNm = device["Caption"].ToString();
                        _msg += $"CAM({device["Caption"]})/";
                    }
                }
            }
            catch (Exception ex)
            {
                _msg += "CAM로드 실패/";
            }
        }

        private void GetBatteryInfo()
        {
            try
            {
                ProjectInfo._batteryRemain = 0;
                bool isBatteryExist = false;
                SelectQuery selectQuery = new SelectQuery("Win32_Battery");

                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(selectQuery);

                foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                {
                    isBatteryExist = true;
                    break;
                }

                if (isBatteryExist)
                {

                    BatteryInformation cap = BatteryInfo.GetBatteryInformation();

                    double designCapacity = ConvertUtil.ToDouble(cap.DesignedCapacity);
                    double fullCapacity = ConvertUtil.ToDouble(cap.FullChargeCapacity);

                    ProjectInfo._batteryRemain = (fullCapacity / designCapacity) * 100;

                    _msg += $"BATTERY REMAIN[{ ProjectInfo._batteryRemain.ToString("F2")}]/";

                    //string filePath = $"{System.Windows.Forms.Application.StartupPath}\\batteryReport.html";
                    //ProcessStartInfo cmd = new ProcessStartInfo();
                    //Process process = new Process();
                    //cmd.FileName = @"cmd";
                    //cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
                    //cmd.CreateNoWindow = true;                               // cmd창을 띄우지 안도록 하기

                    //cmd.UseShellExecute = false;
                    //cmd.RedirectStandardOutput = false;        // cmd창에서 데이터를 가져오기
                    //cmd.RedirectStandardInput = true;          // cmd창으로 데이터 보내기
                    //cmd.RedirectStandardError = false;          // cmd창에서 오류 내용 가져오기

                    //process.EnableRaisingEvents = false;
                    //process.StartInfo = cmd;
                    //process.Start();
                    //process.StandardInput.Write($"powercfg /batteryreport /output \"{filePath}\"{Environment.NewLine}");
                    //// 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine이 필요하다
                    //process.StandardInput.Close();

                    //string result = process.StandardOutput.ReadToEnd();
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("[Result Info]" + DateTime.Now + "\r\n");
                    //sb.Append(result);
                    //sb.Append("\r\n");

                    //string resultCmd = sb.ToString();
                    //process.WaitForExit();
                    //process.Close();

                    //FileInfo fileInfo = new FileInfo(filePath);
                    ////파일 있는지 확인 있을때(true), 없으면(false)

                    //Dangol.Message($"fileInfo.Exists = {fileInfo.Exists}");
                    //if (fileInfo.Exists)
                    //{
                    //    string designCapacity;
                    //    string fullChargeCapacity;

                    //    string DesignValue;
                    //    string FullValue;

                    //    double DesignCapa;
                    //    double FullCapa;

                    //    string textValue = File.ReadAllText(filePath);
                    //    string text = Regex.Replace(textValue, @"<(.|\n)*?>", string.Empty);

                    //    int indexReport = text.IndexOf("Report generated");

                    //    if (indexReport > 0)
                    //    {
                    //        int indexDesign = text.IndexOf("DESIGN CAPACITY");
                    //        int indexFull = text.IndexOf("FULL CHARGE CAPACITY");
                    //        int indexCycle = text.IndexOf("CYCLE COUNT");

                    //        designCapacity = text.Substring(indexDesign + 15, indexFull - indexDesign - 15).Replace("\r\n", string.Empty);
                    //        fullChargeCapacity = text.Substring(indexFull + 20, indexCycle - indexFull - 20).Replace("\r\n", string.Empty);

                    //        DesignValue = Regex.Replace(designCapacity, @"\D", "");
                    //        FullValue = Regex.Replace(fullChargeCapacity, @"\D", "");


                    //        DesignCapa = ConvertUtil.ToDouble(DesignValue);
                    //        FullCapa = ConvertUtil.ToDouble(FullValue);

                    //        ProjectInfo._batteryRemain = (FullCapa / DesignCapa) * 100;
                    //    }
                    //    else
                    //    {
                    //        ProjectInfo._batteryRemain = 0;
                    //    }




                    //File.Delete(filePath);
                    //}
                }

            }
            catch (Exception ex)
            {
                _msg += "CAM로드 실패/";
            }
        }


        private List<DeviceInfo> GetVGAData()
        {
            List<string> listVgaIngernal = new List<string>();
            bool isInternalFile = false;
            SelectQuery selectQuery = new SelectQuery("Win32_VideoController");

            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(selectQuery);

            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
                isInternalFile = false;

                foreach (PropertyData propertyData in managementObject.Properties)
                {
                    string valueString = null;

                    if (propertyData.Value != null)
                    {
                        valueString = propertyData.Value.ToString();
                    }

                    string[] itemArray = { propertyData.Name, valueString };

                    if (itemArray[0].Equals("AdapterDACType"))
                    {
                        string type = itemArray[1];

                        if (type.Equals("Internal"))
                            isInternalFile = true;
                    }
                    else if (itemArray[0].Equals("Name"))
                    {
                        if (isInternalFile)
                        {
                            string name = itemArray[1];

                            if (!listVgaIngernal.Contains(name))
                                listVgaIngernal.Add(name);
                        }
                    }
                }
            }

            List<DeviceInfo> list = new List<DeviceInfo>();
            int k = 0;

            try
            {
                int vga_count = sdk_instance.GetNumberOfDisplayAdapter();

                //_deviceCnt["VGA"] = vga_count;
                //vga_count = vga_count > 2 ? 2 : vga_count; // max는 2개까지임
                //int i = 0;
                for (int i = 0; i < vga_count; i++)
                {
                    // 웹에서는 1개만 받게 구현되어 있음 
                    // VGA_MODEL
                    string model = sdk_instance.GetDisplayAdapterName(i);

                    //Dangol.Message("model = " + model);

                    if (string.IsNullOrWhiteSpace(model))
                        continue;


                    list.Add(new DeviceInfo($"VGA_{k}_MODEL", model));

                    if (listVgaIngernal.Contains(model))
                        list.Add(new DeviceInfo($"VGA_{k}_EX_TYPE", "0"));
                    else
                        list.Add(new DeviceInfo($"VGA_{k}_EX_TYPE", "1"));

                    // VGA_MANUFACTURE            
                    string manufacturer = "";
                    int vendor_id = 0, device_id = 0, revision_id = 0, sub_vendor_id = 0, sub_model_id = 0;
                    if (sdk_instance.GetDisplayAdapterPCIID(i, ref vendor_id, ref device_id, ref revision_id, ref sub_vendor_id, ref sub_model_id))
                    {
                        //value = "GIGABYTE Technology";
                        manufacturer = GetManufacture(sub_vendor_id);

                    }
                    else
                    {
                        manufacturer = "";
                    }

                    list.Add(new DeviceInfo($"VGA_{k}_MANUFACTURE", manufacturer));

                    // VGA_REVISION
                    string revision = "";
                    revision = String.Format("{0:x2}", revision_id); // 4글자 헥사
                    if (!string.IsNullOrEmpty(revision))
                        revision = revision.ToUpper();
                    list.Add(new DeviceInfo($"VGA_{k}_REVISION", revision));

                    string codeName = sdk_instance.GetDisplayAdapterCodeName(i);
                    list.Add(new DeviceInfo($"VGA_{k}_CODENAME", codeName));

                    //// VGA_CORE_FAMILY
                    //int core = 0;
                    //value = sdk_instance.GetDisplayAdapterCoreFamily(i, ref core);
                    //list.Add(new DeviceInfo($"VGA_{i}_CORE_FAMILY", value));

                    // VGA_TECH
                    string process = "";
                    float fValue = sdk_instance.GetDisplayAdapterManufacturingProcess(i);
                    if (sdk_instance.IS_F_DEFINED(fValue))
                    {
                        if (fValue < 0.10f)
                        {
                            float fVal = fValue * 1000.0f;
                            process = Convert.ToString((int)(fVal + 0.5f)) + " nm";
                        }
                        else
                        {
                            process = Convert.ToString(fValue) + " um";
                        }
                    }
                    else
                    {
                        process = "";
                    }


                    list.Add(new DeviceInfo($"VGA_{k}_PROCESS", process));

                    // VGA_MEM_SIZE
                    string memSize = "";
                    int size = 0;
                    if (sdk_instance.GetDisplayAdapterMemorySize(i, ref size))
                    {
                        if (sdk_instance.IS_I_DEFINED(size))
                        {
                            memSize = size.ToString() + " MBytes";
                        }
                    }
                    else
                    {
                        memSize = "";
                    }
                    list.Add(new DeviceInfo($"VGA_{k}_MEM_SIZE", memSize));

                    // VGA_MEM_TYPE
                    string memType = "";
                    int mem_type = 0;
                    if (sdk_instance.GetDisplayAdapterMemoryType(i, ref mem_type))
                    {
                        memType = GetDisplayAdapterMemoryName(mem_type);
                    }
                    else
                    {
                        memType = "";
                    }
                    list.Add(new DeviceInfo($"VGA_{k}_MEM_TYPE", memType));

                    // VGA_MEM_VENDOR
                    string vendor = "";
                    vendor = sdk_instance.GetDisplayAdapterMemoryVendor(i);
                    list.Add(new DeviceInfo($"VGA_{k}_MEM_VENDOR", vendor));

                    k++;
                }
            }
            catch (Exception ex)
            {


                _msg += "VGA 로드 실패/";
                return list;
            }

            _deviceCnt["VGA"] = k;
            _msg += "VGA 로드 성공/";
            return list;
        }

        string GetDisplayAdapterMemoryName(int type)
        {
            string memory_name = "";
            switch (type)
            {
                case CPUIDSDK.MEMORY_TYPE_DDR:
                    memory_name = "DDR";
                    break;
                case CPUIDSDK.MEMORY_TYPE_DDR2:
                    memory_name = "DDR2";
                    break;
                case CPUIDSDK.MEMORY_TYPE_DDR3:
                    memory_name = "DDR3";
                    break;
                case CPUIDSDK.MEMORY_TYPE_GDDR3:
                    memory_name = "GDDR3";
                    break;
                case CPUIDSDK.MEMORY_TYPE_GDDR4:
                    memory_name = "GDDR4";
                    break;
                case CPUIDSDK.MEMORY_TYPE_GDDR5:
                    memory_name = "GDDR5";
                    break;
                case CPUIDSDK.MEMORY_TYPE_GDDR5X:
                    memory_name = "GDDR5X";
                    break;
                case CPUIDSDK.MEMORY_TYPE_HBM1:
                    memory_name = "HBM1";
                    break;
                case CPUIDSDK.MEMORY_TYPE_HBM2:
                    memory_name = "HBM2";
                    break;
                case CPUIDSDK.MEMORY_TYPE_SDDR4:
                    memory_name = "SDDR4";
                    break;
                case CPUIDSDK.MEMORY_TYPE_GDDR6:
                    memory_name = "GDDR6";
                    break;
                default:
                    memory_name = "";
                    break;
            }

            return memory_name;
        }

        string GetManufacture(int sub_vendor_id)
        {
            string manufacture = "";
            switch (sub_vendor_id)
            {
                case 0x10DE:
                    manufacture = "NVIDIA Corporation"; break;

                case 0x1043:
                    manufacture = "ASUSTeK Computer Inc.";
                    break;
                case 0x1458:
                    manufacture = "GIGABYTE Technology";
                    break;
                case 0x1462:
                    manufacture = "Micro-Star International Co., Ltd. (MSI)";
                    break;
                case 0x8086:
                    manufacture = "Intel Corporation";
                    break;
                case 0x1545:
                    manufacture = "Visiontek";
                    break;
                case 0x1022:
                    manufacture = "Advanced Micro Devices Inc. (AMD)";
                    break;
                case 0x1787:
                    manufacture = "Hightech Information System Ltd. (HIS)";
                    break;
                case 0x1092:
                    manufacture = "Diamond";
                    break;
                case 0x16F3:
                    manufacture = "Jetway";
                    break;
                case 0x148C:
                    manufacture = "PowerColor";
                    break;
                case 0x1682:
                    manufacture = "XFX Pine Group Inc.";
                    break;
                case 0x0721:
                    manufacture = "Sapphire, Inc.";
                    break;
                case 0x1569:
                    manufacture = "Palit Microsystems";
                    break;
                case 0x1554:
                    manufacture = "Prolink Microsystems Corp.";
                    break;
                case 0x3842:
                    manufacture = "EVGA Corp.";
                    break;
                case 0x19DA:
                    manufacture = "ZOTAC International Ltd.";
                    break;
                case 0x196D:
                    manufacture = "Club 3D";
                    break;
                case 0x18BC:
                    manufacture = "GeCube";
                    break;
                case 0x10B0:
                    manufacture = "Gainward";
                    break;
                case 0x1B0A:
                    manufacture = "Pegatron";
                    break;
                case 0x2A15:
                    manufacture = "3D Vision";
                    break;
                case 0x1849:
                    manufacture = "ASRock Inc.";
                    break;
                case 0x17AA:
                    manufacture = "Lenovo";
                    break;
                case 0x1179:
                    manufacture = "Toshiba";
                    break;
                case 0x1775:
                    manufacture = "General Electric";
                    break;
                case 0x1558:
                    manufacture = "Clevo";
                    break;
                case 0x1297:
                    manufacture = "Shuttle";
                    break;
                case 0x174B:
                    manufacture = "PC Partner";
                    break;
                case 0x103C:
                    manufacture = "Hewlett-Packard";
                    break;
                case 0x1B4C:
                    manufacture = "Galaxy Microsystems Ltd.";
                    break;
                case 0x7377:
                    manufacture = "Colorful";
                    break;
                case 0x1565:
                    manufacture = "Biostar";
                    break;
                case 0x1028:
                    manufacture = "Dell";
                    break;
                case 0x106B:
                    manufacture = "Apple";
                    break;
                case 0x107D:
                    manufacture = "Leadtek";
                    break;
                case 0x1D17:
                    manufacture = "Zhaoxin";
                    break;
                case 0x1002:
                    manufacture = "ATI/AMD";
                    break;
                default:
                    manufacture = String.Format("{0:x4}", sub_vendor_id);
                    break;
            }

            return manufacture;
        }
    }
}
