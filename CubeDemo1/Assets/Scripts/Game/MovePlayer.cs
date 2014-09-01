using UnityEngine;
using System.Collections;

// commenting this to test git hub

public class MovePlayer : MonoBehaviour {

	public float Speed;
	public Camera MainCam;

	// animator for this cube
	private Animator m_animator;

	// which direction is the cube currently showing to the camera?
	private Faces m_eFaceShowing;

	// Use this for initialization
	void Start () {
		// get the animator for this cube
		m_animator = GetComponentInChildren<Animator>();

		// default showing is front
		m_eFaceShowing = Faces.Front;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vDirection = Vector3.zero;
		if ( Input.GetKey( KeyCode.RightArrow ) )
			vDirection = Vector3.right;
		else if ( Input.GetKey( KeyCode.LeftArrow ) )
			vDirection = Vector3.left;

		if ( vDirection != Vector3.zero ) {
			m_animator.SetBool( "ToWalk", true );
			Vector3 vMove = vDirection * Time.deltaTime * Speed;
			transform.Translate( vMove );
			MainCam.transform.Translate( vMove );
		}
		else {
			if ( m_animator.GetCurrentAnimatorStateInfo(0).IsName( "Walk" ) )
				m_animator.SetBool( "ToWalk", false );
		}
	}
}
