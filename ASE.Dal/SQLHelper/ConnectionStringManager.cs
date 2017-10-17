using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.DAL
{
    class ConnectionStringManager
    {

        internal static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //internal static string LoggingConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LoggingConnection"].ConnectionString;
    }
}
