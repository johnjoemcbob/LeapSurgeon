﻿using UnityEngine;
using System.Collections;
using Leap;

// Allows the Leap Motion Controller scene asset to be moved between the three workstations
// (left; supplies, middle; patient, right; menu)
// Matthew Cormack @johnjoemcbob
// 19/02/16 - 21:23

public class LeapControllerWorkstationScript : MonoBehaviour
{
	public float AngleChange = 50;
	public RiggedHand Hand_Left;
	public RiggedHand Hand_Right;
	public RigidHand Hand_Physics_Left;
	public RigidHand Hand_Physics_Right;
	public TriggerObjectsWithinScript Bound_Left;
	public TriggerObjectsWithinScript Bound_Right;
	public Transform Table_Left;
	public Transform Table_Right;

	private int CurrentWorkstation = 0;
	private float LerpAngle = 0;
	private Vector3 LerpPosition = Vector3.zero;
	private float NextMove = 0;
    private bool[][] HandWorkstationMoved = new bool[][] { new bool[2], new bool[2], new bool[2] };
	private bool Setup = false;

	void Update()
	{
		// Check for workstation movement on each hand
		UpdateHand( Hand_Left, Hand_Physics_Left, 0 );
		UpdateHand( Hand_Right, Hand_Physics_Right, 1 );

		// Lerp the rotation
		Quaternion rot;
		{
			rot = Quaternion.AngleAxis( LerpAngle, transform.up );
		}
		transform.rotation = Quaternion.Lerp( transform.rotation, rot, Time.deltaTime * 5 );

		// Lerp the position
		transform.position = Vector3.Lerp( transform.position, LerpPosition, Time.deltaTime * 5 );

		if ( !Setup )
		{
			MoveWorkstation( 1 );
			LerpPosition = new Vector3( 3.4f, 0, 0.85f );
			transform.position = LerpPosition;
			LerpAngle = AngleChange;
            transform.localEulerAngles = new Vector3( 0, AngleChange, 0 );
			Setup = true;
		}
	}

	private void UpdateHand( RiggedHand scenehand, RigidHand physicshand, int handid )
	{
		Hand hand = scenehand.GetLeapHand();
		if ( hand == null ) return;

		// Check for workstation movement on each bound of this hand
		UpdateHandBound( scenehand, physicshand, Bound_Left, handid, -1 );
		UpdateHandBound( scenehand, physicshand, Bound_Right, handid, 1 );
	}

	private void UpdateHandBound( RiggedHand scenehand, RigidHand physicshand, TriggerObjectsWithinScript bound, int handid, int offset )
	{
		Hand hand = scenehand.GetLeapHand();

		// Hand is within bounds of workstation movement
		if ( bound.Objects.Contains( physicshand.gameObject ) && physicshand.gameObject.activeInHierarchy )
		{
			// Ensure it hasn't already moved this gesture
			if ( ( !HandWorkstationMoved[offset + 1][handid] ) && ( NextMove <= Time.time ) )
			{
				// Hand is moving fast enough to move to another workstation
				//float speed = 0.4f * offset;
				//if (
				//	( ( offset < 0 ) && ( hand.PalmVelocity.x < speed ) ) ||
				//	( ( offset > 0 ) && ( hand.PalmVelocity.x > speed ) )
				//)
				{
					MoveWorkstation( offset );
					HandWorkstationMoved[offset + 1][handid] = true;
					NextMove = Time.time + 0.5f;
                }
			}
        }
		else if ( HandWorkstationMoved[offset + 1][handid] )
		{
			HandWorkstationMoved[offset + 1][handid] = false;
			bound.RemoveObject( physicshand.gameObject );
        }
	}

	public void MoveWorkstation( int offset )
	{
		// Don't move if already at the limit of that direction
		if ( CurrentWorkstation == offset ) return;

		MoveToWorkstation( CurrentWorkstation + offset );
	}

	public void MoveToWorkstation( int station )
	{
		// Don't move if already at the station
		if ( CurrentWorkstation == station ) return;

		// Rotate to new workstation
		LerpAngle = AngleChange * station;

		// Update workstation
		CurrentWorkstation = station;

		// Translate if one of the side tables
		LerpPosition = Vector3.zero;
		if ( CurrentWorkstation != 0 )
		{
			Transform towards = Table_Left;
			{
				if ( CurrentWorkstation == 1 )
				{
					towards = Table_Right;
				}
			}
			LerpPosition = ( towards.position - LerpPosition ) * 0.85f;
			LerpPosition.y = 0;
		}
	}
}
