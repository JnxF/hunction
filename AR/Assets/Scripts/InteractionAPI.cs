using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InteractionAPI : MonoBehaviour {

    void Start () {
		StartCoroutine(communicateHeroku());
    }
 
    void Update () {

    }

    public void sendData(string option) {
        if (option == "coords") {
            StartCoroutine(getCoords());
        }
    }

    void getCoords() {
        UnityWebRequest www = UnityWebRequest.Get("http://hunction2018.herokuapp.com/");
        yield return www.SendWebRequest ();
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            
        }
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
    }
}