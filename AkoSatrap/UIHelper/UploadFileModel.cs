using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace AkoSatrap.UIHelper
{
    public class ValidateFileSizeAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }

            if (file.ContentLength > 3 * 1024 * 1024)
            {
                return false;
            }


            return true;
        }
    }
    public class UploadFileModel
    {
        [Required(ErrorMessage = "انتخاب فایل اجباری می باشد")]
        [ValidateFileSize(ErrorMessage = "حجم فایل بیشتر از 5 مگابایت نباید باشد")]
        public HttpPostedFileBase File { get; set; }

        [Required(ErrorMessage = "عنوان فایل اجباری می باشد")]
        public string Title { get; set; }

        public string Description { get; set; }


        public FileType FileType { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "حاصل جمع")]
        public string CaptchaInputText { get; set; }
    }

    public enum FileType
    {
        [Description("صورتحساب")]
        Factor = 1,
        [Description("رزومه استخدام")]
        Resume = 2,
        [Description("سایر")]
        Other = 3
    }
}