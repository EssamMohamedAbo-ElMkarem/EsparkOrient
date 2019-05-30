using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ESpark
{
    public partial class Mail : Form
    {
        public Mail()
        {
            InitializeComponent();
        }

        FilesLoader fl = new FilesLoader();

        private void Mail_Load(object sender, EventArgs e)
        {
            menuStrip1.ForeColor = Color.White;
            passwrd.BringToFront();
            username.BringToFront();
            to.BringToFront();
        }

        private void Mail_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.Delete(@"mail_data\username.edxf");
            File.Delete(@"mail_data\passwrd.edxf");
            File.Delete(@"mail_data\to_mail.edxf");
            File.Delete(@"mail_data\message.edxf");
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Powered by EssamMohamed");
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://esparkorient.wixsite.com/supertech");
        }

        

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.ForeColor = cd.Color;
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
                message_text.Font = font.Font;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(message_text.Text);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            message_text.Text = Clipboard.GetText();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            message_text.Text = string.Empty;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mail m = new Mail();
            m.Show();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EsparkMainScreen eo = new EsparkMainScreen();
            eo.Show();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void send_q_Click(object sender, EventArgs e)
        {


            string user = username.Text;
            string pass = passwrd.Text;
            string To = to.Text;
            string Message = message_text.Text;
            File.WriteAllText(@"mail_data\username.edxf", string.Empty);
            File.WriteAllText(@"mail_data\passwrd.edxf", string.Empty);
            File.WriteAllText(@"mail_data\to_mail.edxf", string.Empty);
            File.WriteAllText(@"mail_data\message.edxf", string.Empty);
            File.WriteAllText(@"mail_data\response.dx", string.Empty);




            if (user != "" && pass != "" && Message != "")
            {
                fl.set_text(user, @"mail_data\username.edxf");
                fl.set_text(pass, @"mail_data\passwrd.edxf");
                fl.set_text(To, @"mail_data\to_mail.edxf");
                fl.set_text(Message, @"mail_data\message.edxf");
                process_exec ps = new process_exec(@"Python\python.exe", @"scripts\SendMail.py");
                ps.process();
            }
            else
            {
                MessageBox.Show("Please, fill all the fields", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            string result = fl.get_text(@"mail_data\response.dx");
            if (result == "success")
            {
                MessageBox.Show("Message sent successfully", "Success (:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (result == "error")
            {
                MessageBox.Show("Something went wrong, Please try again later.", "Sorry x_x", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
