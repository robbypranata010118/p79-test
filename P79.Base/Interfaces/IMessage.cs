using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Interfaces
{
    public interface IMessage
    {
        Task SendMessage(string message);
    }
}
