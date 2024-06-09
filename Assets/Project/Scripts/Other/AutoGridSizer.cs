using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoGridSizer : MonoBehaviour
{
    [Tooltip("Will Be Called Inside the Update Method (Only Allowed In Unity Editor)")]
    [SerializeField] private bool isUpdatingPerFrame;
    
    private float width;
    private float height;
    private Vector2Int gridCounts;
    private RectTransform rectTransform;
    private GridLayoutGroup gridLayoutGroup;
    private Vector2 newCardSize;
    private int lastItemCount;
    private IEnumerator updateGridLayoutCoroutine;
    private WaitForSeconds waitForLayoutUpdate = new WaitForSeconds(0.5f);
    void Awake()
    {
        gridLayoutGroup ??= GetComponent<GridLayoutGroup>();
        rectTransform ??= GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        lastItemCount = -1;
        if (updateGridLayoutCoroutine != null)
        {
            StopCoroutine(updateGridLayoutCoroutine);
        }

        updateGridLayoutCoroutine = null;
    }

#if UNITY_EDITOR
    [TextArea(1,1)] public string info = "Update Function will not be part of build as screen Size Will be fixed";
    private void OnValidate()
    {
        info = "Update Function will not be part of build as screen Size Will be fixed";
    }
    void Update()
    {
        if (isUpdatingPerFrame)
            UpdateLayout();    
    }
#endif
    
    public void ResetAndUpdateGridLayout()
    {
        lastItemCount = -1;
        if (updateGridLayoutCoroutine != null)
        {
            StopCoroutine(updateGridLayoutCoroutine);
        }
                
        updateGridLayoutCoroutine = UpdateLayoutCoroutine();
        StartCoroutine(updateGridLayoutCoroutine);
    }

    private IEnumerator UpdateLayoutCoroutine()
    {
        gridLayoutGroup.enabled = true;
        UpdateLayout();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        yield return waitForLayoutUpdate;
        gridLayoutGroup.enabled = false;
        
    }
    private void UpdateLayout()
    {
        if (lastItemCount != rectTransform.childCount || isUpdatingPerFrame)
        {
            if (!gridLayoutGroup.isActiveAndEnabled)
                gridLayoutGroup.enabled = true;
            
            width = rectTransform.rect.width;
            height = rectTransform.rect.height;
            
            
            gridCounts = CalculateGrid(rectTransform.childCount);
            
            //Calculating the size of cards based on Width on Container
            gridLayoutGroup.constraintCount = gridCounts.x;
            newCardSize = new Vector2(width / gridCounts.x, width / gridCounts.x);

            //Checking and Adjusting, if calculated card size in complimenting the height of panel
            var verticalCount = gridCounts.y;
            var totalVeticalSize = verticalCount * newCardSize.y;
            if (totalVeticalSize > height)
            {
                //Calculating the size of cards based on Height on Container
                newCardSize = new Vector2(height / verticalCount, height / verticalCount);
            }
            
            gridLayoutGroup.cellSize = newCardSize;
            lastItemCount = rectTransform.childCount;
        }
    }
    
    /// <summary>
    /// This Algorithm is will help to create Square like grid view based on the number items 
    /// </summary>
    /// <param name="itemCount"></param>
    /// <returns></returns>
    private Vector2Int CalculateGrid(int itemCount)
    {
        var bestHorizontalCount = 1;
        var bestVerticalCount = itemCount;
        var minDifference = itemCount - 1; //Maximum possible Difference for a single Row or column
        
        for (int i = 1; i <= Mathf.Sqrt(itemCount); i++)
        {
            if (itemCount % i == 0)
            {
                var verticalCount = i;
                var horizontalCount = itemCount / i;
                var difference = Mathf.Abs(horizontalCount - verticalCount);
                
                //Check if this division gives us a Better shape
                if (difference < minDifference)
                {
                    bestHorizontalCount = horizontalCount;
                    bestVerticalCount = verticalCount;
                    minDifference = difference;
                }
                //If The difference is The same, prefer Horizontal rectangles
                else if (difference == minDifference && horizontalCount >= verticalCount)
                {
                    bestHorizontalCount = horizontalCount;
                    bestVerticalCount = verticalCount;
                }
            }
        }
        return new Vector2Int(bestHorizontalCount, bestVerticalCount);
    }
}