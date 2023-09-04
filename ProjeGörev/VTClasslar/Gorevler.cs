using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeGörev
{
    public class Gorevler 
    {
        // Gorevler Tablosu

        public int id {  get; set; }

        public int KategoriId { get; set; }

        public DateTime Tarih { get; set; }

        public string Baslik { get; set; }

        public string Aciklama { get; set; }

        public int AciliyetId { get; set; }

        public int DurumId { get; set; }

        public int EkleyenKullaniciId { get; set; }



        // Kategoriler Tablosu

        public string KategoriAdi { get; set; }

        public int Aktif { get; set; }


        // Aciliyet Tablosu

        public  string Aciliyet { get; set; }


        // Durum Tablosu

        public string Durum { get; set;}


        // Kullanicilar Tablosu

        public int KullaniciId { get; set; }

        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }

        public int Yonetici { get; set; }

        public string Mail { get; set; }

        public string AdSoyad { get; set; }

        public int Onay { get; set; }

        public int KAktif { get; set; }


        // Gorevler_Gorevli Tablosu

        public int Id { get; set; }

        public int GorevId { get; set; }

        public int GorevliId { get; set; }






    }
}
