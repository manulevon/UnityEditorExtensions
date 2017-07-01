using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

#region enum
//enum AlbedoType { Everything, Albedo, Base_Color,}
//enum NormalType { Everything, Normal, OpenGL}
//enum AOType {Everything, AO, Mixed_AO }
#endregion

public class SetTexture 
{

    public static List<Material> material = new List<Material>();
    List<string> path = new List<string>();
    static Object FBX;

    private List<string> GetDirectoryFBX()
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


    [MenuItem("Assets/SetTexture")]
    private static void SetMaterial()
    {
        FindTextureFBX(Selection.activeGameObject);
        Debug.Log("Complited");
        
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

     static void FindTextureFBX(GameObject a)
    {
        string tmp = EditorUtility.GetAssetPath(a);
        Debug.Log(tmp);
        tmp = tmp.Remove(tmp.LastIndexOf('/'), tmp.Length - tmp.LastIndexOf('/'));
        
        if(a!=null) SetMaterial(tmp); 
    }

}