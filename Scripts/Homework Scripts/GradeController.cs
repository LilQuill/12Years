using UnityEngine;

public class GradeController : MonoBehaviour {

    [SerializeField] private Sprite[] grades;
    private SpriteRenderer render;
    private AudioSource audioSource;

    //timer references
    static TimeManager timer;
    static GameObject clock;

    //HomeworkManager reference
    HomeworkManager manager;

    public float displayTime = 1; //how long the grade stays on the screen for
    float startDisplay; //when the letter grade shows up

    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;

        clock = GameObject.Find("TimeManager");
        timer = (TimeManager)clock.GetComponent(typeof(TimeManager));
    }

    //showing a grade
    public void showGrade(char x)
    {
        switch (x)
        {
            case 'a':
                render.sprite = grades[0];
                break;
            case 'b':
                render.sprite = grades[1];
                break;
            case 'c':
                render.sprite = grades[2];
                break;
            case 'd':
                render.sprite = grades[3];
                break;
            case 'f':
                render.sprite = grades[4];
                break;
            default:
                Debug.Log("Input for GradeController.showGrade(char x) was not a grade.");
                break;
        }
        render.enabled = true;
        startDisplay = timer.GetTimeElapsed();
        audioSource.enabled = true;
        audioSource.Play();
    }

    //hides the grade
    public void hideGrade()
    {
        render.enabled = false;
    }

    //hides the grade after a while
    public void hideGradeAfterAWhile()
    {
        if (timer.GetTimeElapsed() == (startDisplay + displayTime))
        {
            hideGrade();
        }
    }


}
