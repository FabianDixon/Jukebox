using UnityEngine;

[CreateAssetMenu(fileName = "ActiveItems", menuName = "ScriptableObjects/ActiveItems")]
public class ActiveItem : ScriptableObject
{
    public float speed;
    public int damage;
    public int distance;
}
