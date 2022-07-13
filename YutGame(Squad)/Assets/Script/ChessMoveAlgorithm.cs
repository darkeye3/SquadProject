using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMoveAlgorithm : MonoBehaviour
{
    private Vector3 yutMoving = new Vector3(0, 0, 0);

    public void Move(int moving)
    {
        //조건 값을 이제 형님이 만든 Enum에 들어가진 번호들이 출력되었을 때 그 값만큼 돌리면 된다
        //Ex) 걸(3칸이동)이 나오면 Enum 변수 "걸(Enum값 3)"을 받고 그 값을 switch문으로 case 3번에 대입 후 Vector3.forward * EnumState.걸 이런식으로 하면 되겠지 월요일 확인해봐야함
        //최적화 하고싶으면 끝모서리마다 내 캐릭터를 회전 시켜주면 되지 (그러면 Vector.forward만 사용하면 되기 때문에)
        
        //gameObject.transform.Translate(Vector3.forward);                                                   //직선으로 순간이동
        yutMoving = new Vector3(transform.position.x, transform.position.y, moving);
        transform.position = Vector3.Slerp(transform.position, yutMoving, 1f * Time.deltaTime);        //Slerp 곡선이동/ 임시값 : 2번째 인자 Vector3.forward
        
    }
    //1. 특정 값만큼 직선으로 체스말이 이동 
    
    //2. 특정 값만큼 곡선으로 체스말이 이동    (완료)

    //3. 특정 값만큼 체스말 순간이동        (완료)

    //1. 특정 값은 움직일 크기가 고정되어 있음. (Ex : 1이 반환 시, world 좌표값의 N만큼 이동)       (완료?)

    //2. 
}
