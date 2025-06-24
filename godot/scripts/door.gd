extends Sprite2D
@onready var door_area: Area2D = $DoorArea

var wallNames: Array = ["Left", "Right", "Top", "Bottom"]

@onready var collision_shape: CollisionShape2D = $Door/CollisionShape2D

func _ready():
	get_parent().connect("isRoomFinished", Callable(self, "_on_isRoomFinished"))

func _on_door_area_body_entered(body: Node2D) -> void:
	if body.name in wallNames:
		body.queue_free()

func _on_isRoomFinished():
	collision_shape.set_deferred("disabled", true)
