using UnityEngine;
using System.Collections;

public class Target1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		activeMe = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (activeMe) {
			if(collision.gameObject.name == "cup(Clone)")
			{
				activeMe = false;
				gameObject.GetComponent<Rigidbody>().useGravity = true;
			}
		}
	}

	bool activeMe = true;
}
