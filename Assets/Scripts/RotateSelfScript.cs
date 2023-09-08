using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelfScript : MonoBehaviour {

    public float XSpeed;
    public float YSpeed;
    public float ZSpeed;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(XSpeed, YSpeed, ZSpeed);
    }
}
