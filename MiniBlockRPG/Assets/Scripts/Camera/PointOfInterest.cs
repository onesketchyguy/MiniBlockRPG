using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: UNOPTIMIZED

[RequireComponent(typeof(SphereCollider))]
public class PointOfInterest : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float interestValue = 0.5f;

    private Vector4 point = Vector4.zero;

    private void OnValidate()
    {
        var sphereCollider = GetComponent<SphereCollider>();

        if (sphereCollider.isTrigger == false)
        {
            Debug.LogWarning($"{nameof(PointOfInterest)} Unable to use non-trigger colliders!");
            sphereCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var lam = other.GetComponentInChildren<LookAtManager>();

            if (lam != null)
            {
                if (point == Vector4.zero)
                {
                    point = (Vector4)transform.position + new Vector4(0, 0, 0, interestValue);
                }

                lam.AddPointOfInterest(point);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var lam = other.GetComponentInChildren<LookAtManager>();

            if (lam != null)
            {
                lam.RemovePointOfInterest(point);
            }
        }
    }
}
