using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class ColorManager : MonoBehaviour
{
    //cp_PlaceOnPlane placeOnPlane = new cp_PlaceOnPlane();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRedButton()
    {
        cp_PlaceOnPlane placeOnPlane = new cp_PlaceOnPlane();
        placeOnPlane.Color = "Red";
    }

    public void OnBlueButton()
    {
        cp_PlaceOnPlane placeOnPlane = new cp_PlaceOnPlane();
        placeOnPlane.Color = "Blue";
    }

    public void OnYellowButton()
    {
        cp_PlaceOnPlane placeOnPlane = new cp_PlaceOnPlane();
        placeOnPlane.Color = "Yellow";
    }
}
