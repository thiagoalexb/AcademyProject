using System;

namespace Academy.Domain.Entities
{
    public class ErrorLog
    {
        public Guid ErrorLogId { get; set; }
        public string Message { get; set; }
        public string ExceptionString { get; set; }
    }
}
