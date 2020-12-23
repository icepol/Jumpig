using System;
using System.Collections;
using UnityEngine;

namespace pixelook
{
    public class ScoreBalloon : MonoBehaviour
    {
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