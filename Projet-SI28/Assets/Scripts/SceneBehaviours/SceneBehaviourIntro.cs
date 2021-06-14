using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneBehaviourIntro : SceneBehaviour
{
	[SerializeField] GameManager gm;
	[SerializeField] FadeAnimation fade_anim;
	[SerializeField] GameObject enter_name_ui;
	[SerializeField] TMP_InputField input;

	bool entering_name;
	int dial_num;

	void Start()
	{
		dial_num = 0;
		fade_anim.gameObject.SetActive(true);
		LoadDialogue(dial_num);
		StartCoroutine(LaunchDialogue(fade_anim.duration + .5f));
		entering_name = false;
	}

	public override void OnDialogueFinished()
	{
		switch (dial_num)
		{
			case 0:
				enter_name_ui.SetActive(true);
				input.ActivateInputField();
				entering_name = true;
				gm.SetState(STATE.DIALOGUE);
				break;
			case 1:
				gm.SetState(STATE.PLAYING);
				break;
		}
		dial_num++;
	}

	public void EnteredName()
	{
		LeanTween.scale(enter_name_ui, Vector3.zero, .5f).setEase(LeanTweenType.easeInSine);
		Invoke("DeactivateEnterName", .5f);
		LoadDialogue(dial_num);
		StartCoroutine(LaunchDialogue(1f));
		gm.SetState(STATE.PLAYING);
	}

	void DeactivateEnterName()
	{
		entering_name = false;
		enter_name_ui.SetActive(false);
	}

	public void OnPressEnter(InputAction.CallbackContext value)
	{
		if (value.started && entering_name)
		{
			EnteredName();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerMovement pm = collision.GetComponentInChildren<PlayerMovement>();
		if (pm != null)
		{
			pm.StopGravity();
			fade_anim.PlayAnimBack();
			Invoke("ChangeScene", 1f);
		}
	}

	void ChangeScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
