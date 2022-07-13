using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //public GameObject[] UIGameObject = new GameObject[11];

    private static UIManager instance = null;

    public static UIManager GetInstance
    {
        get
        {
            if(instance == null)
            {
                return null;
			}
            return instance;
        }
	}

	[SerializeField] private TextMeshProUGUI textYutValue;
    [SerializeField] private TextMeshProUGUI textYutValueSmall;
	
    private void Awake()
	{
		if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
		}
        else
        {
            Destroy(this.gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        textYutValue = GameObject.Find("YutValue").GetComponent<TextMeshProUGUI>();
        textYutValueSmall = GameObject.Find("YutValueSmall").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //-------------------------------------------
        //for (int i = 0; i < UIGameObject.Length; i++)
        //{
        //    UIGameObject[i].SetActive(false);
        //}
        //-------------------------------------------


    }

    public void YutDisplay(string yuttext, string yuttextsmall)
    {
        textYutValue.text = yuttext;
        textYutValueSmall.text = yuttextsmall;
	}

    public void YutResultDisplay(int YutResult)
    {
        //if (DrawResult == 0)
        //{
        //    Debug.Log("¸ð");
        //    YutResult = (int)eYutResult.Mo;
        //
        //    //--------------------------
        //    UIGameObject[8].SetActive(true);
        //    UIGameObject[9].SetActive(true);
        //    //----------------------------
        //}
        //if (DrawResult == 1)
        //{
        //    if (GameObject.Find("BackYut").GetComponent<Result>().result == 1)
        //    {
        //        Debug.Log("»ªµµ");
        //        YutResult = (int)eYutResult.BackDo;
        //        //----------------------------------------
        //        UIGameObject[10].SetActive(true);
        //        UIGameObject[11].SetActive(true);
        //        //----------------------------------------
        //    }
        //    else
        //    {
        //        Debug.Log("µµ");
        //        //-------------------------------------
        //        UIGameObject[0].SetActive(true);
        //        UIGameObject[1].SetActive(true);
        //        //-------------------------------------
        //
        //    }
        //    if (DrawResult == 2)
        //    {
        //        Debug.Log("°³");
        //        YutResult = (int)eYutResult.Gae;
        //
        //        //-----------------------------------
        //        UIGameObject[2].SetActive(true);
        //        UIGameObject[3].SetActive(true);
        //
        //        //----------------------------------------
        //    }
        //    if (DrawResult == 3)
        //    {
        //        Debug.Log("°É");
        //        YutResult = (int)eYutResult.Gul;
        //
        //        //-----------------------------------
        //        UIGameObject[4].SetActive(true);
        //        UIGameObject[5].SetActive(true);
        //        //------------------------------------
        //    }
        //    if (DrawResult == 4)
        //    {
        //        Debug.Log("À·");
        //        YutResult = (int)eYutResult.Yut;
        //        //------------------------------------
        //        UIGameObject[6].SetActive(true);
        //        UIGameObject[7].SetActive(true);
        //        //--------------------------------------
        //    }
        //
        //}
    }
}
