using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserBase : MonoBehaviour
{
    public float moveSpeed;
    public float shotCooldown;
    private float nextReadyTime;
    private float moveInput;
    private Rigidbody rb;
    public float jumpForce = 50000.0f;
    public bool isGrounded = true;

    public GameObject laserBeam;
    public float laserBeamDuration = 60.0f;
    private float laserBeamTimer;

    private float laserBeamChargeDuration = 240.0f;
    private float laserBeamChargeTimer;

    private ParticleSystem particles;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        particles = GetComponent<ParticleSystem>();
    }


    // Start is called before the first frame update
    void Start()
    {
        laserBeam.SetActive(false);
        particles.Stop();
        moveSpeed = 10.0f;
        shotCooldown = 0.75f;
        nextReadyTime = 0.0f;
    }

    public AudioClip laserShoot;
    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Shoot") > 0 && Time.time > nextReadyTime)
        {
            Shoot();
            nextReadyTime = Time.time + shotCooldown;
        }
        GameObject global = GameObject.Find("globalObject");
        Global g = global.GetComponent<Global>();
        if (Input.GetKeyDown(KeyCode.C) && g.charge >= 3)
        {
            ShootBig();
            nextReadyTime = Time.time + shotCooldown;
            g.charge = 0;
        }
        if (Input.GetKeyDown(KeyCode.X))// && isGrounded)
        {
            Jump();
        }
        laserBeamTimer--;
        laserBeamChargeTimer--;
        if (laserBeamChargeTimer == 0)
        {
            laserBeam.SetActive(true);

            AudioSource.PlayClipAtPoint(laserShoot, gameObject.transform.position);
            laserBeamTimer = laserBeamDuration;
        }

        if (laserBeamTimer == 0)
        {
            laserBeam.SetActive(false);
            particles.Stop();

        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(
            -moveInput * moveSpeed,
            rb.velocity.y,
            rb.velocity.z
        );

        // Gravity
        rb.AddForce(new Vector3(0, 0, 7000));

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, -45f, 45f);
        rb.position = pos;
    }
    void Jump()
    {
        rb.AddForce(new Vector3(0, 0, -jumpForce), ForceMode.Impulse);
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.z > 0.5f)
        {
            isGrounded = true;
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

    public AudioClip laserCharge;
    void ShootBig()
    {
        particles.Play();
        AudioSource.PlayClipAtPoint(laserCharge, gameObject.transform.position);

        laserBeamChargeTimer = laserBeamChargeDuration;
        // Rigidbody rb = obj.GetComponent<Rigidbody>();
        // rb.mass = 100;
        // rb.AddForce(new Vector3(0, 0, -80000.0f));
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
