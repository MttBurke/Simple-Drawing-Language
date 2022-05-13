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

        /*
         * Testing and if statement, syntax is correct and x is less than 100 so statement should be valid
         */
        [TestMethod]
        public void TestIfStatementValid()
        {
            TextBox txtbox = new TextBox();
            Bitmap bitmap = new Bitmap(800, 600);
            txtbox.Text = "x = 10\r\nIf x < 100";
            ExtendedParser p = new ExtendedParser(new Panel(), bitmap);

            p.ParseTextBox(txtbox.Lines);
            Assert.IsFalse(p.EndifFound);
            Assert.IsTrue(p.IfStatementValid);
        }

        /*
         * Testing for undefined variable that is trying to be modified, should have an error in syntax
         */
        [TestMethod]
        public void TestModifyingUndefinedVariable()
        {
            TextBox txtbox = new TextBox();
            Bitmap bitmap = new Bitmap(800, 600);
            txtbox.Text = "x * 20";
            ExtendedParser p = new ExtendedParser(new Panel(), bitmap);

            p.ParseTextBox(txtbox.Lines);
            Assert.AreEqual(1, p.Errors.Count);
        }

        /*
         * Testing for creation of a new method with the name "test" and command "Circle 25"
         */
        [TestMethod]
        public void TestMakeNewMethod()
        {
            TextBox txtbox = new TextBox();
            Bitmap bitmap = new Bitmap(800, 600);
            txtbox.Text = "Method test ()\r\nCircle 25\r\nEndmethod";
            ExtendedParser p = new ExtendedParser(new Panel(), bitmap);

            p.ParseTextBox(txtbox.Lines);
            Assert.AreEqual("test", p.ListMethods[0].Name);
            Assert.AreEqual("Circle 25", p.ListMethods[0].Commands[0]);
        }

        /*
         * Testing for creation of a variable with the name "var" and value of 200
         */
        [TestMethod]
        public void TestCreatingVariable()
        {
            TextBox txtbox = new TextBox();
            Bitmap bitmap = new Bitmap(800, 600);
            txtbox.Text = "var = 200";
            ExtendedParser p = new ExtendedParser(new Panel(), bitmap);

            p.ParseTextBox(txtbox.Lines);
            Assert.AreEqual(200, p.vars["var"]);
        }

        /*
         * Testing for trying to loop with an undefined variable
         */
        [TestMethod]
        public void TestLoopUndefinedVariable()
        {
            TextBox txtbox = new TextBox();
            Bitmap bitmap = new Bitmap(800, 600);
            txtbox.Text = "Loop invalid < 200\r\nCircle invalid\r\ninvalid + 10\r\nEndloop";
            ExtendedParser p = new ExtendedParser(new Panel(), bitmap);

            p.ParseTextBox(txtbox.Lines);
            Assert.AreEqual(3, p.Errors.Count);
        }
    }
}
