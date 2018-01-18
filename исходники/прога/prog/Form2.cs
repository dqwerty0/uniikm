/*
 * Created by SharpDevelop.
 * User: Zalaev_ds
 * Date: 05.04.2017
 * Time: 10:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace prog
{
	/// <summary>
	/// Description of Form2.
	/// </summary>
	public partial class Form2 : Form
	{
		public Form2()
		{	
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			Program.CallBack1.adminname = textBox1.Text;
			Program.CallBack1.adminpass = textBox2.Text;
			Program.CallBack1.opername = textBox4.Text;
			Program.CallBack1.operpass = textBox3.Text;
			Program.CallBack1.hwspacectrl = Convert.ToInt64(textBox5.Text)*1073741824;
			Program.CallBack1.hwspacectrl2 = Convert.ToInt64(textBox6.Text)*1073741824;
			this.Close();
		}
		void Form2FormClosing(object sender, FormClosingEventArgs e)
		{
			Program.CallBack1.f4=0;
		}
		void Form2Load(object sender, EventArgs e)
		{
			textBox1.Text = Program.CallBack1.adminname;
			textBox2.Text = Program.CallBack1.adminpass;
			textBox4.Text = Program.CallBack1.opername;
			textBox3.Text = Program.CallBack1.operpass;
			textBox5.Text = Convert.ToString(Program.CallBack1.hwspacectrl/1073741824);
			textBox6.Text = Convert.ToString(Program.CallBack1.hwspacectrl2/1073741824);
		}
		void TextBox5TextChanged(object sender, EventArgs e)
		{
		
		}
	}
}
