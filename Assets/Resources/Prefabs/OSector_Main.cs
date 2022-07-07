using System;
using UnityEngine;

public class OSector_Main : MonoBehaviour
{
    [SerializeField] Transform SelfTransform;

    public event Action<Transform> DOnPlayerCollision = delegate { };

    public float Angle
    {
        set
        {
            SelfTransform.rotation = Quaternion.Euler(0, 0, value);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DOnPlayerCollision(collision.transform);
        }
    }

}