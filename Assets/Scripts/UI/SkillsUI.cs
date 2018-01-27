using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour {

    public GameObject[] inputs;

    public GameObject[] currentSkill;
    public GameObject[] currentSkillBg;
    public GameObject[] nextSkill;

    public Image[] currentSkillImgs;
    public Image[] nextSkillImgs;

    public Tween[] cooldownTweens;
    public bool[] noSkills;

    private void Awake()
    {
        cooldownTweens = new Tween[4];
        noSkills = new bool[4];

        ResetSkillUi();
    }

    public void ResetSkillUi()
    {
        foreach(Image img in currentSkillImgs)
        {
            img.fillAmount = 1f;
        }
        for (int i = 0; i < noSkills.Length; i++)
        {
            noSkills[i] = false;
        }
    }

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
        currentSkillBg = new GameObject[4];
        currentSkillImgs = new Image[4];
        nextSkill = new GameObject[4];
        nextSkillImgs = new Image[4];

        //otherSkills = new GameObject[4, 3];
        for (int i = 0; i < inputs.Length; i++)
        {
            currentSkill[i] = inputs[i].transform.GetChild(2).gameObject;
            currentSkillBg[i] = inputs[i].transform.GetChild(1).gameObject;
            currentSkillImgs[i] = currentSkill[i].GetComponent<Image>();
            nextSkill[i] = inputs[i].transform.GetChild(0).gameObject;
            nextSkillImgs[i] = nextSkill[i].GetComponent<Image>();
        }
    }

    public void PressMainSkill(int inputId)
    {
        currentSkillBg[inputId].transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(() => {
            currentSkillBg[inputId].transform.localScale = Vector3.one;
        });

        currentSkill[inputId].transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(()=>{
            currentSkill[inputId].transform.localScale = Vector3.one;
        });
    }

    public void TriggerCooldown(int inputId, float cooldown)
    {
        if(cooldownTweens[inputId] == null)
        {
            cooldownTweens[inputId].Kill();
        }

        currentSkillImgs[inputId].fillAmount = 0f;

        cooldownTweens[inputId] = currentSkillImgs[inputId].DOFillAmount(1f, cooldown).SetEase(Ease.Linear).OnComplete(() => {
            currentSkillImgs[inputId].fillAmount = noSkills[inputId] ? 0f : 1f;
        });
    }

    public void UpdateCurrentAndNext(int skillId, bool currentEnable, bool nextEnable)
    {
        noSkills[skillId] = !currentEnable;

        currentSkillImgs[skillId].material.SetFloat("_Offset", currentEnable ? 0f : 1f);
        nextSkillImgs[skillId].material.SetFloat("_Offset", nextEnable ? 0f : 1f);
    }

    public void UpdateCurrentAndNext(int inputId, Sprite current, Sprite next)
    {
        currentSkillImgs[inputId].sprite = current;
        nextSkillImgs[inputId].sprite = next;
    }
}
