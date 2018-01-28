using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    private ParticleSystem.EmissionModule _emission;

    private bool isDashing = false;
    private bool isStun = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _emission = DashParticles.emission;
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
                target = Vector3.Lerp(transform.position, hit.point, 0.75f);
            }
            
            _collider.enabled = false;
            var col = DashParticles.colorOverLifetime.color;
            col.colorMin = Color.red;
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

    IEnumerator StunCoroutine (float duration){
        StunBall.SetActive(true);
        yield return new WaitForSeconds(duration);
        StunBall.SetActive(false);
        isStun = false;
    }

    public bool IsStun(){
        return isStun;
    }

}
