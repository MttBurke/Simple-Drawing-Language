using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
                    if(p.Errors.Count == 0)
                    {
                        GraphicsPanel.Refresh();
                    }
                    else
                    {
                        foreach(string l in p.Errors)
                        {
                            ConsoleBox.AppendText(l + Environment.NewLine);
                        }
                    }
                }
                else
                {
                    p.Errors.Clear();
                    p.ParseCommand(TxtBox_Input.Text);
                    if (p.Errors.Count == 0)
                    {
                        GraphicsPanel.Refresh();
                    }
                    else
                    {
                        foreach (string l in p.Errors)
                        {
                            ConsoleBox.AppendText(l + Environment.NewLine);
                        }
                    }
                }
            }
        } 

        private void GraphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(MyBitmap, 0, 0);
        }

        private void Menu_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "txt files (*.txt)|*.txt";
            SaveFile.RestoreDirectory = true;

            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFile.FileName, TxtBox_Commands.Text);
            }
        }

        private void Menu_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog LoadFile = new OpenFileDialog();
            LoadFile.Filter = "txt files (*.txt)|*.txt";
            LoadFile.RestoreDirectory = true;

            if (LoadFile.ShowDialog() == DialogResult.OK)
            {
                TxtBox_Commands.Text = File.ReadAllText(LoadFile.FileName);
            }
        }
    }
}
