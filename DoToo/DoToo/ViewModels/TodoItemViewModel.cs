using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    // class that represents each item in the to-do list on MainView
    public class TodoItemViewModel : ViewModel
    {
        //inject all inherited dependencies into the constructor
        public TodoItemViewModel(TodoItem item) => Item = item;

        //used to signal to the view that the state of TodoItem has changed
        public event EventHandler ItemStatusChanged;

        //Property which allows us to acces the item we passed in
        public TodoItem Item { get; private set; }

        //Property used to make the status of the to-do item human-readable in the view
        public string StatusText => Item.Completed ? "Reactivate" : "Completed";

        //toggle items between complete and active
        public ICommand ToggleCompleted => new Command((arg) =>
        {
            //inverse the current state and raise the ItemStatusChanged event so that subscribers are notified
            Item.Completed = !Item.Completed;
            ItemStatusChanged?.Invoke(this, new EventArgs());
        });

    }
}
