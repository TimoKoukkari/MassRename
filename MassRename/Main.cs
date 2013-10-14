using System;
using System.Text;
using System.Windows.Forms;

namespace MassRename
{
    public partial class Form1 : Form
    {
        private String oldText = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            // Read the files
            foreach (String file in openFileDialog1.FileNames)
            {
                listBox1.Items.Add(System.IO.Path.GetFileName(file));
                listBox2.Items.Add(System.IO.Path.GetFileName(file));
            }
            textBox1.Text = LongestCommonSubstring(listBox1.Items);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; ++i) {
                if (oldText.Length > 0) {
                    listBox2.Items[i] = listBox2.Items[i].ToString().Replace(oldText, textBox1.Text);
                }
            }
            oldText = textBox1.Text;
        }
        
        public static string LongestCommonSubstring(ListBox.ObjectCollection values)
        {
            string result = string.Empty;

            // Compare first to all the others
            for (int i=1; i<values.Count; ++i) {
                string tmp = "";
                if (LongestCommonSubstring(values[0].ToString(), values[i].ToString(), out tmp) > result.Length) {
                    
                    // Result must be in all strings
                    bool inAll = true;
                    for (int j=1; j<values.Count; ++j) {
                        if (values[j].ToString().Contains(tmp) == false) {
                            inAll = false;
                            break;
                        }
                    }
                    if (inAll)
                        result = tmp;
                }
            }
            return result;
        }

        // Source: http://en.wikibooks.org/wiki/Algorithm_Implementation/Strings/Longest_common_substring
        public static int LongestCommonSubstring(string str1, string str2, out string sequence)
        {
            sequence = string.Empty;
            if (String.IsNullOrEmpty(str1) || String.IsNullOrEmpty(str2))
                return 0;

            int[,] num = new int[str1.Length, str2.Length];
            int maxlen = 0;
            int lastSubsBegin = 0;
            StringBuilder sequenceBuilder = new StringBuilder();

            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str1[i] != str2[j])
                        num[i, j] = 0;
                    else
                    {
                        if ((i == 0) || (j == 0))
                            num[i, j] = 1;
                        else
                            num[i, j] = 1 + num[i - 1, j - 1];

                        if (num[i, j] > maxlen)
                        {
                            maxlen = num[i, j];
                            int thisSubsBegin = i - num[i, j] + 1;
                            if (lastSubsBegin == thisSubsBegin)
                            {//if the current LCS is the same as the last time this block ran
                                sequenceBuilder.Append(str1[i]);
                            }
                            else //this block resets the string builder if a different LCS is found
                            {
                                lastSubsBegin = thisSubsBegin;
                                sequenceBuilder.Length = 0; //clear it
                                sequenceBuilder.Append(str1.Substring(lastSubsBegin, (i + 1) - lastSubsBegin));
                            }
                        }
                    }
                }
            }
            sequence = sequenceBuilder.ToString();
            return maxlen;
        }

        private void button2_Click(object sender, EventArgs e) {
            String path = System.IO.Path.GetDirectoryName(openFileDialog1.FileName) + "\\";
            for (int i=0; i<listBox1.Items.Count; ++i) {
                System.IO.File.Move(path + listBox1.Items[i], path + listBox2.Items[i]);
            }
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            textBox1.Clear();
        }
    }
}
