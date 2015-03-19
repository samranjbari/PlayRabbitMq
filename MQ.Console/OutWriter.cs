using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cout = System.Console;

namespace MQ.Console
{
    public static class OutWriter
    {
        public static StreamWriter FileWriter()
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut =  cout.Out;
            try
            {
                ostrm = new FileStream("./out.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                cout.WriteLine("Cannot open out.txt for writing");
                cout.WriteLine(e.Message);
                return null;
            }

            return writer;
        }
    }
}