using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rodlix
{
    [System.Serializable]
    public class InfoOfBlockUI
    {
        public RectTransform InfoPanel;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Temperature;
        public TextMeshProUGUI Weight;
    }
}