using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CpanelVerification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string EBSURL = "https://verify.cpanel.net/app/verify?ip=";
        string EBShtml;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string address = Dns.GetHostAddresses(textBox1.Text)[0].ToString();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EBSURL + address);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    EBShtml = reader.ReadToEnd();
                }
                if (EBShtml.IndexOf("Results: Not licensed") != -1)
                {
                    label2.Text = "Lisans Kayıtlı Değil..";
                }
                else
                {
                    webBrowser1.DocumentText = EBShtml;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EBS Securty");
            }
          
        }
    }
}
