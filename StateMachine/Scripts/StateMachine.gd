extends Node
class_name StateMachine

const DEBUG = true

var states_stack = []
var current_state = null

onready var states_map = {
	'idle' : $Idle,
	'walk': $Walk,
	'jump': $Jump
}

func _ready():
	current_state = $Idle
	_change_state('idle')

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var state_name = current_state.update_process(self, delta)
	if state_name:
		_change_state(state_name)

func _change_state(state_name : String):
	"""Changes the State to the new one, uses the exit and enter function
	"""
	if DEBUG:
		print("State change to :", state_name)
	current_state.exit(self)
	if state_name == 'previous':
		states_stack.pop_front()
	else:
		states_stack.push_front(states_map[state_name])
	current_state = states_stack[0]
	current_state.enter(self)


# Route Game Loop function calls to
# current state handler method if it exists

# func _physics_process(delta):
# 	current_state.update_physics_process(self, delta)

# func _input(event):
# 	current_state.update_input(self, event)

# func _unhandled_input(event):
# 	current_state.update_unhandled_input(self, event)

# func _unhandled_key_input(event):
# 	current_state.update_unhandled_key_input(self, event)

# func _notification(what):
# 	current_state.update_notification(self, what)
