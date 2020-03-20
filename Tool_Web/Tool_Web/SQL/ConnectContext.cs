using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Tool_Web.SQL
{
    public class ConnectContext : DbContext
    {
        public ConnectContext(string ConnectStr) : base(ConnectStr)
        {
        }
    }
}