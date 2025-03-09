using UnityEngine;

public class SafePanel : MonoBehaviour
{
    RectTransform rectTransform;
    Rect safeArea;
    Vector2 minAnchor;
    Vector2 maxAnchor;

    [SerializeField] float BannerSizeY = 0;

    void Awake()
    {
        SafeAreaCalculate();
    }

    private void FixedUpdate()
    {
        SafeAreaCalculate();
    }

    public void SafeAreaCalculate()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;



        Vector2 offsetMin = rectTransform.offsetMin;

        offsetMin.y = BannerSizeY;

        rectTransform.offsetMin = offsetMin;

    }
}