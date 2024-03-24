using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeTracker
{
    public class GameFolderHandler
    {
        private List<string> _folderPaths;
        private List<Game> _gameList;

        public GameFolderHandler(List<string> folderPaths, List<Game> gameList)
        {
            _folderPaths = folderPaths;
            _gameList = gameList;
        }

        public void UpdateGameList()
        {
            var visibleGames = getVisibleGames();
            var gamesInList = _gameList.Select(x => x.Name).ToList();

            foreach (var visGame in visibleGames)
            {
                if (!gamesInList.Contains(visGame))
                {
                    gamesInList.Add(visGame);
                    _gameList.Add(new Game(visGame, new TimeSpan(0, 0, 0), true));
                }
            }

            foreach (var game in _gameList)
            {
                if (!visibleGames.Contains(game.Name))
                {
                    game.IsVisible = false;
                }
                else
                {
                    game.IsVisible = true;
                }
            }

            _gameList.Sort();
        }

        private List<string> getVisibleGames()
        {
            var visibleGames = new List<string>();
            foreach (var searchFolder in _folderPaths)
            {
                var gameFolders = Directory.EnumerateDirectories(searchFolder);
                foreach (var game in gameFolders)
                {
                    if (isVisibleGameFolder(game))
                    {
                        var lastSlash = game.LastIndexOf('\\');
                        var gameName = game.Substring(lastSlash + 1, game.Length - lastSlash - 1);
                        visibleGames.Add(gameName);
                    }
                }
            }
            return visibleGames;
        }

        private bool isVisibleGameFolder(string gameFolder)
        {
            var gameFiles = Directory.EnumerateFiles(gameFolder);
            var visibleGames = new List<string>();
            foreach (var gameFile in gameFiles)
            {
                if (gameFile != null)
                {
                    var ending = gameFile.Substring(gameFile.Length - 4, 4);
                    if (ending == ".exe")
                        return true;
                }
            }
            return false;
        }
    }
}