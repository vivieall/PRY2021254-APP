using UnityEngine;

public class LevelButtonListItem : ListItemManager {
    public int levelId;
    [SerializeField] public LevelUIComponent levelUIComponent;
    [SerializeField] public GameObject completedCheck;
}
