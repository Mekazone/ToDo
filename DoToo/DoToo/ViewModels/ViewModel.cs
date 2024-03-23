using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    //a base class for all ViewModel objects. It is not meant to be instantiated on its own, so we mark it as abstract.
    public abstract class ViewModel : INotifyPropertyChanged
    {
        //PropertyChanged event must be raised whenever we want the GUI to be aware of any changes to a property
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //Helps us with navigation so we are not tied to Xamarin forms. Code can be reused for native
        public INavigation Navigation { get; set; }
    }
}
