using UnityEngine;
using System.Collections;

// When the leech is picked up, it should face the camera
// Matthew Cormack @johnjoemcbob
// 20/02/16 - 20:32

public class LeechGrabScript : GrabbableObject
{
	public GameObject Head;

	//public void Update()
	//{
	//	if ( grabbed_ )
	//	{
	//		// Find the direction from the leech to the camera
	//		Transform cam = Camera.main.transform;
	//           Vector3 direction = ( Head.transform.position - cam.position ).normalized;

	//		// Move to look at player
	//		Head.transform.LookAt( cam.transform );
	//		Vector3 target = transform.position - ( direction * 0.7f );
	//		//Head.transform.position = transform.position - ( direction * 1 );
	//		//Head.transform.position = Vector3.Lerp( Head.transform.position, transform.position - ( direction * 1 ), Time.deltaTime * 10 );
	//		Head.GetComponent<Rigidbody>().velocity = ( ( target - Head.transform.position ) ) * 15;
	//       }
	//}

	public override void OnGrab()
	{
		base.OnGrab();

		GetComponent<AudioSource>().Play();
	}
}
