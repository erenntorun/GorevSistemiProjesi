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
    public partial class GorevEkleme : Form
    {
        public GorevEkleme()
        {
            InitializeComponent();
        }


             

        private void ListeyiDoldur()
        {
            treeView1.Nodes.Clear();

            List<Gorevler> list = KullaniciVT.AktifKullanicilarListesiniGetir();

            foreach (Gorevler item in list)
            {
                TreeNode tn = new TreeNode();

                tn.Text = item.KullaniciAdi;
                tn.Name = "k" + item.KullaniciId.ToString();


                treeView1.Nodes.Add(tn);
            }
            
        }





        private void GorevEkleme_Load(object sender, EventArgs e)
        {
            EkleyenKisi = Form1.KullaniciId;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            dateTimePicker1.Value = DateTime.Today;


            List<string> Kategoriler = GorevlerVT.KategorileriGetir();

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Seçiniz");
            foreach (string item in Kategoriler)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;

            ListeyiDoldur();
            

        }

        public bool YenidenYukle { get; set; }

        private int yeniGorevId;

        public int EkleyenKisi { get; set; }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && textBox1.Text != string.Empty && textBox2.Text != string.Empty)
            {
                Gorevler gorev = new Gorevler
                {
                    id = 0,
                    KategoriId = GorevlerVT.KategoriIdGetirme(comboBox1.Text),
                    Tarih = dateTimePicker1.Value,
                    Baslik = textBox1.Text,
                    Aciklama = textBox2.Text,
                    AciliyetId = radioButton1.Checked == true ? 1 : radioButton2.Checked == true ? 2 : radioButton3.Checked == true ? 3 : radioButton4.Checked == true ? 4 : 5,
                    DurumId = 1,
                    EkleyenKullaniciId = EkleyenKisi,              
                };

               
                int seç = 0;
                List<int> list = new List<int>();
                
                string secilenIdler = "";
                for(int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    TreeNode tn = treeView1.Nodes[i];
                    if (tn.Checked)
                    {
                        seç++;

                        secilenIdler = tn.Name.Substring(1, tn.Name.Length - 1);
                        int GorevliId = Convert.ToInt32(tn.Name.Substring(1,tn.Name.Length - 1));
                        list.Add(GorevliId);                     
                    }

                }

                if(seç ==0)
                {
                    MessageBox.Show("En az bir görevli seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    yeniGorevId = GorevlerVT.GorevEkle3(gorev);
                    foreach (int item in list)
                    {
                        KullaniciVT.GoreveGorevliEkle(yeniGorevId, item);
                    }
                    YenidenYukle = true;
                    this.Close();
                }

            

            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

       


    }
}
