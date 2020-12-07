using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
[RequireComponent(typeof(LeaderBehavior))]
[RequireComponent(typeof(FlockEntity))]
[RequireComponent(typeof(AlineationBehavior))]
[RequireComponent(typeof(CohesionBehavior))]
[RequireComponent(typeof(SeparationBehavior))]
*/
public class SphereController : MonoBehaviour
{
    Vector3 _dir = Vector3.zero;
    Sphere _sphere;
    public FlockEntity flock;
    ISteering _steering;
    public float obstacleAvoidWeight;
    public float smoothness = 1;
    public LayerMask obstLayer;
    void Start()
    {
        _sphere = GetComponent<Sphere>();
        flock = GetComponent<FlockEntity>();
        //Target is leader
        _steering = new ObstacleAvoidance(transform, GetComponent<LeaderBehavior>().target, flock.radius, obstacleAvoidWeight, obstLayer);
    }
    void Update()
    {
        Vector3 oldDir = Vector3.zero + _dir;

        _dir = Vector3.Lerp(oldDir, _steering.GetDir() * obstacleAvoidWeight + flock.GetDir(), smoothness);
        _sphere.Move(_dir);
    }
}