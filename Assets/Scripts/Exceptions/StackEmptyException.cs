using System;

[Serializable()]
public class StackEmptyException : System.Exception {
    public StackEmptyException() : base() {}
    public StackEmptyException(string message) : base(message) {}
    public StackEmptyException(string message, System.Exception inner) : base(message, inner) {}
    protected StackEmptyException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context
    ) : base(info, context) {}
}