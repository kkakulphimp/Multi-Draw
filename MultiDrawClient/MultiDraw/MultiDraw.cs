using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using mdtypes;
using System.Drawing.Drawing2D;

namespace MultiDraw
{
    public partial class MultiDraw : Form
    {
        private Socket m_socket;
        private ConnectionDialog m_connector = new ConnectionDialog();
        private Receiver m_receiver;
        private Sender m_sender;
        private int m_currentThick = 1;
        private int m_currentAlpha = 255;
        private Color m_currentColor = Color.Black;
        private PointF m_lastPoint = new PointF(-1, -1);
        private bool m_leftClickDown = false;
        private Thread m_getterDrawer;
        private bool m_alpha = false;
        private delegate void delVoidVoid();
        private delegate void delVoidInt(int size);
        public MultiDraw()
        {
            InitializeComponent();
            m_connector.ShowDialog();
            Setup();
            updateTimer.Enabled = true;
            //Mouse events
            MouseWheel += MultiDraw_MouseWheel;
            lbThickness.Text = $"Thickness: {m_currentThick} [Alpha: {m_currentAlpha}]";
        }

        private void Setup()
        {
            m_socket = m_connector.Socket;
            //Sender here
            m_sender = new Sender(m_socket);
            //Receiver here
            m_receiver = new Receiver(m_socket);
            //Set up drawer thread
            m_getterDrawer = new Thread(Drawer);
            m_getterDrawer.IsBackground = true;
            m_getterDrawer.Start();
            //Update timer needs to start only after the receiver is instantiated
        }

        private void MultiDraw_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!m_alpha)
            {
                if (e.Delta > 0)
                {
                    m_currentThick++;
                    m_currentThick = Math.Min(m_currentThick, ushort.MaxValue);
                }
                else if (e.Delta < 0)
                {
                    m_currentThick--;
                    m_currentThick = Math.Max(m_currentThick, 1);
                }
            }
            else
            {
                if (e.Delta > 0)
                {
                    m_currentAlpha++;
                    m_currentAlpha = Math.Min(m_currentAlpha, byte.MaxValue);
                }
                else if (e.Delta < 0)
                {
                    m_currentAlpha--;
                    m_currentAlpha = Math.Max(m_currentAlpha, 1);
                }
            }
            lbThickness.Text = $"Thickness: {m_currentThick} [Alpha: {m_currentAlpha}]";
        }

        private void MultiDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_leftClickDown)
            {
                Invoke(new Action(() => AddSeg(e)));
            }
        }
        private void Drawer()
        {
            while (true)
            {
                int lines;
                lock (m_receiver.receivedLines)
                {
                    lines = m_receiver.receivedLines.Count;
                }
                if (lines > 0)
                {
                    lock (m_receiver.receivedLines)
                    {
                        LineSegment lineseg = m_receiver.receivedLines.Dequeue();
                        GraphicsPath line = new GraphicsPath();
                        Color col = Color.FromArgb(lineseg.Alpha, lineseg.Colour.R, lineseg.Colour.G, lineseg.Colour.B);
                        Pen pen = new Pen(col, lineseg.Thickness);
                        pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
                        line.AddLine(lineseg.Start, lineseg.End);
                        Graphics gr = CreateGraphics();
                        gr.DrawPath(pen, line);
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
        private void AddSeg(MouseEventArgs e)
        {
            lock (m_sender.LinesToSend)
            {
                LineSegment LineSeg = new LineSegment();
                LineSeg.Colour = m_currentColor;
                LineSeg.Alpha = (byte)m_currentAlpha;
                LineSeg.Thickness = (ushort)m_currentThick;
                LineSeg.Start = e.Location;
                LineSeg.End = m_lastPoint;
                lock (m_sender.LinesToSend)
                {
                    m_sender.LinesToSend.Enqueue(LineSeg);
                }
                m_lastPoint = e.Location;
            }
        }
        private void MultiDraw_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left) && ClientRectangle.Contains(e.Location))
            {
                m_leftClickDown = true;
                m_lastPoint = e.Location;
            }
        }

        private void MultiDraw_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                m_leftClickDown = false;
            }
        }

        private void lbColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                btnColor.ForeColor = cd.Color;
                m_currentColor = cd.Color;
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!m_socket.Connected)
            {
                m_connector.ShowDialog();
                m_socket = m_connector.Socket;
                btnConnect.Text = "Connected";
                Setup();
            }
        }

        private void MultiDraw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                m_alpha = true;
            }
        }

        private void MultiDraw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                m_alpha = false;
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            lbFragment.Text = $"Fragments: {m_receiver.Frags.ToString()}";
            lbBytesRxed.Text = $"Bytes RXed: {m_receiver.BytesRx.Bitify()}";
            lbDestack.Text = $"Destack Avg: {m_receiver.Destack.ToString("F")}";
            lbRXedFrames.Text = $"Frames RX'ed: {m_receiver.Frames.ToString()}";
            btnConnect.Text = m_socket.Connected ? "Connected" : "Disconnected";
        }

        private void MultiDraw_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_socket.Close();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap temp = new Bitmap(dialog.FileName);

                for (int y = 0; y < temp.Height; y++)
                {
                    for (int x = 0; x < temp.Width; x++)
                    {
                        Color col = temp.GetPixel(x, y);
                        LineSegment line = new LineSegment() { Alpha = 255, Colour = col, Start = new PointF(x - 1, y), End = new PointF(x, y) };
                        lock (m_sender.LinesToSend)
                        {
                            m_sender.LinesToSend.Enqueue(line);
                        }
                    }
                }
            }

        }
    }
    public static class Extensions
    {
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
                return num + "bytes";
            }
        }
    }
}
