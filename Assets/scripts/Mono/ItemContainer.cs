using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace XEntity
{
    //이 스크립트는 항목 컨테이너의 UI 표현에 연결됩니다. 예제 항목 컨테이너 UI 프리탭이 자산과 함께 제공됩니다.
    public class ItemContainer : MonoBehaviour
    {
        //반송파는 이 항목 컨테이너가 할당된 상호 작용기입니다. 선수에게 그것은 일반적인 선수 목록이다.
        public Interactor carrier;

        //이 키를 누르면 항목 컨테이너의 UI가 켜지거나 꺼집니다.
        public KeyCode UIToggleKey = KeyCode.I;

        //이 경우 항목이 이 컨테이너에서 제거되면 해당 항목이 컨테이너 앞에 떨어집니다.
        [Tooltip("참일 경우 항목을 제거할 때 해당 프리탭이 캐리어 근처 장면에서 인스턴스화됩니다.")]
        public bool dropRemovedItemPrefabs = true;

        //이 컨테이너에 들어 있는 슬롯 배열입니다. 슬롯은 슬롯 홀더 변환에 포함된 자식 수를 기준으로 코드를 통해 할당됩니다.
        private ItemSlot[] slots;

        //컨테이너의 UI, 컨테이너UI 템플릿 프리탭은 이 자산과 함께 제공됩니다. 모든 컨테이너 UI는 정확히 같은 방식으로 설정해야 합니다.
        //슬롯 수를 수정하려면 용기 내부의 슬롯 홀더 자식 수를 수정합니다.UI가.
        private Transform containerUI;

        //슬롯을 클릭하면 나타나는 옵션 UI입니다. 이 UI에 대한 사전 탭 템플릿은 이 자산과 함께 제공됩니다.
        private GameObject slotOptionsUI;

        //항목 사용 변수이다.
        private Button itemUseButton;
        //[항목 제거] 버튼 변수이다.
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

        //모든 컨테이너 변수는 컨테이너를 기준으로 여기에 할당됩니다.
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

        //컨테이너에 항목을 추가할 수 있으면 true를 반환합니다.
        public bool AddItem(Item item)
        {
            for (int i = 0; i < slots.Length; i++) if (slots[i].Add(item)) return true;
            return false;
        }

        //컨테이너에 전달된 항목이 포함되어 있으면 true를 반환합니다.
        public bool ContainsItem(Item item)
        {
            for (int i = 0; i < slots.Length; i++)
                if (slots[i].slotItem == item) return true;
            return false;
        }

        //컨테이너에 전달된 항목의 양이 포함되어 있으면 true를 반환합니다.
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

        //UI 토글 상태를 업데이트합니다.
        private void ToggleUI()
        {
            slotOptionsUI.SetActive(false);

            //UI 안/밖으로 이동합니다. 이게 정확히 뭘까?
            if (containerUI.gameObject.activeSelf)
            {
                //코루틴 실행 Utils이게 좀 난잡해서 잘 모르겠음
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

        //슬롯을 클릭하면 이 메서드가 호출됩니다.
        //슬롯 옵션 버튼의 수신기가 지워지고 선택한 슬롯에 따라 다시 할당됩니다.
        //조재훈 : 간단하게 Event라고 생각하면 된다 (Delegate와 event, onClickListener 검색 추천 (추가로 "람다"도 delegate에서 자주쓰임))
        //조재훈 : 즉 이 함수는 특정 아이템 버튼을 누르면 "사용"할지 "삭제"할지 "itemInstruction"을 할지에 대한 버튼생성 이벤트라고 생각하면 된다.
        private void OnSlotClicked(ItemSlot slot)
        {
            itemUseButton.onClick.RemoveAllListeners();
            //버튼누르면 아이템사용 해주고 UI비활성화 해줌
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

        //아이템삭제Event 함수 (삭제 or 아이템 Drop)
        //우선 Remove에 관한 Button을 누를시 실행되어줄 함수 같음
        //조재훈 : 이거 MMORPG에서 쓰일법한 인벤토리 같은데요. 아이템 떨구는거 맞나요?
        private void OnRemoveItemClicked(ItemSlot slot)
        {
            //그 아이템이 활성화되어있던게 맞았다면 
            if (dropRemovedItemPrefabs)
            {
                //그 아이템에 관해 N(현재1)개 인벤토리에서 삭제, 캐릭터(?)밑에다가 아이템 떨어트리기. 같아요 (실행해보지 않아 정확하지 않음)
                slot.RemoveAndDrop(1, carrier.ItemDropPosition);
            }
            //dropRemovedItemPrefabs가 False라면
            else
            { 
                //그냥 삭제
                slot.Remove(1); 
            }
        }
        //즉 OnRemoveItemClicked는 아이템을 떨어트리고 인벤토리에서 삭제할지
        //아니면 그냥 인벤토리에서 삭제해줄지. 아이템 속성에 따라 다른 행위를 하는것 같음.
        //

        //UseButton 클릭 이벤트입니다.
        private void OnItemUseClicked(ItemSlot slot) 
        {
            ItemManager.Instance.UseItem(slot);
        }

        //아래는 JSON 유틸리티를 사용하여 컨테이너 데이터를 저장/적재/삭제하기 위한 코드입니다.
        //이건 아마 나영동생분이 더 잘할것 같아요. JSON을 쓰는 것은 게임을 껏다 킬때 데이터를 저장하는데 쓰이는 용도일 겁니다.
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
