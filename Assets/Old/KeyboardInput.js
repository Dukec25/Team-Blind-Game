#pragma strict

static function spacePressed() : boolean {
	return Input.GetKey(KeyCode.Space);
}

static function leftPressed() : boolean {
	return Input.GetKey(KeyCode.LeftArrow);
}

static function rightPressed() : boolean {
	return Input.GetKey(KeyCode.RightArrow);
}
