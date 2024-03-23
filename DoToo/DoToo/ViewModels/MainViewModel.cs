using DoToo.Models;
using DoToo.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using DoToo.Views;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    //first view displayed to the user. Displays a list of to-do items
    public class MainViewModel : ViewModel
    {
        private readonly TodoItemRepository repository;

        //Property for the to-do list items
        public ObservableCollection<TodoItemViewModel> Items { get; set; }

        //all dependencies inherited frfom ViewModel are passed in the constructor
        public MainViewModel(TodoItemRepository repository)
        {
            //hook up some events from the repository to know when data changes
            //When an item is added to the repository, no matter who added it, MainView will add it to the items list.
            //Since the items collection is an observable collection, the list updates.If an item is updated, we simply reload the list
            repository.OnItemAdded += (sender, item) => Items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            //async call to LoadData() as an entry point to initialise ViewModel
            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        //When the user taps this button, we want it to take them to ItemView
        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<ItemView>();
            await Navigation.PushAsync(itemView);
        });

        private async Task LoadData()
        {
            var items = await repository.GetItems();

            //If ShowAll property is false, we limit the content of the list to the items that have not been completed
            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }
            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));
            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
        }
        
      private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
        {
            var itemViewModel = new TodoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        //a stub that is called when we change the status of the to-do list item from active to completed and vice versa.
        private void ItemStatusChanged(object sender, EventArgs e)
        {
            //filter to toggle between viewing active items only and all the items.
            if (sender is TodoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }

                Task.Run(async () => await repository.UpdateItem(item.Item));
            }
        }
        public bool ShowAll { get; set; }

        //Takes us to the selected item details page when clicked. Calls NavigateToItem()
        //Bind it to the ListView control in the view
        public TodoItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async Task NavigateToItem(TodoItemViewModel item)
        {
            if (item == null)
            {
                return;
            }

            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;

            await Navigation.PushAsync(itemView);
        }

        //read-only property used to display the status of the filter toggle as a string in a human-readable form
        public string FilterText => ShowAll ? "All" : "Active";

        //code that toggles the filter
        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });
    }
}
