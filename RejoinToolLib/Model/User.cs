namespace RejoinToolLib.Model;

public record struct User (string Id) {
	public string ToWebSiteURI() => $"https://vrch.at/{Id}";
}
