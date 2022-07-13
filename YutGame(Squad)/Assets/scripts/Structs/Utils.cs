using System.Collections;
using UnityEngine;

namespace XEntity
{
    //Utils스크립트 이건 진짜 좀 어렵네요. 저도 잘 모르겠습니다. 대충번역만 해봤어요.
    //그런데 확실하지 않지만 우린 여길 건들일 필요가 전혀 없어요. 새로운 기능을 만들것이 아닌 이상은요. 나도 여기서 못만져요.

    //이 구조에는 인벤토리 시스템, 객체 조정 및 객체 강조 표시에 대한 유틸리티 기능이 포함되어 있습니다.
    //즉 이 스크립트는 한곳이 아닌 다양하게 쓰일 수 있는 스크립트입니다.
    public readonly struct Utils
    {
        /*
        이 메서드는 트리거 ItemSlot에서 대상 ItemSlot으로 항목을 전송하려고 합니다.
        항목 유형이 동일하면 대상 ItemSlot의 최대 용량에 도달할 때까지 쌓입니다.
        항목 유형이 다르면 위치가 바뀝니다.
        */

        //제가 보기엔? Shuffle할려고 하는거 같은데 저도 정확하겐 모르겠음.
        //Shuffle은 A와 B의 Object자리를 B와 A로 교환해주는 것을 의미함. 
        public static void TransferItem(ItemSlot trigger, ItemSlot target)
        {
            //만약 A매개변수와 B매개변수가 같다면
            if (trigger == target)
            {
                //이 함수(TransferItem)를 종료
                return;
            }

            //
            Item triggerItem = trigger.slotItem;
            Item targetItem = target.slotItem;

            int triggerItemCount = trigger.itemCount;

            //만약 아이템슬릇 스크립트가 True (즉 IsEmpty가 0이하라면)
            if (!trigger.IsEmpty)
            {
                //만약 0이하거나 특정아이템과 타겟아이템이 같다면
                if (target.IsEmpty || targetItem == triggerItem)
                {
                    //갯수만큼 반복
                    for (int i = 0; i < triggerItemCount; i++)
                    {
                        //만약 B매개변수 추가를한다면
                        if (target.Add(triggerItem))
                        {
                            //A매개변수 삭제
                            trigger.Remove(1);
                        }
                        //아니라면
                        else
                        {
                            //종료
                            return;
                        }
                    }
                }
                //0이하거나 특정아이템과 타겟아이템이 같지않다면
                else
                {
                    //
                    int targetItemCount = target.itemCount;

                    //싹다 초기화
                    target.Clear();
                    for (int i = 0; i < triggerItemCount; i++) target.Add(triggerItem);

                    //싹다초기화
                    trigger.Clear();
                    for (int i = 0; i < targetItemCount; i++) trigger.Add(targetItem);
                }
            }
        }

        //이 메서드는 전달된 항목의 양을 트리거 ItemSlot에서 대상 ItemSlot으로 전송하려고 시도합니다.
        public static void TransferItemQuantity(ItemSlot trigger, ItemSlot target, int amount) 
        {
            //isEmpty가 False면
            if (!trigger.IsEmpty) 
            {
                for (int i = 0; i < amount; i++)
                {
                    if (!trigger.IsEmpty)
                    {
                        if (target.Add(trigger.slotItem))
                        {
                            trigger.Remove(1);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        /* 
            이 코루틴은 Vector3의 척도에서 전달된 obj의 척도로 축척됩니다.durationInFrames의 범위에서 전달된 maxScale에 대해 0입니다.
            참고: 이것은 MonoBehavior 스크립트에서 호출해야 하며 StartCoroutine()을 사용하여 호출해야 합니다.
            예: 시작코루틴(GameUtility)입니다.TweenScaleIn(예: GameObject, 40, Vector 3.1);
        */
        public static IEnumerator TweenScaleIn(GameObject obj, float durationInFrames, Vector3 maxScale) 
        {
            Transform tf = obj.transform;
            tf.localScale = Vector3.zero;
            tf.gameObject.SetActive(true);

            float frame = 0;
            while (frame < durationInFrames) 
            {
                tf.localScale = Vector3.Lerp(Vector3.zero, maxScale, frame / durationInFrames);
                frame++;
                yield return null;
            }
        }

        /* 
            이 코루틴은 전달된 오브젝트를 원래의 스케일에서 벡터 3의 스케일로 스케일아웃한다.지속 시간 범위(InFrames)에서 0.
            파괴가 참일 경우 개체는 확장 후 파괴됩니다.
            파괴가 false인 경우 개체의 활성 상태는 확장 후 false로 설정됩니다.
            참고: 이것은 MonoBehavior 스크립트에서 호출해야 하며 StartCoroutine()을 사용하여 호출해야 합니다.
            예: 시작코루틴(GameUtility)입니다.TweenScaleOut(예: GameObject, 40, true);
        */
        public static IEnumerator TweenScaleOut(GameObject obj, float durationInFrames, bool destroy)
        {
            float frame = 0;
            while (frame < durationInFrames)
            {
                if (obj != null)
                {
                    obj.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, frame / durationInFrames);
                }
                frame++;
                yield return null;
            }
            if (obj)
            {
                if (!destroy) obj.SetActive(false);
                else GameObject.Destroy(obj);
            }
        }

        //전달된 위치에서 전달된 항목으로 항목 수집기 개체를 인스턴스화합니다.
        //예를 들어 인벤토리에서 항목을 제거할 때 제거된 항목이 있는 항목 수집기가 플레이어 앞에 놓이는 경우가 있습니다.
        public static void InstantiateItemCollector(Item item, Vector3 position) 
        {
            Vector3 targetSize = Vector3.one * 0.5f;
            GameObject inst = GameObject.Instantiate(item.prefab, position, Quaternion.identity);
            float maxSizeComponent = MaxVec3Component(inst.GetComponent<MeshRenderer>().bounds.size);

            inst.transform.localScale = inst.transform.localScale * (MaxVec3Component(targetSize) / maxSizeComponent);

            var interactable = inst.GetComponent<Interactable>();
            if (interactable != null) GameObject.Destroy(interactable);

            inst.GetComponent<Collider>().isTrigger = true;
            inst.AddComponent<ItemCollector>().Create(item);
        }

        //Returns the maximum of the three components of the passed in Vector3.
        public static float MaxVec3Component(Vector3 vec) 
        {
            return Mathf.Max(Mathf.Max(vec.x, vec.y), vec.z);
        }

        /*
         * Highlights the passed in obj with the passed in highlightColor.
         * NOTE: The object must have a mesh renderer with a valid material in order to be highlighted.
         */
        public static void HighlightObject(GameObject obj, Color highlightColor) 
        {
            obj.GetComponent<MeshRenderer>().material.color = highlightColor;
        }


        /*
         * Unhighlights the passed in obj by setting the color to the original color.
         * NOTE: The object must have a mesh renderer with a valid material in order to be unhighlited.
         */
        public static void UnhighlightObject(GameObject obj, Color original) 
        {
            obj.GetComponent<MeshRenderer>().material.color = original;
        }

        /*
         * Unhighlights the passed in obj by setting the color to Color.white.
         * NOTE: The object must have a mesh renderer with a valid material in order to be unhighlited.
         */
        public static void UnhighlightObject(GameObject obj)
        {
            UnhighlightObject(obj, Color.white);
        }
    }
}
