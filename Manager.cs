using System.Collections.Generic;

namespace MovieLibrary
{
    public interface Manager
    {
        public List<string> ReadMedia(string fileName);
        public void WriteMedia(List<string> media, string fileName);

    }
}