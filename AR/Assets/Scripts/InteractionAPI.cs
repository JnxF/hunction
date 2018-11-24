using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InteractionAPI : MonoBehaviour {

    public Text displayHealth;

    void Start () {
        displayHealth.text = "Aqui tiene que ir holita metro";
		StartCoroutine(communicateHeroku());
    }
 
    void Update () {

    }

    public void sendData() {
        StartCoroutine(communicateHeroku(option));
    }

    IEnumerator communicateHeroku(int option) {
		Debug.Log ("qewqeqeqw");
		UnityWebRequest www = UnityWebRequest.Get("http://hunction2018.herokuapp.com/");
		Debug.Log ("Vamos a enviar");
		yield return www.SendWebRequest ();
		Debug.Log ("Fin");
		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
		}
		Debug.Log(www.downloadHandler.text);
        displayHealth.text = www.downloadHandler.text;
    }
}