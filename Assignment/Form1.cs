using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class Form1 : Form
    {

        Bitmap MyBitmap;
        ExtendedParser p;

        public Form1()
        {
            InitializeComponent();
            MyBitmap = new Bitmap(GraphicsPanel.Width, GraphicsPanel.Height); //Initialising new bitmap to be drawn on
            p = new ExtendedParser(GraphicsPanel, MyBitmap); //Initialising a parser which handles parsing of the CLI and programming language
        }
        
        private void TxtBox_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (TxtBox_Input.Text.Trim() == "run")
                {
                    p.ParseTextBox(TxtBox_Commands);
                }
                else
                {
                    p.ParseCommand(TxtBox_Input.Text);
                }
            }
        } 

        private void GraphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(MyBitmap, 0, 0);
        }

    }
}
