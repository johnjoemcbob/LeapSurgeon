using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Stores the objects currently within the trigger zone
// Special functionality for finding rigidbody hands
// Matthew Cormack @johnjoemcbob
// 19/02/16 - 21:23

public class TriggerObjectsWithinScript : MonoBehaviour
{
	public List<GameObject> Objects = new List<GameObject>();

	void OnTriggerEnter( Collider other )
	{
		GameObject obj = other.gameObject;
		{
			if ( obj.GetComponentInParent<RigidHand>() )
			{
				obj = obj.GetComponentInParent<RigidHand>().gameObject;
			}
		}
        Objects.Add( obj );
    }

	void OnTriggerExit( Collider other )
	{
		GameObject obj = other.gameObject;
		{
			if ( obj.GetComponentInParent<RigidHand>() )
			{
				obj = obj.GetComponentInParent<RigidHand>().gameObject;
			}
		}
		Objects.Remove( obj );
	}
}
