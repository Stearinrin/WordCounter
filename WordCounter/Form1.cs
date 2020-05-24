using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;


namespace WordCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Font = SystemFonts.IconTitleFont;
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private String textObj;
        private Int32 wordCount;
        private String[] separatingStrings = { " ", "\r\n", "\r", "\n", "\t" };
        private String[] permMarks = { ",", ".", "，", "．", "、", "。" };

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            wordCount = 0;
            textObj = textBox1.Text;

            if (checkBox2.Checked)
            {
                wordCount += textObj.Length - textObj.Replace("\t", "").Length;
                
            }

            if (radioButton1.Checked)
            {
                textObj  = textObj.Replace(separatingStrings, "").Trim();
                
                if (!checkBox1.Checked)
                {
                    textObj = textObj.Replace(permMarks, "");
                }

                wordCount += textObj.Length;
            }
            else
            {
                Debug.Assert(radioButton2.Checked);
                wordCount += textObj.Trim().Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries).Length;
            }

            textBox2.Text = wordCount.ToString();
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Window)
            {
                this.Font = SystemFonts.IconTitleFont;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            textBox1_TextChanged(sender, e);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            textBox1_TextChanged(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }
    }

    static class StringExtensions
    {
        public static string Replace(this string str, string[] oldStrings, string newString)
        {
            foreach (string s in oldStrings)
            {
                str = str.Replace(s, newString);
            }

            return str;
        }
    }
}
