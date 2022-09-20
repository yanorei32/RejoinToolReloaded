namespace RejoinToolLib.Model;

using System.Linq;

public record struct User (string Value) {
	public string UserURI => $"https://vrch.at/{Value}";
}
