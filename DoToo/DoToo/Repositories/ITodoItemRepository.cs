using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DoToo.Models;

namespace DoToo.Repositories
{
    //interface for accessing the model, to be accessed by the class in Repositories folder
    public interface ITodoItemRepository
    {
        event EventHandler<TodoItem> OnItemAdded;
        event EventHandler<TodoItem> OnItemUpdated;

        Task<List<TodoItem>> GetItems();
        Task AddItem(TodoItem item);
        Task UpdateItem(TodoItem item);
        Task AddOrUpdate(TodoItem item);
    }
}
