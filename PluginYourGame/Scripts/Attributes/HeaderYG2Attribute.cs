using UnityEngine;

namespace YG.Insides
{
    public class HeaderYG2Attribute : PropertyAttribute
    {
        public string header { get; private set; }
        public Color color { get; private set; }

        public HeaderYG2Attribute(string header)
        {
            this.header = header;
            color = new Color(1.0f, 0.5f, 0.0f);
        }

        public HeaderYG2Attribute(string header, float r, float g, float b)
        {
            this.header = header;
            color = new Color(r, g, b);
        }

        public HeaderYG2Attribute(string header, float r, float g, float b, float a)
        {
            this.header = header;
            color = new Color(r, g, b, a);
        }
    }
}
