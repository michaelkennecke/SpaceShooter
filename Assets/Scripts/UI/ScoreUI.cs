using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI display;

    void Update()
    {
        this.display.text = $"Score: {Score.GetScore().ToString()}";
    }
}
