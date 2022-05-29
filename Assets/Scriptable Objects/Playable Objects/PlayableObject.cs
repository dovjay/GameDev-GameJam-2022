using UnityEngine;

[CreateAssetMenu(fileName = "PlayableObject", menuName = "Create Scriptable Object/PlayableObject", order = 0)]
public class PlayableObject : ScriptableObject {
    public int index; // should match the list index to playerCollision
    public GameObject prefab;
    public bool canFly;
    public bool isGhost;
}