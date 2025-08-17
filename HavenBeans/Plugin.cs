using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
namespace HavenBeans;

public sealed class Plugin : IDalamudPlugin
{
    public static Config Config { get; private set; }

    private WindowSystem WindowSystem = new("HavenBeans");    
    private MainWindow mainWindow;

    public string Name => "HavenBeans";

    public Plugin(IDalamudPluginInterface pluginInterface)
    {
        pluginInterface.Create<Service>();

        Config = pluginInterface.GetPluginConfig() as Config ?? new Config();
        Config.Initialize(pluginInterface);

        mainWindow = new();
        WindowSystem.AddWindow(mainWindow);

        Service.PluginInterface.UiBuilder.Draw += WindowSystem.Draw;
        Service.PluginInterface.UiBuilder.OpenMainUi += OpenMainUi;
        Service.PluginInterface.UiBuilder.OpenConfigUi += OpenMainUi;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();
        mainWindow.Dispose();
        Darts.StopTracking();
    }

    private void OpenMainUi()
    {
        mainWindow.IsOpen = true;
    }
}