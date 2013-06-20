using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using mshtml;
using System.IO;
using System.Reflection;
using Rmilk.Properties;

namespace Rmilk
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			splitContainer1.Panel2Collapsed = true;
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			var reg = Registry.CurrentUser.CreateSubKey(@"Software\RMilk");
			Top = (int)reg.GetValue("Top", 0);
			Left = (int)reg.GetValue("Left", 0);
			Width = (int)reg.GetValue("Width", 0);
			Height = (int)reg.GetValue("Height", 0);
			if (Top > 10000 || Top<0) Top = 0;
			if (Left > 10000 || Left < 0) Left = 0;
			if (Width > 10000 || Width < 0) Width = 520;
			if (Height > 10000 || Height < 0) Height = 660;
			//script.Text = getResource("Rmilk.script.js");
			script.Text = (string)reg.GetValue("Script", getResource("Rmilk.script.js"));
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			var reg = Registry.CurrentUser.CreateSubKey(@"Software\RMilk");
			reg.SetValue("Top", Top, RegistryValueKind.DWord);
			reg.SetValue("Left", Left, RegistryValueKind.DWord);
			reg.SetValue("Width", Width, RegistryValueKind.DWord);
			reg.SetValue("Height", Height, RegistryValueKind.DWord);
			reg.SetValue("Script", script.Text, RegistryValueKind.String);
		}
		public string getResource(string name)
		{
			return new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(name)).ReadToEnd();
		}

		private void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			textBox1.Text = web.Url.ToString();
			if (!checkBox1.Checked) return;
			InjectScript(getResource("Rmilk.jquery.js") + script.Text);
		}

		private void InjectScript(string script)
		{
			HtmlElement scriptEl = web.Document.CreateElement("script");
			(scriptEl.DomElement as IHTMLScriptElement).text = "function myscript() { " + script + " }";
			web.Document.GetElementsByTagName("head")[0].AppendChild(scriptEl);
			web.Document.InvokeScript("myscript");
		}

	
		#region debug
		private void Go_Click(object sender, EventArgs e)
		{
			web.Navigate(textBox1.Text);
		}

		private void Refresh_Click(object sender, EventArgs e)
		{
			web.Refresh();
		}

		private void btHome_Click(object sender, EventArgs e)
		{
			web.Navigate("http://www.rememberthemilk.com");
		}

		int temp_counter = 0;
		private void web_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.F12)
			{
				temp_counter++;
				// This stupid event fires two times when user presses F12.
				if (temp_counter % 2 == 1) splitContainer1.Panel2Collapsed ^= true;
			}
		}
		#endregion
	}
}
