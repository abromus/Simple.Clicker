using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.UI
{
    public class Management : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelInfo;
        [SerializeField] private TMP_Text _incomeInfo;

        [Space]
        [SerializeField] private Button _levelUp;
        [SerializeField] private TMP_Text _levelUpInfo;

        [Space]
        [SerializeField] private Improvement _improvementPrefab;
        [SerializeField] private Transform _improvementContainer;
    }
}
