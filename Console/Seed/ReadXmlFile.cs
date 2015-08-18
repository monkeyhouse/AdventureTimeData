using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace ATConsole.Seed
{
    internal class XmlParser
    {
        private string path;
        public XmlParser(string file)
        {
            var d = Directory.GetCurrentDirectory();
            path = Path.Combine(d, "Seed\\" + file);
        }
        public IEnumerable<T> ParseElements<T>(Func<XElement,T> fxn) where T:new()
        {

            var doc = XDocument.Load(path, LoadOptions.None);

            var result = doc.Descendants("record").Select(fxn);

            return result;
        }

      

    }
}
