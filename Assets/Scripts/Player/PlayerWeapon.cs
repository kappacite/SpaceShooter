using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private PlayerController playerController;

    private float lastTriggeredTime;
    private float shootLength = 1.2f;
    private Animator _animator;

    void Start()
    {
        _animator = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && (Time.time - lastTriggeredTime) >= playerController.weaponData.waitingTime)
        {
            Debug.Log("Appuyer");
            lastTriggeredTime = Time.time;
            _animator.SetTrigger("IsShooting");
        }
        
    }
}
