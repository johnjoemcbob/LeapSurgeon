using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Interface for the battle (arrow removal) surgery
// All arrows must be sawed then hammered through
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 22:55

public class BattleSurgeryScript : BaseSurgeryScript
{
	// Reference to each of the arrows
	public List<HammerArrowScript> Arrows;

	protected override void Update()
	{
		base.Update();

		// Check each boil to see if it is still a problem
		bool allgone = true;
		{
			foreach ( HammerArrowScript arrow in Arrows )
			{
				if ( !arrow.Pushed )
				{
					allgone = false;
					break;
				}
			}
		}
		if ( allgone && ( ReplaceTimer == -1 ) )
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
