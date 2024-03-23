using DoToo.Models;
using DoToo.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    //used to create new items and to edit existing items
    public class ItemViewModel : ViewModel
    {
        private TodoItemRepository repository;

        //The Item property holds a reference to the current item that we want to add or edit
        public TodoItem Item { get; set; }

        public ItemViewModel(TodoItemRepository repository)
        {
            this.repository = repository;
            //assign a due date
            Item = new TodoItem() { Due = DateTime.Now.AddDays(1) };
        }
        //save to db and go back
        public ICommand Save => new Command(async () =>
        {
            await repository.AddOrUpdate(Item);
            await Navigation.PopAsync();
        });
    }
}
