// using UnityEngine;

// public class GridManager : MonoBehaviour
// {
//     public GameObject squarePrefab;
//     public int width = 6;
//     public int height = 6;

//     private Square[,] grid;

//     void Start()
//     {
//         grid = new Square[width, height];
//         GenerateGrid();
//     }

//     void GenerateGrid()
//     {
//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 GameObject obj = Instantiate(squarePrefab, new Vector3(x, y, 0), Quaternion.identity);
//                 Square square = obj.GetComponent<Square>();
//                 square.Init(x, y, Random.Range(0, 5), this);
//                 grid[x, y] = square;
//             }
//         }
//     }

//     public Square GetSquare(int x, int y)
//     {
//         if (x < 0 || y < 0 || x >= width || y >= height) return null;
//         return grid[x, y];
//     }
// }