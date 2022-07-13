using UnityEngine;

namespace XEntity
{
    //이 스크립트는 항목을 저장할 수 있는 모든 컨테이너 슬롯에 연결됩니다.
    public class ItemSlot : MonoBehaviour
    {
        //현재 이 슬롯에 있는 항목. 없으면 null입니다.
        public Item slotItem;

        //이 슬롯의 항목 수.
        public int itemCount;

        //itemCount가 0이면 true를 반환합니다.
        public bool IsEmpty { get { return itemCount <= 0; } }

        //항목 아이콘을 표시하기 위한 이미지입니다.
        private UnityEngine.UI.Image iconImage;

        //항목 수를 표시하는 텍스트입니다.
        private UnityEngine.UI.Text countText;

        private void Awake() 
        {
            //UI 변수가 여기에 할당됩니다.
            iconImage = transform.Find("Icon Image").GetComponent<UnityEngine.UI.Image>();      //Icon Image라는 Object를 찾고 유니티엔진.Image를 넣어줌
            countText = transform.Find("Count Text").GetComponent<UnityEngine.UI.Text>();       //Count Text라는 Object를 찾고 유니티엔진.Text를 넣어줌

            iconImage.gameObject.SetActive(false);                                              //게임 오브젝트 비활성화
            countText.text = string.Empty;                                                      //텍스트를 비어있게 초기화 시켜줌
        }

        //슬롯에 항목을 추가할 수 있으면 true를 반환합니다.
        //참고: 항목이 같은 유형이면 갯수가 쌓입니다 (A이미지 string "x2" 이런식인듯?)
        public bool Add(Item item) 
        {
            //만약 Item Script를 가지고있다면 
            if (IsAddable(item))
            {   
                //받은 매개변수값을 변수에 넣어주고
                slotItem = item;
                //아이템카운트변수 (배열같음) 추가
                itemCount++;
                //
                OnSlotModified();
                return true;
            }
            else return false; 
            
        }

        //슬롯에서 전달된 항목의 양을 제거하고 드롭 위치에 놓습니다.
        public void RemoveAndDrop(int amount, Vector3 dropPosition) 
        {
            for (int i = 0; i < amount; i++) 
            {
                if (itemCount > 0)
                {
                    //Utils 너무 어려움
                    Utils.InstantiateItemCollector(slotItem, dropPosition);
                    itemCount--;
                    
                }
                else break;
            }

            OnSlotModified();
        }

        //슬롯에서 전달된 항목의 양을 제거합니다.
        public void Remove(int amount)
        {
            //?연산자(삼향 연산자)에 대해서 찾아볼 기회
            //amount가 itemCount보다 크다면 ? True면 itemCount False면 amount 값 반환      (햇갈리는 부분은 True라고 해서 1 False라고 해서 0을 반환하는것이 아님)
            itemCount -= amount > itemCount ? itemCount : amount;
            OnSlotModified();
        }

        //슬롯을 완전히 비웁니다. (초기화 개념)
        public void Clear() 
        {
            itemCount = 0;
            OnSlotModified();
        }

        //슬롯을 완전히 비우고 모든 항목을 드롭 위치에 놓습니다.
        public void ClearAndDrop(Vector3 dropPosition) 
        {
            RemoveAndDrop(itemCount, dropPosition);
        }

        //추가 항목에 대한 모든 조건이 충족되면 true를 반환합니다.
        private bool IsAddable(Item item)
        {
            if (item != null)
            {
                if (IsEmpty) return true;
                else
                {
                    if (item == slotItem && itemCount < item.itemPerSlot) return true;
                    else return false;
                }
            }
            return false;
        }

        //이 메서드는 슬롯의 변수가 수정될 때마다 호출됩니다.
        private void OnSlotModified() 
        {
            //만약에 IsEmpty가 False면 (Property로 이루어져있고 0보다 아래면 True 1이상이면 False)
            if (!IsEmpty)
            {
                //이미지를 변환해줌 (열거형에 존재하는 숫자로 원하는 이미지를 넣어줌)
                iconImage.sprite = slotItem.icon;
                //텍스트를 그 아이템 이름으로 바꿔줌
                countText.text = itemCount.ToString();
                //이미지를 활성화 시켜줌
                iconImage.gameObject.SetActive(true);
            }
            //isEmpty가 True면 (0아래일 경우)
            else 
            {
                //0으로 초기화
                itemCount = 0;
                //슬릇을 비워줌 (0으로 만드는 것이 아님)
                slotItem = null;
                //이미지 비워줌 
                iconImage.sprite = null;
                //텍스트를 다 지워줌
                countText.text = string.Empty;
                //아이콘이미지를 비활성화
                iconImage.gameObject.SetActive(false);
            }
        }


        //사전 확인 없이 항목 및 항목 수를 직접 할당합니다.
        //참고: 이것은 용기 슬롯 데이터를 적재할 때만 사용해야 합니다.
        //조재훈 : 데이터를 설정해주기 위함 (Item스크립트와 가지고있는 갯수를 받아옴)
        public void SetData(Item item, int count)
        {
            slotItem = item;
            itemCount = count;
            OnSlotModified();
        }
    }
}
