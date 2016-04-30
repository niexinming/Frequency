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
    public partial class Form3 : Form
    {
        public Form3(int flag)
        {

            string temp = "";
            int templine = 2;
            InitializeComponent();
            if (flag == 1)
            {
                 temp = "单词出现频率";
            }
            else
            {
                 temp = "单词查询频率";
                templine = 3;
            }
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Columns.Add("单词", 150);
            this.listView1.Columns.Add(temp, 150);

           database myDatabase=new database();
            DataTable Rs=myDatabase.gettopten(flag);
            for (int i = 0; i < Rs.Rows.Count; i++)
            {
                listView1.Items.Add(Rs.Rows[i][1].ToString());
                listView1.Items[i].SubItems.Add(Rs.Rows[i][templine].ToString());
            }
        }

      
    }
}
