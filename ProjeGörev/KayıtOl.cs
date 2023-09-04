using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ProjeGörev
{
    public partial class KayıtOl : Form
    {
        public KayıtOl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex rx = new Regex("^\\S+@\\S+\\.\\S+$");
            bool result = rx.IsMatch(textBox5.Text);
         

            bool islem = false;

            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty)
            {
                if (result)
                {
                    Gorevler gorev = new Gorevler
                    {
                        KullaniciId = 0,
                        AdSoyad = textBox1.Text,
                        KullaniciAdi = textBox2.Text,
                        Sifre = textBox3.Text == textBox4.Text ? textBox3.Text : string.Empty,
                        Mail = textBox4.Text
                    };

                    islem = KullaniciVT.KullaniciEkle(gorev);
                    if (islem)
                    {
                        MessageBox.Show("Kullanıcı Kaydınız Başarıyla Oluşturuldu Onay Bekliyorsunuz!", "Onay Bekleniyor!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Email bilgisi istenilen kriterde değil!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Tüm Alanların Doldurulması Zorunludur!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
    }
}
