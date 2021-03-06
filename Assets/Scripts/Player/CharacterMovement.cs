﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CharacterMovement : MonoBehaviour {

    public float MoveSpeed = 1f;
    public float DashLength = 3f;
    public float DashTime = 0.2f;
    
    public float maxSpeed = 4f;

    public float SpeedMultiplicator
    {
        get { return speedMultiplicator; }
        set { speedMultiplicator = Mathf.Min(value, maxSpeed); }
    }
    private float speedMultiplicator = 1f;
    public bool canMove = true;
    
    public GameObject StunBall;
    public ParticleSystem DashParticles;
    private Rigidbody _rb;
    private Collider _collider;
    private AudioSource _src;
    private ParticleSystem.EmissionModule _emission;

    private bool isDashing = false;
    private bool isStun = false;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _emission = DashParticles.emission;
        _src = GetComponent<AudioSource>();
    }

    public void Move(float x, float y){

        if (!isStun && canMove) {
            _rb.MovePosition(_rb.position + ((x * Vector3.right) + (y * Vector3.forward)) * MoveSpeed * speedMultiplicator * Time.deltaTime/20f);
            _rb.MoveRotation(Quaternion.LookRotation(new Vector3(x, 0f, y)));
        }

        //make missile locomotion
    }

    public void Dash(){
        if(!isDashing){
            isDashing = true;

            Vector3 target = _rb.position + transform.forward * DashLength;
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(transform.position, transform.forward, out hit, DashLength)){

                if(!hit.collider.CompareTag("Exit"))
                {
                    target = Vector3.Lerp(transform.position, hit.point, 0.75f);
                }
            }
            
            _collider.enabled = false;
            var col = DashParticles.colorOverLifetime.color;
            col.colorMin = Color.red;
            AudioManager.singleton.PlayAt(AudioManager.singleton.GetSFXclip("Dash"), _src);
            _rb.DOMove(target, DashTime).OnComplete(()=>{
                isDashing = false;
                _collider.enabled = true;
                _rb.velocity = Vector3.zero;
            });
        }
    }

    public void Stun(float duration){
        isStun = true;
        StartCoroutine(StunCoroutine(duration));
    }

    public void KnockBack(Vector3 dir, float strength)
    {
        _rb.AddForce(dir * strength, ForceMode.Impulse);

        Invoke("StopKnockBack", 0.2f);
    }

    private void StopKnockBack()
    {
        _rb.velocity = Vector3.zero;
    }

    IEnumerator StunCoroutine (float duration){
        AudioManager.singleton.PlayAt(AudioManager.singleton.GetSFXclip("Stun"), _src);
        StunBall.SetActive(true);
        yield return new WaitForSeconds(duration);
        StunBall.SetActive(false);
        _src.Stop();
        isStun = false;
    }

    public bool IsStun(){
        return isStun;
    }
}
