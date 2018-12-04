using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult ProjectDone()
        {
            var business = new Business.ProjectBusiness();
            var allProject = business.GetAllProject(false).Where(x => x.CompletionPercentage == 100).ToList();
            allProject.ForEach(r =>
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

            return View(allProject);
        }

        public ActionResult ProjectRunning()
        {
            var business = new Business.ProjectBusiness();
            var allProject = business.GetAllProject(false).Where(x => x.CompletionPercentage < 100).ToList();
            allProject.ForEach(r =>
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

            return View(allProject);
        }

       
    }
}