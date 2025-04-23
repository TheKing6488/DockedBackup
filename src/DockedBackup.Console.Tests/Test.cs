using CommandLine;
using KopiaBackup.Console.Commands;
using KopiaBackup.Console.Models.Kopia;
using KopiaBackup.Lib.Interfaces.Helpers;
using Moq;
using Shouldly;


namespace KopiaBackup.Console.Tests;

public class Test
{
    [Fact]
    public void RunCreateExternalS3Config_WithValidArguments_ReturnsZero()
    {
        // Arrange: set up the command line arguments.
        var args = new[]
        {
            "connect",
            "-t", "0",
            "-b", "asdf",
            "-e", "asdf",
            "-a", "asdf",
            "-s", "asdf",
            "-p", "asdf",
            "-c", "asdf"
        };

        // Create a mock for IKopiaHelper.
        var mockHelper = new Mock<IKopiaHelper>();
        // Optionally set up expectations:
        // mockHelper.Setup(h => h.DoSomething()).Verifiable();

        // Act: Parse the arguments and use MapResult to call our command method.
        int exitCode = Parser.Default.ParseArguments<KopiaRepositoryConnect>(args)
            .MapResult(
                options => KopiaCommands.RunCreateExternalS3Config(mockHelper.Object, options),
                errors => 1  // Return 1 in case of parse errors.
            );

        // Assert: Expect the exit code to be 0.
        exitCode.ShouldBe(0);
    }
}