public class EnemyFSM
{
    private int[,] fsm;
    private int currentState;

    public EnemyFSM(int stateCount, int eventCount, int initState)
    {

        fsm = new int[stateCount, eventCount];

        for (int i = 0; i < stateCount; i++)
        {
            for (int j = 0; j < eventCount; j++)
            {
                fsm[i, j] = -1;
            }
        }

        currentState = initState;
    }

    public void SetRelation(int srcState, int evt, int dstState)
    {
        fsm[srcState, evt] = dstState;
    }

    public void SendEvent(int evt)
    {
        if (fsm[currentState, evt] != -1)
        {
            currentState = fsm[currentState, evt];
        }
    }

    public int GetState()
    {
        return currentState;
    }
}