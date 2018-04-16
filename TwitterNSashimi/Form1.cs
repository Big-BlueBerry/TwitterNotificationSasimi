using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Collections;
using System.Web;


namespace TwitterNSashimi
{
    public partial class Form1 : Form
    {
        WebBrowser webBrowser1;
        int count, oldCount;
        public Form1()
        {
            InitializeComponent();

            this.Width = Height = 0;
            timer1.Interval = 5000;
            timer1.Start();

            webBrowser1 = new WebBrowser();
            webBrowser1.Parent = this;
            webBrowser1.Dock = DockStyle.Fill;
            this.webBrowser1.Navigate("twitter.com/");

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                Hide();
            }));
        }

        private void Check()
        {
            HtmlElementCollection collection = default(HtmlElementCollection);
            foreach (HtmlElement c in webBrowser1.Document.GetElementsByTagName("span"))
            {
                if (c.GetAttribute("className") == "count-inner")
                {
                    count = Convert.ToInt32(c.InnerText);

                    //중복으로 알림뜨는 걸 막아야 한당 
                    //헉 오늘따라 코딩 집중 겁나 잘되고 평소보다 더 재밌다  흑흑 개발 수행하기싫어
                    if (count > oldCount)
                    {
                        Notify();
                        oldCount = count;
                        break;
                    }
                    break;
                }
            }
        }

        private void Notify()
        {
            notifyIcon1.BalloonTipText = $"새로운 {count}개의 알림이 왔습니다!";
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void 종료QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Check();
        }
    }
}
