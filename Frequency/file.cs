using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
namespace Frequency
{
    class file
    {
       
        public List<string> path=new List<string>();

        private FileStream[] aFile=new FileStream[1024];

        public void inputfile()
        {
            int linecount = 1,sencentecount=1;
            handle myhand=new handle();
            myhand.initkeyword();
            int i = 0;
            foreach (string p in path)
            {
               
                try
                {
                     aFile[i] = new FileStream(p, FileMode.Open);
                    StreamReader sr = new StreamReader(aFile[i],System.Text.Encoding.GetEncoding("utf-8"));
                    int content = sr.Read();
                    while (-1 != content)
                    {
                        if (Convert.ToChar(content) == '\n')
                        {
                            linecount++;
                        }
                        myhand.handleword(Convert.ToChar(content),sencentecount,linecount,p);
                        sencentecount=myhand.handlesentence(Convert.ToChar(content));
                        content = sr.Read();
                    }
                    sr.Close();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                i++;
            }

           
        }
    }
}
