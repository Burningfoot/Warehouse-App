using ReactiveUI;
using SqliteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerApp.Models
{
    public class ReactivePackage : Package, IReactiveObject
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        public void RaisePropertyChanging(System.ComponentModel.PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }
    }
}
