using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frequency
{

    class handle
    {
        private string worddata;
        private string sentencedata;
        private static int mysentenceid=1;
        private database myDatabase = new database();
        private static Hashtable ht = new Hashtable();
        private static int i = 1;
        private static Dictionary<int, string> myDictionary = new Dictionary<int, string>();
        public void handleword(char word,int senceid,int line,string filename)
        {
            if(word=='\r' || word=='\n')return;
         
          
           
            Regex r = new Regex("[,，;':“”\"]");
                
                if (word==' ' || word=='\n' || word=='.'||word=='?'||word=='!')
                {
                       
                        worddata = r.Replace(worddata, "");

                    if(clearkeyword(worddata)==true)return;//包含虚词，连词就过滤
                  
                    if (ht.ContainsKey(worddata) == false)
                    {
                        ht.Add(worddata, i);

                        myDatabase.setword(i,worddata,1,line,filename,senceid);
                        i++;
                    }
                    else
                    {
                        int tempi = (int)ht[worddata];
                        myDatabase.setword(tempi,worddata, 0, line, filename, senceid);

                    }

                   
                    worddata = "";
                    
                }
                else
                {
                    worddata += word.ToString();
                   
                  
                   
                }
        }


        public int handlesentence(char word)
        {
            if (word == '\r' || word == '\n') return mysentenceid;
            if (word == '.' || word == '?' || word == '？' || word == '!' || word == '！')
            {
                
               
                myDatabase.setsencet(sentencedata);
                mysentenceid++;
                sentencedata = "";
                return mysentenceid;
            }
            else
            {
                sentencedata += word.ToString();

            }
            return mysentenceid;
        }

        public void initkeyword()
        {
            int i = 0;
            string keyword = "a,an,the,in,on,from,above,behind,and,but,before,oh,well,hi,he";
            string[] keyowrdarry = keyword.Split(',');
            foreach (var mykey in keyowrdarry)
            {
                myDictionary.Add(i,mykey);
                i++;
            }
        }

        private bool clearkeyword(string worddata)
        {

            if (myDictionary.ContainsValue(worddata))
                return true;
            return false;
        }
       
    }
}
