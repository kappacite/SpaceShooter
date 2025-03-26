using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBonusDrop : MonoBehaviour
{
    public List<GameObject> bonuses;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop(Transform transform) {

        GameObject gameObject = bonuses[Random.Range(0, bonuses.Count)];
        GameObject spawned = Instantiate(gameObject);
        spawned.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        spawned.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1.5f, 0);
        Destroy(spawned, 10f);

    }
}
