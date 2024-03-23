using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

namespace DoToo.Repositories
{
    //inherits the ITodoItemRepository interface
    public class TodoItemRepository : ITodoItemRepository
    {
        private SQLiteAsyncConnection connection;

        //If a SQLite connection to the db doesn't exist, create a new one, create a table and insert default data
        private async Task CreateConnection()
        {
            if (connection != null)
            {
                return;
            }

            var documentPath = Environment.GetFolderPath(
                               Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "TodoItems.db");

            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<TodoItem>();

            if (await connection.Table<TodoItem>().CountAsync() == 0)
            {
                await connection.InsertAsync(new TodoItem() { Title = "Welcome to DoToo" });
            }
        }

        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;

        //CRUD operation codes
        public async Task<List<TodoItem>> GetItems()
        {
            await CreateConnection();
            return await connection.Table<TodoItem>().ToListAsync();
        }
        public async Task AddItem(TodoItem item)
        {
            await CreateConnection();
            await connection.InsertAsync(item);
            //OnItemAdded event to notify any subscribers after item has been added
            OnItemAdded?.Invoke(this, item);
        }

        public async Task UpdateItem(TodoItem item)
        {
            await CreateConnection();
            await connection.UpdateAsync(item);
            //OnItemUpdated event to notify any subscribers after item has been updated
            OnItemUpdated?.Invoke(this, item);
        }
        public async Task AddOrUpdate(TodoItem item)
        {
            if (item.Id == 0)
            {
                await AddItem(item);
            }
            else
            {
                await UpdateItem(item);
            }
        }
    }
}
