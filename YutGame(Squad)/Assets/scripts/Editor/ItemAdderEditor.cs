using UnityEngine;
using UnityEditor;

namespace XEntity
{
    //�̰��� ItemManager.itemList�� ������ �׸� ��ũ���� ���� ��ü�� �ڵ����� �߰��ϴ� ������ ��ũ��Ʈ�Դϴ�.
    //����: �̷��� �ϸ� ��Ͽ� �ߺ� �׸��� �߰��Ǵ� ���� ������ �� ������ �׸� ������ �ƴ� ���õ� �׸��� ���õ˴ϴ�.
    //����: ��鿡 ItemManager�� �־�� �մϴ�.

    //������ : �� ��ũ��Ʈ�� Editor�� ���� ������ ���̴�. �� Unity Custom�� ���̶� �̾߱�.

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
                //���� ������ ��� ��ü�� ���캸�� ������ �׸��� ��Ͽ� �߰��մϴ�.
                //�ڼ��� ����: foreach(obj
                foreach (Object obj in Selection.objects)
                {
                    //���� obj�� Item��ũ��Ʈ�� �������ִٸ�
                    if (obj.GetType() == typeof(Item))
                    {
                        //obj�� item script�� ������ ������ �־���
                        Item item = (Item)obj;
                        //���� ItemManager Script�� ���� ������ List�� item�� �ִٸ�
                        if (manager.itemList.Contains(item))
                        {
                            //���⼭ �������ִ� ������ �־��ָ� �� �� �ϴ�.
                            Debug.Log($"<color=#80aaff>{item.name} already exists in the list.</color>");
                        }
                        //���ٸ�
                        else
                        {
                            //�� item�� List�� �߰��Ѵ�
                            manager.itemList.Add(item);
                            //�׸��� ���⿡ �������� ������ �־��ָ� �� ���ϴ�.
                            Debug.Log($"<color=#ccff99>{item.name} succesfully added to the list!</color>");
                        }
                    }
                    //���� obj�� item��ũ��Ʈ�� ���������� �ʴٸ�
                    else
                    {
                        //���� ���� �Ǵ� �������� ������ �־��ָ� �� �� �ϴ�.
                        Debug.Log($"<color=#ffd480>{obj.name} is not an item</color>");
                    }
                }
            }
        }
    } 
}
