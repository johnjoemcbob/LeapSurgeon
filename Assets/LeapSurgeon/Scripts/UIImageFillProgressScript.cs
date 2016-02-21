using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Lerp the progress of a UI Image fill
// Matthew Cormack @johnjoemcbob
// 21/02/16 - 13:46

public class UIImageFillProgressScript : MonoBehaviour
{
	public float FillSpeed = 1;

	void Update()
	{
		GetComponent<Image>().fillAmount = Mathf.Clamp( GetComponent<Image>().fillAmount + ( Time.deltaTime * FillSpeed ), 0, 1 );
	}
}
