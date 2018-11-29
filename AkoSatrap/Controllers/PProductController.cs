using AkoSatrap.UIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace AkoSatrap.Controllers
{
    public class PProductController : Controller
    {
        // GET: PProduct
        [HttpPost]
        public JsonResult AddProductCategory(ViewModel.ProductCategoty productCategory)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.AddProductCategory(productCategory);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult UpdateProductCategory(ViewModel.ProductCategoty productCategory)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.UpdateProductCategory(productCategory);
            return Json(returnResult);
        }

        [HttpGet]
        public JsonResult GetProductCategoryList()
        {
            int pageNumber = Convert.ToInt32(Request.QueryString["page"]);
            int pageSize = Convert.ToInt32(Request.QueryString["pageSize"]);

            GridResult<ViewModel.ProductCategoty> gridResult = new GridResult<ViewModel.ProductCategoty>();
            var allCategory = new Business.ProductBusiness().GetAllProductCategory();

            gridResult.Data = allCategory.OrderByDescending(r => r.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            gridResult.Total = allCategory.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductCategoryListForDropDown()
        {

            var allCategory = new Business.ProductBusiness().GetAllProductCategory();

            return Json(allCategory, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProduct(ViewModel.Product product)
        {
            var business = new Business.ProductBusiness();
            var returnResult = business.AddProduct(product);
            return Json(returnResult);
        }

        [HttpGet]
        public JsonResult GetProductList()
        {
            int pageNumber = Convert.ToInt32(Request.QueryString["page"]);
            int pageSize = Convert.ToInt32(Request.QueryString["pageSize"]);

            GridResult<ViewModel.Product> gridResult = new GridResult<ViewModel.Product>();
            var allProduct = new Business.ProductBusiness().GetAllProduct();

            gridResult.Data = allProduct.OrderByDescending(r => r.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            gridResult.Total = allProduct.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProduct(ViewModel.Product product)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.UpdateProduct(product);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult AddImage(FileAttachment fileAttachment, int id)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();
            Business.ProductBusiness business = new Business.ProductBusiness();
            var product = business.GetProductById(id);
            var path = Server.MapPath($"~/AkoSatrapImages/{product.ImageFolderName}/");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            fileAttachment.Attachment.SaveAs($"{path}/{Guid.NewGuid()}.{fileAttachment.Attachment.FileName.Split('.')[1].ToString()}");

            return Json(returnResult);

        }

        [HttpPost]
        public JsonResult DeleteImage(string imageFolderName, string fileName)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();

            System.IO.File.Delete(Server.MapPath($"~/AkoSatrapImages/{imageFolderName}/{fileName}"));

            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult GetProductImages(string imageFolderName)
        {
            ViewModel.ReturnResult<List<string>> returnResult = new ViewModel.ReturnResult<List<string>>();
            var path = Server.MapPath($"~/AkoSatrapImages/{imageFolderName}/");
            if (Directory.Exists(path))
            {
                returnResult.Data = Directory.GetFiles(path).Select(r => Path.GetFileName(r)).ToList();
            }
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult GetProductListForDropDown()
        {

            var allProduct = new Business.ProductBusiness().GetAllProduct();

            return Json(allProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductDetail()
        {
            int pageNumber = Convert.ToInt32(Request.QueryString["page"]);
            int pageSize = Convert.ToInt32(Request.QueryString["pageSize"]);
            var productId = Convert.ToInt32(Request.QueryString["productId"]);

            GridResult<ViewModel.ProductFeature> gridResult = new GridResult<ViewModel.ProductFeature>();
            var allDetail = new Business.ProductBusiness().GetAllProductDetail(productId);

            gridResult.Data = allDetail.OrderByDescending(r => r.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            gridResult.Total = allDetail.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProductFeature(ViewModel.ProductFeature productFeature)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.AddProductDetail(productFeature);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult UpdateProductDetail(ViewModel.ProductFeature productFeature)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.UpdateProductFeature(productFeature);
            return Json(returnResult);
        }


    }
}