using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KosovRPM18
{
    public partial class AccountingEntites : DbContext
    {
        private static AccountingEntites context;

        public static AccountingEntites GetContext()
        {
            if (context == null)
                context = new AccountingEntites();
            return context;
        }
    }
}
