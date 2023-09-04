using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ProjeGörev
{
    public partial class KullanıcıGirişi : Form
    {
        public KullanıcıGirişi()
        {
            InitializeComponent();
        }

        public bool GirisBasarili { get; set; }

        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
             KullaniciAdi = textBox1.Text;
             Sifre = textBox2.Text;
            GorevlerVT.KullaniciAdi1 = textBox1.Text;

            /*
                        bool GirisDeneme = KullaniciVT.KullanıcıGirisKontrolu2(KullaniciAdi, Sifre);
                        if (GirisDeneme)
                        {
                            GirisBasarili = false;
                            MessageBox.Show("Kullanıcı Bloke Edilmiş!!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else
                        {

                            bool GirisDeneme2 = KullaniciVT.KullanıcıGirisKontrolu3(KullaniciAdi, Sifre);
                            if (GirisDeneme2)
                            {
                                GirisBasarili = false;
                                MessageBox.Show("Kullanıcı Yönetici Onayı Bekliyor!!", "Onay Bekleniyor", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                            else
                            {
                                bool GirisDeneme3 = KullaniciVT.KullanıcıGirisKontrolu(KullaniciAdi, Sifre);
                                if (GirisDeneme3)
                                {

                                    GirisBasarili = true;
                                    this.Close();

                                }
                                else if (GirisDeneme3 == false)
                                {
                                    MessageBox.Show("Kullanici adi veya Sifre yanlis.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                } 
                            }

                        }
            */

            string girisdeneme = KullaniciVT.KullanıcıGirisKontrolu4(KullaniciAdi, Sifre);
            if(girisdeneme == "Giris Basarili")
            {
                GirisBasarili = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(girisdeneme, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void KullanıcıGirişi_Load(object sender, EventArgs e)
        {
            GirisBasarili = false;
            KullaniciAdi = "";
            Sifre = "";
           
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SifremiUnuttum giris = new SifremiUnuttum();
            giris.ShowDialog();


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KayıtOl giris = new KayıtOl();
            giris.ShowDialog();
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }
    }
}
