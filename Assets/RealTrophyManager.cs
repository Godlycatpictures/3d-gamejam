using UnityEngine;
using UnityEngine.UIElements;

public class RealTrophyManager : MonoBehaviour
{
    void Start()
    {
        if (FindFirstObjectByType<TrophyManager>().hasTrophy)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
