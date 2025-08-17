using Dalamud.Game.ClientState.Objects.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HavenBeans;

public class Darts
{
    public static bool Started;

    public static Dictionary<ulong, IGameObject> Players;
    public static List<(string Name, Vector3 Position, int Count)> LandedPlayers = new();

    public static void StartTracking()
    {
        Players = null;

        if (Started)
            return;
        Started = true;

        Service.Framework.Update += Framework_Update;
    }
    public static void StopTracking()
    {
        Started = false;
        Players = null;
        Service.Framework.Update -= Framework_Update;
    }
    public static void Clear()
    {
        LandedPlayers.Clear();
    }

    private static void Framework_Update(Dalamud.Plugin.Services.IFramework framework)
    {
        var first = Players == null;
        Players ??= new();

        foreach (var obj in Service.ObjectTable)
        {
            if (obj.ObjectKind != Dalamud.Game.ClientState.Objects.Enums.ObjectKind.Player)
                continue;

            if (first)
            {
                if (obj.Position.Y > Plugin.Config.Floor.Y + Plugin.Config.MinJump)
                    Players.Add(obj.GameObjectId, obj);
                continue;
            }

            if (Players.ContainsKey(obj.GameObjectId))
            {
                if (obj.Position.Y <= Plugin.Config.Floor.Y + Plugin.Config.LandOffset)
                {
                    var name = obj.Name.ToString();
                    var duplicates = LandedPlayers.Count(x => x.Name == name);

                    LandedPlayers.Add((name, obj.Position, duplicates));
                    Players.Remove(obj.GameObjectId);
                }
            }
        }
    }
}
