using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.5f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Npc" || collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<HealthColtroller>().TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
