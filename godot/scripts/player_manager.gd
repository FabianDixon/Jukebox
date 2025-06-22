extends Node

enum Weapon{
	none,
	guitar,
	mic
}

#Weapon
signal currentWeapon(weaponName)
var current_Weapon = Weapon.guitar
var previous_Weapon = Weapon.mic

#Stats
var base_HP: float = 3.0
var player_HP: float = 3.0
var armor: int = 1
var player_speed: float = 120.0
var bullet_dmg: float = 0.5
var bullet_speed: float = 150
var bullet_range: float = 100.0
var fire_rate: float = 1.0 #Shots per second

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

func switchWeapon():
	var intermediate
	if Input.is_action_just_pressed("SwitchWeapon"):
		intermediate = current_Weapon
		current_Weapon = previous_Weapon
		currentWeapon.emit(current_Weapon)
		previous_Weapon = intermediate

func hpLoss(dmg: float):
	if armor == 1:
		print("Lost armor")
		armor = 0
	else:
		player_HP -= dmg
		if player_HP == 0.0:
			Engine.time_scale = 0.5
			timer.start()

func recoverHP(hp: float):
	if player_HP < base_HP:
		player_HP += hp
		if player_HP > base_HP:
			player_HP = base_HP

func extraHP(hp: float):
	base_HP += hp

func armorGain():
	armor = 1

func gainSpeed(speed: float):
	player_speed += speed

func looseSpeed(speed: float):
	if player_speed > 10:
		player_speed -= speed
		if player_speed < 10:
			player_speed = 10

func gainDmg(dmg: float):
	bullet_dmg += dmg

func looseDmg(dmg: float):
	if bullet_dmg > 0.1:
		bullet_dmg -= dmg
		if bullet_dmg < 0.1:
			bullet_dmg = 0.1

func increaseBulletSpeed(speed: float):
	bullet_speed += speed

func decreaseBulletSpeed(speed: float):
	if bullet_speed > 10:
		bullet_speed -= speed
		if bullet_speed < 10:
			bullet_speed = 10

func increaseBulletRange(B_Range: float):
	bullet_range += B_Range

func decreaseBulletRange(B_Range: float):
	if bullet_range > 50:
		bullet_range -= B_Range
		if bullet_range < 50:
			bullet_range = 50

func increaseFireRate(seconds: float):
	if fire_rate > 0.0:
		fire_rate -= seconds
		if fire_rate < 0.1:
			fire_rate = 0.1
	
func decreaseFireRate(seconds: float):
	fire_rate += seconds

func _on_timer_timeout() -> void:
	Engine.time_scale = 1.0
	get_tree().reload_current_scene()
