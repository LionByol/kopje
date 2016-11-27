using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "cup(Clone)")
			AudioSource.PlayClipAtPoint (uak, Camera.main.transform.position);
	}

	public AudioClip uak;
}
