using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GlobalAlien : MonoBehaviour
{

    public List<GameObject> aliens;

    public float horizontalSpacing;
    public float verticalSpacing;
    public float alienBaseMoveSpeed;
    private float alienMoveSpeed;
    private Vector3 alienMoveDirection;
    private Vector3 swarmOffset;

    public float moveDownLength;
    private float moveDownSteps;
    private bool setMoveDownSteps = false;

    public float ufoTimeLength = 3000;
    private float ufoTimer;
    // Start is called before the first frame update
    void Start()
    {
        ufoTimer = ufoTimeLength;
        DoReset(0.0f);
    }

    public GameObject alien;
    private GameObject ufo;
    public void DoReset(float spawnOffset)
    {

        foreach (GameObject alien in aliens)
        {
            Destroy(alien);
        }
        alienMoveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        swarmOffset = new Vector3(45, 2, -35);
        setMoveDownSteps = false;
        alienMoveSpeed = alienBaseMoveSpeed;

        for (int i = 0; i < 11; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                Vector3 spawnPos = new Vector3(45.0f - horizontalSpacing * i,
                                               2.0f,
                                              -35.0f + verticalSpacing * j + spawnOffset);
                aliens.Add(Instantiate(alien, spawnPos, alien.transform.rotation));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ufoTimer--;
        if (ufoTimer == 0)
        {
            ufo = Instantiate(alien, new Vector3(50, 2, -35), alien.transform.rotation);
            ufoTimer = ufoTimeLength;
        }

        if (ufo != null)
        {
            ufo.transform.position -= 10 * alienMoveSpeed * Vector3.right;
        }

        float farSideOffset = swarmOffset.x - 10 * horizontalSpacing;

        if (swarmOffset.x > 45.0f || farSideOffset < -45.0f)
        {
            if (!setMoveDownSteps)
            {
                moveDownSteps = moveDownLength;
                setMoveDownSteps = true;
            }

            if (moveDownSteps > 0)
            {
                alienMoveDirection = new Vector3(0.0f, 0.0f, 1.0f);
                moveDownSteps--;
            }
            else if (swarmOffset.x > 45.0f)
            {
                alienMoveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
                setMoveDownSteps = false;
            }
            else
            {
                alienMoveDirection = new Vector3(1.0f, 0.0f, 0.0f);
                setMoveDownSteps = false;
            }
        }




        swarmOffset += alienMoveSpeed * alienMoveDirection;
        int count = 0;
        foreach (GameObject alien in aliens)
        {
            if (alien != null)
            {
                alien.transform.position += alienMoveSpeed * alienMoveDirection;
                count++;
            }
        }
        alienMoveSpeed = alienBaseMoveSpeed * (1.0f + (55.0f - count) / 55.0f);
        if (count == 0)
        {
            GameObject global = GameObject.Find("globalObject");
            Global g = global.GetComponent<Global>();
            g.alienOffset += 2.5f;
            g.Reset();
        }
    }
}
