using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITodoRepository
    {
        Task AddAsync(TodoItem item);
        void Update(TodoItem item);
        void Delete(TodoItem item);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(Guid guid);

    }
}
