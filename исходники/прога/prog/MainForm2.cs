/*
 * Created by SharpDevelop.
 * User: Computer
 * Date: 27.10.2016
 * Time: 17:57
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
using System.Management;

namespace prog
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{	
		public class MyToolStripRenderer : System.Windows.Forms.ToolStripProfessionalRenderer
		{
		    public MyToolStripRenderer()
		    {
		        this.RoundedEdges = false;
		    }
		}	
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			toolStrip1.Renderer = new MyToolStripRenderer();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		private void my_proc1(object sender, System.EventArgs z){
			RegistryKey pow = Registry.Users.OpenSubKey(@"1\Control Panel\PowerCfg");
	        var scheme = pow.GetValue("CurrentPowerPolicy");
	         if(!scheme.ToString().Contains("4")){
	            	checkBox6.Checked = true;
	            	Program.CallBack1.b7++;
	         }else{
	            	checkBox6.Checked = false;
	         }
	        pow.Close();
		}
		private void localadm(object sender, System.EventArgs z){
			Process localadmpas = new Process() {
            StartInfo = new ProcessStartInfo("cmd.exe", @"/c net user "+Program.CallBack1.localadm+" "+Program.CallBack1.adminpass) {
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           	};
			localadmpas.Start();
			localadmpas.Close();
		}
		private void admgroup(object sender, System.EventArgs z){
			Process proc33 = new Process() {
            StartInfo = new ProcessStartInfo("cmd.exe", @"/c net localgroup "+Program.CallBack1.admgroup+" "+Program.CallBack1.adminname+" /add") {
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           	};
			proc33.EnableRaisingEvents = true;
			proc33.Exited += new EventHandler(localadm);
			proc33.Start();
		}
		void Button1Click(object sender, EventArgs e)
		{
			Program.CallBack1.b7 = 0;
			Cursor.Current = Cursors.WaitCursor;
			Process proc2 = new Process() {
              	StartInfo = new ProcessStartInfo(@"cmd.exe", @"/c net user") { 
					RedirectStandardOutput = true,
					StandardOutputEncoding = Encoding.GetEncoding(866), //считывание пользователей настраиваемого компьютера
                    UseShellExecute = false,							//по средствам команды cmd - net user
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           		};
            proc2.Start() ;
            StreamReader upusers = proc2.StandardOutput;
            progressBar2.Value = 30;
            Program.CallBack1.pcusers = upusers.ReadToEnd();
            proc2.Close();
           
            Process proc11 = new Process() {
              	StartInfo = new ProcessStartInfo("cmd.exe", @"/c wmic group where ""SID = 'S-1-5-32-544'"" get name") {
            		RedirectStandardOutput = true,						 //идентификация группы администраторов ПК, необходима для
            		StandardOutputEncoding = Encoding.GetEncoding(866),  //корректноой проверки на наличие уч. записи администратора
                    UseShellExecute = false,							 //как в анг. версии Windows так и в русиф.
                    CreateNoWindow = true,								
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           		};
            proc11.Start();
            StreamReader adgroup = proc11.StandardOutput;
            String admgroup1 = adgroup.ReadToEnd();
            if(!admgroup1.Contains("Администраторы")){
            	Program.CallBack1.admgroup="Administrators"; 
            	Program.CallBack1.localadm="Administrator"; 									            	
            }else{
            	Program.CallBack1.admgroup="Администраторы";
            	Program.CallBack1.localadm="Администратор";
            }
            proc11.Close();
            
				ManagementScope namespaceScope = new ManagementScope("\\\\.\\ROOT\\CIMV2");
				ObjectQuery diskQuery = new ObjectQuery("SELECT * FROM Win32_LogicalDisk"); //выполнение sql подобного запроса для получения коллекции
				ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(namespaceScope, diskQuery);//с информацией о логических дисках
				ManagementObjectCollection colDisks = mgmtObjSearcher.Get();
			
			foreach (ManagementObject objDisk in colDisks)
			{      
				if(Convert.ToString(objDisk["DeviceID"])=="C:" && Convert.ToUInt32(objDisk["DriveType"])==3){//проверка диска C
					if(Convert.ToString(objDisk["FileSystem"])=="NTFS")			
						pictureBox13.Visible = true;
					else
						pictureBox12.Visible = true;
					if(Convert.ToInt64(objDisk["FreeSpace"])>Program.CallBack1.hwspacectrl)
						pictureBox19.Visible = true;
					else
						pictureBox18.Visible = true;
					pictureBox7.Visible = true;
					Program.CallBack1.c++;
				}
				if(Convert.ToString(objDisk["DeviceID"])=="D:" && Convert.ToUInt32(objDisk["DriveType"])==3){//проверка диска D
					if(Convert.ToString(objDisk["FileSystem"])=="NTFS")	
						pictureBox15.Visible = true;
					else
						pictureBox14.Visible = true;
					if(Convert.ToInt64(objDisk["FreeSpace"])>Program.CallBack1.hwspacectrl)
						pictureBox17.Visible = true;
					else
						pictureBox16.Visible = true;
					pictureBox9.Visible = true;
					Program.CallBack1.d++;
				}
				if(Convert.ToString(objDisk["DeviceID"])=="E:"){ //проверка дисковода E
					if(Convert.ToUInt32(objDisk["DriveType"])==5)
						pictureBox11.Visible=true;
					else
						pictureBox10.Visible = true;
					Program.CallBack1.e++;
				}
			}
			if (Program.CallBack1.c==0){
				pictureBox6.Visible = true;
           		pictureBox12.Visible = true;
           		pictureBox18.Visible = true;	
			}
			if (Program.CallBack1.d==0){
				pictureBox8.Visible = true;
           		pictureBox14.Visible = true;
           		pictureBox16.Visible = true;
			}
            if (Program.CallBack1.e==0){
				pictureBox10.Visible = true;
			}
    
           	RegistryKey upkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update");  
			var update = upkey.GetValue("AUOptions");    //функция для проверки реестрового параметра автообновлений
			if (update == null||Convert.ToInt32(update)!=1){
           	checkBox3.Checked = true;
           	Program.CallBack1.b7++;
			}else{
				checkBox3.Checked = false;
			}
			upkey.Close();
			
			RegistryKey autorunkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", true);	
            string[] arrrunk = autorunkey.GetSubKeyNames();
           	for(int i=0;i<arrrunk.Length;i++){
           	if(arrrunk[i]=="Explorer"){      //проверка на наличие ключа реестра explorer если он отстутствует то создаем
           		Program.CallBack1.sh1 = true;
           	}
           	}
            if (Program.CallBack1.sh1 == false){
            	autorunkey.CreateSubKey("Explorer");
            	checkBox4.Checked = true;
            	Program.CallBack1.b7++;
            }else{
           	RegistryKey autorunn = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer");
            var ak = autorunn.GetValue("NoDriveTypeAutoRun"); //проверка параметра автозагрузки
			if (ak==null||Convert.ToInt32(ak)!=255){
           	checkBox4.Checked = true;
           	Program.CallBack1.b7++;
			}else{
				checkBox4.Checked = false;
			}
			autorunn.Close();
            }
			autorunkey.Close();
			

            if(OSVersionInfo.Name.Contains("7")){   //необходимая проверка на версию windows необходима для корректной работы как в win 7 так и XP
           	Process proc100 = new Process() {
                StartInfo = new ProcessStartInfo("cmd.exe", "/c tzutil /g") { //проверка часового пояса в Win 7  проверяем по средствам tzutil
					RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
			proc100.Start();
            StreamReader srIncoming3 = proc100.StandardOutput;
            String utc = srIncoming3.ReadToEnd();
            if (utc.Contains("Ekaterinburg Standard Time")){ 
            	checkBox5.Checked = false;
            }else{
            	checkBox5.Checked = true;
            	Program.CallBack1.b7++;
            }

            RegistryKey powerscheme = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Power\User\PowerSchemes");
           	if(powerscheme!=null){
           		var sch = powerscheme.GetValue("ActivePowerScheme");  //проверка схемы электропитания в Win7 просто по коду схемы
           		if(!Convert.ToString(sch).Contains("8c5e7fda")){
           		checkBox6.Checked = true;	
           		Program.CallBack1.b7++;
           		}else{
           		checkBox6.Checked = false;
           		}
           	}
           
           	RegistryKey shutdownkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT",true);			
            string[] arr = shutdownkey.GetSubKeyNames();
           	for(int i=0;i<arr.Length;i++){
           	if(arr[i]=="Reliability"){      //проверка ключа Reliablity если нет то создаем
           		Program.CallBack1.sh = true;
           	}
           	}
            if (Program.CallBack1.sh == false){
            	shutdownkey.CreateSubKey("Reliability"); //если ключ отсутсвовал у парметра исп. диалога при зав. работы параметр неуд.
            	checkBox7.Checked = true;
            	Program.CallBack1.b7++;
            }else{
           	RegistryKey created = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\Reliability");
            var shutdown = created.GetValue("ShutdownReasonOn"); //проверка реестрового параметра диалога завершения работы 
			var shutdownUI = created.GetValue("ShutdownReasonUI");
			if ((shutdown==null||Convert.ToInt32(shutdown)!=1)&&(shutdownUI==null||Convert.ToInt32(shutdownUI)!=1)){
           	checkBox7.Checked = true;
           	Program.CallBack1.b7++;
			}else{
				checkBox7.Checked = false;
			}
			created.Close();
            }
			shutdownkey.Close();
			}else if(OSVersionInfo.Name.Contains("XP")){ //проверка на XP
				TimeZoneInfo local = TimeZoneInfo.Local;
					if (local.DisplayName.Contains("Екат")||local.DisplayName.Contains("Ekat")){//проверка часового пояса по средствам функции c#
					checkBox5.Checked = false;
            		}else{
            		checkBox5.Checked = true;
            		Program.CallBack1.b7++;
					}	
				string ntdatFile = @"C:\Documents and Settings\"+Program.CallBack1.opername+@"\NTUSER.DAT"; //проверка на наличие NTUSER.dat оператора 
				if(!File.Exists(ntdatFile)){															
					checkBox6.Checked = true;
					Program.CallBack1.b7++;
					Program.CallBack1.ntdat = 0;
				}else{
					Program.CallBack1.ntdat = 1;
					string str = @"C:\Documents and Settings\"+Program.CallBack1.opername+@"\NTUSER.DAT";//если NTUSER.dat оператора сущ. то вгружаем в реестр
					Process proc5 = new Process() {
	                StartInfo = new ProcessStartInfo("cmd.exe", @"/c reg load HKU\1 """+str+@"""") {
						RedirectStandardOutput = true,
	                    UseShellExecute = false,
	                    CreateNoWindow = true,
	                    WindowStyle = ProcessWindowStyle.Hidden
	                    
	                }
	           		};
					proc5.EnableRaisingEvents = true;
					proc5.Exited += new EventHandler(my_proc1);
	            	proc5.Start();
				}	
				checkBox7.Enabled = false;
            }
			
           	progressBar2.Value = 100;
           	label17.Visible = true;
			label17.Text = "Тестирование прошло успешно!";
			if(!Program.CallBack1.pcusers.Contains(Program.CallBack1.opername)){
				checkBox2.Checked = true;
				Program.CallBack1.b7++;
			}else{
				checkBox2.Checked = false;
			}
           	if(!Program.CallBack1.pcusers.Contains(Program.CallBack1.adminname)){
           		checkBox1.Checked = true;
           		Program.CallBack1.b7++;
			}else{
				checkBox1.Checked = false;
			}
			if(progressBar2.Value == 100 && Program.CallBack1.admgroup != ""){
				if(Program.CallBack1.b7!=0){
				button3.Enabled = true;
				groupBox1.Enabled =true;
				}else{
					Form1 frm1 = new Form1();
					frm1.Show();
					groupBox1.Enabled = false;
					button3.Enabled = false;
				}
			}
		}
		void Button3Click(object sender, EventArgs e)
		{	
			button3.Enabled=false;
			groupBox1.Enabled = false;
			if(!OSVersionInfo.Name.Contains("XP")){
			Form4 frm4 = new Form4();
			frm4.Show();
			}
			Cursor.Current = Cursors.WaitCursor;
            if(checkBox1.Checked == true){
            Process proc3 = new Process() {
               	StartInfo = new ProcessStartInfo("cmd.exe", @"/c net user "+Program.CallBack1.adminname+" "+Program.CallBack1.adminpass+" /add") { 
                    UseShellExecute = true,					//функция создания профиля администратора по средствам команды cmd - net user
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           	};
			proc3.EnableRaisingEvents = true;
			proc3.Exited += new EventHandler(admgroup);
			proc3.Start();
            }
			if(checkBox2.Checked == true){
            Process proc4 = new Process() {
               	StartInfo = new ProcessStartInfo("cmd.exe", @"/c net user "+Program.CallBack1.opername+" "+Program.CallBack1.operpass+" /add") { 
                    UseShellExecute = true,				//функция создания профиля пользователя по средствам команды cmd - net user
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           		};
            proc4.Start();
            proc4.Close();
            }
			if(checkBox3.Checked == true){
				using (RegistryKey uved1221 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update", true)){ 
				uved1221.SetValue("AUOptions", 1, RegistryValueKind.DWord); //отключение автообновлений
				}
			}
			if(checkBox4.Checked == true){
				using (RegistryKey uved1221 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", true)){ 
				uved1221.SetValue("NoDriveTypeAutoRun", 255, RegistryValueKind.DWord); //отключение автозагрузки
				}
			}
			if(checkBox5.Checked == true){
				if(OSVersionInfo.Name.Contains("XP")){
				Process proc2 = new Process() {
                StartInfo = new ProcessStartInfo("cmd.exe", @"/c control.exe TIMEDATE.CPL,,/Z Ekaterinburg Standard Time") { //смена часового пояса win XP
					RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
              		}
            	};
				proc2.Start();
				proc2.Close();
				}else{
				Process proc2 = new Process() {
                StartInfo = new ProcessStartInfo("cmd.exe", @"/c tzutil /s ""Ekaterinburg Standard Time""") { //смена часового пояса win 7
					RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
              		}
            	};
				proc2.Start();
				proc2.Close();
				using(RegistryKey tz = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\TimeZoneInformation",true)){ 
					tz.SetValue("DisableAutoDaylighTimeSet", 1 , RegistryValueKind.DWord);
				}
			}
			}
			if(checkBox7.Checked == true){
				using (RegistryKey uved1221 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\Reliability", true)){ 
				uved1221.SetValue("ShutdownReasonOn", 1, RegistryValueKind.DWord); //включение функции использования диалога зав. работы
				uved1221.SetValue("ShutdownReasonUI", 1, RegistryValueKind.DWord);
				}
			}	
			if(checkBox6.Checked == true){
				if(OSVersionInfo.Name.Contains("XP")){
					if(Program.CallBack1.ntdat == 1){
						using(RegistryKey pow = Registry.Users.OpenSubKey(@"1\Control Panel\PowerCfg" , true)){ //настройка схемы электропитания XP
							pow.SetValue("CurrentPowerPolicy", "4", RegistryValueKind.String);
						}
					}else{
						Form5 frm = new Form5();
						frm.Show();
					}
				}else{
				Process proc48 = new Process() {
               	StartInfo = new ProcessStartInfo("cmd.exe", @"/c POWERCFG -SETACTIVE 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c") {  
                    UseShellExecute = true,											//настройка схемы электропитания win7
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           		};
            	proc48.Start();
            	proc48.Close();
				
			Process proc45 = new Process() {
               	StartInfo = new ProcessStartInfo("cmd.exe", @"/c POWERCFG /X /MONITOR-TIMEOUT-AC 0") { //отключение прехода монитора в жд. режим
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           		};
            proc45.Start();
            proc45.Close();
			Process proc44 = new Process() {
               	StartInfo = new ProcessStartInfo("cmd.exe", @"/c POWERCFG /X /STANDBY-TIMEOIT-AC 0") { //отключение перехода пк в жд. режима
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
           		};
            proc44.Start();
            proc44.Close();	
			}
			}	
		}
		void ToolStripLabel1Click(object sender, EventArgs e)
		{
			if (Program.CallBack1.f4==0){
			Program.CallBack1.f4=1;
			Form2 form1 = new Form2();
			form1.Show();
			}
		}
		void HelpToolStripButtonClick(object sender, EventArgs e)
		{
			if(Program.CallBack1.f3 == 0){
			Form3 form = new Form3();
			form.Show();
			Program.CallBack1.f3 = 1;
			}
		}
		void MainFormActivated(object sender, EventArgs e)
		{
			label1.Text = Convert.ToString(Program.CallBack1.hwspacectrl/1073741824)+" Гб";
			label4.Text = Convert.ToString(Program.CallBack1.hwspacectrl2/1073741824)+" Гб";
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			label3.Text = label3.Text +" "+ OSVersionInfo.Name;
			label15.Text = label15.Text +" "+ OSVersionInfo.Edition;
			label20.Text = label20.Text +" "+ OSVersionInfo.ServicePack;
			label19.Text = label19.Text +" "+ OSVersionInfo.Version;
			label18.Text = label18.Text +" "+ OSVersionInfo.ProcessorBits;
			label21.Text = label21.Text +" "+ OSVersionInfo.OSBits;
			label1.Text = Convert.ToString(Program.CallBack1.hwspacectrl/1073741824)+" Гб";
			label4.Text = Convert.ToString(Program.CallBack1.hwspacectrl2/1073741824)+" Гб";
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
		    const string message = "Вы действительно хотите закрыть программу?";
   			const string caption = "Подтверждение";
   			var result = MessageBox.Show(message, caption,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
    		if (result == DialogResult.No)
    		{
        		e.Cancel = true;
    		}else{
    			Process proc444 = new Process() {
			       StartInfo = new ProcessStartInfo("cmd.exe", @"/c reg unload HKU\1") {
						RedirectStandardOutput = true,
			            UseShellExecute = false,
			            CreateNoWindow = true,
			            WindowStyle = ProcessWindowStyle.Hidden
			      	}
			      };
			    proc444.Start();
				proc444.Close();
    		}
  		}
		void FolderBrowserDialog1HelpRequest(object sender, EventArgs e)
		{
	
		}
		void Button2Click(object sender, EventArgs e)
		{
		DialogResult result = folderBrowserDialog1.ShowDialog();
        if( result == DialogResult.OK )
        {
        	button4.Enabled = true;
            Program.CallBack1.folderName = folderBrowserDialog1.SelectedPath;
            textBox1.Enabled = true;
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }
		}
		void CheckBox8CheckedChanged(object sender, EventArgs e)
		{
			if(checkBox8.Checked == true){
				if(OSVersionInfo.Name.Contains("XP")){
				textBox1.Text = "Укажите путь к папке Default user";
				}else{
				textBox1.Text = "Укажите путь к папке Default";
				}
				button2.Enabled = true;
			}else{
				button4.Enabled = false;
				button2.Enabled = false;
				textBox1.Enabled = false;
				textBox1.Text = " ";
			}
		}
		void Button4Click(object sender, EventArgs e)
		{
				checkBox8.Enabled = false;
				checkBox8.Checked = false;
				if(OSVersionInfo.Name.Contains("7")){
					Process proc44 = new Process() {
               		StartInfo = new ProcessStartInfo("cmd.exe", @"/c xcopy "+Program.CallBack1.folderName+@" C:\Users\Default /H /Y /C /R /S") {
                    	UseShellExecute = true,
                    	CreateNoWindow = true,
                   		WindowStyle = ProcessWindowStyle.Hidden
                	}
           			};
            		proc44.Start();
            		proc44.Close(); 
            		Form7 frm12 = new Form7();
					frm12.Show();
				}else if(OSVersionInfo.Name.Contains("XP")){
					Process proc44 = new Process() {
               		StartInfo = new ProcessStartInfo("cmd.exe", @"/c xcopy "+Program.CallBack1.folderName+@" ""C:\Documents and Settings\Default User"" /H /Y /C /R /S") {
                    	UseShellExecute = true,
                    	CreateNoWindow = true,
                   		WindowStyle = ProcessWindowStyle.Hidden
                	}
           			};
            		proc44.Start();	
					proc44.Close();   
					Form7 frm12 = new Form7();
					frm12.Show();					
				}
			}
		}
	}
