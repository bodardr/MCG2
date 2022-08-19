using UnityEngine;

public class OnCardDamagedNode : CardTriggerNode
{
    public OnCardDamagedNode() : base("On Card Damaged",
        new PortInfo[] { new(typeof(GraphicsBuffer.Target)) })
    {
    }
}