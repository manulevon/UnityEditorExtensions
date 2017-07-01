using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;



public class FindTexture 
{

    public static List<Material> material = new List<Material>();


    static List<string> path = new List<string>();


    bool InProject = false;
    bool ForObject = false;

    private static List<string> GetDirectoryFBX()
    {
        string[] files = Directory.GetFiles(Application.dataPath, "*.fbx", SearchOption.AllDirectories);

        List<string> path = new List<string>();
        foreach (var item in files)
        {
            string tmp = item.Remove(0, Application.dataPath.Length - 6);
            tmp = tmp.Remove(tmp.LastIndexOf('\\'), tmp.Length - tmp.LastIndexOf('\\'));
            path.Add(tmp);
        }
        return path;
    }

    private static void SetMaterial(string path)
    {
        material = new List<Material>();
        string[] pathMaterial = Directory.GetFiles(path, "*.mat", SearchOption.AllDirectories);
        foreach (var item in pathMaterial)
        {
            Material tmp = (Material)AssetDatabase.LoadAssetAtPath(item, typeof(Material));
            material.Add(tmp);
            SetTExture(tmp, path);
        }
        foreach (var item in material)
        {
            item.EnableKeyword("_NORMALMAP");
            item.EnableKeyword("_METALLICGLOSSMAP");
            item.EnableKeyword("_SPECGLOSSMAP");

        }

    }

    static void SetTExture(Material material, string path)
    {
        List<Texture> texture = GetTexture(path);

        foreach (var item in texture)
        {
            if (item.name.Contains(material.name) && item.name.Contains("Albedo") || item.name.Contains("basecolor") || item.name.Contains("base_color"))
            {
                material.SetTexture("_MainTex", item);
            }

            if (item.name.Contains(material.name) && item.name.Contains("Normal") || item.name.Contains("NormalOpenGL") || item.name.Contains("Normal_OpenGL"))
            {
                material.SetTexture("_BumpMap", item);
            }

            if (item.name.Contains(material.name) && item.name.Contains("AO") || item.name.Contains("Mixed_AO") || item.name.Contains("MixedAO"))
            {
                material.SetTexture("_OcclusionMap", item);
            }

            if (item.name.Contains(material.name) && item.name.Contains("Metalic"))
            {
                material.SetTexture("_MetallicGlossMap", item);
            }

            if (item.name.Contains(material.name) && item.name.Contains("Specular"))
            {
                material.SetTexture("_SpecGlossMap", item);
            }
        }

    }

    static List<Texture> GetTexture(string path)
    {
        string[] pathPng = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
        string[] pathJpg = Directory.GetFiles(path, "*.jpg.", SearchOption.AllDirectories);
        string[] pathTga = Directory.GetFiles(path, "*.tga.", SearchOption.AllDirectories);
        string[] pathTiff = Directory.GetFiles(path, "*.tiff.", SearchOption.AllDirectories);
        string[] pathPsd = Directory.GetFiles(path, "*.psd.", SearchOption.AllDirectories);

        List<string> pathTexture = new List<string>();
        #region Set PathTexture
        foreach (var item in pathPng)
        {
            pathTexture.Add(item);
        }
        foreach (var item in pathJpg)
        {
            pathTexture.Add(item);
        }
        foreach (var item in pathTga)
        {
            pathTexture.Add(item);
        }
        foreach (var item in pathTiff)
        {
            pathTexture.Add(item);
        }
        foreach (var item in pathPsd)
        {
            pathTexture.Add(item);
        }
        #endregion

        List<Texture> tmp = new List<Texture>();
        foreach (var item in pathTexture)
        {
            Texture temp = (Texture)AssetDatabase.LoadAssetAtPath(item, typeof(Texture));
            tmp.Add(temp);

        }

        return tmp;
    }

    static void FindTextureFBX(GameObject obj)
    {
        string tmp = EditorUtility.GetAssetPath(obj);
       
        tmp = tmp.Remove(tmp.LastIndexOf('/'), tmp.Length - tmp.LastIndexOf('/'));
        SetMaterial(tmp);
    }

    [MenuItem("Assets/SetTextureObject")]
    private static void SetTextureObject()
    {
        
            FindTextureFBX(Selection.activeGameObject);
            Debug.Log("Complited");
    }

    [MenuItem("Assets/SetTextureProject")]
    private static void SetTextureProject()
    {
        path = GetDirectoryFBX();
        foreach (var item in path)
        {
            SetMaterial(item);
        }
        Debug.Log("Complited");
   
    }

    [MenuItem("Assets/SetTextureObject", true)]
    private static bool NewMenuOptionValidation()
    {
        return Selection.activeObject.GetType() == typeof(GameObject);
    }


}

