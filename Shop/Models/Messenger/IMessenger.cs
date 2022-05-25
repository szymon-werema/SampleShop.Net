using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Messenger
{
    public interface IMessenger <T>
    {
        public Task sendMessageAsync(string message, string recipient);
    }
}
