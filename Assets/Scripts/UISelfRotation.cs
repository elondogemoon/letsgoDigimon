using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISelfRotation : MonoBehaviour
{
    [SerializeField] GameObject EvolutionOutLine;
    [SerializeField] GameObject HighlightOutLine;
    [SerializeField] Button button;

    [SerializeField] float _rotationSpeed = 100f;
    private bool isRotating = false;
    private float rotationDuration = 3f;

    private void Update()
    {
        EvolutionOutLine.transform.Rotate(0, 0, 0.1f);

        if (button.interactable == true && !isRotating)
        {
            StartCoroutine(SmoothReverseRotate());
        }
        else
        {
            StopCoroutine(SmoothReverseRotate());
        }
    }

    public IEnumerator SmoothReverseRotate()
    {
        isRotating = true;

        float elapsedTime = 0f;
        float startSpeed = _rotationSpeed; 
        float targetSpeed = -_rotationSpeed; 

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;

            float newSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / rotationDuration);
            HighlightOutLine.transform.Rotate(0, 0, newSpeed * Time.deltaTime);

            yield return null; 
        }

        
        _rotationSpeed = targetSpeed;
        isRotating = false;
    }
}
