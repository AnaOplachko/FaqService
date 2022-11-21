using System.Runtime.InteropServices;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace FaqService.ComponentTests.ExternalEnvironment.Containers;

/// <summary>
/// Базовый контейнер для работы с Docker Api
/// </summary>
public abstract class BaseContainer
{
    protected readonly DockerClient DockerClient;
    protected string ContainerId = null!;

    protected string Image = null!;
    protected string Tag = null!;

    protected string ImageFull => $"{Image}:{Tag}";

    /// <summary>
    /// ctor
    /// </summary>
    protected BaseContainer()
    { 
        DockerClient = new DockerClientConfiguration(new Uri(DockerApiUri())).CreateClient();
    }

    /// <summary>
    /// Получение Url API Docker
    /// </summary>
    /// <returns>Url</returns>
    private string DockerApiUri()
    {
        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        if (isWindows)
            return "npipe://.pipe/docker_engine";

        var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        if (isLinux)
            return "tcp://127.0.0.1:2375";

        var isOsx = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        if (isOsx)
            return "unix:///var/run/docker.sock";

        throw new Exception("Was unable to determine what OS this is running on, does not appear to be Windows or Linux!?");
    }

    /// <summary>
    /// Скачать образ
    /// </summary>
    protected async Task PullImage(string image, string tag)
    {
        await DockerClient.Images
            .CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = image,
                Tag = tag
            }, 
                new AuthConfig(),
                new Progress<JSONMessage>());
    }

    /// <summary>
    /// Остановить все контейнеры
    /// </summary>
    public async Task StopAllContainers()
    {
        IList<ContainerListResponse> containers =
            await DockerClient.Containers.ListContainersAsync(new ContainersListParameters());

        foreach (var container in containers)
        {
            await DockerClient.Containers.KillContainerAsync(container.ID, new ContainerKillParameters());
            await DockerClient.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters());
        }
    }

    /// <summary>
    /// Запустить контейнер
    /// </summary>
    public abstract Task StartContainer();

    /// <summary>
    /// Ожидание готовности контейнера
    /// </summary>
    protected abstract Task WaitContainer();
}