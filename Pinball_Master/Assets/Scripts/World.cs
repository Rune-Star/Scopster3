using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameBoard;
using UnityEngine;

public class World : MonoBehaviour
{
    public Board board;

    public int width;
    public int height;
    public float size;

    public Tile tilePrefab;

    public int count;
    public float lerpTerm;

    [SerializeField] List<Block> spawnPool;

    void Start() { new Board(width, height, size, tilePrefab, out board); }

    void Summon(Tile tile)
    {
        var r = Random.Range(0, spawnPool.Capacity);

        board.Summon(spawnPool[r], tile);
    }

    void LoopSummon(int count) // count >= 2 일 때 게임이 튕김.(loop)
    {
        // List<Tile> im = new List<Tile>(count);
        //
        // Summon(out selectedTile);
        // im.Add(selectedTile);
        // count--;
        //
        // while (count > 0) {
        //     if (im.Contains(selectedTile)) continue;
        //     Summon(out selectedTile);
        //     im.Add(selectedTile);
        //     count--;
        // }

        Tile[] spawnTileList = new Tile[count];
        Tile randomTile;

        for (int i = 0; i < count;) {
            randomTile = board.tileArray[Random.Range(0, width), Random.Range(0, height)];

            if (spawnTileList.Contains(randomTile)) {
                randomTile = board.tileArray[Random.Range(0, width), Random.Range(0, height)];
            } else {
                spawnTileList[i] = randomTile;
                i++;
            }
        }

        foreach (var tile in spawnTileList) {
            Summon(tile);
        }
    }

    IEnumerator Loop(int count)
    {
        Tile[] spawnTileList = new Tile[count];
        Tile randomTile;

        for (int i = 0; i < count;) {
            InfiniteLoopDetector.Run();
            randomTile = board.tileArray[Random.Range(0, width), Random.Range(0, height)];

            if (spawnTileList.Contains(randomTile)) {
                randomTile = board.tileArray[Random.Range(0, width), Random.Range(0, height)];
            } else {
                spawnTileList[i] = randomTile;
                i++;
            }
        }

        int k = 0;

        while (true) {
            InfiniteLoopDetector.Run();
            if (k < spawnTileList.Length) {
                Summon(spawnTileList[k++]);

                yield return new WaitForSeconds(lerpTerm);
            } else {
                yield break;
            }
        }
    }

    // System.Random random = new System.Random();
    // int N = 0;
    // bool[] selected = Enumerable.Repeat(false, N).ToArray();

    public void ButtonMethod()
    {
        //LoopSummon(count);
        StartCoroutine(Loop(count));
    }
}