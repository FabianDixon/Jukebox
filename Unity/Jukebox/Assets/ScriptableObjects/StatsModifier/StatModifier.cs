using UnityEngine;

[CreateAssetMenu(fileName = "StatModifier", menuName = "ScriptableObjects/StatModifier")]
public class StatModifier : ScriptableObject
{
    public int health;
    public float speed;
    public float damage;
}
