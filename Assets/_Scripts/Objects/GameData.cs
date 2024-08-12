using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "SaveSystem/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public enum GameViewType { WASD,Click,Stick}
    public GameViewType GameView;
    
}
