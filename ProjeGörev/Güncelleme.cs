using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjeGörev
{
    public partial class Güncelleme : Form
    {

        public Güncelleme()
        {
            InitializeComponent();
        }


        public int Id { get; set; }
        public bool YenidenYukle { get; set; }

        public static string KullaniciAdi { get; set; }


        private void ListeyiDoldur1()
        {
            treeView1.Nodes.Clear();

            string EkleyenKisi = KullaniciVT.EkleyenKullaniciGetirme(Id);

            
            TreeNode tn = new TreeNode();
            tn.Text = EkleyenKisi;
            tn.Name = "K" + EkleyenKisi;
            tn.Checked = true;

            treeView1.Nodes.Add(tn);

        }



        
        private void ListeyiDoldur2()
        {
            treeView2.Nodes.Clear();

            List<Gorevler> AtananKullanicilar = KullaniciVT.GorevlileriGetir(Id);
            List<Gorevler> AktifKullanicilar = KullaniciVT.AktifKullanicilarListesiniGetir();

            foreach (Gorevler item in AktifKullanicilar)
            {

                bool varmi = false;
                foreach (Gorevler i2 in AtananKullanicilar)
                {
                    if (i2.KullaniciAdi == item.KullaniciAdi)
                    {
                        varmi = true;
                    }
                }
                TreeNode tn = new TreeNode();
                tn.Text = item.KullaniciAdi;
                tn.Name = "K" + item.KullaniciId.ToString();
                tn.Checked = varmi;

                treeView2.Nodes.Add(tn);

            }

        }


        // 2.Yol olarak yaptım.
        private void ListeyiDoldur3()
        {
            treeView2.Nodes.Clear();

            List<Gorevler> AtananKullanicilar = KullaniciVT.GorevlileriGetir(Id);
            List<Gorevler> AktifKullanicilar = KullaniciVT.AktifKullanicilarListesiniGetir();

            // Atanan kullanıcıların KullaniciId'lerini toplu olarak alma
            var atanilanKullaniciIdler = AtananKullanicilar.Select(a => a.KullaniciId).ToList();

            // Aktif kullanıcılar içinden atanmış olanları çıkartıp, sadece atanmamışları alma
            var atanmamisAktifKullanicilar = AktifKullanicilar.Where(a => !atanilanKullaniciIdler.Contains(a.KullaniciId)).ToList();

            // Atanan kullanıcıları seçili olarak TreeView'e ekleme
            foreach (Gorevler item in AtananKullanicilar)
            {
                TreeNode tn = new TreeNode();
                tn.Text = item.KullaniciAdi;
                tn.Name = "K" + item.KullaniciId.ToString();
                tn.Checked = true;

                treeView2.Nodes.Add(tn);
            }

            // Atanmamış aktif kullanıcıları seçili olmadan TreeView'e ekleme
            foreach (Gorevler item in atanmamisAktifKullanicilar)
            {
                TreeNode tn = new TreeNode();
                tn.Text = item.KullaniciAdi;
                tn.Name = "K" + item.KullaniciId.ToString();
                tn.Checked = false;

                treeView2.Nodes.Add(tn);
            }


        }


        private void ListeyiDoldur4()
        {
            listView1.Items.Clear();         

            List<Notlar> list = NotlarVT.NotlarListesiniGetir(Id);

            foreach (Notlar item2 in list)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = item2.GorevId.ToString();
                lvitem.SubItems.Add(item2.NotId.ToString());
                lvitem.SubItems.Add(item2.EklenenNot);
                lvitem.SubItems.Add(item2.EkleyenKullanici);
                lvitem.SubItems.Add(item2.EklenenTarih.ToString("dd.MM.yyyy"));

                listView1.Items.Add(lvitem);
            }
        }



        private void Güncelleme_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            treeView1.Enabled = false;
            treeView2.Enabled = false;


            ListeyiDoldur1();
            ListeyiDoldur2();
            ListeyiDoldur4();


            bool islemyapilsinmi = false;
            islemyapilsinmi = KullaniciVT.GorevlendirenGorevliDegistirebilirMi(Id, KullaniciAdi);
            if (islemyapilsinmi)
            {
                treeView2.Enabled=true;            
                comboBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                button2.Enabled = false; 
                listView1.Enabled = false;
            }
            
           
      
            List<Gorevler> AtananKullanicilar = KullaniciVT.GorevlileriGetir(Id);
            comboBox2.Enabled = false;
            foreach(Gorevler item in AtananKullanicilar)
            {
                if(item.KullaniciAdi == KullaniciAdi)
                {
                    treeView2.Enabled = false;
                    comboBox2.Enabled = true;
                    textBox4.Enabled = true;
                    button2.Enabled = true;               
                    textBox3.Enabled = false;
                    comboBox1.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;                   
                    radioButton1.Enabled = false;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                    radioButton4.Enabled = false;   
                    radioButton5.Enabled = false;
                }
               
            }

            



            List<string> Kategoriler = GorevlerVT.KategorileriGetir();

            comboBox1.Items.Clear();        
            foreach (string item in Kategoriler)
            {
                comboBox1.Items.Add(item);
            }



            List<string> Durumlar = GorevlerVT.DurumlarıGetir();

            comboBox2.Items.Clear();
            foreach (string item in Durumlar)
            {
                comboBox2.Items.Add(item);
            }




            Gorevler gorev = GorevlerVT.GorevIdyeGoreGetir(Id);

            textBox3.Text = gorev.id.ToString();
            comboBox1.SelectedItem = gorev.KategoriAdi.ToString();
            dateTimePicker1.Text = gorev.Tarih.ToString("dd.MM.yyyy");       
            textBox1.Text = gorev.Baslik.ToString();
            textBox2.Text = gorev.Aciklama.ToString();
            comboBox2.SelectedItem = gorev.Durum.ToString();
            if (radioButton1.Text == gorev.Aciliyet.ToString())
            {
                radioButton1.Checked = true;
            }
            else if (radioButton2.Text == gorev.Aciliyet.ToString())
            {
                radioButton2.Checked = true;
            }
            else if (radioButton3.Text == gorev.Aciliyet.ToString())
            {
                radioButton3.Checked = true;
            }
            else if (radioButton4.Text == gorev.Aciliyet.ToString())
            {
                radioButton4.Checked = true;
            }
            else if (radioButton5.Text == gorev.Aciliyet.ToString())
            {
                radioButton5.Checked = true;
            }
    
        }

        public int YeniGorevliId { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null) 
            {
                Gorevler gorev = new Gorevler
                {
                    id = Convert.ToInt32(textBox3.Text),
                    KategoriId = GorevlerVT.KategoriIdGetirme(comboBox1.Text),
                    Tarih = dateTimePicker1.Value,
                    Baslik = textBox1.Text.ToString(),
                    Aciklama = textBox2.Text.ToString(),
                    AciliyetId = radioButton1.Checked == true ? 1 : radioButton2.Checked == true ? 2 : radioButton3.Checked == true ? 3 : radioButton4.Checked == true ? 4 : 5,
                    DurumId = GorevlerVT.DurumIdGetirme(comboBox2.Text),                   
                };


//*********************************************************************************************************************************
                List<int> list = new List<int>();

                int sec = 0;
                for (int i = 0; i < treeView2.Nodes.Count; i++)
                {
                    TreeNode tn = treeView2.Nodes[i];
                    if (tn.Checked)
                    {
                        sec++;
                        YeniGorevliId = Convert.ToInt32(tn.Name.Substring(1, tn.Name.Length - 1));
                        list.Add(YeniGorevliId);
                    }

                }


                if (sec == 0)
                {
                    MessageBox.Show("En az bir görevli seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    KullaniciVT.Gorevler_GorevliSil(Id);
                    foreach (int item in list)
                    {
                        KullaniciVT.GoreveGorevliEkle(Id, item);
                    }
                    YenidenYukle = true;
                    this.Close();
                }
//*********************************************************************************************************************************

                bool islemYapildi = GorevlerVT.GorevGuncelle(gorev);
                if (islemYapildi)
                {
                    YenidenYukle = true;
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurun.","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Notlar not = new Notlar
            {
                GorevId = Id,
                NotId = 0,  
                EklenenNot = textBox4.Text,
                EkleyenKullanici = KullaniciAdi,
                EklenenTarih = DateTime.Now
            };

            if (not.EklenenNot != string.Empty)
            {
                bool islemyapildi = NotlarVT.NotEkle(not);
                if (islemyapildi)
                {
                    ListeyiDoldur4();
                }
                else
                {
                    MessageBox.Show("Not Eklenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Bir Not yazın!!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }


        int id;
        private void listView1_MouseDoubleClick(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Bu Notu Silmek İstiyor Musun?", "Uyarı!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (sonuc == DialogResult.Yes)
            {
                id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

                Notlar not = new Notlar
                {
                    GorevId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text),
                    NotId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text),
                    EklenenNot = (listView1.SelectedItems[0].SubItems[2]).Text,
                    EkleyenKullanici = (listView1.SelectedItems[0].SubItems[3]).Text,
                    EklenenTarih = Convert.ToDateTime(listView1.SelectedItems[0].SubItems[4].Text)
                };

                bool islemyapıldı = NotlarVT.NotSil(not);
                if (islemyapıldı)
                {
                    MessageBox.Show("Not Başarıyla Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListeyiDoldur4();
                }
            }
        }





       
    }
}
