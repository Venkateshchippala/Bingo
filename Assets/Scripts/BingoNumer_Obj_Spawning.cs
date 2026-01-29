using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BingoNumer_Obj_Spawning : MonoBehaviour
{
    private BingoGridGenerator bingoGridGenerator;
    public Transform spawnRandom_Number_BackGround;
    private List<GameObject> spawnRandom_numObjs = new List<GameObject>();
    public GameObject spawnRandom_Number_Obj;
    public int random_Value;
    public int random_index;
    public int totalNumbers;
    public bool cell_Scuccess = false;

    private void Awake()
    {
        bingoGridGenerator = FindObjectOfType<BingoGridGenerator>();
    }
    private void Start()
    {
        StartCoroutine(SpawnRandom_BingoNumber());
    }
    IEnumerator SpawnRandom_BingoNumber()
    {
        Debug.Log("I Am Working ");
        while (totalNumbers > 0)
        {
            SpawnRandom_Number();
            yield return new WaitForSeconds(2.7f);
        }
    }

    public void SpawnRandom_Number()
    {
        Move_Left_and_Destroy_SpawnObjs();

        if (cell_Scuccess == true)
        {
            totalNumbers--;
            cell_Scuccess = false;
        }

        GameObject random_num_obj = Instantiate(spawnRandom_Number_Obj, spawnRandom_Number_BackGround);


        random_num_obj.transform.localPosition = new Vector2(random_num_obj.transform.localPosition.x,
                                                                                   random_num_obj.transform.localPosition.y);
        spawnRandom_numObjs.Add(random_num_obj);
        random_index = Random.Range(0, bingoGridGenerator.gridValues.Count);
        random_Value = bingoGridGenerator.gridValues[random_index];
        //Debug.Log("random_Value : " + random_Value);
        random_num_obj.GetComponentInChildren<TMP_Text>().text = random_Value.ToString();
        random_num_obj.transform.GetComponent<Image>().color = random_Value < 16 ? new Color32(142, 215, 187, 255) :
                                                               random_Value < 31 ? new Color32(215, 255, 152, 255) :
                                                               random_Value < 46 ? new Color32(244, 169, 255, 255) :
                                                               random_Value < 61 ? new Color32(245, 194, 35, 255) :
                                                                                   new Color32(179, 195, 226, 255);


    }
    public void Move_Left_and_Destroy_SpawnObjs()
    {
        for (int i = spawnRandom_numObjs.Count - 1; i >= 0; i--)
        {
            GameObject obj = spawnRandom_numObjs[i];

            if (obj.transform.localPosition.x < -400f)
            {
                Destroy(obj);
                spawnRandom_numObjs.RemoveAt(i);
            }
            else
            {
                obj.transform.localPosition =
                    new Vector2(obj.transform.localPosition.x - 210f,
                                obj.transform.localPosition.y);

                obj.GetComponent<Image>().color = Color.gray;
            }
        }
    }
}
