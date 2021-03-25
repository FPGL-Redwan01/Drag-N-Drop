using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTypeSet : MonoBehaviour
{
    private Dictionary<InstructionTypeSO, Transform> _instructionTransformDictionary;
    private void Awake()
    {
        _instructionTransformDictionary = new Dictionary<InstructionTypeSO, Transform>();
        Transform instructionTemplate = transform.Find("instructionTemplate");
        instructionTemplate.gameObject.SetActive(false);

        InstructionTypeListSO instructionTypeList =
            Resources.Load<InstructionTypeListSO>(nameof(InstructionTypeListSO));

        int index = 0;
        foreach (InstructionTypeSO instructionType in instructionTypeList.list)
        {
            Transform instructionTransform = Instantiate(instructionTemplate, transform);
            instructionTransform.gameObject.SetActive(true);
            instructionTransform.GetComponent<InstructionTypeHolder>().instructionType = instructionType;

            float offsetAmount = 2f;
            instructionTransform.GetComponent<Transform>().position = new Vector3(transform.position.x + offsetAmount * index, transform.position.y, 0);
            instructionTransform.Find("sprite").GetComponent<SpriteRenderer>().sprite = instructionType.sprite;

            _instructionTransformDictionary[instructionType] = instructionTransform;
            
            index++;
        }
    }

    private void Update()
    {
        UpdateActiveInstructionTypeButton();
    }

    private void UpdateActiveInstructionTypeButton()
    {
        // foreach (InstructionTypeSO instructionType in _instructionTransformDictionary.Keys)
        // {
        //     Transform btnTransform = _instructionTransformDictionary[instructionType];
        //     btnTransform.Find("selected").gameObject.SetActive(false);
        // }
        //
        // InstructionTypeSO activeInstructionType = InstructionManager.Instance.GetActiveInstructionType();
        // _btnTransformDictionary[activeInstructionType].Find("selected").gameObject.SetActive(true);
    }
}
