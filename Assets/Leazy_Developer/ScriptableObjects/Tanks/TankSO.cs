using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TankSO", menuName = "Scriptable Objects/TankSO")]
public class TankSO : ScriptableObject
{
    public GameObject tankPrefab;
    public BulletSO bulletSO;
}
