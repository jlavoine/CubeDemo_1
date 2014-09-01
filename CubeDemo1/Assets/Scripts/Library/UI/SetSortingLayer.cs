using UnityEngine;
using System.Collections;

//////////////////////////////////////////
/// SetSortingLayer
/// Simple script that sets the sorting
/// layer of an object.
//////////////////////////////////////////

public class SetSortingLayer : MonoBehaviour {
	// layer this game object will be set to
	public string Layer;

	// order within the layer
	public int Order;

	//////////////////////////////////////////
	/// Start()
	//////////////////////////////////////////
	void Start () {
		// set the sorting layer
		if (renderer && Layer != null) {
			renderer.sortingLayerName = Layer;
			renderer.sortingOrder = Order;
		}
	}
}
