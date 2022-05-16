using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GegaGamez.WebUI.Pages.Users;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace GegaGamez.Tests.UITests;

public class Page_Tests
{
    public Page_Tests()
    {
        var page = new IndexModel();
        page.Url = new UrlHelper(new ActionContext());
        page.PageContext.HttpContext.User.
    }

    [Fact]
    public void Something()
    {

    }
}
