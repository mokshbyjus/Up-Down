using System;

[Serializable]
public class MonsterModel {
    public float speed;
    public Side spawnSide;
    public MonsterModel() {

    }
    public MonsterModel(float speed, Side side) {
        this.speed = speed;
        // this.spawnSide = side;
    }
    public int floor;
}

public enum State {
    MOVING,
    HITTING
}