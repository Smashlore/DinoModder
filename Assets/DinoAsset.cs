using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum Deco_Teir
{
    Deco_Tier_1,
    Deco_Tier_2
}

//NEVER CHANGE THESE
public enum Species
{
    Albertosaurus,
    Ankylosaurus,
    Apatosaurus,
    Brachiosaurus,
    Gastonia,
    Gigantspinosaurus,
    Kentrosaurus,
    Mamenchisaurus,
    Oviraptor,
    Parasaurolophus,
    Shantungosaurus,
    Stegosaurus,
    Styracosaurus,
    Triceratops,
    TyrannosaurusRex,
    Velociraptor,
    Lambeosaurus,
    Seismosaurus,
    Achelousaurus,
    Ornithomimus,
    Spinosaurus,
    Minmi,
    Animantarx,
    Kosmoceratops,
    Generic,
    Wuerhosaurus,
    Carnotaurus,
    Hadrosaurus,
    Ampelosaurus,
    Protoceratops,
    Huayangosaurus,
    Sauropelta
}

public class DinoAsset : MonoBehaviour
{
    public enum DinoAssetType
    {
        Human,
        Dinosaur,
        Building,
        Facilities,
        Decorations,
        Lights
    }
    public string m_name = "Default Name";
    public DinoAssetType m_type = DinoAssetType.Building;
    [Header("Building")]

    public bool m_useCustomColors = false;
    public int m_price = 100;
    public Sprite m_previewImage;
    public int m_gridWidth = 1;
    public int m_gridLength = 1;

    public bool m_PathOnNorthSide = false;
    public bool m_PathOnEastSide = false;
    public bool m_PathOnSouthSide = false;
    public bool m_PathOnWestSide = false;
    public bool m_PathOnAllSides = false;

    [Header("Selling")]
    public int m_itemPrice = 5;
    [Range(0, 1f)]
    public float m_interest = 0.0f;
    [Range(0, 1f)]
    public float m_fun = 0.0f;
    [Range(0, 1f)]
    public float m_poop = 0.0f;
    [Range(0, 1f)]
    public float m_food = 0.0f;
    [Range(0, 1f)]
    public float m_energy = 0.0f;
    [Range(0, 1f)]
    public float m_scenery = 0.0f;
    [Header("Dinosaur")]
    public Species m_dinosaurType;
    public Color m_dinoEyeColor = Color.green;
    public bool m_drawEyes = true;

    [Header("Decoration")]
    public Deco_Teir m_enrichmentAmount = Deco_Teir.Deco_Tier_1;
    [Range(1, 6)]
    public int m_enrichmentRange = 3;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
