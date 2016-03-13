using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public GameObject[] cifri;
    public GameObject[] samoleti;
    public int broiSamoleti;
    public Vector3 samoletiRastoianie;
    public int broiCifri;
    public float cifriIzchakvane;
    public Vector3 cifriRastoianie;
    public float samoletiIzchakvane;
   
    

    void Start()
    {
        StartCoroutine(idvashtiCifri());
        StartCoroutine(idvashtiSamoleti());
    }
  

    IEnumerator idvashtiCifri()
    {
        for (int i = 0; i < broiCifri; i++)
        {
            GameObject cifra = cifri[Random.Range(0, cifri.Length)];
            Vector3 cifriPosition = new Vector3(Random.Range(-cifriRastoianie.x, cifriRastoianie.x), Random.Range(90f, cifriRastoianie.y), Random.Range(15f, cifriRastoianie.z));


            Instantiate(cifra, cifriPosition, transform.rotation);
            yield return new WaitForSeconds(cifriIzchakvane);

        }
    }
    IEnumerator idvashtiSamoleti()
    {
        while (true) 
        {
            for (int n = 0; n < broiSamoleti; n++)
            {
                GameObject samolet = samoleti[Random.Range(0, samoleti.Length)];
                Vector3 samoletiPosition = new Vector3(Random.Range(-samoletiRastoianie.x, samoletiRastoianie.x), Random.Range(90f, samoletiRastoianie.y), Random.Range(30f, samoletiRastoianie.z));

                Instantiate(samolet, samoletiPosition, transform.rotation);
            }
            yield return new WaitForSeconds(samoletiIzchakvane);
        }
      
    }
    
}
