﻿using AkoSatrap.UIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ViewModel;
using DomainDeriven;

namespace AkoSatrap.Controllers
{
    public class PProductController : Controller
    {
        // GET: PProduct
        [HttpPost]
        public JsonResult AddProductCategory(ViewModel.ProductCategotyModel productCategory)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.AddProductCategory(productCategory);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult UpdateProductCategory(ViewModel.ProductCategotyModel productCategory)
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

            GridResult<ViewModel.ProductCategotyModel> gridResult = new GridResult<ViewModel.ProductCategotyModel>();
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
        public JsonResult AddProduct(ViewModel.ProductModel product)
        {
            var business = new Business.ProductBusiness();
            var returnResult = business.AddProduct(product);
            return Json(returnResult);
        }

        [HttpGet]
        public JsonResult GetProductList()
        {
            GridResult<ViewModel.ProductModel> gridResult = new GridResult<ViewModel.ProductModel>();
            var allProduct = new Business.ProductBusiness().GetAllProduct();

            gridResult.Data = allProduct.OrderByDescending(r => r.Id).ToList();
            gridResult.Total = allProduct.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProduct(ViewModel.ProductModel product)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.UpdateProduct(product);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult AddImage(FileAttachment fileAttachment, int id)
        {
            var returnResult = new ViewModel.ReturnResult<bool>();

            if (fileAttachment.Attachment.ContentLength > 2097152)
            {
                returnResult.SetError("حجم فایل نمیتواند بیشتر از 2 مگابایت باشد");
                return Json(returnResult);
            }

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
        public JsonResult DeleteFeature(int id)
        {
            var result = new ReturnResult<bool>();
            using (var dbContext = new AkoSatrapDb())
            {
                var feature = dbContext.ProductFeatures.Find(id);
                dbContext.ProductFeatures.Remove(feature);
                dbContext.SaveChanges();
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            var result = new ReturnResult<bool>();
            using (var dbContext = new AkoSatrapDb())
            {
                var features = dbContext.ProductFeatures.Where(x => x.ProductId == id).ToList();
                if (features.Any())
                {
                    foreach (var item in features)
                    {
                        dbContext.ProductFeatures.Remove(item);
                    }
                }
                dbContext.SaveChanges();

                var product = dbContext.Products.Find(id);
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();

                var path = Server.MapPath($"~/AkoSatrapImages/{product.ImageFolderName}/");
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            return Json(result);
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
            var allProduct = new List<ProductModel>();
            allProduct.Add(new ProductModel
            {
                Title = "",
                Id = 0
            });
            allProduct.AddRange(new Business.ProductBusiness().GetAllProduct());
            return Json(allProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductDetail(int productId)
        {
            //var productId = Convert.ToInt32(Request.QueryString["productId"]);

            GridResult<ViewModel.ProductFeatureModel> gridResult = new GridResult<ViewModel.ProductFeatureModel>();
            var allDetail = new Business.ProductBusiness().GetAllProductDetail(productId);

            gridResult.Data = allDetail.OrderByDescending(r => r.Id).ToList();
            gridResult.Total = allDetail.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProductFeature(ViewModel.ProductFeatureModel productFeature)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.AddProductDetail(productFeature);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult UpdateProductDetail(ViewModel.ProductFeatureModel productFeature)
        {
            Business.ProductBusiness business = new Business.ProductBusiness();
            var returnResult = business.UpdateProductFeature(productFeature);
            return Json(returnResult);
        }


    }
}