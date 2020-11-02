using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab2;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerAPM server = new ServerAPM(IPAddress.Parse("127.0.0.1"), 4000);
            server.Start();
        }
    }
}
