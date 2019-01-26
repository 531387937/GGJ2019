using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "/Lancer_Data")]
public class Lancer_ScriptableObject : ScriptableObject
{
    public float ViewRange;
    public float CompareRange = 0.1f;
    public float AttackRange;
    public float Speed;
}
