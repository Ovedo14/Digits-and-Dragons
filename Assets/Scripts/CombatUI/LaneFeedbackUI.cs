using UnityEngine;
using TMPro;
using System.Collections;

public class LaneFeedbackUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _resultTexts;
    [SerializeField] private TextMeshProUGUI[] _comparisonTexts;
    [SerializeField] private float _feedbackDuration = 3f;

    [SerializeField] private EnemyManager _enemyManager;

    void OnEnable()
    {
        EventBus.Subscribe<OnLanesResolved>(HandleLanesResolved);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<OnLanesResolved>(HandleLanesResolved);
    }

    private void HandleLanesResolved(OnLanesResolved evt)
    {
        StartCoroutine(ShowFeedback(evt.LaneResults));
    }

    private IEnumerator ShowFeedback(float[] results)
    {
        for (int i = 0; i < 3; i++)
        {
            LaneEquation eq = _enemyManager.GetLaneEquation(i);
            float enemyValue = CalculateEquationValue(eq);
            
            _resultTexts[i].text = enemyValue.ToString("0.#");
            
            if (results[i] > 0)
                _comparisonTexts[i].text = ">";
            else if (results[i] < 0)
                _comparisonTexts[i].text = "<";
            else
                _comparisonTexts[i].text = "=";
        }

        yield return new WaitForSeconds(_feedbackDuration);

        for (int i = 0; i < 3; i++)
        {
            _resultTexts[i].text = "";
            _comparisonTexts[i].text = "";
        }

        //publish feedback is done
        EventBus.Publish(new OnFeedbackComplete());
    }

    private float CalculateEquationValue(LaneEquation equation)
    {
        switch (equation.OpType)
        {
            case OperationType.Add:
                float sum = 0;
                foreach (float term in equation.Terms)
                    sum += term;
                return sum;

            case OperationType.Mult:
                float product = 1;
                foreach (float term in equation.Terms)
                    product *= term;
                return product;

            case OperationType.Div:
                return equation.Terms[0] / equation.Terms[1];

            default:
                return 0;
        }
    }
}