using UnityEngine;
using System.Collections;

public class CifriTerenDve : MonoBehaviour
{
    public GameObject[] cifri;
    public static int rezultat;
    public static int cifraEdno;
    public static int cifraDve;
    Transform myTransform;
    //public SamoletTeren samoletTeren;
    
  public void Start()
    {
        

        cifraEdno = Random.Range(0, 5);
        cifraDve = Random.Range(0, 5);

        rezultat = cifraEdno + cifraDve;
        Debug.Log("Rezultata e: " + rezultat);
        
        Debug.Log(cifraEdno);
        Debug.Log(cifraDve);
        

        myTransform = transform;
        int broiach = myTransform.childCount;
        cifri = new GameObject[broiach];

        for (int i = 0; i < broiach; i++)
        {
            

            cifri[i] = myTransform.GetChild(i).gameObject;
            cifri[i].transform.position = new Vector3
                (Random.Range(-40f, 40f),
                 Random.Range(4f, 8f),
                 Random.Range(-40f, 40f));

            GameObject cifra = cifri[i];
            cifra.SetActive(true);

            

        }
       
    }
  public void Update()
  {

      
  }
  //void LateUpdate() {

  //    if (samoletTeren.pravilenOtgovor == true)
  //    {
  //        Debug.Log("Ajde promeni zadachata de");
  //        cifraEdno = 222;
  //    }
  
  //}
}
