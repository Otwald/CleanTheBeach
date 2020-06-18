extends "PlayerState.gd"

func enter(machine):
	pass

func update_process(machine, delta):
	.update_process(machine, delta)
	if Input.is_action_pressed("player_left") or Input.is_action_pressed("player_right"):
		machine._change_state('walk')
