using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour{

    public path pathToFollow;
    public int CurrentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;

    Vector3 last_position;
    Vector3 current_position;

	// Use this for initialization
	void Start () {
        //pathToFollow = GameObject.Find(pathName).GetComponent<path>();
        last_position = transform.position;		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, -0.2f, 0);
        float distance = Vector3.Distance(pathToFollow.path_objs[CurrentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);

        if (distance <= reachDistance)
        {
            CurrentWayPointID++;
            if (CurrentWayPointID == pathToFollow.path_objs.Count)
                CurrentWayPointID = 0;
        }
	}
}
