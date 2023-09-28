using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheTodoService.DataTransferObjects;
using TheTodoService.Interfaces;

namespace TheTodoWeb.Pages
{
    public class EditToDoListModel : PageModel
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IToDoItemService _toDoItemService;

        public EditToDoListModel(LinkGenerator linkGenerator, IToDoItemService toDoItemService)
        {
            this.linkGenerator = linkGenerator;
            _toDoItemService = toDoItemService;
        }

        public async Task OnGetAsync(Guid guid)
        {
            string guid1 = linkGenerator.GetPathByRouteValues(HttpContext, "EditToDoList", new { guid });

            if (guid != null)
            {
                ToDoItemDto toDoItemDto = await _toDoItemService.GetByIDAsync(guid);
                if (toDoItemDto != null)
                {
                }
            }

        }


    }
}
