using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleA : MonoBehaviour
{
    private int m_intA = 5;
    public int IntA
    {
        get
        {
            return m_intA;
        }
        set
        {
            m_intA = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
