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

	void LateUpdate()
	{
		Objects.Clear();
	}

	void OnTriggerEnter( Collider other )
	{
		AddObject( other.gameObject );
    }

	void OnTriggerStay( Collider other )
	{
		AddObject( other.gameObject );
	}

	void OnTriggerExit( Collider other )
	{
		RemoveObject( other.gameObject );
	}

	public void AddObject( GameObject obj )
	{
		if ( obj.GetComponentInParent<RigidHand>() )
		{
			obj = obj.GetComponentInParent<RigidHand>().gameObject;
		}
		Objects.Add( obj );
	}

	public void RemoveObject( GameObject obj )
	{
		if ( obj.GetComponentInParent<RigidHand>() )
		{
			obj = obj.GetComponentInParent<RigidHand>().gameObject;
		}
		Objects.Remove( obj );
	}
}
