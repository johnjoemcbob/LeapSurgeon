using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Interface for the guild surgery
// Letter must be folded and placed in the evelope
// Matthew Cormack @johnjoemcbob
// 22/02/16 - 18:58

public class GuildSurgeryScript : BaseSurgeryScript
{
	// Reference to the envelope
	public GameObject Envelope;

	protected override void Update()
	{
		base.Update();

		// Check the envelope to see if it has been sent off
		EnvelopeScript[] envelopes = Envelope.GetComponentsInChildren<EnvelopeScript>();
        if ( ( envelopes[0].Won || envelopes[1].Won ) && ( ReplaceTimer == -1 ) )
		{
			Win();
		}
	}
}
