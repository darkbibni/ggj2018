using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public float durationOfTransmition = 0.5f;

    public int playerId = 0;
    private Player _player = null;

    [HideInInspector]
    public CharacterMovement SkillMove;

    public Transform modelTransform;

    public List<GameObject> TrailList = new List<GameObject>();
    
    public float cooldownA = 1;
    public float cooldownB = 1;
    public float cooldownX = 2;
    public float cooldownY = 5;
    public float globalCooldown = 0.3f;

    public float dashCooldown = 0.5f;
    
    private bool inCooldownA = false;
    private bool inCooldownB = false;
    private bool inCooldownX = false;
    private bool inCooldownY = false;
    private bool inCooldownGlobal = false;
    private bool inCooldownDash = false;

    private List<Skill> SkillA = new List<Skill>();
    private List<Skill> SkillB = new List<Skill>();
    private List<Skill> SkillX = new List<Skill>();
    private List<Skill> SkillY = new List<Skill>();

    private float moveX = 0.0f;
    private float moveY = 0.0f;

    private GameObject highlightVfx;

    public bool IsEmpty {
        get
        {
            return SkillA.Count == 0 && SkillB.Count == 0 && SkillX.Count == 0 && SkillY.Count == 0;
        }
    }

    private bool isHighligted;

    void Awake()
    {
        SkillMove = GetComponent<CharacterMovement>();

        if(GameManager.instance == null)
        {
            _player = ReInput.players.GetPlayer(playerId);
        }
    }

    public void ShowTrails(bool value){
        foreach(GameObject go in TrailList){
            go.SetActive(value);
        }
    }

    void FixedUpdate()
    {
        if(moveX != 0.0f || moveY != 0.0f){
            SkillMove.Move(moveX, moveY);
        }
    }

    public void SetupPlayer(int playerId)
    {
        this.playerId = playerId;
        _player = ReInput.players.GetPlayer(playerId);
        Instantiate(GameManager.instance.characterPrefabs[playerId], modelTransform);
        highlightVfx = Instantiate(GameManager.instance.highlightVfx[playerId], transform);
        highlightVfx.SetActive(false);
    }

    void Update()
    {
        if(_player == null)
        {
            return;
        }
        
        if(GameManager.instance != null)
        {
            if (GameManager.instance.GameState == GameStates.FIGHT)
            {
                HandleFightInput();
            }
        } 

        else
        {
            HandleFightInput();
        }
    }

    private void HandleFightInput()
    {
        moveX = _player.GetAxis("Horizontal");

        moveY = _player.GetAxis("Vertical");

        if (!SkillMove.IsStun() && !inCooldownGlobal)
        {
            bool isUse = false;

            if (_player.GetButtonDown("A") && !inCooldownA)
            {
                if (UseSkillList(ref SkillA))
                {
                    isUse = true;

                    inCooldownA = true;
                    Invoke("CooldownAFinished", cooldownA);

                    if (GameManager.instance != null)
                    {
                        GameManager.instance.uiMgr.UseSkill(playerId, 0);
                        GameManager.instance.uiMgr.FeedbackCooldown(playerId, 0, cooldownA);
                    }
                }
            }

            else if(_player.GetButtonDown("B") && !inCooldownB)
            {
                if (UseSkillList(ref SkillB))
                {
                    isUse = true;

                    inCooldownB = true;
                    Invoke("CooldownBFinished", cooldownB);

                    if (GameManager.instance != null)
                    {
                        GameManager.instance.uiMgr.UseSkill(playerId, 1);
                        GameManager.instance.uiMgr.FeedbackCooldown(playerId, 1, cooldownB);
                    }
                }
            }

            else if(_player.GetButtonDown("X") && !inCooldownX)
            {
                if (UseSkillList(ref SkillX))
                {
                    isUse = true;

                    inCooldownX = true;
                    Invoke("CooldownXFinished", cooldownX);

                    if (GameManager.instance != null)
                    {
                        GameManager.instance.uiMgr.UseSkill(playerId, 2);
                        GameManager.instance.uiMgr.FeedbackCooldown(playerId, 2, cooldownX);
                    }
                }
            }

            else if (_player.GetButtonDown("Y") && !inCooldownY)
            {
                if(UseSkillList(ref SkillY))
                {
                    isUse = true;

                    inCooldownY = true;
                    Invoke("CooldownYFinished", cooldownY);

                    if (GameManager.instance != null)
                    {
                        GameManager.instance.uiMgr.UseSkill(playerId, 3);
                        GameManager.instance.uiMgr.FeedbackCooldown(playerId, 3, cooldownY);
                    }
                }
            }
            
            if(isUse)
            {
                inCooldownGlobal = true;
                Invoke("CooldownGlobalFinished", globalCooldown);
            }
        }

        if (!SkillMove.IsStun() && _player.GetButtonDown("Dash") && !inCooldownDash)
        {
            SkillMove.Dash();
            inCooldownDash = true;
            Invoke("CooldownDashFinished", dashCooldown);
        }
    }

    private void CooldownAFinished()
    {
        inCooldownA = false;
    }

    private void CooldownBFinished()
    {
        inCooldownB = false;
    }

    private void CooldownXFinished()
    {
        inCooldownX = false;
    }

    private void CooldownYFinished()
    {
        inCooldownY = false;
    }

    private void CooldownGlobalFinished()
    {
        inCooldownGlobal = false;
    }

    private void CooldownDashFinished()
    {
        inCooldownDash = false;
    }

    /// <summary>
    /// Use the first skill available, return false if the list is empty.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public bool UseSkillList(ref List<Skill> list){

        if(list.Count > 0){
            Skill s = list[0];

            s.Execute(new List<Skill>(list));
            list.RemoveAt(0);
            list.Add(s);

            return true;
        }

        return false;
    }

    public void AddSkill(Skill s){
        Skill sloc = (Skill)gameObject.AddComponent(s.GetType());
        sloc.Init(this);
        switch(sloc.eButton){
            case SkillButton.A : 
                SkillA.Add(sloc);

                GameManager.instance.uiMgr.playerSkillsUI[playerId].UpdateCurrentAndNext(0, SkillA.Count > 0, SkillA.Count > 1);

                break;
            case SkillButton.B : 
                SkillB.Add(sloc);

                GameManager.instance.uiMgr.playerSkillsUI[playerId].UpdateCurrentAndNext(1, SkillB.Count > 0, SkillB.Count > 1);

                break;
            case SkillButton.X : 
                SkillX.Add(sloc);

                GameManager.instance.uiMgr.playerSkillsUI[playerId].UpdateCurrentAndNext(2, SkillX.Count > 0, SkillX.Count > 1);

                break;
            case SkillButton.Y :
                SkillY.Add(sloc);

                GameManager.instance.uiMgr.playerSkillsUI[playerId].UpdateCurrentAndNext(3, SkillY.Count > 0, SkillY.Count > 1);

                break;
        }
    }

    public void TransmitToEnemy(List<Skill> skills, SkillButton sb, PlayerController pc)
    {
        switch (sb)
        {
            case SkillButton.A:
                foreach(Skill s in skills)
                {
                    SkillA.Remove(s);
                    pc.AddSkill(s);
                    s.HasBeenTransmitted();
                }

                GameManager.instance.uiMgr.playerSkillsUI[playerId].UpdateCurrentAndNext(0, false, false);

                break;
            case SkillButton.B:
                foreach (Skill s in skills)
                {
                    SkillB.Remove(s);
                    pc.AddSkill(s);
                    s.HasBeenTransmitted();
                }

                GameManager.instance.uiMgr.playerSkillsUI[playerId].UpdateCurrentAndNext(1, false, false);

                break;
            case SkillButton.X:
                foreach (Skill s in skills)
                {
                    pc.AddSkill(s);
                    SkillX.Remove(s);
                    s.HasBeenTransmitted();
                }

                GameManager.instance.uiMgr.playerSkillsUI[playerId].UpdateCurrentAndNext(2, false, false);

                break;
            case SkillButton.Y:
                foreach (Skill s in skills)
                {
                    SkillY.Remove(s);
                    pc.AddSkill(s);
                    s.HasBeenTransmitted();
                }

                GameManager.instance.uiMgr.playerSkillsUI[playerId].UpdateCurrentAndNext(3, false, false);

                break;
        }

        GameObject transmition = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(transmition.GetComponent<Collider>());

        if(IsEmpty && !isHighligted)
        {
            GameManager.instance.arenaMgr.exitMgr.HighlightExit(playerId, true);
            GameManager.instance.uiMgr.playerSkillAnims[playerId].SetBool("HasNoAbilities", true);
            highlightVfx.SetActive(true);
            isHighligted = true;
        }

        else if(!IsEmpty && isHighligted)
        {
            GameManager.instance.arenaMgr.exitMgr.HighlightExit(playerId, false);
            GameManager.instance.uiMgr.playerSkillAnims[playerId].SetBool("HasNoAbilities", false);
            highlightVfx.SetActive(false);
            isHighligted = false;
        }

        StartCoroutine(TransmitCoroutine(transform.position, pc.transform, transmition, durationOfTransmition));
    }

    IEnumerator TransmitCoroutine(Vector3 from, Transform to, GameObject obj, float duration)
    {
        float timer = 0.0f;
        Transform trsf = obj.transform;
        if(duration != 0.0f)
        {
            float div = 1 / duration;

            while (timer < duration)
            {
                trsf.position = Vector3.Lerp(from, to.position, timer * div);
                timer += Time.deltaTime;
                yield return null;
            }
            
        }

        Destroy(obj);
    }
}
