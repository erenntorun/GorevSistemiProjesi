using System;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjeGörev
{
    public partial class SifremiUnuttum : Form
    {
        public SifremiUnuttum()
        {
            InitializeComponent();
        }

        public string Email { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Email = textBox1.Text.ToString();

            bool EmailDogrumu = KullaniciVT.KullanıcıEmailKontrolu(Email);
            if (EmailDogrumu)
            {

                string YeniSifre = Guid.NewGuid().ToString().ToUpper().Substring(0,6);
                
                KullaniciVT.KullaniciGuncelle(YeniSifre, Email);
                // KullaniciVT.MailGonder(Email, "Şifre Sıfırlama", "Şifreniz Başarıyla Güncellendi Yeni Şifreniz:" + YeniSifre);

                MessageBox.Show("Yeni Şifreniz: "+ YeniSifre);
                this.Hide();
            }
        }



    }
}
