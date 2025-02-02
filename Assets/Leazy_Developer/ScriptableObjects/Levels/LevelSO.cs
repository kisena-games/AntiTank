using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    [Tooltip("Список всех волн на уровне")]
    public List<Wave> waves;
}


