using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StardewValleyExtraSlots
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void find_Click(object sender, EventArgs e)
        {
            AddSlots addSlot = new AddSlots();
            addSlot.ReadTitleName();
        }
    }
}
