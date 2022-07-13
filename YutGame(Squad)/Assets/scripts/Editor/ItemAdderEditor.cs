using UnityEngine;
using UnityEditor;

namespace XEntity
{
    //이것은 ItemManager.itemList에 선택한 항목 스크립팅 가능 개체를 자동으로 추가하는 편집기 스크립트입니다.
    //참고: 이렇게 하면 목록에 중복 항목이 추가되는 것을 방지할 수 있으며 항목 유형이 아닌 선택된 항목은 무시됩니다.
    //참고: 장면에 ItemManager가 있어야 합니다.

    //조재훈 : 이 스크립트는 Editor에 관한 내용일 뿐이다. 즉 Unity Custom일 뿐이란 이야기.

    public class ItemAdderEditor : Editor
    {
        [MenuItem("Assets/Add To Item List")]
        public static void AddItemToList()
        {
            ItemManager manager = FindObjectOfType<ItemManager>();
            if (manager == null)
            {
                Debug.Log("<color=#ff8080>Item Manager does not exist in the current scene</color>");
                return;
            }
            else
            {
                //현재 선택한 모든 개체를 살펴보고 적합한 항목을 목록에 추가합니다.
                //자세한 설명: foreach(obj
                foreach (Object obj in Selection.objects)
                {
                    //만약 obj가 Item스크립트를 가지고있다면
                    if (obj.GetType() == typeof(Item))
                    {
                        //obj의 item script를 아이템 변수에 넣어줌
                        Item item = (Item)obj;
                        //만약 ItemManager Script를 가진 변수가 List에 item이 있다면
                        if (manager.itemList.Contains(item))
                        {
                            //여기서 실행해주는 구문을 넣어주면 될 듯 하다.
                            Debug.Log($"<color=#80aaff>{item.name} already exists in the list.</color>");
                        }
                        //없다면
                        else
                        {
                            //그 item을 List에 추가한다
                            manager.itemList.Add(item);
                            //그리고 여기에 실행해줄 구문을 넣어주면 될 듯하다.
                            Debug.Log($"<color=#ccff99>{item.name} succesfully added to the list!</color>");
                        }
                    }
                    //만약 obj가 item스크립트를 가지고있지 않다면
                    else
                    {
                        //실패 구문 또는 강제해줄 구문을 넣어주면 될 듯 하다.
                        Debug.Log($"<color=#ffd480>{obj.name} is not an item</color>");
                    }
                }
            }
        }
    } 
}
