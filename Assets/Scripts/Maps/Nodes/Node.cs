using Fantazee.Maps.Nodes.Information;
using Fantazee.Maps.Settings;
using Shapes;
using UnityEngine.Splines;

namespace Fantazee.Maps.Nodes
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        private NodeType type;
        public NodeType Type 
        {
            get => type; 
            set => type = value; 
        }
        
        [SerializeField]
        private List<Node> next;
        public List<Node> Next => next;
        
        [SerializeField]
        private SplineContainer splineContainer;
        public SplineContainer SplineContainer => splineContainer;
        
        [SerializeField] 
        private Disc disc;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private GameObject highlightGroup;

        [SerializeField]
        private List<Disc> highlightDiscs = new();

        [SerializeField]
        private float highlightRadius = 0.5f;

        [SerializeField]
        private float highlightDif = 0.1f;
        
        public void OnValidate()
        {
            if (MapSettings.Settings.NodeInformation.TryGetInformation(Type, out NodeInformation info))
            {
                if (disc != null)
                {
                    disc.Color = info.Color;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = info.Sprite;
                }

                foreach (var h in highlightDiscs)
                {
                    h.Color = info.Color;
                }
            }
        }

        private void Awake()
        {
            foreach (var h in highlightDiscs)
            {
                h.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            foreach (var h in highlightDiscs)
            {
                float t = Mathf.Sin(Time.time * 1.5f);
                float r = highlightRadius + t * highlightDif;
                h.Radius = r;
            }
        }

        public void SetHighlight(bool set)
        {
            foreach (var h in highlightDiscs)
            {
                if (h == null)
                {
                    continue;
                }
                h.gameObject.SetActive(set);
            }
        }
        
        public override string ToString()
        {
            return $"{name} - {transform.position}";
        }
    }
}