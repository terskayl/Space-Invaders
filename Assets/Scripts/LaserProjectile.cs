using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Unity.VisualScripting;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public Vector3 thrust;
    // Start is called before the first frame update
    void Start()
    {
        thrust.z = 4000.0f;

        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().AddForce(-thrust);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.z > 45.0f || gameObject.transform.position.z < -45.0f)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.CompareTag("Alien"))
        {
            Alien alien = collider.gameObject.GetComponent<Alien>();
            alien.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("AlienProjectile"))
        {
            AlienProjectile alienProjectile = collider.gameObject.GetComponent<AlienProjectile>();
            alienProjectile.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Shield"))
        {
            Shield shield = collider.gameObject.GetComponent<Shield>();
            shield.Hit();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Collided with" + collider.transform.root.tag);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
