using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Vector3 previousCameraPosition;
    public float parallaxEffectMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        previousCameraPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = Camera.main.transform.position - previousCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier);
        previousCameraPosition = Camera.main.transform.position;
    }
}