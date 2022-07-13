using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YutController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Yut;
    float AddJump;
    public float RatateDistance;
    int DrawResult;
    public int YutResult;
    eYutState eState = eYutState.Idle;
    bool bResetPosition = false;



    // Start is called before the first frame update
    void Start()
    {
        eState = eYutState.Ready;
    }
    // Update is called once per frame
    void Update()   
    {
        if (Input.GetMouseButton(0))
        {
            PositionReset();
            bResetPosition = true;
            eState = eYutState.Ready;
            AddJump += 10 * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0) && eState == eYutState.Ready)
        {
            RandomDraw(AddJump);
            AddJump = 0;
            eState = eYutState.Draw;
        }

        if (Yut[0].GetComponent<Rigidbody>().velocity.y == 0 && Yut[1].GetComponent<Rigidbody>().velocity.y == 0 &&
            Yut[2].GetComponent<Rigidbody>().velocity.y == 0 && Yut[3].GetComponent<Rigidbody>().velocity.y == 0 && 
            eState == eYutState.Draw)
        {
            eState = eYutState.Judge;
        }

        if (eState == eYutState.Judge)
        {
            Yutjudge(YutResult);
            //UI À· ¸îÄ­¾ÕÀ¸·Î
            eState = eYutState.ActiveFalse;
        }

        if(eState == eYutState.ActiveFalse)
        {
            bResetPosition = false;
        }
    }
    void RandomDraw(float force)
    {

        if (force <= 7) { force = 7; }
        if (force >= 15) { force = 15; }

        for(int i = 0; i <4; i++)
        {
            Yut[i].GetComponent<Rigidbody>().AddForce(0, 110 * force, 0 * Time.deltaTime);
        }
    }

    void Yutrotate()
    {
        Vector3 dir = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));

        for (int i = 0; i < 4; i++)
        {
            Yut[i].GetComponent<Rigidbody>().transform.Rotate(dir * 20f * Time.deltaTime);
        }
    }

    void PositionReset()
    {
        if(bResetPosition == false)
        {
            for (int i = 0; i < 4; i++)
            {
                Yut[i].transform.position = new Vector3(Random.Range(4, -4), Random.Range(6, 7), 0);
                Yut[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
        
    }

    int Yutjudge(int YutResult)
    {
        DrawResult = GameObject.Find("BackYut").GetComponent<Result>().result + GameObject.Find("Yut1").GetComponent<Result>().result + GameObject.Find("Yut2").GetComponent<Result>().result + GameObject.Find("Yut3").GetComponent<Result>().result;

        if (DrawResult == 0)
        {
            Debug.Log("¸ð");
            YutResult = (int)eYutResult.Mo;
        }
        if (DrawResult == 1)
        {
            if(GameObject.Find("BackYut").GetComponent<Result>().result == 1)
            {
                Debug.Log("»ªµµ");
                YutResult = (int)eYutResult.BackDo;
            }
            else { Debug.Log("µµ"); }

        }
        if (DrawResult == 2)
        {
            Debug.Log("°³");
            YutResult = (int)eYutResult.Gae;
        }
        if (DrawResult == 3)
        {
            Debug.Log("°É");
            YutResult = (int)eYutResult.Gul;
        }
        if (DrawResult == 4)
        {
            Debug.Log("À·");
            YutResult = (int)eYutResult.Yut;
        }

        return YutResult;
    }

    private void OnTriggerStay(Collider other)
    {
        if (transform.position.y >= RatateDistance)
        {
            Yutrotate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
/*        if (collision.transform.tag == "Yut")
        {
            
        }*/
    }

    enum eYutState {
        Idle = 0, 
        Ready, 
        Draw,
        Judge,
        ActiveFalse,
        Count
    };

    enum eYutResult
    {
        BackDo = -1,
        Do = 1,
        Gae,
        Gul,
        Yut,
        Mo,
        Count
    };
}
