using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Success"+hit.transform.tag);
                if (hit.transform.tag == "Rocket")
                {
                    RocketScript rs = hit.transform.GetComponent<RocketScript>();
                    if (rs.IsRocketMoving() == false)
                    { // Start Moving
                        rs.LaunchRocket();
                    }
                }
            }
            else
            {
                Debug.Log("NULL");
            }
        }
    }
}
