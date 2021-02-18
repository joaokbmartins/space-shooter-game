using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField]
	private Asteroid asteroidPrefab = null;
	[SerializeField]
	private GameObject asteroidContainer = null;
	[SerializeField]
	private bool isPlayerAlive = true;
	[SerializeField]
	private int ASTEROID_LIMIT = 2;
	private int asteroidCount = 0;
	bool startSpawning = true;

	// Start is called before the first frame update
	void Start() {
		this.StartCoroutine( this.SpawnRoutine( 5f ) );
	}

	// Update is called once per frame
	void Update() {

		//caso o limite de asteroides tenha sido atingido
		//encerrar o spawn de asteroides, e emitir alerta
		//de limite atingido.

		//caso a quantidade de asteroides fique abaixo do limite
		//reiniciar rotina de spawn de asteroides, e emitir alerta
		//de quantidade normalizada.

	}

	IEnumerator SpawnRoutine( float toWaitSeconds ) {
		while ( this.GetIsPlayerAlive( ) && (this.GetAsteroidCount( ) < ASTEROID_LIMIT) ) {
			this.EnemyInstatiator( );
			Debug.Log( "Enemy spawned..." );
			if ( this.GetIsPlayerAlive( ) && (this.GetAsteroidCount( ) >= ASTEROID_LIMIT) ) {
				Debug.Log( "***Quantidade limite de asteroides atingido***Spawn de asteroides interrompido***" );
			}
			yield return new WaitForSeconds( toWaitSeconds );
		}
		startSpawning = true;
	}

	IEnumerator SpawnDelay( float secondsToDelay ) {
		yield return new WaitForSeconds( secondsToDelay );
	}

	void EnemyInstatiator() {
		float randomY = this.RandomFloat( -5f, 5f );
		Asteroid newEnemy = Instantiate( this.asteroidPrefab, new Vector3( 12f, randomY, 0f ), Quaternion.identity );
		newEnemy.transform.SetParent( this.asteroidContainer.transform );
		this.asteroidCount++;
	}

	float RandomFloat( float min, float max ) {
		return Random.Range( min, max );
	}

	public void SetIsPlayerAlive( bool isPlayerAlive ) {
		this.isPlayerAlive = isPlayerAlive;
	}

	public bool GetIsPlayerAlive() {
		return this.isPlayerAlive;
	}

	public void SetAsteroidCount( int asteroidCount ) {
		this.asteroidCount = asteroidCount;
	}

	public int GetAsteroidCount() {
		return this.asteroidCount;
	}

	public void RemoveOneAsteroid() {
		this.SetAsteroidCount( this.GetAsteroidCount( ) - 1 );

		if ( startSpawning && this.GetAsteroidCount( ) < ASTEROID_LIMIT ) {
			startSpawning = false;
			Debug.Log( "***Quantidade de asteroides normalizada***Spawn de asteroides reiniciando***" );
			this.StartCoroutine( this.SpawnDelay( 5f ) );
			this.StartCoroutine( this.SpawnRoutine( 5f ) );
		}
	}
}
