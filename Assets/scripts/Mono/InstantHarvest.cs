namespace XEntity
{

    //
    //이 스크립트는 작은 바위와 막대기와 같은 클릭 한 번으로 인터랙터에 의해 픽업되는 모든 항목에 첨부됩니다.
    //참고: 항목은 상호 작용이 상호 작용 범위 내에 있는 경우에만 추가됩니다.
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
