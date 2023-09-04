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
    public partial class KullaniciGuncelle : Form
    {
        public KullaniciGuncelle()
        {
            InitializeComponent();
        }

        private void ListeyiDoldur()
        {
            listView1.Items.Clear();

            List<Gorevler> list = KullaniciVT.TumKullanicilarListesiniGetir();

            foreach (Gorevler item in list)
            {
                ListViewItem lvitem = new ListViewItem();

                lvitem.Text = item.KullaniciId.ToString();
                lvitem.SubItems.Add(item.KullaniciAdi);
                lvitem.SubItems.Add(item.Sifre);
                lvitem.SubItems.Add(item.Yonetici.ToString());
                lvitem.SubItems.Add(item.Mail);
                lvitem.SubItems.Add(item.AdSoyad);
                lvitem.SubItems.Add(item.Onay.ToString());
                lvitem.SubItems.Add(item.KAktif.ToString());

                listView1.Items.Add(lvitem);
            }
        }

        public bool YenidenYukle { get; set; }

        private void KullaniciGuncelle_Load(object sender, EventArgs e)
        {
            
            ListeyiDoldur();
        }

        int id;
        int idtut;

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

            idtut = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[4].Text;
            checkBox1.Checked = Convert.ToInt32(listView1.SelectedItems[0].SubItems[3].Text) == 1 ? true : false;
            checkBox2.Checked = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text) == 1 ? true : false;
            checkBox3.Checked = Convert.ToInt32(listView1.SelectedItems[0].SubItems[7].Text) == 1 ? true : false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool islem = false;

            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty)
            {
                Gorevler gorev = new Gorevler
                {
                    KullaniciId = idtut,
                    KullaniciAdi = textBox1.Text,
                    Sifre = textBox2.Text,
                    AdSoyad = textBox3.Text,
                    Mail = textBox4.Text,
                    Yonetici = checkBox1.Checked == true ? 1 : 0,
                    Onay = checkBox2.Checked == true ? 1 : 0,
                    KAktif = checkBox3.Checked == true ? 1 : 0
                };

                islem = KullaniciVT.KullaniciGuncelle(gorev);
                if (islem)
                {
                    MessageBox.Show("Güncelleme Başarıyla Tamamlandı!", "Güncelleme Durumu!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Eksik Kullanıcı Bilgisi!!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }
        
    }
}
