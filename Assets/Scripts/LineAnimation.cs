using UnityEngine;

public class LineAnimation : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private float speed = 7f;

    public bool play;
    public bool forward = true;

    public Vector3 direction;
    public Vector3[] positiononsDrigin;

    void Start()
    {
        var count = line.positionCount;
        positiononsDrigin = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            positiononsDrigin[i] = line.GetPosition(i);
        }

        var lastPoint = line.GetPosition(count - 1);
        direction = lastPoint - line.GetPosition(count - 2);
    }

    public void Play(bool forwardDirection)
    {
        StopAndReset();
        forward = forwardDirection;
        play = true;
    }

    public void StopAndReset()
    {
        play = false;
        if (line == null || positiononsDrigin == null) return;

        line.positionCount = positiononsDrigin.Length;
        line.SetPositions(positiononsDrigin);
    }

    void Update()
    {
        if (!play) return;
        if (!line || line.positionCount < 2) return;

        if (forward) AnimationForward();
        else AnimationBackward();
    }

    private void AnimationForward()
    {
        var count = line.positionCount;

        var lastPoint = line.GetPosition(count - 1);
        lastPoint += direction.normalized * speed * Time.deltaTime;
        line.SetPosition(count - 1, lastPoint);

        var tailPoint = line.GetPosition(0);
        var tailDirection = line.GetPosition(1) - tailPoint;
        tailPoint += tailDirection.normalized * speed * Time.deltaTime;
        line.SetPosition(0, tailPoint);

        if (Vector3.Distance(tailPoint, line.GetPosition(1)) >= 0.1f) return;

        var newcount = count - 1;
        var newPositions = new Vector3[newcount];

        for (int i = 1; i < count; i++)
        {
            newPositions[i - 1] = line.GetPosition(i);
        }

        line.positionCount = newcount;
        line.SetPositions(newPositions);
    }

    private void AnimationBackward()
    {
        var countCurrent = line.positionCount;
        var countOrigin = positiononsDrigin.Length;

        var tailPosition = line.GetPosition(0);
        var originTailPosition = positiononsDrigin[0];

        if (countCurrent >= countOrigin &&
            Vector3.Distance(tailPosition, originTailPosition) < 0.1f)
        {
            play = false;
            return;
        }

        var positionHeadOrigin = positiononsDrigin[^1];

        var indexHead = countCurrent - 1;
        var headPosition = line.GetPosition(indexHead);

        if (Vector3.Distance(positionHeadOrigin, headPosition) > 0.1f)
        {
            headPosition -= direction.normalized * speed * Time.deltaTime;
            line.SetPosition(indexHead, headPosition);
        }

        var indexTarget = countOrigin - countCurrent;
        var positionTail = line.GetPosition(0);
        var target = positiononsDrigin[indexTarget];

        var dir = target - positionTail;
        positionTail += dir.normalized * speed * Time.deltaTime;
        line.SetPosition(0, positionTail);

        if (Vector3.Distance(positionTail, target) >= 0.1f) return;

        var newCount = countCurrent + 1;
        var newPositions = new Vector3[newCount];

        newPositions[0] = target;
        newPositions[1] = target;

        for (int i = 0; i < countCurrent; i++)
        {
            newPositions[i + 1] = line.GetPosition(i);
        }

        line.positionCount = newCount;
        line.SetPositions(newPositions);
    }
}