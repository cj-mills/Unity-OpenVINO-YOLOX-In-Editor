using UnityEngine;
using TMPro;

public class BoundingBox
{
    // Contains the bounding box
    private GameObject bbox = new GameObject();
    // Contains the label text
    private GameObject text = new GameObject();
    // The canvas on which the bounding box labels will be drawn
    private GameObject canvas = GameObject.Find("Label Canvas");

    // The object information for the bounding box
    private Utils.Object info;

    // The object class color
    public Color color;

    // The adjusted line width for the bounding box
    public int lineWidth = (int)(Screen.width * 1.75e-3);
    // The adjusted font size based on the screen size
    private float fontSize = (float)(Screen.width * 9e-3);

    // The label text
    private TextMeshProUGUI textContent;

    // Indicates whether to render the bounding box on screen
    public bool renderBox = false;

    // The bounding box
    public Rect boxRect = new Rect();
    // The texture used for rendering the bounding box on screen
    public Texture2D boxTex = Texture2D.whiteTexture;


    /// <summary>
    /// Initialize the label for the bounding box
    /// </summary>
    /// <param name="label"></param>
    private void InitializeLabel()
    {
        // Set the label text
        textContent.text = $"{text.name}: {(info.prob * 100).ToString("0.##")}%";
        // Set the text color
        textContent.color = color;
        // Set the text alignment
        textContent.alignment = TextAlignmentOptions.MidlineLeft;
        // Set the font size
        textContent.fontSize = fontSize;
        // Resize the text area
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(250, 50);
        // Position the label above the top left corner of the bounding box
        Vector3 textPos = new Vector3(info.x0, info.y0, -10f);
        float xOffset = rectTransform.rect.width / 2;
        textPos = new Vector3(textPos.x + xOffset, textPos.y + textContent.fontSize, textPos.z);
        text.transform.position = textPos;
    }

    /// <summary>
    /// Toggle the visibility for the bounding box
    /// </summary>
    /// <param name="show"></param>
    public void ToggleBBox(bool show)
    {
        renderBox = show;
        text.SetActive(show);
    }

    /// <summary>
    /// Initialize the position and dimensions for the bounding box
    /// </summary>
    private void InitializeBBox()
    {
        // Set the position and dimensions
        boxRect = new Rect(info.x0, Screen.height - info.y0, info.width, info.height);

        // Make sure the bounding box is rendered
        ToggleBBox(true);
    }

    /// <summary>
    /// Update the object info for the bounding box
    /// </summary>
    /// <param name="objectInfo"></param>
    public void SetObjectInfo(Utils.Object objectInfo)
    {
        // Set the object info
        info = objectInfo;
        // Get the object class label
        bbox.name = Utils.object_classes[objectInfo.label].Item1;
        text.name = bbox.name;
        // Get the object class color
        color = Utils.object_classes[objectInfo.label].Item2;

        // Initialize the label
        InitializeLabel();
        // Initializ the position and dimensions
        InitializeBBox();
    }

    /// <summary>
    /// Constructor for the bounding box
    /// </summary>
    /// <param name="objectInfo"></param>
    public BoundingBox(Utils.Object objectInfo)
    {
        // Add a text componenet to store the label text
        textContent = text.AddComponent<TextMeshProUGUI>();
        // Assign text object to the label canvas
        text.transform.SetParent(canvas.transform);

        // Update the object info for the bounding box
        SetObjectInfo(objectInfo);
    }
}
