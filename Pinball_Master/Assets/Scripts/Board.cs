using UnityEngine;

namespace GameBoard
{
    public class Board : MonoBehaviour
    {
        int width;
        int height;
        float size;

        [SerializeField, ColorUsage(false, true)]
        Color color;

        public Tile tilePrefab;

        public Tile[,] tileArray;

        public Board(int width, int height, float size, Tile tile)
        {
            SetGridSizeInfo(width, height, size);
            tilePrefab = tile;
            Draw();
        }

        public Board(int width, int height, float size, Tile tile, out Board outBoard)
        {
            var board = new GameObject(nameof(Board)).AddComponent<Board>();
            board.SetGridSizeInfo(width, height, size);
            board.tilePrefab = tile;
            board.Draw();

            outBoard = board;
        }

        void SetGridSizeInfo(int width, int height, float size)
        {
            this.width = width;
            this.height = height;
            this.size = size;
        }
        void Draw()
        {
            var pos = -new Vector3((width - 1) * 0.5f, (height - 1) * 0.5f);
            tileArray = new Tile[width, height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    var tile = Instantiate(tilePrefab, transform).GetComponent<Tile>();
                    tile.name = $"Tile[{x},{y}]";
                    tile.Init(x, y);
                    tile.transform.localPosition = new Vector3(x, y) + pos;
                    tileArray[x, y] = tile;
                }
            }

            transform.localScale = new Vector3(size, size);
        }

        public void Summon(Block entity, Tile tile)
        {
            var obj = Spawner.Spawn<Block>(tile.transform.position, tile.transform);
            // var obj = Instantiate(entity.gameObject, tile.transform.position, Quaternion.identity, tile.transform)
            //     .GetComponent<Block>();
            obj.name = nameof(entity);
            obj.spr.sortingOrder = 2;
            obj.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
            tileArray[tile.x, tile.y].tile = obj;

            GameManager.Instance.player.AddTargetEnemy(obj);


            // var obj = Instantiate(entity.gameObject, tile.transform.position, Quaternion.identity, tile.transform)
            //     .GetComponent<Block>();
            // obj.name = "TEST";
            // obj.spr.sortingOrder = 2;
            // obj.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
            // tileArray[tile.x, tile.y].tile = obj;
            //
            // GameManager.Instance.player.AddTargetEnemy(obj);
        }
    }
}