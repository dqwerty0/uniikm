/*
 * Created by SharpDevelop.
 * User: Computer
 * Date: 27.10.2016
 * Time: 17:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace prog
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		public static class CallBack1
	    {
	        public static string adminname="Adm";
	        public static string adminpass="12345678";
	        public static string opername="Operator";
	        public static string operpass="12345678";
	        public static string admgroup="";
	        public static string pcusers="";
	        public static string folderName="";
	        public static string localadm="";
	        public static Int64 hwspacectrl = 2147483648;
	        public static Int64 hwspacectrl2 = 2147483648;
	        public static int f3 = 0;
	        public static int f4 = 0;
	        public static int b3 = 0;
	        public static int b4 = 0;
	        public static int b5 = 0;
	        public static int b6 = 0;
	        public static int b7 = 0;
	        public static int ntdat;
	        public static int circle = 0;
	        public static bool sh = false;
	        public static bool sh1 = false;
	        public static int c = 0;
	        public static int d = 0;
	        public static int e = 0;
	    }
		
	}
}
