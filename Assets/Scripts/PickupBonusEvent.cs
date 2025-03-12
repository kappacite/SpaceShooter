using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBonusEvent : MonoBehaviour
{

    public ModuleData shieldData;
    public float value;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Player") {
            PlayerController controller = collision.GetComponent<PlayerController>();
            controller.LoadShield(shieldData);
            controller.AddShield(this.value);
            Destroy(this.gameObject);
        }

    }
}
