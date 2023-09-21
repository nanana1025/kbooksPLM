using System;
using System.Runtime.InteropServices;

namespace WareHousingMaster.view.common
{
    static class Language
    {
        // Windows API 함수를 가져옵니다.
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        // Windows API 상수를 정의합니다.
        private const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
        private const int INPUTLANGCHANGE_SYSCHARSET = 0x0001;
        private const int INPUTLANGCHANGE_FORWARD = 0x0002;
        private const int INPUTLANGCHANGE_BACKWARD = 0x0004;

        //public static void Main()
        //{
        //    //// 한글로 전환
        //    //SwitchInputLanguage(INPUTLANGCHANGE_FORWARD);

        //    //// 몇 초 후에 다시 영어로 전환
        //    //System.Threading.Thread.Sleep(2000); // 2초 대기
        //    //SwitchInputLanguage(INPUTLANGCHANGE_BACKWARD);

        //    //Console.WriteLine("Input language switched.");
        //}

        public static void SwitchToKorean()
        {
            SwitchInputLanguage(INPUTLANGCHANGE_FORWARD);
        }

        public static void SwitchToEnglish()
        {
            SwitchInputLanguage(INPUTLANGCHANGE_BACKWARD);
        }
        

        private static void SwitchInputLanguage(int changeType)
        {
            int hwnd = 0; // 현재 활성 창 핸들

            // 현재 활성 창의 핸들을 가져옵니다.
            hwnd = (int)GetForegroundWindow();

            // 입력 언어 변경 요청을 보냅니다.
            SendMessage(hwnd, WM_INPUTLANGCHANGEREQUEST, INPUTLANGCHANGE_SYSCHARSET, changeType);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}
