using ABC.Database.ObjectContexts;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.ObjectRepository
{
    public class AgencyRepository
    {
        public static List<Agency> All
        {
            get
            {
                var list = new List<Agency>();
                using (var context = new MyDatabaseContext())
                {
                    list = context.Agencies.ToList();
                }
                return list;
            }
        }

        public static void Add(Agency which)
        {
            using (var context = new MyDatabaseContext())
            {
                context.Agencies.Add(which);
                context.SaveChanges();
            }
        }
    }
}
