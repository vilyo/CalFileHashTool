using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace CalFileHashTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int FILE_MODE = 1;
        const int TEXT_MODE = 2;

        public int mode = TEXT_MODE;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }
            mode = FILE_MODE;
            txtContent.Text = openFileDialog.FileName;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (mode == FILE_MODE)
            {
                CalculateFileHash();
            }
            else if (mode == TEXT_MODE)
            {
                CalculateTextHash();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "";
            txtHash.Text = "";
        }

        private void CalculateFileHash()
        {
            if (string.IsNullOrEmpty(txtContent.Text))
            {
                MessageBox.Show("请选择文件");
                return;
            }
            StringBuilder result = new StringBuilder();
            using (FileStream fs = File.OpenRead(txtContent.Text))
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] bytes = md5.ComputeHash(fs);
                    StringBuilder hex = new StringBuilder(bytes.Length * 2);
                    foreach (byte b in bytes)
                    {
                        hex.AppendFormat("{0:x2}", b);
                    }
                    result.AppendLine("MD5: " + hex.ToString());
                }
                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] bytes = sha1.ComputeHash(fs);
                    StringBuilder hex = new StringBuilder(bytes.Length * 2);
                    foreach (byte b in bytes)
                    {
                        hex.AppendFormat("{0:x2}", b);
                    }
                    result.AppendLine("SHA1: " + hex.ToString());
                }
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(fs);
                    StringBuilder hex = new StringBuilder(bytes.Length * 2);
                    foreach (byte b in bytes)
                    {
                        hex.AppendFormat("{0:x2}", b);
                    }
                    result.AppendLine("SHA256: " + hex.ToString());
                }
            }
            txtHash.Text = result.ToString();
        }

        private void CalculateTextHash()
        {
            if (string.IsNullOrEmpty(txtContent.Text))
            {
                MessageBox.Show("请输入内容");
                return;
            }
            byte[] contentBytes = Encoding.UTF8.GetBytes(txtContent.Text);
            StringBuilder result = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(contentBytes);
                StringBuilder hex = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                result.AppendLine("MD5: " + hex.ToString());
            }
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] bytes = sha1.ComputeHash(contentBytes);
                StringBuilder hex = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                result.AppendLine("SHA1: " + hex.ToString());
            }
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(contentBytes);
                StringBuilder hex = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                result.AppendLine("SHA256: " + hex.ToString());
            }
            txtHash.Text = result.ToString();
        }
    }
}
