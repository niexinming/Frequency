using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frequency
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            file getnewFile=new file();
            database myDatabase=new database();
            myDatabase.cleandatabase();
            Graphics graphics = CreateGraphics();  
            int i = 1;
         
            OpenFileDialog ofd=new OpenFileDialog();
            ofd.Multiselect = true;
          
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                foreach (string pname in ofd.FileNames)
                {
                    getnewFile.path.Add(pname);
                    getnewFile.inputfile();
                    SizeF sizeF = graphics.MeasureString(pname, new Font("宋体",20));  
                    Label label = new Label();
                  
                    label.Text = i+":"+pname+"\n"; //文本
                    label.Size = new Size((int)sizeF.Width+1, 15); //label大小
                    label.Location = new Point(36, 72 + 15 * i); //label坐标
                    this.Controls.Add(label);
                    i++;
                  
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            
            Form2 f2=new Form2(textBox1.Text.Trim());
           
            f2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3=new Form3(1);
            f3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(0);
            f3.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            database myDatabase=new database();
            myDatabase.findfrequency(textBox2.Text.Trim());
        }
    }
}
