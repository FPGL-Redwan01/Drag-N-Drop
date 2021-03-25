using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/InstructionTypeList")]
public class InstructionTypeListSO : ScriptableObject
{
    public List<InstructionTypeSO> list;
}