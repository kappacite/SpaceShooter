using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float _health;
    private float _maxHealth = 100;
    public float _shield = 0;
    private float _maxShield = 100;
    private float _gold;
    private int score;

    private SpriteRenderer _renderer;

    public GameObject engine;
    public GameObject weapon;
    public GameObject shield;

    public ModuleData engineData;
    public WeaponData weaponData;
    public ModuleData shieldData;

    private bool isPlaying = true;

    private static PlayerController instance;

    public List<Sprite> sprites;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
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
        if (_health <= 0) {
            Debug.Log("test");
            //UnityEditor.EditorApplication.isPlaying = false;

            if (isPlaying) {
                isPlaying = false;
                StartCoroutine(GameOver());
                
            }

        }
    }

    public void LoadShield(ModuleData data) {
        this.shieldData = data;
        shield.GetComponentInChildren<Animator>().runtimeAnimatorController = data.animatorController;
        shield.GetComponent<SpriteRenderer>().sprite = data.sprite;
        shield.transform.localPosition = new Vector3(data.offsetX, data.offsetY, 0);
        GetComponent<SpriteRenderer>().sprite = shieldData.sprite;
        Destroy(shield.GetComponent<PolygonCollider2D>());
        shield.AddComponent<PolygonCollider2D>();
        this._maxShield = 100;
        this._shield = 100;
    }

    public void LoadWeapon(WeaponData data) {
        this.weaponData = data;
        weapon.GetComponent<Animator>().runtimeAnimatorController = data.moduleData.animatorController;
        weapon.GetComponent<SpriteRenderer>().sprite = data.moduleData.sprite;
        weapon.transform.position += new Vector3(data.moduleData.offsetX, 0, 0);
        weapon.transform.position += new Vector3(0, data.moduleData.offsetY, 0);
    }

    public void LoadEngine(ModuleData data) {
        this.engineData = data;
        engine.GetComponentInChildren<Animator>().runtimeAnimatorController = data.animatorController;
        engine.GetComponent<SpriteRenderer>().sprite = data.sprite;
        engine.transform.position += new Vector3(data.offsetX, 0, 0);
        engine.transform.position += new Vector3(0, data.offsetY, 0);
    }


    public void AddHealth(float health) {
        _health += health;
    }

    public void RemoveHealth(float health) {
        _health -= health;
        gameObject.GetComponent<PlayerMovement>().currentAcceleration /= 2;


    }

    public void AddShield(float shield) {
        _shield += shield;
    }

    public void RemoveShield(float shield) { _shield -= shield; }

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
        } else {
            shield.GetComponent<SpriteRenderer>().sprite = null;
            shield.GetComponent<Animator>().runtimeAnimatorController = null;
            shield.GetComponent<PolygonCollider2D>().enabled = false;
        }


    }

    public static PlayerController getInstance() {
        return instance;
    }

    public void AddScore(int score) {
        this.score += score;
        GameObject.Find("Score").GetComponent<TMP_Text>().text = "Score: " + this.score.ToString();
    }

    public int getScore() {
        return score;
    }

    IEnumerator GameOver() {
        SceneManager.LoadScene("GameOverScene");
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("ScoreText").GetComponent<TMP_Text>().text = "Votre score : " + PlayerController.getInstance().getScore() + " points";
        Destroy(gameObject);
        Destroy(GameObject.Find("GameController"));
    }

}
