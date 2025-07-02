extends Node2D

@export var _enemies : Array[PackedScene]
var enemy_counter: int = 0

enum Enemy_Types
{
	EMPTY = 0,
	Annoying_Fan = 16
}

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var enemy
	for i in range(3):
		var enemy_randomizer = randi_range(0,1)
		if enemy_randomizer != 0:
			enemy = _enemies[enemy_randomizer].instantiate()
			add_child(enemy)
			var offset = global_position + Vector2(0.5,0)
			enemy.position = offset.rotated(-PI * i / 2.0)
			enemy_counter += 1

func _process(_delta: float) -> void:
	countEnemies()

func countEnemies():
	enemy_counter = self.get_child_count()
