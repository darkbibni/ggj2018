using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInputManager : MonoBehaviour {

    public int playerId = 1;
    private Player _player;

    private CharacterMovement SkillMove;

    private List<Skill> SkillA = new List<Skill>();
    private List<Skill> SkillB = new List<Skill>();
    private List<Skill> SkillX = new List<Skill>();
    private List<Skill> SkillY = new List<Skill>();

    private float moveX = 0.0f;
    private float moveY = 0.0f;

    void Awake()
    {
        SkillMove = GetComponent<CharacterMovement>();
        _player = ReInput.players.GetPlayer(playerId);
    }

    void FixedUpdate()
    {
        if(moveX != 0.0f || moveY != 0.0f){
            SkillMove.Move(moveX, moveY);
        }
    }
    void Update()
    {
        moveX = _player.GetAxis("Horizontal");

        moveY = _player.GetAxis("Vertical");

        if(_player.GetButtonDown("A")){
            foreach(Skill s in SkillA){
                s.Execute();
            }
        }

        if(_player.GetButtonDown("B")){
            foreach(Skill s in SkillB){
                s.Execute();
            }
        }

        if(_player.GetButtonDown("X")){
            foreach(Skill s in SkillX){
                s.Execute();
            }
        }

        if(_player.GetButtonDown("Y")){
            foreach(Skill s in SkillY){
                s.Execute();
            }
        }

        if(_player.GetButtonDown("Dash")){
            SkillMove.Dash();
        }
    }
	
}
