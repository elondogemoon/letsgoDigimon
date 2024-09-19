using UnityEngine;

public class DetectClick : MonoBehaviour
{
    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast로 클릭한 위치 감지
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
