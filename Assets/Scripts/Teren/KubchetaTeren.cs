using UnityEngine;
using System.Collections;

public class KubchetaTeren : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(0f,10f*Time.deltaTime,0f);
    }
}
