using UI;
using UnityEngine;

namespace GameBoard
{
    public interface ITileObject
    {
        
    }
    
    public class Tile : MonoBehaviour
    {
        public Block tile;

        public int x, y;

        public void Init(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        #if UNITY_EDITOR
        public void Print()
        {
            if(tile != null) Debug.Log(tile.gameObject.name);
            Debug.Log((x, y).ToString());
        }
        #endif
    }
}