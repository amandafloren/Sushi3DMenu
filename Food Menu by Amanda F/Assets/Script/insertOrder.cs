using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insertOrder : MonoBehaviour {
	public string namaSushi;
	string CreateUserURL = "http://localhost:8080/SushiResto/insertOrderTemp.php";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void insertOrderUser(string username, string pesanan){
		username = PlayerPrefs.GetString ("namaUser"); 
		WWWForm form = new WWWForm ();
		form.AddField("usernamePost", username);
		form.AddField ("pesananPost", pesanan);

		WWW www = new WWW (CreateUserURL, form);
	}
}
