using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

#endif

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [Header("Values")] [SerializeField] private int minimum = 0;
    [SerializeField] private int maximum = 100;
    [SerializeField] private int current = 0;

    [Header("Components")] [SerializeField]
    private Image mask;

    [SerializeField] private Image fill;
    public Color fillColor;


    // Update is called once per frame
    private void Update()
    {
        GetCurrentFill();
      }

    /// <summary>
    ///     Calculate the actual value for adding to the progress Bar, this won't need to reset the values, as it uses offsets
    /// </summary>
    private void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        var fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
        fill.color = fillColor;
    }

    private void OnValidate()
    {
        current = Mathf.Clamp(current, 0, maximum);
    }

#if UNITY_EDITOR
    /// <summary>
    ///     Adds to Unity Menu (UI => A Linear Progress bar and instantiate it, as child of the selected Game Object in
    ///     Hierarchy)
    /// </summary>
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/Linear"));
        if (Selection.activeObject != null)
        {
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
        }
    }

    /// <summary>
    ///     Adds to Unity Menu (UI => A Radius Progress bar and instantiate it, as child of the selected Game Object in
    ///     Hierarchy)
    /// </summary>
    [MenuItem("GameObject/UI/Radius Progress Bar")]
    public static void AddRadiusProgressBar()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/Radius"));
        if (Selection.activeObject != null)
        {
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
        }

       
    }

#endif

    #region GettersAndSetters

    public int Minimum
    {
        get => minimum;
        set => minimum = value;
    }

    public int Maximum
    {
        get => maximum;
        set => maximum = value;
    }

    public int Current
    {
        get => current;
        set => current = value;
    }

    #endregion
}