using UnityEngine;
using System.Collections;

// When the cover letter collides with this, play the sending away animation
// Matthew Cormack @johnjoemcbob
// 22/02/16 - 18:15

public class EnvelopeScript : MonoBehaviour
{
	public bool Won = false;

	private GameObject CoverLetter;
	private float NextStageTime;
	private int Stage = 0;

	void Update()
	{
		if ( !CoverLetter ) return;

		// Move the cover letter into the envelope
		Transform envelope = transform.parent.GetChild( 1 );
        foreach ( Transform letter in CoverLetter.transform.GetComponentInChildren<Transform>() )
		{
			letter.position = Vector3.Lerp( letter.position, envelope.position, Time.deltaTime );
			//letter.rotation = Quaternion.Lerp( letter.rotation, envelope.rotation, Time.deltaTime );
			foreach ( Renderer renderer in letter.GetComponentsInChildren<Renderer>() )
			{
				renderer.material.color = Color.Lerp( renderer.material.color, new Color( 1, 1, 1, 0 ), Time.deltaTime * 2 );
			}
        }

		// Then close the envelope
		if ( Stage > 0 )
		{
			Transform envelopetop = transform.parent.GetChild( 0 );

			// Initialize time taken for this stage
			if ( ( Stage == 1 ) && ( NextStageTime == -1 ) )
			{
				NextStageTime = Time.time + 0.5f;
				envelopetop.GetComponent<Collider>().isTrigger = true;
            }

            envelopetop.localRotation = Quaternion.Lerp( envelopetop.localRotation, Quaternion.Euler( new Vector3( -178, 0, 0 ) ), Time.deltaTime );
		}

		// Then turn
		if ( Stage > 1 )
		{
			// Initialize time taken for this stage
			if ( ( Stage == 2 ) && ( NextStageTime == -1 ) )
			{
				NextStageTime = Time.time + 8;
			}

			transform.parent.localPosition = Vector3.Lerp( transform.parent.localPosition, new Vector3( 0, 0.75f, -1.5f ), Time.deltaTime * 2 );
			transform.parent.localRotation = Quaternion.Lerp( transform.parent.localRotation, Quaternion.Euler( new Vector3( -40, 0, 180 ) ), Time.deltaTime / 2 );
		}

		// Then fly off
		if ( Stage > 2 )
		{
			// Initialize time taken for this stage
			if ( ( Stage == 3 ) && ( NextStageTime == -1 ) )
			{
				NextStageTime = Time.time + 1;
			}

			transform.parent.localPosition = Vector3.Lerp( transform.parent.localPosition, transform.parent.localPosition + transform.parent.forward * 4, Time.deltaTime * 2 );
		}

		// Then flag as won
		if ( Stage > 3 )
		{
			Won = true;
		}

		// Count time
		if ( NextStageTime <= Time.time )
		{
			Stage++;
			NextStageTime = -1;
        }
    }

	void OnCollisionEnter( Collision collision )
	{
		if ( CoverLetter ) return;

		Transform parent = collision.collider.transform.parent;
        if ( parent )
		{
			if ( parent.gameObject.name == "CoverLetter" )
			{
				CoverLetter = parent.gameObject;
				foreach ( Transform letter in CoverLetter.transform.GetComponentInChildren<Transform>() )
				{
					Destroy( letter.GetComponent<Joint>() );
					Destroy( letter.GetComponent<Rigidbody>() );
					foreach ( Collider col in letter.GetComponents<Collider>() )
					{
						Destroy( col );
					}
				}
				NextStageTime = Time.time + 0.5f;
			}
		}
	}
}
