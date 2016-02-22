using UnityEngine;
using System.Collections;

// Allow some objects to saw arrows in half
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 20:37

public class SawArrowScript : MonoBehaviour
{
	// Reference to the top of the arrow
	public GameObject Arrow_Top;
	public GameObject Arrow_Bottom;

	// Enable grabbable and physics on the top of the arrow when sawed, and play audio of sawing, and enable hammer functionality
	void OnTriggerEnter( Collider other )
	{
		if ( other.GetComponent<CanSawFlagScript>() )
		{
			if ( Arrow_Top.GetComponent<Rigidbody>().isKinematic )
			{
				Arrow_Top.GetComponent<Rigidbody>().isKinematic = false;
				Arrow_Top.gameObject.layer = LayerMask.NameToLayer( "Grabbable" );
				GetComponent<ParticleSystem>().Play();
				GetComponent<AudioSource>().Play();
				Arrow_Bottom.GetComponent<HammerArrowScript>().enabled = true;
			}
		}
	}
}
