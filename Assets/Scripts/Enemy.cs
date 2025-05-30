using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 100f;
    public float speed = 2f;
    private Transform player;
    private Animator animator;
    bool collided = false;
    public EnemyData enemyData;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        /*if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }*/
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collided) return;

        if (!GetComponent<Renderer>().isVisible) return;

        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            TakeDamage(PlayerController.getInstance().weaponData.baseDamage);
            if(life <= 0) {
                AddScore();
            }
            return;
        }
        
        if (collision.gameObject.tag == "Shield"){
            collided = true;
            StartCoroutine(reset());
            TakeDamage(life);
            PlayerController.getInstance().RemoveShield(35);
            return;
        } else if (collision.gameObject.tag == "Player") {
            PlayerController.getInstance().RemoveHealth(35);
            TakeDamage(life);
        }
    }
    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.1f);
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        yield return new WaitForSeconds(clipInfo[0].clip.length - 0.15f);
        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyController>().EnemyDestroyed();
    }

    private void AddScore() {
        PlayerController.getInstance().AddScore(enemyData.score);
    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(1f);
        collided = false;
    }
}
