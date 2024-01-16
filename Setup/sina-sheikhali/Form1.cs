using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace sina_sheikhali
{
    public partial class Form1 : Form
    {
        public Button button_2;
  
       
        public Form1()
        {
            InitializeComponent();
      
            button_2 = button2;
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Install")
            {
                userControl31.Show();
                userControl21.Hide();
                Worker.RunWorkerAsync();
                button3.Enabled = false;
                button2.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                userControl11.Hide();
                userControl21.Show();
                button1.Enabled = true;

                button2.Text = "Install";
                button2.Enabled = false;
                userControl21.Text_b = "";
            }


      

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            userControl11.Show();
            userControl21.Hide();
            button1.Enabled = false;
            button2.Text = "Next";
            button2.Enabled = true;

        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<string, byte[]> ProgramFiles = new Dictionary<string, byte[]>
                {
                    { "MusicPlayer.exe", Properties.Resources.MusicPlayer },
                    { "Interop.WMPLib.dll", Properties.Resources.Interop_WMPLib },
                    { "AxInterop.WMPLib.dll" , Properties.Resources.AxInterop_WMPLib }
                };

            string InstallPath = userControl21.AppAddress.Text + @"\";


            int TotalBytes = 0; // All Files bytes
            foreach (var file in ProgramFiles)
            {
                TotalBytes += file.Value.Length; //Add Bytes to totalBytes
            }

            if (InstallPath.Length > 3 && !Directory.Exists(InstallPath))
                Directory.CreateDirectory(InstallPath); // Check if Directory is not exist Create New!

            int TotalBytesWritten = 0;

            foreach (var PFile in ProgramFiles)
            {

                string FilePath = InstallPath + PFile.Key;
                byte[] Data = PFile.Value;
                int BytesWritten = 0;
                int BufferSize = 5; //Writerbuffer 4096
                int FileBytes = PFile.Value.Length; // Get a File Bytes 

                using (FileStream fs = new FileStream(FilePath, FileMode.Create))
                {
                    while (BytesWritten < FileBytes)
                    {
                        int BytesToWrite = Math.Min(BufferSize, Data.Length - BytesWritten);
                        fs.Write(Data, BytesWritten, BytesToWrite);
                        BytesWritten += BytesToWrite;
                        TotalBytesWritten += BytesToWrite;

                        int ProgressPercentage = ((TotalBytesWritten * 100) / TotalBytes);

                        MethodInvoker methodInvokerDelegate = delegate ()
                        { userControl31.InstallProgressBar.Value = ProgressPercentage; };
                        if (this.InvokeRequired)
                            this.Invoke(methodInvokerDelegate);


                    }
                }


            }


        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string Filepath = userControl21.AppAddress.Text + @"\MusicPlayer.exe";
            string shortCutName = "MusicPlayer.lnk";


            if(userControl21.Shortcut.Checked)
            {
                CreateShortcut(Filepath, shortCutName);
            }


            if(userControl21.Default.Checked) {
                string[] Formats = { ".mp3" };
                string AppPath = userControl21.AppAddress.Text + @"\MusicPlayer.exe";
                string ProgramID = "MusicPlayer.exe";

                ChangeDefaultAppinReg(Formats, AppPath, ProgramID);

            }


        }

        void CreateShortcut(string path,string shortcut_name)
        {
            try
            {
                object shDesktop = (object)"Desktop";
                WshShell shell = new WshShell();
                string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) +@"\"+ shortcut_name;
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.TargetPath = path;
                shortcut.Save();
            }
            catch (System.Exception ex)
            {

            }
        }

        void ChangeDefaultAppinReg(string[] Formats,string AppPath,string ProgramID)
        {
            foreach (string Format in Formats)
            {
              
                Registry.ClassesRoot.OpenSubKey(Format,true).SetValue("", ProgramID);

                Registry.ClassesRoot.CreateSubKey(ProgramID + "\\shell\\open\\command", true).SetValue("",AppPath+" %1");
                Registry.ClassesRoot.CreateSubKey(ProgramID + "\\Applications\\shell\\open\\command", true).SetValue("", AppPath + " %1");

                
            }
          

        }

    }
}
