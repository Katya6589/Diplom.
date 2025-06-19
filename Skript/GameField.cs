using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameField : MonoBehaviour
{
    public List<Cell> cells;
    public List<Transform> cubes;
    public GameObject coinPrefab;
    public MoveCounter moveCounter;



    [Serializable]
    public class Cell
    {
        public Vector2Int position;
        public Transform cellObject;
    }

    private Dictionary<Transform, Vector2Int> cubePositions = new Dictionary<Transform, Vector2Int>();

    void Start()
    {
        InitField();
    }

    private void InitField()
    {
        int totalMoves = 0;

        List<Cell> startCells = cells.FindAll(c => Mathf.Abs(c.position.x) != 1 || Mathf.Abs(c.position.y) != 1);
        List<Cell> targetCells = cells.FindAll(c => Mathf.Abs(c.position.x) == 1 && Mathf.Abs(c.position.y) == 1);
        targetCells = ShuffleCells(targetCells);

        for (int i = 0; i < cubes.Count; i++)
        {
            var cube = cubes[i];
            Cell cell = null;

            if (cube.tag == "c1" || cube.tag == "c4") // Горизонтальные кубы
            {
                var possibleCells = startCells.FindAll(c => c.position.y == targetCells[i].position.y);
                cell = possibleCells[Random.Range(0, possibleCells.Count)];
                totalMoves += Mathf.Abs(targetCells[i].position.x - cell.position.x);
            }
            else // Вертикальные кубы
            {
                var possibleCells = startCells.FindAll(c => c.position.x == targetCells[i].position.x);
                cell = possibleCells[Random.Range(0, possibleCells.Count)];
                totalMoves += Mathf.Abs(targetCells[i].position.y - cell.position.y);
            }

            startCells.Remove(cell);

            Vector3 worldPos = cell.cellObject.position;
            worldPos.y = cube.transform.position.y;
            cube.transform.position = worldPos;

            cube.transform.forward = (targetCells[i].cellObject.position - cell.cellObject.position);

            cubePositions[cube] = cell.position;
        }

        List<Cell> reachableCells = new List<Cell>();
        foreach (var cell in startCells)
        {
            foreach (var cube in cubes)
            {
                Vector2Int cubePos = cubePositions[cube];

                if ((cube.tag == "c1" || cube.tag == "c4") && cubePos.y == cell.position.y)
                {
                    reachableCells.Add(cell);
                    break;
                }
                else if (!(cube.tag == "c1" || cube.tag == "c4") && cubePos.x == cell.position.x)
                {
                    reachableCells.Add(cell);
                    break;
                }
            }
        }

        int minFullPath = int.MaxValue;

        if (reachableCells.Count > 0)
        {
            var coinCell = reachableCells[Random.Range(0, reachableCells.Count)];

            Vector3 coinPos = coinCell.cellObject.position;
            coinPos.y = coinPrefab.transform.position.y;
            Instantiate(coinPrefab, coinPos, Quaternion.identity);

            for (int i = 0; i < cubes.Count; i++)
            {
                var cube = cubes[i];
                Vector2Int cubePos = cubePositions[cube];
                int pathToCoin = 0, pathCoinToTarget = 0;

                if ((cube.tag == "c1" || cube.tag == "c4") && cubePos.y == coinCell.position.y)
                {
                    pathToCoin = Mathf.Abs(cubePos.x - coinCell.position.x);
                    var tCell = targetCells[i];
                    if (coinCell.position.y == tCell.position.y)
                        pathCoinToTarget = Mathf.Abs(tCell.position.x - coinCell.position.x);
                    else
                        pathCoinToTarget = int.MaxValue;
                }
                else if (!(cube.tag == "c1" || cube.tag == "c4") && cubePos.x == coinCell.position.x)
                {
                    pathToCoin = Mathf.Abs(cubePos.y - coinCell.position.y);
                    var tCell = targetCells[i];
                    if (coinCell.position.x == tCell.position.x)
                        pathCoinToTarget = Mathf.Abs(tCell.position.y - coinCell.position.y);
                    else
                        pathCoinToTarget = int.MaxValue;
                }
                else
                {
                    pathToCoin = int.MaxValue;
                    pathCoinToTarget = int.MaxValue;
                }

                if (pathToCoin < int.MaxValue && pathCoinToTarget < int.MaxValue)
                {
                    int fullPath = pathToCoin + pathCoinToTarget;
                    if (fullPath < minFullPath)
                        minFullPath = fullPath;
                }
            }

            if (minFullPath != int.MaxValue)
            {
                totalMoves += minFullPath;
            }
        }

        moveCounter.SetMoves(totalMoves);
    }

    private List<Cell> ShuffleCells(List<Cell> cells)
    {
        for (int i = 0; i < cells.Count - 1; i++)
        {
            int r = Random.Range(i, cells.Count);
            (cells[i], cells[r]) = (cells[r], cells[i]);
        }
        return cells;
    }
}
