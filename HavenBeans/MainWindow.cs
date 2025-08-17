using Dalamud.Interface.Windowing;
﻿using Dalamud.Bindings.ImGui;
using Lumina.Excel.Sheets;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.Types;

namespace HavenBeans;

public class MainWindow : Window, IDisposable
{
    public MainWindow() : base("Haven Beans")
    {
        SizeCondition = ImGuiCond.FirstUseEver;
        SizeConstraints = new WindowSizeConstraints()
        {
            MinimumSize = new Vector2(300, 100)            
        };
        Size = new Vector2(300, 600);
    }

    public override void Draw()
    {
        if (ImGui.Button("Set Floor"))
        {
            Service.Framework.RunOnFrameworkThread(() =>
            {
                Plugin.Config.Floor = Service.ClientState.LocalPlayer.Position;
                Plugin.Config.Save();
            });
        }
        ImGui.SameLine();
        ImGui.Text("Floor Level: " + Plugin.Config.Floor.Y);
        if (ImGui.Button("Set Target"))
        {
            Service.Framework.RunOnFrameworkThread(() =>
            {
                IGameObject obj = Service.ClientState.LocalPlayer;
                if (Service.Targets.Target != null)
                    obj = Service.Targets.Target;
                Plugin.Config.Target = obj.Position;
                Plugin.Config.Save();
            });
        }
        ImGui.SameLine();
        ImGui.Text("Target: " + AsString(Plugin.Config.Target));
        if (ImGui.InputFloat("Jump Height", ref Plugin.Config.MinJump, 0.1f))
            Plugin.Config.Save();

        if (ImGui.Button("Start"))
            Darts.StartTracking();
        ImGui.SameLine();
        if (ImGui.Button("Stop"))
            Darts.StopTracking();
        if (Darts.Started)
        {
            ImGui.SameLine();
            ImGui.Text("Tracking...");
        }
        if (ImGui.Button("Clear"))
            Darts.Clear();

        try
        {
            var players = Darts.Players?.ToList() ?? new();
            ImGui.Text("Active Players:");
            foreach (var p in players)
            {
                ImGui.Text($"  {p.Value.Name}: {AsString(p.Value.Position)}");
            }
            if (players.Count == 0)
                ImGui.Text("  none");
        }
        catch { }

        if (Darts.LandedPlayers.Any())
        {
            ImGui.Spacing();
            ImGui.Text("Landed:");
            foreach (var p in Darts.LandedPlayers.OrderBy(x => Between2D(x.Position, Plugin.Config.Target)))
            {
                ImGui.Text($"  {p.Name}{(p.Count > 1 ? "-" + p.Count:"")}: {Between2D(p.Position, Plugin.Config.Target):0.000}");
            }
        }
    }

    public static float Between2D(Vector3 p1, Vector3 p2)
    {
        var obj1Pos = new Vector2(p1.X, p1.Z);
        var obj2Pos = new Vector2(p2.X, p2.Z);

        return Vector2.Distance(obj1Pos, obj2Pos);
    }
    public static string AsString(Vector3 v)
    {
        return $"<{v.X:0.00}, {v.Y:0.00}, {v.Z:0.00}>";
    }

    public void Dispose()
    {
    }
}
