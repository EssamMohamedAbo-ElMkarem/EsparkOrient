using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESpark
{
    public partial class EsparkMainScreen : Form
    {
        public EsparkMainScreen()
        {
            InitializeComponent();
        }
        FilesLoader fl = new FilesLoader();
        public void save_ans()
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.FileName = "ans.dx";
            sv.InitialDirectory = @"\saved";
            sv.Filter = "DexESparkOrient files (*.dx)|*.dx";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                string ans_name = sv.FileName;
                Console.WriteLine(sv.FileName);
                StreamWriter w1 = new StreamWriter(ans_name);
                w1.Write(fl.get_text(@"inner_temp\response.dx"));
                w1.Close();
            }
        }

        public void open_ans()
        {
            OpenFileDialog od = new OpenFileDialog();
            od.InitialDirectory = @"\saved";
            od.Filter = "DexESparkOrient files (*.dx)|*.dx";
            if (od.ShowDialog() == DialogResult.OK)
            {
                fl.set_text(fl.get_text(od.FileName), @"inner_temp\response.dx");
            }
        }

        private AnswerScreen on_click()
        {
            File.WriteAllText(@"inner_temp\question.dx", string.Empty);
            File.WriteAllText(@"inner_temp\response.dx", string.Empty);
            fl.set_text(question.Text.ToString(), @"inner_temp\question.dx");

            process_exec ps = new process_exec(@"Python\python.exe", @"scripts\PySpark.py");
            ps.process();

            return new AnswerScreen();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EsparkMainScreen eo = new EsparkMainScreen();
            eo.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(question.Text);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            question.Text = Clipboard.GetText();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EsparkMainScreen_Load(object sender, EventArgs e)
        {
            menuStrip1.ForeColor = Color.White;
            this.MaximizeBox = false;

            if (File.Exists(@"inner_temp\question.dx") == false && File.Exists(@"inner_temp\response.dx") == false)
            {
                File.Create(@"inner_temp\question.dx").Close();
                File.Create(@"inner_temp\response.dx").Close();
            }
        }

        private async void send_q_Click(object sender, EventArgs e)
        {
            Task<AnswerScreen> task = new Task<AnswerScreen>(on_click);
            task.Start();
            AnswerScreen sc = await task;
            sc.Show();
            
        }
       
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Powered by EssamMohamed");
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://esparkorient.wixsite.com/supertech");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_ans();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open_ans();
            AnswerScreen ans = new AnswerScreen();
            ans.Show();
        }



        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.ForeColor = cd.Color;
            }
        }

        private void forntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                question.Font = font.Font;
            }
        }

        private void menuColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                menuStrip1.BackColor = cd.Color;
            }
        }

        private void send_mail_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Sign in to your Google Admin console.
Sign in using an administrator account

 From the Admin console Home page, go to Security > and then > Basic settings.
To see Security on the Home page, you might have to click More controls at the bottom.

Under Less secure apps, select Go to settings for less secure apps.

In the subwindow, select the Disable access to less secure apps for all users radio button.
Once you've set Disable access to less secure apps for all users to on, affected users within the selected group or Organizational Unit will not be able to toggle access for less secure apps on or off themselves. You will have to set the setting back to Allow users to manage their access to less secure apps to allow them to toggle access for less secure apps on or off themselves.  ", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Mail m = new Mail();
            m.Show();
        }
    }
}