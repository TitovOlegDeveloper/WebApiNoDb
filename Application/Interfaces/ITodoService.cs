using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDto>> GetAllItemAsync();
        Task<TodoItemDto?> GetItemByIdAsync(Guid guid);
        Task<TodoItemDto> CreateItemAsync(TodoItemDto itemDto);
        Task UpdateItemAsync(TodoItemDto itemDto);
        Task DeleteItemAsync(Guid guid);
    }
}
