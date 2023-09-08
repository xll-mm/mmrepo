using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Planet")
                {
                    PlanetScript ps = hit.transform.GetComponent<PlanetScript>();
                    if (ps.GetMoveState() == false)
                    { // Start Moving
                        ps.StartMoving();
                    }
                }
            }
        }
	}
}
