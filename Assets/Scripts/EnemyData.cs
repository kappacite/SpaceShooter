using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public RuntimeAnimatorController controller;
    public float life;
    public Sprite sprite;
}
