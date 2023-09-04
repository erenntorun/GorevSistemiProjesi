using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjeGörev
{
    public class Connection
    {

        public static string ConnectionString
        {

            get
            {
                return @"Data Source=DESKTOP-H1D7222;Initial Catalog=ProjeGörev;Integrated Security=True";
            }
        }
    }
}
