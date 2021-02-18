using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private float speed = 6f;
	[SerializeField]
	private Laser laser = null;
	[SerializeField]
	private float healthPoints = 100f;
	[SerializeField]
	private float fireRate = .5f;
	private float nextFire = -1f;
	private SpawnManager spawnManager = null;

	// Start is called before the first frame update
	void Start() {
		this.spawnManager = GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>( );
		if ( this.spawnManager == null ) {
			Debug.LogError( "The spawnManager is NULL." );
		}
		this.transform.position = new Vector3( -7f, 0f, 0f );
		this.transform.eulerAngles = new Vector3( 0f, 0f, -90f );

	}

	// Update is called once per frame
	void Update() {
		this.MakeMovement( );

		if ( this.FireEnabled( ) ) {
			this.FireLaser( );
		}
	}

	bool FireEnabled() {
		if ( !Input.GetKeyDown( KeyCode.Space ) || Time.time <= this.nextFire ) {
			return false;
		}
		return true;
	}

	void FireLaser() {
		Instantiate( laser, this.transform.position + new Vector3( 1.044f, 0f, 0f ), Quaternion.identity );
		this.nextFire = Time.time + this.fireRate;
	}

	private void MakeMovement() {
		float horizontalInput = Input.GetAxis( "Vertical" );
		float verticalInput = Input.GetAxis( "Horizontal" );
		Vector3 direction = new Vector3( -horizontalInput, verticalInput, 0f );
		this.transform.Translate( direction * speed * Time.deltaTime );

		float horizontalPos = this.transform.position.x;
		float verticalPos = this.transform.position.y;
		float horizontalBounds = Mathf.Clamp( this.transform.position.x, -9.42f, 9.454f );
		this.transform.position = new Vector3( horizontalBounds, verticalPos, 0f );
		if ( verticalPos <= -6f ) {
			this.transform.position = new Vector3( horizontalPos, 6f, 0f );
		} else if ( verticalPos >= 6f ) {
			this.transform.position = new Vector3( horizontalPos, -6f, 0f );
		}
	}

	public void AbsorbEnemyDamage( float damageAmount ) {
		this.SetHealthPoints( this.GetHealthPoints( ) - damageAmount );
		if ( this.GetHealthPoints( ) <= 0f ) {
			if ( this.spawnManager != null ) {
				this.spawnManager.SetIsPlayerAlive( false );
			}
			Destroy( this.gameObject );
			Debug.LogError( "***GAME OVER***" );
		}
	}

	public float GetHealthPoints() {
		return this.healthPoints;
	}

	public void SetHealthPoints( float hp ) {
		this.healthPoints = hp;
	}

}
