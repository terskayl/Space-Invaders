using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AlienProjectile : MonoBehaviour
{
    public Vector3 thrust;

    private bool onFloor = false;
    // Start is called before the first frame update
    void Start()
    {
        thrust.z = 800.0f;

        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().AddForce(thrust);
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
        if (collider.CompareTag("LaserBase") && !onFloor)
        {
            LaserBase laserBase = collider.gameObject.GetComponent<LaserBase>();
            laserBase.Die();
            GameObject global = GameObject.Find("globalObject");
            Global g = global.GetComponent<Global>();
            g.lives--;
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Shield"))
        {
            Shield shield = collider.gameObject.GetComponent<Shield>();
            shield.Hit();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
