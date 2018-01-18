/*
 * Created by SharpDevelop.
 * User: Zalaev_ds
 * Date: 17.04.2017
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security;

namespace prog
{
	/// <summary>
	/// Description of Form5.
	/// </summary>
	public partial class Form5 : Form
	{
		public Form5()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		private void my_proc(object sender, System.EventArgs z){
				using(RegistryKey pow = Registry.Users.OpenSubKey(@"1\Control Panel\PowerCfg" , true)){
							pow.SetValue("CurrentPowerPolicy", "4", RegistryValueKind.String);
						}
		}
		void Button2Click(object sender, EventArgs e)
		{
			button2.Enabled = false;
			Process proc1 = new Process() {
                StartInfo = new ProcessStartInfo("cmd.exe", "/c tsdiscon") {
					RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
			proc1.Start();
			proc1.Close();
			button1.Enabled = true;
		}
		void Button1Click(object sender, EventArgs e)
		{
			Program.CallBack1.ntdat = 1;
			string str = @"C:\Documents and Settings\"+Program.CallBack1.opername+@"\NTUSER.DAT";
			Process proc5 = new Process() {
	                StartInfo = new ProcessStartInfo("cmd.exe", @"/c reg load HKU\1 """+str+@"""") {
						RedirectStandardOutput = true,
	                    UseShellExecute = false,
	                    CreateNoWindow = true,
	                    WindowStyle = ProcessWindowStyle.Hidden
	                    
	                }
	           		};
			proc5.EnableRaisingEvents = true;
			proc5.Exited += new EventHandler(my_proc);
			proc5.Start();
			Form6 frm = new Form6();
			frm.Show();
			this.Close();
		}
	}
}
