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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int KullaniciId { get; set; }

        private void ListeyiDoldur()
        {
            listView1.Items.Clear();

            KullaniciId = KullaniciVT.GorevliIdGetir(GorevlerVT.KullaniciAdi1.ToString());
 
            List<Gorevler> list = GorevlerVT.GorevlerListesiniGetir2(comboBox1.Text,comboBox2.Text,textBox1.Text,dateTimePicker1.Value,dateTimePicker2.Value,KullaniciId );

            foreach (Gorevler item in list)
            {
                ListViewItem lvitem = new ListViewItem();

                lvitem.Text = item.id.ToString();
                lvitem.SubItems.Add(item.KategoriAdi);
                lvitem.SubItems.Add(item.Tarih.ToString("dd.MM.yyyy"));
                lvitem.SubItems.Add(item.Baslik);
                lvitem.SubItems.Add(item.Aciliyet);
                lvitem.SubItems.Add(item.Durum);
               

                if (item.Tarih < DateTime.Now)
                {
                    lvitem.ForeColor = Color.Red;
                }
                listView1.Items.Add(lvitem);

            }      
        }
      
        public bool Yonetici { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {           
            
            this.Visible = false;
            KullanıcıGirişi giris = new KullanıcıGirişi();
            giris.ShowDialog();
            if (giris.GirisBasarili)
            {
                GorevlerVT.KullaniciAdi1 = giris.KullaniciAdi;
                Güncelleme.KullaniciAdi = giris.KullaniciAdi;
                SifreDegistir.KullaniciAdi = giris.KullaniciAdi;
                SifreDegistir.Sifre = giris.Sifre;


                this.Visible = true;
                timer1.Enabled = true;
                

                Yonetici = KullaniciVT.KullaniciYoneticiKontrolu(giris.KullaniciAdi);
                kategorilerToolStripMenuItem.Visible = Yonetici;
                kullanıcıEkleToolStripMenuItem.Visible=Yonetici;
                kullanıcıEkleToolStripMenuItem1.Visible=Yonetici;
                kullanıcıEkleToolStripMenuItem2.Visible=Yonetici;


                lblKullaniciAdi.Text = "Hoşgeldiniz " + KullaniciVT.KullanıcıAdSoyad(giris.KullaniciAdi, giris.Sifre);

                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

                dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
                dateTimePicker2.Value = DateTime.Now.AddMonths(1);

                int totalWidth = listView1.Width;
                int othertotalWidth = 425;

                columnHeader4.Width = (totalWidth - othertotalWidth);


                List<string> Kategoriler = GorevlerVT.KategorileriGetir();

                comboBox1.Items.Clear();
                comboBox1.Items.Add("TÜMÜ");
                foreach (string item in Kategoriler)
                {
                    comboBox1.Items.Add(item);
                }
                comboBox1.SelectedIndex = 0;


                List<string> Durumlar = GorevlerVT.DurumlarıGetir();

                comboBox2.Items.Clear();
                comboBox2.Items.Add("TÜMÜ");
                foreach (string item in Durumlar)
                {
                    comboBox2.Items.Add(item);
                }
                comboBox2.SelectedIndex = 0;

                ListeyiDoldur();

            }
            else
            {
                Application.Exit();
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            ListeyiDoldur();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            Güncelleme yeni = new Güncelleme();
            yeni.Id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            string EkleyenKisi = KullaniciVT.EkleyenKullaniciGetirme(yeni.Id);
            
            if (Yonetici == false || EkleyenKisi == GorevlerVT.KullaniciAdi1)
            {
                yeni.ShowDialog();
                if (yeni.YenidenYukle == true)
                {
                    ListeyiDoldur();
                }
            }
        }

        private void kategorilerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Kategoriler yeni = new Kategoriler();
            yeni.ShowDialog();
            if (yeni.YenidenYukle)
            {
                List<string> Kategoriler = GorevlerVT.KategorileriGetir();

                comboBox1.Items.Clear();
                comboBox1.Items.Add("TÜMÜ");
                foreach (string item in Kategoriler)
                {
                    comboBox1.Items.Add(item);
                }
                comboBox1.SelectedIndex = 0;
                ListeyiDoldur();
            }

        }


        private void görevEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GorevEkleme yeni = new GorevEkleme();
            yeni.ShowDialog();
            if(yeni.YenidenYukle== true)
            {
                ListeyiDoldur();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            columnHeader3.Width = columnHeader3.Width + 200;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int totalWidth = listView1.Width;
            int othertotalWidth = 425;
            
            columnHeader4.Width = (totalWidth - othertotalWidth);    

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Now.AddMonths(1);
            textBox1.Clear();



            List<string> Kategoriler = GorevlerVT.KategorileriGetir();

            comboBox1.Items.Clear();
            comboBox1.Items.Add("TÜMÜ");
            foreach (string item in Kategoriler)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;


            List<string> Durumlar = GorevlerVT.DurumlarıGetir();

            comboBox2.Items.Clear();
            comboBox2.Items.Add("TÜMÜ");
            foreach (string item in Durumlar)
            {
                comboBox2.Items.Add(item);
            }
            comboBox2.SelectedIndex = 0;


            ListeyiDoldur();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int dakika = Convert.ToInt32(lblDakika.Text);
            int saniye = Convert.ToInt32(lblSaniye.Text);

            
            
            if (saniye == 0 && dakika > 0)
            {
                dakika--;
                lblDakika.Text = "0" + dakika.ToString();
                saniye = 61;
                saniye--;
                lblSaniye.Text = saniye.ToString();
            }
            else if(dakika == 0 && saniye ==0)
            {
                lblDakika.Text = "0" + dakika.ToString();
                lblSaniye.Text = "0" + saniye.ToString();
                timer1.Enabled = false;
                MessageBox.Show("Oturum Süresi Doldu Lütfen Tekrar Oturum Açın.", "Süre Doldu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                Form1 giris = new Form1();
                giris.ShowDialog();
                
            }

            if (saniye > 10)
            {
                saniye--;
                lblSaniye.Text = saniye.ToString();
            }
            if (saniye <= 10)
            {
                saniye--;
                lblSaniye.Text = "0" + saniye.ToString();
            }

        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled=false;
            this.Hide();
            Form1 giris = new Form1(); 
            giris.ShowDialog();
            
        }

 

        private void kullanıcıEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullaniciOnay giris = new KullaniciOnay();
            giris.ShowDialog();

        }

        private void kullanıcıEkleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            KullaniciGuncelle giris = new KullaniciGuncelle();
            giris.ShowDialog();
        }

        private void kullanıcıEkleToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            KullaniciEkle giris = new KullaniciEkle();
            giris.ShowDialog();
        }

        private void şifreDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SifreDegistir giris = new SifreDegistir();
            giris.ShowDialog();
        }
    }
}
