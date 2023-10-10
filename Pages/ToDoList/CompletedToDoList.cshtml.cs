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

        public ObservableCollection<ToDoItemDto> ToDoItems = new();

        public CompletedToDoListModel(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        public async Task<IActionResult> OnGet()
        {
            ToDoItems = await _toDoItemService.GetAllAsync();

            return Page();
        }
    }
}
