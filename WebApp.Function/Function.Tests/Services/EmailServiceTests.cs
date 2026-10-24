using WebAppFunction.Api.Models;
using WebAppFunction.Api.Services;

#nullable disable

[TestFixture]
public class EmailServiceTests
{
    // TODO: These test are failing. I think its because it is not able to read the 
    // Environment variables from the local.settings.json file

    [Test]
    public static void ValidateEmail_ValidEmail_Successful()
    {
        string validEmailAddress = "validperson@domainname.domain";
        ValidationModel model = EmailService.ValidateEmail(validEmailAddress);

        Assert.IsTrue(model.IsValid);
        Assert.AreEqual("Email is formatted correctly", model.ResultMessage);
    }

    [Test]
    public static void ValidateEmail_InvalidEmail_Unsuccessful()
    {
        string invalidEmailAddress = "invalidperson@domainname";
        ValidationModel model = EmailService.ValidateEmail(invalidEmailAddress);

        Assert.IsFalse(model.IsValid);
        Assert.AreEqual("Email is formatted incorrectly.", model.ResultMessage);
    }
}