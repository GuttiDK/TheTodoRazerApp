using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using TheTodoRepository.Models;
using TheTodoService.DataTransferObjects;
using TheTodoService.Interfaces;

namespace TheTodoWeb.Pages
{
    public class DeleteToDoListModel : PageModel
    {

        private readonly IToDoItemService _toDoItemService;

        public ObservableCollection<ToDoItemDto> ToDoItems = new();

        public DeleteToDoListModel(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        public async Task<IActionResult> OnGet()
        {
            ToDoItems = await _toDoItemService.GetAllCompletedAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            if (ModelState.IsValid)
            {
                ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(id);

                if (toDoItemDto != null)
                {
                    await _toDoItemService.DeleteAsync(toDoItemDto);
                }
            }

            return RedirectToPage("DeleteToDoList");
        }
    }
}
