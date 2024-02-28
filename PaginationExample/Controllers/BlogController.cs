using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginationExample.Models;

namespace PaginationExample.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [ActionName("Index")]
        public async Task<IActionResult> List(int pageNo = 1, int pageSize = 3)
        {
            var query = _appDbContext.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.Blog_Id);

            var lst = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var rowCount = await query.CountAsync();
            var pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }

            BlogResponseModel model = new()
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, rowCount, "/Blog/Index")
            };

            return View(model);
        }
    }
}
