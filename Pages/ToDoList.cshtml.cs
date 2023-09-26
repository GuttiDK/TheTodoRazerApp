using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheTodoService.DataTransferObjects;
using TheTodoService.Interfaces;

namespace TheTodoWeb.Pages
{
    public class ToDoListModel : PageModel
    {
        private readonly IToDoItemService _toDoItemService;

        public List<ToDoItemDto> ToDoItems = new();

        public ToDoListModel(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        public async Task OnGet()
        {
            ToDoItems = await _toDoItemService.GetAllAsync();
        }
    }
}
