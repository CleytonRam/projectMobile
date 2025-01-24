using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class ArtManager : Singleton<ArtManager>
{
   public enum ArtType 
    {
        TYPE_01,
        TYPE_02,
        TYPE_03,
        TYPE_04
    }
    public List<ArtSetup> artSetups = new List<ArtSetup>();

    public ArtSetup GetSetUpByType(ArtType artType) 
    {
        return artSetups.Find(i => i.artType == artType);
    }
}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject gameObject;
}
