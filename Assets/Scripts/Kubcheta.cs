using UnityEngine;
using System.Collections;

public class Kubcheta : MonoBehaviour
{
   public float speed = 3f;
  
    void Update()
    {
        transform.position += new Vector3(0, 0f, -speed * Time.deltaTime);

        if (Input.GetButton("Jump")){
            speed += 0.3f; 
            //Debug.Log(speed);
        }

        if (transform.position.z < -9)
        {

            Destroy(gameObject);
        }
    }
}
