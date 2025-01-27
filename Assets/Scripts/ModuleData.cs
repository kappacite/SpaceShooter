using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModuleData", menuName = "ScriptableObject/Player/ModuleData")]
public class ModuleData : ScriptableObject
{
    public int price;
    public string name;
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public float waitingTime;
    public float offsetX;
    public float offsetY;
}
