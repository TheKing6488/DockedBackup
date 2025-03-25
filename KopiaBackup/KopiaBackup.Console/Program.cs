// Erstelle und konfiguriere die ServiceCollection

using KopiaBackup.Lib.DependencyInjection;
using KopiaBackup.Lib.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;


var services = new ServiceCollection();
            
services.AddKopiaBackupServices();
            
var serviceProvider = services.BuildServiceProvider();
            
var backupService = serviceProvider.GetRequiredService<IBackupService>();

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);

// Baue den ServiceProvider
var serviceProvider = serviceCollection.BuildServiceProvider();

// Hole den Service aus dem Provider und nutze ihn
var greetingService = serviceProvider.GetService<IGreetingService>();

greetingService.Greet("Welt");

();

Console.WriteLine("Hello, World!");