using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object3DClick : MonoBehaviour {
	public Transform target1;
	public GameObject canvasPanel1;
//	public GameObject canvasPanel2;
	public Text getUsername;
	public Button cekOrder;
	public GameObject sushiRaycast;
	// Use this for initialization
	void Start () {
		
		canvasPanel1 = GameObject.FindGameObjectWithTag("Popup");
		canvasPanel1.gameObject.SetActive(false);

//		canvasPanel2 = GameObject.FindGameObjectWithTag("Popup2");
//		canvasPanel2.gameObject.SetActive(false);

		getUsername.text = "Halo, " + PlayerPrefs.GetString ("namaUser");
		cekOrder.onClick.AddListener (() => lihatOrder ());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit)) {
				string colliderName = hit.collider.name;
				Debug.Log ("My Marker is " + colliderName);
				canvasPanel1.gameObject.SetActive(true);
				cekOrder.gameObject.SetActive (false);

//				if(hit.transform == target1) {
//					canvasPanel = GameObject.FindGameObjectWithTag("OrderPanel");
//					canvasPanel.gameObject.SetActive(true);	
//				}
			}
		}
	}

	void lihatOrder(){
		Application.LoadLevel ("checkOrder");
//		canvasPanel1.gameObject.SetActive(false);
//		canvasPanel2.gameObject.SetActive(true);
//		sushiRaycast.gameObject.SetActive(false);
	}
}
