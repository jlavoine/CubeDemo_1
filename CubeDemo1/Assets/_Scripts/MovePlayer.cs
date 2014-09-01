using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public float Speed;
	public Camera MainCam;

	private Animator m_animator;

	// Use this for initialization
	void Start () {
		m_animator = GetComponentInChildren<Animator>();
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
