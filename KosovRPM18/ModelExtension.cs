using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KosovRPM18
{
    public partial class AccountingEntities : DbContext
    {
        private static AccountingEntities context;

        public static AccountingEntities GetContext()
        {
            if (context == null)
                context = new AccountingEntities();
            return context;
        }
    }
}
