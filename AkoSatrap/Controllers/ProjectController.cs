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

        public ActionResult Project(int id)
        {
            //string CurrentAction = (string)this.RouteData.Values["id"];
            var business = new Business.ProjectBusiness();
            var project = business.GetProjectById(id);

            var path = Server.MapPath($"~/AkoSatrapImages/{project.ImageFolderName}/");
            project.Images = new List<string>();
            if (System.IO.Directory.Exists(path))
            {

                var files = System.IO.Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var fileName = System.IO.Path.GetFileName(file);
                    project.Images.Add($"../../AkoSatrapImages/{project.ImageFolderName}/{fileName}");
                }
            }

            var lostFile = 4 - project.Images.Count;
            for (int i = 0; i < lostFile; i++)
            {
                project.Images.Add("../../AkoSatrapImages/amghezi.jpg");
            }

            return View(project);
        }

    }
}