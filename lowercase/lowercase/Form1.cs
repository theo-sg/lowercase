using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lowercase
{
    public partial class Form1 : Form
    {
        bool saved = true;                 //is the file saved?
        string currentFile = "";            //curent file name

        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) => ChangesMade();
        private void newToolStripMenuItem_Click(object sender, EventArgs e) => CreateNewFile();
        private void openToolStripMenuItem_Click(object sender, EventArgs e) => OpenFile();
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) => SaveFile();
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) => SaveFileAs();
        
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if not saved, show warning message, and then close application
            if (!saved)
                if (DialogResult.OK == MessageBox.Show("You have unsaved changes. All unsaved files will be lost", "Continue?", MessageBoxButtons.OKCancel))
                    Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if not saved, show warning message, and then cancel the warning
            if (!saved)
                if (DialogResult.Cancel == MessageBox.Show("You have unsaved changes. All unsaved files will be lost", "Continue?", MessageBoxButtons.OKCancel))
                    e.Cancel = true;
        }

        /// <summary>
        /// sets title of WinForm
        /// </summary>
        /// <param name="x">the current file name</param>
        void SetTitle(string x) => this.Text = x + " - lowercase";

        /// <summary>
        /// called when RTB is updated
        /// </summary>
        void ChangesMade() => saved = false;

        /// <summary>
        /// creates a new file
        /// </summary>
        void CreateNewFile()
        {
            //if the textbox is not empty...
            if (rtb.Text != "")
            {
                if (DialogResult.OK == MessageBox.Show("You have unsaved changes. All unsaved files will be lost", "Continue?", MessageBoxButtons.OKCancel))
                {
                    rtb.Text = "";
                    currentFile = "";
                }
            }
        }

        /// <summary>
        /// opens a file using WinForms
        /// </summary>
        void OpenFile()
        {
            if (!saved)
                if (DialogResult.OK != MessageBox.Show("You have unsaved changes. All unsaved files will be lost", "Continue?", MessageBoxButtons.OKCancel))
                    return;

            //attempt to open file
            try
            {
                if (DialogResult.OK == openFileDialog.ShowDialog())
                {
                    currentFile = openFileDialog.FileName;
                    switch (Path.GetExtension(currentFile))
                    {
                        case ".txt":
                            rtb.LoadFile(currentFile, RichTextBoxStreamType.PlainText);
                            break;
                        default:
                            rtb.LoadFile(currentFile);
                            break;
                    }

                    SetTitle(currentFile);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        /// <summary>
        /// saves the current file
        /// </summary>
        void SaveFile()
        {
            if (currentFile == "")
                SaveFileAs();
            else
            {
                rtb.SaveFile(currentFile, RichTextBoxStreamType.PlainText);
                saved = true;
            }
                
        }

        /// <summary>
        /// saves the current file as
        /// </summary>
        void SaveFileAs()
        {
            if (currentFile == "")
                saveFileDialog.FileName = "New File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt")
                    rtb.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                else
                    rtb.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                currentFile = saveFileDialog.FileName;
                SetTitle(currentFile);
                saved = true;
            }
           
        }

    }
}
