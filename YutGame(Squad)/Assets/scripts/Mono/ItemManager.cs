using System.Collections.Generic;
using UnityEngine;

namespace XEntity
{
    //조재훈 : 여긴 진짜 아이템 매니저에 관한것을 수행 (사용할지 말지
    

    //이 스크립트에는 모든 다양한 항목 참조 및 다양한 유형의 항목 사용 이벤트가 포함됩니다.
    //저장/로딩/항목 사용 시스템이 제대로 작동하려면 이 스크립트가 현장에 있어야 합니다.
    //참고: 한 번에 하나의 참조만 씬(scene) 에 존재해야 합니다.

    public class ItemManager : MonoBehaviour
    {
        //싱글톤으로 만들어줌
        //싱글톤 공부 기회
        public static ItemManager Instance { get; private set; }

        //스크립트 가능한 모든 항목의 목록입니다.
        //생성 시 항목을 수동으로 할당하거나 스크립트 가능한 항목을 선택합니다. > 마우스 오른쪽 버튼 > 항목 목록에 추가를 선택합니다.
        public List<Item> itemList = new List<Item>();

        private void Awake()
        {
            //Singleton logic
            #region Singleton
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            #endregion

            //Any code in awake should be after the singleton evaluation
        }

        //인벤토리 항목 중 하나에서 항목 사용 버튼을 클릭하면 이 기능이 호출됩니다.
        public void UseItem(ItemSlot slot) 
        {
            if (slot.IsEmpty) return;

            //사용자 지정 항목 유형에 대한 케이스를 추가하고 아래에 작성된 해당 사용 방법을 호출하십시오.
            //메소드의 매개 변수로 슬롯을 전달합니다.
            switch (slot.slotItem.type) 
            {
                default: DefaultItemUse(slot); break;
                case ItemType.Consumeable: ConsumeItem(slot); break;
                case ItemType.ToolOrWeapon: EquipItem(slot); break;
                case ItemType.Placeable: PlaceItem(slot); break;
                case ItemType.Armor: ArmorUse(slot); break;
                case ItemType.MagicTool: MagicToolUse(slot); break;
            }
        }

        //여기에 사용자 지정 항목 유형 사용 방법을 추가합니다.
        //사용자 지정 항목 사용 방법은 ItemSlot을 인수로 사용해야 합니다.
        //참고: 이 항목 슬롯은 사용 방법을 호출할 때 항목이 유지되는 슬롯입니다.


        //아래 항목 사용 방법을 미리 설정합니다.


        //아까 말씀드렸던 if(obj.CompareTag("태그이름")을 여기서 지정해줘서 사용효과를 다 따로따로 넣어 사용하시는 공간이라 생각하면 될거 같아요
        private void ArmorUse(ItemSlot slot) 
        {
            //사용효과에 관해서

            //정확하진 않지만 전 그렇게 보입니다.
            Debug.Log("Using armor");
        }

        private void MagicToolUse(ItemSlot slot) 
        {
            //사용효과에 관해서 
            Debug.Log("Using magic tool");
        }

        private void ConsumeItem(ItemSlot slot)
        {
            //사용효과에 관해서 
            Debug.Log("Consuming " + slot.slotItem.itemName);
            slot.Remove(1);
        }

        private void EquipItem(ItemSlot slot)
        {
            //사용효과에 관해서 
            Debug.Log("Equipping " + slot.slotItem.itemName);
        }

        private void PlaceItem(ItemSlot slot)
        {
            //사용효과에 관해서 
            Debug.Log("Placing " + slot.slotItem.itemName);
        }

        private void DefaultItemUse(ItemSlot slot)
        {
            //사용효과에 관해서 
            Debug.Log($"Using {slot.slotItem.itemName}.");
        }


        //Returns the item from itemList at index.
        public Item GetItemByIndex(int index)
        {
            //사용효과에 관해서 
            return itemList[index];
        }

        //Returns the item from the itemList with the name.
        public Item GetItemByName(string name) 
        {
            foreach (Item item in itemList) if (item.itemName == name) return item;
            return null;
        }

        //Returns the index of the passed in item on the itemList.
        //NOTE: Returns -1 if the item does not exist in the list and the item should be added to the list.
        public int GetItemIndex(Item item) 
        {
            for (int i = 0; i < itemList.Count; i++) if (itemList[i] == item) return i;
            return -1;
        }
    } 
}
