namespace GegaGamez.WebUI.Models.ModifyModels
{
    public record class NewCommentModel
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public string Text { get; set; }
    }
}
