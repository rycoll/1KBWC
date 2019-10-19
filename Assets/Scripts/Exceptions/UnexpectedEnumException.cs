using System;

[Serializable()]
public class UnexpectedEnumException : System.Exception {
    public UnexpectedEnumException() : base() {}
    public UnexpectedEnumException(string message) : base(message) {}
    public UnexpectedEnumException(string message, System.Exception inner) : base(message, inner) {}
    protected UnexpectedEnumException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context
    ) : base(info, context) {}
}