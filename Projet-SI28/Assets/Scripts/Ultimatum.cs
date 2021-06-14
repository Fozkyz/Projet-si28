using UnityEngine.InputSystem;
using UnityEngine;

public class Ultimatum : MonoBehaviour
{
	[SerializeField] SceneBehaviourIntrogun scene_behaviour;
	[SerializeField] GameManager gm;
	public RectTransform left_panel, right_panel;

	int selection;

    private void OnEnable()
    {
		selection = 0;
		ActivateUltimatum();
    }

    void ActivateUltimatum()
	{
		left_panel.gameObject.SetActive(true);
		right_panel.gameObject.SetActive(true);
		selection = 0;
		gm.SetState(STATE.ULTIMATUM);
	}

	void DeactivateUltimatum()
	{
		left_panel.gameObject.SetActive(false);
		right_panel.gameObject.SetActive(false);
		right_panel.anchorMin = new Vector2(.5f, 0f);
		right_panel.offsetMin = Vector2.zero;
		left_panel.anchorMax = new Vector2(.5f, 1f);
		left_panel.offsetMax = Vector2.zero;
		gm.SetState(STATE.PLAYING);
	}

	public void OnUltimatumLeft(InputAction.CallbackContext value)
	{
		if (value.started && gm.GetState() == STATE.ULTIMATUM)
		{
			if (selection == 1)
			{
				DeactivateUltimatum();
				scene_behaviour.Selection(selection);
			}
			else
			{
				selection = 1;
				right_panel.anchorMin = new Vector2(.6f, 0f);
				right_panel.offsetMin = Vector2.zero;
				left_panel.anchorMax = new Vector2(.6f, 1f);
				left_panel.offsetMax = Vector2.zero;
			}
		}
	}

	public void OnUltimatumRight(InputAction.CallbackContext value)
	{
		if (value.started && gm.GetState() == STATE.ULTIMATUM)
		{
			if (selection == 2)
			{
				DeactivateUltimatum();
				scene_behaviour.Selection(selection);
			}
			else
			{
				selection = 2;
				right_panel.anchorMin = new Vector2(.4f, 0f);
				right_panel.offsetMin = Vector2.zero;
				left_panel.anchorMax = new Vector2(.4f, 1f);
				left_panel.offsetMax = Vector2.zero;
			}
		}
	}

	public void OnUltimatumConfirm(InputAction.CallbackContext value)
	{
		if (value.started && selection != 0)
		{
			DeactivateUltimatum();
			scene_behaviour.Selection(selection);
		}
	}
}
