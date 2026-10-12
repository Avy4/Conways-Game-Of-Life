using System;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Grid grid;
    private CellInformation[,] cells;
    private Dictionary<(int i, int j), CellInformation> cellReference;

    // Corresponds to M (rows)
    const float length = 14.0f;

    // Corresponds to N (columns)
    const float height = 10.0f;

    public void Reset()
    {
        foreach(CellInformation ci in cells)
        {
            Destroy(ci.gameObject);
        }
        
        cells = null;
        cellReference = null;
        grid.transform.position = new Vector3(1, 0, 0);
        grid.transform.localScale = Vector3.one;
    }

    public void Initialize(int M, int N, float pctAlive)
    {
        cells = new CellInformation[N,M];
        cellReference = new Dictionary<(int i, int j), CellInformation>();

        SetGrid(M, N);

        // Columns is X
        for (int i = 0; i < N; i++)
        {
            // Rows is Y
            for (int j = 0; j < M; j++)
            {
                int cellValue = (UnityEngine.Random.Range(0.0f, 1.0f) * 100 <= pctAlive) ? 1 : 0;
                cells[i, j] = Instantiate(cellPrefab, grid.CellToWorld  (new Vector3Int(i, j, 0)), Quaternion.identity, grid.transform).GetComponent<CellInformation>();
                cells[i, j].SetState(cellValue);
                cellReference[(i, j)] = cells[i, j];
            }
        }
    }

    private void SetGrid(int M, int N)
    {
        float scale = Mathf.Clamp((M > N) ? height / M : length / N, 0, 1);

        float gridXTranslate = -(N * scale) / 2;
        float gridYTranlate = -(M * scale) / 2;
        
        grid.transform.localScale = new Vector3(scale, scale, scale);
        grid.transform.position += new Vector3(gridXTranslate, gridYTranlate, 0);
    }

    public void Step()
    {
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                int aliveNeighborCount = (cellReference[(i, j)].GetState() == 1) ? -1 : 0;

                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        CellInformation temp;
                        cellReference.TryGetValue((i + x, j + y), out temp);

                        if (temp)
                        {
                            aliveNeighborCount += temp.GetState();
                        }
                    }
                }

                if (aliveNeighborCount == 3)
                {
                    cellReference[(i, j)].SetState(1);
                }
                else if (aliveNeighborCount != 2)
                {
                    cellReference[(i, j)].SetState(0);
                }
            }
        }        
    }
}
