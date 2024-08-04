using Application.Providers;
using FluentAssertions;
using Xunit.Abstractions;

namespace Tests;

public class AESEncryptionTests
{
    private readonly ITestOutputHelper _logger;

    public AESEncryptionTests(ITestOutputHelper logger)
    {
        DotNetEnv.Env.TraversePath().Load();

        _logger = logger;
    }

    [Fact]
    public void TestEncryptAndDecrypt()
    {
        var content = "teste";

        var contentEncrypted = AESEncryptionHelper.Encrypt(content);

        _logger.WriteLine($"contentEncrypted: {contentEncrypted}");

        var contentDecrypted = AESEncryptionHelper.Decrypt(contentEncrypted);

        // Assertions
        contentEncrypted.Should().NotBeEmpty();
        contentDecrypted.Should().Be("teste");
    }
}