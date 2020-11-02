using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab2
{
    public abstract class ServerTcp
    {
        
        IPAddress ip;
        int port;
        int buffer_size = 1024;
        bool is_running;
        TcpListener listener;
        TcpClient client;
        NetworkStream stream;

        protected bool checkPort() {
            if (port < 1024 || port > 49151) return false;
            return true;
        }
        public IPAddress Ip { get => ip;set {
                if (!is_running) ip = value; else throw new Exception("Nie mozna zmieniac ip gdy serwer jest uruchomiony!");
            } }

        public int Port { get => port;set { if (!is_running) { port = value; if (!checkPort()) throw new Exception("zly port"); } else throw new Exception("nie mozna zmieniac portu kiedy serwer jest uruchomiony!"); } }
        public int Buffer_size { get => buffer_size;set {
                if (value < 0 || value > 1024 * 1024 * 64) throw new Exception("Zly rozmiar bufora!");if (!is_running) buffer_size = value; else throw new Exception("nie mozna zmieniac rozmiaru bufora gdy serwer jest uruchomiony!");
            } }
        protected TcpListener Listener { get => listener; set => listener = value; }
        protected TcpClient Client { get => client; set => client = value; }
        protected NetworkStream Stream { get => stream; set => stream = value; }

        public ServerTcp(IPAddress ip, int port) {
            is_running = false;
            Port = port;
            Ip = ip;
            if (!checkPort()) {
                Port = 6000;
                throw new Exception("Nieprawidlowy numer portu, ustawione na 6000");
            }

        }
        protected void StartListening() {
            listener = new TcpListener(ip, port);
            listener.Start();

        }
        protected abstract void AcceptClient();
        protected abstract void BeginDataTransmission(NetworkStream stream);
        public abstract void Start();


    }
}
