using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSpy.Repositories
{
    public interface IEmailService
    {
        public void SendEmailAsync(string email, string subject, string message);
    }
}
