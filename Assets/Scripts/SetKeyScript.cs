using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKeyScript : MonoBehaviour
{
    //public GameObject newKeyConfig;
    //public GameObject existingKeyConfig;

    public string newKeyConfig; //use a string for testing. Will plug in when we decide what a key looks like
    public string existingKeyConfig;
    public void SetKey()
    {
        Debug.Log("Set Key Called");
        //existingKeyConfig = newKeyConfig;
    }
}
