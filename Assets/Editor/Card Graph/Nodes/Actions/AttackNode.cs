using UnityEngine;

public class AttackNode : CardActionNode
{
    public AttackNode() : base("Attack Card", new[] { new(typeof(GraphicsBuffer.Target), "Source"), new PortInfo(typeof(GraphicsBuffer.Target)) }, null)
    {
    }
}