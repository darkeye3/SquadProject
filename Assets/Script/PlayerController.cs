using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public NodeController Node;

    public int NodePosition;

    public int steps;

    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
/*        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            steps = 1;

            if(NodePosition+steps < Node.childNodeList.Count)
            {
                StartCoroutine(Move());
            }
        }*/
    }

    public IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;

        while(steps>0)
        {
            Vector3 nextPos = Node.childNodeList[NodePosition + 1].position;
            while (MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            steps--;
            NodePosition++;
        }

        isMoving = false;
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.Slerp(transform.position, goal, 2f));
    }
}
