using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Services.Interfaces
{
    public interface ISMSService
    {
        Task SendSMSAsync(string email, string subject, string message);
    }
}
