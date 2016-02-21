using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Identifier for a leech's head
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 10:41

public class LeechHeadScript : MonoBehaviour
{
	public List<GameObject> Segments;

	private GameObject Attached;

	public void PlayDrainAnimation( float ratio )
	{
		float scale = ( 1 - ratio ) / 2;

		int segnum = 0;
        foreach ( GameObject seg in Segments )
		{
			float temp = seg.transform.localScale.y;
			float plus = scale + ( Mathf.Sin( ( Time.time / 10 ) + ( segnum * 10 ) ) / 5 );
            seg.transform.localScale = new Vector3( 1 + plus, 0, 1 + plus );
			seg.transform.localScale += new Vector3( 0, temp, 0 );
			segnum++;
        }
	}

    public bool Attach( GameObject attach )
	{
		if ( Attached != null ) return false;

		GetComponent<Rigidbody>().isKinematic = true;
		transform.LookAt( attach.transform );

		Attached = attach;
		return true;
    }

	public void Detach()
	{
		GetComponent<Rigidbody>().isKinematic = false;

		// Never attach again
		//Attached = null;
    }
}
