extends Sprite2D
 
@export var _door_scene: PackedScene
@export var _drops : Array[PackedScene]

@onready var spawner: Node2D = $Spawner
@onready var spawner_2: Node2D = $Spawner2
var enemyCounter: int = 0
var enemyCountZero: bool = false
var roomFinished: bool = false
var dropCounter: int = 0 # if > 0 : already dropped an item

func _physics_process(_delta):
	count_Enemies()
	room_Finished()

func add_door(direction: int) -> void:
	var door: Sprite2D = _door_scene.instantiate()
	add_child(door)
	door.position = Vector2(212, 0).rotated(-PI * direction / 2.0)
	door.rotation = -PI * direction / 2.0
 
func clear_doors() -> void:
	for child in get_children():
		if not child.name.begins_with("F") and not child.name.begins_with("I"):
			child.queue_free()
 
func set_icon(icon: Texture2D) -> void:
	$Icon.texture = icon

func room_Finished():
	if enemyCountZero && not roomFinished:
		Events.EnemyRoomIsFinished.emit(self)
		roomFinished = true
		item_drop()
		print("room finished")

func count_Enemies():
	if spawner != null :
		enemyCounter = spawner.enemy_counter + spawner_2.enemy_counter
		if enemyCounter == 0 && not roomFinished:
			enemyCountZero = true

func _on_player_detector_body_entered(_body: Node2D) -> void:
	Events.room_entered.emit(self)
	if not roomFinished:
		Events.EnemyRoomEntered.emit(self)

func item_drop():
	if dropCounter <= 0:
		var drop_chance = 10 * PlayerStats.luck  # 10% base chance
		var random_number = randi() % 100
		if random_number < drop_chance:
			var drop_randomizer = randi_range(0,_drops.size() - 1)
			var droppedItem = _drops[drop_randomizer ].instantiate()
			add_child(droppedItem)
			droppedItem.global_position = self.global_position
		
		dropCounter += 1
