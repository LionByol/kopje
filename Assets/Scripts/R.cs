using UnityEngine;
using System.Collections;

public class R : MonoBehaviour 
{
	public static int fanDirection;			//fan direction
	public static float fanPower;			//fan power
	public static string user = "_X_";		//user
	public static int current_level;		//current level
	public static int last_level;			//last level
	public static bool backmusic;			//background music
	public static int levelScore;			//level score
	public static int[] goalScores = {100, 200, 300, 200, 500, 500, 500, 1000};		//goal scores
	public static string[] levels = {"Knor", "Feut", "Sjaars", "Deurwacht", "Ouderejaars", "Commissaris", "Reunist", "Erelid"};
	public static bool sound;
	public static Vector3[] levelPos = {
		new Vector3(68.12633f, 9.297511f, 3.841661f),
		new Vector3(68.62189f, 9.245163f, 4.59951f),
		new Vector3(62.40481f, 9.166414f, 3.873729f),
		new Vector3(76.75339f, 9.166414f, 4.929735f),
		new Vector3(78.52716f, 9.166414f, -1.593349f),
		new Vector3(76.75339f, 9.166414f, 4.929735f),
		new Vector3(70.66343f, 9.208076f, -8.159565f),
		new Vector3(70.66343f, 9.208076f, -8.159565f),
	};
	public static int cupCnt;
	public static int[] defCupCnt = {50, 100, 150, 40, 50, 100, 500};
	public static string[] invite;
	
	public static string url = "http://www.passionleague.com/request.php";
//	public static string url = "http://localhost/request.php";
}
