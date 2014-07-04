using UnityEngine;
using System.Collections;

public class Prop_AI : MonoBehaviour {
	
	public Transform[] AICARS;
	public GameObject[] prop;
	
	public float timeLeft =5;
	
	private Vector3 propPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		timeLeft -= Time.deltaTime;
		if(timeLeft < 0)
		{	
			int rand = Random.Range(0,2);
			int propRand=Random.Range(0,2);
			if(AICARS[rand])
			{
				propPosition = propRand==0?new Vector3(AICARS[rand].position.x-0.5f,AICARS[rand].position.y,AICARS[rand].position.z):new Vector3(AICARS[rand].position.x-0.5f,AICARS[rand].position.y+0.5f,AICARS[rand].position.z);
			
				Instantiate(prop[propRand],propPosition,Quaternion.Euler(-90,0,0));
				timeLeft = Random.Range(1,6);
			}
		}
	
	}
}
