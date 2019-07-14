using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryLog : MonoBehaviour {
    private Text textComp;
    void Awake() {
        textComp = GameObject.Find("HistoryLog").GetComponent<Text>();
        textComp.text = "";
    }

    public void AppendText(string text) {
        textComp.text += text + "\n";
    }

    public void ClearText() {
        textComp.text = "";
    }
}