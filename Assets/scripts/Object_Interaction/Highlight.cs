using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Transform cam;
    private float highlightDistance = 6f;
    public Material highlightMaterial;

    private Material originalMaterial;

    private float timer;


    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        cam = GameObject.Find("Playercam").transform;
        //highlightDistance = GameObject.Find("playerObj").GetComponent<FirstPersonPlayerInteraction>().interactionDistance;
    }

    private void Update()
    {
        if (GetComponent<Renderer>().material != originalMaterial)
        {
            timer -= .5f;
            if (timer <= 0)
            {
                GetComponent<Renderer>().material = originalMaterial;
            }
        }
    }


    public void SetHighlight()
    {
        if (timer > 0) { GetComponent<Renderer>().material = highlightMaterial; }
         timer = 1f;

    }
    
}
