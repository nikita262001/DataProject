using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            NI.BalloonTipText = "Текст";
            NI.BalloonTipTitle = "Заголовок";
            NI.BalloonTipIcon = ToolTipIcon.Info;
            NI.Icon = this.Icon;
            NI.Visible = true;
            NI.ShowBalloonTip(10000);

            Task.Run(async () =>
            {
                await Task.Delay(1000);
                //NI_BalloonTipClosed();
            });
        }
        private NotifyIcon NI = new NotifyIcon();

        private void NI_BalloonTipClosed()
        {
            NI.Visible = false;
        }
    }
}
