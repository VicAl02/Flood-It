using UnityEngine;

[CreateAssetMenu(fileName = "GamePropertiesSO", menuName = "ScriptableObjects/GamePropertiesSO")]
public class GamePropertiesSO : ScriptableObject
{
    public int gridSize = 10;
    public GameObject cubePrefab;
    public Material[] materials;
}
