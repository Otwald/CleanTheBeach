extends "res://Scripts/State.gd"
class_name PlayerState


var player
var velocity = Vector2()

# Called when the node enters the scene tree for the first time.
func _ready():
	player = get_parent().get_parent()

func _handle_gravity():
	velocity.y = 0
	velocity.y += player.gravity
	print(velocity)
	velocity = player.move_and_slide(velocity)

func update_process(machine, delta):
	_handle_gravity()

