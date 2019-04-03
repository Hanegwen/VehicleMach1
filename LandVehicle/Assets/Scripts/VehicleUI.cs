using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VehicleUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Speed;

    [SerializeField]
    TextMeshProUGUI Elevation;

    [SerializeField]
    Slider Fuel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Speed.text = "";
        Elevation.text =  "";
    }
}
