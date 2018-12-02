using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ProductBusiness
    {
        #region ProductCategory
        public ViewModel.ReturnResult<bool> AddProductCategory(ViewModel.ProductCategotyModel productCategory)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();
            try
            {
                DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
                var enDbProductCategory = new DomainDeriven.ProductCategory();
                var dbProductCategory = new DomainDeriven.ProductCategory();

                enDbProductCategory.Title = productCategory.EnTitle;
                enDbProductCategory.IsEnglish = true;

                dbProductCategory.Title = productCategory.Title;
                dbProductCategory.IsEnglish = false;
                dbProductCategory.ProductCategory2 = enDbProductCategory;
                context.ProductCategories.Add(dbProductCategory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }

            return returnResult;
        }

        public List<ViewModel.ProductCategotyModel> GetAllProductCategory()
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
            var a = context.ProductCategories.Include("ProductCategory2").Where(r => r.IsEnglish == false)
                .Select(r => new ViewModel.ProductCategotyModel { Title = r.Title, EnTitle = r.ProductCategory2.Title, Id = r.Id, EnId = r.EnId.Value }).ToList();
            return a;
        }

        public ViewModel.ProductCategotyModel GetProductCategory(int id)
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
            return context.ProductCategories.Include("ProductCategory2").Where(r => r.Id == id)
                .Select(r => new ViewModel.ProductCategotyModel { Title = r.Title, EnTitle = r.ProductCategory2.Title, Id = r.Id, EnId = r.EnId.Value }).FirstOrDefault();

        }

        public ViewModel.ReturnResult<bool> UpdateProductCategory(ViewModel.ProductCategotyModel productCategory)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();
            try
            {
                DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();

                var dbProductCategory = context.ProductCategories.Include("ProductCategory2").FirstOrDefault(r => r.IsEnglish == false && r.Id == productCategory.Id);
                if (dbProductCategory != null)
                {
                    dbProductCategory.Title = productCategory.Title;
                    dbProductCategory.ProductCategory2.Title = productCategory.EnTitle;

                    context.SaveChanges();
                }
                else
                {
                    returnResult.SetError("دسته بندی مورد نظر پیدا نشد");
                }


                context.SaveChanges();
            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }

            return returnResult;
        }
        #endregion

        #region product
        public ViewModel.ReturnResult<bool> AddProduct(ViewModel.ProductModel product)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();

            try
            {
                DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
                var enDbProduct = new DomainDeriven.Product();
                var dbProduct = new DomainDeriven.Product();

                enDbProduct.Brand = product.EnBrand;
                enDbProduct.CreateDate = DateTime.Now;
                enDbProduct.ImageFolderName = Guid.NewGuid().ToString();
                enDbProduct.ImportantTip1 = product.EnImportantTip1;
                enDbProduct.ImportantTip2 = product.EnImportantTip2;
                enDbProduct.ImportantTip3 = product.EnImportantTip3;
                enDbProduct.IsEnglish = true;
                enDbProduct.ProductCategoryId = product.Category.EnId;
                enDbProduct.Title = product.EnTitle;
                enDbProduct.Description = product.EnDescription;


                dbProduct.Brand = product.Brand;
                dbProduct.CreateDate = DateTime.Now;
                dbProduct.ImageFolderName = enDbProduct.ImageFolderName;
                dbProduct.ImportantTip1 = product.ImportantTip1;
                dbProduct.ImportantTip2 = product.ImportantTip2;
                dbProduct.ImportantTip3 = product.ImportantTip3;
                dbProduct.IsEnglish = false;
                dbProduct.ProductCategoryId = product.Category.Id;
                dbProduct.Title = product.Title;
                dbProduct.Description = product.Description;
                dbProduct.Product2 = enDbProduct;

                context.Products.Add(dbProduct);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }

            return returnResult;
        }

        public List<ViewModel.ProductModel> GetAllProduct()
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();

            var products = context.Products.Include("Product2").Include("ProductCategory").Where(r => r.IsEnglish == false)
                .Select(r => new ViewModel.ProductModel
                {
                    Category = new ViewModel.ProductCategotyModel
                    {
                        Title = r.ProductCategory.Title,
                        Id = r.ProductCategory.Id,
                        EnId = r.Product2.ProductCategoryId.Value,
                        EnTitle = r.Product2.ProductCategory.Title
                    },

                    Brand = r.Brand,
                    Id = r.Id,
                    Title = r.Title,
                    ImportantTip1 = r.ImportantTip1,
                    ImportantTip2 = r.ImportantTip2,
                    ImportantTip3 = r.ImportantTip3,
                    ImageFolderName = r.ImageFolderName,
                    CategoryId = r.ProductCategoryId.Value,
                    Description = r.Description,
                    EnBrand = r.Product2.Brand,
                    EnId = r.Product2.Id,
                    EnImportantTip1 = r.Product2.ImportantTip1,
                    EnImportantTip2 = r.Product2.ImportantTip2,
                    EnImportantTip3 = r.Product2.ImportantTip3,
                    EnTitle = r.Product2.Title,
                    EnDescription = r.Product2.Description,
                }).ToList();

            products.ForEach(item =>
            {
                var path = System.Web.Hosting.HostingEnvironment.MapPath($"~/AkoSatrapImages/{item.ImageFolderName}/");
                if (Directory.Exists(path))
                {
                    item.Images = Directory.GetFiles(path).Select(r => Path.GetFileName(r)).ToList();
                }
            });

            return products;
        }

        public ViewModel.ProductModel GetProduct(int id)
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
            var productView = new ViewModel.ProductModel();
            var product = context.Products.Include("ProductCategory").Include("ProductFeatures").FirstOrDefault(r => r.Id == id);
            if (product != null)
            {
                productView = new ViewModel.ProductModel
                {
                    Category = new ViewModel.ProductCategotyModel
                    {
                        Title = product.ProductCategory.Title,
                        Id = product.ProductCategory.Id,
                        EnId = product.Product2.ProductCategoryId.Value,
                        EnTitle = product.Product2.ProductCategory.Title
                    },

                    Brand = product.Brand,
                    Id = product.Id,
                    Title = product.Title,
                    ImportantTip1 = product.ImportantTip1,
                    ImportantTip2 = product.ImportantTip2,
                    ImportantTip3 = product.ImportantTip3,
                    ImageFolderName = product.ImageFolderName,
                    CategoryId = product.ProductCategoryId.Value,
                    Description = product.Description
                };
                productView.ProductFeature = new List<ViewModel.ProductFeatureModel>();
                product.ProductFeatures.ToList().ForEach(feature => {
                    productView.ProductFeature.Add(new ViewModel.ProductFeatureModel { Title = feature.Title, Id = feature.Id, Description = feature.Description });
                });
            }

            return productView;
        }

        public List<ViewModel.ProductModel> GetAllProduct(bool isEnglish)
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();

            var products = context.Products.Include("ProductCategory").Where(r => r.IsEnglish == isEnglish)
                .Select(r => new ViewModel.ProductModel
                {
                    Category = new ViewModel.ProductCategotyModel
                    {
                        Title = r.ProductCategory.Title,
                        Id = r.ProductCategory.Id,
                        EnId = r.Product2.ProductCategoryId.Value,
                        EnTitle = r.Product2.ProductCategory.Title
                    },

                    Brand = r.Brand,
                    Id = r.Id,
                    Title = r.Title,
                    ImportantTip1 = r.ImportantTip1,
                    ImportantTip2 = r.ImportantTip2,
                    ImportantTip3 = r.ImportantTip3,
                    ImageFolderName = r.ImageFolderName,
                    CategoryId = r.ProductCategoryId.Value,
                    Description = r.Description                    
                }).ToList();

            return products;
        }

        public ViewModel.ReturnResult<bool> UpdateProduct(ViewModel.ProductModel product)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();
            try
            {
                DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();

                var dbProductCategory = context.Products.Include("Product2").FirstOrDefault(r => r.IsEnglish == false && r.Id == product.Id);
                if (dbProductCategory != null)
                {
                    dbProductCategory.Title = product.Title;
                    dbProductCategory.Brand = product.Brand;
                    dbProductCategory.ImportantTip1 = product.ImportantTip1;
                    dbProductCategory.ImportantTip2 = product.ImportantTip2;
                    dbProductCategory.ImportantTip3 = product.ImportantTip3;
                    dbProductCategory.Description = product.Description;

                    dbProductCategory.Product2.Title = product.EnTitle;
                    dbProductCategory.Product2.Brand = product.EnBrand;
                    dbProductCategory.Product2.ImportantTip1 = product.EnImportantTip1;
                    dbProductCategory.Product2.ImportantTip2 = product.EnImportantTip2;
                    dbProductCategory.Product2.ImportantTip3 = product.EnImportantTip3;
                    dbProductCategory.Product2.Description = product.EnDescription;

                    context.SaveChanges();
                }
                else
                {
                    returnResult.SetError("دسته بندی مورد نظر پیدا نشد");
                }


                context.SaveChanges();
            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }

            return returnResult;
        }

        public ViewModel.ProductModel GetProductById(int id)
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();

            var product = context.Products.FirstOrDefault(r => r.Id == id);

            var productViewModel = new ViewModel.ProductModel
            {
                Id = product.Id,
                Brand = product.Brand,
                CategoryId = product.ProductCategoryId.Value,
                Description = product.Description,
                ImageFolderName = product.ImageFolderName
            };
            return productViewModel;

        }

        #endregion

        #region productDetail

        public ViewModel.ReturnResult<bool> AddProductDetail(ViewModel.ProductFeatureModel productFeature)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();
            try
            {
                DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();

                var product = context.Products.Include("Product2").FirstOrDefault(r => r.Id == productFeature.ProductId);
                if (product != null)
                {
                    var enProductFeatureDb = new DomainDeriven.ProductFeature
                    {
                        Title = productFeature.EnTitle,
                        Description = productFeature.EnDescription,
                        Order = productFeature.Order,
                        ProductId = product.Product2.Id,
                        IsEnglish = true
                    };

                    var faProductFeatureDb = new DomainDeriven.ProductFeature
                    {
                        Title = productFeature.Title,
                        Description = productFeature.Description,
                        Order = productFeature.Order,
                        ProductFeature2 = enProductFeatureDb,
                        ProductId = product.Id,
                        IsEnglish = false
                    };

                    context.ProductFeatures.Add(faProductFeatureDb);

                    context.SaveChanges();
                }
                else
                {
                    returnResult.SetError("محصول مورد نظر پیدا نشدُ");
                }

            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }

            return returnResult;
        }

        public List<ViewModel.ProductFeatureModel> GetAllProductDetail(int productId)
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
            var productFeatureList = context.ProductFeatures.Include("ProductFeatures2").Where(r => !r.IsEnglish.Value && r.ProductId == productId)
                .Select(r => new ViewModel.ProductFeatureModel
                {
                    Title = r.Title,
                    EnTitle = r.ProductFeature2.Title,
                    Id = r.Id,
                    EnId = r.EnId.Value,
                    Description = r.Description,
                    EnDescription = r.ProductFeature2.Description,
                    Order = r.Order.Value,
                    ProductId = r.ProductId.Value
                }).ToList();

            return productFeatureList;
        }

        public ViewModel.ReturnResult<bool> UpdateProductFeature(ViewModel.ProductFeatureModel productFeature)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();
            try
            {
                DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();

                var dbProductFeature = context.ProductFeatures.Include("ProductFeature2")
                    .FirstOrDefault(r => !r.IsEnglish.Value && r.Id == productFeature.Id);

                if (dbProductFeature != null)
                {
                    dbProductFeature.Title = productFeature.Title;
                    dbProductFeature.ProductFeature2.Title = productFeature.EnTitle;
                    dbProductFeature.Description = productFeature.Description;
                    dbProductFeature.ProductFeature2.Description = productFeature.EnDescription;
                    dbProductFeature.Order = productFeature.Order;
                    dbProductFeature.ProductFeature2.Order = productFeature.Order;


                    context.SaveChanges();
                }
                else
                {
                    returnResult.SetError("جزییات محصول مورد نظر پیدا نشد");
                }


                context.SaveChanges();
            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }

            return returnResult;
        }

        #endregion
    }
}
