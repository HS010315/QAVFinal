using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public Text scoreText; // 점수를 표시할 UI 텍스트

    private int score = 0; // 현재 점수
    private int maxScore = 20; // 최대 점수

    private XRGrabInteractable grabInteractable; // VR 상호작용 컴포넌트

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        grabInteractable = GetComponent<XRGrabInteractable>(); // XRGrabInteractable 컴포넌트 할당
    }

    private void Start()
    {
        grabInteractable.onSelectEntered.AddListener(OnSelectEntered);
        UpdateScoreText();
    }

    private void OnDestroy()
    {
        if (grabInteractable != null && grabInteractable.onSelectEntered != null)
        {
            grabInteractable.onSelectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(XRBaseInteractor interactor)
    {
        // 점수 증가
        AddScore(1);
    }

    public void AddScore(int value)
    {
        score = Mathf.Clamp(score + value, 0, maxScore); // 최대값을 20으로 제한
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString() + "/" + maxScore.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}