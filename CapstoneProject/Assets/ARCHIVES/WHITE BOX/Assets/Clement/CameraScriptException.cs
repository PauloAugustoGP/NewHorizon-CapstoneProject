using System;
using System.Runtime.Serialization;

[Serializable]
internal class CameraScriptException : Exception {
    public CameraScriptException() {
    }

    public CameraScriptException(string message) : base(message) {
    }

    public CameraScriptException(string message, Exception innerException) : base(message, innerException) {
    }

    protected CameraScriptException(SerializationInfo info, StreamingContext context) : base(info, context) {
    }
}