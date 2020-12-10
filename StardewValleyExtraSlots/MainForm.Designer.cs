namespace StardewValleyExtraSlots
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Find = new System.Windows.Forms.Button();
            this.slotBox = new System.Windows.Forms.TextBox();
            this.farmBox = new System.Windows.Forms.TextBox();
            this.farmerBox = new System.Windows.Forms.TextBox();
            this.FarmName = new System.Windows.Forms.Label();
            this.FarmerName = new System.Windows.Forms.Label();
            this.CabinNumber = new System.Windows.Forms.Label();
            this.saveComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.addComboBox = new System.Windows.Forms.ComboBox();
            this.newSlotsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Find
            // 
            this.Find.Location = new System.Drawing.Point(161, 12);
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(75, 23);
            this.Find.TabIndex = 0;
            this.Find.Text = "Find";
            this.Find.UseVisualStyleBackColor = true;
            this.Find.Click += new System.EventHandler(this.find_Click);
            // 
            // slotBox
            // 
            this.slotBox.Location = new System.Drawing.Point(216, 177);
            this.slotBox.Name = "slotBox";
            this.slotBox.ReadOnly = true;
            this.slotBox.Size = new System.Drawing.Size(100, 20);
            this.slotBox.TabIndex = 2;
            // 
            // farmBox
            // 
            this.farmBox.Location = new System.Drawing.Point(216, 135);
            this.farmBox.Name = "farmBox";
            this.farmBox.ReadOnly = true;
            this.farmBox.Size = new System.Drawing.Size(100, 20);
            this.farmBox.TabIndex = 3;
            // 
            // farmerBox
            // 
            this.farmerBox.Location = new System.Drawing.Point(216, 96);
            this.farmerBox.Name = "farmerBox";
            this.farmerBox.ReadOnly = true;
            this.farmerBox.Size = new System.Drawing.Size(100, 20);
            this.farmerBox.TabIndex = 4;
            // 
            // FarmName
            // 
            this.FarmName.AutoSize = true;
            this.FarmName.Location = new System.Drawing.Point(146, 135);
            this.FarmName.Name = "FarmName";
            this.FarmName.Size = new System.Drawing.Size(64, 13);
            this.FarmName.TabIndex = 5;
            this.FarmName.Text = "Farm Name:";
            // 
            // FarmerName
            // 
            this.FarmerName.AutoSize = true;
            this.FarmerName.Location = new System.Drawing.Point(140, 99);
            this.FarmerName.Name = "FarmerName";
            this.FarmerName.Size = new System.Drawing.Size(70, 13);
            this.FarmerName.TabIndex = 6;
            this.FarmerName.Text = "Farmer Name";
            // 
            // CabinNumber
            // 
            this.CabinNumber.AutoSize = true;
            this.CabinNumber.Location = new System.Drawing.Point(129, 177);
            this.CabinNumber.Name = "CabinNumber";
            this.CabinNumber.Size = new System.Drawing.Size(81, 13);
            this.CabinNumber.TabIndex = 7;
            this.CabinNumber.Text = "Existing Cabins:";
            // 
            // saveComboBox
            // 
            this.saveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.saveComboBox.FormattingEnabled = true;
            this.saveComboBox.Location = new System.Drawing.Point(95, 55);
            this.saveComboBox.MaxDropDownItems = 100;
            this.saveComboBox.Name = "saveComboBox";
            this.saveComboBox.Size = new System.Drawing.Size(221, 21);
            this.saveComboBox.TabIndex = 8;
            this.saveComboBox.SelectedIndexChanged += new System.EventHandler(this.saveComboBox_SelectedIndexChanged);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(161, 290);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 9;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addComboBox
            // 
            this.addComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addComboBox.Enabled = false;
            this.addComboBox.FormattingEnabled = true;
            this.addComboBox.Location = new System.Drawing.Point(216, 249);
            this.addComboBox.Name = "addComboBox";
            this.addComboBox.Size = new System.Drawing.Size(44, 21);
            this.addComboBox.TabIndex = 10;
            // 
            // newSlotsLabel
            // 
            this.newSlotsLabel.AutoSize = true;
            this.newSlotsLabel.Location = new System.Drawing.Point(140, 252);
            this.newSlotsLabel.Name = "newSlotsLabel";
            this.newSlotsLabel.Size = new System.Drawing.Size(66, 13);
            this.newSlotsLabel.TabIndex = 12;
            this.newSlotsLabel.Text = "Extra Cabins";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 354);
            this.Controls.Add(this.newSlotsLabel);
            this.Controls.Add(this.addComboBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.saveComboBox);
            this.Controls.Add(this.CabinNumber);
            this.Controls.Add(this.FarmerName);
            this.Controls.Add(this.FarmName);
            this.Controls.Add(this.farmerBox);
            this.Controls.Add(this.farmBox);
            this.Controls.Add(this.slotBox);
            this.Controls.Add(this.Find);
            this.Name = "MainForm";
            this.Text = "Stardew Valley Add Cabin Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Find;
        private System.Windows.Forms.TextBox slotBox;
        private System.Windows.Forms.TextBox farmBox;
        private System.Windows.Forms.TextBox farmerBox;
        private System.Windows.Forms.Label FarmName;
        private System.Windows.Forms.Label FarmerName;
        private System.Windows.Forms.Label CabinNumber;
        private System.Windows.Forms.ComboBox saveComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ComboBox addComboBox;
        private System.Windows.Forms.Label newSlotsLabel;
    }
}

