using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        public string description; // Описание задания
        public GameObject uiElement; // UI-элемент с подсказкой
        public InputActionProperty[] requiredActions; // Действия для выполнения этапа
        public bool requiresHold; // Нужно ли удерживать действие
        public int[] requiredPresses; // Количество нажатий для каждого действия
        public bool allowExternalCompletion; // Разрешить внешний вызов NextStep()
        public bool useVector2Input; // Использовать ли Vector2 вместо float
        public float joystickThreshold = 0.5f; // Порог для джойстика
    }

    public TutorialStep[] steps;
    public int currentStep = 0;
    private int[] actionPressCounts;

    void Start()
    {
        foreach (var step in steps)
        {
            if (step.uiElement)
                step.uiElement.SetActive(false);
        }

        if (steps.Length > 0)
        {
            actionPressCounts = new int[steps[currentStep].requiredActions.Length];
            ShowStep(currentStep);
        }
    }

    void Update()
    {
        if (currentStep >= steps.Length || steps[currentStep].allowExternalCompletion) return;

        bool stepCompleted = true;

        for (int i = 0; i < steps[currentStep].requiredActions.Length; i++)
        {
            if (steps[currentStep].useVector2Input)
            {
                Vector2 input = steps[currentStep].requiredActions[i].action.ReadValue<Vector2>();
                if (input.magnitude < steps[currentStep].joystickThreshold)
                {
                    stepCompleted = false;
                }
            }
            else if (steps[currentStep].requiresHold)
            {
                if (steps[currentStep].requiredActions[i].action.ReadValue<float>() <= 0)
                {
                    stepCompleted = false;
                }
            }
            else
            {
                if (steps[currentStep].requiredActions[i].action.WasPressedThisFrame())
                {
                    actionPressCounts[i]++;
                }
                if (actionPressCounts[i] < steps[currentStep].requiredPresses[i])
                {
                    stepCompleted = false;
                }
            }
        }

        if (stepCompleted)
        {
            NextStep();
        }
    }

    void ShowStep(int stepIndex)
    {

        // Включаем текущий UI элемент
        if (steps[stepIndex].uiElement)
        {
            steps[stepIndex].uiElement.SetActive(true);
        }

        // Обнуляем счетчик нажатий для нового этапа
        actionPressCounts = new int[steps[stepIndex].requiredActions.Length];
    }

    public void NextStep()
    {
        if (currentStep < steps.Length - 1)
        {
            currentStep++;
            ShowStep(currentStep);
        }
    }

    public void CompleteStepExternally(int stepIndex)
    {
        if (currentStep < steps.Length && steps[currentStep].allowExternalCompletion && currentStep == stepIndex)
        {
            NextStep();
        }
    }
}
