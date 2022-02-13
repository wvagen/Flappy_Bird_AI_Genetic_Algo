
[System.Serializable]
public class MyDecision
{
    public int fitness;
    public byte decisionTaken; //0: means release || 1: means jump

    public MyDecision(int fitness, byte decisionTaken)
    {
        this.fitness = fitness;
        this.decisionTaken = decisionTaken;
    }

}
