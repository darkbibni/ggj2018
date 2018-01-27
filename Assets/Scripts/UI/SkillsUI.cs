using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillsUI : MonoBehaviour {

    public GameObject[] inputs;

    public GameObject[] mainSkills;
    private GameObject[,] otherSkills;

    private void Reset()
    {
        int actualChild = 0;
        inputs = new GameObject[transform.childCount];
        foreach (Transform t in transform)
        {
            inputs[actualChild] = t.gameObject;
            actualChild++;
        }

        mainSkills = new GameObject[4];
        
        //otherSkills = new GameObject[4, 3];
        for (int i = 0; i < inputs.Length; i++)
        {
            /*
            for (int j = 0; j < 3; i++)
            {
                otherSkills[i, j] = inputs[i].transform.GetChild(j).gameObject;
            }
            */
            mainSkills[i] = inputs[i].transform.GetChild(3).gameObject;
        }
    }

    public void PressMainSkill(int inputId)
    {
        mainSkills[inputId].transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(()=>{
            mainSkills[inputId].transform.localScale = Vector3.one;
        });
    }

    public void UpdateOtherSkill(int inputId, int otherSkillId)
    {
        // Color in black and white --> use shader
        //otherSkills[inputId, otherSkillId]
    }
}
