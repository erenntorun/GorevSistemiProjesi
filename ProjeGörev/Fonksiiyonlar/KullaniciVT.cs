using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Drawing;

namespace ProjeGörev
{
    public class KullaniciVT
    {

        public static bool KullanıcıGirisKontrolu(string KullaniciAdi, string Sifre)
        {
            bool GirisDeneme = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select COUNT(*) from Kullanicilar WHERE " +
                "KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre AND Onay=1 AND Aktif=1";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciAdi",System.Data.SqlDbType.VarChar);
            prm.Value = KullaniciAdi;
            cmd.Parameters.Add(prm);

            SqlParameter prm2 = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm2.Value = Sifre;
            cmd.Parameters.Add(prm2);

            baglan.Open();
            int kullaniciSayisi = Convert.ToInt32(cmd.ExecuteScalar());
            if (kullaniciSayisi > 0)
            {
                GirisDeneme = true;
            }
            baglan.Close();

            return GirisDeneme;
        }


        public static string KullanıcıAdSoyad(string KullaniciAdi, string Sifre)
        {
            string sonuc =" ";
            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select AdSoyad from Kullanicilar WHERE " +
                "KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm.Value = KullaniciAdi;
            cmd.Parameters.Add(prm);

            SqlParameter prm2 = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm2.Value = Sifre;
            cmd.Parameters.Add(prm2);

            baglan.Open();
            object result = cmd.ExecuteScalar();
            if (result != string.Empty)
            {
                sonuc = result.ToString();
            }
            baglan.Close();


            return sonuc;
        }


        public static bool KullaniciYoneticiKontrolu(string KullaniciAdi)
        {
            bool Yonetici;
            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Yonetici FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm.Value = KullaniciAdi;
            cmd.Parameters.Add(prm);

            baglan.Open();
            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                Yonetici = Convert.ToBoolean(result);
                
            }
            else
            {
                Yonetici = false;
            }
            baglan.Close();



            return Yonetici;
        }


        public static bool KullanıcıEmailKontrolu(string Email)
        {
            bool EmailDeneme = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select Count(*) from Kullanicilar WHERE " +
                "Mail = @Mail";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@Mail", System.Data.SqlDbType.VarChar);
            prm.Value = Email;
            cmd.Parameters.Add(prm);


            baglan.Open();
            int kullaniciSayisi = Convert.ToInt32(cmd.ExecuteScalar());
            if (kullaniciSayisi > 0)
            {
                EmailDeneme = true;
            }
            baglan.Close();


            return EmailDeneme;
        }


        public static void KullaniciGuncelle(string YeniSifre ,string Email)
        {
            
            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Kullanicilar set Sifre=@Sifre where Mail=@Mail";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm.Value = YeniSifre;
            cmd.Parameters.Add(prm);

            SqlParameter prm2 = new SqlParameter("@Mail", System.Data.SqlDbType.VarChar);
            prm2.Value = Email;
            cmd.Parameters.Add(prm2);


            baglan.Open();
            cmd.ExecuteNonQuery();
            baglan.Close();
        }


        public static void MailGonder(string Email, string konu, string icerik)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;

            string kime = Email;


            sc.Credentials = new NetworkCredential("erenntorun2@gmail.com", "ET123456");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("erenn_843@hotmail.com", "Eren Torun");
            mail.To.Add(kime);
            //mail.To.Add("alici2@mail.com");
            //mail.CC.Add("alici3@mail.com");
            //mail.CC.Add("alici4@mail.com");
            mail.Subject = konu;
            mail.IsBodyHtml = false;
            mail.Body = icerik;
            //mail.Attachments.Add(new Attachment(DosyaYolu));
            sc.Send(mail);
        }


        public static bool KullaniciEkle(Gorevler gorev)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Kullanicilar(KullaniciAdi,Sifre,Yonetici,Mail,AdSoyad,Onay,Aktif) " +
                "VALUES(@KullaniciAdi,@Sifre,0,@Mail,@AdSoyad,0,1)";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;


            SqlParameter prm1 = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm1.Value = gorev.KullaniciAdi;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm2.Value = gorev.Sifre;
            cmd.Parameters.Add(prm2);

            SqlParameter prm3 = new SqlParameter("@Mail", System.Data.SqlDbType.VarChar);
            prm3.Value = gorev.Mail;
            cmd.Parameters.Add(prm3);

            SqlParameter prm4 = new SqlParameter("@AdSoyad", System.Data.SqlDbType.VarChar);
            prm4.Value = gorev.AdSoyad;
            cmd.Parameters.Add(prm4);


            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }
            baglan.Close();

            return islem;
        }


        public static List<Gorevler> KullanicilarListesiniGetir()
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Kullanicilar where Onay = 0";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.KullaniciId = dr["KullaniciId"] is DBNull ? 0 : Convert.ToInt32(dr["KullaniciId"]);
                gorev.KullaniciAdi = dr["KullaniciAdi"] is DBNull ? string.Empty : dr["KullaniciAdi"].ToString();                
                gorev.Sifre = dr["Sifre"] is DBNull ? string.Empty : dr["Sifre"].ToString();
                gorev.Yonetici = dr["Yonetici"] is DBNull ? 0 : Convert.ToInt32(dr["Yonetici"]);
                gorev.Mail = dr["Mail"] is DBNull ? string.Empty : dr["Mail"].ToString();
                gorev.AdSoyad = dr["AdSoyad"] is DBNull ? string.Empty : dr["AdSoyad"].ToString();
                gorev.Onay = dr["Onay"] is DBNull ? 0 : Convert.ToInt32(dr["Onay"]);

                list.Add(gorev);
            }
            baglan.Close();



            return list;
        }

        public static void KullaniciOnayla(int KullaniciId)
        {
            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Kullanicilar set Onay = 1 where KullaniciId = @KullaniciId";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciId", System.Data.SqlDbType.Int);
            prm.Value = KullaniciId;
            cmd.Parameters.Add(prm);



            baglan.Open();

            cmd.ExecuteNonQuery();

            baglan.Close();

        }

        public static List<Gorevler> TumKullanicilarListesiniGetir()
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Kullanicilar";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.KullaniciId = dr["KullaniciId"] is DBNull ? 0 : Convert.ToInt32(dr["KullaniciId"]);
                gorev.KullaniciAdi = dr["KullaniciAdi"] is DBNull ? string.Empty : dr["KullaniciAdi"].ToString();
                gorev.Sifre = dr["Sifre"] is DBNull ? string.Empty : dr["Sifre"].ToString();
                gorev.Yonetici = dr["Yonetici"] is DBNull ? 0 : Convert.ToInt32(dr["Yonetici"]);
                gorev.Mail = dr["Mail"] is DBNull ? string.Empty : dr["Mail"].ToString();
                gorev.AdSoyad = dr["AdSoyad"] is DBNull ? string.Empty : dr["AdSoyad"].ToString();
                gorev.Onay = dr["Onay"] is DBNull ? 0 : Convert.ToInt32(dr["Onay"]);
                gorev.KAktif = dr["Aktif"] is DBNull ? 0 : Convert.ToInt32(dr["Aktif"]);

                list.Add(gorev);
            }
            baglan.Close();



            return list;
        }



        public static bool KullaniciGuncelle(Gorevler gorev)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Kullanicilar set KullaniciAdi=@KullaniciAdi, Sifre=@Sifre, Yonetici=@Yonetici, Mail=@Mail, " +
                "AdSoyad=@AdSoyad, Onay=@Onay, Aktif = @Aktif where KullaniciId=@KullaniciId";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm.Value = gorev.KullaniciAdi;
            cmd.Parameters.Add(prm);

            SqlParameter prm1 = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm1.Value = gorev.Sifre;
            cmd.Parameters.Add(prm1);


            SqlParameter prm2 = new SqlParameter("@Yonetici", System.Data.SqlDbType.Bit);
            prm2.Value = gorev.Yonetici;
            cmd.Parameters.Add(prm2);


            SqlParameter prm3 = new SqlParameter("@Mail", System.Data.SqlDbType.VarChar);
            prm3.Value = gorev.Mail;
            cmd.Parameters.Add(prm3);


            SqlParameter prm4 = new SqlParameter("@AdSoyad", System.Data.SqlDbType.VarChar);
            prm4.Value = gorev.AdSoyad;
            cmd.Parameters.Add(prm4);


            SqlParameter prm5 = new SqlParameter("@Onay", System.Data.SqlDbType.Bit);
            prm5.Value = gorev.Onay;
            cmd.Parameters.Add(prm5);

            SqlParameter prm6 = new SqlParameter("@Aktif", System.Data.SqlDbType.Bit);
            prm6.Value = gorev.KAktif;
            cmd.Parameters.Add(prm6);

            SqlParameter prm7 = new SqlParameter("@KullaniciId", System.Data.SqlDbType.Int);
            prm7.Value = gorev.KullaniciId;
            cmd.Parameters.Add(prm7);




            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            baglan.Close();


            return islem;
        }


        public static bool KullaniciEkleYonetici(Gorevler gorev)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Kullanicilar(KullaniciAdi,Sifre,Yonetici,Mail,AdSoyad,Onay,Aktif) " +
                "VALUES(@KullaniciAdi,@Sifre,@Yonetici,@Mail,@AdSoyad,1,1)";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;


            SqlParameter prm1 = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm1.Value = gorev.KullaniciAdi;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm2.Value = gorev.Sifre;
            cmd.Parameters.Add(prm2);

            SqlParameter prm3 = new SqlParameter("@Yonetici", System.Data.SqlDbType.Bit);
            prm3.Value = gorev.Yonetici;
            cmd.Parameters.Add(prm3);

            SqlParameter prm4 = new SqlParameter("@Mail", System.Data.SqlDbType.VarChar);
            prm4.Value = gorev.Mail;
            cmd.Parameters.Add(prm4);

            SqlParameter prm5 = new SqlParameter("@AdSoyad", System.Data.SqlDbType.VarChar);
            prm5.Value = gorev.AdSoyad;
            cmd.Parameters.Add(prm5);



            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            baglan.Close();


            return islem;
        }



        public static bool KullanıcıGirisKontrolu2(string KullaniciAdi, string Sifre)
        {
            bool GirisDeneme = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select COUNT(*) from Kullanicilar WHERE " +
                "KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre  AND Aktif = 0";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm.Value = KullaniciAdi;
            cmd.Parameters.Add(prm);

            SqlParameter prm2 = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm2.Value = Sifre;
            cmd.Parameters.Add(prm2);

            baglan.Open();
            int kullaniciSayisi = Convert.ToInt32(cmd.ExecuteScalar());
            if (kullaniciSayisi > 0)
            {
                GirisDeneme = true;
            }


            baglan.Close();


            return GirisDeneme;
        }

        public static bool KullanıcıGirisKontrolu3(string KullaniciAdi, string Sifre)
        {
            bool GirisDeneme = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select COUNT(*) from Kullanicilar WHERE " +
                "KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre  AND Aktif = 1 AND Onay=0";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm.Value = KullaniciAdi;
            cmd.Parameters.Add(prm);

            SqlParameter prm2 = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm2.Value = Sifre;
            cmd.Parameters.Add(prm2);

            baglan.Open();
            int kullaniciSayisi = Convert.ToInt32(cmd.ExecuteScalar());
            if (kullaniciSayisi > 0)
            {
                GirisDeneme = true;
            }
            baglan.Close();

            return GirisDeneme;
        }


        public static string KullanıcıGirisKontrolu4(string KullaniciAdi, string Sifre)
        {
            string sonuc = "";

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "KullanıcıGiris";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm.Value = KullaniciAdi;
            cmd.Parameters.Add(prm);

            SqlParameter prm2 = new SqlParameter("@Sifre", System.Data.SqlDbType.VarChar);
            prm2.Value = Sifre;
            cmd.Parameters.Add(prm2);


            baglan.Open();
            object result = cmd.ExecuteScalar();
            if (result != string.Empty)
            {
                sonuc = result.ToString();
            }
            baglan.Close();


            return sonuc;
        }


        // Bu Tabloyu Kaldırdım Artık Bu fonksiyon kullanılmıyor.
        /*
        public static List<Gorevler> GorevlilerListesiniGetir()
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Gorevliler";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                gorev.GorevliAdSoyad = dr["GorevliAdSoyad"] is DBNull ? string.Empty : dr["GorevliAdSoyad"].ToString();

                list.Add(gorev);
            }
            baglan.Close();



            return list;
        }
        */


        public static bool GoreveGorevliEkle(int GorevId, int GorevliId)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Gorevler_Gorevli(GorevId,GorevliId) " +
                "VALUES(@GorevId,@GorevliId)";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;


            SqlParameter prm1 = new SqlParameter("@GorevId", System.Data.SqlDbType.Int);
            prm1.Value = GorevId;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@GorevliId", System.Data.SqlDbType.VarChar);
            prm2.Value = GorevliId;
            cmd.Parameters.Add(prm2);

           

            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            baglan.Close();


            return islem;
        }


        public static List<Gorevler> AktifKullanicilarListesiniGetir()
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Kullanicilar where Onay=1 And Aktif=1";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gorevler gorev = new Gorevler();
                gorev.KullaniciId = dr["KullaniciId"] is DBNull ? 0 : Convert.ToInt32(dr["KullaniciId"]);
                gorev.KullaniciAdi = dr["KullaniciAdi"] is DBNull ? string.Empty : dr["KullaniciAdi"].ToString();
                gorev.Sifre = dr["Sifre"] is DBNull ? string.Empty : dr["Sifre"].ToString();
                gorev.Yonetici = dr["Yonetici"] is DBNull ? 0 : Convert.ToInt32(dr["Yonetici"]);
                gorev.Mail = dr["Mail"] is DBNull ? string.Empty : dr["Mail"].ToString();
                gorev.AdSoyad = dr["AdSoyad"] is DBNull ? string.Empty : dr["AdSoyad"].ToString();
                gorev.Onay = dr["Onay"] is DBNull ? 0 : Convert.ToInt32(dr["Onay"]);

                list.Add(gorev);
            }
            baglan.Close();



            return list;
        }

       
        public static int GorevliIdGetir(string kullaniciAd)
        {
            kullaniciAd = GorevlerVT.KullaniciAdi1;
            int sonuc = 0;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT KullaniciId FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm.Value = kullaniciAd;
            cmd.Parameters.Add(prm);

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int ad = dr["KullaniciId"] is DBNull ? 0 : Convert.ToInt32(dr["KullaniciId"]);

                sonuc = ad;
            }
            
            baglan.Close();

            return sonuc;
        }


        public static string EkleyenKullaniciGetirme(int GorevId)
        {
            string sonuc = "";


            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"Select K.KullaniciAdi from Gorevler G 
left join Kullanicilar K on G.EkleyenKullaniciId = K.KullaniciId
where G.Id = @GorevId ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@GorevId",System.Data.SqlDbType.Int);
            prm.Value = GorevId;
            cmd.Parameters.Add(prm);

            baglan.Open();

            sonuc = cmd.ExecuteScalar().ToString();

            baglan.Close();


            return sonuc;
        }

        public static List<Gorevler> GorevlileriGetir(int GorevId)
        {
            List<Gorevler> list = new List<Gorevler>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select KullaniciId, KullaniciAdi from Kullanicilar
where  KullaniciId = Any (
	select GG.GorevliId from Gorevler_Gorevli GG 
	left join Gorevler G on G.Id = GG.GorevId
	where GG.GorevId = @GorevId 
); ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@GorevId", System.Data.SqlDbType.Int);
            prm.Value = GorevId;
            cmd.Parameters.Add(prm);


            baglan.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               Gorevler gorev = new Gorevler();
                gorev.KullaniciAdi = dr["KullaniciAdi"] is DBNull ? string.Empty : dr["KullaniciAdi"].ToString();
                gorev.KullaniciId = dr["KullaniciId"] is DBNull ? 0 : Convert.ToInt32(dr["KullaniciId"]);

                list.Add(gorev);
            }

            baglan.Close();

            return list;
        }

        public static string KullaniciAdi1 { get; set; }

        public static bool GorevlendirenGorevliDegistirebilirMi(int GorevId, string KullaniciAdi)
        {
            bool islem;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select COUNT(K.KullaniciAdi) from Gorevler G " +
"left join Kullanicilar K on G.EkleyenKullaniciId = K.KullaniciId " +
"where G.Id = @GorevId " +
"and K.KullaniciAdi = @KullaniciAdi ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;


            SqlParameter prm1 = new SqlParameter("@GorevId", System.Data.SqlDbType.Int);
            prm1.Value = GorevId;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm2.Value = KullaniciAdi;
            cmd.Parameters.Add(prm2);



            baglan.Open();
            int islemsayisi = Convert.ToInt32(cmd.ExecuteScalar());
            if (islemsayisi > 0) { islem = true; } else { islem = false; }
            baglan.Close();


            return islem;
        }


        public static bool Gorevler_GorevliSil(int GorevId)
        {
            bool islem = false;

            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Delete from Gorevler_Gorevli where GorevId = @GorevId ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = connection;

            SqlParameter prm = new SqlParameter("@GorevId", System.Data.SqlDbType.Int);
            prm.Value = GorevId;
            cmd.Parameters.Add(prm);



            connection.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }

            connection.Close();


            return islem;

        }


        public static bool SifreDegistir(string YeniSifre, string EskiSifre, string KullaniciAdi)
        {
            bool islem= true;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update Kullanicilar set Sifre = @YeniSifre where KullaniciAdi = @KullaniciAdi and Sifre = @EskiSifre ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;


            SqlParameter prm = new SqlParameter("@YeniSifre", System.Data.SqlDbType.VarChar);
            prm.Value = YeniSifre;
            cmd.Parameters.Add(prm);

            SqlParameter prm2 = new SqlParameter("@KullaniciAdi", System.Data.SqlDbType.VarChar);
            prm2.Value = KullaniciAdi;
            cmd.Parameters.Add(prm2);

            SqlParameter prm3 = new SqlParameter("@EskiSifre",System.Data.SqlDbType.VarChar);
            prm3.Value = EskiSifre;
            cmd.Parameters.Add(prm3);


            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }
            baglan.Close();


            return islem;
        }




    }
}
