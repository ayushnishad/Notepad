using NotepadLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadApp
{
    public partial class MainForm : Form
    {
        FileOperation fileOperation;
        public MainForm()
        {
            InitializeComponent();
            fileOperation = new FileOperation();
           // fileOperation.InitializeNewFile();
            this.Text = fileOperation.Filename;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            fileOperation.IsFileSaved = false;
            UpdateView();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileOperation.IsFileSaved)
            {
                txtArea.Text = "saved";
                fileOperation = new FileOperation();
                UpdateView();
            }
            else
            {
                DialogResult res =  MessageBox.Show("Do you want to save the change?\n ","Notepad",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    
                if(res == DialogResult.Yes)
                {
                    if(fileOperation.Filename.Contains("ayush_test"))
                    {
                        SaveFileDialog newFileSave = new SaveFileDialog();
                        newFileSave.Filter = "Text(*.txt)|*.txt";
                        if(newFileSave.ShowDialog() == DialogResult.OK)
                        {
                            fileOperation.Savefile(newFileSave.FileName , txtArea.Lines);
                            UpdateView();
                        }
                        else
                        {
                            fileOperation.Savefile(fileOperation.FileLocation, txtArea.Lines);
                            UpdateView();
                        }
                    }
                }
                if(res == DialogResult.No)
                {
                    txtArea.Text = "";
                    fileOperation = new FileOperation();
                    UpdateView();
                }
            }
        }

        private void UpdateView()
        {
            this.Text = (!fileOperation.IsFileSaved ? fileOperation.Filename + "*" : fileOperation.Filename);
            //throw new NotImplementedException();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!fileOperation.IsFileSaved)
            {
                // already saved (file created)
                if(!this.Text.Contains("ayush_test.txt"))
                {
                    fileOperation.Savefile(fileOperation.FileLocation, txtArea.Lines);
                    UpdateView();
                }
                else
                {
                    SaveFile();
                }
            }
        }

        private void SaveFile()
        {
            SaveFileDialog fileSave = new SaveFileDialog();
            fileSave.Filter = "Text(*.txt)|*.txt";
            if (fileSave.ShowDialog() == DialogResult.OK)
            {
                fileOperation.Savefile(fileSave.FileName, txtArea.Lines);
                UpdateView();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text(*.txt)|*.txt";
            openFile.InitialDirectory = "D:";
            openFile.Title = "Open file ";
            if(openFile.ShowDialog() ==  DialogResult.OK)
            {
               txtArea.TextChanged -= richTextBox1_TextChanged;
                txtArea.Text = fileOperation.OpenFile(openFile.FileName);
                txtArea.TextChanged += richTextBox1_TextChanged;
                UpdateView();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutTool.Enabled = txtArea.SelectedText.Length > 0 ? true : false;
            copyTool.Enabled = txtArea.SelectedText.Length > 0 ? true : false;
            pasteTool.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Text);
        }

        private void editToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            editToolStripMenuItem_Click(sender, e);
        }

        private void cutTool_Click(object sender, EventArgs e)
        {
            txtArea.Cut();
            pasteTool.Enabled = true;
        }

        private void copyTool_Click(object sender, EventArgs e)
        {
            txtArea.Copy();
            pasteTool.Enabled = true;
        }

        private void pasteTool_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text)) 
            txtArea.Paste();
        }
    }
}
