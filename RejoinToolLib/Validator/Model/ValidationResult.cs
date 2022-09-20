namespace RejoinToolLib.Validator.Model;

using System;

public record struct ValidationResult<T>
	(T Property, string Reason) where T : struct, IConvertible {
	public override string ToString() => $"{Property.ToString()}: {Reason}";
}
