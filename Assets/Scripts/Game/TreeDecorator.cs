namespace Assets.Scripts.Game
{
    using UnityEngine;

    public class TreeDecorator : MonoBehaviour
    {
        public Material[] WarmMaterials;

        public Material[] ColdMaterials;

        public TreeStyle Style;

        public GameObject Crown;

        public void Decorate()
        {
            var wantedMaterials = Style == TreeStyle.Warm
                ? WarmMaterials
                : Style == TreeStyle.Cold ? ColdMaterials : null;
            Crown.GetComponent<Renderer>().material = wantedMaterials[Random.Range(0, wantedMaterials.Length)];
        }
    }

    public enum TreeStyle
    {
        Warm, Cold
    }
}