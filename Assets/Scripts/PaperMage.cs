using UnityEngine;
using System.Collections;

public class PaperMage : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		selected = false;
		activeMe = true;
		canNew = false;
		GetComponent<Rigidbody> ().useGravity = false;
	}
	 
	// Update is called once per frame
	void Update () 
	{
		CheckThrow ();
		isFlying ();
		SpinMage ();
	}
	
	void SpinMage()
	{
		if (GetComponent<PaperMage>().activeMe && GetComponent<PaperMage>().flying)
		{
			switch (R.fanDirection) 
			{
			case 0:		//none
				GetComponent<Rigidbody>().AddForce(Camera.main.transform.right * R.fanPower * 10.0f, ForceMode.Acceleration);
				break;
			case 1:		//left
				GetComponent<Rigidbody>().AddForce(Camera.main.transform.right * R.fanPower * 10.0f, ForceMode.Acceleration);
				break;
			case 2:		//right
				GetComponent<Rigidbody>().AddForce(-Camera.main.transform.right* R.fanPower * 10.0f, ForceMode.Acceleration);
				break;
			}
		}
	}

	void CheckThrow ()
	{
		if (Input.GetMouseButtonUp (0))
		{
			if (selected && activeMe) 
			{
				selected = false;
				cnt = 100;
				releasePoint = Input.mousePosition;
				ratio = (Mathf.Sqrt(Vector2.SqrMagnitude (releasePoint - pushPoint)) / Screen.height);
				print ("Ratio: "+(Mathf.Sqrt(Vector2.SqrMagnitude (releasePoint - pushPoint))/Screen.height*6));
//				if (ratio >= 0.25f)
//					alpha = 60.0f;
//				else
					alpha = 30.0f + ratio * 30.0f;
				print ("Toss mage!!!: "+releasePoint.ToString()+": "+Screen.height);
				ThrowMage ();
			}
		}
		//print (GetComponent<Rigidbody> ().velocity.ToString ());
	}

	void ThrowMage ()
	{
		float angle;
		angle = Vector2.Angle (new Vector2 (0, releasePoint.y - pushPoint.y), (releasePoint - pushPoint));
		angle *= (releasePoint.x - pushPoint.x) > 0 ? 1 : -1;
		print ("angle: " + angle);
		GetComponent<Rigidbody> ().useGravity = true;
		Vector3 v1, v2, v3;
		v1 = gameObject.transform.position - Camera.main.transform.position;
		v2 = Camera.main.transform.forward;
		v3 = Vector3.Cross (v1,v2).normalized;
		GameObject go = new GameObject ();
		go.transform.position = v2;
		go.transform.RotateAround (new Vector3(0, 0, 0), v3, (30+15*ratio));		//angle
		go.transform.RotateAround (new Vector3 (0, 0, 0), v2, -angle);
		GetComponent<Rigidbody> ().AddForce (go.transform.position * (55+100*ratio));	//power
		R.cupCnt --;
	}

	void OnMouseDown ()
	{
		if (activeMe) 
		{
			selected = true;
			pushPoint = Input.mousePosition;
			print ("mage selected: " + pushPoint.ToString ());
		}
	}

	public bool isFlying()
	{
		bool ret = false;
		float dist = Mathf.Sqrt (Vector3.SqrMagnitude (GetComponent<Rigidbody> ().velocity));
		if (activeMe)
		{
			if(cnt>0)
			{
				flying = true;
				ret = true;
				cnt --;
			}
			else
			{
				if(dist > 2.0f)
				{
					flying = true;
					ret = true;
				}
				else if(flying)
				{
					flying = false;
					activeMe = false;
					ret = false;
				}
			}
		}
		else
		{
			flying = false;
			ret = false;
		}
		return ret;
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (activeMe) 
		{
			if(colcnt<3)
				soundRand();
			canNew = true;
			switch (collision.gameObject.name) {
			case "target1":
				flying = false;
				activeMe = false;
				switch(R.current_level)
				{
				case 1:
					R.levelScore += 10;
					break;
				case 2:
					R.levelScore += 20;
					break;
				case 3:
					R.levelScore += 30;
					break;
				case 4:
				case 5:
					R.levelScore += 25;
					break;
				case 6:
				case 7:
					R.levelScore += 50;
					break;
				}
				int rnd = Random.Range (0, 1);
				AudioSource.PlayClipAtPoint (audios[rnd], Camera.main.transform.position);
				break;
			case "target2":
				flying = false;
				activeMe = false;
				Destroy (gameObject);
				switch(R.current_level)
				{
				case 1:
					R.levelScore += 10;
					break;
				case 2:
					R.levelScore += 20;
					break;
				case 3:
					R.levelScore += 30;
					break;
				case 4:
				case 5:
					R.levelScore += 25;
					break;
				case 6:
				case 7:
					R.levelScore += 50;
					break;
				}
				rnd = Random.Range (0, 1);
				AudioSource.PlayClipAtPoint (audios[rnd], Camera.main.transform.position);
				break;
			case "target3":
				flying = false;
				activeMe = false;
				switch(R.current_level)
				{
				case 1:
					R.levelScore += 10;
					break;
				case 2:
					R.levelScore += 20;
					break;
				case 3:
					R.levelScore += 30;
					break;
				case 4:
				case 5:
					R.levelScore += 25;
					break;
				case 6:
				case 7:
					R.levelScore += 50;
					break;
				}
				rnd = Random.Range (0, 1);
				AudioSource.PlayClipAtPoint (audios[rnd], Camera.main.transform.position);
				break;
			case "target4":
				flying = false;
				activeMe = false;
				switch(R.current_level)
				{
				case 1:
					R.levelScore += 10;
					break;
				case 2:
					R.levelScore += 20;
					break;
				case 3:
					R.levelScore += 30;
					break;
				case 4:
				case 5:
					R.levelScore += 25;
					break;
				case 6:
				case 7:
					R.levelScore += 50;
					break;
				}
				rnd = Random.Range (0, 1);
				AudioSource.PlayClipAtPoint (audios[rnd], Camera.main.transform.position);
				break;
			case "target5":
				flying = false;
				activeMe = false;
				switch(R.current_level)
				{
				case 1:
					R.levelScore += 10;
					break;
				case 2:
					R.levelScore += 20;
					break;
				case 3:
					R.levelScore += 30;
					break;
				case 4:
				case 5:
					R.levelScore += 25;
					break;
				case 6:
				case 7:
					R.levelScore += 50;
					break;
				}
				rnd = Random.Range (0, 1);
				AudioSource.PlayClipAtPoint (audios[rnd], Camera.main.transform.position);
				break;
			case "target7":
				flying = false;
				activeMe = false;
				switch(R.current_level)
				{
				case 1:
					R.levelScore += 10;
					break;
				case 2:
					R.levelScore += 20;
					break;
				case 3:
					R.levelScore += 30;
					break;
				case 4:
				case 5:
					R.levelScore += 25;
					break;
				case 6:
				case 7:
					R.levelScore += 50;
					break;
				}
				rnd = Random.Range (0, 1);
				AudioSource.PlayClipAtPoint (audios[rnd], Camera.main.transform.position);
				break;
			}
			try{
				if(collision.gameObject.transform.parent.name == "target6")
				{
					flying = false;
					activeMe = false;
					switch(R.current_level)
					{
					case 1:
						R.levelScore += 10;
						break;
					case 2:
						R.levelScore += 20;
						break;
					case 3:
						R.levelScore += 30;
						break;
					case 4:
					case 5:
						R.levelScore += 25;
						break;
					case 6:
					case 7:
						R.levelScore += 50;
						break;
					}
					collision.gameObject.transform.parent.gameObject.GetComponent<Animator>().enabled = true;
					int rnd = Random.Range (0, 1);
					AudioSource.PlayClipAtPoint (audios[rnd], Camera.main.transform.position);
					print ("enable animator");
				}
			}catch(UnityException e){}
			colcnt++;
		}
	}

	void soundRand()
	{
		int rnd = Random.Range (0, 7);
		AudioSource.PlayClipAtPoint (broken[rnd], Camera.main.transform.position);
	}

	Vector2 pushPoint;
	Vector2 releasePoint;
	float force = 100.0f;			//force to mage
	float alpha;					//throw angle upward
	float ratio;					//touch distance
	int cnt;						//count value
	int colcnt = 0;

	public bool selected;			//if selected
	public bool activeMe;			//enable/disable
	public bool flying;				//is flying
	public AudioClip[] audios;		//audios
	public AudioClip[] broken;
	public bool canNew;				//can new make object
}
