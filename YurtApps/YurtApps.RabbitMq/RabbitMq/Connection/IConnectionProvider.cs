using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YurtApps.Messaging.RabbitMq.Connection
{
    public interface IConnectionProvider
    {
        Task<IConnection> GetConnectionAsync();
    }
}
