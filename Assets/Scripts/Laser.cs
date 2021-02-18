using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	[SerializeField]
	private float _speed = 5f;

	// Start is called before the first frame update
	void Start() {
		this.transform.eulerAngles = new Vector3( 0f, 0f, -90f );
	}

	// Update is called once per frame
	void Update() {
		this.MakeMovement( );
		this.DestroyLaser( );
	}

	void DestroyLaser() {
		if ( this.transform.position.x >= 11f ) {
			Destroy( this.gameObject );
		}
	}

	void MakeMovement() {
		this.transform.Translate( new Vector3( 0f, 5f, 0f ) * _speed * Time.deltaTime );
	}

}
