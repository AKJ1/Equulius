using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gui2 : MonoBehaviour
{

    public void OnPlayGame()
    {
        Application.LoadLevelAsync("Teren");
    }

    public void LoadLevel(string name)
    {
        Application.LoadLevelAsync(name);
    }
}
