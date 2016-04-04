using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class TerenDurveta : MonoBehaviour
{
    public GameObject samolet;
    public GameObject restartButton;

    public void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player"){

            samolet.GetComponent<SamoletTeren>().udar = true;
            coll.attachedRigidbody.useGravity = true;
            GetComponent<AudioSource>().Play();
            //Debug.Log("Udar");

            restartButton.SetActive(true);
        }
    }
}
