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

namespace ProjeGörev
{
    public partial class Kategoriler : Form
    {
        public Kategoriler()
        {
            InitializeComponent();
        }

        private void ListeyiDoldur1()
        {
            listView1.Items.Clear();
            List<Gorevler> list = GorevlerVT.KategorilerListesiniGetir();

            foreach (Gorevler item in list)
            {
                ListViewItem lvitem = new ListViewItem();

                lvitem.Text = item.id.ToString();
                lvitem.SubItems.Add(item.KategoriAdi);
                lvitem.SubItems.Add(item.Aktif.ToString());

                listView1.Items.Add(lvitem);
            }
        }

        private void Kategoriler_Load(object sender, EventArgs e)
        {
            
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                ListeyiDoldur1();


        }

        public bool YenidenYukle { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                Gorevler gorev = new Gorevler
                {
                    id = 0,
                    KategoriAdi = textBox1.Text,
                    Aktif = checkBox1.Checked == true ? 1 : 0
                };

                bool İslemYapildi = GorevlerVT.KategoriEkle(gorev);
                if (İslemYapildi)
                {
                    YenidenYukle = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        int id;
        int idtut;

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;

            if (Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text) == 1)
            {
                button3.Enabled = true;
                button4.Enabled = false;
            }
            if (Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text) == 0)
            {
                button3.Enabled = false;
                button4.Enabled = true;
            }


            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

            idtut = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            checkBox1.Checked = Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text) == 1 ? true : false;


        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            button1.Enabled = true;

            Gorevler kategori = new Gorevler
            {
                id = id,
                KategoriAdi = textBox1.Text,
                Aktif = checkBox1.Checked == true ? 1 : 0
            };

            bool islemyapildi = GorevlerVT.KategoriGuncelle(kategori);
            if (islemyapildi)
            {
                YenidenYukle = true;
                ListeyiDoldur1();
            }

            textBox1.Clear();
            checkBox1.Checked = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Bu Kategoriyi Pasifleştirmek İstiyor Musunuz?", "Uyarı!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            button1.Enabled = true;


            if(sonuc == DialogResult.Yes)
            {
                bool islemyapildi = GorevlerVT.KategoriPasifleştir(Convert.ToInt32(idtut));
                if (islemyapildi)
                {
                    YenidenYukle = true;
                    this.Close();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Bu Kategoriyi Aktifleştirmek İstiyor Musunuz?", "Uyarı!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            button1.Enabled = true;

            if (sonuc == DialogResult.Yes)
            {
                bool islemyapildi = GorevlerVT.KategoriAktifleştir(Convert.ToInt32(idtut));
                if (islemyapildi)
                {
                    YenidenYukle = true;
                    this.Close();
                }
            }
        }
    }
}
