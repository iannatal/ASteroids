using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour {
    
    private float originalWidth = 1980f;
    private float originalHeight = 1080f;

    private void Start () {
        GetComponent<Camera>().aspect = (originalWidth / originalHeight) * (Screen.width / Screen.height);
	}
	
}
