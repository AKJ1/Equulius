using UnityEngine;
using System.Collections;

public class SamoletTeren : MonoBehaviour
{
    const float MAX_SPEED = 10;
    const float MIN_SPEED = 3;

    public float speed;
   
    public Rigidbody rbd;
    float angle;
    public bool useGravity;
    float planeRotateTime = 1f;
    float planeReturnRotationSpeed = 0f;
    float planeReturnDampTime = 0.2f;
    public bool udar = false;
    private static int novRezultat;
    private static int cifraEdnoN;
    private static int cifraDveN;
    public bool pravilenOtgovor = false;

    public GUIText zadachaOtgovor;
    public GUIText zadachaVupros;
    
    int cifraEdnoNN = cifraEdnoN;
    int cifraDveNN = cifraDveN;
    //int novRezultatN;

    public GameObject pravilnoParticles;
    public GameObject greshkaParticles;

    public void Start() {

        cifraEdnoN = CifriTerenDve.cifraEdno;
        cifraDveN = CifriTerenDve.cifraDve;
        
        zadachaVupros.text = cifraEdnoN + " + " + cifraDveN + " = ? ";
        zadachaOtgovor.text = "";

        
    }
   
    void Update() {

        
        novRezultat = CifriTerenDve.rezultat;

        

        if (udar == false)
        {
            this.transform.Translate(0f, 0f, speed * Time.deltaTime);
        }

        if (pravilenOtgovor == true)
        {
            zadachaVupros.text = cifraEdnoNN + " + " + cifraDveNN + " = ?";
            novRezultat = cifraEdnoNN + cifraDveNN;
            //Debug.Log("Новият сбор е: " + novRezultat);
        }
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (udar == false)
        {
            //this.transform.Translate(0f, 0f, speed * Time.deltaTime);

            if (moveHorizontal > 0f)
            {
                angle += 30 * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0f, angle, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, -30f, ref planeReturnRotationSpeed, planeRotateTime));
            }
            else if (moveHorizontal < 0f)
            {
                angle -= 30 * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0f, angle, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 30f, ref planeReturnRotationSpeed, planeRotateTime));
            }
            else if (transform.rotation.eulerAngles != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0f, angle, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 0f, ref planeReturnRotationSpeed, planeReturnDampTime));

            }

            Vector3 movement = new Vector3(0f, moveVertical, 0f);
            

            if(Input.GetKey(KeyCode.Space))
            {
                speed = Mathf.Min(speed + 1, MAX_SPEED);
            } else
            {
                speed = Mathf.Max(speed - .3f, MIN_SPEED);
            }

            rbd.velocity = movement * speed;

        }
    }

    public void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "Cifri") //"Cifri" за сцента с Pool
        {
            coll.transform.position = new Vector3 //Преместване на ударена цифра в Пула
                   (Random.Range(-40f, 40f),
                    Random.Range(4f, 8f),
                    Random.Range(-40f, 40f));

            //coll.transform.gameObject.SetActive(false); изключване на обектите в Pool
            int number = coll.transform.GetComponent<NumberData>().number;
            Debug.Log("Udari cifra: " + number);


            if (number == novRezultat)
            {
                Debug.Log("Правилен отговор!");
                pravilenOtgovor = true;

                cifraEdnoNN = Random.Range(0, 5);
                cifraDveNN = Random.Range(0, 5);

                
                Instantiate(pravilnoParticles, transform.position, transform.rotation);
                //zadachaOtgovor.text = "         " + number + " - правилен отговор!";
            }
            else if (novRezultat != number)
            {
                //zadachaOtgovor.text =  "         X";
                Instantiate(greshkaParticles, transform.position, transform.rotation);
                Debug.Log("Неправилен отговор!");
            }
            
        }
    }

}
