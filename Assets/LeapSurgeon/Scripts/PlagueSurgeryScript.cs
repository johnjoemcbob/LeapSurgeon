using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Interface for the plague leech surgery
// All boils must be leeched
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 14:54

public class PlagueSurgeryScript : BaseSurgeryScript
{
	// Reference to each of the boils
	public List<PlagueBoilScript> Boils;

	protected override void Update()
	{
		base.Update();

		// Check each boil to see if it is still a problem
		bool allgone = true;
		{
			foreach ( PlagueBoilScript boil in Boils )
			{
				if ( boil.GetPuss() > 0 )
				{
					allgone = false;
					break;
				}
			}
		}
		if ( allgone )
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
