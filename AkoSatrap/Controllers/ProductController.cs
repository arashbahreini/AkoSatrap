using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var business = new Business.ProductBusiness();
            var allProduct = business.GetAllProduct(false);
            allProduct.ForEach(r =>
            {
                var path = Server.MapPath($"~/AkoSatrapImages/{r.ImageFolderName}/");
                r.Images = new List<string>();
                if (System.IO.Directory.Exists(path))
                {
                    var files = System.IO.Directory.GetFiles(path);
                    if (files.Count() > 0)
                    {
                        r.Images.Add($"../../AkoSatrapImages/{r.ImageFolderName}/{System.IO.Path.GetFileName(files[0])}");
                    }
                    else
                    {
                        r.Images.Add($"../../AkoSatrapImages/{r.ImageFolderName}/amghezi.jpg");
                    }
                }
                else
                {
                    r.Images.Add($"../../AkoSatrapImages/amghezi.jpg");
                }
            });

            return View(allProduct);
        }

        public ActionResult Product(int id)
        {
            //string CurrentAction = (string)this.RouteData.Values["id"];
            var business = new Business.ProductBusiness();
            var product = business.GetProduct(id);

            var path = Server.MapPath($"~/AkoSatrapImages/{product.ImageFolderName}/");
            product.Images = new List<string>();
            if (System.IO.Directory.Exists(path))
            {

                var files = System.IO.Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var fileName = System.IO.Path.GetFileName(file);
                    product.Images.Add($"../../AkoSatrapImages/{product.ImageFolderName}/{fileName}");
                }
            }

            var lostFile = 8 - product.Images.Count;
            for (int i = 0; i < lostFile; i++)
            {
                product.Images.Add("../../AkoSatrapImages/amghezi.jpg");
            }

            return View(product);
        }
    }
}