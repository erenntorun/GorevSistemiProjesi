using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ProjeGörev
{
    public class GorevlerVT
    {

        public static List<Gorevler> GorevlerListesiniGetir()
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select G.Id, K.KategoriAdi, G.Tarih, G.Baslik, A.Aciliyet, D.Durum,G.EkleyenKullaniciId from Gorevler G " +
                "left join Kategoriler K on G.KategoriId = K.Id " +
                "left join Aciliyet A on G.AciliyetId = A.Id " +
                "left join Durum D on G.DurumId = D.Id " +
                "left join Kullanicilar Ku on G.EkleyenKullaniciId = Ku.KullaniciId";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                gorev.KategoriAdi = dr["KategoriAdi"] is DBNull ? string.Empty : dr["KategoriAdi"].ToString();
                gorev.Tarih = dr["Tarih"] is DBNull ? new DateTime(2000,1,1) : Convert.ToDateTime(dr["Tarih"]);
                gorev.Baslik = dr["Baslik"] is DBNull ? string.Empty : dr["Baslik"].ToString();
                gorev.Aciliyet = dr["Aciliyet"] is DBNull ? string.Empty : dr["Aciliyet"].ToString();
                gorev.Durum = dr["Durum"] is DBNull ? string.Empty : dr["Durum"].ToString();
                gorev.EkleyenKullaniciId= dr["EkleyenKullaniciId"] is DBNull ? 0 : Convert.ToInt32(dr["EkleyenKullaniciId"]);

                list.Add(gorev);
            }
            baglan.Close();



            return list;
        }


        public static List<string> KategorileriGetir()
        {
            List<string> list = new List<string>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select distinct KategoriAdi from Kategoriler where Aktif = 1 ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(dr["KategoriAdi"] is DBNull ? string.Empty : dr["KategoriAdi"].ToString());
            }
            baglan.Close();



            return list;
        }


        public static List<string> DurumlarıGetir()
        {
            List<string> list = new List<string>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select distinct Durum from Durum";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(dr["Durum"] is DBNull ? string.Empty : dr["Durum"].ToString());
            }
            baglan.Close();



            return list;
        }


/*
        public static List<string> KullaniciAdlariGetir()
        {
            List<string> list = new List<string>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select distinct KullaniciAdi from Kullanicilar";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(dr["KullaniciAdi"] is DBNull ? string.Empty : dr["KullaniciAdi"].ToString());
            }
            baglan.Close();



            return list;
        }

*/


        public static List<Gorevler> GorevlerListesiniGetir(string Kategori, string Durum, string Ara)
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select G.Id, K.KategoriAdi, G.Tarih, G.Baslik, A.Aciliyet, D.Durum  from Gorevler G " +
                "left join Kategoriler K on G.KategoriId = K.Id " +
                "left join Aciliyet A on G.AciliyetId = A.Id " +
                "left join Durum D on G.DurumId = D.Id where G.Baslik like '%'+ @Ara +'%' and " +
                "KategoriAdi=(case when @KategoriAdi='TÜMÜ' then KategoriAdi else @KategoriAdi end) " +
                "and Durum=(case when @Durum='TÜMÜ' then Durum else @Durum end)";

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@Ara", System.Data.SqlDbType.VarChar);
            prm.Value = Ara;
            cmd.Parameters.Add(prm);

            SqlParameter prm1 = new SqlParameter("@KategoriAdi", System.Data.SqlDbType.VarChar);
            prm1.Value = Kategori;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@Durum", System.Data.SqlDbType.VarChar);
            prm2.Value = Durum;
            cmd.Parameters.Add(prm2);

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                gorev.KategoriAdi = dr["KategoriAdi"] is DBNull ? string.Empty : dr["KategoriAdi"].ToString();
                gorev.Tarih = dr["Tarih"] is DBNull ? new DateTime(2000, 1, 1) : Convert.ToDateTime(dr["Tarih"]);
                gorev.Baslik = dr["Baslik"] is DBNull ? string.Empty : dr["Baslik"].ToString();
                gorev.Aciliyet = dr["Aciliyet"] is DBNull ? string.Empty : dr["Aciliyet"].ToString();
                gorev.Durum = dr["Durum"] is DBNull ? string.Empty : dr["Durum"].ToString();


                list.Add(gorev);
            }
            baglan.Close();



            return list;
        }


        public static List<Gorevler> GorevlerListesiniGetir(string Kategori, string Durum, string Ara, DateTime BasTarihi, DateTime BitTarihi)
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select G.Id, K.KategoriAdi, G.Tarih, G.Baslik, A.Aciliyet, D.Durum  from Gorevler G " +
                "left join Kategoriler K on G.KategoriId = K.Id " +
                "left join Aciliyet A on G.AciliyetId = A.Id " +
                "left join Durum D on G.DurumId = D.Id where G.Baslik like '%'+ @Ara +'%' and " +
                "KategoriAdi=(case when @KategoriAdi='TÜMÜ' then KategoriAdi else @KategoriAdi end) " +
                "and Durum=(case when @Durum='TÜMÜ' then Durum else @Durum end)" +
                "and Tarih between @BasTarihi and @BitTarihi ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;



            SqlParameter prm = new SqlParameter("@Ara", System.Data.SqlDbType.VarChar);
            prm.Value = Ara;
            cmd.Parameters.Add(prm);

            SqlParameter prm1 = new SqlParameter("@KategoriAdi", System.Data.SqlDbType.VarChar);
            prm1.Value = Kategori;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@Durum", System.Data.SqlDbType.VarChar);
            prm2.Value = Durum;
            cmd.Parameters.Add(prm2);

            SqlParameter prm3 = new SqlParameter("@BasTarihi", System.Data.SqlDbType.DateTime);
            prm3.Value = BasTarihi;
            cmd.Parameters.Add(prm3);

            SqlParameter prm4 = new SqlParameter("@BitTarihi", System.Data.SqlDbType.DateTime);
            prm4.Value = BitTarihi;
            cmd.Parameters.Add(prm4);


            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                gorev.KategoriAdi = dr["KategoriAdi"] is DBNull ? string.Empty : dr["KategoriAdi"].ToString();
                gorev.Tarih = dr["Tarih"] is DBNull ? new DateTime(2000, 1, 1) : Convert.ToDateTime(dr["Tarih"]);
                gorev.Baslik = dr["Baslik"] is DBNull ? string.Empty : dr["Baslik"].ToString();
                gorev.Aciliyet = dr["Aciliyet"] is DBNull ? string.Empty : dr["Aciliyet"].ToString();
                gorev.Durum = dr["Durum"] is DBNull ? string.Empty : dr["Durum"].ToString();


                list.Add(gorev);
            }
            baglan.Close();



            return list;
        }


        public static string KullaniciAdi1 { get; set; }

     
        public static List<Gorevler> GorevlerListesiniGetir2(string Kategori, string Durum, string Ara, DateTime BasTarihi, DateTime BitTarihi, int KullaniciId)
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);
            bool Yonetici = true;
            KullanıcıGirişi yeni = new KullanıcıGirişi();



            SqlCommand cmd = new SqlCommand();
            if (Yonetici == KullaniciVT.KullaniciYoneticiKontrolu(KullaniciAdi1))
            {
                cmd.CommandText = "Select G.Id, K.KategoriAdi, G.Tarih, G.Baslik, A.Aciliyet, D.Durum from Gorevler G " +
                    "left join Kategoriler K on G.KategoriId = K.Id " +
                    "left join Aciliyet A on G.AciliyetId = A.Id " +
                    "left join Durum D on G.DurumId = D.Id where G.Baslik like '%'+ @Ara +'%' and " +
                    "KategoriAdi=(case when @KategoriAdi='TÜMÜ' then KategoriAdi else @KategoriAdi end) " +
                    "and Durum=(case when @Durum='TÜMÜ' then Durum else @Durum end)" +
                    "and Tarih between @BasTarihi and @BitTarihi " +
                    "Order By Tarih asc";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = baglan;
            }
            else
            {
                cmd.CommandText = "Select G.Id, K.KategoriAdi, G.Tarih, G.Baslik, A.Aciliyet, D.Durum, G.EkleyenKullaniciId " +
                    @"from (
	                select Id from Gorevler where  EkleyenKullaniciId = @KullaniciId

	                union

	                select GorevId from Gorevler_Gorevli where GorevliId = @KullaniciId
                    ) x " +
                    "left join Gorevler G on G.Id = x.Id " +
                    "left join Kategoriler K on G.KategoriId = K.Id " +
                    "left join Aciliyet A on G.AciliyetId = A.Id " +
                    "left join Durum D on G.DurumId = D.Id " +
                    "where G.Baslik like '%'+ @Ara +'%' and " +
                    "KategoriAdi=(case when @KategoriAdi='TÜMÜ' then KategoriAdi else @KategoriAdi end) " +
                    "and Durum=(case when @Durum='TÜMÜ' then Durum else @Durum end) " +
                    "and Tarih between @BasTarihi and @BitTarihi " +
                    "Order By Tarih asc";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = baglan;

                SqlParameter prm5 = new SqlParameter("@KullaniciId", System.Data.SqlDbType.VarChar);
                prm5.Value = KullaniciId;
                cmd.Parameters.Add(prm5);
            }



            SqlParameter prm = new SqlParameter("@Ara", System.Data.SqlDbType.VarChar);
            prm.Value = Ara;
            cmd.Parameters.Add(prm);

            SqlParameter prm1 = new SqlParameter("@KategoriAdi", System.Data.SqlDbType.VarChar);
            prm1.Value = Kategori;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@Durum", System.Data.SqlDbType.VarChar);
            prm2.Value = Durum;
            cmd.Parameters.Add(prm2);

            SqlParameter prm3 = new SqlParameter("@BasTarihi", System.Data.SqlDbType.DateTime);
            prm3.Value = BasTarihi;
            cmd.Parameters.Add(prm3);

            SqlParameter prm4 = new SqlParameter("@BitTarihi", System.Data.SqlDbType.DateTime);
            prm4.Value = BitTarihi;
            cmd.Parameters.Add(prm4);


            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                gorev.KategoriAdi = dr["KategoriAdi"] is DBNull ? string.Empty : dr["KategoriAdi"].ToString();
                gorev.Tarih = dr["Tarih"] is DBNull ? new DateTime(2000, 1, 1) : Convert.ToDateTime(dr["Tarih"]);
                gorev.Baslik = dr["Baslik"] is DBNull ? string.Empty : dr["Baslik"].ToString();
                gorev.Aciliyet = dr["Aciliyet"] is DBNull ? string.Empty : dr["Aciliyet"].ToString();
                gorev.Durum = dr["Durum"] is DBNull ? string.Empty : dr["Durum"].ToString();

                list.Add(gorev);      
            }
            baglan.Close();

            return list;
        }





        public static Gorevler GorevIdyeGoreGetir(int Id)
        {
            Gorevler gorev = new Gorevler();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select G.Id, K.KategoriAdi, G.Tarih, G.Baslik,G.Aciklama, A.Aciliyet, D.Durum from Gorevler G " +
                "left join Kategoriler K on G.KategoriId = K.Id " +
                "left join Aciliyet A on G.AciliyetId = A.Id " +
                "left join Durum D on G.DurumId = D.Id WHERE G.Id= @Id ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            prm.Value = Id;
            cmd.Parameters.Add(prm);

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler item = new Gorevler();
                item.id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                item.KategoriAdi = dr["KategoriAdi"] is DBNull ? string.Empty : dr["KategoriAdi"].ToString();
                item.Tarih = dr["Tarih"] is DBNull ? new DateTime(2000, 1, 1) : Convert.ToDateTime(dr["Tarih"]);
                item.Baslik = dr["Baslik"] is DBNull ? string.Empty : dr["Baslik"].ToString();
                item.Aciklama = dr["Aciklama"] is DBNull ? string.Empty : dr["Aciklama"].ToString();
                item.Aciliyet = dr["Aciliyet"] is DBNull ? string.Empty : dr["Aciliyet"].ToString();
                item.Durum = dr["Durum"] is DBNull ? string.Empty : dr["Durum"].ToString();

                gorev = item;
            }



            baglan.Close();

            return gorev;
        }




        public static bool GorevGuncelle(Gorevler gorev)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Gorevler set KategoriId=@KategoriId, Tarih=@Tarih, Baslik=@Baslik, Aciklama=@Aciklama, " +
                "AciliyetId=@AciliyetId, DurumId=@DurumId where Id=@Id";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            prm.Value = gorev.id;
            cmd.Parameters.Add(prm);

            SqlParameter prm1 = new SqlParameter("@KategoriId", System.Data.SqlDbType.Int);
            prm1.Value = gorev.KategoriId;
            cmd.Parameters.Add(prm1);


            SqlParameter prm2 = new SqlParameter("@Tarih", System.Data.SqlDbType.DateTime);
            prm2.Value = gorev.Tarih;
            cmd.Parameters.Add(prm2);


            SqlParameter prm3 = new SqlParameter("@Baslik", System.Data.SqlDbType.VarChar);
            prm3.Value = gorev.Baslik;
            cmd.Parameters.Add(prm3);


            SqlParameter prm4 = new SqlParameter("@Aciklama", System.Data.SqlDbType.VarChar);
            prm4.Value = gorev.Aciklama;
            cmd.Parameters.Add(prm4);


            SqlParameter prm5 = new SqlParameter("@AciliyetId", System.Data.SqlDbType.Int);
            prm5.Value = gorev.AciliyetId;
            cmd.Parameters.Add(prm5);


            SqlParameter prm6 = new SqlParameter("@DurumId", System.Data.SqlDbType.Int);
            prm6.Value = gorev.DurumId;
            cmd.Parameters.Add(prm6);


            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            baglan.Close();


            return islem;
        }


        public static bool KategoriEkle(Gorevler gorev)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Kategoriler(KategoriAdi,Aktif) VALUES(@KategoriAdi,@Aktif)";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;


            SqlParameter prm1 = new SqlParameter("@KategoriAdi", System.Data.SqlDbType.VarChar);
            prm1.Value = gorev.KategoriAdi;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@Aktif", System.Data.SqlDbType.Bit);
            prm2.Value = gorev.Aktif;
            cmd.Parameters.Add(prm2);

            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            baglan.Close();


            return islem;
        }

        
        public static List<Gorevler> KategorilerListesiniGetir()
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Kategoriler";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.id= dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                gorev.KategoriAdi = dr["KategoriAdi"] is DBNull ? string.Empty : dr["KategoriAdi"].ToString();
                gorev.Aktif = dr["Aktif"] is DBNull ? 0 : Convert.ToInt32(dr["Aktif"]);

                list.Add(gorev);

            }
            baglan.Close();



            return list;
        }



        
        public static bool KategoriGuncelle(Gorevler gorev)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Kategoriler set KategoriAdi=@KategoriAdi, Aktif=@Aktif where Id=@Id";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            prm.Value = gorev.id;
            cmd.Parameters.Add(prm);

            SqlParameter prm1 = new SqlParameter("@KategoriAdi", System.Data.SqlDbType.VarChar);
            prm1.Value = gorev.KategoriAdi;
            cmd.Parameters.Add(prm1);


            SqlParameter prm2 = new SqlParameter("@Aktif", System.Data.SqlDbType.Int);
            prm2.Value = gorev.Aktif;
            cmd.Parameters.Add(prm2);

   

            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            baglan.Close();


            return islem;
        }


        public static bool KategoriPasifleştir(int Id)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Kategoriler Set Aktif = 0  where  Id = @Id";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            prm.Value = Id;
            cmd.Parameters.Add(prm);



            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            baglan.Close();


            return islem;
        }

        public static bool KategoriAktifleştir(int Id)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Kategoriler Set Aktif = 1 where  Id = @Id";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            prm.Value = Id;
            cmd.Parameters.Add(prm);



            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            baglan.Close();


            return islem;
        }



        public static bool GorevEkle(Gorevler gorev)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Gorevler(KategoriId,Tarih,Baslik,Aciklama,AciliyetId,DurumId,EkleyenKullaniciAdi) " +
                "VALUES(@KategoriId,@Tarih,@Baslik,@Aciklama,@AciliyetId,@DurumId,@EkleyenKullaniciId)";
            cmd.CommandType= System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KategoriId",System.Data.SqlDbType.Int);
            prm.Value = gorev.KategoriId;
            cmd.Parameters.Add(prm);

            SqlParameter prm2 = new SqlParameter("@Tarih",System.Data.SqlDbType.DateTime);
            prm2.Value = gorev.Tarih;
            cmd.Parameters.Add(prm2);

            SqlParameter prm3 = new SqlParameter("@Baslik",System.Data.SqlDbType.VarChar);
            prm3.Value = gorev.Baslik;
            cmd.Parameters.Add(prm3);

            SqlParameter prm4 = new SqlParameter("@Aciklama", System.Data.SqlDbType.VarChar);
            prm4.Value = gorev.Aciklama;
            cmd.Parameters.Add(prm4);

            SqlParameter prm5 = new SqlParameter("@AciliyetId", System.Data.SqlDbType.Int);
            prm5.Value = gorev.AciliyetId;
            cmd.Parameters.Add(prm5);

            SqlParameter prm6 = new SqlParameter("@DurumId", System.Data.SqlDbType.Int);
            prm6.Value = gorev.DurumId;
            cmd.Parameters.Add(prm6);

            SqlParameter prm7 = new SqlParameter("@EkleyenKullaniciId", System.Data.SqlDbType.VarChar);
            prm7.Value = gorev.EkleyenKullaniciId;
            cmd.Parameters.Add(prm7);


            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }
            baglan.Close();


            return islem;
        }


        public static int KategoriIdGetirme(string deger)
        {
            int sonuc=0;
            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select TOP 1 G.KategoriId from Gorevler G inner join Kategoriler K on G.KategoriId = K.Id " +
                "where K.KategoriAdi = @deger  ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@deger", System.Data.SqlDbType.VarChar);
            prm.Value = deger;
            cmd.Parameters.Add(prm);

            baglan.Open();
            object result = cmd.ExecuteScalar();
            if(result != null && result!= DBNull.Value)
            {
                sonuc = Convert.ToInt32(result);
            }
            baglan.Close();


            return sonuc;
        }

        public static int DurumIdGetirme(string deger)
        {
            int sonuc = 0;
            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select TOP 1 G.DurumId from Gorevler G inner join Durum D on G.DurumId = D.Id " +
                "where D.Durum = @deger  ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@deger", System.Data.SqlDbType.VarChar);
            prm.Value = deger;
            cmd.Parameters.Add(prm);

            baglan.Open();
            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                sonuc = Convert.ToInt32(result);
            }
            baglan.Close();


            return sonuc;
        }


        public static int GorevEkle2(Gorevler gorev)
        {
            int yeniGorevId = -1; // Varsayılan değeri -1 olarak belirleyelim

            using (SqlConnection baglan = new SqlConnection(Connection.ConnectionString))
            {
                baglan.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Gorevler(KategoriId,Tarih,Baslik,Aciklama,AciliyetId,DurumId,EkleyenKullaniciId) " +
                    "VALUES(@KategoriId,@Tarih,@Baslik,@Aciklama,@AciliyetId,@DurumId,@EkleyenKullaniciId);" +
                    "SELECT SCOPE_IDENTITY();"; // Son eklenen kaydın ID'sini döndüren sorgu
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = baglan;

                // Parametreleri eklemeye devam edin...
                SqlParameter prm = new SqlParameter("@KategoriId", System.Data.SqlDbType.Int);
                prm.Value = gorev.KategoriId;
                cmd.Parameters.Add(prm);

                SqlParameter prm2 = new SqlParameter("@Tarih", System.Data.SqlDbType.DateTime);
                prm2.Value = gorev.Tarih;
                cmd.Parameters.Add(prm2);

                SqlParameter prm3 = new SqlParameter("@Baslik", System.Data.SqlDbType.VarChar);
                prm3.Value = gorev.Baslik;
                cmd.Parameters.Add(prm3);

                SqlParameter prm4 = new SqlParameter("@Aciklama", System.Data.SqlDbType.VarChar);
                prm4.Value = gorev.Aciklama;
                cmd.Parameters.Add(prm4);

                SqlParameter prm5 = new SqlParameter("@AciliyetId", System.Data.SqlDbType.Int);
                prm5.Value = gorev.AciliyetId;
                cmd.Parameters.Add(prm5);

                SqlParameter prm6 = new SqlParameter("@DurumId", System.Data.SqlDbType.Int);
                prm6.Value = gorev.DurumId;
                cmd.Parameters.Add(prm6);

                SqlParameter prm7 = new SqlParameter("@EkleyenKullaniciId", System.Data.SqlDbType.Int);
                prm7.Value = gorev.EkleyenKullaniciId;
                cmd.Parameters.Add(prm7);

                yeniGorevId = Convert.ToInt32(cmd.ExecuteScalar()); // SCOPE_IDENTITY() değerini alalım

                baglan.Close();
            }

            return yeniGorevId;
        }



        public static int GorevEkle3(Gorevler gorev)
        {
            int yeniGorevId = -1; // Varsayılan değeri -1 olarak atadım.

            using (SqlConnection baglan = new SqlConnection(Connection.ConnectionString))
            {
                baglan.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Gorevler(KategoriId,Tarih,Baslik,Aciklama,AciliyetId,DurumId,EkleyenKullaniciId) " +
                    "VALUES(@KategoriId,@Tarih,@Baslik,@Aciklama,@AciliyetId,@DurumId,@EkleyenKullaniciId);" +
                    "SELECT SCOPE_IDENTITY();"; // Son eklenen kaydın ID'sini döndüren sorgu
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = baglan;

                // Parametreleri eklemeye devam edin...
                SqlParameter prm = new SqlParameter("@KategoriId", System.Data.SqlDbType.Int);
                prm.Value = gorev.KategoriId;
                cmd.Parameters.Add(prm);

                SqlParameter prm2 = new SqlParameter("@Tarih", System.Data.SqlDbType.DateTime);
                prm2.Value = gorev.Tarih;
                cmd.Parameters.Add(prm2);

                SqlParameter prm3 = new SqlParameter("@Baslik", System.Data.SqlDbType.VarChar);
                prm3.Value = gorev.Baslik;
                cmd.Parameters.Add(prm3);

                SqlParameter prm4 = new SqlParameter("@Aciklama", System.Data.SqlDbType.VarChar);
                prm4.Value = gorev.Aciklama;
                cmd.Parameters.Add(prm4);

                SqlParameter prm5 = new SqlParameter("@AciliyetId", System.Data.SqlDbType.Int);
                prm5.Value = gorev.AciliyetId;
                cmd.Parameters.Add(prm5);

                SqlParameter prm6 = new SqlParameter("@DurumId", System.Data.SqlDbType.Int);
                prm6.Value = gorev.DurumId;
                cmd.Parameters.Add(prm6);

                SqlParameter prm7 = new SqlParameter("@EkleyenKullaniciId", System.Data.SqlDbType.Int);
                prm7.Value = gorev.EkleyenKullaniciId;
                cmd.Parameters.Add(prm7);


                yeniGorevId = Convert.ToInt32(cmd.ExecuteScalar()); // SCOPE_IDENTITY() değerini aldım.

                baglan.Close();
            }

            return yeniGorevId;
        }




    }
}
