using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float _health;
    private float _maxHealth = 100;
    private float _shield = 0;
    private float _maxShield;
    private float _gold;

    private SpriteRenderer _renderer;

    public GameObject engine;
    public GameObject weapon;

    public ModuleData engineData;
    public ModuleData weaponData;
    // Start is called before the first frame update

    public List<Sprite> sprites;
    void Start()
    {

        this._health = this._maxHealth;

        this._renderer = GetComponent<SpriteRenderer>();

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


    }

}
