using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBonusEvent : MonoBehaviour
{

    public bool isShield;
    public bool isWeapon;
    public bool isLife;
    public ModuleData shieldData;
    public WeaponData weaponData;
    public float value;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Player") {

            PlayerController controller = PlayerController.getInstance();

            if (isLife) {
                controller.AddHealth(25);
                Destroy(this.gameObject);
                return;
            }

            if (isWeapon) {
                controller.LoadWeapon(weaponData);
                Destroy(this.gameObject);
                return;
            }

            
            controller.LoadShield(shieldData);
            controller.AddShield(this.value);
            Destroy(this.gameObject);
        }

    }
}
