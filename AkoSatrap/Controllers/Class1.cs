using System;

namespace AkoSatrap.Controllers
{
    public class HappuBirthDay
    {
        public string Alert(DateTime when)
        {
            return
                when.Day == DateTime.Now.Day ?
                "Happy BirthDay" :
                string.Empty;
        }
    }
}