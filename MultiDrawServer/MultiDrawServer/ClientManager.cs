using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using mdtypes;

namespace MultiDrawServer
{
    public class ClientManager
    {
        //List of all the clients that are connected
        public List<Client> ClientList { get; private set; } = new List<Client>();
        //Delegate definitions for cross-thread invokes (sockets and line segments)
        private delegate void delVoidSocket(Socket sock);
        private delegate void delVoidLine(LineSegment ls);
        private delVoidSocket m_socketArrived;
        private delVoidLine m_lineReceived;
        //Perpetual listener for new connections
        private Socket m_listener;
        /// <summary>
        /// Get the number of connections
        /// </summary>
        public int Connections
        {
            get
            {
                return ClientList.Count;
            }
        }
        /// <summary>
        /// Get the number of total bytes for all connected clients
        /// </summary>
        public string BytesReceived
        {
            get
            {
                return ClientList.Sum(x => x.BytesReceived).Bitify();
            }
        }
        /// <summary>
        /// Get number of frames received for all clients
        /// </summary>
        public int FramesReceived
        {
            get
            {
                return ClientList.Sum(x => x.FramesReceived);
            }
        }
        /// <summary>
        /// Is this thing closed?
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// On instantiation, initial setup
        /// </summary>
        public ClientManager()
        {
            Closed = false;
            m_socketArrived = SocketArrived;
            Setup();
        }
        /// <summary>
        /// When a line is received, parallel send it to everyone
        /// This is kind of shifty because a thread gets spun up in the framework without me doing anything
        /// </summary>
        /// <param name="o"></param>
        private void LineReceived(object o)
        {
            LineSegment line = o as LineSegment;
            Parallel.For
                (
                0,
                ClientList.Count,
                i => ClientList[i].Send(line)
                );
        }
        /// <summary>
        /// A socket has arrived and is ready to get associated to a client class
        /// </summary>
        /// <param name="sock"></param>
        private void SocketArrived(Socket sock)
        {
            IPEndPoint remote = sock.RemoteEndPoint as IPEndPoint;

            Client client = new Client(sock, remote.Address.ToString());
            client.LineReceived += LineReceived;
            ClientList.Add(client);
            Rearm();
        }
        /// <summary>
        /// Initial setup
        /// </summary>
        private void Setup()
        {
            m_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                m_listener.Bind(new IPEndPoint(IPAddress.Any, 1666));
            }
            catch(Exception err)
            {
                MessageBox.Show("(Rearm)Bind failed: " + err.Message);
            }
            try
            {
                m_listener.Listen(5);
                Rearm();
            }
            catch(Exception err)
            {
                MessageBox.Show("((Rearm)Listen failed: " + err.Message);
            }
        }
        /// <summary>
        /// Rearming mechanism for accepting a new connection
        /// </summary>
        private void Rearm()
        {
            try
            {
                m_listener.BeginAccept(CBAccept, m_listener);
            }
            catch (Exception err)
            {
                MessageBox.Show("(Rearm)BeginAccept Failed: " + err.Message);
            }
        }
        /// <summary>
        /// Callback when a begin accept goes through
        /// </summary>
        /// <param name="ar"></param>
        private void CBAccept(IAsyncResult ar)
        {
            Socket lsock = ar.AsyncState as Socket;
            try
            {
                Socket connsock = lsock.EndAccept(ar);
                m_socketArrived.Invoke(connsock);
            }
            catch(Exception err)
            {
                MessageBox.Show("(CBAccept)EndAccept Failed: " + err.Message);
            }
        }
        public void CheckConn()
        {
            for (int i = 0; i < ClientList.Count; i++)
            {
                if (!(ClientList[i].Socket.Connected))
                {
                    ClientList.RemoveAt(i);
                }
            }
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Will convert a number into its SI byte format (doesn't make assumptions about where that number comes from, could be number of goats or chunks of coal or whatever)
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Bitify(this int num)
        {
            if (num / Math.Pow(2, 30) > 1)
            {
                return (num / Math.Pow(2, 30)).ToString("F") + "GB";
            }
            else if (num / Math.Pow(2, 20) > 1)
            {
                return (num / Math.Pow(2, 20)).ToString("F") + "MB";
            }
            else if (num / Math.Pow(2, 10) > 1)
            {
                return (num / Math.Pow(2, 10)).ToString("F") + "kB";
            }
            else
            {
                return num + " bytes";
            }
        }
    }
}
