using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using TheTodoRepository.Enums;
using TheTodoService.DataTransferObjects;
using TheTodoService.Interfaces;

namespace TheTodoWeb.Pages.ToDoList
{
    public class CompletedToDoListModel : PageModel
    {
        private readonly IToDoItemService _toDoItemService;

        [BindProperty]
        public ObservableCollection<ToDoItemDto>? ToDoItems { get; set; }


        public CompletedToDoListModel(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        public async Task<IActionResult> OnGet()
        {
            ToDoItems = await _toDoItemService.GetAllCompletedAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCompletedTask(Guid id)
        {
            if (ModelState.IsValid)
            {
                ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(id);

                if (toDoItemDto != null)
                {
                    toDoItemDto.IsCompleted = false;
                    toDoItemDto.FinishedTime = null;

                    await _toDoItemService.UpdateAsync(toDoItemDto);
                }
            }
            return RedirectToPage("/ToDoList/CompletedToDoList");
        }

        public async Task<IActionResult> OnPostDeleteTask(Guid id)
        {
            if (ModelState.IsValid)
            {
                ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(id);

                if (toDoItemDto != null)
                {
                    await _toDoItemService.DeleteAsync(toDoItemDto);
                }
            }
            return RedirectToPage("/ToDoList/CompletedToDoList");
        }
    }
}
