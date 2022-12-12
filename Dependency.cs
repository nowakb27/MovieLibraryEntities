using MovieLibraryEntities;

namespace MovieLibrary
{
    public class Dependency
    {
        public Manager GetManager()
        {
            return new Data();
        }

        public Services GetInputOutputService()
        {
            return new Services();
        }

        public Media GetFormatter()
        {
            return new Media();
        }

        public Search GetSearch()
        {
            return new Search();
        }

        public Database GetDatabase()
        {
            return new Database();
        }
    }
}