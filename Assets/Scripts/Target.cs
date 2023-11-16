using UnityEngine;

public class Target : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject scoreMessage;
    private bool isHit = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        animator.Play("Start");
    }

    private void OnEnable()
    {
        Countdown.onCountdownEnd += OnCountdownEnd;
    }

    private void OnDisable()
    {
        Countdown.onCountdownEnd -= OnCountdownEnd;
    }

    private void OnCountdownEnd()
    {
        isHit = true;
        animator.Play("Go");
    }
    public void OnHit(Vector3 point)
    {
        if (isHit)
        {
            GameObject child = transform.GetChild(0).gameObject;
            Debug.Log(Vector3.Distance(point, child.transform.position));
            float distance = Vector3.Distance(point, child.transform.position);



            audioSource.Play();
            if (distance <= 0.25)
            {
                Instantiate(scoreMessage, point, Quaternion.identity).GetComponent<MessageScore>().SetScore("100");
            }
            else if (distance > 0.25 && distance <= 0.3)
            {
                Instantiate(scoreMessage, point, Quaternion.identity).GetComponent<MessageScore>().SetScore("50");
            }
            else if (distance > 0.3 && distance <= 0.4)
            {
                Instantiate(scoreMessage, point, Quaternion.identity).GetComponent<MessageScore>().SetScore("20");
            }
            else if (distance > 0.4)
            {
                Instantiate(scoreMessage, point, Quaternion.identity).GetComponent<MessageScore>().SetScore("Miss");
            }

            animator.Play("Target");
            Destroy(GetComponent<BoxCollider>());
        }

    }
}
