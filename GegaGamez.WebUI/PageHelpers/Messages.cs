using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.PageHelpers;

public static class Messages
{
    public const string ErrorKey = "ErrorMessage";
    public const string InfoKey = "InfoMessage";

    public static void AccountDeleted(this PageModel page)
        => page.TempData [InfoKey] = AccountDeletedMsg();

    public static string AccountDeletedMsg()
        => $"Your account has been deleted. We'll miss you.";

    public static void CommentDeleted(this PageModel page)
        => page.TempData [InfoKey] = CommentDeletedMsg();

    public static string CommentDeletedMsg()
        => "Your comment has been deleted";

    public static void GameAdded(this PageModel page, string gameTitle, string collectionName)
                => page.TempData [InfoKey] = GameAddedMsg(gameTitle, collectionName);

    public static string GameAddedMsg(string gameTitle, string collectionName)
        => $"{gameTitle} has been added to {collectionName}";

    public static void GameDeleted(this PageModel page, string gameTitle)
        => page.TempData [InfoKey] = GameDeletedMsg(gameTitle);

    public static string GameDeletedMsg(string gameTitle)
        => $"Game {gameTitle} has been deleted.";

    public static void GameRemoved(this PageModel page, string gameTitle, string collectionName)
        => page.TempData [InfoKey] = GameRemovedMsg(gameTitle, collectionName);

    public static string GameRemovedMsg(string gameTitle, string collectionName)
        => $"{gameTitle} has been removed from {collectionName}";

    public static void IncorrectPassword(this PageModel page)
        => page.TempData [InfoKey] = IncorrectPasswordMsg();

    public static string IncorrectPasswordMsg()
        => "Incorrect password.";

    public static void LoggedIn(this PageModel page, string username)
        => page.TempData [InfoKey] = LoggedInMsg(username);

    public static string LoggedInMsg(string username)
        => $"Successfully logged in. Welcome, {username}";

    public static void Logout(this PageModel page)
        => page.TempData [InfoKey] = LogoutMsg();

    public static string LogoutMsg()
            => "You've been logged out.";

    public static void ProfileUpdated(this PageModel page)
            => page.TempData [InfoKey] = ProfileUpdatedMsg();

    public static string ProfileUpdatedMsg()
        => "Your profile has been updated.";

    public static void Registered(this PageModel page, string username)
        => page.TempData [InfoKey] = RegisteredMsg(username);

    public static string RegisteredMsg(string username)
        => $"Successfully registed. Welcome, {username}.";

    public static void UnknownError(this PageModel page)
        => page.TempData [InfoKey] = UnknownErrorMsg();

    public static string UnknownErrorMsg()
        => "An unknown error has occured.";

    public static void UserDoesNotExist(this PageModel page, string username)
                                        => page.TempData [InfoKey] = UserDoesNotExistMsg(username);

    public static string UserDoesNotExistMsg(string username)
        => $"User {username} does not exist.";

    public static void UserExists(this PageModel page, string username)
        => page.TempData [InfoKey] = UserExistsMsg(username);

    public static string UserExistsMsg(string username)
        => $"User {username} already exists";

    public static void ValidationError(this PageModel page)
        => page.TempData [InfoKey] = ValidationErrorMsg();

    public static string ValidationErrorMsg()
        => "Validation error(s).";
}
