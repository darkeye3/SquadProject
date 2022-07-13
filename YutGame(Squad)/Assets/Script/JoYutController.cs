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
    //���� ���� ���� �� �ִ� �������� 
    public enum eYutState
    {
        Idle = 0,           //�⺻���
        Ready,              //�غ�Ǿ���.
        Draw,               //������ ����
        Judge,              //���� (���� �����̴� ����)
        ActiveFalse,        //�ݺ� 
        Count               
    };
    public PlayerController Player;
    public eYutState yutChangeState = eYutState.Idle;
    public eYutResult yutValue = eYutResult.Idle;
    ChessMoveAlgorithm playerAlgorithm;
    bool isOneMoreChance = false;   //��, �� ������ �ѹ� �� ������

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
        //���ʹ�ư ������ �� �ڸ����� �� ������
        if (Input.GetMouseButton(0))
        {
            PositionReset();
            bResetPosition = true;
            eState = eYutState.Ready;
            AddJump += 10 * Time.deltaTime;         //�������� �� �׷��� �̹��� ��.
        }

        //�� ������ �Լ� �� �ʱ�ȭ
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
            //UI �� ��ĭ������
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
                //   playerAlgorithm.Move((int)eYutResult.BackDo);                     //����ȯ �����ֱ�
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
                //�ѹ��� ������ �� �ִ� ��ȸ �߰�
                OneMoreChance();
                break;
            case eYutResult.Mo:
                //   playerAlgorithm.Move((int)eYutResult.Mo);
                Player.steps = 5;
                if (Player.NodePosition + Player.steps < Player.Node.childNodeList.Count)
                {
                    Player.StartCoroutine(Player.Move());
                }
                //�ѹ��� �������� �ִ� ��ȸ �߰�
                OneMoreChance();
                break;
        }

        YutResult = 0;
    }

    //���� �ѹ��� ���� ��ȸ�� ��
    void OneMoreChance()
    {
        if (isOneMoreChance == true)
        {
            return;
        }

        isOneMoreChance = true;
	}

    //�̰͵� ��������
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

    //�ڸ� �ʱ�ȭ�����ֱ�
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
                YutResult += 0;         //�޸�
		    }
            else if(DrawResult[i] == 1)
            {
                YutResult += 1;         //�ո�
		    }
		}

        //������ �޸��̰� �������� �� �ո��̸� -1ĭ�̵� ��
        if(DrawResult[0] == 0 && YutResult == 3)
        {
            YutResult = (int)eYutResult.BackDo;
            yutValue = eYutResult.BackDo;
            return;             //���⼭ ������ �߸� �ؿ� �� ���� �ʿ䰡 ���� ������ ���� �Լ����Ḧ ����.
		}

        //�� or �� �߸�
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
            Debug.Log("��");
            YutResult = (int)eYutResult.Mo;
            yutValue = eYutResult.Mo;
            UIManager.GetInstance.YutDisplay("��", "5ĭ ������ \n�ѹ� ��!");
        }
        if (DrawResult == 1)
        {
            if (GameObject.Find("BackYut").GetComponent<Result>().result == 1)
            {
                Debug.Log("����");
                YutResult = (int)eYutResult.BackDo;
                yutValue = eYutResult.BackDo;
                UIManager.GetInstance.YutDisplay("����", "1ĭ �ڷ�");
            }
            else 
            { 
                Debug.Log("��");
                YutResult = (int)eYutResult.Do;
                yutValue = eYutResult.Do;
                UIManager.GetInstance.YutDisplay("��", "1ĭ ������");
            }
        
        }
        if (DrawResult == 2)
        {
            Debug.Log("��");
            YutResult = (int)eYutResult.Gae;
            yutValue = eYutResult.Gae;
            UIManager.GetInstance.YutDisplay("��", "2ĭ ������");
        }
        if (DrawResult == 3)
        {
            Debug.Log("��");
            YutResult = (int)eYutResult.Gul;
            yutValue = eYutResult.Gul;
            UIManager.GetInstance.YutDisplay("��", "3ĭ ������");
        }
        if (DrawResult == 4)
        {
            Debug.Log("��");
            YutResult = (int)eYutResult.Yut;
            yutValue = eYutResult.Yut;
            UIManager.GetInstance.YutDisplay("��", "4ĭ ������ \n�ѹ� ��!");
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