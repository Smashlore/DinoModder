using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
[CustomEditor(typeof(DinoAsset))]
public class DinoAssetInspetor : Editor
{
    public override void OnInspectorGUI()
    {
        DinoAsset myTarget = (DinoAsset)target;
        List<string> excludedProperties = new List<string>();

        //if (!myScript.someBool)
        if(myTarget.m_type != DinoAsset.DinoAssetType.Dinosaur)
        {
            excludedProperties.Add("m_dinosaurType");
            excludedProperties.Add("m_dinoEyeColor");
            excludedProperties.Add("m_drawEyes");
        }

        if (
            myTarget.m_type != DinoAsset.DinoAssetType.Building &&
            myTarget.m_type != DinoAsset.DinoAssetType.Facilities 
            )
        {

            excludedProperties.Add("m_itemPrice");
            excludedProperties.Add("m_fun");
            excludedProperties.Add("m_poop");
            excludedProperties.Add("m_food");
            excludedProperties.Add("m_energy");
            excludedProperties.Add("m_scenery");
            
        }

        if (
           myTarget.m_type != DinoAsset.DinoAssetType.Building &&
           myTarget.m_type != DinoAsset.DinoAssetType.Facilities &&
           myTarget.m_type != DinoAsset.DinoAssetType.Decorations &&
           myTarget.m_type != DinoAsset.DinoAssetType.Lights
           )
        {
            excludedProperties.Add("m_interest");
            excludedProperties.Add("m_useCustomColors");
            excludedProperties.Add("m_price");
            excludedProperties.Add("m_gridWidth");
            excludedProperties.Add("m_gridLength");
            excludedProperties.Add("m_PathOnNorthSide");
            excludedProperties.Add("m_PathOnEastSide");
            excludedProperties.Add("m_PathOnSouthSide");
            excludedProperties.Add("m_PathOnWestSide");
            excludedProperties.Add("m_PathOnAllSides");

            excludedProperties.Add("m_previewImage");

        }

        if (
           myTarget.m_type != DinoAsset.DinoAssetType.Decorations
           )
        {
            excludedProperties.Add("m_enrichmentAmount");
            excludedProperties.Add("m_enrichmentRange");
        }

            DrawPropertiesExcluding(serializedObject, excludedProperties.ToArray());
        serializedObject.ApplyModifiedProperties();
    }

}