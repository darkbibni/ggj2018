using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {
    
    public int playerId = 0;
    private Player _player = null;

    [HideInInspector]
    public CharacterMovement SkillMove;

    public UiManager uiMgr;

    private List<Skill> SkillA = new List<Skill>();
    private List<Skill> SkillB = new List<Skill>();
    private List<Skill> SkillX = new List<Skill>();
    private List<Skill> SkillY = new List<Skill>();

    private float moveX = 0.0f;
    private float moveY = 0.0f;

    public bool IsEmpty {
        get
        {
            return SkillA.Count == 0 && SkillB.Count == 0 && SkillX.Count == 0 && SkillY.Count == 0;
        }
    }

    void Awake()
    {
        SkillMove = GetComponent<CharacterMovement>();
        SetupPlayer(playerId);
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

        if (!SkillMove.IsStun())
        {
            if (_player.GetButtonDown("A"))
            {
                if (UseSkillList(ref SkillA))
                {
                    GameManager.instance.uiMgr.UseSkill(playerId, 0);
                }
            }

            if (_player.GetButtonDown("B"))
            {
                if (UseSkillList(ref SkillB))
                {
                    GameManager.instance.uiMgr.UseSkill(playerId, 1);
                }
            }

            if (_player.GetButtonDown("X"))
            {
                if (UseSkillList(ref SkillX))
                {
                    GameManager.instance.uiMgr.UseSkill(playerId, 2);
                }
            }

            if (_player.GetButtonDown("Y"))
            {
                if(UseSkillList(ref SkillY))
                {
                    GameManager.instance.uiMgr.UseSkill(playerId, 3);
                }
                
            }

            if (_player.GetButtonDown("Dash"))
            {
                SkillMove.Dash();
            }
        }
    }

    /// <summary>
    /// Use the first skill available, return false if the list is empty.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public bool UseSkillList(ref List<Skill> list){

        if(list.Count > 0){
            Skill s = list[0];
            s.Execute();
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
                break;
            case SkillButton.B : 
                SkillB.Add(sloc);
                break;
            case SkillButton.X : 
                SkillX.Add(sloc);
                break;
            case SkillButton.Y : 
                SkillY.Add(sloc);
                break;
        }
    }

    public void RemoveSkill(Skill s, SkillButton sb)
    {
        switch(sb){
            case SkillButton.A : 
                SkillA.Remove(s);
                break;
            case SkillButton.B : 
                SkillB.Remove(s);
                break;
            case SkillButton.X : 
                SkillX.Remove(s);
                break;
            case SkillButton.Y : 
                SkillY.Remove(s);
                break;
        }
    }
}
