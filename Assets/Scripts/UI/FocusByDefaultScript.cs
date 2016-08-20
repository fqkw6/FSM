using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FocusByDefaultScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var inputField = GetComponent<InputField>();

        inputField.Select();


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
