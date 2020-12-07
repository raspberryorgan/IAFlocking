using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionBehavior : MonoBehaviour, IFlockBehavior
{
    public float CohesionWeight;
    public Vector3 GetDir(List<IFlockEntity> entities, IFlockEntity entity)
    {
        Vector3 center = Vector3.zero;
        Debug.Log("xd");
        for (int i = 0; i < entities.Count; i++)
        {
            center += entities[i].Position - entity.Position;
        }
        center /= entities.Count;
        return (center.normalized * CohesionWeight);
    }
}
