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
        //���� RigidBody ��������
    }

    // Update is called once per frame
    void Update()
    {
        //���� �������� x���� �����ͼ� �ո����� �޸����� �Ǻ�
        float Angle = Mathf.Abs(transform.rotation.eulerAngles.z);

        if (Angle > 360)
        {
            int i = (int)Angle / 360;
            X = Angle - (360 * i);
            // rotation.x = 30; �ո�

            if (X > 270.0f || X <= 90.0f)
            {
                 result = 0;
            }
            else
            {
                 result = 1;
            }
        }
        else
        {
            if (Angle > 270.0f || Angle <= 90.0f) { result = 0; }
            else { result = 1; }
        }
    }
}
