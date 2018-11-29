using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class ReturnResult<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public ReturnResult()
        {
            Success = true;
            Message = "عملیات مورد نظر با موفقیت انجام شد";
        }
        public void SetError(string message)
        {
            this.Message = message;
            Success = false;
        }
    }
}