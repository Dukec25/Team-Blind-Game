#pragma strict

static function aPressed() : boolean {
	return Input.GetKey(KeyCode.JoystickButton0);
}

static function bPressed() : boolean {
	return Input.GetKey(KeyCode.JoystickButton1);
}

static function xPressed() : boolean {
	return Input.GetKey(KeyCode.JoystickButton2);
}

static function yPressed() : boolean {
	return Input.GetKey(KeyCode.JoystickButton3);
}

static function startPressed() : boolean {
	return Input.GetKeyUp(KeyCode.JoystickButton7);
}

static function selectPressed() : boolean {
    return Input.GetKeyUp(KeyCode.JoystickButton6);
}

static function leftBumperPressed() : boolean {
	return Input.GetKey(KeyCode.JoystickButton4);
}

static function rightBumperPressed() : boolean {
	return Input.GetKey(KeyCode.JoystickButton5);
}

static function leftJoystickDown() : boolean {
	return Input.GetAxis("Vertical") < 0;
}

static function leftJoystickUp() : boolean {
	return Input.GetAxis("Vertical") > 0;
}

static function leftJoystickLeft() : boolean {
	return Input.GetAxis("Horizontal") < 0;
}

static function leftJoystickRight() : boolean {
	return Input.GetAxis("Horizontal") > 0;
}