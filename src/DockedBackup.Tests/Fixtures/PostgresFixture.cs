using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Xunit;

public class CustomImageFixture : IAsyncLifetime
{
    // now holds the running container, not the builder
    public TestcontainersContainer Container { get; private set; }

    // English comment: Build image from local Dockerfile and start the container.
    public async Task InitializeAsync()
    {
        var imageName = await new ImageFromDockerfileBuilder()
            .WithName("dockedbackup")
            .WithDockerfileDirectory("./DockerTest")
            .Build();                           // 1) BUILD  -> imageName zurück

        Container = new TestcontainersBuilder<TestcontainersContainer>()
            .WithImage(imageName)               // 2) RUN    -> Container aus Image
            .WithEntrypoint("/usr/local/bin/entrypoint.sh")
            .WithCleanUp(true)
            .Build();

        await Container.StartAsync();
 
        
        //
        // Container = new TestcontainersBuilder<TestcontainersContainer>()
        //     .WithCleanUp(true)    
        //     .WithName("dockedbackup")
        //     .WithImage("dockedbackup")
        //     .Build();
        //
        // await Container.StartAsync().ConfigureAwait(false);
    }

    // English comment: Stop and remove container after all tests.
    public async Task DisposeAsync()
    {
        await Container.StopAsync().ConfigureAwait(false);
    }
}