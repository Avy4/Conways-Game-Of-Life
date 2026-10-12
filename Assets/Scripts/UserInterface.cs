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
        VisualElement root = uiDocument.rootVisualElement;

        initializeButton = root.Query<Button>("Initialize-Button");
        stepButton = root.Query<Button>("Step-Button");
        resetButton = root.Query<Button>("Reset-Button");
        M = root.Query<IntegerField>("M");
        N = root.Query<IntegerField>("N");
        pctAlive = root.Query<FloatField>("pctAlive");

        initializeButton.clicked += Initialize;
        resetButton.clicked += simManager.Reset;
        resetButton.clicked += this.Reset;
    }

    void Initialize()
    {
        int mData = Mathf.Clamp(M.value, 1, 200);
        int nData = Mathf.Clamp(N.value, 1, 280);
        float pctAliveData = Mathf.Clamp(pctAlive.value, 0, 100);

        simManager.Initialize(mData, nData, pctAliveData);

        initializeButton.clicked -= Initialize;
        stepButton.clicked += simManager.Step;
    }

    void Reset()
    {
        initializeButton.clicked += Initialize;
        stepButton.clicked -= simManager.Step;
    }
}
