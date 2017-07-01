using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

#region enum
//enum AlbedoType { Everything, Albedo, Base_Color,}
//enum NormalType { Everything, Normal, OpenGL}
//enum AOType {Everything, AO, Mixed_AO }
#endregion

public class FindTexture : EditorWindow
{
    #region enam
    //AlbedoType albedoType = AlbedoType.Albedo;
    //NormalType normalType = NormalType.Normal;
    //AOType aoType = AOType.AO;
    #endregion

    public List<Material> material = new List<Material>();
  

    List<string> path = new List<string>();

    string Albedo;
    string Metalic;
    string Normal;
    string Oclussion;
    string Specular;

    bool InProject = false;
    bool ForObject = false;

    Object FBX;

    [MenuItem("Extension/FindTexture")]
    static void findTextureExample()
    {
        FindTexture window = EditorWindow.GetWindow<FindTexture>(true);
        window.Show();
    }

    void OnGUI()
    {

        #region StringName of a certain type texture and Enum
        //Albedo = EditorGUILayout.TextField("Name Albedo", Albedo);
        //Metalic = EditorGUILayout.TextField("Name Metalic", Metalic);
        //Normal = EditorGUILayout.TextField("Name Normal", Normal);
        //Oclussion = EditorGUILayout.TextField("Name Oclission", Oclussion);
        //Specular = EditorGUILayout.TextField("Name Specullar", Specular);

        //albedoType = (AlbedoType)EditorGUILayout.EnumPopup("Albedo Type", albedoType);
        //normalType = (NormalType)EditorGUILayout.EnumPopup("Normal Type", normalType);
        //aoType = (AOType)EditorGUILayout.EnumPopup("AO Type", aoType);
        #endregion



        #region VisualisationListMaterial
        //for (int i = 0; i < material.Count; i++)
        //{
        //    material[i] = (Material)EditorGUILayout.ObjectField(material[i], typeof(Material));
        //}
        #endregion

        InProject = EditorGUILayout.Toggle("In Project", InProject);

        if (InProject)
        {
            if (GUILayout.Button("SetTexture"))
            {
                this.path = GetDirectoryFBX();
                foreach (var item in this.path)
                {
                    SetMaterial(item);
                }
                Debug.Log("Complited");
                //SetMaterial(path[0]);
                //Debug.Log(path[5]);
            }
        }

        ForObject = EditorGUILayout.Toggle("For Object", ForObject);

        if (ForObject)
        {
            FBX = EditorGUILayout.ObjectField("FBX", FBX, typeof(GameObject));
            if (GUILayout.Button("SetTexture"))
            {
                FindTextureFBX();
            }
        }
    }

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

    private void SetMaterial(string path)
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

    void SetTExture(Material material, string path)
    {
        List<Texture> texture = GetTexture(path);
        #region Custom
        //foreach (var item in texture)
        //{
        //    if (item.name == string.Concat(material.name,Albedo))
        //    {
        //        Debug.Log("item.name :" + item.name + ":::::::" + "material.name, Albedo:" + string.Concat(material.name, Albedo));
        //        material.SetTexture( "_MainTex", item);
        //    }
        //    if (item.name == string.Concat(material.name, Metalic))
        //    {
        //        Debug.Log("item.name :" + item.name + ":::::::" + "material.name, Metalic:" + string.Concat(material.name, Metalic));

        //        material.SetTexture("_MetallicGlossMap", item);
        //    }
        //    if (item.name == string.Concat(material.name, Normal))
        //    {
        //        Debug.Log("item.name :" + item.name + ":::::::" + "material.name, Normal:" + string.Concat(material.name, Normal));

        //        material.SetTexture("_BumpMap", item);
        //    }
        //    if (item.name == string.Concat(material.name, Oclussion))
        //    {
        //        Debug.Log("item.name :" + item.name + ":::::::" + "material.name, Oclussion:" + string.Concat(material.name, Oclussion));

        //        material.SetTexture("_OcclusionMap", item);
        //    }
        //    if (item.name == string.Concat(material.name, Specular))
        //    {
        //        Debug.Log("item.name :" + item.name + ":::::::" + "material.name, Oclussion:" + string.Concat(material.name, Oclussion));

        //        material.SetTexture("_SpecGlossMap", item);
        //    }
        //}
        //foreach (var item in texture)
        //{
        //    if (albedoType == AlbedoType.Everything)
        //    {
        //        if (item.name.Contains(material.name) && item.name.Contains("Albedo") || item.name.Contains("basecolor") || item.name.Contains("base_color"))
        //        {
        //            material.SetTexture("_MainTex", item);
        //        }
        //    }

        //    if (albedoType == AlbedoType.Albedo)
        //    {
        //        if (item.name.Contains(material.name) && item.name.Contains("Albedo"))
        //        {
        //            material.SetTexture("_MainTex", item);
        //        }
        //    }

        //    if (albedoType == AlbedoType.Base_Color)
        //    {
        //        if (item.name.Contains(material.name) && item.name.Contains("basecolor") || item.name.Contains("base_color"))
        //        {
        //            material.SetTexture("_MainTex", item);
        //        }
        //    }

        //    if (normalType == NormalType.Everything)
        //    {
        //        if (item.name.Contains(material.name) && item.name.Contains("Normal") || item.name.Contains("NormalOpenGL") || item.name.Contains("Normal_OpenGL"))
        //        {
        //            material.SetTexture("_BumpMap", item);
        //        }
        //    }

        //    if (normalType == NormalType.Normal)
        //    {
        //        if (item.name.Contains(material.name) && item.name.Contains("Normal"))
        //        {
        //            material.SetTexture("_BumpMap", item);
        //        }
        //    }

        //    if (normalType == NormalType.OpenGL)
        //    {
        //        if (item.name.Contains(material.name) &&  item.name.Contains("NormalOpenGL") || item.name.Contains("Normal_OpenGL"))
        //        {
        //            material.SetTexture("_BumpMap", item);
        //        }
        //    }

        //    if (aoType == AOType.Everything)
        //    {
        //        if (item.name.Contains(material.name) && item.name.Contains("AO.") || item.name.Contains("Mixed_AO") || item.name.Contains("MixedAO"))
        //        {
        //            material.SetTexture("_OcclusionMap", item);
        //        }
        //    }
        //    if (aoType == AOType.AO)
        //    {
        //        if (item.name.Contains(material.name) && item.name.Contains("AO"))
        //        {
        //            material.SetTexture("_OcclusionMap", item);
        //        }
        //    }

        //    if (aoType == AOType.Mixed_AO)
        //    {
        //        if (item.name.Contains(material.name) && item.name.Contains("Mixed_AO") || item.name.Contains("MixedAO"))
        //        {
        //            material.SetTexture("_OcclusionMap", item);
        //        }
        //    }

        //    if (item.name.Contains(material.name) && item.name.Contains("Metalic"))
        //        {
        //            material.SetTexture("_MetallicGlossMap", item);
        //        }

        //    if (item.name.Contains(material.name) && item.name.Contains("Specular"))
        //        {
        //            material.SetTexture("_SpecGlossMap", item);
        //        }
        //}
        #endregion

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

    List<Texture> GetTexture(string path)
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

     void FindTextureFBX()
    {
        string tmp = EditorUtility.GetAssetPath(FBX);
        
        tmp = tmp.Remove(tmp.LastIndexOf('/'), tmp.Length - tmp.LastIndexOf('/'));
        
        if(FBX!=null) SetMaterial(tmp); 
    }

}