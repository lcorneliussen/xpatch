using System.Collections.Generic;
using System.IO;

namespace XPatch.Console.Tests.Helper
{
    public class MemoryXmlFileSource : IXmlFileSource
    {
        IDictionary<string, string> _files;

        public MemoryXmlFileSource(IDictionary<string, string> contents)
        {
            _files = contents;
        }

        public string Load(string fileName)
        {
            string value;
            if (!_files.TryGetValue(fileName, out value))
            {
                throw new FileNotFoundException("File not found!", fileName);
            }

            return value;
        }

        public void Save(string fileName, string contents)
        {
            _files[fileName] = contents;
        }
    }
}