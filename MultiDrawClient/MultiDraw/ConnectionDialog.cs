using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace MultiDraw
{
    public partial class ConnectionDialog : Form
    {
        public Socket Socket { get; private set; }
        public ConnectionDialog()
        {
            InitializeComponent();
        }

        private void UI_buttonConnect_Click(object sender, EventArgs e)
        {
            Socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
                );
            try
            {
                Socket.BeginConnect(UI_textBoxAddress.Text, (int)UI_numericUpDown.Value, CBConnected, Socket);
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void CBConnected(IAsyncResult o)
        {
            Invoke(new Action(()=> Connected()));
        }
        private void Connected()
        {
            Hide();
        }
    }
}
