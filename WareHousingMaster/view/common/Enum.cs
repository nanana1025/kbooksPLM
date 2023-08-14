using System;
using System.ComponentModel;



namespace WareHousingMaster.view.common
{
    static class Enum
    {
        public enum SearchType : Int32
        {
            [Description("판매점")]
            SELLER = 1,
            [Description("출판사")]
            PUBLISHER = 2,
        }

        public enum SearchListType : Int32
        {
            [Description("일반검색")]
            SEARCH = 1,
            [Description("매입처별검색")]
            PURCHASE = 2,
            [Description("출판사별검색")]
            PUBLISHER = 3,
        }















































        public enum EnumView : Int32
        {
            [Description("HOME")]
            Home = 0,
            [Description("검색")]
            Search = 8,
            [Description("정산")]
            Adjustment = 9,
            [Description("검수")]
            Check = 10,
            [Description("생산")]
            produce = 4,
        }

        public enum EnumTableAdjustment : Int32
        {
            [Description("검수리스트")]
            ExamList = 2,
            CompleteList = 3,
            ExamListByBarcode = 4,

        }
        public enum EnumTableCheck : Int32
        {
            [Description("체크리스트")]
            CheckList = 2,
            Tablet = 9,
        }


        public enum EnumProduce : Int32
        {
            [Description("생산")]
            ProductCheckList = 2,
        }


        public enum EnumAttachedFile : Int32
        {
            [Description("입고")]
            WAREHOUSING = 1,
            [Description("생산대행")]
            CONSIGNED = 2,
            [Description("스페어")]
            SPARE = 3,
            [Description("작업")]
            TODO = 4,
            [Description("technicalRequest")]
            TECHREQUEST = 5,
        }












        public enum EnumAuthView : Int64
        {
            [Description("HOME")]
            DashBoard = 0,
          
            [Description("입고")]
            Contract = 1000,
            [Description("일반입고")]
            ContractMain = 1001,
            [Description("표준프로젝트 관리")]
            ProjectStandard = 1002,
            [Description("제조원가관리")]
            ProductionCostsMngt = 1003,
            [Description("수주통계")]
            ContractStatistics = 1004,
            [Description("인건비 기준정보")]
            LaborCost = 1005,

            [Description("프로젝트")]
            Project = 1100,
            [Description("프로젝트 정보 관리")]
            ProjectMain = 1101,

            [Description("협업")]
            Work = 2000,
            [Description("일정관리")]
            Schedule = 2001,
            [Description("업무일지 등록")]
            DailyReport = 2002,
            [Description("업무일지 조회")]
            ReportSummary = 2003,
            [Description("인력투입현황")]
            ProjectWorkSummary = 2004,
            //[Description("연차사용현황")]
            //UsedOff = 2005,
            [Description("업무요청")]
            Job = 2006,
            //[Description("주월간보고")]
            //WeekMonthlyReport = 2007,
            [Description("메일전송이력")]
            MailLog = 2008,

            [Description("설계")]
            Item = 3000,
            [Description("제품정보")]
            Product = 3001,
            [Description("BOM 등록")]
            BOM = 3002,
            [Description("도면 관리")]
            Drawing = 3003,
            [Description("분류 관리")]
            DrawingCategory = 30031,
            [Description("권한 관리")]
            DrawingAuth = 30032,
            [Description("도면 배포")]
            DeployManager = 3004,
            [Description("설계문서 관리")]
            Document = 3005,
            [Description("분류 관리")]
            DocumentCategory = 30051,
            [Description("권한 관리")]
            DocumentAuth = 30052,
            //[Description("원도 관리")]
            //DrawingSrc = 3007,
            //[Description("분류 관리")]
            //DrawingSrcCategory = 30071,
            //[Description("권한 관리")]
            //DrawingSrcAuth = 30072,
            //[Description("라이브러리 관리")]
            //Library = 3008,
            //[Description("분류 관리")]
            //LibraryCategory = 30081,
            //[Description("권한 관리")]
            //LibraryAuth = 30082,
            [Description("문서통합검색")]
            DocSearch = 3009,

            [Description("설계 변경")]
            Change = 3100,
            [Description("설계변경요청")]
            Ecr = 3101,
            [Description("설계변경통보")]
            Ecn = 3102,

            [Description("프로젝트 자재")]
            Material = 4000,
            [Description("자재마스터")]
            Material1 = 4001,
            [Description("단가견적요청목록")]
            Material2 = 4002,
            [Description("단가견적요청작성")]
            Material3 = 4003,
            [Description("자재발주목록")]
            Material4 = 4004,
            [Description("자재발주작성")]
            Material5 = 4005,
            [Description("자재입고")]
            Material6 = 4006,
            [Description("자재입고현황")]
            Material7 = 4007,
            [Description("자재불출")]
            Material8 = 4008,
            [Description("자재불출목록")]
            Material9 = 4009,
            //[Description("지급자재반납")]
            //Material10 = 4010,
            [Description("재고현황")]
            Material11 = 4011,
            [Description("품목별재고현황")]
            Material12 = 4012,
            [Description("창고관리")]
            Material13 = 4013,
            [Description("프로젝트별 품목현황")]
            Material14 = 4014,

            //[Description("하네스 견적")]
            //Harness = 4200,
            //[Description("하네스 견적 관리")]
            //HarnessMaster = 4201,

            [Description("품질")]
            Quality = 5000,
            [Description("품질문서 관리")]
            QualityDoc = 5001,
            [Description("분류 관리")]
            QualityDocCategory = 50011,
            [Description("권한 관리")]
            QualityDocAuth = 50012,
            //[Description("검사성적서 관리")]
            //QualityTestResult = 5002,
            //[Description("부적합 관리")]
            //NonconformanceReport = 5003,
            //[Description("부적합 현황")]
            //NonconformanceStatus = 5004,
            //[Description("품질이력카드 등록")]
            //QualityHistory = 5005,
            //[Description("고객불만 접수")]
            //CustomerComplaint = 5006,
            //[Description("주요지표관리")]
            //IndexManagement = 5007,
            //[Description("고객불만 접수 통계")]
            //CustomerComplaintStatistics = 5008,
            //[Description("SPC")]
            //SPC = 5009,

            [Description("수입검사지정")]
            ImportSelect = 5101,
            [Description("수입검사수행")]
            ImportProcess = 5102,
            [Description("부적합품목")]
            ImportNonConformance = 5103,
            [Description("수입검사현황")]
            ImportIndexManagement = 5104,

            [Description("결재")]
            Approval = 6000,
            //[Description("결재 상신")]
            //Approval1 = 6001,
            [Description("결재 현황")]
            SignOff = 6002,

            [Description("현장")]
            Field = 8000,
            [Description("제조현장 관리")]
            ProductField = 8001,

            [Description("도움말")]
            Help = 9000,
            [Description("원격지원")]
            Support = 9001,

            [Description("시스템")]
            Manage = 9900,
            [Description("업체 관리")]
            Company = 9901,
            //[Description("업체 담당자 관리")]
            //PIC = 9902,
            [Description("사용자 관리")]
            User = 9903,
            [Description("권한 관리")]
            Authrity = 9904,
            //[Description("연차 관리")]
            //SetOff = 9905,
            [Description("휴일 관리")]
            Holiday = 9906,
            [Description("코드 관리")]
            Code = 9907,
            [Description("분류 관리")]
            Category = 9908,
            [Description("번호 관리")]
            NoExpress = 9909,
            [Description("워터마크 관리")]
            Watermark = 9910,
            [Description("공지 관리")]
            Notice = 9911,
            [Description("이력 조회")]
            EventLog = 9912,


            [Description("발주 예측")]
            InventoryPrediction = 9999,

        }
    }
}
