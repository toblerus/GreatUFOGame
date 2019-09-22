using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class UfoService : MonoBehaviour
    {
        public static UfoService Instance { get; private set; }

        public UfoController Ufo { get; private set; }
        public UfoController Alien { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            var ufoControllers = FindObjectsOfType<UfoController>();
            Ufo = ufoControllers.First(controller => controller.name.Contains("Ufo"));
            Alien = ufoControllers.First(controller => controller.name.Contains("alien"));
            Alien.gameObject.SetActive(false);
            if (Ufo == null)
                Debug.LogError("No Ufo was found in the scene.");
        }
    }
}