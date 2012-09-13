using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aqueduct.Mail
{
    public interface IEmail
    {
        void Send();
        MailMessage CreateMessage();
    }
}
