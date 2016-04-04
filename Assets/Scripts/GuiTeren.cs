using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiTeren : MonoBehaviour
{
    public Text rezultat;

    public void OnHomeClicked()
    {
        Application.LoadLevelAsync("Start");
    }

    public void OnResetLevelClicked()
    {
        Application.LoadLevelAsync(Application.loadedLevelName);
    }
}
