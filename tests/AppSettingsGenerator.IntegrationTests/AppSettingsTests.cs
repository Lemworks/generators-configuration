using System.Diagnostics.CodeAnalysis;
using Lemworks.Configuration;
using Microsoft.Extensions.Configuration;

namespace AppSettingsGenerator.IntegrationTests;

[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
[SuppressMessage("Usage", "TUnitAssertions0005:Assert.That(...) should not be used with a constant value")]
internal sealed class AppSettingsTests
{
    [Test]
    public async Task ShouldContainAppSettingsType()
    {
        await Assert.That(Type.GetType("Lemworks.Configuration.AppSettings")).IsNotNull();
    }
    
    [Test]
    public async Task KeySelectorShouldBeCorrect()
    {
        await Assert.That(AppSettings.Object.Key).IsEqualTo("Object:Key");
    }

    [Test]
    public async Task ShouldSelectCorrectValue()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        await Assert.That(configuration[AppSettings.Object.Key]).IsEqualTo("Value");
    }
}
