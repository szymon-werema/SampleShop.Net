using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Messenger
{
    public class EmailMessage : IMessenger
    {
        public void sendMessage(string message, string recipient)
        {
            throw new NotImplementedException();
        }
    }
}
