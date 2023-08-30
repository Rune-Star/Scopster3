using UnityEngine;

namespace GameBoard
{
    public class GridSystem : MonoBehaviour
    {
        public int width;
        public int height;
        public float cellSize;

        int[,] _gridArray;
        public GameObject _obj;
        TextMesh[,] _textMeshes;

        public GridSystem(int width, int height, float cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            _gridArray = new int[this.width, this.height];
            _textMeshes = new TextMesh[this.width, this.height];

            Draw();
        }
        void Draw()
        {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    _gridArray[x, y] = 0;

                    var pos = GetWorldPosition(x, y);

                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.magenta, 100);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.magenta, 100);
                }
            }
        }

        void Start()
        {
            _gridArray = new int[this.width, this.height];
            _textMeshes = new TextMesh[this.width, this.height];

            Draw();
        }
        

        Vector3 GetWorldPosition(int x, int y) { return new Vector3(x, y) * cellSize; }
    }
}