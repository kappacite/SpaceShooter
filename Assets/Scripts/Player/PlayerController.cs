using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float _health;
    private float _maxHealth = 100;
    private float _shield = 0;
    private float _maxShield = 5;
    private float _gold;

    private SpriteRenderer _renderer;

    public GameObject engine;
    public GameObject weapon;
    public GameObject shield;

    public ModuleData engineData;
    public WeaponData weaponData;
    public ModuleData shieldData;
    // Start is called before the first frame update

    public List<Sprite> sprites;
    void Start()
    {

        this._health = this._maxHealth;

        this._renderer = GetComponent<SpriteRenderer>();

        engine = GameObject.FindGameObjectWithTag("Engine");
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        shield = GameObject.FindGameObjectWithTag("Shield");

        engine.GetComponentInChildren<Animator>().runtimeAnimatorController = engineData.animatorController;
        engine.GetComponent<SpriteRenderer>().sprite = engineData.sprite;
        engine.transform.position += new Vector3(engineData.offsetX, 0,0);
        engine.transform.position += new Vector3(0, engineData.offsetY,0);

        weapon.GetComponent<Animator>().runtimeAnimatorController = weaponData.moduleData.animatorController;
        weapon.GetComponent<SpriteRenderer>().sprite = weaponData.moduleData.sprite;
        weapon.transform.position += new Vector3(weaponData.moduleData.offsetX,0,0);
        weapon.transform.position += new Vector3(0,weaponData.moduleData.offsetY,0);

        shield.transform.position += new Vector3(shieldData.offsetX,0);
        shield.transform.position += new Vector3(0,shieldData.offsetY,0);

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayer();
    }

    public void AddHealth(float health) {
        _health += health;
    }

    public void RemoveHealth(float health) { _health -= health; }

    private void UpdatePlayer() {

        if (this._health > 75) {
            this._renderer.sprite = sprites[0];
        }
        else if (this._health > 50) {
            this._renderer.sprite = sprites[1];
        }
        else if (this._health > 25) {
            this._renderer.sprite = sprites[2];
        }
        else if (this._health > 0) {
            this._renderer.sprite = sprites[3];
        }

        if(_shield > 0) {
            shield.GetComponent<Animator>().runtimeAnimatorController = shieldData.animatorController;
            shield.GetComponent<SpriteRenderer>().sprite = shieldData.sprite;
        }


    }

}
