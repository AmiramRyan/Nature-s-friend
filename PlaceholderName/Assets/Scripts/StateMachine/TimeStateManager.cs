using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStateManager : GenericSingletonClass_TimeState<MonoBehaviour>
{
    public BaseTimeState currentTimeState;
    public List <Vector2> timesList;
    public bool readyToSpawn;

    //States
    public MorningTimeState morningTimeState = new MorningTimeState();
    public NoonTimeState noonTimeState = new NoonTimeState();
    public EveningTimeState  evneningTimeState= new EveningTimeState();
    public DaySwitchState daySwitchState = new DaySwitchState();
    public PauseTimeState pauseTimeState = new PauseTimeState();

    //States Data
    public bool timesSetForTheDayMorning;
    public bool timesSetForTheDayNoon;
    public bool timesSetForTheDayEvening;

    //Managers
    public GameManager gameManager;
    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        GameManager.MorningQueDone += MorningQue;
        GameManager.NoonQueDone += NoonQue;
        GameManager.EveningQueDone += EvningQue;
    }

    private void OnDisable()
    {
        GameManager.MorningQueDone -= MorningQue;
        GameManager.NoonQueDone -= NoonQue;
        GameManager.EveningQueDone -= EvningQue;
    }

    public void Start()
    {
        currentTimeState.EnterState(this);
    }

    public void Update()
    {
        currentTimeState.UpdateState(this);
    }

    public void SwtichState(BaseTimeState state)
    {
        currentTimeState = state;
        currentTimeState.EnterState(this);
    }

    public List<Vector2> CostumerTimeToArrive(int n, int minHour, int maxHour)
    {
        for (int i = 0; i < n; i++)
        {
            timesList.Add(new Vector2(Random.Range(minHour, maxHour), Random.Range(0, 59)));
        }
        return timesList;
    }

    public void StartSpawnDelayCo()
    {
        StartCoroutine(SpawnDelayCo());
    }
    private IEnumerator SpawnDelayCo()
    {
        yield return new WaitForSeconds(2f);
        readyToSpawn = true;
    }

    public void ResetSpawnQue() //from the dayswitch state only
    {
        timesSetForTheDayMorning = false;
        timesSetForTheDayNoon = false;
        timesSetForTheDayEvening = false;
    }

    #region StateQueResets

    public void MorningQue()
    {
        timesSetForTheDayMorning = true;
    }

    public void NoonQue()
    {
        timesSetForTheDayNoon = true;
    }
    public void EvningQue()
    {
        timesSetForTheDayEvening = true;
    }
    #endregion
}
