using System;

namespace DocumentLibrary.Infrastructure.DateTimeHelpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTime GetDateTimeNow() 
            => DateTime.Now;
    }
}