using UnityEngine;
using System.Collections;
using Leap;

// Chooses the mid point between both hands and smooths the camera to focus there
// Matthew Cormack @johnjoemcbob
// 19/02/16 - 17:16

public class CameraFollowHandsScript : MonoBehaviour
{
	public float Influence = 0.5f;
    public GameObject Hand_Left;
	public GameObject Hand_Right;

	private Vector3 BasePosition;

	void Start()
	{
		BasePosition = transform.position;
	}

	void Update()
	{
		// Position
		Vector3 pos = BasePosition + ( HandInfluence( Hand_Left ) * Influence ) + ( HandInfluence( Hand_Right ) * Influence );
		float mag = Vector3.Distance( transform.position, pos ) * 1;
		transform.position = Vector3.Lerp( transform.position, pos, Time.deltaTime * mag );

		// Rotation
		Vector3 handcenter = HandInfluence( Hand_Left ) + HandInfluence( Hand_Right );
        Vector3 lookat = -( transform.position - handcenter );
		{
            lookat.Normalize();
		}
        Quaternion rot = Quaternion.LookRotation( lookat );
		transform.rotation = Quaternion.Lerp( transform.rotation, rot, Time.deltaTime );
	}

	private Vector3 HandInfluence( GameObject hand )
	{
		Vector3 influence = Vector3.zero;
		{
			Hand leaphand = (Hand) hand.GetComponent<CapsuleHand>().GetLeapHand();
            if ( hand.activeInHierarchy && ( leaphand != null ) )
			{
				// Don't influence the camera if they are offscreen
				Vector pos = leaphand.PalmPosition;
				if ( ( pos.y > -0.5f ) && ( pos.z > -1.5f ) )
				{
					influence.x = pos.x;
					influence.y = pos.y;
					influence.z = pos.z;
				}
			}
		}
        return influence;
    }
}
