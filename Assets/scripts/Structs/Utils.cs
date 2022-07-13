using System.Collections;
using UnityEngine;

namespace XEntity
{
    //Utils��ũ��Ʈ �̰� ��¥ �� ��Ƴ׿�. ���� �� �𸣰ڽ��ϴ�. ��������� �غþ��.
    //�׷��� Ȯ������ ������ �츰 ���� �ǵ��� �ʿ䰡 ���� �����. ���ο� ����� ������� �ƴ� �̻�����. ���� ���⼭ ��������.

    //�� �������� �κ��丮 �ý���, ��ü ���� �� ��ü ���� ǥ�ÿ� ���� ��ƿ��Ƽ ����� ���ԵǾ� �ֽ��ϴ�.
    //�� �� ��ũ��Ʈ�� �Ѱ��� �ƴ� �پ��ϰ� ���� �� �ִ� ��ũ��Ʈ�Դϴ�.
    public readonly struct Utils
    {
        /*
        �� �޼���� Ʈ���� ItemSlot���� ��� ItemSlot���� �׸��� �����Ϸ��� �մϴ�.
        �׸� ������ �����ϸ� ��� ItemSlot�� �ִ� �뷮�� ������ ������ ���Դϴ�.
        �׸� ������ �ٸ��� ��ġ�� �ٲ�ϴ�.
        */

        //���� ���⿣? Shuffle�ҷ��� �ϴ°� ������ ���� ��Ȯ�ϰ� �𸣰���.
        //Shuffle�� A�� B�� Object�ڸ��� B�� A�� ��ȯ���ִ� ���� �ǹ���. 
        public static void TransferItem(ItemSlot trigger, ItemSlot target)
        {
            //���� A�Ű������� B�Ű������� ���ٸ�
            if (trigger == target)
            {
                //�� �Լ�(TransferItem)�� ����
                return;
            }

            //
            Item triggerItem = trigger.slotItem;
            Item targetItem = target.slotItem;

            int triggerItemCount = trigger.itemCount;

            //���� �����۽��� ��ũ��Ʈ�� True (�� IsEmpty�� 0���϶��)
            if (!trigger.IsEmpty)
            {
                //���� 0���ϰų� Ư�������۰� Ÿ�پ������� ���ٸ�
                if (target.IsEmpty || targetItem == triggerItem)
                {
                    //������ŭ �ݺ�
                    for (int i = 0; i < triggerItemCount; i++)
                    {
                        //���� B�Ű����� �߰����Ѵٸ�
                        if (target.Add(triggerItem))
                        {
                            //A�Ű����� ����
                            trigger.Remove(1);
                        }
                        //�ƴ϶��
                        else
                        {
                            //����
                            return;
                        }
                    }
                }
                //0���ϰų� Ư�������۰� Ÿ�پ������� �����ʴٸ�
                else
                {
                    //
                    int targetItemCount = target.itemCount;

                    //�ϴ� �ʱ�ȭ
                    target.Clear();
                    for (int i = 0; i < triggerItemCount; i++) target.Add(triggerItem);

                    //�ϴ��ʱ�ȭ
                    trigger.Clear();
                    for (int i = 0; i < targetItemCount; i++) trigger.Add(targetItem);
                }
            }
        }

        //�� �޼���� ���޵� �׸��� ���� Ʈ���� ItemSlot���� ��� ItemSlot���� �����Ϸ��� �õ��մϴ�.
        public static void TransferItemQuantity(ItemSlot trigger, ItemSlot target, int amount) 
        {
            //isEmpty�� False��
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
            �� �ڷ�ƾ�� Vector3�� ô������ ���޵� obj�� ô���� ��ô�˴ϴ�.durationInFrames�� �������� ���޵� maxScale�� ���� 0�Դϴ�.
            ����: �̰��� MonoBehavior ��ũ��Ʈ���� ȣ���ؾ� �ϸ� StartCoroutine()�� ����Ͽ� ȣ���ؾ� �մϴ�.
            ��: �����ڷ�ƾ(GameUtility)�Դϴ�.TweenScaleIn(��: GameObject, 40, Vector 3.1);
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
            �� �ڷ�ƾ�� ���޵� ������Ʈ�� ������ �����Ͽ��� ���� 3�� �����Ϸ� �����Ͼƿ��Ѵ�.���� �ð� ����(InFrames)���� 0.
            �ı��� ���� ��� ��ü�� Ȯ�� �� �ı��˴ϴ�.
            �ı��� false�� ��� ��ü�� Ȱ�� ���´� Ȯ�� �� false�� �����˴ϴ�.
            ����: �̰��� MonoBehavior ��ũ��Ʈ���� ȣ���ؾ� �ϸ� StartCoroutine()�� ����Ͽ� ȣ���ؾ� �մϴ�.
            ��: �����ڷ�ƾ(GameUtility)�Դϴ�.TweenScaleOut(��: GameObject, 40, true);
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

        //���޵� ��ġ���� ���޵� �׸����� �׸� ������ ��ü�� �ν��Ͻ�ȭ�մϴ�.
        //���� ��� �κ��丮���� �׸��� ������ �� ���ŵ� �׸��� �ִ� �׸� �����Ⱑ �÷��̾� �տ� ���̴� ��찡 �ֽ��ϴ�.
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
