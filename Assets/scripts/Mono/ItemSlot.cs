using UnityEngine;

namespace XEntity
{
    //�� ��ũ��Ʈ�� �׸��� ������ �� �ִ� ��� �����̳� ���Կ� ����˴ϴ�.
    public class ItemSlot : MonoBehaviour
    {
        //���� �� ���Կ� �ִ� �׸�. ������ null�Դϴ�.
        public Item slotItem;

        //�� ������ �׸� ��.
        public int itemCount;

        //itemCount�� 0�̸� true�� ��ȯ�մϴ�.
        public bool IsEmpty { get { return itemCount <= 0; } }

        //�׸� �������� ǥ���ϱ� ���� �̹����Դϴ�.
        private UnityEngine.UI.Image iconImage;

        //�׸� ���� ǥ���ϴ� �ؽ�Ʈ�Դϴ�.
        private UnityEngine.UI.Text countText;

        private void Awake() 
        {
            //UI ������ ���⿡ �Ҵ�˴ϴ�.
            iconImage = transform.Find("Icon Image").GetComponent<UnityEngine.UI.Image>();      //Icon Image��� Object�� ã�� ����Ƽ����.Image�� �־���
            countText = transform.Find("Count Text").GetComponent<UnityEngine.UI.Text>();       //Count Text��� Object�� ã�� ����Ƽ����.Text�� �־���

            iconImage.gameObject.SetActive(false);                                              //���� ������Ʈ ��Ȱ��ȭ
            countText.text = string.Empty;                                                      //�ؽ�Ʈ�� ����ְ� �ʱ�ȭ ������
        }

        //���Կ� �׸��� �߰��� �� ������ true�� ��ȯ�մϴ�.
        //����: �׸��� ���� �����̸� ������ ���Դϴ� (A�̹��� string "x2" �̷����ε�?)
        public bool Add(Item item) 
        {
            //���� Item Script�� �������ִٸ� 
            if (IsAddable(item))
            {   
                //���� �Ű��������� ������ �־��ְ�
                slotItem = item;
                //������ī��Ʈ���� (�迭����) �߰�
                itemCount++;
                //
                OnSlotModified();
                return true;
            }
            else return false; 
            
        }

        //���Կ��� ���޵� �׸��� ���� �����ϰ� ��� ��ġ�� �����ϴ�.
        public void RemoveAndDrop(int amount, Vector3 dropPosition) 
        {
            for (int i = 0; i < amount; i++) 
            {
                if (itemCount > 0)
                {
                    //Utils �ʹ� �����
                    Utils.InstantiateItemCollector(slotItem, dropPosition);
                    itemCount--;
                    
                }
                else break;
            }

            OnSlotModified();
        }

        //���Կ��� ���޵� �׸��� ���� �����մϴ�.
        public void Remove(int amount)
        {
            //?������(���� ������)�� ���ؼ� ã�ƺ� ��ȸ
            //amount�� itemCount���� ũ�ٸ� ? True�� itemCount False�� amount �� ��ȯ      (�ް����� �κ��� True��� �ؼ� 1 False��� �ؼ� 0�� ��ȯ�ϴ°��� �ƴ�)
            itemCount -= amount > itemCount ? itemCount : amount;
            OnSlotModified();
        }

        //������ ������ ���ϴ�. (�ʱ�ȭ ����)
        public void Clear() 
        {
            itemCount = 0;
            OnSlotModified();
        }

        //������ ������ ���� ��� �׸��� ��� ��ġ�� �����ϴ�.
        public void ClearAndDrop(Vector3 dropPosition) 
        {
            RemoveAndDrop(itemCount, dropPosition);
        }

        //�߰� �׸� ���� ��� ������ �����Ǹ� true�� ��ȯ�մϴ�.
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

        //�� �޼���� ������ ������ ������ ������ ȣ��˴ϴ�.
        private void OnSlotModified() 
        {
            //���࿡ IsEmpty�� False�� (Property�� �̷�����ְ� 0���� �Ʒ��� True 1�̻��̸� False)
            if (!IsEmpty)
            {
                //�̹����� ��ȯ���� (�������� �����ϴ� ���ڷ� ���ϴ� �̹����� �־���)
                iconImage.sprite = slotItem.icon;
                //�ؽ�Ʈ�� �� ������ �̸����� �ٲ���
                countText.text = itemCount.ToString();
                //�̹����� Ȱ��ȭ ������
                iconImage.gameObject.SetActive(true);
            }
            //isEmpty�� True�� (0�Ʒ��� ���)
            else 
            {
                //0���� �ʱ�ȭ
                itemCount = 0;
                //������ ����� (0���� ����� ���� �ƴ�)
                slotItem = null;
                //�̹��� ����� 
                iconImage.sprite = null;
                //�ؽ�Ʈ�� �� ������
                countText.text = string.Empty;
                //�������̹����� ��Ȱ��ȭ
                iconImage.gameObject.SetActive(false);
            }
        }


        //���� Ȯ�� ���� �׸� �� �׸� ���� ���� �Ҵ��մϴ�.
        //����: �̰��� ��� ���� �����͸� ������ ���� ����ؾ� �մϴ�.
        //������ : �����͸� �������ֱ� ���� (Item��ũ��Ʈ�� �������ִ� ������ �޾ƿ�)
        public void SetData(Item item, int count)
        {
            slotItem = item;
            itemCount = count;
            OnSlotModified();
        }
    }
}
