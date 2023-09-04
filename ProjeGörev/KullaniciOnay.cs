using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ProjeGörev
{
    public partial class KullaniciOnay : Form
    {
        public KullaniciOnay()
        {
            InitializeComponent();
        }

        private void ListeyiDoldur()
        {
            listView1.Items.Clear();

            List<Gorevler> list = KullaniciVT.KullanicilarListesiniGetir();

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

              
                listView1.Items.Add(lvitem);
            }
        }




        private void KullaniciOnay_Load(object sender, EventArgs e)
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KullaniciVT.KullaniciOnayla(idtut);
            this.Close();
        }
    }
}
