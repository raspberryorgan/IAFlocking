using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLeaderController : MonoBehaviour, ISteering
{
    Vector3 _dir = Vector3.zero;
    Sphere _sphere;
    ISteering _steering;
    public float obstacleAvoidWeight;
    public float obstacleAvoidRadius;
    public LayerMask obstLayer;
    Transform targetTransform;
    void Start()
    {
        _sphere = GetComponent<Sphere>();
        //Target is leader
        _steering = new ObstacleAvoidance(transform, targetTransform, obstacleAvoidRadius, obstacleAvoidWeight, obstLayer);
    }
    void Update()
    {
        Vector3 owo = _steering.GetDir();
        _dir = owo + _sphere.PathFind();
        //_dir = _sphere.PathFind();
        _sphere.Move(_dir);
    }
    public void SetTarget(Transform _transform)
    {
        targetTransform = _transform;
    }
    public void SetPath(List<Node> path, Vector3 pos)
    {
        _sphere.SetPath(path);
        _sphere.SetFinalDestination(pos);
    }
    //Esto lo modificas con lo del coso del muouse
    public Vector3 GetDir()
    {
        return Vector3.zero;
    }
}
