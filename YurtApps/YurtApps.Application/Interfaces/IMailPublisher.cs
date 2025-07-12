using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.Dtos;

namespace YurtApps.Application.Interfaces
{
    public interface IMailPublisher
    {
        Task Publish(MailDto dto);
    }
}
