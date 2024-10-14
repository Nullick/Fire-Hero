using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BoosterDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Booster _booster;

    [SerializeField] private Text _name;
    [SerializeField] private Text _description;

    public static event Action<BoosterType> BoosterClicked;

    private void Start()
    {
        _name.text = _booster.Name;
        _description.text = _booster.Description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BoosterClicked?.Invoke(_booster.BoosterType);
    }
}