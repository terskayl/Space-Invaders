using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserBase : MonoBehaviour
{
    public float moveSpeed;
    public float shotCooldown;
    private float nextReadyTime;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.1f;
        shotCooldown = 0.75f;
        nextReadyTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && gameObject.transform.position.x > -45.0f)
        {
            gameObject.transform.Translate(-moveSpeed, 0, 0);
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && gameObject.transform.position.x < 45.0f)
        {
            gameObject.transform.Translate(moveSpeed, 0, 0);
        }

        if (Input.GetAxisRaw("Shoot") > 0 && Time.time > nextReadyTime)
        {
            Shoot();
            nextReadyTime = Time.time + shotCooldown;
        }

    }

    public GameObject laserProjectile;
    public AudioClip shoot;
    void Shoot()
    {
        AudioSource.PlayClipAtPoint(shoot, gameObject.transform.position);
        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.z -= 4.1f;
        GameObject obj = Instantiate(laserProjectile, spawnPos,
            Quaternion.identity) as GameObject;
    }

    public GameObject explosionParticles;
    public AudioClip playerExplosion;
    public void Die()
    {
        Camera.main.GetComponent<CameraShake>().TriggerShake(0.7f, 0.4f);
        AudioSource.PlayClipAtPoint(playerExplosion, gameObject.transform.position);
        Instantiate(explosionParticles, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

    }
}
