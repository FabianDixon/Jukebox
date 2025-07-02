extends Camera2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	Events.room_entered.connect(func(room):
		global_position = room.global_position
		)
