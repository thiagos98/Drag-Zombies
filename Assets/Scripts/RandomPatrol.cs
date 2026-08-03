using UnityEngine;

public class RandomPatrol : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float secondsToMaxDifficulty = 10f;

    private float speed;
    private Vector2 targetPosition;

    private void Start()
    {
        targetPosition = GetRandomPosition();
        speed = minSpeed;
    }

    private void Update()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPercent());
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            targetPosition = GetRandomPosition();
        }
    }

    private Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    private float GetDifficultyPercent()
    {
        if (secondsToMaxDifficulty <= 0f)
        {
            return 1f;
        }

        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
