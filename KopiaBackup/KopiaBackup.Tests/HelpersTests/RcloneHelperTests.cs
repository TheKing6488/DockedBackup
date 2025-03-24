using KopiaBackup.Lib.Helpers;
using Shouldly;

namespace KopiaBackup.Tests.HelpersTests;

public class RcloneHelperTests
{
#if DEBUG
    [Fact]
    public void IsRcloneInstalled_ReturnsTrue_WhenRcloneIsInstalled()
    {
        bool isInstalled = RcloneHelper.IsRcloneInstalled();

        isInstalled.ShouldBeTrue("rclone should be installed for this test to be successful.");
    }
#endif
}