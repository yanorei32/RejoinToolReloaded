namespace RejoinToolLib.Model;

public record struct User (string Value) {
	public string UserURI => $"https://vrch.at/{Value}";
}
