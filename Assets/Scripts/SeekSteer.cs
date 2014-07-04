using UnityEngine;
using System.Collections;




public class SeekSteer : MonoBehaviour
{

	
	public Transform waypointContainer;
	public Transform[] allPaths;
	public string pathName;
	public int currentPathNumber;
	private Transform[] waypointsDump;
	public Transform []waypoints;
	private int waypointLength;
	public float waypointRadius = 1.5f;
	public float damping = 0.1f;
	public bool loop = false;
	public float speed = 2.0f;
	public bool faceHeading = true;
	public bool isAI;
	public bool isGameOver;
	
	public GameObject explosion;
	public GameObject smoke;
	
	private Vector3 currentHeading,targetHeading;
	private int targetwaypoint;
	private Transform xform;
	private bool useRigidbody;
	private Rigidbody rigidmember;
	
	private Vector3 AITransformPosition;

	public bool isButtonPressed;
	
	public GUIText LapText;
	public int lapCounter;
	
	public int health;
	public GUITexture health_gui;
	public GUIText scoreText;
	public int score = 1000000;
	public Texture2D[] healthTex;
	public int scorePercentage;
	
	public AudioClip[] clips;
	public AudioSource audioSource;
	
	// Use this for initialization
	protected void Start ()
	{
		isGameOver = false;
		health = 5;
		scorePercentage =100;
		waypointLength = 0;
		lapCounter =1;
		LapText.text = "Lap: "+lapCounter+"/10";
		waypointsDump = waypointContainer.GetComponentsInChildren<Transform>() as Transform[];
		
		foreach(Transform waypoint in waypointsDump)
		{
			if(waypoint != waypointContainer.transform )
			{
				waypoints[waypointLength] = waypoint.transform;
				waypointLength++;
			}
			
		}
		
		xform = transform;
		currentHeading = xform.forward;
		if(waypoints.Length<=0)
		{
			Debug.Log("No waypoints on "+name);
			enabled = false;
		}
		targetwaypoint = 0;
		if(rigidbody!=null)
		{
			useRigidbody = true;
			rigidmember = rigidbody;
		}
		else
		{
			useRigidbody = false;
		}
		
		pathName = waypointContainer.name;
		
		
		
	}
	
	
	public void RandomizePath()
	{
		 if(isAI)
			    {
			    	currentPathNumber = Random.Range(1,3);
			    	
			    }
	}
	
	// calculates a new heading
	protected void FixedUpdate ()
		{
			
				targetHeading = waypoints [targetwaypoint].position - xform.position;
		
			    currentHeading = Vector3.Lerp (currentHeading, targetHeading, damping * Time.deltaTime);
			    
			    
			    if(isAI)
				{
					RaycastHit hit;
					AITransformPosition = transform.position + new Vector3(0,0.1f,0);
					if(Physics.Raycast(AITransformPosition,transform.forward,out hit,4))
					{
						
								Debug.Log("Something up");
								RandomizePath();
						
						
					}
				}
				else
			   	{
			   		
			   		 if(score < 1000)
			   		 	scorePercentage = 10;
			   		 if(score <=0)
			   		 	score =0;
			   		 else if(!isGameOver)
			   		 	 score-=scorePercentage;
					scoreText.text = score.ToString();
			   	}
			   
			    			
	}
	
	// moves us along current heading
	protected void Update ()
		{
			
			
			if(pathName!= waypointContainer.name)
				{
					waypointLength = 0;
					waypointsDump = waypointContainer.GetComponentsInChildren<Transform>() as Transform[];
		
					foreach(Transform waypoint in waypointsDump)
					{
						if(waypoint != waypointContainer.transform )
						{
							waypoints[waypointLength] = waypoint.transform;
							waypointLength++;
						}	
			
					}
					
					pathName = waypointContainer.name;
		
				}

			
			
				if (isButtonPressed) {
					
					if(speed < 10.0f)
						speed +=0.01f;
					else
					 	speed = 10.0f;
				}
				else {
					if(speed >0)
						speed -=0.01f;
					else
						speed =0;
					
				}
						if (useRigidbody)
								rigidmember.velocity = currentHeading * speed;
						else
								xform.position += currentHeading * Time.deltaTime * speed;
						if (faceHeading)
								xform.LookAt (xform.position + currentHeading);
			
						
				
			
				
				
				
				if (Vector3.Distance (xform.position, waypoints [targetwaypoint].position) <= waypointRadius) {
						targetwaypoint++;
						if (targetwaypoint >= waypoints.Length) {
							targetwaypoint = 0;
						if (!loop)
							enabled = false;
					}
				}
				
				if(currentPathNumber > allPaths.Length)
				{
					currentPathNumber = allPaths.Length;
				}
				
				if(currentPathNumber < 1)
				{
					currentPathNumber = 1;
				}
				waypointContainer = allPaths[currentPathNumber-1];
				
				
								
				Debug.DrawRay(AITransformPosition,transform.forward*4,Color.red);
								
				//InvokeRepeating("RandomizePath",Random.Range(10,20),Random.Range(10,20));
				
				if(health <=0)
					Destroy(gameObject);
					
					
				
		}
	
	
	// draws red line from waypoint to waypoint
	/*public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		if(waypoints==null)
			return;
		for(int i=0;i< waypoints.Length;i++)
		{
			Vector3 pos = waypoints[i].position;
			if(i>0)
			{
				Vector3 prev = waypoints[i-1].position;
				Gizmos.DrawLine(prev,pos);
			}
		}
		
		
		
	}*/
	

		
	void OnCollisionEnter(Collision collide)
	{
		if(collide.transform.tag =="Obstacle_Explosion")
		{
			Instantiate (explosion,transform.position,transform.rotation);
			audio.volume = 1f;
			audio.PlayOneShot(clips[1]);
			Destroy(collide.gameObject);
			speed=speed/2;
			health--;
		
				switch(health)
				{
					case 4:
					health_gui.guiTexture.texture = healthTex[1];
					break;
				
					case 3:
					health_gui.guiTexture.texture = healthTex[2];
					break;
				
					case 2:
					health_gui.guiTexture.texture = healthTex[3];
					break;
					
					case 1:
					health_gui.guiTexture.texture = healthTex[4];
					break;
				
					case 0:
					health_gui.guiTexture.texture = healthTex[5];
					break;
				}
			
			//rigidmember.velocity=currentHeading * 0;
		}
		
		else if(collide.transform.tag =="Obstacle_Smoke")
		{
			Instantiate (smoke,transform.position,transform.rotation);
			audio.volume =0.2f;
			audio.PlayOneShot(clips[0]);
			Destroy(collide.gameObject);
			speed=speed/2;
			//rigidmember.velocity=currentHeading * 0;
		}
		
	}
	
	
	void OnTriggerEnter(Collider collide)
	{
		if(collide.transform.name == "StartPoint"&&!isAI)
		{
			lapCounter++;
			
			LapText.text = "Lap: "+lapCounter+"/10";
		}

	}
}