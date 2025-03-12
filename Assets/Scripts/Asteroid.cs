using System.Collections;
using System.Collections.Generic;
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

        if(collision.gameObject.tag == "Projectile") {
            Destroy(collision.gameObject);
            GetComponent<PickupBonusDrop>().Drop(this.transform);
        }

        if(collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().RemoveHealth(26);
        }

        Destroy(this.gameObject, 0.35f);
    }
}
