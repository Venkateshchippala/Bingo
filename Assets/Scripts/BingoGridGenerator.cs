using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BingoGridGenerator : MonoBehaviour
{
    private BingoSuccessChecking bingosuccess_checking;
    private BingoNumer_Obj_Spawning bingoNumber_Obj_Spawning;
    public GameObject cellPrefab;
    public Transform gridParent;
    public Transform spawnRandom_Number_BackGround;
    public GameObject spawnRandom_Number_Obj;   
    private int[,] cellNumbers = new int[5, 5];
    public bool[,] selected = new bool[5, 5];
    // 5 Columns, each column has its own number range
    private List<List<int>> columns = new List<List<int>>
    {
        new List<int> { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15 },
        new List<int> { 16,17,18,19,20,21,22,23,24,25,26,27,28,29,30 },
        new List<int> { 31,32,33,34,35,36,37,38,39,40,41,42,43,44,45 },
        new List<int> { 46,47,48,49,50,51,52,53,54,55,56,57,58,59,60 },
        new List<int> { 61,62,63,64,65,66,67,68,69,70,71,72,73,74,75 }
    };
    public List<int> allNumbers = new List<int>();
    public List<int> gridValues = new List<int>();
    //private int[,] left_Diagonal_cells = new int[,] { { 0, 0 }, { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 } };
    //private int[,] right_Diagonal_cells = new int[,] { { 0, 4 }, { 1, 3 }, { 2, 2 }, { 3, 1 }, { 4, 0 } };

    int button_clickedValue=80;
    public bool win_Bingo = false;
    //int totalNumbers;
    //bool cell_Scuccess = false;
    private void Awake()
    {
        bingosuccess_checking = FindObjectOfType<BingoSuccessChecking>();
        bingoNumber_Obj_Spawning = FindObjectOfType<BingoNumer_Obj_Spawning>();
        Collect_AllNumbers();

    }

    void Start()
    {
        GenerateGrid();       
    }

    void GenerateGrid()
    {
        int gridSize = 5;

        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);

                // Get random number from correct column
                int index = Random.Range(0, columns[col].Count);
                int value = columns[col][index];

                int r = row;
                int c = col;

                cell.transform.GetComponent<Image>().color = c == 0 ? new Color32(142, 215, 187, 255) : 
                                                             c == 1 ? new Color32(215, 255, 152, 255) :
                                                             c == 2 ? new Color32(244, 169, 255, 255) :
                                                             c == 3 ? new Color32(245, 194, 35, 255) : 
                                                                      new Color32(179, 195, 226, 255);
                // Store in 2D array
                cellNumbers[r, c] = value;

                // Remove used number
                columns[col].RemoveAt(index);

                // Update cell UI
                cell.GetComponentInChildren<TMP_Text>().text = value.ToString();
                gridValues.Add(value);
                if(r == 2 && c == 2)
                {
                    cell.GetComponentInChildren<TMP_Text>().text = "Free";
                    cell.transform.GetComponent<Image>().color = Color.magenta;
                    cell.transform.GetComponentInChildren<TMP_Text>().fontSize = 45f;

                    selected[r, c] = true;
                }

                // VERY IMPORTANT: Capture local value for button
                int clickedValue = value;

                cell.GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnCellClicked(clickedValue, cell, r,c);
                });
            }
        }
        //Debug.Log("cellNumbers.length  : " + cellNumbers.Length);
    }

    void OnCellClicked(int clicked_number,GameObject cellobj,int row,int col)
    {
        //Debug.Log("cellNumbers[row,col] : " + row + "," + col);
        //Debug.Log("Cell Clicked: " + clicked_number);
        button_clickedValue = clicked_number;
        //Debug.Log("button_clickedValue : " + button_clickedValue);
        if (button_clickedValue == bingoNumber_Obj_Spawning.random_Value)
        {
            //Debug.Log("row , col : " + row + "," + col);

            bingoNumber_Obj_Spawning.cell_Scuccess = true;
            selected[row,col] = true;
            //Debug.Log("random_index : " + random_index);
            gridValues.RemoveAt(bingoNumber_Obj_Spawning.random_index);
            // Debug.Log("allNumbers count : " + allNumbers.Count);
            /*Debug.Log("random_Value : " + random_Value);

            Debug.Log("button_clickedValue : " + button_clickedValue);*/

            cellobj.transform.GetComponent<Image>().color = Color.green;
            if(row == 2 && col == 2)
            {
                cellobj.transform.GetComponent<Image>().color = Color.magenta;
            }
           // Debug.Log("selected[row,col] : " + selected[row, col]);
            bingosuccess_checking.Row_Cells_Checking();
            bingosuccess_checking.Col_Cells_Checking();
            bingosuccess_checking.Left_Diagonal_Bingo_Checking();
            bingosuccess_checking.Right_Diagonal_Bingo_Checking();
        }

    }

    // Add the all (Lists_Numbers) of Columns add to  allNumbers
    public void Collect_AllNumbers()
    {
        foreach (var num in columns)
        {
            allNumbers.AddRange(num);
        }
        bingoNumber_Obj_Spawning.totalNumbers = allNumbers.Count;
        // Debug.Log("allNumbers count : " + allNumbers.Count);
    }

}
