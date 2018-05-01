using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdtypes;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace MultiDraw
{
    class Receiver
    {
        private Socket socket;
        private Thread receiveThread;
        private bool open;
        public Queue<LineSegment> receivedLines { get; set; } = new Queue<LineSegment>();
        public int Frags { get; private set; } = 0;
        public int Frames { get; private set; } = 0;
        public double Destack { get; private set; } = 1;
        public int BytesRx { get; private set; } = 0;
        private int samples = 0;

        public Receiver(Socket soc)
        {
            open = true;
            socket = soc;
            receiveThread = new Thread(GetLines);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        public void GetLines()
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            byte[] buff = new byte[2048];
            while (open)
            {
                float stackFrames = 0;
                try
                {
                    int count = socket.Receive(buff);
                    BytesRx += count;
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
                            lock (receivedLines)
                            {
                                if (thing is LineSegment)
                                {
                                    receivedLines.Enqueue(thing as LineSegment);
                                    stackFrames++;
                                    Frames++;
                                }
                                else
                                {
                                    throw new Exception("Object is not line segment!");
                                }
                            }
                        }
                        catch (Exception erro)
                        {
                            if (erro is SerializationException)
                            {
                                memoryStream.Position = startPosition;
                                Frags++;
                                break;
                            }
                            else
                            {
                                MessageBox.Show(erro.Message);
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
                    MessageBox.Show(erro.Message);
                    break;
                }
                samples++;
                Destack = Destack + (stackFrames - Destack) / samples;
                
            }

        }
    }
}
