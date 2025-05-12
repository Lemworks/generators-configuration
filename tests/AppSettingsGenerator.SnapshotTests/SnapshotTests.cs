namespace AppSettingsGenerator.SnapshotTests;

public class SnapshotTests
{
    [Test]
    public Task GeneratorTest() =>
        TestHelper.Verify(
            """
            {
                "Test": "",
                "Object": {
                    "Value": 1,
                    "Object": {
                        "Something": null
                    }
                },
                "Last": ""
            }
            """);
}
