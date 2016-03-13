using UnityEngine;
using System.Collections;

public class JivotColliderUdar : MonoBehaviour
{

    private Particle colliderZaUdar;
    float lifetimeStar = 1f;

    void Update()
    {

        lifetimeStar -= Time.deltaTime;

        if (lifetimeStar <= 0f)
        {

            Destroy(gameObject);
        }

    }
}
