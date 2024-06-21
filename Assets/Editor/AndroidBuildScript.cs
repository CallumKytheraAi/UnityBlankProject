using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class AndroidBuildScript
{
	
	public static void MyBuild()
    {
        /*
            Example command: 
            ./Unity.exe -projectpath C:/dev/unity/myProjPath -quit -batchmode -noGraphics -executeMethod AndroidBuildScript.MyBuild -buildPath "c:/dev/my/folder/" -apkName myApkName.apk            
            Remember, if you are having problems with this script you can run it without these two params: "-batchmode -noGraphics". This will make it easier to debug as the Editor will open but still be run automatically.
        */

        // Defaults. Expected to be overriden with commandline args.
        string buildPath = "c:/dev/UnityOutput/Automated/Try3/";
        string apkName = "androidBuild.apk";

        // Extract parameters from the command line.
        string[] args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            Debug.Log(args[i]);
            
            if (args[i].Contains("buildPath")) { 
                // Debug.Log("Detected this arg was 'buildPath'.");
                buildPath = args[i+1];

                if(buildPath[buildPath.Length - 1] != '/') buildPath += "/";
            }

            if (args[i].Contains("apkName")) { 
                // Debug.Log("Detected this arg was 'apkName'.");
                apkName = args[i+1];
            }
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        // The Unity blank project has this scene and is already added to the build, but we do it here as well anyway.
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/SampleScene.unity" };
        buildPlayerOptions.locationPathName = buildPath + apkName;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        // These are used to automatically sign keystores. Might become an issue at some point but seems ok for now (June 2024)
        // PlayerSettings.keystorePass = "android";
        // PlayerSettings.keyaliasPass = "android";

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        // It has to be said that these reports are not very useful.
        BuildSummary summary = report.summary;
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build Succeeded... Logging summary... ");
            // Space to do something else
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build Failed... Logging summary... ");
            // Space to do something else
        }

        Debug.Log("<Start of summary>" + summary + "<End of summary>");
    }
}
