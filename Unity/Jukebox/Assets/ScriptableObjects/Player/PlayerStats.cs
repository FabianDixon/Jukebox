using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int health;
    public float speed;
    public float damage;
    public float projectileDistance;
    public float projectileSpeed;
    public int luck;
}