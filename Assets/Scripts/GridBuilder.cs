using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public GamePropertiesSO gameProperties;

    private Vector2Int gridDimensions;
    private GameObject cubePrefab;
    private Material[] materials;

    public Cube[,] instantiatedCubes;

    public Camera mainCamera;

    void Awake()
    {
        gridDimensions = new Vector2Int(gameProperties.gridSize, gameProperties.gridSize);
        cubePrefab = gameProperties.cubePrefab;
        materials = gameProperties.materials;
        instantiatedCubes = new Cube[gridDimensions.y, gridDimensions.x];
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < gridDimensions.y; y++)
        {
            for (int x = 0; x < gridDimensions.x; x++)
            {
                GameObject instantiatedCube = Instantiate(cubePrefab, new Vector3(x, y, 0), Quaternion.identity);
                int materialIndex = Random.Range(0, materials.Length);
                instantiatedCube.GetComponent<Renderer>().material = materials[materialIndex];

                Cube instantiatedCubeStruct = new Cube
                {
                    gameObject = instantiatedCube,
                    materialIndex = materialIndex
                };

                instantiatedCubes[gridDimensions.y - 1 - y, x] = instantiatedCubeStruct;
            }
        }

        // Set the camera position and size
        // If the grid has an even number of rows or columns, the camera will be centered between the two middle rows or columns
        Vector2Int gridSizeEven = new Vector2Int((gridDimensions.x % 2 + 1) % 2, (gridDimensions.y % 2 + 1) % 2);
        mainCamera.transform.position = new Vector3(
            gridDimensions.x / 2 - 0.5f * gridSizeEven.x,
            gridDimensions.y / 2 - 0.5f * gridSizeEven.y,
            -1f
        );
        mainCamera.orthographicSize = Mathf.Max(gridDimensions.x, gridDimensions.y) / 2 + 1;
    }
}