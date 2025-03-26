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

        isDestroyed = true;

        GetComponent<Animator>().SetTrigger("explode");
        
        if (collision.gameObject.tag == "Shield"){
            Debug.Log("shield");
            Destroy(this.gameObject, 0.35f);
            PlayerController.getInstance().RemoveShield(35);
            return;
        }else if(collision.gameObject.tag == "Player") {
            PlayerController.getInstance().RemoveHealth(26);
        }
        
        if(collision.gameObject.tag == "Projectile") {
            Destroy(collision.gameObject);
            GetComponent<PickupBonusDrop>().Drop(this.transform);
        } 

        Destroy(this.gameObject, 0.35f);
        PlayerController.getInstance().AddScore(100);
        GameObject.Find("Score").GetComponent<TMP_Text>().text = "Score: " + PlayerController.getInstance().getScore();
    }
}
