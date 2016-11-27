using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		fbactive = false;
		invitinglab.SetActive (false);
		buttons.SetActive (true);
		foreach (GameObject item in contents) 
		{
			item.SetActive (false);
		}
		panel.SetActive (false);
		print ("USER: " + R.user);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (KeyCode.Escape)) 
		{
			Application.Quit();
		}
		rankpane.GetComponent<UIGrid>().Reposition();			
		rankpane.GetComponent<UIGrid>().repositionNow = true;
	}

	public void onBack()
	{
		buttons.SetActive (true);
		panel.SetActive (false);
		foreach (GameObject item in contents) 
		{
			item.SetActive (false);
		}
	}

	public void onPlay()
	{
		if (fbactive)
			return;
		if (R.user == "_X_")
			Application.LoadLevel ("sign");
		else 
		{
			if(R.current_level<=3)
				Application.LoadLevel ("level"+R.current_level);		
			if(R.current_level==4 || R.current_level ==5)
			{
				if(R.invite.Length>=3)
					Application.LoadLevel("level"+R.current_level);
				else 
				{
					buttons.SetActive(false);
					dialog.SetActive(true);
					panel.SetActive (false);
					foreach(GameObject go in contents)
						go.SetActive (false);
					dialog.transform.FindChild("content").gameObject.GetComponent<UILabel>().text = "To Level Up, You must invite 3 players to game now.";
				}
			}
			if(R.current_level >= 6)
			{
				if(R.invite.Length>=6)
					Application.LoadLevel("level"+R.current_level);
				else 
				{
					buttons.SetActive(false);
					dialog.SetActive(true);
					panel.SetActive (false);
					foreach(GameObject go in contents)
						go.SetActive (false);
					dialog.transform.FindChild("content").gameObject.GetComponent<UILabel>().text = "To Level Up, You must invite 3 more players to game now.";
				}
			}
		}

	}

	public void onOption(){
		if (fbactive)
			return;
		normalBtns [0].SetActive (false);
		normalBtns [1].SetActive (true);
		normalBtns [2].SetActive (true);
		normalBtns [3].SetActive (true);
		activeBtns [0].SetActive (true);
		activeBtns [1].SetActive (false);
		activeBtns [2].SetActive (false);
		activeBtns [3].SetActive (false);
		if(R.user != "_X_")
		{
			foreach (GameObject item in contents)
			{
				item.SetActive (false);
			}
			contents [0].SetActive (true);
			panel.SetActive (true);
			sound.value = R.sound;
		}
		else
		{
			buttons.SetActive(false);
			dialog.SetActive(true);
			dialog.transform.FindChild("content").gameObject.GetComponent<UILabel>().text = "You should login before this operation!";
		}
	}
		
	public void onSound()
	{
		if (fbactive)
			return;
		R.sound = R.sound ? false : true;
	}
	
	public void onStatus(){
		if (fbactive)
			return;
		normalBtns [0].SetActive (true);
		normalBtns [1].SetActive (false);
		normalBtns [2].SetActive (true);
		normalBtns [3].SetActive (true);
		activeBtns [0].SetActive (false);
		activeBtns [1].SetActive (true);
		activeBtns [2].SetActive (false);
		activeBtns [3].SetActive (false);
		if(R.user != "_X_")
		{
			foreach (GameObject item in contents)
			{
				item.SetActive (false);
			}
			contents [1].SetActive (true);
			panel.SetActive (true);
			
			for(int i=0; i<8; i++)
			{
				if(i>R.last_level-1)
				{
					level[i].GetComponent<BoxCollider>().enabled = false;
				}
				else
				{
					level[i].GetComponent<BoxCollider>().enabled = true;
				}
			}
			level[R.current_level-1].GetComponent<UIToggle>().value = true;
		}
		else
		{
			buttons.SetActive(false);
			dialog.SetActive(true);
			dialog.transform.FindChild("content").gameObject.GetComponent<UILabel>().text = "You should login before this operation!";
		}
	}
	
	public void onLevel(){
		if (fbactive)
			return;
		normalBtns [0].SetActive (true);
		normalBtns [1].SetActive (true);
		normalBtns [2].SetActive (false);
		normalBtns [3].SetActive (true);
		activeBtns [0].SetActive (false);
		activeBtns [1].SetActive (false);
		activeBtns [2].SetActive (true);
		activeBtns [3].SetActive (false);
		if(R.user != "_X_")
		{
			foreach (GameObject item in contents)
			{
				item.SetActive (false);
			}
			contents [2].SetActive (true);
			panel.SetActive (true);
			StartCoroutine(getMyLevels());
		}
		else
		{
			buttons.SetActive(false);
			dialog.SetActive(true);
			dialog.transform.FindChild("content").gameObject.GetComponent<UILabel>().text = "You should login before this operation!";
		}
	}

	public IEnumerator getMyLevels()
	{
		WWW request = new WWW (R.url+"?getlevels=true&username="+R.user);
		yield return request;
		if (request.error != null) 
		{
			onBack ();
		}
		else
		{
			JSONNode jnode = JSONNode.Parse(request.text);	
			for(int i=0; i<8; i++)
			{
				levels[i].text = jnode["level"+(i+1)].Value!="-1"?jnode["level"+(i+1)].Value:"deactivated";
			}		
			int totalscore = jnode["total_score"].AsInt;
			totalScore.text = "TOTAL SCORE:  "+totalscore;
		}
	}

	public void onRank(){
		if (fbactive)
			return;
		normalBtns [0].SetActive (true);
		normalBtns [1].SetActive (true);
		normalBtns [2].SetActive (true);
		normalBtns [3].SetActive (false);
		activeBtns [0].SetActive (false);
		activeBtns [1].SetActive (false);
		activeBtns [2].SetActive (false);
		activeBtns [3].SetActive (true);
		if(R.user != "_X_")
		{
			foreach (GameObject item in contents)
			{
				item.SetActive (false);
			}
			contents [3].SetActive (true);
			panel.SetActive (true);
			StartCoroutine(getRank());
		}
		else
		{
			buttons.SetActive(false);
			dialog.SetActive(true);
			dialog.transform.FindChild("content").gameObject.GetComponent<UILabel>().text = "You should login before this operation!";
		}
	}

	public IEnumerator getRank()
	{
		WWW request = new WWW (R.url+"?getrank=true");
		yield return request;
		if (request.error != null) 
		{
			onBack();
		}
		else 
		{
			for(int i=0; i<rankpane.transform.childCount; i++)
			{
				Destroy(rankpane.transform.GetChild(i).gameObject);
			}
			int myrank = 0;
			JSONNode jnode = JSONNode.Parse(request.text);
			for(int i=0; i<jnode.Count; i++)
			{
				GameObject item = (GameObject)GameObject.Instantiate(rankitem);
				item.transform.parent = rankpane.transform;
				item.transform.localScale = new Vector3(1, 1, 1);
				item.transform.localPosition = new Vector2(0, 40);
				if(jnode[i]["username"].Value == R.user)
				{
					item.transform.FindChild("rank").gameObject.GetComponent<UILabel>().text = "[00ffff]"+jnode[i]["rank"].Value;
					item.transform.FindChild("name").gameObject.GetComponent<UILabel>().text = "[00ffff]"+jnode[i]["username"].Value;
					item.transform.FindChild("score").gameObject.GetComponent<UILabel>().text = "[00ffff]"+jnode[i]["total_score"].Value;
					myrank = i+1;
				}
				else
				{
					item.transform.FindChild("rank").gameObject.GetComponent<UILabel>().text = jnode[i]["rank"];
					item.transform.FindChild("name").gameObject.GetComponent<UILabel>().text = jnode[i]["username"];
					item.transform.FindChild("score").gameObject.GetComponent<UILabel>().text = jnode[i]["total_score"];
				}
//				myranklab.text = "("+myrank+"/"+(jnode.Count+1)+")";
			}
			rankpane.GetComponent<UIGrid>().Reposition();			
			rankpane.GetComponent<UIGrid>().repositionNow = true;
		}
	}

	public void OnSelectLevel()
	{
		if (fbactive)
			return;
		for (int i=0; i<8; i++) 
		{
			if(level[i].GetComponent<UIToggle>().value == true)
				R.current_level = i+1;
			print ("current_lev:"+R.current_level);
		}
	}
	
	public void OnDialog()
	{
		buttons.SetActive (true);
		dialog.SetActive (false);
	}

	public void onExit()
	{
		Application.Quit ();
	}

	public void OnAboutCoffee()
	{
		if (fbactive)
			return;
		Application.ExternalEval("window.open('http://www.moyeecoffee.com')");
//		Application.OpenURL ("http://www.moyeecoffee.com");
	}

	public void OnFairChain()
	{
		if (fbactive)
			return;
		Application.ExternalEval ("window.open('http://www.moyeecoffee.com/ons-verhaal')");
//		Application.OpenURL ("http://www.moyeecoffee.com/ons-verhaal");
	}

	public void OnCheatWayUp()
	{
		Application.ExternalEval ("window.open('http://www.moyeecoffee.com/koffie-bestellen.html')");
//		Application.OpenURL ("http://www.moyeecoffee.com/koffie-bestellen.html");
	}

	
	#region FB.AppRequest() Friend Selector
	
	public string FriendSelectorTitle = "KopjeGooien Game";
	public string FriendSelectorMessage = "Guido would like you to play kopje gooien with him.";
	private string[] FriendFilterTypes = new string[] { "None (default)", "app_users", "app_non_users" };
	private int FriendFilterSelection = 0;
	private List<string> FriendFilterGroupNames = new List<string>();
	private List<string> FriendFilterGroupIDs = new List<string>();
	private int NumFriendFilterGroups = 0;
	public string FriendSelectorData = "{}";
	public string FriendSelectorExcludeIds = "";
	public string FriendSelectorMax = "5";
	string status;
	
	public void OnInvite()
	{
		invitinglab.SetActive (true);
		if (!FB.IsInitialized)
			FB.Init (AfterInit);
		else
			AfterInit ();
	}
	public void AfterInit()
	{
		fbactive = true;
		if (!FB.IsLoggedIn) 
		{
			try 
			{
				FB.Login ("public_profile,email,user_friends", LoginCallback);
			} 
			catch (Exception e) 
			{
				fbactive = false;
				invitinglab.SetActive (false);
			}
		} else
			CallAppRequestAsFriendSelector ();
	}
	
	public void LoginCallback(FBResult result)
	{
		CallAppRequestAsFriendSelector ();
	}
	
	private void CallAppRequestAsFriendSelector()
	{
		// If there's a Max Recipients specified, include it
		int? maxRecipients = null;
		if (FriendSelectorMax != "")
		{
			try
			{
				maxRecipients = System.Int32.Parse(FriendSelectorMax);
			}
			catch (Exception e)
			{
				status = e.Message;
				fbactive = false;
				invitinglab.SetActive (false);
			}
		}
		
		// include the exclude ids
		string[] excludeIds = (FriendSelectorExcludeIds == "") ? null : FriendSelectorExcludeIds.Split(',');
		
		// Filter groups
		List<object> FriendSelectorFilters = new List<object>();
		if (FriendFilterSelection > 0)
		{
			FriendSelectorFilters.Add(FriendFilterTypes[FriendFilterSelection]);
		}
		if (NumFriendFilterGroups > 0)
		{
			for (int i = 0; i < NumFriendFilterGroups; i++)
			{
				FriendSelectorFilters.Add(
					new FBAppRequestsFilterGroup(
					FriendFilterGroupNames[i],
					FriendFilterGroupIDs[i].Split(',').ToList()
					)
					);
			}
		}
		
		FB.AppRequest(
			FriendSelectorMessage,
			null,
			(FriendSelectorFilters.Count > 0) ? FriendSelectorFilters : null,
			excludeIds,
			maxRecipients,
			FriendSelectorData,
			FriendSelectorTitle,
			Invite
		);
	}
	
	public void Invite(FBResult result)
	{
		JSONNode jnode = JSON.Parse (result.Text);
		if (jnode.Count > 0) 
		{
			string[] to = new string[jnode["to"].Count];
			for(int i=0; i<jnode["to"].Count; i++)
			{
				to[i] = jnode["to"][i].Value;
			}
			FB.AppRequest (
				""+FriendSelectorMessage, null, null,
				to, "{\"app name\":\"KopjeGooien\"}",
				FriendSelectorTitle+"",
				ResultInvite
			);
		}
	}
	
	public void ResultInvite(FBResult result)
	{	
		JSONNode jnode = JSON.Parse (result.Text);
		string invited = R.invite[0];
		for (int i=1; i<R.invite.Length; i++)
			invited += "," + R.invite [i];
		if (jnode.Count > 0) 
		{
			for (int i=0; i<jnode["to"].Count; i++) 
			{
				bool ff = true;
				for (int j=0; j<R.invite.Length; j++) 
				{
					if (jnode ["to"] [i].Value == R.invite [j]) 
					{
						ff = false;
						break;
					}
				}
				if (ff) 
				{
					invited += (invited == "" ? "" : ",") + jnode ["to"] [i].Value;
				}
			}
			R.invite = null;
			R.invite = invited.Split (',');
			StartCoroutine (UpdateInvited (invited));
		}
		else 
		{
			fbactive = false;
			invitinglab.SetActive (false);
		}
	}

	public IEnumerator UpdateInvited(string invited)
	{
		WWW request = new WWW (R.url+"?updateinvite=true&username="+R.user+"&invite="+invited);
		yield return request;
		if (request.error != null) 
		{
			onBack ();
			invitinglab.SetActive (false);
			fbactive = false;
		}
		else 
		{			
			fbactive = false;
			invitinglab.SetActive (false);
		}
	}
	#endregion


	bool fbactive;

	public GameObject buttons;
	public GameObject panel;
	public GameObject[] normalBtns;
	public GameObject[] activeBtns;
	public GameObject[] contents;
	public GameObject dialog;
	public UILabel[] levels;
	public UILabel totalScore;
	public UIToggle[] level;
	public GameObject rankitem;
	public GameObject rankpane;
	public UIToggle sound;
	public GameObject invitinglab;
//	public UILabel myranklab;
}
