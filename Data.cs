using System.Collections.Generic;
using System.IO;
using MovieLibraryEntities;

namespace MovieLibrary
{
    public class Data : Manager
    {
        Media formatter = new Media();
        public List<string> ReadMedia(string name)
        {
            string fileName = name + ".csv";

            var file = new List<string>();

            if (File.Exists(fileName))
            {
                StreamReader fileReader = new StreamReader(fileName);
                string? line = "";

                do
                {
                    line = fileReader.ReadLine();

                    if (line != null)
                    {
                        file.Add(line);
                    }

                } while (line != null);
                fileReader.Close();

            }
            return file;
        }

        public void WriteMedia(List<string> media, string name)
        {
            string fileName = name + ".csv";

            StreamWriter writer = new StreamWriter(fileName);

            if (fileName == "movies.csv")
            {
                writer.WriteLine("id,title,genres");
            }
            else if (fileName == "shows.csv")
            {
                writer.WriteLine("id,title,season,episode,writers");
            }
            else
            {
                writer.WriteLine("id,title,format,length,regions");
            }

            for (int i = 0; i < media.Count; i++)
            {
                writer.WriteLine(media[i]);
            }
            writer.Close();
        }

    }
}