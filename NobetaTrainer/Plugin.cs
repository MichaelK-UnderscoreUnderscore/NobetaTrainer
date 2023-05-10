﻿using System.Threading;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using NobetaTrainer.Overlay;
using NobetaTrainer.Patches;
using NobetaTrainer.Utils;
using UnityEngine;

namespace NobetaTrainer;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("LittleWitchNobeta")]
public class Plugin : BasePlugin
{
    internal new static ManualLogSource Log;

    public static TrainerOverlay TrainerOverlay;

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        // Create and show overlay
        TrainerOverlay = new TrainerOverlay();
        Task.Run(TrainerOverlay.Run);

        // Apply patches
        ApplyPatches();

        // Add UnityMainThreadDispatcher
        AddComponent<UnityMainThreadDispatcher>();
    }

    public static void ApplyPatches()
    {
        Harmony.CreateAndPatchAll(typeof(Singletons));
        Harmony.CreateAndPatchAll(typeof(CharacterPatches));
        Harmony.CreateAndPatchAll(typeof(AppearancePatches));
        Harmony.CreateAndPatchAll(typeof(MovementPatches));
        Harmony.CreateAndPatchAll(typeof(OtherPatches));
        Harmony.CreateAndPatchAll(typeof(ItemPatches));
    }
}