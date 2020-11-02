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
        private static byte[] buffer = new byte[256];
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
        public static void WriteCallback(IAsyncResult ar) {
            NetworkStream s = (NetworkStream)ar.AsyncState;
            
            s.EndWrite(ar);
            s.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback),s);
        }
        protected override void BeginDataTransmission(NetworkStream stream)
        {
            
            
            
            

            
                    stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), stream);
                   
                    

                
                
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            NetworkStream s = (NetworkStream)ar.AsyncState;
            
            

            int n = s.EndRead(ar);
            s.BeginWrite(buffer, 0, n,new AsyncCallback(WriteCallback), s);
            
        }
    }
}
