using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WareHousingMaster.view.common;

namespace ScreenCopy
{
    // <summary>
    /// 미디어 상태
    /// </summary>
    public enum MediaStatus
    {
        /// <summary>
        /// NONE
        /// </summary>
        NONE,

        /// <summary>
        /// STOPPED
        /// </summary>
        STOPPED,

        /// <summary>
        /// PAUSED
        /// </summary>
        PAUSED,

        /// <summary>
        /// RUNNING
        /// </summary>
        RUNNING
    };
}