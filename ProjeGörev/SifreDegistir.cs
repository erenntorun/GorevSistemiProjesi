using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjeGörev
{
    public partial class SifreDegistir : Form
    {
        public SifreDegistir()
        {
            InitializeComponent();
        }


        public static string KullaniciAdi { get; set; }
        
        public static string Sifre { get; set; }

        private string YeniSifre { get; set; }

        private string EskiSifre { get; set; }

        private void SifreDegistir_Load(object sender, EventArgs e)
        {





        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox2.Text == textBox3.Text)
            {
                YeniSifre = textBox2.Text;
                EskiSifre = textBox1.Text;
                if(EskiSifre == Sifre)
                {
                   bool islemyapildi = KullaniciVT.SifreDegistir(YeniSifre, EskiSifre, KullaniciAdi);
                    if(islemyapildi)
                    {
                        MessageBox.Show("Şifreniz Başarıyla Değiştirildi Giriş Ekranına Yönlendiriliyorsunuz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);                                         
                        this.Hide();
                        Form1 giris = new Form1();
                        giris.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("Eski Şifreniz Yanlış Tekrar Deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            else
            {
                MessageBox.Show("Şifreler Uyuşmuyor Tekrar Deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
