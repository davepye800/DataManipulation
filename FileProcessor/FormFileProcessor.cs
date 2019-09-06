using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileProcessing.Business;
using FileProcessing.File;

namespace FileProcessor
{
    public partial class FormFileProcessor : Form
    {
        public FormFileProcessor()
        {
            InitializeComponent();
        }

        private void btnProcessFile_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessLogic bl = new BusinessLogic();
                if (bl.ProcessClaims(txtFile.Text
                                , txtOutput.Text
                                , new ClaimsFile()))
                {
                    txtFeedback.Text = "Processing completed sccuessfully";
                }
            }
            catch (Exception ex)
            {
                txtFeedback.Text = ex.ToString();
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse Text Files",
                CheckFileExists = true,
                CheckPathExists = true,

            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = openFileDialog1.FileName;
                int lastIndexOfBackSlash = openFileDialog1.FileName.LastIndexOf('\\');
                txtOutput.Text = openFileDialog1.FileName.Substring(0, lastIndexOfBackSlash) + "\\output.csv";
            }
        }
    }
}
