using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using mdtypes;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Windows.Input;

namespace MultiDraw
{
    class Sender
    {
        //Form adds lines to queue
        Socket Sock;
        public Queue<LineSegment> LinesToSend { get; set; } = new Queue<LineSegment>();
        //Thread keeps sending stuff as long as it's in the queue
        private Thread sender;
        private static BinaryFormatter bf = new BinaryFormatter();

        //The socket gets connected from outside so we don't need connection stuff here
        //I added a connection dialog on form so we can use that socket -kk
        public Sender(Socket s)
        {
            Sock = s;
            sender = new Thread(SendData);
            sender.IsBackground = true;
            sender.Start();
        }
        private void SendData()     //thread to send the queued data
        {
            while (true)
            {
                try
                {
                    int linesCount = 0;
                    lock (LinesToSend)
                    {
                        linesCount = LinesToSend.Count;
                    }
                    if (linesCount > 0)
                    {
                        LineSegment line;
                        lock (LinesToSend)
                        {
                            line = LinesToSend.Dequeue();
                        }
                        MemoryStream ms = new MemoryStream();
                        try
                        {
                            bf.Serialize(ms, line);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error serializing LineSegment " + ex.Message);
                            break;
                        }
                        try
                        {
                            Sock.Send(ms.GetBuffer(), (int)ms.Length, SocketFlags.None);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to send LineSegment: " + ex.Message);
                            break;
                        }
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " (Thread is dead)");
                    break;
                }
            }
        }
    }
}
