using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace QConfig
{
    /// <summary>
    /// .qgt file reader
    /// </summary>
    public class Reader
    {
        private static string objectPattern = @"(?<objectName>\w+)\W*{(?<object>[\w\W]*?)}";
        // private static string parameterValuePattern = @"\W*(?<variable>[^:]+)(\W*:\W*)(?<value>[\S]+)";
        //private static string parameterValuePattern = @"\s*(?<variable>[^:]+)\s*:\s*(?<value>[\S]+)";
        private static string parameterValuePattern = @"\s*(?<variable>[^:]+)\s*:\s*(?<value>[^\n\r]+)";
            
        /// <summary>
        /// Returns a list of QObject that has been read from the given path.
        /// </summary>
        /// <param name="path">File path to load</param>
        /// <returns>List of QObject</returns>
        public static List<QObject> LoadFile(string path)
        {
            try
            {
                StreamReader reader = File.OpenText(path);//new StreamReader(path);
                string contents = reader.ReadToEnd();
                reader.Dispose();
                return Read(contents);
            }
            catch(Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// Returns a type of the parsed value.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Best type guess of the given string</returns>
        public static Type ParseString(string str)
        {
            bool boolValue;
            int intValue;
            long bigintValue;
            float floatValue;
            double doubleValue;
            DateTime dateValue;

            if (bool.TryParse(str, out boolValue))
                return typeof(bool);
            else if (int.TryParse(str, out intValue))
                return typeof(int);
            else if (long.TryParse(str, out bigintValue))
                return typeof(long);
            else if (float.TryParse(str, out floatValue))
                return typeof(float);
            else if (double.TryParse(str, out doubleValue))
                return typeof(double);
            else if (DateTime.TryParse(str, out dateValue))
                return typeof(DateTime);
            else
                return typeof(string);

        }

        public static Dictionary<string, QObject> LoadFileDict(string path)
        {
            try
            {
                StreamReader reader = File.OpenText(path);//new StreamReader(path);
                string contents = reader.ReadToEnd();
                reader.Dispose();
                return ReadDict(contents);
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        private static Dictionary<string, QObject> ReadDict(string input)
        {
            //List<QObject> qObjects = new List<QObject>();
            Dictionary<string, QObject> qObjects = new Dictionary<string, QObject>();
            Regex objectRegex = new Regex(objectPattern);
            Regex parValRegex = new Regex(parameterValuePattern);
            foreach (Match match in objectRegex.Matches(input))
            {
                QObject qObject = new QObject(match.Groups["objectName"].Value);
                foreach (Match parValMatch in parValRegex.Matches((match.Groups["object"].Value)))
                {
                    qObject.Add(parValMatch.Groups["variable"].Value, parValMatch.Groups["value"].Value);
                }
                qObjects.Add(qObject.name,qObject);
            }
            return qObjects;
        }
        public static List<QObject> Read(string input)
        {
            List<QObject> qObjects = new List<QObject>();
            Regex objectRegex = new Regex(objectPattern);
            Regex parValRegex = new Regex(parameterValuePattern);
            foreach (Match match in objectRegex.Matches(input))
            {
                QObject qObject = new QObject(match.Groups["objectName"].Value);
                foreach(Match parValMatch in parValRegex.Matches((match.Groups["object"].Value)))
                {
                    qObject.Add(parValMatch.Groups["variable"].Value, parValMatch.Groups["value"].Value);
                }
                qObjects.Add(qObject);
            }
            return qObjects;
        }
    }
}
