using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class getOrder : MonoBehaviour {
	//public List<string> SushiName = new List<string>();
	//public List<string> JumlahPerSushi = new List<string>();
	//public List<string> HargaSushi = new List<string>();
	public string json;
	public Text display;
	public GameObject orderListPrefab;
	public GameObject canvasPanel2;
	public GameObject parentPanel;
	public Button ExitPanel2;
	public Button resetBtn;
	public GameObject sushiRaycast;
	//public GameObject utama;
	Dictionary<string, string[]> hasil;

	IEnumerator Start()
	{

		//sushiRaycast.gameObject.SetActive(false);
		string uname = PlayerPrefs.GetString ("namaUser"); 
		WWW w = new WWW ("http:localhost:8080/SushiResto/getUserOrder.php?nama="+ uname);
		yield return w;
		json = w.text;
		getList();
		ExitPanel2.onClick.AddListener(() => closePanel());
		resetBtn.onClick.AddListener (() => resetOrder ());
	}

	[System.Serializable]
	public struct UserOrderList
	{
		public string pesanan;
		public string jumlah;
		public string harga;
	}

	[System.Serializable]
	public class orderData{
		public List<UserOrderList> orderList;

	}

	public void getList () {
		orderData data = JsonUtility.FromJson<orderData>(json);

		while (this.transform.childCount > 0) {
			Transform c = this.transform.GetChild(0);
			c.SetParent (null);
			Destroy (c.gameObject);
		}

		//here is the coded that generate prefab on each array on json's have
		foreach (UserOrderList getData in data.orderList) {

				//SushiName.Add (getData.pesanan);
				//JumlahPerSushi.Add (getData.jumlah);
				//HargaSushi.Add (getData.harga);

				GameObject listPrefab = (GameObject)Instantiate (orderListPrefab);
				listPrefab.transform.SetParent (parentPanel.transform, false);
				listPrefab.transform.SetParent (this.transform);
				listPrefab.transform.Find ("Nama Sushi").GetComponent<Text> ().text = getData.pesanan;
				listPrefab.transform.Find ("Harga").GetComponent<Text> ().text = getData.harga;
				listPrefab.transform.Find ("Jumlah").GetComponent<Text> ().text = getData.jumlah;
				print (getData.pesanan + " " + getData.jumlah + " " + getData.harga);
		}
	}

	void resetOrder(){
		string username = PlayerPrefs.GetString ("namaUser"); 
		string CreateUserURL = "http:localhost:8080/SushiResto/deleteorder.php?usernamePost=" + username;
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		WWW www = new WWW (CreateUserURL, form);

		Debug.Log ("Triggered Tes");
		Application.LoadLevel ("sushiScene1");
	}

	void closePanel(){
		canvasPanel2.gameObject.SetActive(false);
		sushiRaycast.gameObject.SetActive(true);
		Debug.Log ("Triggered Close");
	}


}
