using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseController : MonoBehaviour
{
    public Transform hitTransform;
    public SphereLeaderController leaderController;
    public LayerMask nodes;
    List<Node> _list;
    Theta<Node> _theta = new Theta<Node>();
    Node _start;
    Node _end;
    void Start()
    {
        leaderController.SetTarget(hitTransform);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform.position = hit.point;
                _start = FindNearNode(leaderController.transform.position);
                _end = FindNearNode(hit.point);
                //Aca va lo de run thetaStar, el setpoints y ya xd
                //Aca seteas eso y despues cuando le pones el true en el controller de la sphere va a tomar lo de la lista y darle
                _list = _theta.Run(_start, Satisfies, GetNeighbours, GetCost, Heuristic, InSight, 500);
                leaderController.SetPath(_list, hit.point);
            }

        }
    }

    //Codigo de ia
    public IEnumerable PointB(IEnumerable<GameObject> colectionGO, Func<GameObject, bool> funcGO)
    {
        for (int i = 0; i < 9; i++)
        {

        }
        foreach (var item in colectionGO)
        {
            if (funcGO(item))
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(hitTransform.position, 0.5f);
    }
    Node FindNearNode(Vector3 pos)
    {
        Collider[] _nodes = Physics.OverlapSphere(pos, 1000, nodes);
        Collider closer = null;
        float dist = 99999;
        foreach (var item in _nodes)
        {
            var currDistance = Vector3.Distance(item.transform.position, pos);
            if (currDistance < dist)
            {
                dist = currDistance;
                closer = item;
            }
        }
        return closer.GetComponent<Node>();
    }
    bool Satisfies(Node curr)
    {
        return curr == _end;
    }
    List<Node> GetNeighbours(Node curr)
    {
        return curr.neighbours;
    }
    float Heuristic(Node curr)
    {
        return Vector3.Distance(curr.transform.position, _end.transform.position);
    }
    float GetCost(Node from, Node to)
    {
        return Vector3.Distance(from.transform.position, to.transform.position);
    }
    bool InSight(Node parent, Node current)
    {
        var dir = current.transform.position - parent.transform.position;
        if (Physics.Raycast(parent.transform.position, dir.normalized, dir.magnitude, leaderController.obstLayer))
        {
            return false;
        }
        return true;
    }
}