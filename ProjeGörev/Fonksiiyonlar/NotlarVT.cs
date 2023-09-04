using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeGörev
{
    public class NotlarVT
    {

        public static List<Notlar> NotlarListesiniGetir()
        {
            List<Notlar> list = new List<Notlar>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Notlar";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Notlar not = new Notlar();
                not.GorevId = dr["GorevId"] is DBNull ? 0 : Convert.ToInt32(dr["GorevId"]);
                not.NotId = dr["NotId"] is DBNull ? 0 : Convert.ToInt32(dr["NotId"]);
                not.EklenenNot = dr["EklenenNot"] is DBNull ? string.Empty : dr["EklenenNot"].ToString();
                not.EkleyenKullanici = dr["EkleyenKullanici"] is DBNull ? string.Empty : dr["EkleyenKullanici"].ToString();
                not.EklenenTarih = dr["EklenenTarih"] is DBNull ? new DateTime(2000, 1, 1) : Convert.ToDateTime(dr["EklenenTarih"]);
                
                
                list.Add(not);
            }
            baglan.Close();


            return list;
        }


        public static List<Notlar> NotlarListesiniGetir(int GorevId)
        {
            List<Notlar> list = new List<Notlar>();

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Notlar where GorevId=@GorevId";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;

            SqlParameter prm = new SqlParameter("@GorevId",System.Data.SqlDbType.Int);
            prm.Value = GorevId;
            cmd.Parameters.Add(prm);

            baglan.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Notlar not = new Notlar();
                not.GorevId = dr["GorevId"] is DBNull ? 0 : Convert.ToInt32(dr["GorevId"]);
                not.NotId = dr["NotId"] is DBNull ? 0 : Convert.ToInt32(dr["NotId"]);
                not.EklenenNot = dr["EklenenNot"] is DBNull ? string.Empty : dr["EklenenNot"].ToString();
                not.EkleyenKullanici = dr["EkleyenKullanici"] is DBNull ? string.Empty : dr["EkleyenKullanici"].ToString();
                not.EklenenTarih = dr["EklenenTarih"] is DBNull ? new DateTime(2000, 1, 1) : Convert.ToDateTime(dr["EklenenTarih"]);


                list.Add(not);
            }
            baglan.Close();


            return list;
        }


        public static bool NotEkle(Notlar not)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Notlar(GorevId,EklenenNot,EkleyenKullanici,EklenenTarih) " +
                "VALUES(@GorevId,@EklenenNot,@EkleyenKullanici,@EklenenTarih)";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;


            SqlParameter prm1 = new SqlParameter("@GorevId", System.Data.SqlDbType.Int);
            prm1.Value = not.GorevId;
            cmd.Parameters.Add(prm1);

            SqlParameter prm2 = new SqlParameter("@EklenenNot", System.Data.SqlDbType.VarChar);
            prm2.Value = not.EklenenNot;
            cmd.Parameters.Add(prm2);

            SqlParameter prm3 = new SqlParameter("@EkleyenKullanici", System.Data.SqlDbType.VarChar);
            prm3.Value = not.EkleyenKullanici;
            cmd.Parameters.Add(prm3);

            SqlParameter prm4 = new SqlParameter("@EklenenTarih", System.Data.SqlDbType.DateTime);
            prm4.Value = not.EklenenTarih;
            cmd.Parameters.Add(prm4);


            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }
            baglan.Close();

            return islem;

        }


        public static bool NotSil(Notlar not)
        {
            bool islem = false;

            SqlConnection baglan = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Delete from Notlar where NotId = @NotId";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = baglan;


            SqlParameter prm = new SqlParameter("@NotId", System.Data.SqlDbType.Int);
            prm.Value = not.NotId;
            cmd.Parameters.Add(prm);



            baglan.Open();
            int islemsayisi = cmd.ExecuteNonQuery();
            if (islemsayisi > 0) { islem = true; } else { islem = false; }
            baglan.Close();

            return islem;

        }







    }
}
