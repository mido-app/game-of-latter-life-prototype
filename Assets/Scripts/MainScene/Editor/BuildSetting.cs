using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildSetting
{
    private static string[] levelsToPack = new string[] {
        "Assets/Scenes/TopScene.unity",
        "Assets/Scenes/MainScene.unity",
        "Assets/Scenes/ResultScene.unity",
    };

    [MenuItem("Build/Build Web")]
    public static void BuildWebGL()
    {
        PlayerSettings.WebGL.memorySize = 512;
        var path = EditorUtility.SaveFolderPanel("Save Build Result", "Assets", "LifeGamePrototype");
        UnityEditor.Build.Reporting.BuildReport report = BuildPipeline
            .BuildPlayer(
                levelsToPack,
                path,
                BuildTarget.WebGL,
                BuildOptions.None
        );
    }
}
