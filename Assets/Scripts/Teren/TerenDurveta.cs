using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class TerenDurveta : MonoBehaviour
{
    public GameObject samolet;
    public GameObject restartButton;

    const float GAME_OVER_SATURATION = .250f;

    public void OnTriggerEnter(Collider coll)
    {
        
        if(coll.gameObject.tag == "Player"){

            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            ColorSuite colorSuit = camera.GetComponent<ColorSuite>();

            samolet.GetComponent<SamoletTeren>().udar = true;
            coll.attachedRigidbody.useGravity = true;
            GetComponent<AudioSource>().Play();
            //Debug.Log("Udar");

            restartButton.SetActive(true);

            colorSuit.saturation = GAME_OVER_SATURATION;
        }
    }
}
