using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Interface for the heart transplant surgery
// All organs must be removed from the body, then the new heart placed inside
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 14:09

public class HeartSurgeryScript : BaseSurgeryScript
{
	// Reference to each of the organs in the scene that must be removed from the body
	public GameObject Lung_Left;
	public GameObject Lung_Right;
	public GameObject Heart_Old;
	public GameObject Heart_New;
	// Reference to the replacement heart detection trigger
	public TriggerObjectsWithinScript HeartTrigger;

	protected override void Update()
	{
		base.Update();

		// Check the HeartTrigger's TriggerObjectsWithinScript for each object, if only the new heart remains then win
		bool lung_left = HeartTrigger.Objects.Contains( Lung_Left );
		bool lung_right = HeartTrigger.Objects.Contains( Lung_Right );
		bool heart_old = HeartTrigger.Objects.Contains( Heart_Old );
		bool heart_new = HeartTrigger.Objects.Contains( Heart_New );
		if ( !lung_left && !lung_right && !heart_old && heart_new )
		{
			if ( WinTimer >= 1 )
			{
				Win();
			}
			WinTimer += Time.deltaTime * 12;
        }
		else
		{
			WinTimer = 0;
		}
	}
}
