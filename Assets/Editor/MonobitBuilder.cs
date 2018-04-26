using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEditor;

public class MonobitBuilder {

    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "YourProject";
    static string TARGET_DIR = "target";

    [MenuItem("Custom/CI/Build Mac OS X")]
    static void PerformMacOSXBuild()
    {
        string target_dir = APP_NAME + ".app";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTargetGroup.iOS, BuildTarget.iOS, BuildOptions.None);
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