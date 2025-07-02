extends CharacterBody2D

var direction = Vector2.RIGHT
@export
var speed = 150 # pixels / s
@onready var animated_sprite: AnimatedSprite2D = $AnimatedSprite2D
var damage: float = 0.5
var bullet_range: float = 100.0
var start_position: Vector2
var distance_traveled: float = 0.0

func _ready():
	set_as_top_level(true) # move independent from parent node
	distance_traveled = 0.0
	start_position = global_position

func _physics_process(delta):
	damage =PlayerStats.bullet_dmg
	speed = PlayerStats.bullet_speed
	bullet_range = PlayerStats.bullet_range
	global_position += direction * speed * delta
	distance_traveled = global_position.distance_to(start_position)
	animated_sprite.play("default")
	if distance_traveled > bullet_range:
		#Play some animation then delete
		queue_free()

func _on_area_2d_area_entered(area: Area2D) -> void:
	area.get_parent().hpLoss(damage)
	#Play some animation then delete
	queue_free()
