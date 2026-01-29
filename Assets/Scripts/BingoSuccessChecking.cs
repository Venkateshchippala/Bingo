using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BingoSuccessChecking : MonoBehaviour
{
    public TextMeshProUGUI success_Bingo_Txt;
    private BingoNumer_Obj_Spawning bingoNumer_Obj_Spawning;
    private BingoGridGenerator bingogridgenerator;
    private int[,] left_Diagonal_cells = new int[,] { { 0, 0 }, { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 } };
    private int[,] right_Diagonal_cells = new int[,] { { 0, 4 }, { 1, 3 }, { 2, 2 }, { 3, 1 }, { 4, 0 } };
    
    private void Awake()
    {
        bingogridgenerator = FindObjectOfType<BingoGridGenerator>();
        bingoNumer_Obj_Spawning = FindObjectOfType<BingoNumer_Obj_Spawning>();
    }
    // Start is called before the first frame update
    public bool Row_Cells_Checking()
    {
        for (int row = 0; row < 5; row++)
        {
            int count = 0;
            for (int col = 0; col < 5; col++)
            {
                if (bingogridgenerator.selected[row, col] == true)
                {
                    count++;
                }
            }
            if (count == 5)
            {
                success_Bingo_Txt.gameObject.SetActive(true);
                bingogridgenerator.win_Bingo = true;
                bingoNumer_Obj_Spawning.totalNumbers = 0;
                Debug.Log("Row Bingo Success ");
                return true;
            }
        }
        return false;
    }
    public bool Col_Cells_Checking()
    {
        for (int col = 0; col < 5; col++)
        {
            int count = 0;

            for (int row = 0; row < 5; row++)
            {
                if (bingogridgenerator.selected[row, col] == true)
                {
                    count++;
                   
                }
            }
            if (count == 5)
            {
                success_Bingo_Txt.gameObject.SetActive(true);
                bingogridgenerator.win_Bingo = true;
                bingoNumer_Obj_Spawning.totalNumbers = 0;
                Debug.Log(" Colum Bingo Success ");
                return true;
            }
        }
        return false;
    }
    public bool Left_Diagonal_Bingo_Checking()
    {
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            int row = left_Diagonal_cells[i, 0];
            int col = left_Diagonal_cells[i, 1];

            if (bingogridgenerator.selected[row, col] == true)
            {
                count++;
            }
            if (count == 5)
            {
                success_Bingo_Txt.gameObject.SetActive(true);
                bingogridgenerator.win_Bingo = true;
                bingoNumer_Obj_Spawning.totalNumbers = 0;
                Debug.Log("Left_Diagonal_Bingo Success ");
                return true;
            }
        }
        return false;
    }
    public bool Right_Diagonal_Bingo_Checking()
    {
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            int row = right_Diagonal_cells[i, 0];
            int col = right_Diagonal_cells[i, 1];
            if (bingogridgenerator.selected[row, col] == true)
            {
                count++;
            }
            if (count == 5)
            {
                success_Bingo_Txt.gameObject.SetActive(true);
                bingogridgenerator.win_Bingo = true;
                bingoNumer_Obj_Spawning.totalNumbers = 0;
                Debug.Log("Right Diagonal Bingo Scuccess ");
                return true;
            }
        }
        return false;
    }
}
