using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ABBuild:Editor
{
    private static string m_AppName = PlayerSettings.productName;
    public static string m_AndroidPath = Application.dataPath + "/../BuildTarget/Android/";
    public static string m_IOSPath = Application.dataPath + "/../BuildTarget/IOS/";
    public static string m_WindowsPath = Application.dataPath + "/../BuildTarget/Windows/";
    

    public static void Bu()
    {

        Build();
        //File.WriteAllText(Application.dataPath + "/Resources/1.txt", "123");
    }

    [MenuItem("Build/标准包")]
   public  static void Build()
    {
        //打ab包
     //   BundleEditor.NormalBuild();
        //写入版本信息
        SaveVersion(PlayerSettings.bundleVersion, PlayerSettings.applicationIdentifier);
         
        //生成可执行程序
        string abPath = Application.dataPath + "/../AssetBundle/" + EditorUserBuildSettings.activeBuildTarget.ToString() + "/";
        Copy(abPath, Application.streamingAssetsPath);
        string savePath = "";
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
             
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "Debug;ABC");
           
           
            savePath = m_AndroidPath + m_AppName + "_" + EditorUserBuildSettings.activeBuildTarget + string.Format("_{0:yyyy_MM_dd_HH_mm}", DateTime.Now) + ".apk";

        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
        {
            savePath = m_IOSPath + m_AppName + "_" + EditorUserBuildSettings.activeBuildTarget + string.Format("_{0:yyyy_MM_dd_HH_mm}", DateTime.Now);
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows || EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64)
        {
            savePath = m_WindowsPath + m_AppName + "_" + EditorUserBuildSettings.activeBuildTarget + string.Format("_{0:yyyy_MM_dd_HH_mm}/{1}.exe", DateTime.Now, m_AppName);
        }
                                    //打包的场景             路径              构建目标
        BuildPipeline.BuildPlayer(FindEnableEditorrScenes(), savePath, EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
        DeleteDir(Application.streamingAssetsPath);
        Debug.Log(savePath);
        Debug.Log(0);
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 查找已打开的场景
    /// </summary>
    /// <returns></returns>
    private static string[] FindEnableEditorrScenes()
    {
        List<string> editorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            editorScenes.Add(scene.path);
        }
        return editorScenes.ToArray();
    }

    /// <summary>
    /// 删除指定文件夹下文件
    /// </summary>
    /// <param name="scrPath"></param>
    public static void DeleteDir(string scrPath)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(scrPath);
            FileSystemInfo[] fileInfo = dir.GetFileSystemInfos();
            foreach (FileSystemInfo info in fileInfo)
            {
                if (info is DirectoryInfo)
                {
                    DirectoryInfo subdir = new DirectoryInfo(info.FullName);
                    subdir.Delete(true);
                }
                else
                {
                    File.Delete(info.FullName);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    /// <summary>
    /// 拷贝文件到指定路径
    /// </summary>
    /// <param name="srcPath">源路径</param>
    /// <param name="targetPath">目标路径</param>
    private static void Copy(string srcPath, string targetPath)
    {
        try
        {
            if (!Directory.Exists(targetPath))//目标路径是否存在
            {
                Directory.CreateDirectory(targetPath);
            }

            string scrdir = Path.Combine(targetPath, Path.GetFileName(srcPath));
            if (Directory.Exists(srcPath))
                scrdir += Path.DirectorySeparatorChar;//路径+ 平台分隔符  window \   IOS/
            if (!Directory.Exists(scrdir))
            {
                Directory.CreateDirectory(scrdir);
            }

            string[] files = Directory.GetFileSystemEntries(srcPath);
            foreach (string file in files)
            {
                if (Directory.Exists(file))
                {
                    Copy(file, scrdir);
                }
                else
                {
                    File.Copy(file, scrdir + Path.GetFileName(file), true);
                }
            }
        }
        catch
        {
            Debug.LogError("无法复制：" + srcPath + "  到" + targetPath);
        }
    }

    /// <summary>
    /// 保存版本
    /// </summary>
    /// <param name="version">版本号</param>
    /// <param name="package">包名</param>
    private static void SaveVersion(string version, string package)
    {
        string content = "Version|" + version + ";PackageName|" + package + ";";
        string savePath = Application.dataPath + "/Resources/Version.txt";
        string oneLine = "";
        string all = "";
        using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
            using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
            {
                all = sr.ReadToEnd();
                oneLine = all.Split('\n')[0];
            }
        }
        using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
        {
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
            {
                if (string.IsNullOrEmpty(all))
                    all = content;
                else
                    all = all.Replace(oneLine, content);
                sw.Write(all);
            }
        }
    }
}
