using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Frequency
{
    class database
    {
        private OleDbConnection conn;
        private OleDbDataAdapter oda = new OleDbDataAdapter();
        private OleDbCommand cmd;
        private DataSet myds = new DataSet();

        public database()
        {
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"./data/worddata.mdb");
        }
        public bool setword(int id,string worddata,int countflag,int line,string filename,int sentenceid)
        {
            string sqlstr = "";
            conn.Open();
            if (countflag==1)
            {
                try
                {
                    sqlstr = "INSERT INTO myword(myworddata,mycountdata) values('"+worddata+"',1)";
                    cmd = new OleDbCommand(sqlstr, conn);
                    cmd.ExecuteNonQuery();
                    sqlstr = "insert into sawr(sentenceid,wordid,mylinecount,myfilename) values(" + sentenceid + "," + id + "," +
                             line + ",'" + filename + "')";
                    cmd = new OleDbCommand(sqlstr, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                sqlstr = "UPDATE myword SET mycountdata = mycountdata+1 where myworddata='" + worddata + "'";
                cmd = new OleDbCommand(sqlstr, conn);
                cmd.ExecuteNonQuery();
                sqlstr = "insert into sawr(sentenceid,wordid,mylinecount,myfilename) values(" + sentenceid+ "," + id + "," + line +  ",'" + filename + "')";
                cmd = new OleDbCommand(sqlstr, conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return true;
        }

        public bool setsencet(string sentence)
        {
            string sqlstr = "";
            conn.Open();
            sqlstr = "insert into sentence(mycontent) values('"+sentence+"')";
            cmd = new OleDbCommand(sqlstr, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public DataTable gettopten(int flag)
        {
            string sqlstr = "";
            conn.Open();
            if (flag == 1)
            {
                sqlstr = "SELECT top 10 * from myword ORDER BY mycountdata DESC";
            }
            else
            {
                sqlstr = "SELECT top 10 * from myword ORDER BY hotwordcount DESC";
            }
            DataTable ds = new DataTable();
            OleDbCommand cmd = new OleDbCommand(sqlstr, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
        public DataTable getfind(string input)
        {
            conn.Open();
            string sqlstr = "SELECT sawr.myfilename, sawr.mylinecount, sentence.mycontent FROM (myword INNER JOIN sawr ON myword.wordid = sawr.wordid) INNER JOIN sentence ON sawr.sentenceid = sentence.sentenceid where myword.myworddata='"+input+"'";
            DataTable ds = new DataTable();
            OleDbCommand cmd = new OleDbCommand(sqlstr, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            conn.Close();
            return ds;
        }

        public bool hotword(string input)
        {
            string sqlstr = "";
            conn.Open();
            sqlstr = "UPDATE myword SET hotwordcount = hotwordcount+1 where myworddata='" + input + "'";
            cmd = new OleDbCommand(sqlstr, conn);
            cmd.ExecuteNonQuery();
           
            conn.Close();
            return true;
        }

        public void findfrequency(string input)
        {
            try
            {
                string sqlstr = "";
                conn.Open();
                sqlstr = "SELECT myword.mycountdata, myword.hotwordcount FROM myword where myworddata='" + input + "'";
                OleDbCommand odCommand = conn.CreateCommand();
                odCommand.CommandText = sqlstr;
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                odrReader.Read();
                MessageBox.Show("出现过的频次：" + odrReader.GetInt32(0) + "\n" + "查询过的次数：" + odrReader.GetInt32(1));
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("不存在这个词");
            }

        }


        public void cleandatabase()
        {
            string sqlstr = "";
            conn.Open();

            sqlstr = "delete * from myword";
            cmd = new OleDbCommand(sqlstr, conn);
            cmd.ExecuteNonQuery();

            sqlstr = "delete * from sawr";
            cmd = new OleDbCommand(sqlstr, conn);
            cmd.ExecuteNonQuery();

            sqlstr = "delete * from sentence";
            cmd = new OleDbCommand(sqlstr, conn);
            cmd.ExecuteNonQuery();

            sqlstr = "ALTER TABLE myword ALTER COLUMN wordid COUNTER (1, 1) ";
            cmd = new OleDbCommand(sqlstr, conn);
            cmd.ExecuteNonQuery();

            sqlstr = "ALTER TABLE sawr ALTER COLUMN ID COUNTER (1, 1) ";
            cmd = new OleDbCommand(sqlstr, conn);
            cmd.ExecuteNonQuery();

            sqlstr = "ALTER TABLE sentence ALTER COLUMN sentenceid COUNTER (1, 1) ";
            cmd = new OleDbCommand(sqlstr, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }
    }
}
