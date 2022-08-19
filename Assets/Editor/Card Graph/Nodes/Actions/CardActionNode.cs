public abstract class CardActionNode : CardNode
{
    protected CardActionNode(string title, PortInfo[] inputPortInfos, PortInfo[] outputPortInfos) : base(title,
        inputPortInfos, outputPortInfos)
    {
    }
}