using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundlePacker : EditorWindow
{
    [MenuItem("Window/AssetBundle Packer")]
    public static void ShowWindow()
    {
        GetWindow<AssetBundlePacker>("AssetBundle Packer");
    }

    private string baseFolderPath = "Assets/AssetBundles/";

    private void OnGUI()
    {
        GUILayout.Label("AssetBundle Packer", EditorStyles.boldLabel);
        GUILayout.Space(10);

        baseFolderPath = EditorGUILayout.TextField("Base Folder Path:", baseFolderPath);

        if (GUILayout.Button("Set Asset Bundle Names"))
        {
            SetAssetBundleNames();
        }
    }

    private void SetAssetBundleNames()
    {
        string[] subfolders = Directory.GetDirectories(baseFolderPath);

        foreach (string subfolder in subfolders)
        {
            string bundleName = Path.GetFileName(subfolder).ToLower(); // Use the subfolder name as the bundle name
            string[] assetPaths = Directory.GetFiles(subfolder, "*", SearchOption.AllDirectories);

            foreach (string assetPath in assetPaths)
            {
                AssetImporter importer = AssetImporter.GetAtPath(assetPath);
                if (importer != null)
                {
                    importer.assetBundleName = bundleName;
                    importer.assetBundleVariant = "";
                    Debug.Log("Asset Bundle name set for: " + assetPath);
                }
            }
        }
    }
}
