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
    public partial class Form2 : Form
    {
       
        public Form2(string input)
        {
            database myDatabase=new database();
            myDatabase.hotword(input);
            InitializeComponent();
            this.Text = input;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Columns.Add("文件名", 500);
            this.listView1.Columns.Add("行号", 60);
            this.listView1.Columns.Add("所在语句", 500);
             DataTable Rs=myDatabase.getfind(input);
             for (int i = 0; i < Rs.Rows.Count; i++)
             {
                 listView1.Items.Add(Rs.Rows[i][0].ToString());
                 listView1.Items[i].SubItems.Add(Rs.Rows[i][1].ToString());
                 listView1.Items[i].SubItems.Add(Rs.Rows[i][2].ToString());
             }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
