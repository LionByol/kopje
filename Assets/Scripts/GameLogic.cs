using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		currentMage = null;
		R.fanDirection = NONE;
		R.fanPower = 0f;
		R.levelScore = 0;
		R.cupCnt = R.defCupCnt [R.current_level - 1];
		fanDirectionLabel.text = "";
		gameactive = true;
		continued = false;
		savingtext.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!gameactive)
			return;
		MakeMage ();
		ClearFanPowerLable ();
		ShowScore ();
		if(!continued)
			CheckWin ();
		if (Input.GetKeyUp (KeyCode.Escape)) 
		{
			gameactive = false;
			backDlg.SetActive(true);
			backContent.text = "Would you finish this level?\nYour score is "+R.levelScore+".";
		}
	}

	public void OnBackSave()
	{

		backDlg.SetActive (false);
		savingtext.SetActive (true);
		StartCoroutine (saveStatus ());
	}

	public void OnBackCancel()
	{
		backDlg.SetActive (false);
		gameactive = true;
	}
	public void OnDontSave()
	{
		Application.LoadLevel ("menu");
	}
	
	void CheckWin()
	{
		if (R.current_level == 7)
			return;
		if (R.goalScores [R.current_level - 1] <= R.levelScore) 
		{
			gameactive = false;
			winDlg.SetActive(true);
			winContent.text = "Congratulations!\n You can play next Level... Level Score: "+R.levelScore;
		}
	}

	public void OnNext()
	{
		if(R.current_level < 8)
			R.current_level ++;
		if(R.current_level > R.last_level)
			R.last_level = R.current_level;
		StartCoroutine (saveStatus ());
	}
	
	public IEnumerator saveStatus()
	{
		WWW request = new WWW (R.url+"?savelevelstatus=true&currentlevel="+R.current_level+"&lastlevel="+R.last_level+"&levelscore="+R.levelScore+"&username="+R.user);
		yield return request;
		if (request.error != null) 
		{

		}
		else 
		{
			if(request.text == "success")
			{
				Application.LoadLevel("menu");
			}
		}
	}

	public void OnContinue()
	{
		gameactive = true;
		print (gameactive);
		winDlg.SetActive (false);
		continued = true;
	}

	void ShowScore() 
	{
		scorelab.text = "" + R.levelScore;
		levellab.text = "Lv: " + R.levels[R.current_level-1];
		cupCntLab.text = "X " + R.cupCnt;
		if (R.cupCnt <= 0) 
		{
			gameactive = false;
			backDlg.SetActive(true);
			backContent.text = "Game Over\nYour score is "+R.levelScore+".";
		}
	}

	void MakeMage ()
	{
//		if (currentMage != null)
//		{
//			bool tmp = currentMage.GetComponent<PaperMage> ().activeMe;
//			if (!tmp)
//			{
//				currentMage = null;
//			}
//		}
//		else
//		{
//			currentMage = (GameObject)GameObject.Instantiate (originMage);
//			currentMage.GetComponent<PaperMage> ().activeMe = true;
//			currentMage.transform.position = R.levelPos[R.current_level-1];
//			MakeFan ();
//		}
	
		if (currentMage == null || currentMage.GetComponent<PaperMage> ().canNew) 
		{
			currentMage = (GameObject)GameObject.Instantiate (originMage);
			currentMage.GetComponent<PaperMage> ().activeMe = true;
			currentMage.transform.position = R.levelPos[R.current_level-1];
			MakeFan ();
		}

	}

	void MakeFan()
	{
		float rnd = Random.Range (0.0f, 1.0f);
		if (rnd > 0f && rnd <= 0.3f)
		{				//left
			fans [1].SetActive(true);
			fans [0].SetActive (false);
			R.fanDirection = LEFT;
			R.fanPower = rnd/0.3f;
			print ("Fan Power: "+R.fanPower);
			fanDirectionLabel.text = "→ " + string.Format("{0:0.00}", R.fanPower * 5.0f) + "m/s";
		} 
		else if (rnd > 0.3f && rnd < 0.7f)
		{		//none
			fans[1].SetActive(false);
			fans [0].SetActive (false);
			R.fanDirection = NONE;
			R.fanPower = 0f;
			fanDirectionLabel.text = "";
		} 
		else 
		{									//right
			fans[1].SetActive(false);
			fans [0].SetActive (true);
			R.fanDirection = RIGHT;
			R.fanPower = (rnd-0.7f)/0.3f;
			print ("Fan Power: "+R.fanPower);
			fanDirectionLabel.text = "← " + string.Format("{0:0.00}", R.fanPower * 5.0f) + "m/s";
		}
	}

	void ClearFanPowerLable()
	{
		if (currentMage!=null && currentMage.GetComponent<PaperMage> ().flying)
		{
			fanDirectionLabel.text = "";
		}
	}

	public void OnGoBack()
	{
		gameactive = false;
		backDlg.SetActive(true);
		backContent.text = "Would you finish this level?\nYour score is "+R.levelScore+".";
	}

	const int NONE = 0, LEFT = 1, RIGHT = 2;
	GameObject currentMage;
	bool gameactive;
	bool continued;

	public int windDirection;
	public GameObject originMage;
	public GameObject[] fans;
	public UILabel fanDirectionLabel;
	public UILabel scorelab;
	public UILabel levellab;
	public GameObject winDlg;
	public UILabel winContent;
	public GameObject backDlg;
	public UILabel backContent;
	public GameObject savingtext;
	public UILabel cupCntLab;

}
