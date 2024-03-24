namespace GameTimeTracker
{
    public class Game : IEquatable<Game>, IComparable<Game>
    {
        public Game(string name, TimeSpan playTime, bool isVisible)
        {
            Name = name;
            PlayTime = playTime;
            IsVisible = isVisible;
        }
        public string Name { get; }
        public TimeSpan PlayTime { get; set; }
        public bool IsVisible { get; set;  }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Game? other)
        {
            if (other == null) return false;
            return Name == other.Name;
        }

        public int CompareTo(Game? other)
        {
            if (other == null) return 1;
            if (other == this) return 0;
            return Name.CompareTo(other.Name);
        }
    }
}
