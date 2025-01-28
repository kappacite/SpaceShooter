using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="WeaponData", menuName="ScriptableObject/Weapon/Data")]
public class WeaponData : ScriptableObject
{
    public GameObject munitionPrefabs;
    public List<int[]> canonsLocations;
    public int baseDamage;
}
