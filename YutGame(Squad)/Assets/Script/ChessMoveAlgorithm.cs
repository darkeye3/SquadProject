using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMoveAlgorithm : MonoBehaviour
{
    private Vector3 yutMoving = new Vector3(0, 0, 0);

    public void Move(int moving)
    {
        //���� ���� ���� ������ ���� Enum�� ���� ��ȣ���� ��µǾ��� �� �� ����ŭ ������ �ȴ�
        //Ex) ��(3ĭ�̵�)�� ������ Enum ���� "��(Enum�� 3)"�� �ް� �� ���� switch������ case 3���� ���� �� Vector3.forward * EnumState.�� �̷������� �ϸ� �ǰ��� ������ Ȯ���غ�����
        //����ȭ �ϰ������ ���𼭸����� �� ĳ���͸� ȸ�� �����ָ� ���� (�׷��� Vector.forward�� ����ϸ� �Ǳ� ������)
        
        //gameObject.transform.Translate(Vector3.forward);                                                   //�������� �����̵�
        yutMoving = new Vector3(transform.position.x, transform.position.y, moving);
        transform.position = Vector3.Slerp(transform.position, yutMoving, 1f * Time.deltaTime);        //Slerp ��̵�/ �ӽð� : 2��° ���� Vector3.forward
        
    }
    //1. Ư�� ����ŭ �������� ü������ �̵� 
    
    //2. Ư�� ����ŭ ����� ü������ �̵�    (�Ϸ�)

    //3. Ư�� ����ŭ ü���� �����̵�        (�Ϸ�)

    //1. Ư�� ���� ������ ũ�Ⱑ �����Ǿ� ����. (Ex : 1�� ��ȯ ��, world ��ǥ���� N��ŭ �̵�)       (�Ϸ�?)

    //2. 
}
