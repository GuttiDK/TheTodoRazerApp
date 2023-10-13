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

        [BindProperty]
        public ToDoItemDto ToDoItems { get; set; }

        public EditToDoListModel(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        public async Task OnGetAsync(Guid id)
        {
            ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(id);

            if (toDoItemDto != null)
            {
                ToDoItems = new()
                {
                    Id = toDoItemDto.Id,
                    TaskDescription = toDoItemDto.TaskDescription,
                    CreatedTime = toDoItemDto.CreatedTime,
                    FinishedTime = toDoItemDto.FinishedTime,
                    IsCompleted = toDoItemDto.IsCompleted,
                    Priority = toDoItemDto.Priority
                };

            }
        }

        public async Task<IActionResult> OnPostUpdateTask()
        {

            ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(ToDoItems.Id);

            if (toDoItemDto != null)
            {
                
                toDoItemDto.Id = ToDoItems.Id;
                toDoItemDto.TaskDescription = ToDoItems.TaskDescription;
                toDoItemDto.CreatedTime = ToDoItems.CreatedTime;
                toDoItemDto.Priority = ToDoItems.Priority;

                if (toDoItemDto.IsCompleted == false && ToDoItems.IsCompleted == true)
                {
                    toDoItemDto.IsCompleted = true;
                    toDoItemDto.FinishedTime = DateTime.Now;
                }
                else if (toDoItemDto.IsCompleted == true && ToDoItems.IsCompleted == false)
                {
                    toDoItemDto.IsCompleted = false;
                    toDoItemDto.FinishedTime = null;
                }
                else if (toDoItemDto.IsCompleted == true && ToDoItems.IsCompleted == true)
                {
                    toDoItemDto.IsCompleted = ToDoItems.IsCompleted;
                    toDoItemDto.FinishedTime = ToDoItems.FinishedTime;
                }

                await _toDoItemService.UpdateAsync(toDoItemDto);
            }

            return Page();
        }

    }
}
