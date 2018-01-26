using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float VerticalSpeed = 1f;
    public float HorizontalSpeed = 1f;
    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void MoveX(float i){
        _rb.MovePosition(_rb.position + i * transform.right * HorizontalSpeed/1000f);
    }

    public void MoveY(float i){
        _rb.MovePosition(_rb.position + i * transform.forward * VerticalSpeed/1000f);
    }

    public void Dash(){

    }

}
