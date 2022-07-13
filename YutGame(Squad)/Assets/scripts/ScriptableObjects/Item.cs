using UnityEngine;

namespace XEntity
{
    //�׸� ���� ������ ���ø��� �����ϴ� ��ũ��Ʈ ���� ��ü�Դϴ�.
    [CreateAssetMenu(fileName = "New Item", menuName = "XEntity/Item")]
    public class Item : ScriptableObject
    {
        //**�߿�**
        //"�ʼ� ����"���� ���� �Ӽ��� �����ϰų� �������� ���ʽÿ�.
        //�ٸ� ���� �ڵ带 ����� �����Ͽ� �۾��� ��츦 �����ϰ�.
        //�ʼ� ���� �ڿ� ���ϴ� ��ŭ �Ӽ��� �߰��� �� �ֽ��ϴ�.

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
