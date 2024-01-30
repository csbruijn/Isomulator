using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//www.youtube.com/watch?v=xMFx9HfRknU
public class ObjectTransparency : MonoBehaviour
{

    [SerializeField] private GameObject solidBody;
    [SerializeField] private GameObject transBody;



    // Start is called before the first frame update
    void Awake()
    {
        ShowSolid();
    }

    public void ShowSolid()
    {
        solidBody.SetActive(true);
        transBody.SetActive(false);
    }

    public void ShowTransperent()
    {
        solidBody.SetActive(false);
        transBody.SetActive(true);
    }

}
