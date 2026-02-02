using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChargeUI : MonoBehaviour
{
    Global globalObj;
    TMP_Text chargeText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("globalObject");
        globalObj = g.GetComponent<Global>();
        chargeText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        chargeText.text = "Charge: " + Mathf.RoundToInt(globalObj.charge * 33.3333f).ToString() + "%";
    }
}
