using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class DragAndDrop : MonoBehaviour
{
    private bool movedAllowed;
    private Collider2D col;
    private Camera mainCamera;
    private GameMaster gm;
    private AudioSource source;

    [SerializeField] private AudioSource explosion;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GM")?.GetComponent<GameMaster>();
        col = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = mainCamera != null
            ? (Vector2)mainCamera.ScreenToWorldPoint(touch.position)
            : touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
            movedAllowed = col != null && col == touchedCollider;

            if (movedAllowed)
            {
                source?.Play();
            }

            Analytics.CustomEvent("inicioDoToque", new Dictionary<string, object>
            {
                { "x", touchPosition.x },
                { "y", touchPosition.y }
            });
        }

        if (touch.phase == TouchPhase.Moved)
        {
            if (movedAllowed)
            {
                transform.position = touchPosition;
            }

            Analytics.CustomEvent("movendoDedo", new Dictionary<string, object>
            {
                { "x", touchPosition.x },
                { "y", touchPosition.y }
            });
        }

        if (touch.phase == TouchPhase.Ended)
        {
            movedAllowed = false;

            Analytics.CustomEvent("fimDoToque", new Dictionary<string, object>
            {
                { "x", touchPosition.x },
                { "y", touchPosition.y }
            });
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            explosion?.Play();
            gm?.GameOver();
        }
    }
}
