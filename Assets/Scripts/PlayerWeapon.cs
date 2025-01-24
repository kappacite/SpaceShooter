using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private float lastTriggeredTime;
    private float shootLength = 1.2f;
    private Animator _animator;

    void Start()
    {
        _animator = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Animator>();
    }



    void Update()
    {

        if (Input.GetMouseButtonDown(0) && (Time.time - lastTriggeredTime >= shootLength))
        {
            Debug.Log("Appuyer");
            lastTriggeredTime = Time.time;
            _animator.SetTrigger("IsShooting");
            _animator.ResetTrigger("IsShooting");
        }

    }
}
