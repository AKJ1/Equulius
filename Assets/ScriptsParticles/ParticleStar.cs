using UnityEngine;
using System.Collections;

public class ParticleStar : MonoBehaviour
{

    private Particle particles;
    float lifetimeStar = 3f;

    void Update()
    {

        lifetimeStar -= Time.deltaTime;

        if (lifetimeStar <= 0f)
        {

            Destroy(gameObject);
        }

    }
}
