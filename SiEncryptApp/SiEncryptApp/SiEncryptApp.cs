using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SiEncrypt;

namespace SiEncryptApp
{
    public partial class SiEncryptApp : Form
    {
        public SiEncryptApp()
        {
            InitializeComponent();
        }

        private void SiEncryptApp_Load(object sender, EventArgs e)
        {

        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }
            else
            {
                return;
            }
            string localFileName = string.Concat(
            Environment.CurrentDirectory,
            Path.DirectorySeparatorChar,
            RegistFileHelper.ComputerInfofile);

            if (fileName != localFileName)
                File.Copy(fileName, localFileName, true);
            string computer = RegistFileHelper.ReadComputerInfoFile();
            EncryptionHelper help = new EncryptionHelper(EncryptionKeyEnum.KeyB);
            string md5String = help.GetMD5String(computer);
            string registInfo = help.EncryptString(md5String);
            RegistFileHelper.WriteRegistFile(registInfo);
            MessageBox.Show("注册码已生成");
        }
    }
}
