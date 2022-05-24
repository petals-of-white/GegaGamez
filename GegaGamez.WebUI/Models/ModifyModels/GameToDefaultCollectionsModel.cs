namespace GegaGamez.WebUI.Models.ModifyModels;

public record class GameToDefaultCollectionsModel
{
    public int GameId { get; set; }
    public HashSet<int> CollectionIds { get; set; }
}
