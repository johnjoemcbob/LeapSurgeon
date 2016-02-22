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
	public Transform DefaultLookAt;
	public GameObject Controller;

	private Vector3 BasePosition;

	void Start()
	{
		BasePosition = transform.position;
	}

	void Update()
	{
		BasePosition = DefaultLookAt.position;

        Vector3 handcenter = RotateVector( HandInfluence( Hand_Left ) ) + RotateVector( HandInfluence( Hand_Right ) );

		// Position
		Vector3 pos = BasePosition + RotateVector( HandInfluence( Hand_Left ) * Influence ) + RotateVector( HandInfluence( Hand_Right ) * Influence );
		{
			// As it moves further into the scene the camera should also move downwards to face the surgery area
			float extraz = Mathf.Clamp( ( handcenter.z + 0.75f ) * 0.5f, 0, 1 );
			pos += transform.parent.forward * extraz;
			pos.y -= 0.5f;
			handcenter.y -= extraz * 2;
		}
		float mag = Vector3.Distance( transform.position, pos ) * 1;
		transform.position = Vector3.Lerp( transform.position, pos, Time.deltaTime * mag );

		// Rotation
		transform.rotation = Quaternion.Lerp( transform.rotation, DefaultLookAt.rotation, Time.deltaTime );
	}

	private Vector3 HandInfluence( GameObject hand )
	{
		Vector3 influence = Vector3.zero;
		{
			Hand leaphand = (Hand) hand.GetComponent<RiggedHand>().GetLeapHand();
			if ( hand.activeInHierarchy && ( leaphand != null ) )
			{
				// Don't influence the camera if they are offscreen
				Vector pos = leaphand.PalmPosition;
				if ( ( pos.y > -1.5f ) && ( pos.z > -1.5f ) )
				{
					influence.x = pos.x;
					influence.y = pos.y;
					influence.z = pos.z;
					influence -= Controller.transform.position;
                }
			}
        }
        return influence;
    }

	private Vector3 RotateVector( Vector3 vec )
	{
		Vector3 output = Vector3.zero;
		{
			output += vec;
			output.Normalize();
		}
		return output;
	}
}
