using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.WebApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}