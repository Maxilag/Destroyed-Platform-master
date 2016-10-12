using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class move : MonoBehaviour
{
    private Transform myTransform;            // this transform

    private float destinationDistance;         // The distance between myTransform and destinationPosition

	private Vector3 nouvellePosition;

	int mask;

    public float moveSpeed;      // The Speed the character will move

    public float speed;

	public Vector3 centreCamera;

	public OVRCameraRig camera1;

	public GameObject cube;

	public double chrono;

	public AudioClip source;
    void Start()
    {
        myTransform = transform;                     // sets myTransform to this GameObject.transform
		nouvellePosition = myTransform.position;         // prevents myTransform reset*/
		mask = (1 << 8);
		centreCamera = new Vector3(Screen.width/2f,Screen.height/2f, camera1.transform.forward.z);
		source = GetComponent<AudioClip> ();

    }


    // Update is called once per frame
    void Update()
	{
		
		chrono += Time.deltaTime;
		destinationDistance = Vector3.Distance (nouvellePosition, myTransform.position);
		cube = GameObject.Find ("GameObject");
		cube.transform.Rotate (Vector3.down * 700f * Time.deltaTime);
		cube.transform.position = nouvellePosition;

		if (destinationDistance < .5f) {      // To prevent shakin behavior when near destination
			moveSpeed = 0;
			chrono = 0;
		} else if (destinationDistance > .5f) {         // To Reset Speed to default
			moveSpeed = speed;
		}

	
		/*Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Mathf.Infinity, mask)) {
			//Vector3 targetPoint = ray.GetPoint(hit);
			//Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			//myTransform.rotation = targetRotation;

			Vector3 targetPoint = hit.point;
			nouvellePosition = hit.point;
			//Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			Quaternion targetRotation = Quaternion.LookRotation (new Vector3 (targetPoint.x - transform.position.x, targetPoint.y + (transform.localScale.y / 2) - transform.position.y, targetPoint.z - transform.position.z));
			myTransform.rotation = targetRotation;
		}*/

		Plane playerPlane = new Plane(Vector3.up, myTransform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hit = 0.0f;

		if (playerPlane.Raycast(ray, out hit))
		{
			
			Vector3 targetPoint = ray.GetPoint(hit);
			nouvellePosition = ray.GetPoint (hit);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			myTransform.rotation = targetRotation;

		}

		Debug.Log (centreCamera);

		// To prevent code from running if not needed
		if (destinationDistance > .5f) {
			transform.position = Vector3.MoveTowards (myTransform.position, nouvellePosition, moveSpeed * Time.deltaTime);
			//Debug.Log (transform.position.x + "  " + transform.position.y + "  " + transform.position.z);
		}

    }
}