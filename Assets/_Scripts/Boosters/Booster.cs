using UnityEngine;

[CreateAssetMenu(fileName = "New Booster", menuName = "Booster")]
public class Booster : ScriptableObject
{
    public BoosterType BoosterType;
    public string Description;
    public string Name;
}
