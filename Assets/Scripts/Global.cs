using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Global : MonoBehaviour
{
    public int score;
    public int lives;
    public float alienOffset;

    public int charge;

    public GameObject gameOverPanel;
    private bool gameOverTriggered = false;

    private int gameOverTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        alienOffset = 0;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            TriggerGameOver();
        }

        if (gameOverTimer > 0)
        {
            gameOverTimer--;
            if (gameOverTimer == 0)
            {
                score = 0;
                Reset();
            }
        }
    }

    public GameObject shield;
    public void Reset()
    {
        charge = 0;

        gameOverTriggered = false;
        lives = 3;
        gameOverPanel.SetActive(false);

        GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield");
        foreach (GameObject sh in shields)
        {
            Destroy(sh);
        }

        Instantiate(shield, new Vector3(15, 0, 17), shield.transform.rotation);
        Instantiate(shield, new Vector3(39, 0, 17), shield.transform.rotation);
        Instantiate(shield, new Vector3(-15, 0, 17), shield.transform.rotation);
        Instantiate(shield, new Vector3(-39, 0, 17), shield.transform.rotation);


        GameObject global = GameObject.Find("globalAlien");
        GlobalAlien ga = global.GetComponent<GlobalAlien>();
        ga.DoReset(alienOffset);

        // TBD: Reset shields
    }

    public void TriggerGameOver()
    {
        if (gameOverTriggered) return;

        gameOverTriggered = true;
        gameOverPanel.SetActive(true);

        gameOverTimer = 2000;
    }
}
