using UnityEngine;
using UnityEngine.UIElements;

public class UserInterface : MonoBehaviour
{
    [SerializeField] SimulationManager simManager;
    [SerializeField] UIDocument uiDocument;
    private Button initializeButton;
    private Button stepButton;
    private Button resetButton;
    // Rows
    private IntegerField M;
    // Cols
    private IntegerField N;
    private FloatField pctAlive;
    void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
        // Allows the browser to properly pass text inputs into the fields
        WebGLInput.captureAllKeyboardInput = false;
        #endif
        
        VisualElement root = uiDocument.rootVisualElement;

        initializeButton = root.Query<Button>("Initialize-Button");
        stepButton = root.Query<Button>("Step-Button");
        resetButton = root.Query<Button>("Reset-Button");
        M = root.Query<IntegerField>("M");
        N = root.Query<IntegerField>("N");
        pctAlive = root.Query<FloatField>("pctAlive");

        initializeButton.clicked += Initialize;
    }

    // Initializing allows you to step and reset
    void Initialize()
    {
        int mData = Mathf.Clamp(M.value, 1, 200);
        int nData = Mathf.Clamp(N.value, 1, 280);
        float pctAliveData = Mathf.Clamp(pctAlive.value, 0, 100);

        simManager.Initialize(mData, nData, pctAliveData);

        initializeButton.clicked -= Initialize;
        stepButton.clicked += simManager.Step;

        resetButton.clicked += simManager.Reset;
        resetButton.clicked += this.Reset;
    }

    // Can't step or reset if the board is clear
    void Reset()
    {
        initializeButton.clicked += Initialize;
        stepButton.clicked -= simManager.Step;
        resetButton.clicked -= simManager.Reset;
        resetButton.clicked -= this.Reset;
    }
}
