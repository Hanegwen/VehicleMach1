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
    float baseElevation = 0;

    [SerializeField]
    Slider Fuel;

    VehicleMovement vehicleMovement;
    // Start is called before the first frame update
    void Start()
    {
        vehicleMovement = FindObjectOfType<VehicleMovement>();
        baseElevation = vehicleMovement.transform.position.y;

        Fuel.minValue = 0;
        Fuel.maxValue = vehicleMovement.maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (vehicleMovement.currentSpeed < 1)
        {
            Speed.text = "0 MPH";
        }
        else
        {
            Speed.text = vehicleMovement.currentSpeed + " MPH";
        }

        if (vehicleMovement.transform.position.y - baseElevation < 1)
        {
            Elevation.text = "0 FT";
        }
        else
        {
            Elevation.text = (vehicleMovement.transform.position.y - baseElevation) + " FT";
        }


        Fuel.value = vehicleMovement.currentFuel;
    }
}
