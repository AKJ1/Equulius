using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public void OnPlayGame()
    {
        Application.LoadLevelAsync("LookAt");
    }


    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
