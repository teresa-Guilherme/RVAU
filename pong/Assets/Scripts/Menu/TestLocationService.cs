﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TestLocationService : MonoBehaviour
{
    IEnumerator Start()
    {

        Debug.Log("yolololo");

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("nao esta enabled");
            yield break;
        }
           

        Debug.Log("passei isto");

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Latitude: " + Input.location.lastData.latitude +"\n" +"Longitude: " + Input.location.lastData.longitude;
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}