using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

    public float bulletSpawnChance;
    public float shotCooldown;
    private float nextReadyTime;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnChance = 0.0001f;
        shotCooldown = 0.75f;
        nextReadyTime = 0.0f;
    }

    public GameObject alienProjectile;
    public AudioClip alienShoot;
    // Update is called once per frame
    void Update()
    {
        if (Random.value < bulletSpawnChance && Time.time > nextReadyTime)
        {
            AudioSource.PlayClipAtPoint(alienShoot, gameObject.transform.position);
            nextReadyTime = Time.time + shotCooldown;
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 4.1f;
            GameObject obj = Instantiate(alienProjectile, spawnPos,
                Quaternion.identity) as GameObject;
        }
    }

    public GameObject explosionParticles;
    public AudioClip alienDeath;
    public void Die()
    {

        AudioSource.PlayClipAtPoint(alienDeath, gameObject.transform.position);
        Instantiate(explosionParticles, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

        GameObject global = GameObject.Find("globalObject");
        Global g = global.GetComponent<Global>();
        g.score += 10;
        Destroy(gameObject);
    }
}
