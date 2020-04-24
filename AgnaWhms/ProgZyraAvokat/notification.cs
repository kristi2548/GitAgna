using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgZyraAvokat
{
    public partial class notification : Form
    {
        public notification()
        {
            InitializeComponent();
            ApplicationLookAndFeel.UseTheme(this, 12);
            this.ControlBox = true;
            this.MinimizeBox = true;
            this.MaximizeBox = true;

            this.Size = new System.Drawing.Size(600, 200);
            this.WindowState = FormWindowState.Normal;
        }

        private void notification_Move(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                    notifyIcon1.ShowBalloonTip(1000, "Kujtese e rendesishme", "Kujtese e rendesishme nga Zyra App", ToolTipIcon.Info);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("notification_Move err " + ex.Message);
            }
        }
        public void setText(string alertText)
        {
            //this.Text = "Kujtese e rendesishme";
            this.label1.Text  = alertText;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
    }
}
