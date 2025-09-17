using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _items = new();
        private int _nextId = 1;
        public Task AddAsync(TodoItem item)
        {
            _nextId++;
            item.Guid = new Guid();
            item.CreatedAt = DateTime.UtcNow;
            _items.Add(item);
            return Task.CompletedTask;
        }

        public void Delete(TodoItem item)
        {
            _items.RemoveAll(x => x.Guid == item.Guid);
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return Task.FromResult(_items.AsEnumerable());
        }

        public Task<TodoItem?> GetByIdAsync(Guid guid)
        {
            var item = _items.FirstOrDefault(x => x.Guid == guid);
            return Task.FromResult(item);
        }

        public void Update(TodoItem item)
        {
            var existingItem = _items.FirstOrDefault(i => i.Guid == item.Guid);
            if (existingItem != null)
            {
                existingItem.Title = item.Title;
                existingItem.Description = item.Description;
                existingItem.IsCompleted = item.IsCompleted;
            }
        }
    }
}
