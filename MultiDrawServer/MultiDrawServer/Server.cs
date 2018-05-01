/* Title: MultiDraw Server
 * Written by: Karun Kakulphimp
 * Description: This program is a socket server handling "Drawer" clients
 * It hopes to receive line information and to send it back to every client
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mdtypes;

namespace MultiDrawServer
{
    public partial class Server : Form
    {
        //Datagridview objects to show client
        private BindingSource m_bindingSource = new BindingSource();
        private ClientManager m_clientManager = new ClientManager();
        public Server()
        {
            InitializeComponent();
            //Associate datasources with DGV
            m_bindingSource.DataSource = m_clientManager.ClientList;
            UI_dataGridView.DataSource = m_bindingSource;
        }

        private void Server_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Update statistics on timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_timer_Tick(object sender, EventArgs e)
        {
            var info = from q in m_clientManager.ClientList select new { q.Address, BytesReceived = q.BytesReceived.Bitify(), Destacked = q.Destacked.ToString("F5"), q.Fragmentation, q.FramesReceived };
            m_bindingSource.DataSource = info;
            UI_toolStripStatusLabel.Text = $"Total Bytes: {m_clientManager.BytesReceived} Total Frames: {m_clientManager.FramesReceived}";
            m_clientManager.CheckConn();
        }
        /// <summary>
        /// Send drawings to every client for giggles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_toolStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bm = new Bitmap(dialog.FileName);
                //Parallel sender for each client
                Parallel.For
                (
                0,
                m_clientManager.ClientList.Count,
                i =>
                {
                    Bitmap temp;
                    lock (bm)
                    {
                        temp = bm.Clone() as Bitmap;
                    }
                    for (int x = 0; x < temp.Width; x++)
                    {
                        for (int y = 0; y < temp.Height; y++)
                        {
                            Color col = temp.GetPixel(x, y);
                            LineSegment line = new LineSegment() { Alpha = 255, Colour = col, Start = new PointF(x-1, y), End = new PointF(x, y) };
                            m_clientManager.ClientList[i].Send(line);
                        }
                    }

                }
            );

            }
        }
    }
}
