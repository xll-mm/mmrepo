using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {

    public PathScript originalPath;
    public PathScript galaxyPath;
    public float moveSpeed = 10.0f;
    public float scaleSpeed = 4.0f;
    public float innerNormalLightRange = 2.0f;
    public float innerMovingLightRange = 5.0f;
    private AudioSource engineAudio;

    private int currentWayPointID = 0;
    private int pathType = 0; // 0 - original path, 1 - galaxy path
    private float reachDistance = 5f;
    private int objState = 0; // 0 - ready to move, 1 - moving, 2 - reseting
    private float defaultScale = 1.5f;

    private ParticleSystem particleFlame;

    public bool GetMoveState()
    {
        return !(objState == 0);
    }
    public void StartMoving()
    {
        objState = 1;
        moveSpeed = 8.0f + Random.Range(-3.0f, 3.0f);
        reachDistance = moveSpeed / 2;
        particleFlame.Play();
    }

    // Use this for initialization
    void Start()
    {
        //pathToFollow = GameObject.Find(pathName).GetComponent<path>();
        defaultScale = transform.localScale.x;
        particleFlame = transform.Find("Flame").GetComponent<ParticleSystem>();
        particleFlame.Stop();
        engineAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moving Code
        switch (objState) 
        {
            case 0:
                {
                    
                }
                break;
            case 1:
                {
                    PathScript pathToFollow = (pathType == 0) ? originalPath : galaxyPath;
                    float distance = Vector3.Distance(pathToFollow.path_objs[currentWayPointID].position, transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, pathToFollow.path_objs[currentWayPointID].position, Time.deltaTime * moveSpeed);

                    if (distance <= reachDistance)
                    {
                        currentWayPointID++;
                        if (currentWayPointID == pathToFollow.path_objs.Count)
                        {
                            if (pathType == 0)
                            {
                                currentWayPointID = galaxyPath.GetNearestPointIndex(transform.position);
                            }
                            else
                            {
                                ResetTransform();
                            }
                            pathType = (pathType + 1) % 2;
                        }
                    }
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
                }
                break;
        }

        Light inner = transform.Find("InnerLight").GetComponent<Light>();
        // Light Code
        switch (objState)
        {
            case 1:
                {
                    inner.range = innerMovingLightRange;
                    //if (!engineAudio.isPlaying)
                    //    engineAudio.Play();
                }
                break;
            case 0:
            case 2:
                {
                    inner.range = innerNormalLightRange;
                    engineAudio.Stop();
                }
                break;
        }
    }

    void ResetTransform()
    {
        objState = 2;
        currentWayPointID = 0;
        pathType = 0;
        transform.position = originalPath.GetFirstPointPosition();
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        particleFlame.Stop();
    }
}
