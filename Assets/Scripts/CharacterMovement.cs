using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour {

    public float MoveSpeed = 1f;
    public float DashLength = 3f;
    public float DashTime = 0.2f;
    public GameObject StunBall;
    private Rigidbody _rb;
    private bool isDashing = false;

    private bool isStun = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(float x, float y){
        if(!isStun){
            _rb.MovePosition(_rb.position + (x * Vector3.right * MoveSpeed/1000f) + (y * Vector3.forward * MoveSpeed/1000f));
            _rb.MoveRotation(Quaternion.LookRotation(new Vector3(x, 0f, y)));
        }
    }

    public void Dash(){
        if(!isDashing){
            isDashing = true;

            Vector3 target = _rb.position + transform.forward * DashLength;
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(transform.position, transform.forward, out hit, DashLength)){
                target = Vector3.Lerp(transform.position, hit.point, 0.75f);
            }
            _rb.DOMove(target, DashTime).OnComplete(()=>{ isDashing = false; });
        }
    }

    public void Stun(float duration){
        isStun = true;
        StartCoroutine(StunCoroutine(duration));
    }

    IEnumerator StunCoroutine (float duration){
        StunBall.SetActive(true);
        yield return new WaitForSeconds(duration);
        StunBall.SetActive(false);
        isStun = false;
    }

}
