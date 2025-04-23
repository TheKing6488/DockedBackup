using KopiaBackup.Lib.Helpers;
using Shouldly;

namespace KopiaBackup.Tests.HelpersTests;

public class RcloneHelperTests
{

    [Fact]
    public void IsRcloneInstalled_ReturnsFalse_WhenRcloneIsNotInstalled()
    {
        RcloneHelper rcloneHelper = new();
         rcloneHelper.RcloneConfigAsync();
    }
}