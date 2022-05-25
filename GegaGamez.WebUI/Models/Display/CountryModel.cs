namespace GegaGamez.WebUI.Models.Display;

public record struct CountryModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ThreeCharCode { get; set; }
    public string TwoCharCode { get; set; }
}
