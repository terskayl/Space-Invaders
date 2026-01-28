using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    Global globalObj;
    TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("globalObject");
        globalObj = g.GetComponent<Global>();
        scoreText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + globalObj.score.ToString();
    }
}
