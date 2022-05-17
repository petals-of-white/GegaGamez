namespace GegaGamez.Tests;

public class AsyncTests
{
    /*
    [Fact]
    public void GetAsync_ShouldWork()
    {
        List<Task> tasks = new();
        IUnitOfWork db = new UnitOfWork(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GegaGamez;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        var devsQuery = db.Developers.AsEnumerableAsync();
        tasks.Add(devsQuery);
        Task.WhenAll(tasks).ContinueWith(task =>
        {
            Debug.Write("Hooray!" + string.Join(',', devsQuery.Result.Select(d => d.Name)));
        });

        for (int i = 0; i < 5000; i++)
        {
            Debug.WriteLine($"Do {i}, ");
        }

        Assert.True(devsQuery.IsCompletedSuccessfully);
    }

    [Fact]
    public void InsertExistingAsync_ShouldFail()
    {
        IUnitOfWork db = new UnitOfWork(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GegaGamez;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        User user = db.Users.Get(3002)!;
        user.About = "Something new";

        db.Users.Add(user);

        var saveFailed = Assert.ThrowsAnyAsync<Exception>(() => db.SaveAsync());
        saveFailed.ContinueWith(task =>
        {
            Debug.WriteLine($"\n Save failed for user {user.Id}");
        });

        for (int i = 0; i < 5000; i++)
        {
            Debug.Write($"Do {i} ");
        }
    }
    */
}
