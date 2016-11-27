using UnityEngine;
using System.Collections;
using SimpleJSON;

public class Sign : MonoBehaviour {

	// Use this for initialization
	void Start () {
		modal = false;
		state = 0;
		logintext.SetActive (false);
		registertext.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (KeyCode.Escape)) 
		{
			OnBack ();
		}
	}

	public void OnBack()
	{
		Application.LoadLevel ("menu");
	}

	public void OnLogin()
	{
		if (login_user.value == "") 
		{
			ShowDialog("Please Input UserName or Email.", 0);
			return;
		}
		if (login_password.value == "") 
		{
			ShowDialog("Please Input Password.", 0);
			return;
		}
		logintext.SetActive (true);
		login.SetActive (false);
		StartCoroutine(loginin ());
	}
	public IEnumerator loginin()
	{
		WWW request = new WWW (R.url+"?login=true&username="+login_user.value+"&password="+login_password.value);
		yield return request;
		if (request.error != null) 
		{
			ShowDialog (request.error, 0);
		}
		else 
		{
			JSONNode jnode = JSON.Parse(request.text);
			string res = jnode["result"].Value;
			if(res == "success")
			{				
				R.current_level = int.Parse(jnode["current_level"].Value);
				R.last_level = int.Parse(jnode["last_level"].Value);
				R.levelScore = (int.Parse(jnode["level_score"].Value))==-1?0:(int.Parse(jnode["level_score"].Value));
				R.user = login_user.value;

				string tmp = jnode["to"].Value;
				R.invite = tmp.Split(',');
//				Application.LoadLevel("level"+R.current_level);
				Application.LoadLevel("menu");
			}
			else if(res == "none")
			{
				ShowDialog ("This user is not exist.", 0);
			}
			else
			{
				ShowDialog ("Wrong Input or Internet error.", 0);
			}
		}
	}

	public void OnRegister()
	{
		login.SetActive (false);
		register.SetActive (true);
	}

	public void OnBack1()
	{
		register.SetActive (false);
		login.SetActive (true);
	}

	public void OnRegister1()
	{
		if (register_user.value == "") 
		{
			ShowDialog("Please Input UserName.", 1);
			return;
		}
		if (register_user.value == "") 
		{
			ShowDialog("Please Input Email.", 1);
			return;
		}
		if (register_password.value == "") 
		{
			ShowDialog("Please Input Passwrod.", 1);
			return;
		}
		if (register_confirm.value == "") 
		{
			ShowDialog("Please Input Confirm.", 1);
			return;
		}		
		if (register_confirm.value != register_password.value) 
		{
			ShowDialog("Password must be equal to Confirm.", 1);
			return;
		}		
		register.SetActive (false);
		registertext.SetActive (true);
		StartCoroutine(registering ());
	}
	public IEnumerator registering()
	{
		WWW request = new WWW (R.url+"?register=true&username="+register_user.value+"&email="+register_email.value+"&password="+register_password.value);
		yield return request;
		if (request.error != null) 
		{
			ShowDialog (request.error, 1);
		}
		else 
		{
			if(request.text == "success")
			{
				ShowDialog ("Registered successfully.", 1);
			}
			else if(request.text == "duplicate")
			{
				ShowDialog ("This user is already exist.", 1);
			}
			else
			{
				ShowDialog (request.text, 1);
			}
		}
	}

	public void ShowDialog(string msg, int state)
	{
		modal = true;
		this.state = state;
		result.SetActive (true);
		result_content.text = msg;
		if (state == 0) 
		{
			login.SetActive (false);
			logintext.SetActive (false);
		}
		else
		{
			register.SetActive (false);
			registertext.SetActive(false);
		}
	}
	public void CloseDialog()
	{
		modal = false;
		if (state == 0) {
			login.SetActive (true);
			logintext.SetActive(false);
		} else {
			register.SetActive (true);
			registertext.SetActive(false);
		}
		result.SetActive (false);
	}

	bool modal;
	int state;

	public GameObject login;
	public GameObject register;
	public GameObject result;
	public GameObject logintext;
	public GameObject registertext;
	public UILabel result_content;
	public UIInput login_user;
	public UIInput login_password;
	public UIInput register_user;
	public UIInput register_email;
	public UIInput register_password;
	public UIInput register_confirm;	
}