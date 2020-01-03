using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NetCoreFrame.Web
{

    public class ServerHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }

    public interface IMyService
    {
        Task<string> SayHello();

        Task Sleep();
    }

    public class MyService : IMyService
    {
        public async Task<string> SayHello()
        {
            return await Task.Factory.StartNew(() => "Hello");
        }

        public async Task Sleep()
        {
            await Task.Delay(3000);
        }

    }
}
