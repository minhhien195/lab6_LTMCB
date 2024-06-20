using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _22520415_Doanso
{
    public partial class CreateGame : Form
    {
        public CreateGame()
        {
            InitializeComponent();
        }

        private void btnCreateserver_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            server.Show();
        }

        private void btnJoingame_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
