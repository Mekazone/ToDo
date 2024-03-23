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
    public partial class MainView : ContentPage
    {
        //Bind the ViewModel to the View (in the constructor)
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

            //Reset the item selected to null so it can be selected again
            ItemsListView.ItemSelected += (s, e) => ItemsListView.SelectedItem = null;
        }
    }
}