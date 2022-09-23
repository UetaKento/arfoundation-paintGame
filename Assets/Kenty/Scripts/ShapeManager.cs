using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class ShapeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCircleButton()
    {
        cp_PlaceOnPlane placeOnPlane = new cp_PlaceOnPlane();
        placeOnPlane.Shape = "Circle";
    }

    public void OnTriangleButton()
    {
        cp_PlaceOnPlane placeOnPlane = new cp_PlaceOnPlane();
        placeOnPlane.Shape = "Triangle";
    }

    public void OnSquareButton()
    {
        cp_PlaceOnPlane placeOnPlane = new cp_PlaceOnPlane();
        placeOnPlane.Shape = "Square";
    }
}
