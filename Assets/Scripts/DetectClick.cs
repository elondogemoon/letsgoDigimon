using UnityEngine;

public class DetectClick : MonoBehaviour
{
    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast�� Ŭ���� ��ġ ����
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Item"))
                {
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
