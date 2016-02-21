using UnityEngine;
using System.Collections;

// Allow some objects to hammer arrows through the body
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 20:37

public class HammerArrowScript : MonoBehaviour
{
	public bool Pushed = false;

	void Start()
	{
		enabled = false;
	}

	// Push forward through the body, clamp to distances
	void OnTriggerEnter( Collider other )
	{
		if ( other.GetComponent<CanHammerFlagScript>() )
		{
			if ( !Pushed )
			{
				transform.localPosition -= transform.forward * 0.5f;
				Pushed = true;
            }
		}
	}
}
