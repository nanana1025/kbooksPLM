using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WareHousingMaster.view.common;
using Newtonsoft.Json.Linq;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using System.Drawing.Printing;
using System.Management;
using SM = System.Media;
using System.Windows.Controls;
using System.Windows.Media;
using ImportSound;
using System.Runtime.InteropServices;

namespace WareHousingMaster.view.PreView
{
    public partial class DlgSoundTest : DevExpress.XtraEditors.XtraForm
    {
        MediaPlayer sound;
        string soundTestProgramPath = System.Windows.Forms.Application.StartupPath + @"\sample.mp3";

        public DlgSoundTest()
        {
            InitializeComponent();
            sound = new MediaPlayer();
        }

        [DllImport("winmm.dll")]
        private static extern int waveOutGetVolume(IntPtr waveOutHandle, out uint volume);
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr waveOutHandle, uint volume);

        private void DlgCameraTest_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ProjectInfo.ProjectIcon;

                tbarVolume.Value = GetSoundVolume();



            }
            catch(Exception ex)
            {
                //Console.Write(ex.Message);
           
            }
            finally
            {
               
            }
        }

        private static int GetSoundVolume()
        {
            uint value;
            waveOutGetVolume(IntPtr.Zero, out value);
            ushort leftChannelValue = (ushort)(value & 0x0000ffff);
            ushort rightChannelValue = (ushort)(value >> 16);
            int volume = (int)(leftChannelValue * 100.0 / ushort.MaxValue + 0.5);
            return volume;
        }

        private static void SetSoundVolume(ushort volume)
        {
            try
            {
                int value = (int)((double)volume * ushort.MaxValue / 100);
                uint leftChannelValue = ((uint)value & 0x0000ffff);
                uint rightChannelValue = ((uint)value << 16);
                waveOutSetVolume(IntPtr.Zero, leftChannelValue | rightChannelValue);

            }
            catch

            {

            }
        }


        private int TrackbarToVolume(int Min, int Max, double Value)
        {
            double iRange = Max - Min;
            double iTarget = Value - Min;
            return (int)(iTarget / iRange * 100);
        }

        private int VolumeToTrackbar(int Min, int Max, int Per)
        {
            double iRange = Max - Min;
            double iTarget = iRange * Per / 100;
            return (int)(iTarget + Min);
        }

        private void sbLeft_Click(object sender, EventArgs e)
        {
            sound.Balance = -1.0;
            //SM.SystemSounds.Hand.Play();
            sound.Pause();
            sound.Open(new Uri(soundTestProgramPath));
            //sound.MediaOpened += loadedMusic;
            sound.Play();
        }

        private void sbRight_Click(object sender, EventArgs e)
        {
            sound.Balance = 1.0;
            sound.Pause();
            //SM.SystemSounds.Hand.Play();
            sound.Open(new Uri(soundTestProgramPath));
            //sound.MediaOpened += loadedMusic;
            sound.Play();
        }

        private void DlgSoundTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            sound.Balance = 0;
            sound.Pause();
            
        }

        private void sbPause_Click(object sender, EventArgs e)
        {
            sound.Balance = 0;
            sound.Pause();
        }

        private void tbarVolume_EditValueChanged(object sender, EventArgs e)
        {
            int vol = ConvertUtil.ToInt32(tbarVolume.Value.ToString());

            //int iVolumn = VolumeToTrackbar(-1200, 0, vol);
                //sound.Volume = vol;
            //SoundUtils.SetVolumePercent(vol);
            SetSoundVolume((ushort)vol);



        }
    }
}