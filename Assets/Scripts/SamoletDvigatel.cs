using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class SamoletDvigatel : MonoBehaviour
{

    public float speed;
    public Rigidbody rbd;
    public float xMin, xMax, yMin, yMax;
    float planeRotateTime = 1f;
    float planeReturnRotationSpeed = 0f;
    float planeReturnDampTime = 0.2f;
    private int score = 0;
    public GameObject particles;
    public GameObject particlesMinus;
    public GameObject colliderZaUdar;
    public bool useGravity;
    public GameObject guiTeren;

    void Update()
    {
        guiTeren.GetComponent<Text>().text = string.Format(" {0}", score.ToString());
    }


    void FixedUpdate()
    {
        rbd.position = new Vector3( // ограничаваща рамка, работи когато не е кинематик
            Mathf.Clamp(rbd.position.x, xMin, xMax),
            Mathf.Clamp(rbd.position.y, yMin, yMax),
            0.0f
            );

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, -30f, ref planeReturnRotationSpeed, planeRotateTime));
        }
        else if (moveHorizontal < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 30f, ref planeReturnRotationSpeed, planeRotateTime));
        }
        else if (transform.rotation.eulerAngles != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 0f, ref planeReturnRotationSpeed, planeReturnDampTime));
        }

        
        //transform.position += new Vector3 (moveHorizontal * speed * Time.deltaTime, 0f, 0f); // is kinematic

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f); // движение, когато rbd не е кинематик
        rbd.velocity = movement * speed; // движение, когато rbd не е кинематик
        
        //moveVertical += 1;

    }
  
    //void OnCollisionEnter(Collision coll) { // collider is not trigger

    //    if (coll.gameObject.tag == "Kubcheta") {
    //        Destroy(coll.gameObject);
    //    }
       
    
    //}
public void OnTriggerEnter(Collider coll) // collider is trigger
    {

        if (coll.gameObject.tag == "Kubcheta")
        {
            int number = coll.transform.GetComponent<NumberData>().number;
            //Debug.Log(number);
            Destroy(coll.gameObject);
            Instantiate(particles, rbd.position, rbd.rotation);
            score = score + number;
            Debug.Log(score);
        }
        if (coll.gameObject.tag == "Samoleti")
        {

            Debug.Log("opaa nulirash si rezultata");
            coll.attachedRigidbody.useGravity = true;
            Instantiate(particlesMinus, rbd.position, rbd.rotation);
            Instantiate(colliderZaUdar, rbd.position, rbd.rotation);
            score = score - 10;
            Debug.Log("Udari samoletche:" + score);
        }


    }
}
