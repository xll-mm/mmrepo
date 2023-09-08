using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour {

    public Color rayColor = Color.green;
    public List<Transform> path_objs = new List<Transform>();
    Transform[] theArray;

    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        path_objs.Clear();

        foreach (Transform path_obj in theArray)
        {
            if (path_obj != this.transform)
            {
                path_objs.Add(path_obj);
            }
        }
        for (int i = 0; i < path_objs.Count; i++)
        {
            Vector3 position = path_objs[i].position;
            if (i > 0)
            {
                Vector3 previous = path_objs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.1f);
            }
        }
    }

    public int GetNearestPointIndex(Vector3 targetPos)
    {
        float distance = -1.0f;
        int idx = 0;
        for (int i = 0; i < path_objs.Count; i ++)
        {
            float dist = Vector3.Distance(path_objs[i].position, targetPos);
            if (distance == -1)
            {
                distance = dist;
                idx = i;
            } else if (distance > dist)
            {
                distance = dist;
                idx = i;
            }
        }
        return idx;
    }

    public Vector3 GetFirstPointPosition()
    {
        return path_objs[0].position;
    }
}
