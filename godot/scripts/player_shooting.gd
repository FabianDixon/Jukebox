extends Node2D
var bullet_path = preload("res://scenes/bullet.tscn")
var weaponState: int = 0
var can_fire = true
@onready var fire_rate = $FireRate
@onready var player_manager: Node = $"../PlayerManager"
@onready var player: CharacterBody2D = $".."

func _physics_process(_delta: float) -> void:
	fire_rate.wait_time = player_manager.fire_rate
	get_input()
	
func get_input():
	if weaponState != 0:
		var shooting_direction = Input.get_vector("Shoot_Left", "Shoot_right", "Shoot_Up", "Shoot_Down")
		if Input.is_action_pressed("Shoot"):
			player.play_animations(shooting_direction)
			if can_fire:
				firePosition(shooting_direction)
				fire(shooting_direction)

func firePosition(shooting_direction: Vector2):
	match shooting_direction:
		Vector2(1,0):
			global_position.x = player.global_position.x + 15
			global_position.y = player.global_position.y
		Vector2(-1,0):
			global_position.x = player.global_position.x - 15
			global_position.y = player.global_position.y
		Vector2(0,1):
			global_position.x = player.global_position.x
			global_position.y = player.global_position.y + 10
		Vector2(0,-1):
			global_position.x = player.global_position.x
			global_position.y = player.global_position.y - 30

func fire(shooting_direction: Vector2):
	can_fire = false
	fire_rate.start()
	var bullet = bullet_path.instantiate()
	bullet.global_position = self.global_position
	match shooting_direction:
		Vector2(1,0):
			bullet.direction = Vector2.RIGHT.rotated(rotation)
		Vector2(-1,0):
			bullet.direction = Vector2.LEFT.rotated(rotation)
		Vector2(0,1):
			bullet.direction = Vector2.DOWN.rotated(rotation)
		Vector2(0,-1):
			bullet.direction = Vector2.UP.rotated(rotation)
	get_parent().add_child(bullet)

func _on_player_manager_current_weapon(weaponName: Variant) -> void:
	weaponState = weaponName

func _on_fire_rate_timeout() -> void:
	can_fire = true
