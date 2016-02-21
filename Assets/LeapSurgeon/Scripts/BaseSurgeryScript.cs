using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Base interface for individual surgery operations (win conditions, etc)
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 12:28

public class BaseSurgeryScript : MonoBehaviour
{
	public GameObject CurrentDynamic;
	public BaseSurgeryScript NextSurgery;
	public GameObject NextDynamic;
	public LeapControllerWorkstationScript WorkstationSwitcher;
	public Image Image_ScoreOut;
	public GrabbingHand Hand_Left;
	public GrabbingHand Hand_Right;

	protected float ReplaceTimer = -1;
	protected float WinTimer = 0;

	protected virtual void Update()
	{
		if ( ( ReplaceTimer != -1 ) && ( ReplaceTimer <= Time.time ) )
		{
			// Replace surgery now
			gameObject.SetActive( false );
			CurrentDynamic.SetActive( false );
			NextSurgery.gameObject.SetActive( true );
			NextDynamic.SetActive( true );

			// Start filling the scoreout through the text
			Image_ScoreOut.GetComponent<UIImageFillProgressScript>().enabled = true;

			// Only do it once
			ReplaceTimer = -1;

			// Reenable the hands
			Hand_Left.enabled = true;
			Hand_Right.enabled = true;
		}
	}

	protected void Win()
	{
		ChangeSurgery();
	}

	// Return to menu book, Switch to 'NextSurgery', Tick this surgery off
	protected void ChangeSurgery()
	{
		// Move back to the book (right hand workstation)
		WorkstationSwitcher.MoveWorkstation( 1 );

		// Replace surgery after a timer
		ReplaceTimer = Time.time + 0.5f;

		// Drop any objects being held
		Hand_Left.enabled = false;
		Hand_Right.enabled = false;
	}
}
