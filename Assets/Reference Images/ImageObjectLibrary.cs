using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

[CreateAssetMenu(fileName = "SO_ImageObjectLibrary", menuName ="Scriptable Objects/ImageObjectLibrary")] public class ImageObjectLibrary : ScriptableObject
{
    [Serializable] public class Img_Obj_Data
    {
        public Texture Tex;
        public GameObject[] Objects = new GameObject[2];
    }

    [SerializeField] List<Img_Obj_Data> _img_Obj_Data;


    public GameObject GetGameObject(string TexName, int objIndex)
    {
        for(int i = 0; i < _img_Obj_Data.Count; i++)
        {
            if(TexName == _img_Obj_Data[i].Tex.name)
            {
                return _img_Obj_Data[i].Objects[objIndex];
            }
        }

        Debug.LogError("FAILED to find obj for tex: " + TexName);
        return null;
    }

    public GameObject GetGameObject (GameObject searchFromMatchingObj, int objIndex)
    {
        for (int i = 0; i < _img_Obj_Data.Count; i++)
        {
            for (int j = 0; j < _img_Obj_Data[i].Objects.Length; j++)
            {
                Mesh m0 = searchFromMatchingObj.GetComponentInChildren<MeshFilter>().sharedMesh;
                Mesh m1 = _img_Obj_Data[i].Objects[j].GetComponentInChildren<MeshFilter>().sharedMesh;

                if (m0 == m1)
                {
                    return _img_Obj_Data[i].Objects[objIndex];
                }
            }
        }

        Debug.LogError("FAILED to find obj for tex: " + searchFromMatchingObj);
        return null;
    }
}