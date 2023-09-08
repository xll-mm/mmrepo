using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

    public PathScript[] rocketPath;
    public float moveSpeed = 10.0f;
    public float scaleSpeed = 4.0f;
    public float rotateSpeed = 5.0f;
    public GameObject rocketFlames;
    public ParticleEmitter[] flames;

    private int currentWayPointID = 0;
    private float reachDistance = 20.0f;
    private int objState = 0; // 0 - ready to move, 1 - moving, 2 - reseting
    private float defaultScale = 1.0f;
    private int pathIndex = 0;
    private Quaternion defaultRotation;
    private AudioSource engineAudio;

    public bool IsRocketMoving()
    {
        return (objState == 1);
    }
    public void LaunchRocket()
    {
        pathIndex = (int)Random.Range(0.0f, 3.9f);
        objState = 1;
    }

    void ResetRockect()
    {
        objState = 2;
        currentWayPointID = 0;
        transform.position = rocketPath[pathIndex].GetFirstPointPosition();
        transform.rotation = defaultRotation;
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
    void SetFlames(bool bShow)
    {
        for (int i = 0; i < flames.Length; i ++)
        {
            if (bShow)
            {
                //flames[i].GetComponent<ParticleSystem>().Play();
                flames[i].enabled = true;
                if (!engineAudio.isPlaying)
                    engineAudio.Play();
            }
            else
            {
                flames[i].ClearParticles();
                //flames[i].GetComponent<ParticleSystem>().Stop();
                flames[i].enabled = false;
                engineAudio.Stop();
            }
        }
    }

	// Use this for initialization
	void Start () {
        defaultScale = transform.localScale.x;
        defaultRotation = transform.rotation;
        engineAudio = GetComponent<AudioSource>();
        currentWayPointID = 0;
    }
	
	// Update is called once per frame
	void Update () {
        // Moving Code
        switch (objState)
        {
            case 0:
                {
                    SetFlames(false);
                    rocketFlames.SetActive(false);
                }
                break;
            case 1:
                {
                    PathScript pathToFollow = rocketPath[pathIndex];
                    float distance = Vector3.Distance(pathToFollow.path_objs[currentWayPointID].position, transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, pathToFollow.path_objs[currentWayPointID].position, Time.deltaTime * moveSpeed);

                    if (currentWayPointID > 1)
                    {
                        var rotation = Quaternion.LookRotation(pathToFollow.path_objs[currentWayPointID].position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
                    }

                    if (distance <= reachDistance)
                    {
                        currentWayPointID++;
                        if (currentWayPointID == pathToFollow.path_objs.Count)
                        {
                            ResetRockect();
                        }
                    }
                    rocketFlames.SetActive(true);
                    SetFlames(true);
                }
                break;
            case 2:
                {
                    Vector3 scaleValue = transform.localScale;
                    scaleValue += new Vector3(Time.deltaTime * scaleSpeed, Time.deltaTime * scaleSpeed, Time.deltaTime * scaleSpeed);
                    if (scaleValue.x >= defaultScale)
                    {
                        scaleValue = new Vector3(defaultScale, defaultScale, defaultScale);
                        objState = 0;
                    }
                    transform.localScale = scaleValue;
                    SetFlames(false);
                    rocketFlames.SetActive(false);
                }
                break;
        }

        //Light inner = transform.Find("InnerLight").GetComponent<Light>();
        // Light Code
        switch (objState)
        {
            case 1:
                {
                    //inner.range = innerMovingLightRange;
                }
                break;
            case 0:
            case 2:
                {
                    //inner.range = innerNormalLightRange;
                }
                break;
        }
    }
}
