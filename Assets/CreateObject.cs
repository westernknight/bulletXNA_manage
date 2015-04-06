using UnityEngine;
using System.Collections;

public class CreateObject : MonoBehaviour {

    public GameObject go;
    public Transform trans;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        if (GUILayout.Button("create"))
        {
            go = GameObject.Instantiate(go) as GameObject;
            go.transform.position = trans.position;
        }
    }
}
