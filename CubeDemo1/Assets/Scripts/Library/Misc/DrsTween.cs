using UnityEngine;
using System.Collections;

//////////////////////////////////////////
/// DrsTween
/// Purposefully obfuscating the tween
/// plugin in case we need to change it 
/// at some point.
//////////////////////////////////////////

public class DrsTween : MonoBehaviour {

	//////////////////////////////////////////
	/// MoveByLocal()
	/// Tweens the incoming gameobject by a
	/// certain vector.
	//////////////////////////////////////////
	public static void MoveByLocal( GameObject i_go, Vector3 i_vBy, float i_fTime, Hashtable i_hashOptions = null ) {
		// calculate the final vector
		Vector3 vFinal = i_go.transform.localPosition + i_vBy;

		// hashtable may have additional options, or not
		if (i_hashOptions == null)
			i_hashOptions = new Hashtable();

		// do the tween
		LeanTween.moveLocal(i_go, vFinal, i_fTime, i_hashOptions);
	}

	//////////////////////////////////////////
	/// MoveToLocal()
	/// Tweens the incoming gameobject to a
	/// certain position.
	//////////////////////////////////////////
	public static void MoveToLocal( GameObject i_go, Vector3 i_vTo, float i_fTime, Hashtable i_hashOptions = null ) {
		// hashtable may have additional options, or not
		if (i_hashOptions == null)
			i_hashOptions = new Hashtable();
		
		// do the tween
		LeanTween.moveLocal(i_go, i_vTo, i_fTime, i_hashOptions);
	}

	//////////////////////////////////////////
	/// Stop()
	/// Stops any tweens on the incoming game
	/// object.
	//////////////////////////////////////////
	public static void Stop( GameObject i_go ) {
		LeanTween.cancel( i_go );
	}
}
