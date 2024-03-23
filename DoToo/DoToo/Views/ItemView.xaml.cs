using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoToo.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoToo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemView : ContentPage
    {
        //Bind the ViewModel to the View (in the constructor)
        public ItemView(ItemViewModel viewmodel)
        {
            InitializeComponent();
            viewmodel.Navigation = Navigation;
            BindingContext = viewmodel;
        }
    }
}