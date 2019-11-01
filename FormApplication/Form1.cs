using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            try
            {
                ProcessStartInfo processStart = new ProcessStartInfo(@"C:\Program Files (x86)\GCTI\Workspace Desktop Edition (1)\InteractionWorkspace\InteractionWorkspace.exe");
                processStart.WorkingDirectory = @"C:\Program Files (x86)\GCTI\Workspace Desktop Edition (1)\InteractionWorkspace";
                processStart.UseShellExecute = true;
                processStart.WindowStyle = ProcessWindowStyle.Maximized;
                Process process = Process.Start(processStart);
                process.WaitForInputIdle();
                Thread.Sleep(15000);
                SetParent(process.MainWindowHandle, panel1.Handle);
                 //MessageBox.Show("Running");
            }catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}
