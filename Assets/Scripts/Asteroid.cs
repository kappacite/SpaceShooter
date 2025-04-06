using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Asteroid : MonoBehaviour
{
    private bool isDestroyed;
    private void OnTriggerEnter2D(Collider2D collision) {

        if (isDestroyed) return;

        if (!IsVisible(gameObject.transform.position, GameObject.FindWithTag("MainCamera").GetComponent<Camera>())) return;

        isDestroyed = true;

        GetComponent<Animator>().SetTrigger("explode");
        
        if (collision.gameObject.tag == "Shield"){
            Debug.Log("shield");
            AsteroidSpawner.getInstance().currentAsteroids--;
            PlayerController.getInstance().RemoveShield(35);
            return;
        }else if(collision.gameObject.tag == "Player") {
            PlayerController.getInstance().RemoveHealth(25);
            AsteroidSpawner.getInstance().currentAsteroids--;
           
        }
        
        if(collision.gameObject.tag == "Projectile") {
            Destroy(collision.gameObject);
            AsteroidSpawner.getInstance().currentAsteroids--;
            if(Random.Range(0, 100) < 35) {
                GetComponent<PickupBonusDrop>().Drop(this.transform);
            }
           
            PlayerController.getInstance().AddScore(100);
        }

        Destroy(this.gameObject, 0.35f);

    }
    bool IsVisible(Vector3 position, Camera cam) {
        Vector3 viewportPos = cam.WorldToViewportPoint(position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 &&
               viewportPos.y >= 0 && viewportPos.y <= 1 &&
               viewportPos.z > 0;
    }
}
