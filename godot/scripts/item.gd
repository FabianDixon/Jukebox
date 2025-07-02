extends Area2D

@onready var area2d: Area2D = $"."


func _on_body_entered(body: Node2D) -> void:
	body.get_child(0).pickup_active_item(area2d.name)
	queue_free()
