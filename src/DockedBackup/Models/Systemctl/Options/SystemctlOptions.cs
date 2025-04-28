using System.Diagnostics.CodeAnalysis;
using CommandLine;

 namespace DockedBackup.Models.Systemctl.Options
 {
  [Verb("systemctl", HelpText = "Create a systemd service")]
  public class SystemctlOptions
  {
   [Option('d', "device-id", Required = true, HelpText = "Name of the specific device")]
   public string DeviceId { get; set; } 
   
   public SystemctlOptions(){
  
   }
  }
 }