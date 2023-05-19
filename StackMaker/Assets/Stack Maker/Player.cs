using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject playerGameObj;
    [SerializeField] float offset = 0.15f;
    [SerializeField] private GameObject StackParent;
    [SerializeField] private GameObject BrickPoint;
    [SerializeField] private GameObject Bridge;
    [SerializeField] private GameObject LinePoint;
    [SerializeField] LevelManager LevelManager;

    private List<GameObject> bricks = new List<GameObject>();

    private int countBricks = 0;
    private int countLine = 0;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick")
        {
            countBricks++;
            AddBrick(other.gameObject);
        }
        else if (other.tag == "BridgeBrick")
        {
            other.gameObject.tag = "Untagged";
            other.GetComponent<Bridge>().ShowBrick();
            RemoveBrick();
        }
        else if (other.tag == "win")
        {
            NextLevel();
        }


    }

    private void AddBrick(GameObject brick)
    {
        brick.transform.SetParent(StackParent.transform);
        brick.transform.localPosition = new Vector3(0f, offset * (countBricks - 1), 0f);
        playerGameObj.transform.localPosition = new Vector3(0f, offset * (countBricks - 1), 0f);
        bricks.Add(brick);
    }

    private void RemoveBrick()
    {
        countBricks--;
        //if (StackParent.transform.GetChild(countBricks + 1).gameObject.CompareTag("StackBrick"))
        //{
        //    StackParent.transform.GetChild(countBricks + 1).gameObject.transform.SetParent(Bridge.transform);
        //    Bridge.transform.GetChild(countLine + 1).gameObject.transform.position = new Vector3(LinePoint.transform.position.x, LinePoint.transform.position.y, LinePoint.transform.position.z + (float)countLine);
        //    countLine++;
        //    playerGameObj.transform.localPosition = new Vector3(playerGameObj.transform.localPosition.x, playerGameObj.transform.localPosition.y - offset, playerGameObj.transform.localPosition.z);
        //}

        if(bricks.Count > 0)
        {
            GameObject newBrick = bricks[bricks.Count - 1];
            bricks.Remove(newBrick);
            Destroy(newBrick);
            playerGameObj.transform.localPosition -= new Vector3(0, 0.2f, 0);
        }

    }

    public void ResetPlayer()
    {
        for (int i = bricks.Count - 1; i >= 0; i--) 
        {
            Destroy(bricks[i]);
            bricks.RemoveAt(i);
        }
    }

    public void NextLevel()
    {
        ResetPlayer();
        transform.position = startPos;
        GetComponent<SwipeController>().SetTarget(startPos);
        LevelManager.NextLevel();
    }
}
