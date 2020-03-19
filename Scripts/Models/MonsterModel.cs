public class MonsterModel {
    public float speed;
    public Side spawnSide;
    public MonsterModel(float speed, Side side) {
        this.speed = speed;
        this.spawnSide = side;
    }
}

public enum State {
    MOVING,
    HITTING
}