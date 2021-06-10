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

        private String textObj;
        private Int32 wordCount;
        private readonly String[] separatingStrings = { " ", "\r\n", "\r", "\n", "\t" };
        private readonly String[] permMarks = { ",", ".", "，", "．", "、", "。" };

        public Form1()
        {
            InitializeComponent();

            this.Font = SystemFonts.IconTitleFont;
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Window)
            {
                this.Font = SystemFonts.IconTitleFont;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
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

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            label2.Text = "文字数";
            TextBox1_TextChanged(sender, e);
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            label2.Text = "単語数";
            TextBox1_TextChanged(sender, e);
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            TextBox1_TextChanged(sender, e);
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            TextBox1_TextChanged(sender, e);
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
