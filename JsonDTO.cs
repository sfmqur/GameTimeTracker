namespace GameTimeTracker
{
    public class JsonDTO
    {
        public JsonDTO(List<string> gameFolderPaths, List<Game> gamesList)
        {
            GameFolderPaths = gameFolderPaths;
            GamesList = gamesList;
        }
        public List<string> GameFolderPaths { get; set; }
        public List<Game> GamesList { get; set; } 
    }
}
