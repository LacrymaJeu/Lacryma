using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeBouge : MonoBehaviour
{
    [SerializeField]
    private PointDeRepere _waypointPath;

    [SerializeField]
    private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    private HashSet<Transform> followingObjects = new HashSet<Transform>(); // HashSet to store objects following the platform

    void Start()
    {
        TargetNextWaypoint();
    }

    void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform parentTransform = other.transform.parent;
        if (parentTransform != null)
        {
            parentTransform.SetParent(transform);
            followingObjects.Add(parentTransform); // Add the parent transform to the HashSet
            Debug.Log("Object entered trigger: " + parentTransform.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform parentTransform = other.transform.parent;
        if (parentTransform != null)
        {
            Debug.Log("Object exited trigger: " + parentTransform.name);

                foreach (Transform obj in followingObjects)
                {
                    obj.SetParent(null); // Reset the parent transform for all objects in the HashSet
                    Debug.Log("Reset parent transform for: " + obj.name);
                }
                followingObjects.Clear(); // Clear the HashSet
            
        }
    }
}
