using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "ScriptableObjects/ProjectileStats")]
public class ProjectileStats : ScriptableObject
{
    public float speed;
    public float damage;
    public float distance;
}
