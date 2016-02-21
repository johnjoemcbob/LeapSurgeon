using UnityEngine;
using System.Collections;

// Handles plague boil logic, draining them when leeches are applied
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 10:37

public class PlagueBoilScript : MonoBehaviour
{
	public float MaxPuss = 10;
	public float DrainRate = 1;

	private float Puss;
	private LeechHeadScript Attached;

	void Start()
	{
		Puss = MaxPuss;
	}

	void Update()
	{
		if ( Attached )
		{
			Puss = Mathf.Clamp( Puss - ( DrainRate * MaxPuss * Time.deltaTime ), 0, MaxPuss );
			transform.localScale = new Vector3( 1, 1, 1 ) * ( Puss / MaxPuss );
			if ( Puss == 0 )
			{
				Attached.Detach();
				Attached = null;
			}
			else
			{
				Attached.PlayDrainAnimation( Puss / MaxPuss );
			}
		}
	}

	void OnTriggerEnter( Collider other )
	{
		LeechHeadScript leech = other.GetComponent<LeechHeadScript>();
		if ( leech )
		{
			bool attached = leech.Attach( gameObject );
			if ( attached )
			{
				Attached = leech;
			}
        }
	}

	public float GetPuss()
	{
		return Puss;
	}
}
