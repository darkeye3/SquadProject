using UnityEngine;

namespace XEntity
{
    //This class is attached to the player.
    //This holds the different types of interaction events and interaction trigger methods.
    public class Interactor : MonoBehaviour
    {
        //The minimum distance to an object in order to interact.
        [SerializeField] private float interactionRange;

        //Reference to the main game viewing camera.
        [SerializeField] private Camera mainCamera;

        //Reference to the item container thats dedicated to this interactor.
        [SerializeField] private ItemContainer inventory;

        //Reference to the current interactable target.
        //This is null if there are no valid target interactable objects. 
        private Interactable interactionTarget;

        //Activating the range indicator, draws a wire sphere to indicate interaction range in the editor.
        [Header("Settings")]
        public bool drawRangeIndicator;

        //This is the color target interactable objects are highlighted.
        //The interactable objects must have a mesh renderer with a valid material in order to be highlighted.
        public Color interactableHighlight = Color.white;

        //This is the position at which dropped items will be instantiated (in front of this interactor).
        public Vector3 ItemDropPosition { get { return transform.position + transform.forward; } }

        //Called every frame after the game is started.
        private void Update()
        {
            HandleInteractions();
        }

        //This method draws gizmos in the editor.
        private void OnDrawGizmos() 
        {
            if (drawRangeIndicator) 
            {
                Gizmos.DrawWireSphere(transform.position, interactionRange);
            }
        }

        //이 방법은 대화형 객체 감지, 대화 트리거 및 대화형 이벤트 콜백을 처리합니다.
        private void HandleInteractions()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);                     //내 마우스에 광선
            RaycastHit hit;                                                     

            if (interactionTarget)                                                          //
            {
                Utils.UnhighlightObject(interactionTarget.gameObject);   
            }
            
            if (Physics.Raycast(ray, out hit) && InRange(hit.transform.position))
            {
                Interactable target = hit.transform.GetComponent<Interactable>();
                if (target != null)
                {
                    interactionTarget = target;
                    Utils.HighlightObject(interactionTarget.gameObject, interactableHighlight);
                }
                else interactionTarget = null;
            }
            else
            {
                interactionTarget = null;
            }

            if (Input.GetKeyDown(KeyCode.F)) InitInteraction();
        }

        //대상 위치가 교호작용 범위 내에 있으면 참이 반환됩니다.
        private bool InRange(Vector3 targetPosition)
        {
            //이게 무슨 의미인지 모르겠으나 제 뇌피셜에 의하면
            //1. targetPosition, transform.position의 일직선거리의 길이가 interactionRange보다 짧으면 반환
            //찾아봐야함 나도 잘 모르겠음.
            return Vector3.Distance(targetPosition, transform.position) <= interactionRange;
        }

        //이 메서드는 유효한 상호 작용 대상인 경우 이 상호 작용기와의 상호 작용을 초기화합니다.
        private void InitInteraction() 
        {
            if (interactionTarget == null) return;
            interactionTarget.OnInteract(this);
        }

        //이 메서드는 이 인터랙터의 인벤토리에 항목을 추가하고 해당되는 경우 항목의 실제 인스턴스를 삭제합니다.
        public void AddToInventory(Item item, GameObject instance)
        {
            if (inventory.AddItem(item)) 
                if(instance) StartCoroutine(Utils.TweenScaleOut(instance, 50, true));
        }
    }
}
