using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public static InstructionManager Instance { get; private set; }
    private InstructionTypeSO _activeInstructionType;
    private InstructionTypeListSO _instructionTypeList;
    public event EventHandler OnInstructionGiven;

    private void Awake()
    {
        Instance = this;
        _instructionTypeList = Resources.Load<InstructionTypeListSO>(nameof(InstructionTypeListSO));
        _activeInstructionType = _instructionTypeList.list[0];
    }

    public void GiveInstruction()
    {
        OnInstructionGiven?.Invoke(this, EventArgs.Empty);
    }
    public void SetActiveInstructionType(InstructionTypeSO instructionType)
    {
        _activeInstructionType = instructionType;
    }

    public InstructionTypeSO GetActiveInstructionType()
    {
        return _activeInstructionType;
    }
}
