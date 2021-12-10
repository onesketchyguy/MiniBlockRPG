using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: UNOPTIMIZED

public class LookAtManager : MonoBehaviour
{
    const float CAMERA_OFFSET = 5.0f;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public Transform CinemachineCameraTarget;
    
    [SerializeField] private Transform _lookAt;

    [Range(0.0f, 10.0f)]
    [SerializeField] private float lookAtPointOfInterestOffset = 0.25f;

    [SerializeField] private float lookSpeed = 2.5f;

    [SerializeField] private float maxDistance = 5.0f;

    [Range(10.0f, 90.0f)]
    [SerializeField] private float maxAngle = 90.0f;

    private HashSet<Vector4> points = new HashSet<Vector4>();

    void FixedUpdate()
    {       
        var targetPoint = CinemachineCameraTarget.forward * CAMERA_OFFSET;
        targetPoint += transform.position;

        // Point the user is looking at
        var userPoint = targetPoint;

        if (points.Count > 0)
        {
            var removePoints = new HashSet<Vector4>();

            // Look at point of interest
            foreach (var poi in points)
            {
                var point = (Vector3)poi;

                if (Vector3.Distance(transform.position, point) > maxDistance)
                {
                    removePoints.Add(poi);
                }
                else
                {
                    var dist = Vector3.Distance(transform.position, point);
                    float interest = poi.w + (maxDistance - dist) / maxDistance;

                    var offset = lookAtPointOfInterestOffset;
                    if (dist <= 2.0f) offset = 0.0f;

                    targetPoint = Vector3.Lerp(targetPoint, point + Vector3.up * offset, interest);
                }
            }            

            // Clean up any redundant positions
            foreach (var item in removePoints) points.Remove(item);                                   

            Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.white);
        }

        // Clamp rotation to prevent neck snapping
        var directionToTarget = (targetPoint - CinemachineCameraTarget.position).normalized;
        float angle = Vector3.Angle(CinemachineCameraTarget.forward, directionToTarget);

        // If angle exceeds max angle, return to user defined look direction
        if (angle > maxAngle) targetPoint = userPoint;

        _lookAt.position = Vector3.Lerp(_lookAt.position, targetPoint, lookSpeed * Time.deltaTime);
    }

    public void AddPointOfInterest(Vector4 point)
    {
        points.Add(point);
    }

    public void RemovePointOfInterest(Vector4 point)
    {
        points.Remove(point);
    }
}
