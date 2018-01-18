/*
 * Created by SharpDevelop.
 * User: Zalaev_ds
 * Date: 05.04.2017
 * Time: 14:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace prog
{
	/// <summary>
	/// Description of Form3.
	/// </summary>
	public partial class Form3 : Form
	{
		public Form3()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Form3FormClosing(object sender, FormClosingEventArgs e)
		{
			Program.CallBack1.f3 = 0;
		}
		void Label6Click(object sender, EventArgs e)
		{
	
		}
	}
}
