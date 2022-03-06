using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Case.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Transform PanelBeginning;
        [SerializeField] private Button ButtonStart;
        [SerializeField] private Transform PanelLevelInfo;
        [SerializeField] private TextMeshProUGUI TextLevelInfo;


        public void Initiazlize()
        {
            ButtonStart.onClick.AddListener(OnButtonClickedStart);
            PanelBeginning.gameObject.SetActive(true);
            PanelLevelInfo.gameObject.SetActive(false);
        }


        private void SetLevelPanel()
        {
            string levelNumber = GameManager.Instance.LevelManager.ActiveLevelNumber.ToString();
            TextLevelInfo.text = string.Concat("Level ", levelNumber);
            PanelLevelInfo.gameObject.SetActive(true);
        }


        #region Button Events

        private void OnButtonClickedStart()
        {
            PanelBeginning.gameObject.SetActive(false);
            SetLevelPanel();
            GameManager.Instance.ActionOnGameStarted?.Invoke();
        }

        #endregion
    }
}