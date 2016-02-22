using UnityEngine;
using System.Collections;

// Plays an AudioSource when a Rigidbody collides with something over a defined speed
// Matthew Cormack @johnjoemcbob
// 22/02/16 - 16:22

public class CollisionAudioScript : MonoBehaviour
{
	public float MinimumSpeed;
	public float MaximumSpeed;
	public Vector2 PitchBounds = new Vector2( 1, 1 );
	public Vector2 VolumeBounds = new Vector2( 1, 1 );

	void OnCollisionEnter( Collision collision )
	{
		float speed = collision.relativeVelocity.magnitude;
		// Large enough collision to make noise
		if ( speed > MinimumSpeed )
		{
			// Clamp to range
			speed = Mathf.Clamp( speed, MinimumSpeed, MaximumSpeed );

			// Alter sound
			float speedratio = ( speed - MinimumSpeed ) / ( MaximumSpeed - MinimumSpeed );
			AudioSource audio = GetComponent<AudioSource>();
			audio.pitch = PitchBounds.x + ( ( PitchBounds.y - PitchBounds.x ) * speedratio );
			audio.volume = VolumeBounds.x + ( ( VolumeBounds.y - VolumeBounds.x ) * speedratio );

			// Play sound
			audio.Play();
		}
	}
}
