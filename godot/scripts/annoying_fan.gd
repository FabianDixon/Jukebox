extends AnimatedSprite2D

const Speed = 60
const Damage: float = 0.5
var HP: float = 2.0

var direction = Vector2()
@onready var ray_cast_right: RayCast2D = $RayCastRight
@onready var ray_cast_left: RayCast2D = $RayCastLeft
@onready var annoying_fan: AnimatedSprite2D = $'.'
@onready var area_2d: Area2D = $area2d

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	
	if ray_cast_right.is_colliding():
		direction = Vector2(-1,0)
		annoying_fan.flip_h = true
	if ray_cast_left.is_colliding():
		direction = Vector2(1,0)
		annoying_fan.flip_h = false
	
	position += direction * Speed * delta
	
func _on_area_2d_body_entered(body: Node2D) -> void:
	var playerManager = body.get_child(0)
	playerManager.hpLoss(Damage)

func hpLoss(dmg: float):
	HP -= dmg
	print("Hit! New hp: " + str(HP))
	if HP == 0.0:
		queue_free()
	elif HP < 0.0:
		queue_free()
