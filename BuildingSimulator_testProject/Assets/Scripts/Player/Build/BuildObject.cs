using UnityEngine;

namespace Assets.Scripts.Player.Build
{
    public class BuildObject : MonoBehaviour
    {
        [field: SerializeField] public LayerMask TargetLayer { get; private set; }

        public Collider Collider { get; private set; }
        public Renderer Renderer { get; private set; }

        public Material Material { get; set; }

        public bool CanToPut { get; set; }

        private void Start()
        {
            Collider = GetComponent<Collider>();
            Renderer = GetComponent<Renderer>();

            Material = Renderer.material;
        }

        public void SetBuildMode(bool active)
        {
            Collider.isTrigger = active;

            if (!active)
                ChangeToStartMaterial();
        }

        public void ChangeToStartMaterial()
        {
            Renderer.material = Material;
        }
    }
}