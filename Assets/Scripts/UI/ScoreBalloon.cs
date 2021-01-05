using UnityEngine;

namespace pixelook
{
    public class ScoreBalloon : MonoBehaviour
    {
        [SerializeField] private float positionOffset = 1;
        private void Start()
        {
            transform.position += Vector3.up * positionOffset;
        }

        public void SetScore(int score)
        {
            foreach (TextMesh textMesh in GetComponentsInChildren<TextMesh>())
                textMesh.text = $"+{score}";
        }

        public void Destroy()
        {
            if (gameObject)
                Destroy(gameObject);
        }
    }
}