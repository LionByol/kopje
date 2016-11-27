using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (30.0f, 0f, 0f, Space.Self); 
	}
}
