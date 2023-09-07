using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
//using WareHousingMaster.view.release;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using ScreenCopy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHousingMaster.main;
using WareHousingMaster.UtilTest;
using WareHousingMaster.view.adjustment;
using WareHousingMaster.view.board;
using WareHousingMaster.view.check;
using WareHousingMaster.view.common;
using WareHousingMaster.view.common.kbooks;
using WareHousingMaster.view.consigned;
using WareHousingMaster.view.kbooks.returns;
using WareHousingMaster.view.kbooks.search.bookDetail;
using WareHousingMaster.view.kbooks.search.booksearch;
using WareHousingMaster.view.kbooks.user;
using WareHousingMaster.view.login;
using WareHousingMaster.view.PreView;
using WareHousingMaster.view.price.ntb;
using WareHousingMaster.view.price.partPriceMapping;
using WareHousingMaster.view.produce;
using WareHousingMaster.view.QC;
using WareHousingMaster.view.release;
using WareHousingMaster.view.release.External;
using WareHousingMaster.view.repair;
using WareHousingMaster.view.search;
using WareHousingMaster.view.statistics;
using WareHousingMaster.view.usedPurchase;
using WareHousingMaster.view.warehousing;
using WareHousingMaster.view.warehousingManagement;

namespace WareHousingMaster.view.main
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Dictionary<BarButtonItem, XtraForm> ribbonTabs = new Dictionary<BarButtonItem, XtraForm>();

        public Main()
        {
            InitializeComponent();
            if (ProjectInfo._ISDEBUG)
            {
                UserLookAndFeel.Default.SetSkinStyle("The Bezier");
                this.Text = "KBOOKS PLM TEST TEST TEST";
                //this.ribbonLogoHelper1.Image = global::WareHousingMaster.Properties.Resources.title_img_for_test;
            }
            else
            {
                SkinHelper.InitSkinGallery(rgbiSkins);
            }
            //SkinHelper.InitSkinGallery(rgbiSkins);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Icon = ProjectInfo.ProjectIcon;
            Util.GetINIInfo();
            //externalIPCheck();
            login();
            setLoginInitialize();
            //checkVersion();
            //setInitData();
            //showMain();
            //ProjectInfo.setDatatable();
            //ConsignedInfo.setDatatable();

            bsiDate.Caption = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bsiVersion.Caption = ProjectInfo._version;

            //ProjectInfo._tabbedView = tabbedView1;
            //ProjectInfo._documentManager = documentManager;
            //ProjectInfo._ribbonTabs = ribbonTabs;
        }

        private void setLoginInitialize()
        {
            bsiUserNm.Caption = ProjectInfo._userName;

            ProjectInfo._tabbedView = tabbedView1;
            ProjectInfo._documentManager = documentManager;
            ProjectInfo._ribbonTabs = ribbonTabs;

            ProjectInfo._bbiOrderCartInfo = bbiOrderCartInfo;

            //if (ProjectInfo._userType.Equals("M"))
            //    rptest.Visible = true;
            //else
            //    rptest.Visible = false;

            //rpgAdjustmentExcel.Visible = false;
            //rpgAdjustmentStatistics.Visible = false;

            //if (ProjectInfo._userType.Equals("E"))
            //{
            //    rpWarehousing.Visible = false;
            //    rpProduce.Visible = false;
            //    rpRepair.Visible = false;
            //    rpInventory.Visible = false;
            //    rpCheck.Visible = false;
            //    rpWarehousingManagement.Visible = false;
            //    rpUsedPurchase.Visible = false;
            //    rpPrice.Visible = false;
            //    rpAdjustment.Visible = false;
            //    rpScreen.Visible = false;
            //    rpAdjustment.Visible = false;
            //    rpgConsigned.Visible = false;
            //    rpEtc.Visible = false;
            //    rpgJob.Visible = false;
            //    rpRelease.Visible = false;
            //    rpgStatistics.Visible = false;



            //    biAdjustment.Visibility = BarItemVisibility.Never;
            //    biAdjustmentExamine.Visibility = BarItemVisibility.Never;
            //    bbiAdjustmentComplete.Visibility = BarItemVisibility.Never;
            //    biAdjustmentUpdate.Visibility = BarItemVisibility.Never;
            //    biAdjustmentReceiptList.Visibility = BarItemVisibility.Never;            
            //    biCustomerPaymentState.Visibility = BarItemVisibility.Never;
            //    bbiConsignedReleasePart.Visibility = BarItemVisibility.Never;
            //    bbiCompanyModelList.Visibility = BarItemVisibility.Never;

            //}
            //else
            //{
            //    rpWarehousing.Visible = true;
            //    rpProduce.Visible = true;
            //    rpRepair.Visible = true;
            //    rpInventory.Visible = true;
            //    rpCheck.Visible = true;
            //    rpWarehousingManagement.Visible = true;
            //    rpUsedPurchase.Visible = true;
            //    //rpUsedPurchase.Visible = false;
            //    rpScreen.Visible = true;
            //    rpgConsigned.Visible = true;
            //    rpEtc.Visible = true;
            //    rpgJob.Visible = true;
            //    rpRelease.Visible = true;
            //    rpgStatistics.Visible = true;

            //    //if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))

            //    //else
            //    //    rpUsedPurchase.Visible = false;

            //    bbiConsignedReleasePart.Visibility = BarItemVisibility.Always;

            //    if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S"))
            //    {
            //        biAdjustment.Visibility = BarItemVisibility.Always;                   
            //        biCustomerPaymentState.Visibility = BarItemVisibility.Always;
            //        bbiCompanyModelList.Visibility = BarItemVisibility.Always;
            //        prgCustomerPaymentState.Visible = true;

            //    }
            //    else
            //    {
            //        biAdjustment.Visibility = BarItemVisibility.Never;                  
            //        biCustomerPaymentState.Visibility = BarItemVisibility.Never;
            //        bbiCompanyModelList.Visibility = BarItemVisibility.Never;
            //        prgCustomerPaymentState.Visible = false;

            //        if (ProjectInfo._userId.Equals("123"))
            //            biCustomerPaymentState.Visibility = BarItemVisibility.Always;
            //    }

            //    if (ProjectInfo._userType.Equals("M") || ProjectInfo._userType.Equals("S") || ProjectInfo._userType.Equals("G"))
            //    {

            //        rpAdjustment.Visible = true;
            //        biAdjustmentExamine.Visibility = BarItemVisibility.Always;
            //        bbiAdjustmentComplete.Visibility = BarItemVisibility.Always;

            //        if (ProjectInfo._lisAllowUser.Contains(ProjectInfo._userId) || ProjectInfo._userType.Equals("M"))
            //        {
            //            rpPrice.Visible = true;
            //            biAdjustmentUpdate.Visibility = BarItemVisibility.Always;
            //            biAdjustmentReceiptList.Visibility = BarItemVisibility.Always;
            //            rpgAdjustmentExcel.Visible = true;
            //            rpgAdjustmentStatistics.Visible = true;
            //        }
            //        else
            //        {
            //            rpPrice.Visible = false;
            //            biAdjustmentUpdate.Visibility = BarItemVisibility.Never;
            //            biAdjustmentReceiptList.Visibility = BarItemVisibility.Never;
            //        }
            //    }
            //    else
            //    {
            //        rpPrice.Visible = false;
            //        rpAdjustment.Visible = false;

            //        biAdjustmentExamine.Visibility = BarItemVisibility.Never;
            //        biAdjustmentUpdate.Visibility = BarItemVisibility.Never;
            //        biAdjustmentReceiptList.Visibility = BarItemVisibility.Never;
            //    }
            //}
        }

        private void internalIPCheck()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Dangol.Message(ip.ToString());
                    }
                }
            }catch(Exception e)
            {
                throw new Exception("No network adapters with an IPv4 address in the system!");
            } 
        }
        private void externalIPCheck()
        {
            try
            {
                //string externalip = new WebClient().DownloadString("http://ipinfo.io/ip").Trim(); //http://icanhazip.com
                //                                                                                  //Dangol.Message(externalip);
                //DataTable dtAllowIp = Util.getTable("TN_ALLOW_IP", "IP_ID, IP, LOCATION, TYPE", "USE_YN = 1", "IP_ID ASC");
                ////DataTable dtAllowIp = Util.getCodeListCustom("TN_ALLOW_IP", "IP_ID", "IP", "TYPE = 1", "IP_ID ASC");
                //DataRow[] rows = dtAllowIp.Select($"IP = '{externalip}'");

                //if(rows.Length < 1)
                //{
                //    Dangol.Error("현재 접속하신 IP는 허가되지 않은 IP입니다.\nIP 허가 요청은 관리자에게 문의하세요.");
                //    Util.ExitProgram();
                //}

                //if (ConvertUtil.ToInt32(rows[0]["LOCATION"]) == 1)
                //    ProjectInfo._IPType = "I";
                //else
                    ProjectInfo._IPType = "E";
            }
            catch (Exception e)
            {
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
        }

        private void login()
        {
            ProjectInfo._userType = "M";

            if (!ProjectInfo._autoLogin)
            {
                ProjectInfo._userPasswd = "";
                usrControlLogin login = new usrControlLogin(ProjectInfo._userId, ProjectInfo._userPasswd);

                login.StartPosition = FormStartPosition.Manual;
                login.Location = new Point(this.Location.X + (this.Size.Width / 2) - (login.Size.Width / 2),
                this.Location.Y + (this.Size.Height / 2) - (login.Size.Height / 2));

                if (login.ShowDialog(this) == DialogResult.OK)
                {
                    ProjectInfo._userId = login._userId;
                    ProjectInfo._userPasswd = login._userPasswd;
                    ProjectInfo._userType = "M";
                }
                else
                    Util.ExitProgram();
            }
            else
            {
                if (!DBConnect.SendUserCheckRequest(ProjectInfo._userId, ProjectInfo._userPasswd))
                {
                    Dangol.Error("아이디 또는 비밀번호가 올바르지 않습니다.");

                    ProjectInfo._userPasswd = "";
                    usrControlLogin login = new usrControlLogin(ProjectInfo._userId, ProjectInfo._userPasswd);

                    login.StartPosition = FormStartPosition.Manual;
                    login.Location = new Point(this.Location.X + (this.Size.Width / 2) - (login.Size.Width / 2),
                    this.Location.Y + (this.Size.Height / 2) - (login.Size.Height / 2));

                    if (login.ShowDialog(this) == DialogResult.OK)
                    {
                        ProjectInfo._userId = login._userId;
                        ProjectInfo._userPasswd = login._userPasswd;
                        ProjectInfo._userType = "M";
                    }
                    else
                        Util.ExitProgram();
                }
            }
        }

        private void checkVersion()
        {
            Assembly assemObj = Assembly.GetExecutingAssembly();
            Version version = assemObj.GetName().Version;
            ProjectInfo._version = version.ToString();

            DataTable dtVersion = Util.getTable("TN_VERSION_EXTERNAL", "VERSION_ID, VERSION, CONTENT", "VERSION_ID > 0", "VERSION_ID DESC LIMIT 1");

            if(dtVersion.Rows.Count < 1)
            {
                Dangol.Error("ERROR");
                Util.ExitProgram();
            }

            DataRow row = dtVersion.Select("VERSION_ID > 0")[0];
            string latestVersion = ConvertUtil.ToString(row["VERSION"]);
            string content = ConvertUtil.ToString(row["CONTENT"]);

            LoginCheck.checkVersion(version, latestVersion, 0, content);
        }


        private void setInitData()
        {
            SplashWaitForm.buttonImage = SplashWaitForm.CreateButtonImage();
            SplashWaitForm.hotButtonImage = SplashWaitForm.CreateHotButtonImage();
            SplashWaitForm._overlayLabel = new OverlayTextPainter();
            SplashWaitForm._overlayButton = new OverlayImagePainter(SplashWaitForm.buttonImage, SplashWaitForm.hotButtonImage, SplashWaitForm.OnCancelButtonClick);

            ProjectInfo._isExistNtbCheckWarehousing = false;
            ProjectInfo._isExistNtbCheckRelease = false;
            ProjectInfo._isExistNtbCheckRepair = false;

        }

        private void logout()
        {
            usrControlLogin login = new usrControlLogin("", "");

            login.StartPosition = FormStartPosition.Manual;
            login.Location = new Point(this.Location.X + (this.Size.Width / 2) - (login.Size.Width / 2),
            this.Location.Y + (this.Size.Height / 2) - (login.Size.Height / 2));

            if (login.ShowDialog(this) == DialogResult.OK)
            {
                ProjectInfo._userId = login._userId;
                ProjectInfo._userPasswd = login._userPasswd;
                setLoginInitialize();
            }
        }

        private void bbiLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            logout();
        }

        private void bbiLogout2_ItemClick(object sender, ItemClickEventArgs e)
        {
            logout();
        }


        private void bbiRepNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            dlgRepNo repNo = new dlgRepNo();

            repNo.StartPosition = FormStartPosition.Manual;
            repNo.Location = new Point(this.Location.X + (this.Size.Width / 2) - (repNo.Size.Width / 2),
            this.Location.Y + (this.Size.Height / 2) - (repNo.Size.Height / 2));

            if (repNo.ShowDialog(this) == DialogResult.OK)
            {

            }
        }

        private void bbiSearchBookInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "도서정보조회";
            usrSearchBook searchBook = new usrSearchBook();
            setRibbonTabs(searchBook, tabName, bbiSearchBookInfo);
        }

        private void bbiSearchBookInfoDetail_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "도서상세정보";
            usrBookDetail bookDetail = new usrBookDetail(-1, 1, true);
            setRibbonTabs(bookDetail, tabName, bbiSearchBookInfoDetail);
        }

        private void bbiSearchNewBook_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "분야별신간조회";
            usrNewBooks newBooks = new usrNewBooks();
            setRibbonTabs(newBooks, tabName, bbiSearchNewBook); 
        }

        private void bbiSearchBookBest_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "분야별베스트";
            usrBestSeller bestSeller = new usrBestSeller();
            setRibbonTabs(bestSeller, tabName, bbiSearchBookBest);
        }

        private void bbiSearchCreditorInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "매입처조회(직)";
            usrSearchPurchase searchPurchase = new usrSearchPurchase();
            setRibbonTabs(searchPurchase, tabName, bbiSearchCreditorInfo);
        }

        private void bbiSearchPublisherInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "출판사조회(직)";
            usrSearchPublisher searchPublisher = new usrSearchPublisher();
            setRibbonTabs(searchPublisher, tabName, bbiSearchPublisherInfo);
        }



        private void bbiOrderCartInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "주문 예정입력";
            usrBookOrder bookOrder = new usrBookOrder();
            ProjectInfo._usrBookOrder = bookOrder;
            setRibbonTabs(bookOrder, tabName, bbiOrderCartInfo);
        }

        private void bbiOrderRegistBookInfo_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bbiOrderSaleInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "매장별판매데이터조회";
            usrSaleData saleData = new usrSaleData();
            setRibbonTabs(saleData, tabName, bbiOrderSaleInfo);
        }

        private void bbiOrderModifyOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "주문도서조회수정확정";
            usrOrderBookModify orderBookModify = new usrOrderBookModify();
            setRibbonTabs(orderBookModify, tabName, bbiOrderModifyOrder);
        }

        private void bbiOrderNonRegisterOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "미등록도서주문예정입력";
            usrUnregisteredBookOrder unregisteredBookOrder = new usrUnregisteredBookOrder();
            setRibbonTabs(unregisteredBookOrder, tabName, bbiOrderNonRegisterOrder);
        }

        private void bbiOrderBookAllList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "미등록/등록 주문도서 일람";
            usrBookOrderResult bookOrderResult = new usrBookOrderResult();
            setRibbonTabs(bookOrderResult, tabName, bbiOrderBookAllList);
        }

        private void bbiStoreOrderBookList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "매장별 주문도서 일람";
            usrOrderBookReport orderBookReport = new usrOrderBookReport();
            setRibbonTabs(orderBookReport, tabName, bbiStoreOrderBookList);
        }

        private void bbiReturnList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "반품예정입력";
            usrInputReturn inputReturn = new usrInputReturn();
            setRibbonTabs(inputReturn, tabName, bbiReturnList);
        }

        private void bbiReturnMidif_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "반품예정 수정/출력";
            usrModifReturn modifReturn = new usrModifReturn();
            setRibbonTabs(modifReturn, tabName, bbiReturnMidif);
        }
























































































        private void biUserManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "유저관리";
            usrUserManagement userManagement = new usrUserManagement();
            setRibbonTabs(userManagement, tabName, biUserManagement);
        }

        private void biWarehousing_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "입고";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                OnRunTaskItemClick();

                usrWarehousing warehousing = new usrWarehousing();
                warehousing.Tag = tabName;
                warehousing.MdiParent = this;
                warehousing.Show();
                if(!ribbonTabs.ContainsKey(biWarehousing))
                    ribbonTabs.Add(biWarehousing, warehousing);
                else
                {
                    ribbonTabs.Remove(biWarehousing);
                    ribbonTabs.Add(biWarehousing, warehousing);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehousing]);
            }
        }

        private void biWarehousingConsigned_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void biProduce_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "제품생산";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                OnRunTaskItemClick();

                usrProductProduceStandard ProductProduce = new usrProductProduceStandard();
                ProductProduce.Tag = tabName;
                ProductProduce.MdiParent = this;
                ProductProduce.Show();
                if (!ribbonTabs.ContainsKey(biProduce))
                    ribbonTabs.Add(biProduce, ProductProduce);
                else
                {
                    ribbonTabs.Remove(biProduce);
                    ribbonTabs.Add(biProduce, ProductProduce);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biProduce]);
            } 
        }

        private void bbiProductProductList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "제품생산 리스트";
            usrProductProduceList productProduceList = new usrProductProduceList();
            setRibbonTabs(productProduceList, tabName, bbiProductProductList);
        }

        private void bbiQc_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "QC";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                OnRunTaskItemClick();

                usrQualityControl qualityControl = new usrQualityControl();
                qualityControl.Tag = tabName;
                qualityControl.MdiParent = this;
                qualityControl.Show();
                if (!ribbonTabs.ContainsKey(bbiQc))
                    ribbonTabs.Add(bbiQc, qualityControl);
                else
                {
                    ribbonTabs.Remove(bbiQc);
                    ribbonTabs.Add(bbiQc, qualityControl);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[bbiQc]);
            }
        }


        private void bbiQCProductList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "QC제품 리스트";
            usrProduceProductCheckList produceProductCheckList = new usrProduceProductCheckList();
            setRibbonTabs(produceProductCheckList, tabName, bbiQCProductList);
        }



        private void biRepair_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "repair";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                OnRunTaskItemClick();

                usrRepairStandard repair = new usrRepairStandard();
                repair.Tag = tabName;
                repair.MdiParent = this;
                repair.Show();
                if (!ribbonTabs.ContainsKey(biRepair))
                    ribbonTabs.Add(biRepair, repair);
                else
                {
                    ribbonTabs.Remove(biRepair);
                    ribbonTabs.Add(biRepair, repair);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biRepair]);
            }
        }

        private void biConsigned_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "생산대행출고";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                OnRunTaskItemClickWOMatchingInfo();

                usrReleaseConsigned consigned = new usrReleaseConsigned();
                consigned.Tag = tabName;
                consigned.MdiParent = this;
                consigned.Show();
                if (!ribbonTabs.ContainsKey(biConsigned))
                    ribbonTabs.Add(biConsigned, consigned);
                else
                {
                    ribbonTabs.Remove(biConsigned);
                    ribbonTabs.Add(biConsigned, consigned);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biConsigned]);
            }
        }

        private void bbiExportReceipt_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "출고접수";
            usrReleaseReceipt releaseReceipt = new usrReleaseReceipt();
            setRibbonTabs(releaseReceipt, tabName, bbiExportReceipt);
        }

        private void bbiReleaseReceiptList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "출고접수리스트";
            usrReleaseReceiptList releaseReceiptList = new usrReleaseReceiptList();
            setRibbonTabs(releaseReceiptList, tabName, bbiReleaseReceiptList);
        }

        private void biComponentKeep_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "Stock Movement";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrComponentKeep componentKeep = new usrComponentKeep();
                componentKeep.Tag = tabName;
                componentKeep.MdiParent = this;
                componentKeep.Show();
                if (!ribbonTabs.ContainsKey(biComponentKeep))
                    ribbonTabs.Add(biComponentKeep, componentKeep);
                else
                {
                    ribbonTabs.Remove(biComponentKeep);
                    ribbonTabs.Add(biComponentKeep, componentKeep);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biComponentKeep]);
            }
        }

        private void biWarehouseMovementStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "Stock Movement Status";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrWarehouseMovementStatus warehouseMovementStatus = new usrWarehouseMovementStatus();
                warehouseMovementStatus.Tag = tabName;
                warehouseMovementStatus.MdiParent = this;
                warehouseMovementStatus.Show();
                if (!ribbonTabs.ContainsKey(biWarehouseMovementStatus))
                    ribbonTabs.Add(biWarehouseMovementStatus, warehouseMovementStatus);
                else
                {
                    ribbonTabs.Remove(biWarehouseMovementStatus);
                    ribbonTabs.Add(biWarehouseMovementStatus, warehouseMovementStatus);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehouseMovementStatus]);
            }
        }

        private void biWarehouseState_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "Warehouse Stock Status";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrWarehouseState warehouseState = new usrWarehouseState();
                warehouseState.Tag = tabName;
                warehouseState.MdiParent = this;
                warehouseState.Show();
                if (!ribbonTabs.ContainsKey(biWarehouseState))
                    ribbonTabs.Add(biWarehouseState, warehouseState);
                else
                {
                    ribbonTabs.Remove(biWarehouseState);
                    ribbonTabs.Add(biWarehouseState, warehouseState);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehouseState]);
            }
        }

        private void biComponentKeep1_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "Stock Movement";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrComponentKeep componentKeep = new usrComponentKeep();
                componentKeep.Tag = tabName;
                componentKeep.MdiParent = this;
                componentKeep.Show();
                if (!ribbonTabs.ContainsKey(biComponentKeep))
                    ribbonTabs.Add(biComponentKeep, componentKeep);
                else
                {
                    ribbonTabs.Remove(biComponentKeep);
                    ribbonTabs.Add(biComponentKeep, componentKeep);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biComponentKeep]);
            }
        }

        private void biWarehouseMovementStatus1_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "Stock Movement Status";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrWarehouseMovementStatus warehouseMovementStatus = new usrWarehouseMovementStatus();
                warehouseMovementStatus.Tag = tabName;
                warehouseMovementStatus.MdiParent = this;
                warehouseMovementStatus.Show();
                if (!ribbonTabs.ContainsKey(biWarehouseMovementStatus))
                    ribbonTabs.Add(biWarehouseMovementStatus, warehouseMovementStatus);
                else
                {
                    ribbonTabs.Remove(biWarehouseMovementStatus);
                    ribbonTabs.Add(biWarehouseMovementStatus, warehouseMovementStatus);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehouseMovementStatus]);
            }
        }

        private void biWarehouseState1_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "Warehouse Stock Status";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrWarehouseState warehouseState = new usrWarehouseState();
                warehouseState.Tag = tabName;
                warehouseState.MdiParent = this;
                warehouseState.Show();
                if (!ribbonTabs.ContainsKey(biWarehouseState))
                    ribbonTabs.Add(biWarehouseState, warehouseState);
                else
                {
                    ribbonTabs.Remove(biWarehouseState);
                    ribbonTabs.Add(biWarehouseState, warehouseState);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehouseState]);
            }
        }

        private void bbiCompanyModelList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "업체 모델 리스트";
            usrConsignedCompanyModelList consignedCompanyModelList = new usrConsignedCompanyModelList();
            setRibbonTabs(consignedCompanyModelList, tabName, bbiCompanyModelList);
        }

        private void biConsignedCompanyInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "consigned company inventory";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrConsignedCompanyInventory consignedCompanyInventory = new usrConsignedCompanyInventory();
                consignedCompanyInventory.Tag = tabName;
                consignedCompanyInventory.MdiParent = this;
                consignedCompanyInventory.Show();
                if (!ribbonTabs.ContainsKey(biConsignedCompanyInventory))
                    ribbonTabs.Add(biConsignedCompanyInventory, consignedCompanyInventory);
                else
                {
                    ribbonTabs.Remove(biConsignedCompanyInventory);
                    ribbonTabs.Add(biConsignedCompanyInventory, consignedCompanyInventory);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biConsignedCompanyInventory]);
            }
        }

        private void biAdjustment_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "consigned Adjustment";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrConsignedAdjustment consignedAdjustment = new usrConsignedAdjustment();
                consignedAdjustment.Tag = tabName;
                consignedAdjustment.MdiParent = this;
                consignedAdjustment.Show();
                if (!ribbonTabs.ContainsKey(biAdjustment))
                    ribbonTabs.Add(biAdjustment, consignedAdjustment);
                else
                {
                    ribbonTabs.Remove(biAdjustment);
                    ribbonTabs.Add(biAdjustment, consignedAdjustment);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biAdjustment]);
            }
        }

        private void biAdjustmentReceiptList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "adjustment receipt list";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrAdjustmentReceiptList adjustmentReceiptList = new usrAdjustmentReceiptList();
                adjustmentReceiptList.Tag = tabName;
                adjustmentReceiptList.MdiParent = this;
                adjustmentReceiptList.Show();
                if (!ribbonTabs.ContainsKey(biAdjustmentReceiptList))
                    ribbonTabs.Add(biAdjustmentReceiptList, adjustmentReceiptList);
                else
                {
                    ribbonTabs.Remove(biAdjustmentReceiptList);
                    ribbonTabs.Add(biAdjustmentReceiptList, adjustmentReceiptList);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biAdjustmentReceiptList]);
            }
        }
        private void biAdjustmentUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "consigned Adjustment update";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrAdjustmentUpdateList adjustmentUpdateList = new usrAdjustmentUpdateList();
                adjustmentUpdateList.Tag = tabName;
                adjustmentUpdateList.MdiParent = this;
                adjustmentUpdateList.Show();
                if (!ribbonTabs.ContainsKey(biAdjustmentUpdate))
                    ribbonTabs.Add(biAdjustmentUpdate, adjustmentUpdateList);
                else
                {
                    ribbonTabs.Remove(biAdjustmentUpdate);
                    ribbonTabs.Add(biAdjustmentUpdate, adjustmentUpdateList);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biAdjustmentUpdate]);
            }
        }

        private void biProductCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            //string tabName = "product Cheke Management";
            //if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            //{
            //    usrWarehousingProductCheck productCheck = new usrWarehousingProductCheck();
            //    productCheck.Tag = tabName;
            //    productCheck.MdiParent = this;
            //    productCheck.Show();
            //    if (!ribbonTabs.ContainsKey(biProductCheck))
            //        ribbonTabs.Add(biProductCheck, productCheck);
            //    else
            //    {
            //        ribbonTabs.Remove(biProductCheck);
            //        ribbonTabs.Add(biProductCheck, productCheck);
            //    }
            //}
            //else
            //{
            //    documentManager.View.ActivateDocument(ribbonTabs[biProductCheck]);
            //}

            string tabName = "product Cheke Management";
            usrWarehousingProductCheck productCheck = new usrWarehousingProductCheck();
            setRibbonTabs(productCheck, tabName, biProductCheck);
        }


        private void biProduceProductCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "produce product Cheke Management";
            usrProduceProductCheck procudeProductCheck = new usrProduceProductCheck();
            setRibbonTabs(procudeProductCheck, tabName, biProduceProductCheck);
        }



        private void biUsedPurchaseStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "used Purchase";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                usrUsedPurchaseStatus usedPurchaseStatus = new usrUsedPurchaseStatus();
                usedPurchaseStatus.Tag = tabName;
                usedPurchaseStatus.MdiParent = this;
                usedPurchaseStatus.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biUsedPurchaseStatus))
                    ribbonTabs.Add(biUsedPurchaseStatus, usedPurchaseStatus);
                else
                {
                    ribbonTabs.Remove(biUsedPurchaseStatus);
                    ribbonTabs.Add(biUsedPurchaseStatus, usedPurchaseStatus);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biUsedPurchaseStatus]);
                Dangol.CloseSplash();
            }
        }

        private void biUsedPurchaseRelease_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "used Purchase release";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                usrUsedPurchaseReturnList usedPurchaseReturnList = new usrUsedPurchaseReturnList();
                usedPurchaseReturnList.Tag = tabName;
                usedPurchaseReturnList.MdiParent = this;
                usedPurchaseReturnList.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biUsedPurchaseStatus))
                    ribbonTabs.Add(biUsedPurchaseRelease, usedPurchaseReturnList);
                else
                {
                    ribbonTabs.Remove(biUsedPurchaseRelease);
                    ribbonTabs.Add(biUsedPurchaseRelease, usedPurchaseReturnList);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biUsedPurchaseRelease]);
                Dangol.CloseSplash();
            }
        }

        private void biCustomerPaymentState_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "used purchase payment";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                usrCustomerPaymentState customerPaymentState = new usrCustomerPaymentState();
                customerPaymentState.Tag = tabName;
                customerPaymentState.MdiParent = this;
                customerPaymentState.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biCustomerPaymentState))
                    ribbonTabs.Add(biCustomerPaymentState, customerPaymentState);
                else
                {
                    ribbonTabs.Remove(biCustomerPaymentState);
                    ribbonTabs.Add(biCustomerPaymentState, customerPaymentState);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biCustomerPaymentState]);
                Dangol.CloseSplash();
            }
        }

        private void biWarehousingPartDetail1_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "warehousingPart Management1";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrWarehousingManagement1 warehousingManagement = new usrWarehousingManagement1();
                warehousingManagement.Tag = tabName;
                warehousingManagement.MdiParent = this;
                warehousingManagement.Show();
                if (!ribbonTabs.ContainsKey(biWarehousingPartDetail1))
                    ribbonTabs.Add(biWarehousingPartDetail1, warehousingManagement);
                else
                {
                    ribbonTabs.Remove(biWarehousingPartDetail1);
                    ribbonTabs.Add(biWarehousingPartDetail1, warehousingManagement);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehousingPartManagement]);
            }
        }

        private void biWarehousingPartManagementNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "warehousingPart Management New";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();

                usrInventoryCheck inventoryCheck = new usrInventoryCheck();
                inventoryCheck.Tag = tabName;
                inventoryCheck.MdiParent = this;
                inventoryCheck.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biWarehousingPartManagementNew))
                    ribbonTabs.Add(biWarehousingPartManagementNew, inventoryCheck);
                else
                {
                    ribbonTabs.Remove(biWarehousingPartManagementNew);
                    ribbonTabs.Add(biWarehousingPartManagementNew, inventoryCheck);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biWarehousingPartManagementNew]);
                Dangol.CloseSplash();
            }
        }

       

        private void biWarehousingPartManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "warehousingPart Management";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrWarehousingManagement warehousingManagement = new usrWarehousingManagement();
                warehousingManagement.Tag = tabName;
                warehousingManagement.MdiParent = this;
                warehousingManagement.Show();
                if (!ribbonTabs.ContainsKey(biWarehousingPartManagement))
                    ribbonTabs.Add(biWarehousingPartManagement, warehousingManagement);
                else
                {
                    ribbonTabs.Remove(biWarehousingPartManagement);
                    ribbonTabs.Add(biWarehousingPartManagement, warehousingManagement);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehousingPartManagement]);
            }  
        }

        private void biWarehousingProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "warehousingProduct Management";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrWarehousingProductManagement warehousingProductManagement = new usrWarehousingProductManagement();
                warehousingProductManagement.Tag = tabName;
                warehousingProductManagement.MdiParent = this;
                warehousingProductManagement.Show();
                if (!ribbonTabs.ContainsKey(biWarehousingProduct))
                    ribbonTabs.Add(biWarehousingProduct, warehousingProductManagement);
                else
                {
                    ribbonTabs.Remove(biWarehousingProduct);
                    ribbonTabs.Add(biWarehousingProduct, warehousingProductManagement);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehousingProduct]);
            }
        }

        private void biWarehousingSwap_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "warehousing Part Swap";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrWarehousingSwap warehousingSwap = new usrWarehousingSwap();
                warehousingSwap.Tag = tabName;
                warehousingSwap.MdiParent = this;
                warehousingSwap.Show();
                if (!ribbonTabs.ContainsKey(biWarehousingSwap))
                    ribbonTabs.Add(biWarehousingSwap, warehousingSwap);
                else
                {
                    ribbonTabs.Remove(biWarehousingSwap);
                    ribbonTabs.Add(biWarehousingSwap, warehousingSwap);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biWarehousingSwap]);
            }
        }

        private void biProductSwap_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "product Swap";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrProductSwap productSwap = new usrProductSwap();
                productSwap.Tag = tabName;
                productSwap.MdiParent = this;
                productSwap.Show();
                if (!ribbonTabs.ContainsKey(biProductSwap))
                    ribbonTabs.Add(biProductSwap, productSwap);
                else
                {
                    ribbonTabs.Remove(biProductSwap);
                    ribbonTabs.Add(biProductSwap, productSwap);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biProductSwap]);
            }
        }

        private void biExamPartList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "warehousingPart Examine";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                usrUsedPurchaseExamine usedPurchaseExamine = new usrUsedPurchaseExamine();
                usedPurchaseExamine.Tag = tabName;
                usedPurchaseExamine.MdiParent = this;
                usedPurchaseExamine.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biExamPartList))
                    ribbonTabs.Add(biExamPartList, usedPurchaseExamine);
                else
                {
                    ribbonTabs.Remove(biExamPartList);
                    ribbonTabs.Add(biExamPartList, usedPurchaseExamine);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biExamPartList]);
                Dangol.CloseSplash();
            }
        }
        
        private void biKeyboardTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            string keyboardTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\key_test.exe";
            Process.Start(keyboardTestProgramPath);
        }

        private void biBatteryTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            //string batteryTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\BattStatInstall-099b.exe";
            string batteryTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\Battery Status\BattStat.exe";
            //Process.Start(batteryTestProgramPath);
            ProcessStartInfo startInfo = new ProcessStartInfo(batteryTestProgramPath);
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            Process.Start(startInfo);
            
        }

        private void biSTGTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            //string storageTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\hdsentinel_pro_setup.exe";
            string storageTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\Hard Disk Sentinel\HDSentinel.exe";
            Process.Start(storageTestProgramPath);



            //Process flash = new Process();
            //flash.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            //flash.StartInfo.FileName = storageTestProgramPath;
            //flash.Start();
            //Thread.Sleep(100);
            //IntPtr id = flash.MainWindowHandle;
            //MoveWindow(flash.MainWindowHandle, 300, 300, 500, 500, true);
        }

        private void bbiVideoPlay_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (DlgVideoPlayer vdeoPlayer = new DlgVideoPlayer())
            {
                if (vdeoPlayer.ShowDialog(this) == DialogResult.OK)
                {

                }
            }
        }

        private void biCamTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("microsoft.windows.camera:");

            //Process flash = new Process();
            //flash.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            //flash.StartInfo.FileName = "microsoft.windows.camera:";
            //flash.Start();
        //    Thread.Sleep(100);
        //    IntPtr id = flash.MainWindowHandle;
        //    MoveWindow(flash.MainWindowHandle, 0, 0, 500, 500, true);
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private void biSoundTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "DlgSoundTest")
                {
                    frm.Activate();
                    return;
                }
            }

            using (DlgSoundTest soundTest = new DlgSoundTest())
            {
                if (soundTest.ShowDialog(this) == DialogResult.OK)
                {

                }
            }
        }

        private async Task cameraTestAsync()
        {
            //CameraCaptureUI dialog = new CameraCaptureUI();
            //StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
        }


        private void biReceipt_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "Consigned Receipt";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrConsignedReceipt consignedReceipt = new usrConsignedReceipt();
                consignedReceipt.Tag = tabName;
                consignedReceipt.MdiParent = this;
                consignedReceipt.Show();
                if (!ribbonTabs.ContainsKey(biReceipt))
                    ribbonTabs.Add(biReceipt, consignedReceipt);
                else
                {
                    ribbonTabs.Remove(biReceipt);
                    ribbonTabs.Add(biReceipt, consignedReceipt);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biReceipt]);
            }
        }

        private void biInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "Inventory List";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrInventoryList inventoryList = new usrInventoryList();
                inventoryList.Tag = tabName;
                inventoryList.MdiParent = this;
                inventoryList.Show();
                if (!ribbonTabs.ContainsKey(biInventory))
                    ribbonTabs.Add(biInventory, inventoryList);
                else
                {
                    ribbonTabs.Remove(biInventory);
                    ribbonTabs.Add(biInventory, inventoryList);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biInventory]);
            }
        }

        private void bbiInventorySearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "부품 리스트 검색";
            usrInventorySearchList inventorySearchList = new usrInventorySearchList();
            setRibbonTabs(inventorySearchList, tabName, bbiInventorySearch);
        }

        private void bbiTebletSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "태블릿 검색";
            usrTabletCheckSearchList TabletCheckSearchList = new usrTabletCheckSearchList();
            setRibbonTabs(TabletCheckSearchList, tabName, bbiTebletSearch);
        }

        private void biConsignedAllList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "consigned All List";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                usrConsignedList consignedList = new usrConsignedList();
                consignedList.Tag = tabName;
                consignedList.MdiParent = this;
                consignedList.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biConsignedAllList))
                    ribbonTabs.Add(biConsignedAllList, consignedList);
                else
                {
                    ribbonTabs.Remove(biConsignedAllList);
                    ribbonTabs.Add(biConsignedAllList, consignedList);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biConsignedAllList]);
                Dangol.CloseSplash();
            }
        }

        private void bbiConsignedReturnList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "생산대행 반품 리스트";
            usrConsignedReturnList consignedReturnList = new usrConsignedReturnList();
            setRibbonTabs(consignedReturnList, tabName, bbiConsignedReturnList);
        }

        private void biConsignedSummaryList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "consigned Summary List";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                usrConsignedSummaryList consignedSummaryList = new usrConsignedSummaryList();
                consignedSummaryList.Tag = tabName;
                consignedSummaryList.MdiParent = this;
                consignedSummaryList.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biConsignedSummaryList))
                    ribbonTabs.Add(biConsignedSummaryList, consignedSummaryList);
                else
                {
                    ribbonTabs.Remove(biConsignedSummaryList);
                    ribbonTabs.Add(biConsignedSummaryList, consignedSummaryList);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biConsignedSummaryList]);
                Dangol.CloseSplash();
            }
        }

        private void biConsignedSummaryListResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "consigned Summary List Result";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                usrConsignedSummaryListResult consignedSummaryListResult = new usrConsignedSummaryListResult();
                consignedSummaryListResult.Tag = tabName;
                consignedSummaryListResult.MdiParent = this;
                consignedSummaryListResult.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biConsignedSummaryListResult))
                    ribbonTabs.Add(biConsignedSummaryListResult, consignedSummaryListResult);
                else
                {
                    ribbonTabs.Remove(biConsignedSummaryListResult);
                    ribbonTabs.Add(biConsignedSummaryListResult, consignedSummaryListResult);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biConsignedSummaryListResult]);
                Dangol.CloseSplash();
            }
        }


        private void biRequest_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "request Board";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrRequest request = new usrRequest();
                request.Tag = tabName;
                request.MdiParent = this;
                request.Show();
                if (!ribbonTabs.ContainsKey(biRequest))
                    ribbonTabs.Add(biRequest, request);
                else
                {
                    ribbonTabs.Remove(biRequest);
                    ribbonTabs.Add(biRequest, request);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biRequest]);
            }
        }
        private void biNtbPrice_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "notbook price";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrNtbPrice ntbPrice = new usrNtbPrice();
                ntbPrice.Tag = tabName;
                ntbPrice.MdiParent = this;
                ntbPrice.Show();
                if (!ribbonTabs.ContainsKey(biNtbPrice))
                    ribbonTabs.Add(biNtbPrice, ntbPrice);
                else
                {
                    ribbonTabs.Remove(biNtbPrice);
                    ribbonTabs.Add(biNtbPrice, ntbPrice);
                }
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biNtbPrice]);
            }
        }


        private void biAdjustmentExamine_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "adjustment Examine List";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();
                usrAdjustmentExamineList adjustmentExamineList = new usrAdjustmentExamineList();
                adjustmentExamineList.Tag = tabName;
                adjustmentExamineList.MdiParent = this;
                adjustmentExamineList.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(biAdjustmentExamine))
                    ribbonTabs.Add(biAdjustmentExamine, adjustmentExamineList);
                else
                {
                    ribbonTabs.Remove(biAdjustmentExamine);
                    ribbonTabs.Add(biAdjustmentExamine, adjustmentExamineList);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[biAdjustmentExamine]);
                Dangol.CloseSplash();
            }
        }

        private void bbiAdjustmentComplete_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "정산완료리스트";
            usrAdjustmentCompleteList adjustmentCompleteList = new usrAdjustmentCompleteList();
            setRibbonTabs(adjustmentCompleteList, tabName, bbiAdjustmentComplete);
        }

        private void bbiProduceCheckByBarcode_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "정산대기검색";
            usrAdjustmentExamineListByBarcode adjustmentExamineListByBarcode = new usrAdjustmentExamineListByBarcode();
            setRibbonTabs(adjustmentExamineListByBarcode, tabName, bbiProduceCheckByBarcode);
        }

        private void biTabletCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "tablet check list";
            usrTabletCheckList tabletCheckList = new usrTabletCheckList();
            setRibbonTabs(tabletCheckList, tabName, biTabletCheck);
        }

        private void biInvoiceManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "생산대행 운송장번호 관리";
            usrConsignedInvoiceManagement consignedInvoiceManagement = new usrConsignedInvoiceManagement();
            setRibbonTabs(consignedInvoiceManagement, tabName, biInvoiceManagement);
        }

        private void bbiConsignedReleasePart_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "생산대행 출고부품 관리";
            usrConsignedReleaseManagement consignedReleaseManagement = new usrConsignedReleaseManagement();
            setRibbonTabs(consignedReleaseManagement, tabName, bbiConsignedReleasePart);
        }

        private void biPartPriceList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "부품 가격 매핑 리스트";
            usrPartMappingList partMappingList = new usrPartMappingList();
            setRibbonTabs(partMappingList, tabName, biPartPriceList);
        }
        private void bbiWmPartMappingList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "W사 부품 매핑 리스트";
            usrWMParList WMParList = new usrWMParList();
            setRibbonTabs(WMParList, tabName, bbiWmPartMappingList);
        }

        private void bbiJob_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "작업내용";
            usrJob usrJob = new usrJob();
            setRibbonTabs(usrJob, tabName, bbiJob);
        }


        private void setRibbonTabs(XtraForm contorl, string tabName, BarButtonItem bbitem)
        {
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                Dangol.ShowSplash();

                contorl.Tag = tabName;
                contorl.MdiParent = this;
                contorl.Show();
                Dangol.CloseSplash();
                if (!ribbonTabs.ContainsKey(bbitem))
                    ribbonTabs.Add(bbitem, contorl);
                else
                {
                    ribbonTabs.Remove(bbitem);
                    ribbonTabs.Add(bbitem, contorl);
                }
            }
            else
            {
                Dangol.ShowSplash();
                documentManager.View.ActivateDocument(ribbonTabs[bbitem]);
                Dangol.CloseSplash();
            }
        }












        void OnRunTaskItemClick()
        {
            IOverlaySplashScreenHandle overlayHandle = SplashScreenManager.ShowOverlayForm(this, customPainter: new OverlayWindowCompositePainter(SplashWaitForm._overlayLabel, SplashWaitForm._overlayButton));
            try
            {
                if (ProjectInfo._getComponentInformationYn == 0)
                {
                    Util.GetSystemInfo();
                    ProjectInfo._getComponentInformationYn = 1;

                    //SplashScreenManager.CloseForm(false);
                }
            }
            finally
            {
                SplashScreenManager.CloseOverlayForm(overlayHandle);
            }
        }

        void OnRunTaskItemClickWOMatchingInfo()
        {
            IOverlaySplashScreenHandle overlayHandle = SplashScreenManager.ShowOverlayForm(this, customPainter: new OverlayWindowCompositePainter(SplashWaitForm._overlayLabel, SplashWaitForm._overlayButton));
            try
            {
                if (ProjectInfo._getComponentInformationYn == 0)
                {
                    Util.GetSystemInfoWOMatchingInfo();
                    ProjectInfo._getComponentInformationYn = 1;

                    //SplashScreenManager.CloseForm(false);
                }
            }
            finally
            {
                SplashScreenManager.CloseOverlayForm(overlayHandle);
            }
        }



        private void ribbon_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void biHome_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMain();
        }

        private void showMain()
        {
            string tabName = "Home";
            if (!(tabbedView1.Documents.Any(x => x.Form.Tag.ToString() == tabName) || tabbedView1.FloatDocuments.Any(x => x.Form.Tag.ToString() == tabName)))
            {
                usrHome home = new usrHome();
                home.Tag = tabName;
                home.MdiParent = this;
                home.Show();
                if (!ribbonTabs.ContainsKey(biHome))
                    ribbonTabs.Add(biHome, home);
            }
            else
            {
                documentManager.View.ActivateDocument(ribbonTabs[biHome]);
            }
        }

        private void bbiCapture_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SendToBack();

            DateTime dt = DateTime.Now;
            string fileNm = $"{ProjectInfo._userId}_{dt.ToString("yyyyMMddHHmmss")}";
            Image image = ScreenCapture.Copy(this, "TEST", $"{fileNm}.png");

            using (DlgImgTest digImgTest = new DlgImgTest(image))
            {
                digImgTest.ShowDialog(this);

                File.Delete($"{System.Windows.Forms.Application.StartupPath}\\{fileNm}.png");
            }


        }

        private void bbiCheckStatistics_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "노트북 검수 통계";
            usrNtbCheckStatistics ntbCheckStatistics = new usrNtbCheckStatistics();
            setRibbonTabs(ntbCheckStatistics, tabName, bbiCheckStatistics);
        }

        private void bbiConsignedReturnStatistics_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "생산대행반품통계";
            usrConsignedReturnStatistics consignedReturnStatistics = new usrConsignedReturnStatistics();
            setRibbonTabs(consignedReturnStatistics, tabName, bbiConsignedReturnStatistics);
        }

        private void bbiWarehousingProduce_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "직접 생산";
            usrWarehousingProduce warehousingProduce = new usrWarehousingProduce();
            setRibbonTabs(warehousingProduce, tabName, bbiWarehousingProduce);
        }

        private void bbiWarehousingProductCheckFull_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "product Cheke ManagementFull";
            usrWarehousingProductCheckFull productCheck = new usrWarehousingProductCheckFull();
            setRibbonTabs(productCheck, tabName, bbiWarehousingProductCheckFull);
        }

        private void bbiReleaseCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "releaseList";
            usrReleaseList releaseList = new usrReleaseList();
            setRibbonTabs(releaseList, tabName, bbiSearchBookInfo);
        }

        private void bbiProduceCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "release product check";
            usrReleaseCheck releaseCheck = new usrReleaseCheck();
            setRibbonTabs(releaseCheck, tabName, bbiOrderCartInfo);
        }

        private void bbiReleaseStatistics_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "release statistics";
            usrReleaseStatistics releaseStatistics = new usrReleaseStatistics();
            setRibbonTabs(releaseStatistics, tabName, bbiSearchBookInfoDetail);
        }

        private void bbiReleaseCheckStatistics_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "release check statistics";
            usrReleaseCheckStatistics releaseCheckStatistics = new usrReleaseCheckStatistics();
            setRibbonTabs(releaseCheckStatistics, tabName, bbiOrderRegistBookInfo);
        }

        private void bbiExternalOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "external part order";
            usrReleaseOrder releaseOrder = new usrReleaseOrder();
            setRibbonTabs(releaseOrder, tabName, bbiExternalOrder);
        }

        private void bbiReleaseOrderList_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "external part order list";
            usrReleaseOrderList releaseOrderList = new usrReleaseOrderList();
            setRibbonTabs(releaseOrderList, tabName, bbiReleaseOrderList);
        }

        private void bbiTechnicalRequest_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "technical request";
            usrTechnicalRequest technicalRequest = new usrTechnicalRequest();
            setRibbonTabs(technicalRequest, tabName, bbiTechnicalRequest);
        }

        private void bbiInventoryManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tabName = "inventory management";
            usrExternalInventoryList externalInventoryList = new usrExternalInventoryList();
            setRibbonTabs(externalInventoryList, tabName, bbiInventoryManagement);
        }

       
    }
}