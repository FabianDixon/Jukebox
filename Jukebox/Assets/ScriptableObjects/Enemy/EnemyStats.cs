using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float health;
    public float speed;
    public int meleeDamage;
    public int rangedDamage;
    public float chargeAmount;
}
