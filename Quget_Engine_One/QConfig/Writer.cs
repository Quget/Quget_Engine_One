using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QConfig
{
    /// <summary>
    /// .qgt file writer
    /// </summary>
    public class Writer
    {
        /// <summary>
        /// Writes a list of QObject to the output. THIS DOES NOT APPEND!
        /// </summary>
        /// <param name="output">File path to write</param>
        /// <param name="qObjects">List of QObject</param>
        public static void Write(string output,List<QObject> qObjects)
        {
            string contents = "";
            foreach(QObject qObject in qObjects)
            {
                contents += qObject.name + "\n{\n";
                foreach(KeyValuePair<string,string> keyValue in qObject.varValue)
                {
                    contents += string.Format("\t{0}:{1}\n", keyValue.Key, keyValue.Value);
                }
                contents += "}\n";
            }
            FileStream file = new FileStream(output, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(contents);
            writer.Dispose();
            file.Dispose();
        }
    }
}
