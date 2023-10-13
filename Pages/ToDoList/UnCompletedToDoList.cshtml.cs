using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TheTodoRepository.Enums;
using TheTodoService.DataTransferObjects;
using TheTodoService.Enums;
using TheTodoService.Interfaces;

namespace TheTodoWeb.Pages
{
    public class UnCompletedToDoListModel : PageModel
    {
        private readonly IToDoItemService _toDoItemService;


        [BindProperty]
        public string? Description { get; set; }
        [BindProperty]
        public PrioryEnum PriorityForm { get; set; }
        [BindProperty]
        public ObservableCollection<ToDoItemDto>? ToDoItems { get; set;}


        public UnCompletedToDoListModel(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        public async Task<IActionResult> OnGet()
        {
            ToDoItems = await _toDoItemService.GetAllUncompletedAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCompletedTask(Guid id)
        {
            if (ModelState.IsValid)
            {
                ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(id);

                if (toDoItemDto != null)
                {
                    toDoItemDto.IsCompleted = true;
                    toDoItemDto.FinishedTime = DateTime.Now;

                    await _toDoItemService.UpdateAsync(toDoItemDto);
                }
            }

            return RedirectToPage("/ToDoList/UnCompletedToDoList");

        }

        public async Task<IActionResult> OnPostCreateTask()
        {
            if (ModelState.IsValid)
            {
                if (Description != null)
                {
                    ToDoItemDto toDoItemDto = new()
                    {
                        Id = Guid.NewGuid(),
                        TaskDescription = Description,
                        CreatedTime = DateTime.UtcNow,
                        FinishedTime = null,
                        IsCompleted = false,
                        Priority = PriorityForm
                    };

                    await _toDoItemService.CreateAsync(toDoItemDto);
                }
            }

            return RedirectToPage("/ToDoList/UnCompletedToDoList");
        }
    }
}
