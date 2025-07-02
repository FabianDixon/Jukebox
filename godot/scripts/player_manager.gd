extends Node

#Weapon
signal currentWeapon(weaponName)
var current_Weapon = Weapon.guitar
var previous_Weapon = Weapon.mic

enum Weapon{
	none,
	guitar,
	mic
}

#Active Item
signal activeItem(itemName)

enum Items{
	none,
	vinyl,
	disc,
	headphones,
	radio,
	cassette,
	amp
}

#Timer for dying
@onready var timer: Timer = $Timer

func _ready():
	currentWeapon.emit(Weapon.guitar)

func _physics_process(_delta):
	switchWeapon()

func pickupWeapon(pickUp: String):
	previous_Weapon = current_Weapon
	if pickUp == "guitar":
		currentWeapon.emit(Weapon.guitar)
		current_Weapon = (Weapon.guitar)
	elif pickUp == "mic":
		currentWeapon.emit(Weapon.mic)
		current_Weapon = (Weapon.mic)

func pickup_active_item(pickUp: String):
	if pickUp == "vinyl":
		activeItem.emit(Items.vinyl)

func switchWeapon():
	var intermediate
	if Input.is_action_just_pressed("SwitchWeapon"):
		intermediate = current_Weapon
		current_Weapon = previous_Weapon
		currentWeapon.emit(current_Weapon)
		previous_Weapon = intermediate

func hpLoss(dmg: float):
	if PlayerStats.armor == 1:
		print("Lost armor")
		PlayerStats.armor = 0
	else:
		PlayerStats.player_HP -= dmg
		if PlayerStats.player_HP == 0.0:
			Engine.time_scale = 0.5
			timer.start()

func recoverHP(hp: float):
	if PlayerStats.player_HP < PlayerStats.base_HP:
		PlayerStats.player_HP += hp
		if PlayerStats.player_HP > PlayerStats.base_HP:
			PlayerStats.player_HP = PlayerStats.base_HP

func extraHP(hp: float):
	PlayerStats.base_HP += hp

func armorGain():
	PlayerStats.armor = 1

func gainSpeed(speed: float):
	PlayerStats.player_speed += speed

func looseSpeed(speed: float):
	if PlayerStats.player_speed > 10:
		PlayerStats.player_speed -= speed
		if PlayerStats.player_speed < 10:
			PlayerStats.player_speed = 10

func gainDmg(dmg: float):
	PlayerStats.bullet_dmg += dmg

func looseDmg(dmg: float):
	if PlayerStats.bullet_dmg > 0.1:
		PlayerStats.bullet_dmg -= dmg
		if PlayerStats.bullet_dmg < 0.1:
			PlayerStats.bullet_dmg = 0.1

func increaseBulletSpeed(speed: float):
	PlayerStats.bullet_speed += speed

func decreaseBulletSpeed(speed: float):
	if PlayerStats.bullet_speed > 10:
		PlayerStats.bullet_speed -= speed
		if PlayerStats.bullet_speed < 10:
			PlayerStats.bullet_speed = 10

func increaseBulletRange(B_Range: float):
	PlayerStats.bullet_range += B_Range

func decreaseBulletRange(B_Range: float):
	if PlayerStats.bullet_range > 50:
		PlayerStats.bullet_range -= B_Range
		if PlayerStats.bullet_range < 50:
			PlayerStats.bullet_range = 50

func increaseFireRate(seconds: float):
	if PlayerStats.fire_rate > 0.0:
		PlayerStats.fire_rate -= seconds
		if PlayerStats.fire_rate < 0.1:
			PlayerStats.fire_rate = 0.1
	
func decreaseFireRate(seconds: float):
	PlayerStats.fire_rate += seconds

func increaseLuck():
	PlayerStats.luck += 1

func decreaseLuck():
	PlayerStats.luck -=1

func _on_timer_timeout() -> void:
	Engine.time_scale = 1.0
	get_tree().reload_current_scene()
