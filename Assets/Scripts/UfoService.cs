using UnityEngine;

namespace DefaultNamespace
{
    public class UfoService : MonoBehaviour
    {
        public static UfoService Instance { get; private set; }

        private UfoController _ufo;
        public UfoController Ufo => _ufo;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _ufo = FindObjectOfType<UfoController>();
            if (_ufo == null)
                Debug.LogError("No Ufo was found in the scene.");
        }
    }
}