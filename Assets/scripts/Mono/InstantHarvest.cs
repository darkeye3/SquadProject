namespace XEntity
{

    //
    //�� ��ũ��Ʈ�� ���� ������ ������ ���� Ŭ�� �� ������ ���ͷ��Ϳ� ���� �Ⱦ��Ǵ� ��� �׸� ÷�ε˴ϴ�.
    //����: �׸��� ��ȣ �ۿ��� ��ȣ �ۿ� ���� ���� �ִ� ��쿡�� �߰��˴ϴ�.
    public class InstantHarvest : Interactable
    {
        //The item that will be harvested on click.
        public Item harvestItem;

        //The item is instantly added to the inventory of the interactor on interact.
        public override void OnInteract(Interactor interactor)
        {
            interactor.AddToInventory(harvestItem, gameObject);
        }
    }
}
