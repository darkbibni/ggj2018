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
                UseSkillList(ref SkillA);
            }

            if (_player.GetButtonDown("B"))
            {
                UseSkillList(ref SkillB);
            }

            if (_player.GetButtonDown("X"))
            {
                UseSkillList(ref SkillX);
            }

            if (_player.GetButtonDown("Y"))
            {
                UseSkillList(ref SkillY);
            }

            if (_player.GetButtonDown("Dash"))
            {
                SkillMove.Dash();
            }
        }
    }

    public void UseSkillList(ref List<Skill> list){

        if(list.Count > 0){
            Skill s = list[0];

            s.Execute(new List<Skill>(list));
            list.RemoveAt(0);
            list.Add(s);
        }
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
                break;
            case SkillButton.B:
                foreach (Skill s in skills)
                {
                    SkillB.Remove(s);
                    pc.AddSkill(s);
                    s.HasBeenTransmitted();
                }
                break;
            case SkillButton.X:
                foreach (Skill s in skills)
                {
                    pc.AddSkill(s);
                    SkillX.Remove(s);
                    s.HasBeenTransmitted();
                }
                break;
            case SkillButton.Y:
                foreach (Skill s in skills)
                {
                    SkillY.Remove(s);
                    pc.AddSkill(s);
                    s.HasBeenTransmitted();
                }
                break;
        }

        GameObject transmition = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(transmition.GetComponent<Collider>());

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
