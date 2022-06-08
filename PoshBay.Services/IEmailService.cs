using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmail(RegisterViewModel model);
        Task<bool> SendResetToKen(string email, string link);
        string EmailBody(RegisterViewModel model);
    }
}