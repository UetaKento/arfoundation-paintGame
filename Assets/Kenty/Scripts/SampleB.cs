using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleB : MonoBehaviour
{
    [SerializeField]
    SampleA seriaSampleA;
    // Start is called before the first frame update
    void Start()
    {
        int intB = seriaSampleA.IntA;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
