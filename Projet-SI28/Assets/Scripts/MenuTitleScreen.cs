using UnityEngine;

public class MenuTitleScreen : MonoBehaviour
{ 
    [SerializeField] LeanTweenType tween_type;
    [SerializeField] float magnitude;
    [SerializeField] float speed;

    // Update is called once per frame
    void Start()
    {
        LeanTween.moveY(gameObject, transform.position.y - magnitude, speed).setLoopPingPong().setEase(tween_type);
    }
}
