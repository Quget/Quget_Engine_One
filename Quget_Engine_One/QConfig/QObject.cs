using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace QConfig
{
    /// <summary>
    /// QObject contains the name of the object and variables with values.
    /// </summary>
    public class QObject
    {
        /// <summary>
        /// The name of this QObject.
        /// </summary>
        public string name { private set; get; } = "";
        public Dictionary<string, string> varValue { private set; get; } = new Dictionary<string, string>();
        /// <summary>
        /// Creates a new QObject with a name.That contains diffrent variables with values
        /// Variables with its values can be added with the Add method.
        /// </summary>
        /// <param name="name">name of the object</param>
        public QObject(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Remove variable from the dictionary
        /// </summary>
        /// <param name="variable"></param>
        public void Remove(string variable)
        {
            if (varValue.ContainsKey(variable))
                varValue.Remove(variable);
        }
        /// <summary>
        /// Adds variable with its value to a dictionary.
        /// </summary>
        /// <param name="variable">variable name</param>
        /// <param name="value">value</param>
        public void Add(string variable,string value)
        {
            if (!varValue.ContainsKey(variable))
                varValue.Add(variable, value);
        }
        /// <summary>
        /// Gets the value of the variable
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>value of the variable</returns>
        public string GetValue(string variable)
        {
            if (varValue.ContainsKey(variable))
                return varValue[variable];
            else
                return null;
        }
        /// <summary>
        /// Change the value of the selected variable.
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        public void ChangeValue(string variable,string value)
        {
            if (varValue.ContainsKey(variable))
                varValue[variable] = value;
        }
    }
}
