using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment;
using System.Windows.Forms;
using System.Drawing;

namespace AssignmentTests
{
    [TestClass]
    public class UnitTest1
    {
        /*
         * Testing if moveto command works
         */
        [TestMethod]
        public void TestMoveTo()
        {
            TextBox txtbox = new TextBox();
            Bitmap bitmap = new Bitmap(800,600);
            txtbox.Text = "moveTo 100,250";
            ExtendedParser p = new ExtendedParser(new Panel(), bitmap);

            p.ParseCommand(txtbox.Text);
            Assert.AreEqual(100, p.dp.PenX);
            Assert.AreEqual(250, p.dp.PenY);
        }

        /*
         * Testing if fill is set to true 
         */
        [TestMethod]
        public void TestFillMode()
        {
            TextBox txtbox = new TextBox();
            Bitmap bitmap = new Bitmap(800, 600);
            txtbox.Text = "Fill on";
            ExtendedParser p = new ExtendedParser(new Panel(), bitmap);

            p.ParseCommand(txtbox.Text);
            Assert.IsTrue(p.dp.Fill);
        }

        /*
         * Testing if invalid parameter will not go through should be caught 
         */
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestDrawToInvalid()
        {
            TextBox txtbox = new TextBox();
            Bitmap bitmap = new Bitmap(800, 600);
            txtbox.Text = "drawTo 50";
            ExtendedParser p = new ExtendedParser(new Panel(), bitmap);

            p.ParseCommand(txtbox.Text);
        }
    }
}
