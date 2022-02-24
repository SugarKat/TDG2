using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New buildings library",menuName = "GameData/Libraries/Buildings")]
public class BuildingLibrary : ScriptableObject
{
    public BuildingBlueprint[] list;
}
