using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GamePropertiesSO gameProperties;

    private Vector2Int gridDimensions;
    private Material[] materials;

    public GridBuilder gridBuilder;
    private Cube[,] instantiatedCubes;

    public Camera mainCamera;

    private int moves = 0;
    public TextMeshProUGUI movesText;

    void Awake()
    {
        gridDimensions = new Vector2Int(gameProperties.gridSize, gameProperties.gridSize);
        materials = gameProperties.materials;
    }

    void Start()
    {
        instantiatedCubes = gridBuilder.instantiatedCubes;
    }

    void Update()
    {
        // Check if the user clicked the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Get the cube that was hit by the ray
                GameObject hitCube = hit.collider.gameObject;
                bool found = false;
                for (int y = 0; y < gridDimensions.y && !found; y++)
                {
                    for (int x = 0; x < gridDimensions.x; x++)
                    {
                        if (instantiatedCubes[y, x].gameObject == hitCube)
                        {
                            if (instantiatedCubes[y, x].materialIndex == instantiatedCubes[0, 0].materialIndex) continue;
                            moves++;
                            movesText.text = moves.ToString();

                            StartCoroutine(BfsFromTopLeft(instantiatedCubes[y, x].materialIndex));
                            found = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator BfsFromTopLeft(int newMaterialIndex)
    {
        Queue<(int, int)> q = new Queue<(int, int)>();
        bool[,] visited = new bool[gridDimensions.y, gridDimensions.x];

        int startMaterialIndex = instantiatedCubes[0, 0].materialIndex;

        q.Enqueue((0, 0));
        while (q.Count > 0)
        {
            (int y, int x) = q.Dequeue();

            if (visited[y, x]) continue;
            visited[y, x] = true;

            Cube currentCube = instantiatedCubes[y, x];

            if (currentCube.materialIndex != startMaterialIndex) continue;

            float transitionTime = 0.03f;
            float elapsedTime = 0;
            Renderer renderer = currentCube.gameObject.GetComponent<Renderer>();
            while (elapsedTime < transitionTime)
            {
                renderer.material.color = Color.Lerp(materials[startMaterialIndex].color, materials[newMaterialIndex].color, elapsedTime / transitionTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            renderer.material = materials[newMaterialIndex];
            instantiatedCubes[y, x].materialIndex = newMaterialIndex;

            if (x > 0) q.Enqueue((y, x - 1));
            if (x < gridDimensions.x - 1) q.Enqueue((y, x + 1));
            if (y > 0) q.Enqueue((y - 1, x));
            if (y < gridDimensions.y - 1) q.Enqueue((y + 1, x));
        }

        // Check if the player has won
        bool won = true;
        for (int y = 0; y < gridDimensions.y && won; y++)
        {
            for (int x = 0; x < gridDimensions.x; x++)
            {
                if (instantiatedCubes[y, x].materialIndex != newMaterialIndex)
                {
                    won = false;
                    break;
                }
            }
        }

        if (won)
        {
            // TODO: Implement the win condition (show menu)
            Debug.Log("You won!");
        }
    }
}
