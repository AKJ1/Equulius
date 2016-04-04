using UnityEngine;
using System.Collections;

public class NovoDvijenie : MonoBehaviour
{
    public Rigidbody rbd;
    public float maxUpAngle;
    public float maxHorAngle;
    float currentUpSpeed;
    float currentHorizontalSpeed;
    public bool udar = false;

    void Start()
    {
        if (float.IsNaN(maxUpAngle))
        {
            maxUpAngle = 30;
        }

        if (float.IsNaN(maxHorAngle))
        {
            maxHorAngle = 30;
        }
    }
    void Update()
    {
        if (udar == false)
        {
            this.transform.Translate(0f, 0f, 5f * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        float upAxis = Input.GetAxis("Vertical");
        float horAxis = Input.GetAxis("Horizontal");

        currentUpSpeed = upAxis * maxUpAngle;
        currentHorizontalSpeed = -horAxis * maxHorAngle;

        Debug.Log(currentHorizontalSpeed + ", " + currentUpSpeed);

        if (upAxis != 0 || horAxis != 0)
        {
            transform.rotation = Quaternion.Euler(currentUpSpeed, 0, currentHorizontalSpeed);
        }
    }

    public float GetUpSpeed()
    {
        return currentUpSpeed;
    }

    public float GetHorizontalSpeed()
    {
        return currentHorizontalSpeed;
    }
}

