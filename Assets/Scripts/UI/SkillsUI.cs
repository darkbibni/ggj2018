using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillsUI : MonoBehaviour {

    public GameObject[] inputs;

    public GameObject[] currentSkill;
    public GameObject[] nextSkill;

    private void Reset()
    {
        Transform inputsParent = transform.GetChild(2);

        int actualChild = 0;
        inputs = new GameObject[inputsParent.childCount];
        foreach (Transform t in inputsParent)
        {
            inputs[actualChild] = t.gameObject;
            actualChild++;
        }

        currentSkill = new GameObject[4];
        nextSkill = new GameObject[4];

        //otherSkills = new GameObject[4, 3];
        for (int i = 0; i < inputs.Length; i++)
        {
            currentSkill[i] = inputs[i].transform.GetChild(1).gameObject;
            nextSkill[i] = inputs[i].transform.GetChild(0).gameObject;
        }
    }

    public void PressMainSkill(int inputId)
    {
        currentSkill[inputId].transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(()=>{
            currentSkill[inputId].transform.localScale = Vector3.one;
        });
    }

    public void UpdateCurrentAndNext(Sprite current, Sprite next)
    {
        // Color in black and white --> use shader
        //otherSkills[inputId, otherSkillId]
    }
}
