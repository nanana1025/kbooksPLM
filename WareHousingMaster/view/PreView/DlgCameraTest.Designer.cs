namespace WareHousingMaster.view.PreView
{
    partial class DlgCameraTest
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxIpl1 = new OpenCvSharp.UserInterface.PictureBoxIpl();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).BeginInit();
            this.SuspendLayout();
            // 
            // rirgPageSize
            // 
 
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 33;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBoxIpl1
            // 
            this.pictureBoxIpl1.Location = new System.Drawing.Point(1, 1);
            this.pictureBoxIpl1.Name = "pictureBoxIpl1";
            this.pictureBoxIpl1.Size = new System.Drawing.Size(640, 480);
            this.pictureBoxIpl1.TabIndex = 0;
            this.pictureBoxIpl1.TabStop = false;
            // 
            // DlgCameraTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(640, 481);
            this.Controls.Add(this.pictureBoxIpl1);
            this.Name = "DlgCameraTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "웹캠 테스트";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgCameraTest_FormClosing);
            this.Load += new System.EventHandler(this.DlgCameraTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
       
        private System.Windows.Forms.Timer timer1;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBoxIpl1;
    }
}