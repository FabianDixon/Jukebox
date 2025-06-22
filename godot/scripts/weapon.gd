extends Area2D

@onready var player_manager: Node = %PlayerManager
@onready var weapon: Area2D = $"."

func _on_body_entered(_body: Node2D) -> void:
	player_manager.pickupWeapon(weapon.name)
	queue_free()
