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
        private AddSlots addSlot;
        public MainForm()
        {
            addSlot = new AddSlots();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            addComboBox.Items.Add("1");
            addComboBox.Items.Add("2");
            addComboBox.Items.Add("3");
            addComboBox.Items.Add("4");
            addComboBox.Items.Add("5");
            addComboBox.Items.Add("6");
            addComboBox.SelectedIndex = 0;
        }

        private void find_Click(object sender, EventArgs e)
        {
            saveComboBox.Items.Clear();
            saveComboBox.ResetText();
            addSlot = new AddSlots();

            string cid = addSlot.GenerateCabinNameID();
            string mpid = addSlot.GenerateUniqueMultiplayerID();
            addSlot.ReadTitleName();

            foreach (GameSave g in addSlot.gameSaves)
            {
                saveComboBox.Items.Add(g.saveFile);
            }

            addComboBox.SelectedIndex = 0;
            saveComboBox.SelectedIndex = 0;
            farmBox.Text = addSlot.gameSaves[0].farmName;
            farmerBox.Text = addSlot.gameSaves[0].playerName;
            slotBox.Text = addSlot.gameSaves[0].cabinNum.ToString();
        }

        private void saveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            farmBox.Text = addSlot.gameSaves[saveComboBox.SelectedIndex].farmName;
            farmerBox.Text = addSlot.gameSaves[saveComboBox.SelectedIndex].playerName;
            slotBox.Text = addSlot.gameSaves[saveComboBox.SelectedIndex].cabinNum.ToString();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string result = addSlot.AddSlotsToFile(addSlot.gameSaves[saveComboBox.SelectedIndex], addComboBox.SelectedIndex + 1);
            saveComboBox.Items.Clear();
            saveComboBox.ResetText();
            farmBox.Clear();
            farmerBox.Clear();
            slotBox.Clear();
            addSlot = new AddSlots();

            MessageBox.Show(result, "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
