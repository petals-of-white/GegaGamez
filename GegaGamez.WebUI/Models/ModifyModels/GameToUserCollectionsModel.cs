namespace GegaGamez.WebUI.Models.ModifyModels;

public record class GameToUserCollectionsModel
{
    public int GameId { get; set; }
    public HashSet<int> CollectionIds { get; set; }
}
