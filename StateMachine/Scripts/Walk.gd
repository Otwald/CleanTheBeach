extends "PlayerState.gd"

func get_input_direction():
	var input_direction = Vector2()
	input_direction.x = int(Input.is_action_pressed("player_right")) - int(Input.is_action_pressed("player_left"))
	return input_direction


func update_process(machine, delta):
	var input_direction = get_input_direction()
	if not input_direction:
		machine._change_state('idle')
	velocity = input_direction.normalized() * player.speed
		# velocity = player.move_and_slide(velocity, Vector2(0,-1))
	.update_process(machine, delta)
