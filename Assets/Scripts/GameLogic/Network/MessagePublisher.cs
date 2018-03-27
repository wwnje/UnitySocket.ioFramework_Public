using UnityEngine;

public class MessagePublisher : IMessagePublisher
{
    public void Public(object message)
    {
        Debug.Log("message");
    }
}
