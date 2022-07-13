using System.Collections.Generic;
using UnityEngine;

namespace XEntity
{
    //������ : ���� ��¥ ������ �Ŵ����� ���Ѱ��� ���� (������� ����
    

    //�� ��ũ��Ʈ���� ��� �پ��� �׸� ���� �� �پ��� ������ �׸� ��� �̺�Ʈ�� ���Ե˴ϴ�.
    //����/�ε�/�׸� ��� �ý����� ����� �۵��Ϸ��� �� ��ũ��Ʈ�� ���忡 �־�� �մϴ�.
    //����: �� ���� �ϳ��� ������ ��(scene) �� �����ؾ� �մϴ�.

    public class ItemManager : MonoBehaviour
    {
        //�̱������� �������
        //�̱��� ���� ��ȸ
        public static ItemManager Instance { get; private set; }

        //��ũ��Ʈ ������ ��� �׸��� ����Դϴ�.
        //���� �� �׸��� �������� �Ҵ��ϰų� ��ũ��Ʈ ������ �׸��� �����մϴ�. > ���콺 ������ ��ư > �׸� ��Ͽ� �߰��� �����մϴ�.
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

        //�κ��丮 �׸� �� �ϳ����� �׸� ��� ��ư�� Ŭ���ϸ� �� ����� ȣ��˴ϴ�.
        public void UseItem(ItemSlot slot) 
        {
            if (slot.IsEmpty) return;

            //����� ���� �׸� ������ ���� ���̽��� �߰��ϰ� �Ʒ��� �ۼ��� �ش� ��� ����� ȣ���Ͻʽÿ�.
            //�޼ҵ��� �Ű� ������ ������ �����մϴ�.
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

        //���⿡ ����� ���� �׸� ���� ��� ����� �߰��մϴ�.
        //����� ���� �׸� ��� ����� ItemSlot�� �μ��� ����ؾ� �մϴ�.
        //����: �� �׸� ������ ��� ����� ȣ���� �� �׸��� �����Ǵ� �����Դϴ�.


        //�Ʒ� �׸� ��� ����� �̸� �����մϴ�.


        //�Ʊ� ������ȴ� if(obj.CompareTag("�±��̸�")�� ���⼭ �������༭ ���ȿ���� �� ���ε��� �־� ����Ͻô� �����̶� �����ϸ� �ɰ� ���ƿ�
        private void ArmorUse(ItemSlot slot) 
        {
            //���ȿ���� ���ؼ�

            //��Ȯ���� ������ �� �׷��� ���Դϴ�.
            Debug.Log("Using armor");
        }

        private void MagicToolUse(ItemSlot slot) 
        {
            //���ȿ���� ���ؼ� 
            Debug.Log("Using magic tool");
        }

        private void ConsumeItem(ItemSlot slot)
        {
            //���ȿ���� ���ؼ� 
            Debug.Log("Consuming " + slot.slotItem.itemName);
            slot.Remove(1);
        }

        private void EquipItem(ItemSlot slot)
        {
            //���ȿ���� ���ؼ� 
            Debug.Log("Equipping " + slot.slotItem.itemName);
        }

        private void PlaceItem(ItemSlot slot)
        {
            //���ȿ���� ���ؼ� 
            Debug.Log("Placing " + slot.slotItem.itemName);
        }

        private void DefaultItemUse(ItemSlot slot)
        {
            //���ȿ���� ���ؼ� 
            Debug.Log($"Using {slot.slotItem.itemName}.");
        }


        //Returns the item from itemList at index.
        public Item GetItemByIndex(int index)
        {
            //���ȿ���� ���ؼ� 
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
