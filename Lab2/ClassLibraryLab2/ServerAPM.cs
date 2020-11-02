using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab2
{
    public class ServerAPM:ServerTcp
    {
        public delegate void TransmissionDataDelegate(NetworkStream stream);

        public ServerAPM(IPAddress ip, int port) : base(ip, port) { 
        
        }

        public override void Start()
        {
            StartListening();
            AcceptClient();
        }

        protected override void AcceptClient()
        {
            while (true) {
                TcpClient tcpClient = Listener.AcceptTcpClient();

                Stream = tcpClient.GetStream();

                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                transmissionDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient);

            }
        }
        private void TransmissionCallback(IAsyncResult ar)

        {

        

        }

        protected override void BeginDataTransmission(NetworkStream stream)
        {
            FileStream fs = new FileStream("plik.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            
            byte[] buffer = new byte[Buffer_size];
            string w,s;
            while (true) {
                try
                {
                    string tekst = "zapis-w odczyt-r";
                    byte[] tmp = Encoding.ASCII.GetBytes(tekst);
                    stream.Write(tmp, 0, tmp.Length);
                    int message_size = stream.Read(buffer, 0, Buffer_size);
                    w = Encoding.ASCII.GetString(buffer);
                    if (w.StartsWith("w"))
                    {
                        message_size = stream.Read(buffer, 0, Buffer_size);
                        fs.Write(buffer, 0, message_size);

                    }
                    else if (w.StartsWith("r"))
                    {
                        int x = fs.Read(buffer, 0, Buffer_size);
                        stream.Write(buffer, 0, x);

                    }
                    else 
                    {
                        string c = "bledny znak";
                        byte[] y = Encoding.ASCII.GetBytes(c);
                        stream.Write(y, 0, y.Length);
                    }

                }
                catch (IOException e) {
                    break;
                }               
            
            }
        }

        
    }
}
