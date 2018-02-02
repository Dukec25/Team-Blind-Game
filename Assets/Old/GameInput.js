#pragma strict

interface GameControlSource {
	function checkLeft() : boolean;
	function checkRight() : boolean;
	function checkJump() : boolean;
}

class ControllerGameControlSource implements GameControlSource {
	function checkLeft() : boolean { return ControllerInput.leftJoystickLeft(); }
	function checkRight() : boolean { return ControllerInput.leftJoystickRight(); }
	function checkJump() : boolean { return ControllerInput.bPressed(); }
}

class KeyboardGameControlSource implements GameControlSource {
	// TODO: Implement this
	function checkLeft() : boolean { return KeyboardInput.leftPressed(); }
	function checkRight() : boolean { return KeyboardInput.rightPressed(); }
	function checkJump() : boolean { return KeyboardInput.spacePressed(); }
}

static class ControlSourceFactory {
	var INPUT_TYPE = "Keyboard";

	function createGameControlSource() : GameControlSource {
		if (INPUT_TYPE === "Controller") return new ControllerGameControlSource();
		else if (INPUT_TYPE === "Keyboard") return new KeyboardGameControlSource();
		return new KeyboardGameControlSource(); // default
	}
}