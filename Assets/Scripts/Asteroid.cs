using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	[SerializeField]
	private float speed = 4f;
	private SpawnManager spawnManager = null;

	// Start is called before the first frame update
	void Start() {
		spawnManager = GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>( );
		float randomY = this.GetRandomFloat( -5f, 5f );
		this.transform.position = new Vector3( 12f, randomY, 0f );
	}

	// Update is called once per frame
	void Update() {
		this.transform.Translate( Vector3.left * speed * Time.deltaTime );
		if ( this.transform.position.x <= -12f ) {
			float randomY = this.GetRandomFloat( -5f, 5f );
			this.transform.position = new Vector3( 12f, randomY, 0f );
		}
	}

	float GetRandomFloat( float min, float max ) {
		return Random.Range( min, max );
	}

	private void OnTriggerEnter2D( Collider2D other ) {
		switch ( other.tag ) {
			case "Laser":
				Destroy( other.transform.gameObject );
				break;
			case "Player":
				Player player = other.transform.GetComponent<Player>( );
				if ( player == null ) {
					break;
				}
				player.AbsorbEnemyDamage( GetRandomFloat( 1f, 10f ) );
				break;
		}
		Destroy( this.gameObject );
		this.DecreasseAsteroidAmountInSpawnManager( );
	}

	private void DecreasseAsteroidAmountInSpawnManager() {
		spawnManager.RemoveOneAsteroid( );
	}

}
