using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private PlayerController playerController;

    private float lastTriggeredTime = 0;
    private float shootLength = 1.2f;
    private Animator _animator;
    
    private Vector3 _mousePosition;

    void Start()
    {
        
        _animator = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        lastTriggeredTime = -playerController.weaponData.moduleData.waitingTime;
    }

    void Update()
    {

        _mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && (Time.time - lastTriggeredTime) >= playerController.weaponData.moduleData.waitingTime)
        {
            Debug.Log("Appuyer");
            lastTriggeredTime = Time.time;
            _animator.SetTrigger("IsShooting");
            StartCoroutine(ShootAll(playerController.weaponData.timeBetweenShoot));

        }
        
    }

    private void Shoot(GameObject cannon) {
        GameObject munition = Instantiate(playerController.weaponData.munitionPrefabs);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        munition.transform.position = cannon.transform.position;
        munition.transform.up = direction;
        munition.GetComponent<Rigidbody2D>().velocity = direction.normalized * playerController.weaponData.ammoSpeed;
        
        AudioSource source = munition.GetComponent<AudioSource>();
        source.clip = playerController.weaponData.sound;
        source.Play();
        
        StartCoroutine(DestroyAmmo(munition));
    }

    private IEnumerator DestroyAmmo(GameObject ammo) {
        yield return new WaitForSeconds(3);
        Destroy(ammo);
    }

    private IEnumerator ShootAll(float delay) {
        foreach (string cannon in playerController.weaponData.cannons) {
            GameObject cannonObject = GameObject.Find(cannon);
            Shoot(cannonObject);
            yield return new WaitForSeconds(delay);
        }
       
    }
}
