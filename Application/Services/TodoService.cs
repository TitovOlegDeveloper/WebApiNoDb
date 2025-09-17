using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoItemDto> CreateItemAsync(TodoItemDto itemDto)
        {
            var item = new TodoItem
            {
                Title = itemDto.Title,
                Description = itemDto.Description,
                IsCompleted = itemDto.IsCompleted
            };

            await _todoRepository.AddAsync(item);

            itemDto.Guid = item.Guid;
            itemDto.CreatedAt = item.CreatedAt;
            return itemDto;
        }

        public async Task DeleteItemAsync(Guid guid)
        {
            var item = await _todoRepository.GetByIdAsync(guid);
            if(item != null)
            {
                throw new KeyNotFoundException($"Todo item with id {guid} not found.");
            }

            _todoRepository.Delete(item);
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllItemAsync()
        {
            var items = await _todoRepository.GetAllAsync();
            return items.Select(MapToDto);
        }

        public async Task<TodoItemDto?> GetItemByIdAsync(Guid guid)
        {
            var item = await _todoRepository.GetByIdAsync(guid);
            return item == null ? null : MapToDto(item);
        }

        public async Task UpdateItemAsync(TodoItemDto itemDto)
        {
            // 1. Получаем существующую сущность из репозитория
            var existingItem = await _todoRepository.GetByIdAsync(itemDto.Guid);

            // 2. Проверяем, существует ли элемент
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"Todo item with id {itemDto.Guid} not found.");
            }

            // 3. Обновляем поля сущности
            existingItem.Title = itemDto.Title;
            existingItem.Description = itemDto.Description;
            existingItem.IsCompleted = itemDto.IsCompleted;

            // 4. Сохраняем изменения через репозиторий
            _todoRepository.Update(existingItem);
        }

        private TodoItemDto MapToDto(TodoItem item)
        {
            return new TodoItemDto
            {
                Guid = item.Guid,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                CreatedAt = item.CreatedAt
            };
        }
    }
}
