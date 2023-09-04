using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ProjeGörev
{
    public partial class KullaniciEkle : Form
    {
        public KullaniciEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool islem = false;

            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty)
            {
                Gorevler gorev = new Gorevler
                {
                    KullaniciId = 0,
                    AdSoyad = textBox1.Text,
                    KullaniciAdi = textBox2.Text,
                    Sifre = textBox3.Text == textBox4.Text ? textBox3.Text : string.Empty,
                    Mail = textBox4.Text,
                    Yonetici = checkBox1.Checked == true ? 1 : 0,                   
                };

                islem = KullaniciVT.KullaniciEkleYonetici(gorev);
                if (islem)
                {
                    MessageBox.Show("Kullanıcı Ekleme Başarılı", "Kullanıcı Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ve Şifre Alanları Boş Kalamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
