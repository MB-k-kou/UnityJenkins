using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEditor;

public class MonobitBuilder {

    static string[] SCENES = FindEnabledEditorScenes();

    [MenuItem("Custom/CI/Build Mac OS X")]
    public static void PerformMacOSXBuild()
    {
        GenericBuild(SCENES, "IOSBuild", BuildTargetGroup.iOS, BuildTarget.iOS, BuildOptions.None);
    }

    [MenuItem("Custom/CI/Build Android")]
    public static void PerformAndroidBuild()
    {
        GenericBuild(SCENES, "AndroidBuild.apk", BuildTargetGroup.Android, BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("Custom/CI/Build Windows")]
    public static void PerformWindowsBuild()
    {
        GenericBuild(SCENES, "WindowsBuild\\Release.exe", BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTargetGroup group, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(group, build_target);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = target_dir;
        buildPlayerOptions.target = build_target;
        buildPlayerOptions.options = build_options;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}