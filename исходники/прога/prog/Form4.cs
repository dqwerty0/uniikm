/*
 * Created by SharpDevelop.
 * User: zalaev_ds
 * Date: 06.04.2017
 * Time: 14:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace prog
{
	/// <summary>
	/// Description of Form4.
	/// </summary>
	public partial class Form4 : Form
	{
		public Form4()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button2Click(object sender, EventArgs e)
		{

		}
		void Button1Click(object sender, EventArgs e)
		{
			Program.CallBack1.b6 = 1;
			this.Close();
		}
		void PictureBox2Click(object sender, EventArgs e)
		{
	
		}
		void Form4FormClosing(object sender, FormClosingEventArgs e)
		{
			
		}
		void Form4Load(object sender, EventArgs e)
		{
			for(int i =0;i<100;i++){
				progressBar1.Value = progressBar1.Value +1;
			}
			if(progressBar1.Value == 100){
				label1.Visible = true;
				button1.Enabled = true;
			}
		}
	}
}
