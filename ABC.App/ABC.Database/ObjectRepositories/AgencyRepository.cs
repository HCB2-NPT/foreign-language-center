using ABC.Database.ObjectContexts;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.ObjectRepositories
{
    public class AgencyRepository : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public List<Agency> All
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
    }
}
