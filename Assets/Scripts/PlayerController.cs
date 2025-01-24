using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float _health;
    private float _maxHealth;
    private float _shield;
    private float _maxShield;
    private float _gold;

    public GameObject engine;
    public GameObject weapon;

    public ModuleData engineData;
    public ModuleData weaponData;
    // Start is called before the first frame update
    void Start()
    {

        engine = GameObject.FindGameObjectWithTag("Engine");
        weapon = GameObject.FindGameObjectWithTag("Weapon");

        engine.GetComponentInChildren<Animator>().runtimeAnimatorController = engineData.animatorController;
        engine.GetComponent<SpriteRenderer>().sprite = engineData.sprite;

        weapon.GetComponent<Animator>().runtimeAnimatorController = weaponData.animatorController;
        weapon.GetComponent<SpriteRenderer>().sprite = weaponData.sprite;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
