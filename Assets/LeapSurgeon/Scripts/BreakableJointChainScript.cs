using UnityEngine;
using System.Collections;

// Some objects are immovable until certain others have been removed
// (i.e. the heart is behind the lungs, and the lungs behind the heard)
// Matthew Cormack @johnjoemcbob
// 20/02/16 - 18:12

public class BreakableJointChainScript : MonoBehaviour
{
	public Joint Immovable;
	public Joint Dependent;

	public float BreakForce = 50;

	void Start()
	{
		Immovable.gameObject.layer = LayerMask.NameToLayer( "Default" );
	}

	void Update()
	{
		if ( !Immovable )
		{
			Destroy( this );
			return;
		}
		if ( !Dependent )
		{
			Immovable.breakForce = BreakForce;
			Immovable.gameObject.layer = LayerMask.NameToLayer( "Grabbable" );
        }
    }
}
