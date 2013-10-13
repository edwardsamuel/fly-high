using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MySql.Web.Security;
using MySql.Data.MySqlClient;

namespace FlyHigh.Models
{
	public class SimpleMembershipTestDbContext : MySqlSecurityDbContext
	{
		// public non argument constructor for MySqlSimpleMembershipProvider
		public SimpleMembershipTestDbContext()
            : base("ErlanggaMysql")
		{
		}

		public static SimpleMembershipTestDbContext CreateContext()
		{
			return new SimpleMembershipTestDbContext();
		}
	}
}
