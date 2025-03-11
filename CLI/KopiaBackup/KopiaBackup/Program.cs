using System;
using System.IO;

if (!OperatingSystem.IsLinux())
{
    Console.Error.WriteLine("Dieses Backup-Tool ist nur unter Linux lauffähig.");
    Environment.Exit(1);
}


var watcher = new FileSystemWatcher("/run/media/tobias")
{
    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
    Filter = "*.*" // Überwacht alle Dateien
};

watcher.Created += (sender, e) =>
    Console.WriteLine($"Datei erstellt: {e.FullPath}");
watcher.Deleted += (sender, e) =>
    Console.WriteLine($"Datei gelöscht: {e.FullPath}");
watcher.Changed += (sender, e) =>
    Console.WriteLine($"Datei geändert: {e.FullPath}");
watcher.Renamed += (sender, e) =>
    Console.WriteLine($"Datei umbenannt: {e.OldFullPath} -> {e.FullPath}");

watcher.EnableRaisingEvents = true;

Console.WriteLine("Überwachung gestartet. Drücke 'q' zum Beenden.");
while (Console.Read() != 'q') ;