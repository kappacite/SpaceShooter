using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="WeaponData", menuName="ScriptableObject/Weapon/Data")]
public class WeaponData : ScriptableObject
{
    public GameObject munitionPrefabs;
    public ModuleData moduleData;
    public int baseDamage;
    public float ammoSpeed;
    public List<string> cannons;
    public float timeBetweenShoot;
}
