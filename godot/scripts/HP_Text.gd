extends RichTextLabel

@onready var rich_text_label: RichTextLabel = $"."
@onready var player: CharacterBody2D = $"../../Player"


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	rich_text_label.text = "HP: " + str(PlayerStats.player_HP).pad_decimals(1)
