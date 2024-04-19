using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGrass : MonoBehaviour
{
    public List<objSpawn> allRandomSpawn = new List<objSpawn>();
    public GameObject treeInWorldObject;
    Vector3 tagetTrans;
    public int treeAmount;
    List<GameObject> treeList = new List<GameObject>();
    GameObject[] treeArray;

    public int minX;
    public int maxX;
    public int minZ;
    public int maxZ;

    public bool isGen;

    void Start()
    {
        tagetTrans = treeInWorldObject.transform.position;
        GenerateTree();
        isGen = false;

    }

    private void Update()
    {
        if (isGen)
        {
            NewGenerate();
        }
    }

    public void NewGenerate()
    {
        for (int i = 0; i < treeList.Count; i++)
        {
            Destroy(treeList[i]);
        }

        treeList.Clear();

        GenerateTree();

        isGen = false;
    }

    public void GenerateTree()
    {
        for (int i = 0; i <= treeAmount; i++)
        {
            int randSpawn = Random.Range(0, allRandomSpawn.Count);

            treeList.Add(Instantiate(allRandomSpawn[randSpawn].obj));
            treeArray = treeList.ToArray();

            treeArray[i].transform.position = new Vector3(tagetTrans.x + (Random.Range(minX, maxX)), tagetTrans.y, tagetTrans.z + Random.Range(minZ, maxZ));
            
            treeArray[i].transform.rotation = new Quaternion(0, Random.Range(0,90f), 0, Random.Range(0, 90f));

            float rand = Random.Range(allRandomSpawn[randSpawn].mixScale, allRandomSpawn[randSpawn].maxScale);
            treeArray[i].transform.localScale = new Vector3(rand, rand, rand);

            treeArray[i].transform.parent = treeInWorldObject.transform;
        }
    }

}

[System.Serializable]
public class objSpawn
{
    public string name;
    public GameObject obj;
    public float mixScale;
    public float maxScale;
}
