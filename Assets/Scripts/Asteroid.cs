using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private bool isDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            PlayerController.getInstance().RemoveHealth(25);
        }
        
        if(collision.gameObject.tag == "Projectile") {
            Destroy(collision.gameObject);
            GetComponent<PickupBonusDrop>().Drop(this.transform);
        }

        PlayerController.getInstance().AddScore(100);

        Destroy(this.gameObject, 0.35f);
    }
}
