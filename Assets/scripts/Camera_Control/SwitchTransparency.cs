using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SwitchTransparency : MonoBehaviour
{
    [SerializeField] private List<ObjectTransparency> currentlyObstructing;
    [SerializeField] private List<ObjectTransparency> alreadyTrans;
    [SerializeField] private Transform player;
    private Transform cam; 


    private void Awake()
    {
        currentlyObstructing = new List<ObjectTransparency>();
        alreadyTrans = new List<ObjectTransparency>();
        cam = this.gameObject.transform; 
    }


    private void Update()
    {
        GetAllObjectsInTheWay();
        MakeObjectsSolid();
        MakeObjectsTrans();
    }

    private void GetAllObjectsInTheWay()
    {
        // clear the list
        currentlyObstructing.Clear();



        // create the raycasts to detect obstructions
        float camPlayerDistance = Vector3.Magnitude(cam.position - player.position);

        Ray ray_Forward = new Ray(cam.position, player.position - cam.position);
        Ray ray_Backward = new Ray(player.position, cam.position - player.position);

        var hits_Forward  =  Physics.RaycastAll(ray_Forward, camPlayerDistance);
        var hits_Backward = Physics.RaycastAll(ray_Backward, camPlayerDistance);

        // put the obstructions in the list
        foreach (var hit in hits_Forward)
        {
            if (hit.collider.gameObject.TryGetComponent(out ObjectTransparency Obstructing)) 
            { 
                currentlyObstructing.Add(Obstructing);
            }
        }
        foreach (var hit in hits_Backward)
        {
            if (hit.collider.gameObject.TryGetComponent(out ObjectTransparency obstructing))
            {
                currentlyObstructing.Add(obstructing);
            }
        }

    }

    private void MakeObjectsTrans()
    {
        for (int i = 0; i < currentlyObstructing.Count; i++)
        {
            ObjectTransparency Obstructing = currentlyObstructing[i];
            if (!alreadyTrans.Contains(Obstructing))
            {
                Obstructing.ShowTransperent();
                alreadyTrans.Add(Obstructing);
            }
        }
    }

    private void MakeObjectsSolid()
    {
        for (int i = alreadyTrans.Count - 1; i>= 0; i--) 
        {
            ObjectTransparency wasObstructing = alreadyTrans[i];

            if (!currentlyObstructing.Contains(wasObstructing))
            {
                wasObstructing.ShowSolid(); 
                alreadyTrans.Remove(wasObstructing);
            }
        }
    }

}
