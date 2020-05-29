using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMenuUI : MonoBehaviour
{
    public void StartGame(string level)
    {
        SceneController.Instance.StartGame(level);
    }
}
