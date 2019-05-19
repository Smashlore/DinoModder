using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
public class DinoWindow : EditorWindow
{
    DinoModSave m_modSave; 
    [MenuItem("DinoModder/Make")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DinoWindow));
    }
    private void OnDestroy()
    {
        EditorUtility.SetDirty(m_modSave);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Saved");

    }
    public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
    {
        
        if (null == texture) return;

        string assetPath = AssetDatabase.GetAssetPath(texture);
        var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (tImporter != null)
        {
            tImporter.textureType = TextureImporterType.Default;
            tImporter.isReadable = isReadable;

            AssetDatabase.ImportAsset(assetPath);
            AssetDatabase.Refresh();
        }
    }
    void OnGUI()
    {
        

 
        if (m_modSave == null)
        {
            m_modSave = (DinoModSave)EditorGUIUtility.Load("Assets/Resources/modInfo.asset") as DinoModSave;
        }
        if (m_modSave == null)
        {
            m_modSave = CreateInstance<DinoModSave>();
            Debug.Log("Creating Mod save file");
            if (!Directory.Exists("Assets/Resources/"))
            {
                Directory.CreateDirectory("Assets/Resources/");
            }
            AssetDatabase.CreateAsset(m_modSave, "Assets/Resources/modInfo.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft };
        
        m_modSave.m_previewImage = (Texture2D)EditorGUILayout.ObjectField("Preview Image(1mb max)", m_modSave.m_previewImage, typeof(Texture2D), false);
        if (m_modSave.m_previewImage != null)
        {
            try
            {
                m_modSave.m_previewImage.GetPixels32();                
            }
            catch (UnityException e)
            {
                SetTextureImporterFormat(m_modSave.m_previewImage, true);
            }
        }
      
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();        
            GUILayout.Label("Mod Name", style);
        m_modSave.m_name = GUILayout.TextField(m_modSave.m_name);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Mod Description", EditorStyles.boldLabel);

        m_modSave.m_des = GUILayout.TextArea(m_modSave.m_des, GUILayout.Height(100));


        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mod Count", style);
        
        string exportCountStr = (GUILayout.TextField(m_modSave.m_expotCount.ToString()));
        int result = 0;
        int arrayCount = m_modSave.m_expotCount;
        if (int.TryParse(exportCountStr,out result))
        {
            m_modSave.m_expotCount = result;
        }
        m_modSave.m_expotCount = Mathf.Clamp(m_modSave.m_expotCount,1, 1000);
        EditorGUILayout.EndHorizontal();
         
        if(m_modSave.m_exportList.Count == 0)
        {
            m_modSave.m_exportList = new List<GameObject>();
            m_modSave.m_exportList.Add(null);
        }
        if (arrayCount != m_modSave.m_exportList.Count)
        {
            while (m_modSave.m_exportList.Count > arrayCount) m_modSave.m_exportList.RemoveAt(m_modSave.m_exportList.Count - 1);
            while (m_modSave.m_exportList.Count < arrayCount) m_modSave.m_exportList.Add(null);
        }
        for (int x = 0; x < m_modSave.m_exportList.Count; x++)
        {
            string name = "(Missing DinoAsset Script)";

            if (m_modSave.m_exportList[x])
            {
                DinoAsset meta = m_modSave.m_exportList[x].GetComponent<DinoAsset>();                
                if(meta)
                {
                    name = meta.m_name;
                }
            }

            m_modSave.m_exportList[x] = (GameObject)EditorGUILayout.ObjectField(name, m_modSave.m_exportList[x], typeof(GameObject), false);
        }
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(m_modSave);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Saved");
        }
 
        if (GUILayout.Button("Create/Update Mod"))
        {

            if (m_modSave.m_guid == 0)
            {
                byte[] buffer = System.Guid.NewGuid().ToByteArray();
                m_modSave.m_guid = System.BitConverter.ToInt64(buffer, 0);
            }
            string path = Path.GetFullPath(Application.persistentDataPath + "/../../Washbear/Parkasaurus/Mods/" + m_modSave.m_guid.ToString());
             
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            GUILayout.Label("Mod Description", EditorStyles.boldLabel);
            int count = m_modSave.m_exportList.Count;
            AssetBundleBuild[] buildMap = new AssetBundleBuild[1];


            List<string> dependencies = new List<string>();
            List<string> dependenciesName = new List<string>();

            {
                string assetPath = AssetDatabase.GetAssetPath(m_modSave);
                dependenciesName.Add("m_modSave");
                dependencies.Add(assetPath);
                Debug.Log("Adding " + assetPath + " added to mod pack");
            }
            if (m_modSave.m_previewImage)
            {
                string assetPath = AssetDatabase.GetAssetPath(m_modSave.m_previewImage);
                dependencies.Add(assetPath);
                Debug.Log("Adding " + assetPath + " added to mod pack");
            }

            for (int x = 0; x < count; x++)
            {
                string assetPath = AssetDatabase.GetAssetPath(m_modSave.m_exportList[x]);
                dependencies.Add(assetPath);
                Debug.Log("Adding " + assetPath + " added to mod pack");

            }

            buildMap[0].assetBundleName = "mod";
            buildMap[0].assetNames = dependencies.ToArray() ;





            BuildPipeline.BuildAssetBundles(path,buildMap, BuildAssetBundleOptions.StrictMode, BuildTarget.StandaloneWindows64);
            Debug.Log("Mod Making Completed to " + path);
            EditorUtility.SetDirty(m_modSave);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
        }
      
    }
}