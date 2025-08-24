using UnityEngine;

public class GameEnums : MonoBehaviour
{
    public enum GameDifficulty
    {
        Easy, 
        Normal, 
        Hard,
        Expert
    }

    public enum DragoonType
    {
        Egg_Dragoon,
        Regular_Dragoon,           
        Long_Dragoon,        
        Chonky_Dragoon          
    }

    public enum EnemyType
    {
        Easy_Fish,
        Normal_Fish,
        Hard_Fish,
        Expert_Pepper
    }

    public static GameDifficulty SelectedDifficulty = GameDifficulty.Normal;

    public static DragoonType SelectedDragoon = DragoonType.Egg_Dragoon;

    public static EnemyType SelectedEnemy = EnemyType.Easy_Fish;
}
