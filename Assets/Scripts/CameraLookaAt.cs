using UnityEngine;
using System.Collections;

public class CameraLookaAt : MonoBehaviour
{

    public Transform Samolet;

    void Update()
    {

        transform.LookAt(Samolet);
    }
}
