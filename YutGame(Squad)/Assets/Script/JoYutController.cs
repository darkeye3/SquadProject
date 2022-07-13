using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoYutController : MonoBehaviour
{
    public enum eYutResult
    {
        BackDo = -1,
        Idle,
        Do = 1,
        Gae,
        Gul,
        Yut,
        Mo,
        Count
    };
    //윷을 내가 던질 수 있는 상태인지 
    public enum eYutState
    {
        Idle = 0,           //기본대기
        Ready,              //준비되었다.
        Draw,               //던지는 역할
        Judge,              //판정 (윷이 움직이는 숫자)
        ActiveFalse,        //반복 
        Count               
    };
    public PlayerController Player;
    public eYutState yutChangeState = eYutState.Idle;
    public eYutResult yutValue = eYutResult.Idle;
    ChessMoveAlgorithm playerAlgorithm;
    bool isOneMoreChance = false;   //윷, 모 나오면 한번 더 던지기

    UIManager uiManager;
    // Start is called before the first frame update
    public GameObject[] Yut;
    float AddJump;
    public float RatateDistance;
    public int DrawResult;
    public int YutResult;
    eYutResult eResult = eYutResult.Idle;
    eYutState eState = eYutState.Idle;

    bool bResetPosition = false;



    // Start is called before the first frame update
    void Start()
    {
        //        playerAlgorithm = GameObject.Find("Player1").GetComponent<ChessMoveAlgorithm>();
        Player = GameObject.Find("PlayerBlue").GetComponent<PlayerController>();
        eState = eYutState.Ready;

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        DrawResult = GameObject.Find("BackYut").GetComponent<Result>().result + GameObject.Find("Yut1").GetComponent<Result>().result + GameObject.Find("Yut2").GetComponent<Result>().result + GameObject.Find("Yut3").GetComponent<Result>().result;
        Yut[0] = GameObject.Find("BackYut");
        //DrawResult[0] = GameObject.Find("BackYut").GetComponent<Result>().result;
        for(int i = 1; i < Yut.Length; i++)
        {
            Yut[i] = GameObject.Find("Yut" + i);
            //DrawResult[i] = GameObject.Find("Yut" + i).GetComponent<Result>().result;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //왼쪽버튼 누르면 윷 자리세팅 후 던지기
        if (Input.GetMouseButton(0))
        {
            PositionReset();
            bResetPosition = true;
            eState = eYutState.Ready;
            AddJump += 10 * Time.deltaTime;         //윷던지는 힘 그러나 미미할 것.
        }

        //윷 던지기 함수 및 초기화
        if (Input.GetMouseButtonUp(0) && eState == eYutState.Ready)
        {
            RandomDraw(AddJump);
            AddJump = 0;
            eState = eYutState.Draw;
        }

        if (Yut[0].GetComponent<Rigidbody>().velocity.y == 0 &&
        Yut[1].GetComponent<Rigidbody>().velocity.y == 0 &&
        Yut[2].GetComponent<Rigidbody>().velocity.y == 0 &&
        Yut[3].GetComponent<Rigidbody>().velocity.y == 0 &&
        eState == eYutState.Draw)
        {
            eState = eYutState.Judge;
        }

        //OLD
        //if (Yut[0].GetComponent<Rigidbody>().velocity.y == 0 && Yut[1].GetComponent<Rigidbody>().velocity.y == 0 &&
        //    Yut[2].GetComponent<Rigidbody>().velocity.y == 0 && Yut[3].GetComponent<Rigidbody>().velocity.y == 0 &&
        //    eState == eYutState.Draw)
        //{
        //    eState = eYutState.Judge;
        //}

        if (eState == eYutState.Judge)
        {
            //Yutjudge(YutResult);
            Yutjudge();
            YutStateChange();
            //UI 윷 몇칸앞으로
            eState = eYutState.ActiveFalse;
        }

        if (eState == eYutState.ActiveFalse)
        {
            bResetPosition = false;
        }
    }

    void YutStateChange()
    {
        switch (yutValue)
        {
            case eYutResult.BackDo:
                //   playerAlgorithm.Move((int)eYutResult.BackDo);                     //형변환 시켜주기
/*                Player.steps = -1;
                if (Player.NodePosition + Player.steps < Player.Node.childNodeList.Count)
                {
                    Player.StartCoroutine(Player.Move());
                }*/
                
                break;
            case eYutResult.Do:
                //  playerAlgorithm.Move((int)eYutResult.Do);
                Player.steps = 1;
                if (Player.NodePosition + Player.steps < Player.Node.childNodeList.Count)
                {
                    Player.StartCoroutine(Player.Move());
                }
                break;
            case eYutResult.Gae:
                //  playerAlgorithm.Move((int)eYutResult.Gae);
                Player.steps = 2;
                if (Player.NodePosition + Player.steps < Player.Node.childNodeList.Count)
                {
                    Player.StartCoroutine(Player.Move());
                }
                break;
            case eYutResult.Gul:
                //  playerAlgorithm.Move((int)eYutResult.Gul);
                Player.steps = 3;
                if (Player.NodePosition + Player.steps < Player.Node.childNodeList.Count)
                {
                    Player.StartCoroutine(Player.Move());
                }
                break;
            case eYutResult.Yut:
                //  playerAlgorithm.Move((int)eYutResult.Yut);
                Player.steps = 4;
                if (Player.NodePosition + Player.steps < Player.Node.childNodeList.Count)
                {
                    Player.StartCoroutine(Player.Move());
                }
                //한번더 윷던질 수 있는 기회 추가
                OneMoreChance();
                break;
            case eYutResult.Mo:
                //   playerAlgorithm.Move((int)eYutResult.Mo);
                Player.steps = 5;
                if (Player.NodePosition + Player.steps < Player.Node.childNodeList.Count)
                {
                    Player.StartCoroutine(Player.Move());
                }
                //한번더 윷던질수 있는 기회 추가
                OneMoreChance();
                break;
        }

        YutResult = 0;
    }

    //윷을 한번더 던질 기회를 줌
    void OneMoreChance()
    {
        if (isOneMoreChance == true)
        {
            return;
        }

        isOneMoreChance = true;
	}

    //이것도 윷던지기
    void RandomDraw(float force)
    {
        if (force <= 7) { force = 7; }
        if (force >= 15) { force = 15; }

        for (int i = 0; i < 4; i++)
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

    //자리 초기화시켜주기
    void PositionReset()
    {
        if (bResetPosition == false)
        {
            for (int i = 0; i < 4; i++)
            {
                Yut[i].transform.position = new Vector3(Random.Range(4, -4), Random.Range(6, 7), 0);
                Yut[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }

    }

    void Yutjudge()
    {
    /*
        for(int i = 0; i < DrawResult.Length; i++)
        {
            if(DrawResult[i] == 0)
            {
                YutResult += 0;         //뒷면
		    }
            else if(DrawResult[i] == 1)
            {
                YutResult += 1;         //앞면
		    }
		}

        //빽윷만 뒷면이고 나머지가 다 앞면이면 -1칸이동 값
        if(DrawResult[0] == 0 && YutResult == 3)
        {
            YutResult = (int)eYutResult.BackDo;
            yutValue = eYutResult.BackDo;
            return;             //여기서 빽도가 뜨면 밑에 더 돌릴 필요가 없기 떄문에 빠른 함수종료를 위함.
		}

        //윷 or 모가 뜨면
        if(YutResult == 4)
        {
            YutResult = (int)eYutResult.Mo;
            yutValue = eYutResult.Mo;
            Debug.Log(YutResult);
        }
        else if(YutResult == 0)
        {
            YutResult = (int)eYutResult.Yut;
            
            yutValue = eYutResult.Yut;
		}
        else if(YutResult == 1)
        {
            yutValue = eYutResult.Do;
        }
        else if (YutResult == 2)
        {
            yutValue = eYutResult.Gae;
        }
        else if (YutResult == 3)
        {
            yutValue = eYutResult.Gul;
        }
        */

        Debug.Log(YutResult);

        //Old
        if (DrawResult == 0)
        {
            Debug.Log("모");
            YutResult = (int)eYutResult.Mo;
            yutValue = eYutResult.Mo;
            UIManager.GetInstance.YutDisplay("모", "5칸 앞으로 \n한번 더!");
        }
        if (DrawResult == 1)
        {
            if (GameObject.Find("BackYut").GetComponent<Result>().result == 1)
            {
                Debug.Log("빽도");
                YutResult = (int)eYutResult.BackDo;
                yutValue = eYutResult.BackDo;
                UIManager.GetInstance.YutDisplay("빽도", "1칸 뒤로");
            }
            else 
            { 
                Debug.Log("도");
                YutResult = (int)eYutResult.Do;
                yutValue = eYutResult.Do;
                UIManager.GetInstance.YutDisplay("도", "1칸 앞으로");
            }
        
        }
        if (DrawResult == 2)
        {
            Debug.Log("개");
            YutResult = (int)eYutResult.Gae;
            yutValue = eYutResult.Gae;
            UIManager.GetInstance.YutDisplay("개", "2칸 앞으로");
        }
        if (DrawResult == 3)
        {
            Debug.Log("걸");
            YutResult = (int)eYutResult.Gul;
            yutValue = eYutResult.Gul;
            UIManager.GetInstance.YutDisplay("도", "3칸 앞으로");
        }
        if (DrawResult == 4)
        {
            Debug.Log("윷");
            YutResult = (int)eYutResult.Yut;
            yutValue = eYutResult.Yut;
            UIManager.GetInstance.YutDisplay("도", "4칸 앞으로 \n한번 더!");
        }

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

}