using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace ScrybeLogger.Configuration
{
    internal class YamlReader
    {
        private static Dictionary<object, object> YamlContent;


        public YamlReader(string filePath)
        {
            LoadYamlContent(filePath);
        }


        public void LoadYamlContent(string filePath)
        {
            filePath = Path.GetFullPath(filePath);
            if(!File.Exists(filePath))
            {
                throw new Exception($"ScrybeLogger configuration file {filePath} does not exist");
            }

            string fileContent = File.ReadAllText(filePath);
            var deserializer = new Deserializer();
            YamlContent = (Dictionary<object, object>)deserializer.Deserialize<object>(fileContent);
        }


        public Dictionary<object, object> GetValues(string key)
        {
            string[] keys = key.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<object, object> pair = YamlContent;
            foreach (string k in keys)
            {
                pair = (Dictionary<object, object>)pair[k];
            }
            return pair;
        }


        public object GetValue(string key)
        {
            string[] keys = key.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            object pair = YamlContent;
            foreach(string k in keys)
            {
                if(pair is Dictionary<object, object> dictPair)
                {
                    pair = dictPair[k];
                }
            }
            return pair;
        }


        public object[] GetValueArray(string key)
        {
            var value = GetValue(key);
            if(value is List<object> valueList)
            {
                return valueList.ToArray();
            }
            return (object[])value;
        }
    }
}
