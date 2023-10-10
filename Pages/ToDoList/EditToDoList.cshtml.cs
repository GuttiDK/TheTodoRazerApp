using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using TheTodoRepository.Models;
using TheTodoService.DataTransferObjects;
using TheTodoService.Interfaces;

namespace TheTodoWeb.Pages
{
    public class EditToDoListModel : PageModel
    {
        private readonly IToDoItemService _toDoItemService;

        public ObservableCollection<ToDoItemDto> ToDoItems = new();

        [BindProperty]
        public ToDoItemDto? TodoItem { get; set; }

        public EditToDoListModel(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        public async Task OnGetAsync(Guid id)
        {
            ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(id);

            if (toDoItemDto != null)
            {
                TodoItem = new ToDoItemDto()
                {
                    Id = toDoItemDto.Id,
                    TaskDescription = toDoItemDto.TaskDescription,
                    CreatedTime = toDoItemDto.CreatedTime,
                    IsCompleted = toDoItemDto.IsCompleted,
                    Priority = toDoItemDto.Priority
                };

            }
        }

        public async Task<IActionResult> OnPostEditTaskAsync()
        {
            if (TodoItem != null)
            {
                ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(TodoItem.Id);

                if (toDoItemDto != null)
                {
                    toDoItemDto.TaskDescription = TodoItem.TaskDescription;
                    toDoItemDto.CreatedTime = TodoItem.CreatedTime;
                    toDoItemDto.IsCompleted = TodoItem.IsCompleted;
                    toDoItemDto.Priority = TodoItem.Priority;

                    await _toDoItemService.UpdateAsync(TodoItem);
                }
            }

            return RedirectToPage("ToDoList");
        }

    }
}
