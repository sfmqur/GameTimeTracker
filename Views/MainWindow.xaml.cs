using System.Windows;

namespace GameTimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimerHandler _timerHandler;
        private JsonHandler _jsonHandler;
        private List<string> _folderPaths;
        private List<Game> _gameList;
        private GameFolderHandler _gameFolderHandler;
        private FolderSelectionWindow? _folderSelectionWindow;
        private Game? _lastSelectedGame;

        public MainWindow()
        {
            InitializeComponent();
            _timerHandler = new TimerHandler();
            _jsonHandler = new JsonHandler();

            _folderPaths = _jsonHandler.JsonData.GameFolderPaths;
            _gameList = _jsonHandler.JsonData.GamesList;
            _gameFolderHandler = new GameFolderHandler(_folderPaths, _gameList);

            _gameFolderHandler.UpdateGameList();
            updateGameListInvokable();

            _timerHandler.TimerElapsed += OnTimerElapsed;
            GameTitlesListBox.SelectionChanged += OnGameSelection;
        }

        private void OnStart(object? sender, RoutedEventArgs args)
        {
            TimeLabel.Content = _timerHandler.CurrentTime.ToString();
            _timerHandler.StartTimer();
        }

        private void OnStop(object? sender, RoutedEventArgs args)
        {
            _timerHandler.StopTimer();
            if (_lastSelectedGame != null)
                _lastSelectedGame.PlayTime = _timerHandler.CurrentTime; //saves time to object
            _jsonHandler.SaveData();
        }

        private void SelectFoldersButton_Click(object sender, RoutedEventArgs e)
        {
            _folderSelectionWindow = new FolderSelectionWindow(_folderPaths, _gameList, _gameFolderHandler);
            _folderSelectionWindow.Closed += OnFolderSelectionClosed;

            _folderSelectionWindow.Show();
        }

        public void OnTimerElapsed(object? sender, TimeUpdatedEventArgs e)
        {
            TimeLabel.Dispatcher.Invoke(new Action(() => { TimeLabel.Content = e.Time.ToString(); }));
        }

        private void OnGameSelection(object? sender, EventArgs e)
        {
            OnStop(this, new RoutedEventArgs());
            _lastSelectedGame = (GameTitlesListBox.SelectedItem as Game);
            if (_lastSelectedGame != null)
            {
                updateTime(_lastSelectedGame.PlayTime);
            }
        }

        private void OnFolderSelectionClosed(object? sender, EventArgs e)
        {
            GameTitlesListBox.Dispatcher.Invoke(updateGameListInvokable);
        }

        private void OnClose(object? sender, EventArgs e)
        {
            _jsonHandler.SaveData();
        }

        /// <summary>
        /// updates the timer and display to the given time
        /// </summary>
        /// <param name="time"></param>
        private void updateTime(TimeSpan time)
        {
            _timerHandler.CurrentTime = time;
            TimeLabel.Dispatcher.Invoke(new Action(() => { TimeLabel.Content = time.ToString(); }));
        }

        private void updateGameListInvokable()
        {
            GameTitlesListBox.Items.Clear();
            foreach (var game in _gameList.Where(game => game.IsVisible))
            {
                GameTitlesListBox.Items.Add(game);
            }
        }
    }
}