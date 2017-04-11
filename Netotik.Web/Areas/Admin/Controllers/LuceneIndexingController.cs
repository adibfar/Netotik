using Netotik.ViewModels;
using Netotik.Services.Abstract;
using Netotik.Web.Extension;
using Netotik.Web.Infrastructure;
using Netotik.Web.Lucene;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [RoutePrefix("LuceneIndexing")]
    [Authorize]
    public partial class LuceneIndexingController : BaseController
    {
        private readonly IProductService _productService;
        public LuceneIndexingController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("ReIndex")]
        public virtual ActionResult ReIndex()
        {
            LuceneIndex.ClearLuceneIndex();


            foreach (var product in _productService.All().Where(x => !x.IsDeleted && x.IsPublished).ToList())
            {
                LuceneIndex.ClearLuceneIndexRecord(product.Id);
                LuceneIndex.AddUpdateLuceneIndex(new ProductSearchModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImageName = product.PictureId.HasValue ? Path.Combine(FilePathes._imagesShopProductPath, product.Picture.FileName) : "",
                    Description = product.ShortDescription,
                });
            }

            return Content("ReIndexing Complete.");
        }
    }
}