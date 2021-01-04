using System;
using UnityEngine;

namespace pixelook
{
    public class ScoreBalloon : MonoBehaviour
    {
        private void Start()
        {
            transform.position += Vector3.up * 1.4f;
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