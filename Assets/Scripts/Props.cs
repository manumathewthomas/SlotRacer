using UnityEngine;
using System.Collections;

public class Props : MonoBehaviour {

	public GameObject dynamite;
	public GameObject Smoke;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyUp("z"))
		{
			Instantiate(dynamite,(transform.position-transform.forward),Quaternion.Euler(-90,0,0));
		}
		
		if(Input.GetKeyUp("x"))
		{
			Instantiate(Smoke,transform.position-transform.forward+new Vector3(0,0.5f,0),Quaternion.Euler(-90,0,0));
		}
	
	}
}
