using System.Threading.Tasks;
using Shouldly;
using Xunit;

public class ShellInCustomContainerTests : IClassFixture<CustomImageFixture>
{
    private readonly CustomImageFixture _fixture;

    public ShellInCustomContainerTests(CustomImageFixture fixture)
        => _fixture = fixture;

    [Fact]
    public async Task Should_Run_Echo_Command_Inside_Custom_Image()
    {
        var cmd = new[] { "sh", "-c", "echo hello_from_custom_image" };
        var result = await _fixture.Container.ExecAsync(cmd).ConfigureAwait(false);

        result.ExitCode.ShouldBe(0);
        result.Stdout.Trim().ShouldBe("hello_from_custom_image");
    }
}


