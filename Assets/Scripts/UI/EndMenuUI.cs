using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndMenuUI : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneController.Instance.ToMainMenu();
    }
}
