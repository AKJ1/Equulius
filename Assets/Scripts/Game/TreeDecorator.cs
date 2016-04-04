namespace Assets.Scripts.Game
{
    using UnityEngine;

    public class TreeDecorator : MonoBehaviour
    {
        public Material[] WarmMaterials;

        public Material[] ColdMaterials;

        public TreeStyle Style;

        public GameObject[] CrownElements;

        public void Decorate()
        {
            var wantedMaterials = Style == TreeStyle.Warm
                ? WarmMaterials
                : Style == TreeStyle.Cold ? ColdMaterials : null;
            int rng = Random.Range(0, wantedMaterials.Length);
            foreach (var element in CrownElements)
            {
                element.GetComponent<Renderer>().material = wantedMaterials[rng];
            }
            
        }
    }

    public enum TreeStyle
    {
        Warm, Cold
    }
}