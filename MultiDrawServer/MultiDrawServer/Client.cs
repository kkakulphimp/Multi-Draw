using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using mdtypes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Serialization;

namespace MultiDrawServer
{
    public class Client
    {
        //Common binary formatter
        private static BinaryFormatter m_binaryFormatter = new BinaryFormatter();
        //Dedicated receiver thread for each client
        private Thread m_clientThread;
        //Number of samples for the purpose of running average
        private int m_samples;
        /// <summary>
        /// Special socket for this client
        /// </summary>
        public Socket Socket { get; private set; }
        /// <summary>
        /// IP address
        /// </summary>
        public string Address { get; private set; }
        /// <summary>
        /// Number of frames from this client
        /// </summary>
        public int FramesReceived { get; set; }
        /// <summary>
        /// Destacked frames
        /// </summary>
        public double Destacked { get; set; }
        /// <summary>
        /// Fragged frames
        /// </summary>
        public int Fragmentation { get; set; }
        /// <summary>
        /// Bytes received from client
        /// </summary>
        public int BytesReceived { get; set; }
        /// <summary>
        /// Event whenever a client line is received
        /// </summary>
        /// <param name="o"></param>
        public delegate void ClientEvent(object o);
        public event ClientEvent LineReceived;
        /// <summary>
        /// Client instantiation, gets a socket and address and thread spins up
        /// </summary>
        /// <param name="soc"></param>
        /// <param name="addr"></param>
        public Client(Socket soc, string addr)
        {
            Socket = soc;
            Address = addr;
            //Start client receiver thread
            m_clientThread = new Thread(Receive);
            m_clientThread.IsBackground = true;
            m_clientThread.Start();
        }
        /// <summary>
        /// Receiver I got from P21 in the socket pdf
        /// </summary>
        private void Receive()
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            byte[] buff = new byte[2048];
            do
            {
                float stackFrames = 0;
                try
                {
                    int count = Socket.Receive(buff);
                    BytesReceived += count;
                    if (count == 0)
                    {
                        Socket.Close();
                        return;
                    }
                    long position = memoryStream.Position;
                    memoryStream.Seek(0, SeekOrigin.End);
                    memoryStream.Write(buff, 0, count);
                    memoryStream.Position = position;

                    do
                    {
                        long startPosition = memoryStream.Position;
                        try
                        {
                            object thing = binaryFormatter.Deserialize(memoryStream);
                            if (thing is LineSegment)
                            {
                                LineReceived.Invoke(thing);
                                stackFrames++;
                                FramesReceived++;
                            }
                            else
                            {
                                Socket.Close();
                                return;
                            }
                        }
                        catch (Exception erro)
                        {
                            if (erro is SerializationException)
                            {
                                memoryStream.Position = startPosition;
                                Fragmentation++;
                                break;
                            }
                            else
                            {
                                throw erro;
                            }
                        }

                    } while (memoryStream.Position < memoryStream.Length);

                    if (memoryStream.Position == memoryStream.Length)
                    {
                        memoryStream.Position = 0;
                        memoryStream.SetLength(0);
                    }
                }
                catch (Exception erro)
                {
                    Console.WriteLine($"Client {Address} disconnected");
                    break;
                }
                m_samples++;
                Destacked = Destacked + (stackFrames - Destacked) / m_samples;

            } while (Socket.Connected);
        }
        /// <summary>
        /// Sender for socket, really simple
        /// </summary>
        /// <param name="ls"></param>
        public void Send(LineSegment ls)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                m_binaryFormatter.Serialize(ms, ls);
                Socket.Send(ms.GetBuffer(), (int)ms.Length, SocketFlags.None);
            }
            catch (Exception err)
            {
                Console.WriteLine("(Send)Send Failed: " + err.Message);
            }
        }
    }
}
