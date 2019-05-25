using System;

[Serializable()]
public class StackFullException : System.Exception {
    public StackFullException() : base() {}
    public StackFullException(string message) : base(message) {}
    public StackFullException(string message, System.Exception inner) : base(message, inner) {}
    protected StackFullException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context
    ) : base(info, context) {}
}