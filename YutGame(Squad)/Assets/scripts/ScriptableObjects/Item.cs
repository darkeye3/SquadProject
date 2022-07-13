using UnityEngine;

namespace XEntity
{
    //항목에 대한 데이터 템플릿을 보유하는 스크립트 가능 개체입니다.
    [CreateAssetMenu(fileName = "New Item", menuName = "XEntity/Item")]
    public class Item : ScriptableObject
    {
        //**중요**
        //"필수 영역"에서 다음 속성을 변경하거나 제거하지 마십시오.
        //다른 종속 코드를 사용자 정의하여 작업할 경우를 제외하고.
        //필수 영역 뒤에 원하는 만큼 속성을 추가할 수 있습니다.

        #region Essential
        public ItemType type;
        public string itemName;
        public int itemPerSlot;
        public Sprite icon;
        public GameObject prefab;
        
        //new
        public string ItemInstruction;

        #endregion
    }
}
