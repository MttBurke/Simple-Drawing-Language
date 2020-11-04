namespace Assignment
{
    partial class Form1
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
            this.TxtBox_Input = new System.Windows.Forms.TextBox();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuBar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.TxtBox_Commands = new System.Windows.Forms.TextBox();
            this.GraphicsPanel = new System.Windows.Forms.Panel();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtBox_Input
            // 
            this.TxtBox_Input.Location = new System.Drawing.Point(594, 27);
            this.TxtBox_Input.Name = "TxtBox_Input";
            this.TxtBox_Input.Size = new System.Drawing.Size(190, 20);
            this.TxtBox_Input.TabIndex = 0;
            this.TxtBox_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBox_Input_KeyDown);
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(788, 24);
            this.MenuStrip1.TabIndex = 1;
            this.MenuStrip1.Text = "menuStrip1";
            // 
            // MenuBar
            // 
            this.MenuBar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Save,
            this.Menu_Load});
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(50, 20);
            this.MenuBar.Text = "Menu";
            // 
            // Menu_Save
            // 
            this.Menu_Save.Name = "Menu_Save";
            this.Menu_Save.Size = new System.Drawing.Size(180, 22);
            this.Menu_Save.Text = "Save";
            this.Menu_Save.Click += new System.EventHandler(this.Menu_Save_Click);
            // 
            // Menu_Load
            // 
            this.Menu_Load.Name = "Menu_Load";
            this.Menu_Load.Size = new System.Drawing.Size(180, 22);
            this.Menu_Load.Text = "Load";
            this.Menu_Load.Click += new System.EventHandler(this.Menu_Load_Click);
            // 
            // TxtBox_Commands
            // 
            this.TxtBox_Commands.Location = new System.Drawing.Point(594, 54);
            this.TxtBox_Commands.Multiline = true;
            this.TxtBox_Commands.Name = "TxtBox_Commands";
            this.TxtBox_Commands.Size = new System.Drawing.Size(190, 507);
            this.TxtBox_Commands.TabIndex = 2;
            // 
            // GraphicsPanel
            // 
            this.GraphicsPanel.BackColor = System.Drawing.Color.White;
            this.GraphicsPanel.Location = new System.Drawing.Point(0, 27);
            this.GraphicsPanel.Name = "GraphicsPanel";
            this.GraphicsPanel.Size = new System.Drawing.Size(588, 534);
            this.GraphicsPanel.TabIndex = 3;
            this.GraphicsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphicsPanel_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 561);
            this.Controls.Add(this.GraphicsPanel);
            this.Controls.Add(this.TxtBox_Commands);
            this.Controls.Add(this.TxtBox_Input);
            this.Controls.Add(this.MenuStrip1);
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtBox_Input;
        private System.Windows.Forms.MenuStrip MenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuBar;
        private System.Windows.Forms.ToolStripMenuItem Menu_Save;
        private System.Windows.Forms.ToolStripMenuItem Menu_Load;
        private System.Windows.Forms.TextBox TxtBox_Commands;
        private System.Windows.Forms.Panel GraphicsPanel;
    }
}

