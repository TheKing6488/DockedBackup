using Docker.DotNet;
using Docker.DotNet.Models;

namespace DockedBackup.Services;

public class CoolifyService
{
    private readonly DockerClient _client;
    public CoolifyService()
    {
        _client = new DockerClientConfiguration(
             new Uri("unix:///var/run/docker.sock") // Linux
        ).CreateClient();
    }
    
    public async Task RestoreAsync(
        string containerId,
        string localDumpPath,
        string[] pgRestoreArgs,
        string dbPassword)
    {
        // 1. Exec erstellen
        var execCreate = await _client.Exec.ExecCreateContainerAsync(
            containerId,
            new ContainerExecCreateParameters
            {
                AttachStderr = true,
                AttachStdout = true,
                AttachStdin  = true,
                Cmd = new[] {
                    "pg_restore",
                    "--verbose", "--clean", "--no-acl", "--no-owner",
                    "-U", pgRestoreArgs[0], "-d", pgRestoreArgs[1]
                },
                Env = new[] { $"PGPASSWORD={dbPassword}" }
            });

        // 2. Streams öffnen
        using var mux = await _client.Exec.StartAndAttachContainerExecAsync(
            execCreate.ID, false);

        // 3. Dump-Datei in den Stream kopieren
        await using var fs = File.OpenRead(localDumpPath);
        await fs.CopyToAsync(mux);

        // 4. Input schließen und auf Ende warten
        mux.CloseWrite();
        var inspect = await _client.Exec.InspectContainerExecAsync(execCreate.ID);

        if (inspect.ExitCode != 0)
            throw new InvalidOperationException($"Exec schlug fehl: Code {inspect.ExitCode}");
    }
}