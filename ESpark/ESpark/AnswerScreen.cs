using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace ESpark
{
    public partial class AnswerScreen : Form
    {
        public AnswerScreen()
        {
            InitializeComponent();
        }

        EsparkMainScreen eo = new EsparkMainScreen();
        static ThreadStart th_st = new ThreadStart(process);
        Thread speak = new Thread(th_st);


        private void AnswerScreen_Load(object sender, EventArgs e)
        {
            menuStrip1.ForeColor = Color.White;
            FilesLoader fl = new FilesLoader();
            byte[] text = Encoding.UTF8.GetBytes(fl.get_text(@"inner_temp\response.dx"));
            answer_text.Text = Encoding.UTF8.GetString(text);
            if (answer_text.Text == string.Empty)
            {
                MessageBox.Show("Something went wrong(Enter you question in another way or check you internet connection and try again later)", "Sorry x_x!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        public static void process()
        {
            process_exec ps = new process_exec(@"Python\python.exe", @"scripts\speak.py");
            ps.process();
        }

        public void speakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!speak.IsAlive)
            {
                speak = new Thread(process);
                speak.Start();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            /*
            process_exec ps = new process_exec(@"CMD.exe", @"/C pkill -f C:\Users\elhussein\source\repos\ESpark\ESpark\bin\Debug\scripts\speak.py");
            ps.process();
            */
        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                answer_text.ForeColor = cd.Color;
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

        private void forntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                answer_text.Font = font.Font;
            }
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http:\\supertech8.wixsite.com\supertech");
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I've been written by EssamMohamed");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eo.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eo.open_ans();
            this.Close();
            AnswerScreen ans = new AnswerScreen();
            ans.Show();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eo.save_ans();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(answer_text.Text);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            answer_text.Text = Clipboard.GetText();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }
    }
}
