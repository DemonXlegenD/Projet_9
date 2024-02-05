using NSave;
using NSecurity;
using Projet_9.Save;

namespace NSaveUserTests;

public class SaveUserTests
{
    private SaveUser saveUser;
    [SetUp]
    public void Setup()
    {
        saveUser = SaveUser.GetInstance();
    }

    [TestCase("Sacha", "Ketchum")]
    public void SaveUsersTestSuccess(string username, string password)
    {
        UserManager userManager = UserManager.GetInstance();
        userManager.NewUser(username, password);

        User user = userManager.ActualUser;
        Assert.IsNotNull(user);
        Assert.That(username, Is.EqualTo(user.Username));
        Assert.IsInstanceOf<User>(user);
    }

    [Test]
    public void ReadUserSaveTestSuccess()
    {
        UserManager userManager = UserManager.GetInstance();
        userManager.LoadUsers();

        User user = userManager.ActualUser;
        Assert.IsNotNull(user);
        Assert.IsTrue(user.IsConnected);
    }

    [TestCase("Sacha", "Ketchum")]
    public void CheckConnexionSaveTestSuccess(string username, string password)
    {
        UserManager userManager = UserManager.GetInstance();
        userManager.LoadUsers();

        Assert.IsTrue(userManager.CheckConnexion(username, password));
    }

    [TestCase("Sacha", "Ketchup")]
    public void CheckConnexionSaveTestFail(string username, string password)
    {
        UserManager userManager = UserManager.GetInstance();
        userManager.LoadUsers();

        Assert.IsTrue(!userManager.CheckConnexion(username, password));
    }
}