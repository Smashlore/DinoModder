using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoModSave : ScriptableObject
{
    public int m_expotCount = 1;
    public List<GameObject> m_exportList = new List<GameObject>(1);
    public List<string> m_tagList = new List<string>(1);
    public long m_guid = 0;
    public string m_name = "Default";
    public string m_des = "Default";
    public string m_path;
    public ulong m_item;
    public bool m_user = false;
    public Texture2D m_previewImage;
}
