using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInputManager : MonoBehaviour {

    public int playerId = 0;
    private Player _player;
    private Rigidbody _rb;

    private CharacterMovement SkillMove;

    private List<Skill> SkillA = new List<Skill>();
    private List<Skill> SkillB = new List<Skill>();
    private List<Skill> SkillX = new List<Skill>();
    private List<Skill> SkillY = new List<Skill>();

    void Awake()
    {
        _player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if(_player.GetAxis("Horizontal") != 0.0f) {
            SkillMove.MoveX(_player.GetAxis("Horizontal"));
        }

        if(_player.GetAxis("Vertical") != 0.0f) {
            SkillMove.MoveY(_player.GetAxis("Vertical"));
        }

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
