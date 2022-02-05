using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.SharedKernal.Utilities
{
    public static class DatetimeUtility
    {
        public static int GetAgeFromDate(this DateTime birthdate)
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(birthdate.ToString("yyyyMMdd"));
            int age = (now - dob) / 10000;
            return age;
        }
    }
}
