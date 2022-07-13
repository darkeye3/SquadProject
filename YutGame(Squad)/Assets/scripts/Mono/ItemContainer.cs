using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace XEntity
{
    //�� ��ũ��Ʈ�� �׸� �����̳��� UI ǥ���� ����˴ϴ�. ���� �׸� �����̳� UI �������� �ڻ�� �Բ� �����˴ϴ�.
    public class ItemContainer : MonoBehaviour
    {
        //�ݼ��Ĵ� �� �׸� �����̳ʰ� �Ҵ�� ��ȣ �ۿ���Դϴ�. �������� �װ��� �Ϲ����� ���� ����̴�.
        public Interactor carrier;

        //�� Ű�� ������ �׸� �����̳��� UI�� �����ų� �����ϴ�.
        public KeyCode UIToggleKey = KeyCode.I;

        //�� ��� �׸��� �� �����̳ʿ��� ���ŵǸ� �ش� �׸��� �����̳� �տ� �������ϴ�.
        [Tooltip("���� ��� �׸��� ������ �� �ش� �������� ĳ���� ��ó ��鿡�� �ν��Ͻ�ȭ�˴ϴ�.")]
        public bool dropRemovedItemPrefabs = true;

        //�� �����̳ʿ� ��� �ִ� ���� �迭�Դϴ�. ������ ���� Ȧ�� ��ȯ�� ���Ե� �ڽ� ���� �������� �ڵ带 ���� �Ҵ�˴ϴ�.
        private ItemSlot[] slots;

        //�����̳��� UI, �����̳�UI ���ø� �������� �� �ڻ�� �Բ� �����˴ϴ�. ��� �����̳� UI�� ��Ȯ�� ���� ������� �����ؾ� �մϴ�.
        //���� ���� �����Ϸ��� ��� ������ ���� Ȧ�� �ڽ� ���� �����մϴ�.UI��.
        private Transform containerUI;

        //������ Ŭ���ϸ� ��Ÿ���� �ɼ� UI�Դϴ�. �� UI�� ���� ���� �� ���ø��� �� �ڻ�� �Բ� �����˴ϴ�.
        private GameObject slotOptionsUI;

        //�׸� ��� �����̴�.
        private Button itemUseButton;
        //[�׸� ����] ��ư �����̴�.
        private Button itemRemoveButton;
        
        //new
        private Button itemInstructionButton;
        //new
        private UnityEngine.UI.Image instructionImage;
        //new
        private UnityEngine.UI.Text instructionText;
        bool isInventory = false;
        

        protected virtual void Awake()
        {
            //The container is initilized on awake.
            InitContainer();

            //new
           // instructionImage = transform.Find("Instruction Image").GetComponent<UnityEngine.UI.Image>();
            //new
           // instructionText = transform.Find("Instruction Text").GetComponent<UnityEngine.UI.Text>();
            //new
            
        }

        protected virtual void Update()
        {
            //Check for the toggle key and update the toggle state.
            if (Input.GetKeyDown(UIToggleKey)) 
            {
                ToggleUI();

                if(isInventory == false)
                    {
                    GameObject.Find("Canvas").transform.Find("Reticle").gameObject.SetActive(false);
                    isInventory = true;
                    
                    }
                else
                    {
                    GameObject.Find("Canvas").transform.Find("Reticle").gameObject.SetActive(true);
                    isInventory = false;
                    }
            }
        }

        //��� �����̳� ������ �����̳ʸ� �������� ���⿡ �Ҵ�˴ϴ�.
        protected virtual void InitContainer()
        {
            containerUI = transform.Find("Inventory UI");
            slotOptionsUI = transform.Find("Slot Options").gameObject;
            itemUseButton = slotOptionsUI.transform.Find("Use Button").GetComponent<Button>();
            itemRemoveButton = slotOptionsUI.transform.Find("Remove Button").GetComponent<Button>();
            //new
            itemInstructionButton = slotOptionsUI.transform.Find("Instruction").GetComponent<Button>();

            Transform slotHolder = containerUI.Find("Slot Holder");
            slots = new ItemSlot[slotHolder.childCount];
            for (int i = 0; i < slots.Length; i++)
            {
                ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
                slots[i] = slot;
                slot.GetComponent<Button>().onClick.AddListener(delegate { OnSlotClicked(slot); });
            }

            slotOptionsUI.SetActive(false);
            containerUI.gameObject.SetActive(false);
        }

        //�����̳ʿ� �׸��� �߰��� �� ������ true�� ��ȯ�մϴ�.
        public bool AddItem(Item item)
        {
            for (int i = 0; i < slots.Length; i++) if (slots[i].Add(item)) return true;
            return false;
        }

        //�����̳ʿ� ���޵� �׸��� ���ԵǾ� ������ true�� ��ȯ�մϴ�.
        public bool ContainsItem(Item item)
        {
            for (int i = 0; i < slots.Length; i++)
                if (slots[i].slotItem == item) return true;
            return false;
        }

        //�����̳ʿ� ���޵� �׸��� ���� ���ԵǾ� ������ true�� ��ȯ�մϴ�.
        public bool ContainsItemQuantity(Item item, int amount)
        {
            int count = 0;
            foreach (ItemSlot slot in slots)
            {
                if (slot.slotItem == item) count += slot.itemCount;
                if (count >= amount) return true;
            }
            return false;
        }

        //UI ��� ���¸� ������Ʈ�մϴ�.
        private void ToggleUI()
        {
            slotOptionsUI.SetActive(false);

            //UI ��/������ �̵��մϴ�. �̰� ��Ȯ�� ����?
            if (containerUI.gameObject.activeSelf)
            {
                //�ڷ�ƾ ���� Utils�̰� �� �����ؼ� �� �𸣰���
                StartCoroutine(Utils.TweenScaleOut(containerUI.gameObject, 50, false));
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
            else
            {
                StartCoroutine(Utils.TweenScaleIn(containerUI.gameObject, 50, Vector3.one));
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
        }

        //������ Ŭ���ϸ� �� �޼��尡 ȣ��˴ϴ�.
        //���� �ɼ� ��ư�� ���űⰡ �������� ������ ���Կ� ���� �ٽ� �Ҵ�˴ϴ�.
        //������ : �����ϰ� Event��� �����ϸ� �ȴ� (Delegate�� event, onClickListener �˻� ��õ (�߰��� "����"�� delegate���� ���־���))
        //������ : �� �� �Լ��� Ư�� ������ ��ư�� ������ "���"���� "����"���� "itemInstruction"�� ������ ���� ��ư���� �̺�Ʈ��� �����ϸ� �ȴ�.
        private void OnSlotClicked(ItemSlot slot)
        {
            itemUseButton.onClick.RemoveAllListeners();
            //��ư������ �����ۻ�� ���ְ� UI��Ȱ��ȭ ����
            itemUseButton.onClick.AddListener(delegate { OnItemUseClicked(slot); slotOptionsUI.SetActive(false); });

            itemRemoveButton.onClick.RemoveAllListeners();
            itemRemoveButton.onClick.AddListener(delegate { OnRemoveItemClicked(slot); slotOptionsUI.SetActive(false); });

            itemInstructionButton.onClick.RemoveAllListeners();
            itemInstructionButton.onClick.AddListener(delegate {OnInstructionClicked(slot); slotOptionsUI.SetActive(false);});

            slotOptionsUI.transform.position = Input.mousePosition;
            StartCoroutine(Utils.TweenScaleIn(slotOptionsUI, 50, Vector3.one));
        }

        //This is the listener for the itemRemoveButton click event.

        //new
        private void OnInstructionClicked(ItemSlot slot)
        {

        }

        //�����ۻ���Event �Լ� (���� or ������ Drop)
        //�켱 Remove�� ���� Button�� ������ ����Ǿ��� �Լ� ����
        //������ : �̰� MMORPG���� ���Ϲ��� �κ��丮 ��������. ������ �����°� �³���?
        private void OnRemoveItemClicked(ItemSlot slot)
        {
            //�� �������� Ȱ��ȭ�Ǿ��ִ��� �¾Ҵٸ� 
            if (dropRemovedItemPrefabs)
            {
                //�� �����ۿ� ���� N(����1)�� �κ��丮���� ����, ĳ����(?)�ؿ��ٰ� ������ ����Ʈ����. ���ƿ� (�����غ��� �ʾ� ��Ȯ���� ����)
                slot.RemoveAndDrop(1, carrier.ItemDropPosition);
            }
            //dropRemovedItemPrefabs�� False���
            else
            { 
                //�׳� ����
                slot.Remove(1); 
            }
        }
        //�� OnRemoveItemClicked�� �������� ����Ʈ���� �κ��丮���� ��������
        //�ƴϸ� �׳� �κ��丮���� ����������. ������ �Ӽ��� ���� �ٸ� ������ �ϴ°� ����.
        //

        //UseButton Ŭ�� �̺�Ʈ�Դϴ�.
        private void OnItemUseClicked(ItemSlot slot) 
        {
            ItemManager.Instance.UseItem(slot);
        }

        //�Ʒ��� JSON ��ƿ��Ƽ�� ����Ͽ� �����̳� �����͸� ����/����/�����ϱ� ���� �ڵ��Դϴ�.
        //�̰� �Ƹ� ������������ �� ���Ұ� ���ƿ�. JSON�� ���� ���� ������ ���� ų�� �����͸� �����ϴµ� ���̴� �뵵�� �̴ϴ�.
        #region Saving & Loading Data

        //This method saves the container data on an unique file path that is aquired based on the passed in id.
        //This id should be unique for different saves.
        //If a save already exists with the id, the data will be overwritten.
        public void SaveData(string id) 
        {
            //An unique file path is aquired here based on the passed in id. 
            string dataPath = GetIDPath(id);

            if (System.IO.File.Exists(dataPath))
            {
                System.IO.File.Delete(dataPath);
                Debug.Log("Exisiting data with id: " + id +"  is overwritten.");
            }

            try 
            {
                Transform slotHolder = containerUI.Find("Slot Holder");
                SlotInfo info = new SlotInfo();
                for (int i = 0; i < slotHolder.childCount; i++) 
                {
                    ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
                    if (!slot.IsEmpty)
                    {
                        info.AddInfo(i, ItemManager.Instance.GetItemIndex(slot.slotItem), slot.itemCount);
                    }
                }
                string jsonData = JsonUtility.ToJson(info);
                System.IO.File.WriteAllText(dataPath, jsonData);
                Debug.Log("<color=green>Data succesfully saved! </color>");
            } 
            catch 
            {
                Debug.LogError("Could not save container data! Make sure you have entered a valid id and all the item scriptable objects are added to the ItemManager item list");
            }
        }

        //Loads container data saved with the passed in id.
        //NOTE: A save file must exist first with the id in order for it to be loaded.
        public void LoadData(string id) 
        {
            string dataPath = GetIDPath(id);

            if (!System.IO.File.Exists(dataPath)) 
            {
                Debug.LogWarning("No saved data exists for the provided id: " + id);
                return;
            }

            try 
            {
                string jsonData = System.IO.File.ReadAllText(dataPath);
                SlotInfo info = JsonUtility.FromJson<SlotInfo>(jsonData);

                Transform slotHolder = containerUI.Find("Slot Holder");
                for (int i = 0; i < info.slotIndexs.Count; i++)
                {
                    Item item = ItemManager.Instance.GetItemByIndex(info.itemIndexs[i]);
                    slotHolder.GetChild(info.slotIndexs[i]).GetComponent<ItemSlot>().SetData(item, info.itemCounts[i]);
                }
                Debug.Log("<color=green>Data succesfully loaded! </color>");
            }
            catch
            {
                Debug.LogError("Could not load container data! Make sure you have entered a valid id and all the item scriptable objects are added to the ItemManager item list.");
            }
        }

        //Deletes the save with the passed in id, if one exists.
        public void DeleteData(string id) 
        {
            string path = GetIDPath(id);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                Debug.Log("Data with id: " + id + " is deleted.");
            }
        }

        //Returns a unique path based on the id.
        protected virtual string GetIDPath(string id) 
        {
            return Application.persistentDataPath + $"/{id}.dat";
        }

        //This struct contains the data for the container slots; used for saving/loading the container slot data.
        public class SlotInfo
        {
            public List<int> slotIndexs;
            public List<int> itemIndexs;
            public List<int> itemCounts;

            public SlotInfo() 
            {
                slotIndexs = new List<int>();
                itemIndexs = new List<int>();
                itemCounts = new List<int>();
            }

            public void AddInfo(int slotInex, int itemIndex, int itemCount) 
            {
                slotIndexs.Add(slotInex);
                itemIndexs.Add(itemIndex);
                itemCounts.Add(itemCount);
            }
            
        }
        #endregion
    }
}
