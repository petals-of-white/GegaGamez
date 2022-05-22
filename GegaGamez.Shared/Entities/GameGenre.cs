namespace GegaGamez.Shared.Entities;

public partial class GameGenre
{
    public Game Game { get; set; }
    public int GameId { get; set; }
    public Genre Genre { get; set; }
    public int GenreId { get; set; }
}
