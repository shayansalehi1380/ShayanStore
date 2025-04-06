using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.BlogPosts;
using Domain.Entity.DiscountCodes;
using Domain.Entity.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class BlogPostController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<Wallet>>> GetAllBlogPost(string searchBlogPost, int tabs = 1)
        {

            ViewBag.selectTab = tabs;

            IQueryable<BlogPost> queryhBlogPost = unitOfWork.GenericRepository<BlogPost>()
                .TableNoTracking;

            if (!string.IsNullOrEmpty(searchBlogPost))
            {
                queryhBlogPost = queryhBlogPost.Where(x =>
                    x.Title.Contains(searchBlogPost) ||
                    x.User.Name.Contains(searchBlogPost) ||
                    x.User.Family.Contains(searchBlogPost));

            }

            ViewBag.BlogPost = await queryhBlogPost.ToListAsync();
            return RedirectToAction("ManageBlogPost", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }


        public async Task<ActionResult<BlogPost>> Create(string name, string content, int userId, DateTime CreatedDate)
        {
            await unitOfWork.GenericRepository<BlogPost>().AddAsync(new BlogPost
            {
                Title = name,
                Content = content,
                UserId = userId,
                CreatedDate = DateTime.Now,

            }, CancellationToken.None);
            return RedirectToAction("ManageBlogPost", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }


        public async Task<ActionResult<BlogPost>> Update(int id ,string name, string content, int userId)
        {
            var blogPost = await unitOfWork.GenericRepository<BlogPost>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.Title = name;
            blogPost.Content = content;
            blogPost.UserId = userId;

            await unitOfWork.GenericRepository<BlogPost>().UpdateAsync(blogPost, CancellationToken.None);
            return RedirectToAction("ManageBlogPost", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }


        public async Task<ActionResult<BlogPost>> SoftDelete(int id)
        {
            var blogPost = await unitOfWork.GenericRepository<BlogPost>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.IsDelete = true;

            await unitOfWork.GenericRepository<BlogPost>().UpdateAsync(blogPost, CancellationToken.None);
            return RedirectToAction("ManageBlogPost", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }
    }
}
