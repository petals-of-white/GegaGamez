using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Admin
{
    [Authorize(PolicyNames.AdminPolicy)]
    public class ReportModel : PageModel
    {
        private readonly ILogger<ReportModel> _logger;
        private readonly IStatisticsService _statisticsService;

        public ReportModel(IStatisticsService statisticsService, ILogger<ReportModel> logger)
        {
            _statisticsService = statisticsService;
            _logger = logger;
        }

        public int AdminsQuantity { get; set; }
        public byte AvgGameScore { get; set; }
        public int CommentsQuantity { get; set; }
        public int DevsQuantity { get; set; }
        public int GamesQuantity { get; set; }
        public int GenresQuantity { get; set; }
        public TimeOnly LastUpdated { get; set; }
        public int UsersQuantity { get; set; }

        public void OnGet()
        {
            GamesQuantity = _statisticsService.GamesQuantity;
            DevsQuantity = _statisticsService.DevsQuantity;
            UsersQuantity = _statisticsService.UsersQuantity;
            GenresQuantity = _statisticsService.GenresQuantity;
            CommentsQuantity = _statisticsService.CommentsQuantity;
            AdminsQuantity = _statisticsService.AdminsQuantity;
            AvgGameScore = _statisticsService.AvgGameScore;
            LastUpdated = TimeOnly.FromDateTime(DateTime.Now);

            _logger.LogInformation("Admin report has been successfully loaded");
        }
    }
}
