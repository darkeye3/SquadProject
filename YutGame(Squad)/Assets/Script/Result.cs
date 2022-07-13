using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Result : MonoBehaviour
{
    public int result = 0;
    float X;
    // Start is called before the first frame update
    
    void Start()
    {
        //윷의 RigidBody 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        //윷의 로테이턴 x값을 가져와서 앞면인지 뒷면인지 판별

        Angle(result);
    }

    int Angle(int result)
    {
        float Angle = Mathf.Abs(transform.rotation.eulerAngles.z);

        if (Angle > 360)
        {
            int i = (int)Angle / 360;
            X = Angle - (360 * i);
            // rotation.x = 30; 앞면

            if (X > 270.0f || X <= 90.0f)
            {
               return result = 0;
            }
            else
            {
               return result = 1;
            }
        }
        else
        {
            if (Angle > 270.0f || Angle <= 90.0f) { return result = 0; }
            else { return result = 1; }
        }
    }
}
