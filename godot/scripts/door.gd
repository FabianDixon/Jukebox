extends Sprite2D
@onready var door_area: Area2D = $DoorArea
var isRoomFinished: bool = true 

@onready var exit_room: Marker2D = $ExitRoom

func _ready():
	Events.connect("EnemyRoomIsFinished", Callable(self, "_on_isRoomFinished"))
	Events.connect("EnemyRoomEntered", Callable(self, "_on_EnemyRoomEntered"))
	

func _on_door_area_body_entered(body: Node2D) -> void:
	print(body.name)
	print(isRoomFinished)
	if body.name == "Player" && isRoomFinished:
		body.global_position = exit_room.global_position

func _on_isRoomFinished(_room):
	isRoomFinished = true
	
func _on_EnemyRoomEntered(_room):
	isRoomFinished = false
