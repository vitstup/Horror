using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class UnusedAssetCleaner
{
    [MenuItem("Tools/Clean Unused Materials and Textures")]
    public static void CleanUnusedAssets()
    {
        GameObject prefab = Selection.activeObject as GameObject;
        if (prefab == null)
        {
            Debug.LogError("������ ������ � Project!");
            return;
        }

        string folderPath = EditorUtility.OpenFolderPanel("������ ����� � �����������/����������", "Assets", "");
        if (string.IsNullOrEmpty(folderPath)) return;

        folderPath = folderPath.Replace(Application.dataPath, "Assets");

        HashSet<string> usedAssets = new HashSet<string>();

        // �������� ��� ��������� � �������� �� �������
        Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>(true);
        foreach (var renderer in renderers)
        {
            foreach (var mat in renderer.sharedMaterials)
            {
                if (mat == null) continue;
                string matPath = AssetDatabase.GetAssetPath(mat);
                if (!string.IsNullOrEmpty(matPath)) usedAssets.Add(matPath);

                foreach (var name in mat.GetTexturePropertyNames())
                {
                    Texture tex = mat.GetTexture(name);
                    if (tex != null)
                    {
                        string texPath = AssetDatabase.GetAssetPath(tex);
                        if (!string.IsNullOrEmpty(texPath)) usedAssets.Add(texPath);
                    }
                }
            }
        }

        // ���� ��� ��������� � �������� � �����
        string[] allAssets = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
        int deletedCount = 0;

        foreach (string assetPath in allAssets)
        {
            string relativePath = assetPath.Replace("\\", "/");
            if (!relativePath.StartsWith("Assets")) continue;

            if (usedAssets.Contains(relativePath)) continue;

            Object asset = AssetDatabase.LoadAssetAtPath<Object>(relativePath);
            if (asset is Material || asset is Texture)
            {
                bool success = AssetDatabase.DeleteAsset(relativePath);
                if (success)
                {
                    Debug.Log("�������: " + relativePath);
                    deletedCount++;
                }
            }
        }

        Debug.Log($"������. ������� {deletedCount} �������������� ����������/�������.");
    }
}