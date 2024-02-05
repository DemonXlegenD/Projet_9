using NSecurity;

namespace Projet_9.Test;

public class SecurityTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GenererSelTestSuccess()
    {
        byte[] saltBytes = Security.GenererSel();
        
        
        Assert.IsNotNull(saltBytes);
        Assert.IsTrue(saltBytes.Length > 0);
        Assert.IsInstanceOf<byte[]>(saltBytes);
    }

    [TestCase("azerty", "BqTMLtO0BK+VqyabD59A7E8IcWmIUD8zkciUqztU5GY=", "749c1aded16fee9f641c68bb1c1e2036e2aff04ba37f18fce2834f61eba2e457")]
    [TestCase("Mamague28021", "hCZr1HrPKgGSx7TNLvbB9BtZWdJ4Z3XiK2fPTpOW7Kc=", "a39a64658e54448f0290fb58c3a7bc154c8e69f7948cb2fb5de943dde366dce7")]
    public static void HachageTestSuccess(string passwordTest, string selTest, string passwordResult)
    {
        byte[] saltBytes = Convert.FromBase64String(selTest);
        string conversionPasswordTest = Security.HacherMotDePasse(passwordTest, saltBytes);

        Assert.That(conversionPasswordTest, Is.EqualTo(passwordResult));
    }

    [TestCase("azerty", "hCZr1HrPKgGSx7TNLvbB9BtZWdJ4Z3XiK2fPTpOW7Kc=", "749c1aded16fee9f641c68bb1c1e2036e2aff04ba37f18fce2834f61eba2e457")]
    [TestCase("Mamague28021", "BqTMLtO0BK+VqyabD59A7E8IcWmIUD8zkciUqztU5GY=", "a39a64658e54448f0290fb58c3a7bc154c8e69f7948cb2fb5de943dde366dce7")]
    [TestCase("", "", "")]
    public void HachageTestFail(string passwordTest, string selTest, string passwordResult)
    {
        byte[] saltBytes = Convert.FromBase64String(selTest);
        string conversionPasswordTest = Security.HacherMotDePasse(passwordTest, saltBytes);

        Assert.That(conversionPasswordTest, !Is.EqualTo(passwordResult));
    }

    [TestCase("azerty", "hCZr1HrPKgGSx7TNLvbB9BtZWdJ4Z3XiK2fPTpOW7Kc=", "749c1aded16fee9f641c68bb1c1e2036e2aff04ba37f18fce2834f61eba2e457")]
    [TestCase("Mamague28021", "BqTMLtO0BK+VqyabD59A7E8IcWmIUD8zkciUqztU5GY=", "a39a64658e54448f0290fb58c3a7bc154c8e69f7948cb2fb5de943dde366dce7")]
    [TestCase("", "", "")]
    public void VerifierMotDePasseTestFail(string passwordTest, string selTest, string passwordResult)
    {
        Assert.IsTrue(!Security.VerifierMotDePasse(passwordTest, passwordResult, selTest));
    }

    [TestCase("azerty", "BqTMLtO0BK+VqyabD59A7E8IcWmIUD8zkciUqztU5GY=", "749c1aded16fee9f641c68bb1c1e2036e2aff04ba37f18fce2834f61eba2e457")]
    [TestCase("Mamague28021", "hCZr1HrPKgGSx7TNLvbB9BtZWdJ4Z3XiK2fPTpOW7Kc=", "a39a64658e54448f0290fb58c3a7bc154c8e69f7948cb2fb5de943dde366dce7")]
    public void VerifierMotDePasseTestSuccess(string passwordTest, string selTest, string passwordResult)
    {
        Assert.IsTrue(Security.VerifierMotDePasse(passwordTest, passwordResult, selTest));
    }
}