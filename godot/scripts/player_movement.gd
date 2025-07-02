extends CharacterBody2D

var SPEED: float = 120.0
@onready var animated_sprite: AnimatedSprite2D = $AnimatedSprite2D
var input_direction = Vector2()
var weaponState: int = 0
@onready var dungeon: Node2D = $"../Dungeon"

func _physics_process(_delta):
	SPEED = PlayerStats.player_speed
	input_direction = get_input()
	if Input.is_action_pressed("Move"):
		play_animations(input_direction)
	else:
		play_animations(Vector2(0,0))
	move_and_slide()

func _on_player_manager_current_weapon(weaponName: Variant) -> void:
	weaponState = weaponName

func get_input():
	input_direction = Input.get_vector("Move_Left", "Move_Right", "Move_Up", "Move_Down")
	velocity = input_direction * SPEED
	return input_direction
	
func play_animations(direction: Vector2):
	match weaponState:
		0:
			play_animations_NoWeapon(direction)
		1:
			play_animations_Guitar(direction)
		2:
			play_animations_Mic(direction)

func play_animations_NoWeapon(direction: Vector2):
	if direction == Vector2(0,0):
		animated_sprite.play("Idle_Down")
	elif direction == Vector2(1,0):
		animated_sprite.flip_h = false
		animated_sprite.play("Run_Right")
	elif direction == Vector2(-1,0):
		animated_sprite.flip_h = true
		animated_sprite.play("Run_Right")
	elif direction == Vector2(0,1):
		animated_sprite.play("Run_Down")
	elif direction == Vector2(0,-1):
		animated_sprite.play("Run_Up")

func play_animations_Guitar(direction: Vector2):
	if direction == Vector2(0,0):
		animated_sprite.play("Idle_Down_Guitar")
	elif direction == Vector2(1,0):
		animated_sprite.flip_h = false
		animated_sprite.play("Run_Right_Guitar")
	elif direction == Vector2(-1,0):
		animated_sprite.flip_h = true
		animated_sprite.play("Run_Right_Guitar")
	elif direction == Vector2(0,1):
		animated_sprite.play("Run_Down_Guitar")
	elif direction == Vector2(0,-1):
		animated_sprite.play("Run_Up_Guitar")

func play_animations_Mic(direction: Vector2):
	if direction == Vector2(0,0):
		animated_sprite.play("Idle_Down_Mic")
	elif direction == Vector2(1,0):
		animated_sprite.flip_h = false
		animated_sprite.play("Run_Right_Mic")
	elif direction == Vector2(-1,0):
		animated_sprite.flip_h = true
		animated_sprite.play("Run_Right_Mic")
	elif direction == Vector2(0,1):
		animated_sprite.play("Run_Down_Mic")
	elif direction == Vector2(0,-1):
		animated_sprite.play("Run_Up_Mic")


func _on_dungeon_start_room(startPos: Vector2i) -> void:
	global_position.x = startPos.x
	global_position.y = startPos.y
