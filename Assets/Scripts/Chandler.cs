using UnityEngine;
using System.Collections;

public class Chandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnEnd()
	{
		gameObject.GetComponent<Animator> ().enabled = false;
	}
}
