using UnityEngine;
using UnityEngine.UI;

namespace pixelook
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private Text score;
        
        [SerializeField] private float tapTimeout = 0.2f;

        private float _currentTapTimeout;
        private int _tapCount;
        private Vector3 _position;

        void Update()
        {
            UpdateScore();
            CheckTimeout();
        }

        private void UpdateScore()
        {
            score.text = GameState.Score.ToString();
        }
        
        void CheckTimeout()
        {
            if (_tapCount == 0) return; // nothing to check

            _currentTapTimeout -= Time.deltaTime;

            if (!(_currentTapTimeout <= 0)) return;
            
            EventManager.TriggerEvent(_tapCount > 1 ? Events.DOUBLE_TAP : Events.SINGLE_TAP, _position);
            _tapCount = 0;
        }

        void OnTap()
        {
            if (_tapCount == 0)
                _currentTapTimeout = tapTimeout;

            _tapCount++;
        }

        public void OnButtonLeftClick()
        {
            OnTap();

            _position = new Vector3(-_tapCount, 0, 1);
        }

        public void OnButtonRightClick()
        {
            OnTap();

            _position = new Vector3(+_tapCount, 0, 1);
        }
    }
}