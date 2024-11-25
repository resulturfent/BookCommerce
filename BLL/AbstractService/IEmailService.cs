using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequestDto email);
    }
}
