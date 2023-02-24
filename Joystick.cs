using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum inputJoystick
{
    left,
    right,
    none
}
public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image JoystickBG;
    [SerializeField] private Image JoystickCentner;
    public static inputJoystick _joystickInput = inputJoystick.none;
    private Vector2 inputVector;
    private Vector2 position;
    private void Start()
    {
        JoystickBG = GetComponent<Image>();
        JoystickCentner = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        JoystickCentner.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBG.rectTransform,eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / JoystickBG.rectTransform.sizeDelta.x * 1.2f);
            position.y = (position.y / JoystickBG.rectTransform.sizeDelta.x * 1.2f);

            inputVector = new Vector2(position.x, position.y);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            JoystickCentner.rectTransform.anchoredPosition = new Vector2(inputVector.x * (JoystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (JoystickBG.rectTransform.sizeDelta.y / 2));
        }
    }
    public inputJoystick HorizontalMove()
    {
        if (inputVector.x > 0)
            return _joystickInput = inputJoystick.right;
        if (inputVector.x < 0)
            return _joystickInput = inputJoystick.left;
        else
            return _joystickInput = inputJoystick.none;
    }
}
