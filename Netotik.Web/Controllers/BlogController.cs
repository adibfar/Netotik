using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Netotik.Web.Infrastructure;
using Netotik.Services.Abstract;
using Netotik.Services.Identity;
using Netotik.Data;
using Netotik.ViewModels.CMS.Content;
using Netotik.ViewModels.CMS.Comment;
using Netotik.Domain.Entity;

namespace Netotik.Web.Controllers
{
    [RoutePrefix("Blog")]
    public partial class BlogController : BaseController
    {
        private readonly IContentService _contentService;
        private readonly IContentTagService _contentTagService;
        private readonly IContentCategoryService _contentCategoryService;
        private readonly IContentCommentService _contentCommentService;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IUnitOfWork _uow;

        public BlogController(
            IContentService contentService,
            IContentTagService contentTagService,
            IContentCategoryService contentCategoryService,
            IContentCommentService contentCommentService,
            IApplicationUserManager applicationUserManager,
            IUnitOfWork uow)
        {
            _contentTagService = contentTagService;
            _contentCategoryService = contentCategoryService;
            _contentCommentService = contentCommentService;
            _contentService = contentService;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
        }


        [Route("category/{categoryId=0}/page/{page=1}/count/{count=8}/tag/{tagId?}")]
        public virtual async Task<ActionResult> Index(int? categoryId, int? tagId, int page = 1, int count = 8)
        {
            categoryId = categoryId == 0 ? null : categoryId;
            int total;

            string tag = "";
            string category = "";
            if (tagId.HasValue)
            {
                var tag_ = await _contentTagService.SingleOrDefaultAsync(tagId.Value);
                if (tag_ != null)
                {
                    tag = tag_.Name;
                }
            }

            if (categoryId.HasValue)
            {
                var cat_ = await _contentCategoryService.SingleOrDefaultAsync(categoryId.Value);
                if (cat_ != null)
                {
                    category = cat_.Name;
                }
            }

            var result = _contentService.GetForPublicView(out total, page, count, categoryId, tagId);
            var model = new PublicTableContentModel
            {
                CategoryId = categoryId,
                Category = category,
                Tag = tag,
                TagId = tagId,
                Total = total,
                Page = page,
                Count = count,
                Contents = result
            };
            return View(model);
        }


        public virtual async Task<ActionResult> Detail(int? id)
        {

            if (!id.HasValue) return HttpNotFound();

            var content = await _contentService.SingleOrDefaultAsync(id.Value);

            #region Add View
            if (Request.Cookies["ViewedPost"] != null)
            {
                if (Request.Cookies["ViewedPost"][string.Format("pId_{0}", id.Value)] == null)
                {
                    HttpCookie cookie = (HttpCookie)Request.Cookies["ViewedPost"];
                    cookie[string.Format("pId_{0}", id.Value)] = id.Value.ToString();
                    cookie.Expires = DateTime.Now.AddHours(4);
                    Response.Cookies.Add(cookie);
                    content.CountView++;
                    await _uow.SaveChangesAsync();
                }
            }
            else
            {
                HttpCookie cookie = new HttpCookie("ViewedPost");
                cookie[string.Format("pId_{0}", id.Value)] = id.Value.ToString();
                cookie.Expires = DateTime.Now.AddHours(4);
                Response.Cookies.Add(cookie);
                content.CountView++;
                await _uow.SaveChangesAsync();
            }
            #endregion
            

            if (content == null)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (content.AllowViewComments)
            {
                ViewBag.Comments = content.ContentComments.Where(x => x.Status == Netotik.Domain.Entity.CommentStatus.Accepted).ToList();
            }



            return View(content);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AddComment(AddCommentModel model)
        {
            if (ModelState.IsValid)
            {
                var content = await _contentService.SingleOrDefaultAsync(model.ContentId);
                if (content == null || !content.AllowComments) return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

                ContentComment entity = new ContentComment
                {
                    Status = CommentStatus.WaitForAccept,
                    CreateDate = DateTime.Now,
                    Text = model.Text,
                    Name = model.Name,
                    Email = model.Email,
                    ContentId = model.ContentId,
                };

                if (model.CommentId.HasValue)
                {
                    var comment = await _contentCommentService.SingleOrDefaultAsync(model.CommentId.Value);
                    if (comment != null) entity.Comments.Add(comment);
                }
                _contentCommentService.Add(entity);
                await _uow.SaveChangesAsync();

                if (content.AllowViewComments) ViewBag.Comments = content.ContentComments.Where(x => x.Status == CommentStatus.Accepted).ToList();

                ModelState.Clear();
                return RedirectToAction(MVC.Blog.ActionNames.Detail, new { Id = model.ContentId });
            }

            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }




        public virtual ActionResult SideBarCategory()
        {
            var list = _contentCategoryService.All().Where(x => !x.IsDeleted).ToList();
            return View(MVC.Blog.Views._Category, list);
        }

        public virtual ActionResult SideBarTag()
        {
            var list = _contentTagService.All().ToList();
            return View(MVC.Blog.Views._Tags, list);
        }


        public virtual ActionResult SideBarPopularPost()
        {
            return View(MVC.Blog.Views._PopularPosts, _contentService.GetLastPopular(5));
        }

    }
}