using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Sphere : MonoBehaviour
{
    //Ahora de verdad
    public float speed = 2;
    public float speedRot = 0.5f;
    List<Node> _path = new List<Node>();
    Rigidbody _rb;
    bool ready;
    bool final;
    int _nextPoint = 0;
    Vector3 _finalPos;
    Vector3 _ddir;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 dir)
    {
        dir.y = 0;
        _rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        DebugDir(dir);

    }
    public Vector3 PathFind()
    {
        if (Vector3.Distance(transform.position, _finalPos) > 0.2f && final)
        {
            Debug.Log("final");
            return (_finalPos - transform.position).normalized;
        }
        if (!ready)
        {
            return Vector3.zero;
        }

        var point = _path[_nextPoint];
        var posPoint = point.transform.position;

        posPoint.y = transform.position.y;
        Vector3 dir = posPoint - transform.position;

        if (dir.magnitude < 1)
        {
            if (_nextPoint + 1 < _path.Count)
                _nextPoint++;
            else
            {
                final = true;
                ready = false;

            }
        }
        return dir.normalized;
    }
    public void SetPath(List<Node> path)
    {
        _path = path;
        _nextPoint = 0;
        ready = true;
        final = false;
    }
    public void SetFinalDestination(Vector3 pos)
    {
        _finalPos = pos;
    }
    void DebugDir(Vector3 d)
    {
        _ddir = d;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + _ddir);
        Gizmos.color = Color.green;
        foreach (var n in _path)
        {
            Gizmos.DrawCube(n.transform.position + Vector3.up, Vector3.one * 0.5f);
        }
    }
    public bool IsReady()
    {
        return ready;
    }
}