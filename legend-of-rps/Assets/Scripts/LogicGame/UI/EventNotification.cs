using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventNotification : MonoBehaviour
{
    RectTransform eventNoti;

    private float speed = 1.0f;
    private float boundTextEnd = -1413.0f;
    private float beginTextPos = -1900.0f;

    [SerializeField] private bool isLoop = false;
    [SerializeField] private TextMeshProUGUI notification;
    private GameService gameService = GameService.@object;

    private string notifyDemo = "This is a offical game, we will give you 50 coins to experience the game now \t\t\t";
    private string notifyDemo1 = "";

    // Start is called before the first frame update
    void Start()
    {
        eventNoti = gameObject.GetComponent<RectTransform>();
        StartCoroutine(AutoSlidingText());
    }

    private void FixedUpdate()
    {
        notifyDemo = !string.IsNullOrEmpty(gameService.jackpotNotify.Value) ? gameService.jackpotNotify.Value : notifyDemo;
    }

    IEnumerator AutoSlidingText()
    {


        while (transform.localPosition.x > boundTextEnd)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.localPosition.x <= boundTextEnd)
            {
                eventNoti.localPosition = Vector2.left * beginTextPos;

                notification.text = notifyDemo;
            }

            yield return null;
        }
    }
}
