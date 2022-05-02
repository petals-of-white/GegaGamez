namespace GegaGamez.WebUI.Models.Display;

public struct DefaultCollectionModel
{
    public int Id { get; set; }
    //public int UserId { get; set; }
    //public int DefaultCollectionTypeId { get; set; }
    public DefaultCollectionTypeModel Type { get; set; }
}
