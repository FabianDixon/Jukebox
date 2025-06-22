extends RichTextLabel

@onready var rich_text_label: RichTextLabel = $"."
@onready var player: CharacterBody2D = $"../.."
@onready var playerManager = player.get_child(0)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	rich_text_label.text = "HP: " + str(playerManager.player_HP).pad_decimals(1)
