using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionTypeSelectUI : MonoBehaviour
{
    private Dictionary<InstructionTypeSO, Transform> _btnTransformDictionary;
    private void Awake()
    {
        _btnTransformDictionary = new Dictionary<InstructionTypeSO, Transform>();
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        InstructionTypeListSO instructionTypeList =
            Resources.Load<InstructionTypeListSO>(nameof(InstructionTypeListSO));

        int index = 0;
        foreach (InstructionTypeSO instructionType in instructionTypeList.list)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            float offsetAmount = 150f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            btnTransform.Find("image").GetComponent<Image>().sprite = instructionType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                InstructionManager.Instance.SetActiveInstructionType(instructionType);
            });

            _btnTransformDictionary[instructionType] = btnTransform;
            
            index++;
        }
    }

    private void Update()
    {
        UpdateActiveInstructionTypeButton();
    }

    private void UpdateActiveInstructionTypeButton()
    {
        foreach (InstructionTypeSO instructionType in _btnTransformDictionary.Keys)
        {
            Transform btnTransform = _btnTransformDictionary[instructionType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        InstructionTypeSO activeInstructionType = InstructionManager.Instance.GetActiveInstructionType();
        _btnTransformDictionary[activeInstructionType].Find("selected").gameObject.SetActive(true);
    }
}
