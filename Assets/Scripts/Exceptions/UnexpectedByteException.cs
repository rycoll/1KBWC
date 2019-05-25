using System;

[Serializable()]
public class UnexpectedByteException : System.Exception {
    public UnexpectedByteException() : base() {}
    public UnexpectedByteException(string message) : base(message) {}
    public UnexpectedByteException(string message, System.Exception inner) : base(message, inner) {}
    protected UnexpectedByteException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context
    ) : base(info, context) {}
}