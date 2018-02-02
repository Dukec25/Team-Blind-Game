#pragma strict
import UnityEngine.Audio;
import UnityEngine.SceneManagement;

// Constants set in Unity Component
var DRONE: AudioSource; // The pink noise in the background
var MIXER: AudioMixer;
var SPEED: int; // Left to right speed
var WALL_DISTANCE: int; // Coordinates of the left wall
var JUMPSPEED: int; // Speed at which the character rises and falls
var JUMPTIME: int; // Length of time for which the character rises and falls
var HANGTIME: int; // Length of time when the character hovers in the air
var PITCH_SCALE: int; // The ratio at which the character's height scales to the pitch

// Variables
var input: GameControlSource = ControlSourceFactory.createGameControlSource();
var jumping;

function Start () {
	jumping = false;
}

function Update () {
	Debug.Log( transform.position.x );
	Debug.Log( transform.position.y );
	// Move left or right
	if ( input.checkLeft() && canMoveLeft() ) {
		transform.Translate( Vector3(-1,0,0) * Time.deltaTime * SPEED );
	} else if ( input.checkRight() && canMoveRight() ) {
		transform.Translate( Vector3(1,0,0) * Time.deltaTime * SPEED );
	}

	// Initiate a jump
	if ( input.checkJump() && canJump() ) {
		jumping = true;
		StartCoroutine("handleJumping");
	}

	MIXER.SetFloat( "PlayerPitch", scalePlayerPitch() );
	DRONE.panStereo = scalePlayerStereo();
}

function canMoveLeft () {
	return transform.position.x > -WALL_DISTANCE;
}

function canMoveRight () {
	return transform.position.x < WALL_DISTANCE;
}

function canJump () {
	return !jumping;
}

function canMoveDown () {
	return transform.position.y > 0;
}

function handleJumping () {
	for ( var i = 0; i < JUMPTIME; i++ ) {
		transform.Translate( Vector3(0,1,0) * Time.deltaTime * JUMPSPEED );
		yield;
	}

	for ( i = 0; i < HANGTIME; i++ ) {
		yield;
	}

	for ( ;; ) {
		transform.Translate( Vector3(0,-1,0) * Time.deltaTime * JUMPSPEED );
		if ( !canMoveDown() ) break;
		yield;
	}

	jumping = false;
}

function scalePlayerPitch () {
	return transform.position.y / PITCH_SCALE + 1;
}

function scalePlayerStereo () {
	return transform.position.x / WALL_DISTANCE / 1.1;
}